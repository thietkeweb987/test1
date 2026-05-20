using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using pro.Models;

namespace pro.Controllers
{
    // Cấu hình tiền tố đường dẫn: api/activities
    [RoutePrefix("api/activities")]
    public class ActivitiesController : ApiController
    {
        // Tạo một Database giả lập (lưu tạm dữ liệu trên RAM) để test luồng chạy dữ liệu
        private static List<ActivityLog> _mockDb = new List<ActivityLog>
        {
            new ActivityLog { Id = 1, PairKey = "pair-01", Date = "19/05/2026", Title = "Nghiên cứu cấu trúc Web API 2", Duration = "2 tiếng", Location = "Phòng làm việc", Description = "Tích hợp thành công cấu hình tích hợp Combo giữa MVC và API.", InternComment = "Em đã nắm được cách hoạt động ngầm." }
        };

        // 1. API Lấy danh sách nhật ký: GET /api/activities/getbykey?pairKey=pair-01
        [HttpGet]
        [Route("getbykey")]
        public IHttpActionResult GetLogs(string pairKey)
        {
            // Lọc ra các dòng nhật ký thuộc về mã cặp Mentor-Intern này
            var logs = _mockDb.Where(x => x.PairKey == pairKey).ToList();

            return Ok(logs); // Tự động trả về chuỗi JSON sạch sẽ, chuẩn hóa cho Frontend hứng
        }

        // 2. API Thêm mới nhật ký: POST /api/activities/create
        [HttpPost]
        [Route("create")]
        public IHttpActionResult CreateActivity([FromBody] ActivityLog newLog)
        {
            if (newLog == null)
            {
                return BadRequest("Dữ liệu gửi lên không hợp lệ hoặc bị trống!");
            }

            // Tự động tăng ID lên 1
            newLog.Id = _mockDb.Count + 1;

            // Lưu dòng nhật ký mới vào Database tạm trên RAM
            _mockDb.Add(newLog);

            // Trả về trạng thái thành công dạng JSON cho JavaScript nhận diện
            return Ok(new { success = true, message = "Ghi nhận hoạt động qua Web API 2 thành công!" });
        }

        // 3. API Xóa hoạt động: DELETE /api/activities/delete/5
        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult DeleteActivity(int id)
        {
            // Tìm dòng nhật ký có ID khớp với ID gửi lên
            var logToDelete = _mockDb.FirstOrDefault(x => x.Id == id);

            if (logToDelete == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy hoạt động này
            }

            // Xóa khỏi Database tạm trên RAM
            _mockDb.Remove(logToDelete);

            // Trả về thông báo JSON thành công
            return Ok(new { success = true, message = "Đã xóa hoạt động thành công!" });
        }

        // 💡 4. API Chỉnh sửa cập nhật hoạt động (Dùng chung cho cả Mentor chỉnh sửa và Intern Comment)
        [HttpPost]
        [Route("update/{id}")]
        public IHttpActionResult UpdateActivity(int id, [FromBody] ActivityLog updatedLog)
        {
            if (updatedLog == null)
            {
                return BadRequest("Dữ liệu cập nhật gửi lên không hợp lệ!");
            }

            // Tìm dòng nhật ký cũ đang nằm trong bộ nhớ RAM dựa vào ID
            var existingLog = _mockDb.FirstOrDefault(x => x.Id == id);

            if (existingLog == null)
            {
                return NotFound();
            }

            // [MENTOR] Tiền hành lưu đè/cập nhật thông tin mới từ Form Mentor gửi lên
            existingLog.Title = updatedLog.Title;
            existingLog.Location = updatedLog.Location;
            existingLog.Date = updatedLog.Date;
            existingLog.Duration = updatedLog.Duration;
            existingLog.Description = updatedLog.Description;

            // 🔥 ĐÂY LÀ DÒNG BẠN CẦN BỔ SUNG: Nhận comment của Intern và lưu vào cơ sở dữ liệu RAM
            existingLog.InternComment = updatedLog.InternComment;

            // Trả về kết quả JSON báo thành công cho JavaScript
            return Ok(new { success = true, message = "Đã cập nhật chỉnh sửa hoạt động thành công!" });
        }
    }
}