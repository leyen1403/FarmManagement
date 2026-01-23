// ***********************************************************************
// File: ForbiddenException.cs
// Project: FarmManagement.Application
// Mô tả: Định nghĩa exception tùy chỉnh dùng cho lỗi truy cập bị cấm.
// ***********************************************************************

namespace FarmManagement.Application.Exceptions
{
    /// <summary>
    /// Exception xảy ra khi người dùng bị cấm truy cập (Forbidden).
    /// </summary>
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Khởi tạo một instance mới của <see cref="ForbiddenException"/> với thông báo lỗi cụ thể.
        /// </summary>
        /// <param name="message">Thông báo mô tả lỗi truy cập bị cấm.</param>
        public ForbiddenException(string message) : base(message) { }
    }
}
