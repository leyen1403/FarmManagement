// ***********************************************************************
// File: BusinessException.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa exception tùy chỉnh dùng cho các lỗi nghiệp vụ (Business Logic).
// ***********************************************************************

namespace FarmManagement.Application.Exceptions
{
    /// <summary>
    /// Exception xảy ra trong quá trình thực thi nghiệp vụ.
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// Khởi tạo một instance mới của <see cref="BusinessException"/> với thông báo lỗi cụ thể.
        /// </summary>
        /// <param name="message">Thông báo mô tả lỗi nghiệp vụ.</param>
        public BusinessException(string message) : base(message) { }
    }
}
