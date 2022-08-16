using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Constants.Application;
using DynaCore.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.ContactPersons.Commands.Delete
{
    public class DeleteContactPersonCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
    }

    internal class DeleteContactPersonCommandHandler : IRequestHandler<DeleteContactPersonCommand, Result<int>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<DeleteContactPersonCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public DeleteContactPersonCommandHandler(IUnitOfWork<int> unitOfWork, IProductRepository productRepository, IStringLocalizer<DeleteContactPersonCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(DeleteContactPersonCommand command, CancellationToken cancellationToken)
        {
            var contactPerson = await _unitOfWork.Repository<ContactPerson>().GetByIdAsync(command.Id);
            if (contactPerson != null)
            {
                await _unitOfWork.Repository<ContactPerson>().DeleteAsync(contactPerson);
                await _unitOfWork.CommitAndRemoveCache(cancellationToken, ApplicationConstants.Cache.GetAllContactPersonsCacheKey);
                return await Result<int>.SuccessAsync(contactPerson.Id, _localizer["ContactPerson Deleted"]);
            }
            else
            {
                return await Result<int>.FailAsync(_localizer["ContactPerson Not Found!"]);
            }


        }
    }
}