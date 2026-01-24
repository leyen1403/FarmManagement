using FarmManagement.Web.Models.Components;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace FarmManagement.Web.Extensions;

/// <summary>
/// Extension methods cho ViewDataDictionary để quản lý breadcrumbs và các component data khác
/// </summary>
public static class ViewDataExtensions
{
    private const string BreadcrumbsKey = "Breadcrumbs";
    private const string PageHeaderKey = "PageHeader";
    private const string PageTitleKey = "PageTitle";

    #region Breadcrumbs Extensions

    /// <summary>
    /// Thiết lập danh sách breadcrumbs cho ViewData
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="breadcrumbs">Danh sách breadcrumb items</param>
    public static void SetBreadcrumbs(this ViewDataDictionary viewData, List<BreadcrumbItem> breadcrumbs)
    {
        viewData[BreadcrumbsKey] = breadcrumbs;
    }

    /// <summary>
    /// Thiết lập breadcrumbs từ params
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="breadcrumbs">Các breadcrumb items</param>
    public static void SetBreadcrumbs(this ViewDataDictionary viewData, params BreadcrumbItem[] breadcrumbs)
    {
        viewData[BreadcrumbsKey] = breadcrumbs.ToList();
    }

    /// <summary>
    /// Lấy danh sách breadcrumbs từ ViewData
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <returns>Danh sách breadcrumb items hoặc list rỗng nếu không có</returns>
    public static List<BreadcrumbItem> GetBreadcrumbs(this ViewDataDictionary viewData)
    {
        if (viewData.TryGetValue(BreadcrumbsKey, out var value) && value is List<BreadcrumbItem> breadcrumbs)
        {
            return breadcrumbs;
        }
        return [];
    }

    /// <summary>
    /// Thêm một breadcrumb item vào danh sách hiện có
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="breadcrumb">Breadcrumb item cần thêm</param>
    public static void AddBreadcrumb(this ViewDataDictionary viewData, BreadcrumbItem breadcrumb)
    {
        var breadcrumbs = viewData.GetBreadcrumbs();
        breadcrumbs.Add(breadcrumb);
        viewData.SetBreadcrumbs(breadcrumbs);
    }

    /// <summary>
    /// Thêm một breadcrumb item với các tham số cơ bản
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="title">Tiêu đề</param>
    /// <param name="url">URL (null nếu là trang hiện tại)</param>
    /// <param name="isActive">Có phải trang hiện tại không</param>
    public static void AddBreadcrumb(this ViewDataDictionary viewData, string title, string? url = null, bool isActive = false)
    {
        viewData.AddBreadcrumb(new BreadcrumbItem(title, url, isActive));
    }

    /// <summary>
    /// Kiểm tra xem có breadcrumbs không
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <returns>True nếu có breadcrumbs</returns>
    public static bool HasBreadcrumbs(this ViewDataDictionary viewData)
    {
        return viewData.GetBreadcrumbs().Count > 0;
    }

    #endregion

    #region PageHeader Extensions

    /// <summary>
    /// Thiết lập PageHeader cho ViewData
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="pageHeader">PageHeaderModel</param>
    public static void SetPageHeader(this ViewDataDictionary viewData, PageHeaderModel pageHeader)
    {
        viewData[PageHeaderKey] = pageHeader;
        // Đồng bộ Title với ViewData["Title"]
        if (!string.IsNullOrEmpty(pageHeader.Title))
        {
            viewData["Title"] = pageHeader.Title;
        }
    }

    /// <summary>
    /// Thiết lập PageHeader với các tham số cơ bản
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="title">Tiêu đề trang</param>
    /// <param name="subtitle">Tiêu đề phụ</param>
    /// <param name="icon">Icon CSS class</param>
    public static void SetPageHeader(this ViewDataDictionary viewData, string title, string? subtitle = null, string? icon = null)
    {
        viewData.SetPageHeader(new PageHeaderModel
        {
            Title = title,
            Subtitle = subtitle,
            Icon = icon
        });
    }

    /// <summary>
    /// Lấy PageHeader từ ViewData
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <returns>PageHeaderModel hoặc null nếu không có</returns>
    public static PageHeaderModel? GetPageHeader(this ViewDataDictionary viewData)
    {
        if (viewData.TryGetValue(PageHeaderKey, out var value) && value is PageHeaderModel pageHeader)
        {
            return pageHeader;
        }
        return null;
    }

    /// <summary>
    /// Lấy PageHeader hoặc tạo mới nếu không có
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <returns>PageHeaderModel</returns>
    public static PageHeaderModel GetOrCreatePageHeader(this ViewDataDictionary viewData)
    {
        var pageHeader = viewData.GetPageHeader();
        if (pageHeader == null)
        {
            pageHeader = new PageHeaderModel();
            viewData.SetPageHeader(pageHeader);
        }
        return pageHeader;
    }

    #endregion

    #region Page Title Extensions

    /// <summary>
    /// Thiết lập tiêu đề trang
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <param name="title">Tiêu đề</param>
    public static void SetPageTitle(this ViewDataDictionary viewData, string title)
    {
        viewData[PageTitleKey] = title;
        viewData["Title"] = title;
    }

    /// <summary>
    /// Lấy tiêu đề trang
    /// </summary>
    /// <param name="viewData">ViewDataDictionary</param>
    /// <returns>Tiêu đề trang hoặc chuỗi rỗng</returns>
    public static string GetPageTitle(this ViewDataDictionary viewData)
    {
        if (viewData.TryGetValue(PageTitleKey, out var value) && value is string title)
        {
            return title;
        }
        // Fallback to ViewData["Title"]
        if (viewData.TryGetValue("Title", out var titleValue) && titleValue is string fallbackTitle)
        {
            return fallbackTitle;
        }
        return string.Empty;
    }

    #endregion
}
