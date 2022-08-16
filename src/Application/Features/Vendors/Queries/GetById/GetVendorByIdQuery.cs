using AutoMapper;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Wrapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.Vendors.Queries.GetById
{
    public class GetVendorByIdQuery : IRequest<Result<GetVendorByIdResponse>>
    {
        public int Id { get; set; }
    }

    internal class GetProductByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, Result<GetVendorByIdResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GetVendorByIdResponse>> Handle(GetVendorByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.Repository<Vendor>().GetByIdAsync(query.Id);
            var mappedVendor = _mapper.Map<GetVendorByIdResponse>(brand);
            return await Result<GetVendorByIdResponse>.SuccessAsync(mappedVendor);
        }
    }
}