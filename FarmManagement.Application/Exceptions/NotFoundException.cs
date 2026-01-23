// ***********************************************************************
// File: NotFoundException.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa exception tùy chỉnh dùng cho lỗi tài nguyên không tìm thấy.
// ***********************************************************************

namespace FarmManagement.Application.Exceptions
{
    /// <summary>
    /// Exception xảy ra khi tài nguyên được yêu cầu không tồn tại.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Khởi tạo một instance mới của <see cref="NotFoundException"/> với thông báo lỗi cụ thể.
        /// </summary>
        /// <param name="message">Thông báo mô tả lỗi tài nguyên không tìm thấy.</param>
        public NotFoundException(string message) : base(message) { }
    }
}
