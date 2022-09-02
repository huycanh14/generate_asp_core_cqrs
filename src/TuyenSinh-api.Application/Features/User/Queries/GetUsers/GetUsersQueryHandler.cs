using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TuyenSinh_api.Application.Contracts.Persistence;
using TuyenSinh_api.Domain.Common;
using TuyenSinh_api.Domain.Enum;
using UserEntity = TuyenSinh_api.Domain.Entities.User;
namespace TuyenSinh_api.Application.Features.TEntity.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, BaseResponse<ListResponse<UserVm>>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ILogger<GetUsersQueryHandler> _logger;

        public GetUsersQueryHandler(
            ILogger<GetUsersQueryHandler> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
         )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ListResponse<UserVm>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUsersQueryHandler@Handle -- Start");
            var filter = request.filter.GetFilterWhere();
            var data = await _unitOfWork.GetRepository<UserEntity>().GetAllFilterAsync(fields: filter, new List<string>() { }, request.filter.UnqualifiedFieldName, new List<string>() { });
            var listUser = _mapper.Map<IEnumerable<UserVm>>(data.ListData);
            PropertyInfo[] props = typeof(UserVm).GetProperties();

            var properties = typeof(UserVm).GetProperties()
            .Where(p => p.IsDefined(typeof(DisplayAttribute), false) && !p.IsDefined(typeof(JsonIgnoreAttribute), false))
            .Select(p => new HeaderTableVm
            {
                Text = p.Name.Substring(0, 1).ToLower() + p.Name.Substring(1),
                Value = p.GetCustomAttributes(typeof(DisplayAttribute),
                        false).Cast<DisplayAttribute>().Single().Name
            }).ToList();

            _logger.LogInformation("GetUsersQueryHandler@Handle -- End");
            return new BaseResponse<ListResponse<UserVm>>(
                data: new ListResponse<UserVm>(data: listUser, listHeader: properties, count: data.Count),
                resultCode: ResultCodeEnum.GET
            );
        }
    }
}
