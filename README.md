# FarmManagement

Ứng dụng quản lý nông trại xây dựng trên **.NET 8**, tách lớp rõ ràng theo hướng **Domain-Driven + CQRS (MediatR)**.

## Mục lục
- [1. Dự án gồm những gì](#1-dự-án-gồm-những-gì)
- [2. Kiến trúc & luồng xử lý](#2-kiến-trúc--luồng-xử-lý)
- [3. Công nghệ sử dụng](#3-công-nghệ-sử-dụng)
- [4. Yêu cầu môi trường](#4-yêu-cầu-môi-trường)
- [5. Cấu hình trước khi chạy](#5-cấu-hình-trước-khi-chạy)
- [6. Chạy dự án local (quick start)](#6-chạy-dự-án-local-quick-start)
- [7. Quy trình phát triển tính năng mới](#7-quy-trình-phát-triển-tính-năng-mới)
- [8. Một số lưu ý kỹ thuật](#8-một-số-lưu-ý-kỹ-thuật)

---

## 1. Dự án gồm những gì

Solution `FarmManagement.sln` gồm 6 project:

- **FarmManagement.Domain**: Entity/domain model cốt lõi.
- **FarmManagement.Application**: Use case, CQRS commands/queries/handlers, mapping.
- **FarmManagement.Infrastructure**: EF Core DbContext, migrations, triển khai service truy cập dữ liệu.
- **FarmManagement.Api**: ASP.NET Core Web API.
- **FarmManagement.Web**: ASP.NET Core MVC (UI), gọi API qua `HttpClient`.
- **FarmManagement.Shared**: Thành phần/kiểu dùng chung.

```text
FarmManagement.sln
├── FarmManagement.Domain
├── FarmManagement.Application
├── FarmManagement.Infrastructure
├── FarmManagement.Shared
├── FarmManagement.Api
└── FarmManagement.Web
```

---

## 2. Kiến trúc & luồng xử lý

### Kiến trúc lớp

- **Web**: render giao diện, nhận input người dùng, gọi API.
- **API**: nhận request HTTP, điều phối qua MediatR.
- **Application**: xử lý nghiệp vụ bằng command/query handlers.
- **Infrastructure**: truy cập SQL Server qua EF Core.

### Luồng request điển hình

```text
Web MVC -> API Controller -> MediatR Handler -> Infrastructure Service -> SQL Server
```

---

## 3. Công nghệ sử dụng

- .NET 8
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR (CQRS)
- AutoMapper
- Swagger/OpenAPI (Development)

---

## 4. Yêu cầu môi trường

- **.NET SDK 8.x**
- **SQL Server** (local instance hoặc server nội bộ)
- IDE đề xuất: Visual Studio 2022 / Rider / VS Code

Kiểm tra nhanh:

```bash
dotnet --version
```

---

## 5. Cấu hình trước khi chạy

### 5.1 API - chuỗi kết nối DB

File: `FarmManagement.Api/appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=FarmManagement;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 5.2 Web - địa chỉ API

File: `FarmManagement.Web/appsettings.json`

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5157"
  }
}
```

> `BaseUrl` phải khớp URL API đang chạy.

---

## 6. Chạy dự án local (quick start)

### Bước 1: Restore & build

```bash
dotnet restore
dotnet build FarmManagement.sln
```

### Bước 2: Cập nhật database từ migrations (khuyến nghị)

```bash
dotnet ef database update --project FarmManagement.Infrastructure --startup-project FarmManagement.Api
```

> Nếu chưa có `dotnet-ef`:
>
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### Bước 3: Chạy API

```bash
dotnet run --project FarmManagement.Api
```

Mặc định profile dev của API có các URL:

- `http://localhost:5157`
- `https://localhost:7222`

Swagger:

- `http://localhost:5157/swagger`

### Bước 4: Chạy Web (terminal khác)

```bash
dotnet run --project FarmManagement.Web
```

Mặc định profile dev của Web:

- `http://localhost:5152`
- `https://localhost:7018`

---

## 7. Quy trình phát triển tính năng mới

Gợi ý flow chuẩn theo kiến trúc hiện tại:

1. Tạo/chỉnh sửa entity tại **Domain**.
2. Thêm command/query + handler tại **Application**.
3. Thêm/điều chỉnh service & mapping tại **Infrastructure**.
4. Expose endpoint ở **API** controller.
5. Nếu cần UI: thêm API client + controller/view ở **Web**.

---

## 8. Một số lưu ý kỹ thuật

- API có middleware xử lý exception tập trung, trả response lỗi dạng JSON thống nhất.
- Nhiều thực thể dùng cơ chế **audit fields** (`CreatedDate`, `UpdatedDate`, `DeletedDate`) và **soft delete**.
- Dashboard Web lấy dữ liệu tổng hợp từ nhiều endpoint API.

---

Nếu bạn muốn, mình có thể bổ sung thêm:
- Sơ đồ kiến trúc dạng hình,
- Hướng dẫn chạy bằng Docker,
- Checklist release/deploy cho môi trường production.
