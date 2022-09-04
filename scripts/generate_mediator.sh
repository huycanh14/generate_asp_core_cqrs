#!/bin/bash

#####################################################################
# Create file Vm
#
#####################################################################

model=$1
echo "Model is: $model";
model_all=$1;
name_controller=$1;
end_code=['s','ss','sh','ch', 'z', 'x'];
# end_code=(s ss sh ch z x)
# case "${myarray[@]}" in  *"two"*) echo "found" ;; esac
# tao folder
if [[ $model == *y ]] # * is used for pattern matching
then
	model_all=${model_all:0:$((${#model_all}-1))}ies
	name_controller=${model_all:0:$((${#model_all}-1))}ies
elif [[ $model == *s || $model == *ss || $model == *sh || $model == *ch || $model == *z || $model == *x ]]; then # * is used for pattern matching
	model_all=${model_all:0:$((${#model_all}))}es
	name_controller=${model_all:0:$((${#model_all}))}es
# elif [[  "${end_code[${model:(-1)}]}" ]]; then # * is used for pattern matching
# 	model_all=${model_all:0:$((${#model_all}))}es
# 	name_controller=${model_all:0:$((${#model_all}))}es
else
	model_all=${model_all:0:$((${#model_all}))}s
	name_controller=${model_all:0:$((${#model_all}))}s
fi
path_application="../src/TuyenSinh-api.Application/Features/${model}"
rm -r $path_application
path_application_template="../src/TuyenSinh-api.Application/Features/_Template"
if [ ! -d "$path_application" ]; then
	mkdir -p ${path_application}
	mkdir -p "${path_application}/Queries"
	mkdir -p "${path_application}/Dtos"
	mkdir -p "${path_application}/Commands/Create${model}"
	mkdir -p "${path_application}/Commands/Update${model}"
	# <Folder Include="Features\_Template\" />
	generate_folder="\t\t<Folder Include='Features\\\\${model}\\\\' \/>\n"
	generate_folder+="\t\t<Folder Include='Features\\\\${model}\\\\Queries\\\\' \/>\n"
	generate_folder+="\t\t<Folder Include='Features\\\\${model}\\\\Dtos\\\\' \/>\n"
	generate_folder+="\t\t<Folder Include='Features\\\\${model}\\\\Commands\\\\Create${model}\\\\' \/>\n"
	generate_folder+="\t\t<Folder Include='Features\\\\${model}\\\\Commands\\\\Update${model}\\\\' \/>"
	sed -i.back  "s/generate_folder-->/generate_folder-->\n${generate_folder}/g" "../src/TuyenSinh-api.Application/TuyenSinh-api.Application.csproj"
	rm "../src/TuyenSinh-api.Application/TuyenSinh-api.Application.csproj.back"
fi

# generate viewmodel, export
if [   -d "${path_application}/Dtos" ]; then
	cp -v "$path_application_template/Dtos/_TemplateDto.cs" "${path_application}/Dtos/${model}Dto.cs"
	cp -v "$path_application_template/Dtos/_TemplateExportVm.cs" "${path_application}/Dtos/${model}ExportVm.cs"
fi

for file in "${path_application}/Dtos/*.cs"
do
	sed -i.back  "s/_Template/${model}/g;s/Template/${model}/g" $file
done

for file in "${path_application}/Dtos/*.cs.back"
do
	rm $file
done

# end generate viewmodel, export

# generate create
cp -v "$path_application_template/Commands/CreateTemplate/CreateTemplateCommandValidator.cs" "${path_application}/Commands/Create${model}/Create${model}CommandValidator.cs"

for file in "${path_application}/Commands/Create${model}/*.cs"
do
	sed -i.back  "s/_Template/${model}/g;s/Template/${model}/g" $file
done
for file in "${path_application}/Commands/Create${model}/*.cs.back"
do
	rm $file
done
# end generate create

# generate update
cp -v "$path_application_template/Commands/UpdateTemplate/UpdateTemplateCommandValidator.cs" "${path_application}/Commands/Update${model}/Update${model}CommandValidator.cs"

for file in "${path_application}/Commands/Update${model}/*.cs"
do
	sed -i.back  "s/_Template/${model}/g;s/Template/${model}/g" $file
done
for file in "${path_application}/Commands/Update${model}/*.cs.back"
do
	rm $file
done

# end generate update

# generate add auto map 
path_automap="../src/TuyenSinh-api.Application/Profiles/MappingProfile.cs"
text_automap="\t\t\tCreateMap<TuyenSinh_api.Domain.Entities.${model}, TuyenSinh_api.Application.Features.${model}.Dtos.${model}Dto>().ReverseMap();\n"
text_automap+="\t\t\tCreateMap<TuyenSinh_api.Domain.Entities.${model}, TuyenSinh_api.Application.Features.${model}.Dtos.${model}ExportVm>().ReverseMap();"
sed -i.back  "s/_generate/_generate\n${text_automap}/g" $path_automap
rm "$path_automap.back"
# end generate add auto map 

# generate add ApplicationServiceRegistration
path_application="../src/TuyenSinh-api.Application/ApplicationServiceRegistration.cs"
namespace_dto="TuyenSinh_api.Application.Features.${model}.Dtos"
namespace_command="TuyenSinh_api.Application.Features.${model}.Commands"
namespace_entity="TuyenSinh_api.Domain.Entities.${model}"
text_application="\t\t\tservices.AddTransient(\n \
				typeof(IRequestHandler<CommonGetListQuery<${namespace_dto}.${model}Dto, ${namespace_entity}>, BaseResponse<ListResponse<${namespace_dto}.${model}Dto>>>),\n \
				typeof(CommonGetListQueryHandler<${namespace_dto}.${model}Dto,  ${namespace_entity}>));\n";
text_application+="\t\t\tservices.AddTransient(\n \
				typeof(IRequestHandler<CommonGetDetailQuery<${namespace_dto}.${model}Dto, ${namespace_entity}>, BaseResponse<DetailResponse<${namespace_dto}.${model}Dto>>>),\n \
				typeof(CommonGetDetailQueryHandler<${namespace_dto}.${model}Dto,  ${namespace_entity}>));\n";
text_application+="\t\t\tservices.AddTransient(\n\
				typeof(IRequestHandler<CommonExportExcelQuery<${namespace_dto}.${model}ExportVm, ${namespace_entity}>, ExportFileVm>),\n\
 				typeof(CommonExportExcelQueryHandler<${namespace_dto}.${model}ExportVm,  ${namespace_entity}>));\n";
text_application+="\t\t\tservices.AddTransient(\n\
				typeof(IRequestHandler<CommonCreateCommand<${namespace_command}.Create${model}.Create${model}CommandValidator,${namespace_dto}.${model}Dto, ${namespace_entity}>,  BaseResponse<${namespace_dto}.${model}Dto>>),\n\
				typeof(CommonCreateCommandHandler<${namespace_command}.Create${model}.Create${model}CommandValidator,${namespace_dto}.${model}Dto, ${namespace_entity}>));\n";
text_application+="\t\t\tservices.AddTransient(\n\
				typeof(IRequestHandler<CommonUpdateCommand<${namespace_command}.Update${model}.Update${model}CommandValidator,${namespace_dto}.${model}Dto, ${namespace_entity}>,  BaseResponse<${namespace_dto}.${model}Dto>>),\n\
				typeof(CommonUpdateCommandHandler<${namespace_command}.Update${model}.Update${model}CommandValidator,${namespace_dto}.${model}Dto, ${namespace_entity}>));\n";
text_application+="\t\t\tservices.AddTransient(\n\
				typeof(IRequestHandler<CommonDeleteCommand<${namespace_dto}.${model}Dto, ${namespace_entity}>, BaseResponse<${namespace_dto}.${model}Dto>>),\n\
				typeof(CommonDeleteCommandHandler<${namespace_dto}.${model}Dto,  ${namespace_entity}>));\n";

sed -i.back  "s/_generate/_generate\n${text_application}/g" $path_application
rm "$path_application.back"
# end generate add ApplicationServiceRegistration

# generate add Controller
path_controller="../src/TuyenSinh-api.Api/Controllers/${model_all}Controller.cs"
if [ ! -f "${path_controller}" ]; then
	cp -v "../src/TuyenSinh-api.Api/Common/_TemplateController.cs" "${path_controller}"
	sed -i.back  "s/_Template/${model_all}/g" $path_controller
	text_extend_controller="\n\t: BaseController< \n\
	\t${namespace_dto}.${model}Dto, \n\
	\t${namespace_dto}.${model}ExportVm, \n\
	\t${namespace_entity}, \n\
	\t${namespace_command}.Create${model}.Create${model}CommandValidator, \n\
	\t${namespace_command}.Update${model}.Update${model}CommandValidator \n\
	>";
	sed -i.back  "s/_genControler/${text_extend_controller}/g" $path_controller;
	text_base="\n\t\t: base(logger, mediator)";
	sed -i.back  "s/_genBase/${text_base}/g" $path_controller;
	rm "$path_controller.back"
fi

# end generate add Controller
#####################################################################
# Finished generate
#
#####################################################################

echo "Generate finished"

