using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Enum;
using TuyenSinh_api.Domain.Filters;
using TuyenSinh_api.Persistence.Repositories;

namespace TuyenSinh_api.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork as UnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DotDangKyFilter dotDangKyFilter)
        {
            _logger.LogInformation("WeatherForecastController@Get -- Start");
            var filter = dotDangKyFilter.GetFilterWhere();
            var data =  await _unitOfWork.dotDangKy.GetAllFilterAsync(fields: filter, new List<string>() { }, dotDangKyFilter.UnqualifiedFieldName, new List<string>() { });
            _logger.LogInformation("WeatherForecastController@Get -- End");
            return StatusCode(200, data);
        }
    }
}
