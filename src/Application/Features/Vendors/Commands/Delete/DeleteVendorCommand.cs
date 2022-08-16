using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Constants.Application;
using DynaCore.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.Vendors.Commands.Delete
{
    public class DeleteVendorCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<DeleteVendorCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteVendorCommandHandler(IUnitOfWork<int> unitOfWork, IProductRepository productRepository, IStringLocalizer<DeleteVendorCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteVendorCommand command, CancellationToken cancellationToken)
        {
            var isVendorUsed = await _productRepository.IsVendorUsed(command.Id);
            if (!isVendorUsed)
            {
                var vendor = await _unitOfWork.Repository<Vendor>().GetByIdAsync(command.Id);
                if (vendor != null)
                {
                    await _unitOfWork.Repository<Vendor>().DeleteAsync(vendor);
                    await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllVendorsCacheKey);
                    return await Result<int>.SuccessAsync(vendor.Id, _localizer["Vendor Deleted"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Vendor Not Found!"]);
                }
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["Deletion Not Allowed"]);
            }
        }
    }
}