using Blazored.FluentValidation;
using DynaCore.Application.Features.Products.Commands.AddEdit;
using DynaCore.Application.Features.Vendors.Queries.GetAll;
using DynaCore.Application.Requests;
using DynaCore.Client.Extensions;
using DynaCore.Client.Infrastructure.Managers.Catalog.Product;
using DynaCore.Client.Infrastructure.Managers.Catalog.Vendor;
using DynaCore.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DynaCore.Client.Pages.Catalog
{
    public partial class AddEditProductModal
    {
        [Inject] private IProductManager ProductManager { get; set; }
        [Inject] private IVendorManager VendorManager { get; set; }

        [Parameter] public AddEditProductCommand AddEditProductModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllVendorsResponse> _vendors = new();

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            var response = await ProductManager.SaveAsync(AddEditProductModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            await LoadImageAsync();
            await LoadVendorsAsync();
        }

        private async Task LoadVendorsAsync()
        {
            var data = await VendorManager.GetAllAsync();
            if (data.Succeeded)
            {
                _vendors = data.Data;
            }
        }

        private async Task LoadImageAsync()
        {
            var data = await ProductManager.GetProductImageAsync(AddEditProductModel.Id);
            if (data.Succeeded)
            {
                var imageData = data.Data;
                if (!string.IsNullOrEmpty(imageData))
                {
                    AddEditProductModel.ImageDataURL = imageData;
                }
            }
        }

        private void DeleteAsync()
        {
            AddEditProductModel.ImageDataURL = null;
            AddEditProductModel.UploadRequest = new UploadRequest();
        }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                AddEditProductModel.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditProductModel.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.Product, Extension = extension };
            }
        }

        private async Task<IEnumerable<int>> SearchVendors(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _vendors.Select(x => x.Id);

            return _vendors.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
    }
}