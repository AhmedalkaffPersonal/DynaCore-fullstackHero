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

namespace DynaCore.Application.Features.ContactPersons.Queries.GetAll
{
    public class GetAllContactPersonsQuery : IRequest<Result<List<GetAllContactPersonsResponse>>>
    {
        public GetAllContactPersonsQuery()
        {
        }
    }

    internal class GetAllContactPersonsCachedQueryHandler : IRequestHandler<GetAllContactPersonsQuery, Result<List<GetAllContactPersonsResponse>>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAppCache _cache;

        public GetAllContactPersonsCachedQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IAppCache cache)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<Result<List<GetAllContactPersonsResponse>>> Handle(GetAllContactPersonsQuery request, CancellationToken cancellationToken)
        {
            Func<Task<List<ContactPerson>>> getAllContactPersons = () => _unitOfWork.Repository<ContactPerson>().GetAllAsync();
            var contactPersonList = await _cache.GetOrAddAsync(ApplicationConstants.Cache.GetAllVendorsCacheKey, getAllContactPersons);
            var mappedContactPersons = _mapper.Map<List<GetAllContactPersonsResponse>>(contactPersonList);
            return await Result<List<GetAllContactPersonsResponse>>.SuccessAsync(mappedContactPersons);
        }
    }
}