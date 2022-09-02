using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using TuyenSinh_api.Application.Contracts.Persistence;
using UserEntity = TuyenSinh_api.Domain.Entities.User;


namespace TuyenSinh_api.Application.Features.User.Commands.CreateUser
{
    public class CreateUserCommandValidatorCore : AbstractValidator<CreateUserCommandCore>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserCommandValidatorCore(IUnitOfWork unitOfWork)
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

        private async Task<bool> CategoryNameUnique(CreateUserCommandCore e, CancellationToken token)
        {
            return !(await (_unitOfWork.GetRepository<UserEntity>() as IUserRepository).IsUserNameAsync(id: 0, userName: e.UserName));
        }
    }
}
