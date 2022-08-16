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

namespace DynaCore.Application.Features.ContactPersons.Queries.Export
{

    internal class ExportContactPersonsQueryHandler : IRequestHandler<ExportContactPersonsQuery, Result<string>>
    {
        private readonly IExcelService _excelService;
        private readonly IUnitOfWork<int> _unitOfWork;
        private readonly IStringLocalizer<ExportContactPersonsQueryHandler> _localizer;

        public ExportContactPersonsQueryHandler(IExcelService excelService
            , IUnitOfWork<int> unitOfWork
            , IStringLocalizer<ExportContactPersonsQueryHandler> localizer)
        {
            _excelService = excelService;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Result<string>> Handle(ExportContactPersonsQuery request, CancellationToken cancellationToken)
        {
            var contactPersonFilterSpec = new ContactPersonFilterSpecification(request.SearchString);
            var contactPersons = await _unitOfWork.Repository<ContactPerson>().Entities
                .Specify(contactPersonFilterSpec)
                .ToListAsync(cancellationToken);
            var data = await _excelService.ExportAsync(contactPersons, mappers: new Dictionary<string, Func<ContactPerson, object>>
            {
                { _localizer["Id"], item => item.Id },
                { _localizer["FirstName"], item => item.FirstName },
                { _localizer["LastName"], item => item.LastName },
                { _localizer["PhoneNumber"], item => item.PhoneNumber },
                { _localizer["Email"], item => item.Email }
            }, sheetName: _localizer["ContactNumber"]);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}
