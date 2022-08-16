using AutoMapper;
using DynaCore.Application.Interfaces.Repositories;
using DynaCore.Domain.Entities.Catalog;
using DynaCore.Shared.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace DynaCore.Application.Features.ContactPersons.Commands.AddEdit
{
    public partial class AddEditContactPersonCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }

    internal class AddEditContactPersonCommandHandler : IRequestHandler<AddEditContactPersonCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditContactPersonCommandHandler> _localizer;
        private readonly IUnitOfWork<int> _unitOfWork;

        public AddEditContactPersonCommandHandler(IUnitOfWork<int> unitOfWork, IMapper mapper, IStringLocalizer<AddEditContactPersonCommandHandler> localizer)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<int>> Handle(AddEditContactPersonCommand command, CancellationToken cancellationToken)
        {
            if (command.Id == 0)
            {
                var contactPerson = _mapper.Map<ContactPerson>(command);
                await _unitOfWork.Repository<ContactPerson>().AddAsync(contactPerson);
                await _unitOfWork.Commit(cancellationToken);
                return await Result<int>.SuccessAsync(contactPerson.Id, _localizer["Contact Person Saved"]);
            }
            else
            {
                var contactPerson = await _unitOfWork.Repository<ContactPerson>().GetByIdAsync(command.Id);
                if (contactPerson != null)
                {
                    contactPerson.FirstName = command.FirstName ?? contactPerson.FirstName;
                    contactPerson.LastName = command.LastName ?? contactPerson.LastName;
                    contactPerson.PhoneNumber = command.PhoneNumber ?? contactPerson.PhoneNumber;
                    contactPerson.Email = command.Email ?? contactPerson.Email;
                    await _unitOfWork.Repository<ContactPerson>().UpdateAsync(contactPerson);
                    await _unitOfWork.Commit(cancellationToken);
                    return await Result<int>.SuccessAsync(contactPerson.Id, _localizer["Contact Person Updated"]);
                }
                else
                {
                    return await Result<int>.FailAsync(_localizer["Contact Person Not Found!"]);
                }
            }
        }
    }
}