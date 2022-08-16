using DynaCore.Application.Extensions;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Application.Interfaces.Services;
using DynaCore.Application.Specifications.Catalog;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.Vendors.Queries.Export
{
    public class ExportVendorsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportVendorsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportVendorsQueryHandler : IRequestHandler<ExportVendorsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportVendorsQueryHandler> _localizer;

        public ExportVendorsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportVendorsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportVendorsQuery request, CancellationToken cancellationToken)
        {
            var vendorFilterSpec = new VendorFilterSpecification(request.SearchString);
            var vendors = await _unitOfWork.Repository<Vendor>().Entities
                .Specify(vendorFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(vendors, mappers: new Dictionary<string, Func<Vendor, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["Address"], item => item.Address },
                { _localizer["TaxNumber"], item => item.TaxNumber },
                { _localizer["PhoneNumber"], item => item.PhoneNumber },
                { _localizer["ContactPerson"], item => item.ContactPerson }
            }, sheetName: _localizer["Vendors"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
