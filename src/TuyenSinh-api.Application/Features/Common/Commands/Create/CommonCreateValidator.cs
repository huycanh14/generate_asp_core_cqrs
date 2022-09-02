using System;
using FluentValidation;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Application.Features.User.Commands.CreateUser;

namespace TuyenSinh_api.Application.Features.Common.Commands.Create
{
    public class CommonCreateValidator<T> : AbstractValidator<T>
        where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonCreateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
