# Hướng dẫn Migration cho Database

## Giới thiệu
Project sử dụng **Entity Framework Core Migrations** để quản lý phiên bản database. Tất cả thay đổi về schema database được quản lý qua migration files.

## Cách sử dụng Migration

### 1. Tạo Migration mới
Khi bạn thay đổi Entity hoặc Configuration:

```bash
# Di chuyển vào thư mục Infrastructure
cd FarmManagement.Infrastructure

# Tạo migration mới
dotnet ef migrations add <TenMigration> --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj

# Ví dụ:
dotnet ef migrations add AddAuditColumnsToLivestockSaleDetails --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

**Lưu ý**: Tên migration nên mô tả rõ ràng thay đổi, sử dụng PascalCase.

### 2. Kiểm tra Migration
Sau khi tạo migration, kiểm tra file được tạo trong folder `Migrations`:
- File `<timestamp>_<TenMigration>.cs` chứa các thay đổi Up/Down
- File `<timestamp>_<TenMigration>.Designer.cs` chứa snapshot của model

**Kiểm tra kỹ các thay đổi trước khi apply!**

### 3. Áp dụng Migration vào Database

```bash
# Di chuyển vào thư mục Infrastructure
cd FarmManagement.Infrastructure

# Áp dụng tất cả pending migrations
dotnet ef database update --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj

# Hoặc áp dụng đến migration cụ thể
dotnet ef database update <TenMigration> --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

### 4. Rollback Migration
Nếu cần quay lại phiên bản trước:

```bash
# Rollback về migration trước đó
dotnet ef database update <TenMigrationTruocDo> --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj

# Rollback về trạng thái ban đầu (xóa tất cả migrations)
dotnet ef database update 0 --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

### 5. Xóa Migration chưa apply
Nếu chưa apply migration vào database:

```bash
dotnet ef migrations remove --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

**Cảnh báo**: Chỉ xóa được migration cuối cùng và chưa được apply!

### 6. Xem danh sách Migrations

```bash
dotnet ef migrations list --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

### 7. Tạo SQL Script từ Migration
Để review SQL sẽ được thực thi:

```bash
# Script cho migration cụ thể
dotnet ef migrations script <FromMigration> <ToMigration> --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj --output migration.sql

# Script cho tất cả pending migrations
dotnet ef migrations script --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj --output migration.sql

# Idempotent script (có thể chạy nhiều lần)
dotnet ef migrations script --idempotent --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj --output migration.sql
```

## Workflow phát triển

### Khi thêm/sửa Entity hoặc Configuration:

1. **Thay đổi code**
   - Sửa Entity trong `FarmManagement.Domain\Entities`
   - Hoặc sửa Configuration trong `FarmManagement.Infrastructure\Persistence\Configurations`

2. **Tạo Migration**
   ```bash
   cd FarmManagement.Infrastructure
   dotnet ef migrations add <TenMigration> --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
   ```

3. **Review Migration**
   - Kiểm tra file migration được tạo
   - Đảm bảo không có data loss warning
   - Kiểm tra phương thức `Up()` và `Down()`

4. **Test trên local database**
   ```bash
   dotnet ef database update --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
   ```

5. **Chạy và test ứng dụng**
   - Đảm bảo không có lỗi
   - Test các chức năng liên quan

6. **Commit code**
   - Commit cả file migration và code thay đổi
   - Push lên repository

### Khi pull code từ team member khác:

1. **Pull code mới nhất**
   ```bash
   git pull
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Áp dụng migrations mới**
   ```bash
   cd FarmManagement.Infrastructure
   dotnet ef database update --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
   ```

4. **Build và chạy ứng dụng**
   ```bash
   dotnet build
   dotnet run --project ..\FarmManagement.Api\FarmManagement.Api.csproj
   ```

## Best Practices

### 1. Đặt tên Migration
✅ **Tốt**:
- `AddUserEmailColumn`
- `CreateProductsTable`
- `AddIndexToOrderDate`
- `UpdateUserPasswordHashLength`

❌ **Không tốt**:
- `Migration1`
- `Update`
- `Fix`
- `Test`

### 2. Review Migration
- ✅ Luôn review file migration trước khi commit
- ✅ Kiểm tra phương thức `Down()` để đảm bảo có thể rollback
- ✅ Chú ý các warning về data loss
- ✅ Test migration trên local database trước

### 3. Data Migration
Nếu cần migration data (không chỉ schema):
```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    // Schema changes
    migrationBuilder.AddColumn<string>(
        name: "NewColumn",
        table: "Users",
        nullable: true);
    
    // Data migration
    migrationBuilder.Sql(@"
        UPDATE Users 
        SET NewColumn = 'DefaultValue' 
        WHERE NewColumn IS NULL
    ");
}
```

### 4. Production Deployment
Khi deploy lên production:

**Option 1: Auto-migrate khi startup**
```csharp
// Program.cs
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FarmManagementDbContext>();
    db.Database.Migrate();
}
```

**Option 2: Run script manually**
```bash
# Tạo SQL script
dotnet ef migrations script --idempotent -o deploy.sql --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj

# Execute script trên production database
sqlcmd -S <server> -d <database> -i deploy.sql
```

## Troubleshooting

### Lỗi: "Build failed"
```bash
# Clean và rebuild
dotnet clean
dotnet build
```

### Lỗi: "A migration named 'XXX' already exists"
```bash
# Xóa migration cũ
dotnet ef migrations remove --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj

# Tạo migration mới với tên khác
dotnet ef migrations add <TenMoi> --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

### Lỗi: "Unable to resolve service for type DbContext"
Đảm bảo:
1. `--startup-project` trỏ đúng đến project có `Program.cs` với DbContext registration
2. Connection string trong `appsettings.json` đúng
3. Package `Microsoft.EntityFrameworkCore.Design` đã được cài đặt

### Database out of sync
```bash
# Xem trạng thái hiện tại
dotnet ef migrations list --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj

# Drop database và recreate (CHỈ DÙNG CHO DEV!)
dotnet ef database drop --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
dotnet ef database update --startup-project ..\FarmManagement.Api\FarmManagement.Api.csproj
```

## Tham khảo
- [Entity Framework Core Migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/)
- [Migration commands](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)
- [EF Core Migration best practices](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/managing)
