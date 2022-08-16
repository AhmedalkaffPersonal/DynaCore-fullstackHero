using AutoMapper;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Constants.Application;
using DynaCore.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.Vendors.Commands.AddEdit
{
    public partial class AddEditVendorCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal TaxNumber { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }
    }

    internal class AddEditVendorCommandHandler : IRequestHandler<AddEditVendorCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditVendorCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditVendorCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditVendorCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditVendorCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var vendor = _mapper.Map<Vendor>(command);
                await _unitOfWork.Repository<Vendor>().AddAsync(vendor);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllVendorsCacheKey);
                return await Result<int>.SuccessAsync(vendor.Id, _localizer["Vendor Saved"]);
            }
            else
            {
                var vendor = await _unitOfWork.Repository<Vendor>().GetByIdAsync(command.Id);
                if (vendor != null)
                {
                    vendor.Name = command.Name ?? vendor.Name;
                    vendor.Address = command.Address ?? vendor.Address;
                    vendor.TaxNumber = (command.TaxNumber == 0) ? vendor.TaxNumber : command.TaxNumber;
                    vendor.PhoneNumber = command.PhoneNumber ?? vendor.PhoneNumber;
                    await _unitOfWork.Repository<Vendor>().UpdateAsync(vendor);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllVendorsCacheKey);
                    return await Result<int>.SuccessAsync(vendor.Id, _localizer["Vendor Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Vendor Not Found!"]);
                }
            }
        }
    }
}