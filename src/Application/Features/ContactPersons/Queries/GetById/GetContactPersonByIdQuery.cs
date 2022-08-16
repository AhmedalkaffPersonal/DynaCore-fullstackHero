using AutoMapper;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.ContactPersons.Queries.GetById
{
    public class GetContactPersonByIdQuery : IRequest<Result<GetContactPersonByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetContactPersonByIdQueryHandler : IRequestHandler<GetContactPersonByIdQuery, Result<GetContactPersonByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetContactPersonByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetContactPersonByIdResponse>> Handle(GetContactPersonByIdQuery query, CancellationToken cancellationToken)
        {
            var contactPerson = await _unitOfWork.Repository<ContactPerson>().GetByIdAsync(query.Id);
            var mappedVendor = _mapper.Map<GetContactPersonByIdResponse>(contactPerson);
            return await Result<GetContactPersonByIdResponse>.SuccessAsync(mappedVendor);
        }
    }
}