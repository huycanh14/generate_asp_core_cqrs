User Interface (Web/Api) Layer
- Được gọi là bản trình bày (Presentation)
- handle các Presentation về UI, API.
- Layer này chịu trách nhiệm hiển thị Giao diện người dùng đồ họa (GUI) để tương tác với người dùng hoặc dữ liệu Json với các hệ thống khác. Nó là điểm nhập của ứng dụng.
- Phụ thuộc vào  Application and Infrastructure layers. Tuy nhiên, sự phụ thuộc Infrastructure chỉ để hỗ trợ dependency injection

```
Controllers
Views
View Models
Middlewares
Filters/Attributes
Web/API Utilities

```
![Clean Architecture](Assets/clean_architecture.png)