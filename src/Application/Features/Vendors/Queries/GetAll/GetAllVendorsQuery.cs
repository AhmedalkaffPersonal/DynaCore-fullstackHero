using AutoMapper;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Constants.Application;
using DynaCore.Shared.Wrapper;
using LazyCache;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.Vendors.Queries.GetAll
{
    public class GetAllVendorsQuery : IRequest<Result<List<GetAllVendorsResponse>>>
    {
        public GetAllVendorsQuery()
        {
        }
    }

    internal class GetAllVendorsCachedQueryHandler : IRequestHandler<GetAllVendorsQuery, Result<List<GetAllVendorsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllVendorsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllVendorsResponse>>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<Vendor>>> getAllVendors = () => _unitOfWork.Repository<Vendor>().GetAllAsync();
            var vendorList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllVendorsCacheKey, getAllVendors);
            var mappedVendors = _mapper.Map<List<GetAllVendorsResponse>>(vendorList);
            return await Result<List<GetAllVendorsResponse>>.SuccessAsync(mappedVendors);
        }
    }
}