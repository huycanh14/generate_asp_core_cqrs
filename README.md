docker build -t sql-serve  .   
docker run -d -p 1433:1433 --name SQLServer -d sql-serve
docker exec -it SQLServer /bin/sh

<!-- docker exec -it SQLServer /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P P@ssw0rd -->

/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -Q "create database CleanArchitecture_db;"

sh generate_mediator.sh "DotDangKy"

Cách sử dụng generate
- Có file class Entity trong  TuyenSinh-api.Domain/Entities (chứa các fiel entity ánh xạ đến table trong db) cần generate 
- Muốn chỉnh sửa DBconxtext, migrate, seeds tìm hiểu lib TuyenSinh-api.Persistence
B1. vào folder scripts chạy file generate_mediator.sh
sh generate_mediator.sh Ten_Entity
ví dụ: sh generate_mediator.sh "DotDangKy"

B2. Ghi các thuộc tính của Dto (viewmodel)
	TuyenSinh-api.Application/Features/Ten_Entity/Dtos: ghi các thuộc tính vào file Dto và ExportVm (nếu cần)
	Xem ví dụ tại: TuyenSinh-api.Application/Features/User/Dtos

B3. Ghi các validate cho Create/Update (nếu cần):
	TuyenSinh-api.Application/Features/Ten_Entity/Commands
	Xem ví dụ tại: TuyenSinh-api.Application/Features/User/Commands/CreateUser/CreateUserCommandValidator.cs
	Xem ví dụ tại: TuyenSinh-api.Application/Features/User/Commands/UpdateUser/UpdateUserCommandValidator.cs