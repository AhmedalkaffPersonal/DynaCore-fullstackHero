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

namespace DynaCore.Application.Features.Products.Queries.Export
{
    public class ExportProductsQuery : IRequest<Result<string>>
    {
        public string SearchString { get; set; }

        public ExportProductsQuery(string searchString = "")
        {
            SearchString = searchString;
        }
    }

    internal class ExportProductsQueryHandler : IRequestHandler<ExportProductsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportProductsQueryHandler> _localizer;

        public ExportProductsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportProductsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportProductsQuery request, CancellationToken cancellationToken)
        {
            var productFilterSpec = new ProductFilterSpecification(request.SearchString);
            var products = await _unitOfWork.Repository<Product>().Entities
                .Specify(productFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(products, mappers: new Dictionary<string, Func<Product, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["Name"], item => item.Name },
                { _localizer["SKU"], item => item.SKU },
                { _localizer["Description"], item => item.Description },
                { _localizer["Quantity"], item => item.Quantity }
            }, sheetName: _localizer["Products"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}