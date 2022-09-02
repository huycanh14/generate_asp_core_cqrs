using System;
using FluentValidation;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Application.Features._Template.Dtos;
using TuyenSinh_api.Application.Features.Common.Commands.Create;

namespace TuyenSinh_api.Application.Features._Template.Commands.CreateTemplate
{
    public class CreateTemplateCommandValidator : CommonCreateValidator<_TemplateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateTemplateCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //RuleFor(p => p.UserName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");

        }

    }
}
