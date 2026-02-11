# Khắc phục lỗi không vào được trang chi tiết vật nuôi

## Vấn đề
Khi truy cập trang chi tiết vật nuôi (`/Livestock/Details/{id}`), hệ thống báo lỗi:
```
Invalid column name 'DeletedDate'.
Invalid column name 'IsDeleted'.
Invalid column name 'UpdatedDate'.
Invalid column name 'CreatedDate'.
```

## Nguyên nhân
- Các entity `LivestockSaleDetail` và `SaleType` kế thừa từ `AuditableEntity`
- Database hiện tại không có các cột audit (CreatedDate, UpdatedDate, DeletedDate, IsDeleted)
- Entity Framework cố gắng query các cột này dẫn đến lỗi SQL

## Giải pháp đã áp dụng

### ✅ Giải pháp: Tạo Migration để thêm các cột audit vào database

#### Bước 1: Xóa các dòng `Ignore` trong Entity Configuration
Đã cập nhật các file configuration để không ignore các cột audit nữa:
- `FarmManagement.Infrastructure\Persistence\Configurations\Livestocks\LivestockSaleDetailConfiguration.cs`
- `FarmManagement.Infrastructure\Persistence\Configurations\Livestocks\SaleTypeConfiguration.cs`

#### Bước 2: Tạo Migration
```bash
cd FarmManagement.Infrastructure
dotnet ef migrations add AddAuditColumnsToLivestockSaleDetailsAndSaleTypes --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

Migration này đã tạo và thêm các cột audit cho:
- ✅ `LivestockSaleDetails` (CreatedDate, UpdatedDate, DeletedDate, IsDeleted)
- ✅ `SaleTypes` (CreatedDate, UpdatedDate, DeletedDate, IsDeleted)
- ✅ Các bảng khác:
  - `UserRoles`
  - `Roles`
  - `LivestockSales` (UpdatedDate, DeletedDate, IsDeleted)
  - `LivestockPrice` (DeletedDate, IsDeleted)
  - `LivestockHealthStatuses`
  - `LivestockHealthLogs`
  - `LivestockCareTypes`
  - `LivestockCareLogs`

#### Bước 3: Áp dụng Migration vào Database
```bash
dotnet ef database update --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

**Kết quả**: ✅ Migration đã được áp dụng thành công!

## Kiểm tra
1. ✅ Build solution: **Thành công**
2. ✅ Migration applied: **Thành công**
3. Chạy lại ứng dụng
4. Truy cập trang danh sách vật nuôi: `/Livestock`
5. Click vào nút "Chi tiết" của bất kỳ vật nuôi nào
6. Trang chi tiết vật nuôi sẽ hiển thị bình thường

## Lợi ích của giải pháp Migration
1. ✅ **Quản lý phiên bản database**: Migration được lưu lại trong source code
2. ✅ **Rollback dễ dàng**: Có thể rollback về phiên bản trước nếu cần
3. ✅ **Team collaboration**: Các thành viên khác chỉ cần chạy `dotnet ef database update`
4. ✅ **Audit trail đầy đủ**: Các entity giờ có đầy đủ thông tin về thời gian tạo, cập nhật, xóa
5. ✅ **Soft delete**: Hỗ trợ xóa mềm cho các entity

## Migration File
- File migration: `20260211072652_AddAuditColumnsToLivestockSaleDetailsAndSaleTypes.cs`
- Location: `FarmManagement.Infrastructure\Migrations\`

## Cấu trúc các cột đã thêm
```sql
-- Mỗi bảng có 4 cột audit:
CreatedDate DATETIME2 NOT NULL DEFAULT '0001-01-01'
UpdatedDate DATETIME2 NULL
DeletedDate DATETIME2 NULL
IsDeleted BIT NOT NULL DEFAULT 0
```

## Lưu ý cho Developer
- Khi có thành viên mới pull code, cần chạy: `dotnet ef database update`
- Khi deploy lên server, chạy migration trong pipeline/script deployment
- Các entity kế thừa từ `AuditableEntity` sẽ tự động có các cột audit
- DbContext sẽ tự động cập nhật `CreatedDate`, `UpdatedDate`, `DeletedDate` khi SaveChanges

## Tham khảo
- Issue: Không vào được trang chi tiết vật nuôi
- Error: SqlException - Invalid column name
- Fix: Entity Framework Migration - Add audit columns
- Migration name: `AddAuditColumnsToLivestockSaleDetailsAndSaleTypes`
