using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Application.Features._Template.Dtos;
using TuyenSinh_api.Application.Features.Common.Commands.Update;
namespace TuyenSinh_api.Application.Features._Template.Commands.UpdateTemplate
{
    public class UpdateTemplateCommandValidator : CommonUpdateValidator<_TemplateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateTemplateCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //RuleFor(p => p.UserName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");
        }
    }
}
