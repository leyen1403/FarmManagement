# Lookup Module Templates

## Tổng quan

Các Lookup Modules là các bảng danh mục đơn giản, thường có cấu trúc:
- Id (int)
- Code (string)
- Name (string)
- Description (string, optional)
- IsActive (bool)

## Danh sách Lookup Modules

| Module | Controller | DTO Namespace | Icon | Color |
|--------|------------|---------------|------|-------|
| CropType | CropTypeController | Crops | bi-flower1 | success |
| CropStatus | CropStatusController | Crops | bi-circle-fill | success |
| CropCareType | CropCareTypeController | Crops | bi-calendar-check | success |
| LivestockType | LivestockTypeController | Livestocks | bi-collection | primary |
| LivestockStatus | LivestockStatusController | Livestocks | bi-circle-fill | primary |
| LivestockCareType | LivestockCareTypeController | Livestocks | bi-heart | primary |
| LivestockHealthStatus | LivestockHealthStatusController | Livestocks | bi-activity | primary |
| SaleType | SaleTypeController | Livestocks | bi-cart | warning |
| LocationType | LocationTypeController | Locations | bi-geo-alt | info |
| LocationStatus | LocationStatusController | Locations | bi-pin-map | info |
| CostType | CostTypeController | Crops | bi-wallet2 | danger |

## UI Components được sử dụng

1. **PageHeaderModel** - Header với title, subtitle, breadcrumb và actions
2. **StatsCardModel** - Thống kê tổng quan (Total, Active, Inactive)
3. **DeleteModalModel** - Modal xác nhận xóa
4. **EmptyStateModel** - Trạng thái khi không có dữ liệu
5. **ActionButtonsModel** - Các nút thao tác (Edit, Delete, Toggle)

## Quy ước màu sắc

| Loại Module | Color Class | Icon Background |
|-------------|-------------|-----------------|
| Crop-related | success | bg-success-subtle |
| Livestock-related | primary | bg-primary-subtle |
| Location-related | info | bg-info-subtle |
| Cost/Sale-related | warning | bg-warning-subtle |
| Status-related | secondary | bg-secondary-subtle |

---

## INDEX TEMPLATE

```razor
@model List<{DtoNamespace}.{DtoType}>

@{
    ViewData["Title"] = "Quản lý {EntityNameTitle}";

  // Breadcrumbs
    ViewData.SetBreadcrumbs(
        BreadcrumbItem.Home(),
     BreadcrumbItem.Current("{EntityNameTitle}", "{Icon}")
    );

    var totalCount = Model.Count;
 var activeCount = Model.Count(x => x.IsActive);
    var inactiveCount = Model.Count(x => !x.IsActive);

// Page Header
    var pageHeader = new PageHeaderModel
    {
        Title = "Quản lý {EntityNameTitle}",
        Subtitle = "Danh sách {EntityName} trong hệ thống",
Icon = "{Icon}",
        ShowBreadcrumb = false,
        Actions = new List<ActionButtonModel>
     {
  ActionButtonModel.Create("/{ControllerName}/Create", "Thêm mới")
        }
    };

    // Stats Cards
    var statsCards = new List<StatsCardModel>
    {
        new StatsCardModel
        {
    Value = totalCount.ToString(),
        Label = "Tổng số",
     Icon = "{Icon}",
    IconBackgroundClass = "bg-{ColorClass}-subtle",
            ColorClass = "text-{ColorClass}"
      },
   new StatsCardModel
        {
            Value = activeCount.ToString(),
            Label = "Đang hoạt động",
         Icon = "bi bi-check-circle",
            IconBackgroundClass = "bg-success-subtle",
  ColorClass = "text-success"
        },
        new StatsCardModel
        {
     Value = inactiveCount.ToString(),
            Label = "Không hoạt động",
            Icon = "bi bi-x-circle",
            IconBackgroundClass = "bg-secondary-subtle",
        ColorClass = "text-secondary"
      }
    };

    // Delete Modal
    var deleteModal = DeleteModalModel.ForEntity("{EntityName}", "{ControllerName}");
}

<partial name="_PageHeader" model="pageHeader" />
<partial name="_StatsCards" model="statsCards" />

<div class="card">
    <div class="card-header bg-white d-flex justify-content-between align-items-center py-3">
      <div class="d-flex align-items-center gap-2">
            <h6 class="card-title mb-0 fw-semibold">
 <i class="bi bi-list-ul text-{ColorClass} me-2"></i>Danh sách {EntityName}
            </h6>
     <span class="badge bg-{ColorClass}">@totalCount</span>
        </div>
 <div class="d-flex align-items-center gap-2">
  <div class="input-group input-group-sm" style="width: 250px;">
    <span class="input-group-text bg-white"><i class="bi bi-search"></i></span>
      <input type="text" class="form-control" id="quickSearch" placeholder="Tìm nhanh...">
            </div>
     </div>
    </div>

    @if (Model.Any())
    {
        <div class="table-responsive">
            <table class="table table-hover align-middle mb-0" id="dataTable">
        <thead class="bg-light">
  <tr>
             <th width="60" class="text-center">#</th>
                  <th width="120">Mã</th>
                 <th>Tên</th>
                 <th>Mô tả</th>
          <th width="130">Trạng thái</th>
    <th width="150" class="text-center">Thao tác</th>
            </tr>
            </thead>
           <tbody>
             @{ var index = 0; }
      @foreach (var item in Model)
         {
index++;
   <tr>
        <td class="text-center text-muted">@index</td>
       <td><code class="bg-light px-2 py-1 rounded">@item.Code</code></td>
                    <td>
          <div class="d-flex align-items-center gap-2">
          <div class="bg-{ColorClass}-subtle text-{ColorClass} rounded d-flex align-items-center justify-content-center" style="width: 36px; height: 36px;">
         <i class="bi {Icon}"></i>
     </div>
     <span class="fw-medium">@item.Name</span>
    </div>
       </td>
 <td class="text-muted text-truncate" style="max-width: 250px;">@(item.Description ?? "—")</td>
       <td>
        @if (item.IsActive)
         {
         <span class="badge bg-success-subtle text-success"><i class="bi bi-check-circle me-1"></i>Hoạt động</span>
             }
         else
      {
     <span class="badge bg-secondary-subtle text-secondary"><i class="bi bi-x-circle me-1"></i>Tắt</span>
         }
       </td>
      <td class="text-center">
            <div class="btn-group btn-group-sm">
       <a asp-action="Edit" asp-route-id="@item.Id" class="btn btn-outline-primary" title="Sửa"><i class="bi bi-pencil"></i></a>
              <form asp-action="ToggleActive" asp-route-id="@item.Id" method="post" class="d-inline">
                 @Html.AntiForgeryToken()
    <button type="submit" class="btn @(item.IsActive ? "btn-outline-warning" : "btn-outline-success")" title="@(item.IsActive ? "Tắt" : "Bật")">
             <i class="bi @(item.IsActive ? "bi-toggle-off" : "bi-toggle-on")"></i>
        </button>
    </form>
         <button type="button" class="btn btn-outline-danger" title="Xóa" onclick="FarmApp.UI.confirmDelete(@item.Id, '@item.Name')">
 <i class="bi bi-trash"></i>
 </button>
  </div>
         </td>
        </tr>
               }
    </tbody>
            </table>
        </div>
    }
    else
    {
        <partial name="_EmptyState" model="@(EmptyStateModel.NoData("{EntityName}", "/{ControllerName}/Create"))" />
    }
</div>

<partial name="_DeleteModal" model="deleteModal" />

@section Scripts {
    <script>
        document.addEventListener('DOMContentLoaded', function () {
          FarmApp.DataTable.init('#dataTable', {
    searchInput: '#quickSearch',
          sortable: true,
      highlightMatches: true,
  searchableColumns: [1, 2, 3, 4]
  });
            FarmApp.UI.initTooltips();
        });
    </script>
}
```

---

## CREATE TEMPLATE

```razor
@model {DtoNamespace}.Create{DtoType}

@{
    ViewData["Title"] = "Thêm {EntityNameTitle}";

    ViewData.SetBreadcrumbs(
        BreadcrumbItem.Home(),
        new BreadcrumbItem("{EntityNameTitle}", "/{ControllerName}", "{Icon}"),
     BreadcrumbItem.Current("Thêm mới")
    );

    var pageHeader = new PageHeaderModel
    {
   Title = "Thêm {EntityNameTitle}",
        Subtitle = "Tạo {EntityName} mới trong hệ thống",
        Icon = "{Icon}",
        ShowBreadcrumb = false,
 Actions = new List<ActionButtonModel>
        {
       ActionButtonModel.Back("/{ControllerName}", "Quay lại")
        }
    };
}

<partial name="_PageHeader" model="pageHeader" />

<div class="row g-4">
  <div class="col-lg-8">
  <div class="card">
            <div class="card-header bg-white">
     <h6 class="card-title mb-0 fw-semibold">
<i class="bi {Icon} text-{ColorClass} me-2"></i>Thông tin {EntityName}
      </h6>
            </div>
      <div class="card-body">
             <form asp-action="Create" method="post">
         <div asp-validation-summary="All" class="alert alert-danger" style="display: @(ViewData.ModelState.ErrorCount > 0 ? "block" : "none")"></div>

           <div class="row g-3">
        <div class="col-md-6">
   <label asp-for="Code" class="form-label">
     <i class="bi bi-hash me-1 text-muted"></i>Mã <span class="text-muted">(tùy chọn)</span>
    </label>
         <input asp-for="Code" class="form-control" placeholder="{CodePlaceholder}" />
           <span asp-validation-for="Code" class="text-danger small"></span>
      <div class="form-text">Để trống hệ thống sẽ tự tạo</div>
                </div>

         <div class="col-md-6">
<label asp-for="Name" class="form-label">
  <i class="bi bi-tag me-1 text-muted"></i>Tên <span class="text-danger">*</span>
             </label>
            <input asp-for="Name" class="form-control" placeholder="{NamePlaceholder}" />
    <span asp-validation-for="Name" class="text-danger small"></span>
       </div>

     <div class="col-12">
    <label asp-for="Description" class="form-label">
          <i class="bi bi-text-paragraph me-1 text-muted"></i>Mô tả
    </label>
    <textarea asp-for="Description" class="form-control" rows="4" placeholder="Mô tả chi tiết..."></textarea>
         <span asp-validation-for="Description" class="text-danger small"></span>
    </div>
</div>

       <hr class="my-4">

        <div class="d-flex gap-2">
       <button type="submit" class="btn btn-{ColorClass}"><i class="bi bi-check-lg me-1"></i>Lưu</button>
        <a asp-action="Index" class="btn btn-outline-secondary"><i class="bi bi-x-lg me-1"></i>Hủy</a>
      </div>
    </form>
     </div>
  </div>
    </div>

    <div class="col-lg-4">
        <div class="card bg-{ColorClass}-subtle border-{ColorClass}">
            <div class="card-header bg-transparent border-{ColorClass}">
                <h6 class="card-title mb-0 text-{ColorClass}">
          <i class="bi bi-lightbulb me-2"></i>Gợi ý
        </h6>
        </div>
  <div class="card-body">
  <p class="mb-3 text-muted">Một số {EntityName} phổ biến:</p>
        <ul class="list-unstyled mb-0">
        <!-- Add hint items here -->
             </ul>
  </div>
      </div>
    </div>
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

## EDIT TEMPLATE

```razor
@model {DtoNamespace}.Update{DtoType}

@{
    ViewData["Title"] = "Sửa {EntityNameTitle}";

    ViewData.SetBreadcrumbs(
        BreadcrumbItem.Home(),
        new BreadcrumbItem("{EntityNameTitle}", "/{ControllerName}", "{Icon}"),
  BreadcrumbItem.Current("Chỉnh sửa")
    );

    var pageHeader = new PageHeaderModel
    {
      Title = "Sửa {EntityNameTitle}",
        Subtitle = $"Cập nhật thông tin #{Model.Id}",
        Icon = "{Icon}",
        ShowBreadcrumb = false,
     Actions = new List<ActionButtonModel>
        {
          ActionButtonModel.Back("/{ControllerName}", "Quay lại")
        }
    };
}

<partial name="_PageHeader" model="pageHeader" />

<div class="row g-4">
    <div class="col-lg-8">
    <div class="card">
<div class="card-header bg-white">
    <h6 class="card-title mb-0 fw-semibold">
       <i class="bi bi-pencil text-{ColorClass} me-2"></i>Thông tin {EntityName}
    </h6>
  </div>
      <div class="card-body">
      <form asp-action="Edit" method="post">
           <div asp-validation-summary="All" class="alert alert-danger" style="display: @(ViewData.ModelState.ErrorCount > 0 ? "block" : "none")"></div>

     <input type="hidden" asp-for="Id" />

          <div class="row g-3">
           <div class="col-md-6">
          <label asp-for="Code" class="form-label">
    <i class="bi bi-hash me-1 text-muted"></i>Mã
         </label>
          <input asp-for="Code" class="form-control bg-light" readonly />
             <div class="form-text">Mã không thể thay đổi</div>
     </div>

          <div class="col-md-6">
          <label asp-for="Name" class="form-label">
   <i class="bi bi-tag me-1 text-muted"></i>Tên <span class="text-danger">*</span>
           </label>
     <input asp-for="Name" class="form-control" />
     <span asp-validation-for="Name" class="text-danger small"></span>
    </div>

  <div class="col-12">
     <label asp-for="Description" class="form-label">
<i class="bi bi-text-paragraph me-1 text-muted"></i>Mô tả
  </label>
              <textarea asp-for="Description" class="form-control" rows="4"></textarea>
     </div>

            <div class="col-12">
           <div class="form-check form-switch">
           <input asp-for="IsActive" class="form-check-input" role="switch" />
        <label asp-for="IsActive" class="form-check-label">
   <i class="bi bi-toggle-on me-1"></i>Trạng thái hoạt động
        </label>
           </div>
       </div>
         </div>

          <hr class="my-4">

          <div class="d-flex gap-2">
     <button type="submit" class="btn btn-{ColorClass}"><i class="bi bi-check-lg me-1"></i>Cập nhật</button>
      <a asp-action="Index" class="btn btn-outline-secondary"><i class="bi bi-x-lg me-1"></i>Hủy</a>
         </div>
     </form>
        </div>
    </div>
    </div>

    <div class="col-lg-4">
        <!-- Info Card -->
   <div class="card mb-4">
          <div class="card-header bg-white">
              <h6 class="card-title mb-0 fw-semibold">
          <i class="bi bi-info-circle text-{ColorClass} me-2"></i>Thông tin
     </h6>
         </div>
      <div class="card-body">
 <div class="mb-3">
     <small class="text-muted d-block">ID</small>
      <strong>@Model.Id</strong>
      </div>
         <div class="mb-3">
              <small class="text-muted d-block">Mã</small>
      <code class="bg-light px-2 py-1 rounded">@Model.Code</code>
      </div>
  <div>
              <small class="text-muted d-block">Trạng thái hiện tại</small>
           @if (Model.IsActive)
   {
        <span class="badge bg-success-subtle text-success"><i class="bi bi-check-circle me-1"></i>Hoạt động</span>
           }
        else
       {
  <span class="badge bg-secondary-subtle text-secondary"><i class="bi bi-x-circle me-1"></i>Không hoạt động</span>
       }
  </div>
            </div>
        </div>

    <!-- Danger Zone -->
      <div class="card border-danger">
      <div class="card-header bg-danger-subtle border-danger">
   <h6 class="card-title mb-0 text-danger">
            <i class="bi bi-exclamation-triangle me-2"></i>Vùng nguy hiểm
       </h6>
     </div>
            <div class="card-body">
      <p class="small text-muted mb-3">Các thao tác không thể hoàn tác</p>
     <form asp-action="Delete" asp-route-id="@Model.Id" method="post" onsubmit="return confirm('Bạn có chắc chắn muốn xóa {EntityName} này?');">
     @Html.AntiForgeryToken()
  <button type="submit" class="btn btn-outline-danger btn-sm w-100">
             <i class="bi bi-trash me-1"></i>Xóa {EntityName} này
        </button>
    </form>
  </div>
   </div>
    </div>
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

---

## Placeholders Reference

| Placeholder | Description | Example (CropType) | Example (LivestockType) |
|-------------|-------------|---------------------|-------------------------|
| `{DtoNamespace}` | Full DTO namespace | FarmManagement.Application.DTOs.Crops | FarmManagement.Application.DTOs.Livestocks |
| `{DtoType}` | DTO type name | CropTypeDto | LivestockTypeDto |
| `{EntityName}` | Entity display name (lowercase) | loại cây trồng | loại vật nuôi |
| `{EntityNameTitle}` | Entity display name (title case) | Loại cây trồng | Loại vật nuôi |
| `{ControllerName}` | Controller name | CropType | LivestockType |
| `{Icon}` | Bootstrap icon | bi-flower1 | bi-collection |
| `{ColorClass}` | Color class | success | primary |
| `{CodePlaceholder}` | Code input placeholder | VD: RICE, CORN... | VD: PIG, COW... |
| `{NamePlaceholder}` | Name input placeholder | VD: Lúa nước | VD: Heo, Bò... |
