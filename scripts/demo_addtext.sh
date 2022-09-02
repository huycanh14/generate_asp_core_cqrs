#!/bin/bash

path_application="../src/TuyenSinh-api.Application/Profiles/MappingProfile.cs"
text="CreateMap<User, UserExportVm>().ReverseMap();\n"
text+="CreateMap<User, UserExportVm>().ReverseMap();\n"
sed -i.back  "s/_generate/_generate\n${text}/g" $path_application
rm "$path_application.back"