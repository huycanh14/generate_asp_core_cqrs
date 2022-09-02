Persistence Layer (thuộc lớp Interface Adapter)
- Xử lý mối quan tâm về csdl và các hoạt động truy cập dữ liệu khác.
- phụ thuộc vào Application layer
- Chứa các triển khai của interfaces (Repositories) đã được xác định trọng Application layer
```
Data Context
Repositories
Data Seeding
Data Migrations
Caching (Distributed, In-Memory)
```