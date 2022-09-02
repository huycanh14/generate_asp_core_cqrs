using MediatR;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Api.Common;
using TuyenSinh_api.Application.Features.User.Commands.CreateUser;
using TuyenSinh_api.Application.Features.User.Commands.UpdateUser;
using TuyenSinh_api.Application.Features.User.Dtos;
using TuyenSinh_api.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuyenSinh_api.Api.Controllers
{
    public class UsersController : BaseController<UserDto, User, CreateUserCommandValidator, UpdateUserCommandValidator>
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IMediator _mediator;

        public UsersController(ILogger<UsersController> logger, IMediator mediator) : base(logger, mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


    }
}
