using System;
using FluentValidation;
using TuyenSinh_api.Application.Contracts.Persistence;

namespace TuyenSinh_api.Application.Features.Common.Commands.Update
{
    public class CommonUpdateValidator<T> : AbstractValidator<T>
        where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommonUpdateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
