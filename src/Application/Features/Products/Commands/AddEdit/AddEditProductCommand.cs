using AutoMapper;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Application.Interfaces.Services;
using DynaCore.Application.Requests;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.Products.Commands.AddEdit
{
    public partial class AddEditProductCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public string Description { get; set; }
        public string ImageDataURL { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }
        public UploadRequest UploadRequest { get; set; }
    }

    internal class AddEditProductCommandHandler : IRequestHandler<AddEditProductCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IStringLocalizer<AddEditProductCommandHandler> _localizer;

        public AddEditProductCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IUploadService uploadService, IStringLocalizer<AddEditProductCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditProductCommand command, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.Repository<Product>().Entities.Where(p => p.Id != command.Id)
                .AnyAsync(p => p.SKU == command.SKU, cancellationToken))
            {
                return await Result<int>.FailAsync(_localizer["SKU already exists."]);
            }

            var uploadRequest = command.UploadRequest;
            if (uploadRequest != null)
            {
                uploadRequest.FileName = $"P-{command.SKU}{uploadRequest.Extension}";
            }

            if (command.Id == 0)
            {
                var product = _mapper.Map<Product>(command);
                if (uploadRequest != null)
                {
                    product.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                }
                await _unitOfWork.Repository<Product>().AddAsync(product);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(product.Id, _localizer["Product Saved"]);
            }
            else
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(command.Id);
                if (product != null)
                {
                    product.Name = command.Name ?? product.Name;
                    product.Description = command.Description ?? product.Description;
                    product.SKU = command.SKU ?? product.Description;
                    if (uploadRequest != null)
                    {
                        product.ImageDataURL = _uploadService.UploadAsync(uploadRequest);
                    }
                    product.Quantity = (command.Quantity == 0) ? product.Quantity : command.Quantity;
                    product.VendorId = (command.VendorId == 0) ? product.VendorId : command.VendorId;
                    await _unitOfWork.Repository<Product>().UpdateAsync(product);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(product.Id, _localizer["Product Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Product Not Found!"]);
                }
            }
        }
    }
}