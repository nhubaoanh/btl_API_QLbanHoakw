using BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private IuserBussiness _userBusiness;
        private string _path;

        public userController(IuserBussiness userBusiness, IConfiguration configuration)
        {
            _userBusiness = userBusiness;
            _path = configuration["AppSettings:PATH"];
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userBusiness.Authenticate(model.UserName, model.Password);
            if (user == null)
            {
                // tạo ra cái model api respon để nào cứ trae ra dữ liệu tì cho nó vào
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Invaid username/ password"
                });
            }

            // cấp token

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Authenticate success",
                Data = new { user_id = user.users_id, hoten = user.hoten, taikhoan = user.taikhoan, token = user.token }
            });
        }

        [Route("create-users")]
        [HttpPost]
        public userModel Create([FromBody] userModel model)
        {
            model.users_id = Guid.NewGuid().ToString();
            _userBusiness.Create(model);
            return model;
        }

        [Route("delete-user/{id}")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id)) // Kiểm tra nếu id rỗng hoặc null
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Xóa thất bại: ID không được để trống."
                });
            }

            var result = _userBusiness.Delete(id); // Xóa user dựa vào id

            if (!result) // Nếu việc xóa không thành công
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "Xóa thất bại: Không tìm thấy người dùng hoặc lỗi hệ thống."
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Xóa thành công."
            });
        }




        [Route("update-users")]
        [HttpPost]
        public userModel Update([FromBody] userModel model)
        {
            _userBusiness.Update(model);
            return model;
        }
    }
}
