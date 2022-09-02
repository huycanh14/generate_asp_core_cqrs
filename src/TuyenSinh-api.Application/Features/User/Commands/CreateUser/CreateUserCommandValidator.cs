using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Application.Features.Common.Commands.Create;
using TuyenSinh_api.Application.Features.User.Dtos;
using UserEntity = TuyenSinh_api.Domain.Entities.User;

namespace TuyenSinh_api.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : CommonCreateValidator<UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 10 characters.");

            RuleFor(e => e)
             .MustAsync(CategoryNameUnique)
             .WithMessage("A user with the same name already exists.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

        }

        private async Task<bool> CategoryNameUnique(UserDto e, CancellationToken token)
        {
            return !(await (_unitOfWork.GetRepository<UserEntity>() as IUserRepository).IsUserNameAsync(id: 0, userName: e.UserName));
        }
    }
}
