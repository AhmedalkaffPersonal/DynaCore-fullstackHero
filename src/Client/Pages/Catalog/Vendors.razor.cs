using DynaCore.Application.Features.Vendors.Commands.AddEdit;
using DynaCore.Application.Features.Vendors.Queries.GetAll;
using DynaCore.Client.Extensions;
using DynaCore.Client.Infrastructure.Managers.Catalog.Vendor;
using DynaCore.Shared.Constants.Application;
using DynaCore.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DynaCore.Client.Pages.Catalog
{
    public partial class Vendors
    {
        [Inject] private IVendorManager VendorManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllVendorsResponse> _vendorList = new();
        private GetAllVendorsResponse _vendor = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateVendors;
        private bool _canEditVendors;
        private bool _canDeleteVendors;
        private bool _canExportVendors;
        private bool _canSearchVendors;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateVendors = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Vendors.Create)).Succeeded;
            _canEditVendors = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Vendors.Edit)).Succeeded;
            _canDeleteVendors = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Vendors.Delete)).Succeeded;
            _canExportVendors = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Vendors.Export)).Succeeded;
            _canSearchVendors = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Vendors.Search)).Succeeded;

            await GetVendorsAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetVendorsAsync()
        {
            var response = await VendorManager.GetAllAsync();
            if (response.Succeeded)
            {
                _vendorList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await VendorManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task ExportToExcel()
        {
            var response = await VendorManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Vendors).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Vendors exported"]
                    : _localizer["Filtered Vendors exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                _vendor = _vendorList.FirstOrDefault(c => c.Id == id);
                if (_vendor != null)
                {
                    parameters.Add(nameof(AddEditVendorModal.AddEditVendorModel), new AddEditVendorCommand
                    {
                        Id = _vendor.Id,
                        Name = _vendor.Name,
                        Address = _vendor.Address,
                        TaxNumber = _vendor.TaxNumber,
                        PhoneNumber = _vendor.PhoneNumber
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditVendorModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _vendor = new GetAllVendorsResponse();
            await GetVendorsAsync();
        }

        private bool Search(GetAllVendorsResponse vendor)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (vendor.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (vendor.Address?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (vendor.PhoneNumber?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}