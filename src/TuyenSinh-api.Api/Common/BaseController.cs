using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TuyenSinh_api.Application.Features.Common.Commands.Create;
using TuyenSinh_api.Application.Features.Common.Commands.Delete;
using TuyenSinh_api.Application.Features.Common.Commands.Update;
using TuyenSinh_api.Application.Features.Common.Queries.Export;
using TuyenSinh_api.Application.Features.Common.Queries.GetDetail;
using TuyenSinh_api.Application.Features.Common.Queries.GetList;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuyenSinh_api.Api.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TDto, TExportVm, TEntiy, TValidatorCreate, TValidatorUpdate> : ControllerBase
        where TDto : class
        where TExportVm : class
        where TEntiy : class
        where TValidatorCreate : class
        where TValidatorUpdate : class
    {
        private IMediator _mediator;
        private readonly ILogger<BaseController<TDto, TExportVm, TEntiy, TValidatorCreate, TValidatorUpdate>> _logger;


        public BaseController(ILogger<BaseController<TDto, TExportVm, TEntiy, TValidatorCreate, TValidatorUpdate>> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] FilterBase<TEntiy> filters)
        {
            _logger.LogInformation($"BaseController@GetList -- Start, data: {filters} {typeof(TDto)} {typeof(TEntiy)}");
            CommonGetListQuery<TDto, TEntiy> getListQuery = new CommonGetListQuery<TDto, TEntiy>() { filter = filters };
            var response = await _mediator.Send(getListQuery);
            _logger.LogInformation("BaseController@GetList -- End");
            return Ok(response);
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportDataExcel([FromQuery] FilterBase<TEntiy> filters, string title = "Danh sach", string filelName = null)
        {
            _logger.LogInformation($"BaseController@ExportData -- Start, data: {filters} {typeof(TExportVm)} {typeof(TEntiy)}");
            CommonExportExcelQuery<TExportVm, TEntiy> getListQuery = new CommonExportExcelQuery<TExportVm, TEntiy> { filter = filters, Title = title };
            var response = await _mediator.Send(getListQuery);
            _logger.LogInformation("BaseController@ExportData -- End");
            return File(response.Data, response.ContentType, filelName ?? response.ExportFileName);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetailById(int id)
        {
            _logger.LogInformation($"BaseController@GetById -- Start, data: {id} {typeof(TDto)} {typeof(TEntiy)}");
            CommonGetDetailQuery<TDto, TEntiy> getDetailQuery = new CommonGetDetailQuery<TDto, TEntiy>() { Id = id };
            var response = await _mediator.Send(getDetailQuery);
            _logger.LogInformation("BaseController@GetById -- End");
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> PostData(TDto value)
        {
            _logger.LogInformation($"BaseController@PostData -- Start, id: {value} {typeof(TValidatorCreate)} {typeof(TDto)} {typeof(TEntiy)}");
            var createCommand = new CommonCreateCommand<TValidatorCreate, TDto, TEntiy>() { DataCreate = value };
            var response = await _mediator.Send(createCommand);
            _logger.LogInformation("BaseController@PostData -- End");
            return StatusCode(StatusCodes.Status201Created, response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutData(int id, TDto value)
        {
            _logger.LogInformation($"BaseController@PutData -- Start, data: {value} {typeof(TValidatorUpdate)} {typeof(TDto)} {typeof(TEntiy)}");
            var userCommand = new CommonUpdateCommand<TValidatorUpdate, TDto, TEntiy>() { DataUpdate = value, Id = id };
            await _mediator.Send(userCommand);
            _logger.LogInformation("BaseController@PutData -- End");
            return StatusCode(204);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteData(int id)
        {
            _logger.LogInformation($"BaseController@Delete -- Start, id: {id} {typeof(TDto)} {typeof(TEntiy)}");
            var userCommand = new CommonDeleteCommand<TDto, TEntiy>() { Id = id };
            var response = await _mediator.Send(userCommand);
            _logger.LogInformation("BaseController@Delete -- End");
            return Ok(response);
        }
    }
}
