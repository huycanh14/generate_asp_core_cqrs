using MediatR;
using Microsoft.Extensions.Logging;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuyenSinh_api.Api.Common
{

    public class _TemplateController //_genControler
    {
        private readonly ILogger<_TemplateController> _logger;
        private readonly IMediator _mediator;

        public _TemplateController(ILogger<_TemplateController> logger, IMediator mediator) //_genBase
        {
            _logger = logger;
            _mediator = mediator;
        }


    }
}
