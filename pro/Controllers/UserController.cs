using System;
using System.Web.Http;

namespace pro.Controllers
{
    // 1. Định nghĩa cấu trúc dữ liệu JSON gửi lên (Model)
    public class ActivityData
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string DateTime { get; set; }
        public string Duration { get; set; }
        public string Desc { get; set; }
        public string Comment { get; set; }
    }

    // 2. Class API Controller duy nhất để xử lý luồng dữ liệu
    public class UserController : ApiController
    {
        // Hàm POST để nhận dữ liệu dạng JSON từ giao diện gửi lên
        // Đường dẫn gọi API này sẽ là: /api/activity/save
        [HttpPost]
        [Route("api/activity/save")]
        public IHttpActionResult SaveActivity([FromBody] ActivityData incomingModel)
        {
            if (incomingModel == null)
            {
                return BadRequest("Dữ liệu gửi lên trống hoặc sai định dạng!");
            }

            try
            {
                // KHU VỰC XỬ LÝ: Sau này nhóm bạn sẽ viết code kết nối Database (SQL Server) 
                // để lưu biến `incomingModel` này vào bảng dữ liệu tại đây.

                // Hiện tại, trả về một chuỗi JSON báo thành công cho giao diện biết
                return Ok(new
                {
                    success = true,
                    message = $"[API Server] Đã nhận thành công hoạt động: {incomingModel.Name}!"
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}