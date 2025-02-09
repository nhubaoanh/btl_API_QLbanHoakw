using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Model;

namespace API.Controllers
{
    // xác thực người dùng

    //[Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserBusiness _userBusiness;
        private string _path;

        public UsersController(IUserBusiness userBusiness, IConfiguration configuration)
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
        public usersModel Create([FromBody]usersModel model)
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
        public usersModel Update([FromBody] usersModel model)
        {
            _userBusiness.Update(model);
            return model;
        }

        //[Route("get-by-id/{id}")]
        //[HttpGet]
        //public IActionResult GetId(string id)
        //{
        //    var use = _userBusiness.GetByID(id);
        //    if(use != null)
        //    {
        //        return StatusCode(200);
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

        [Route("get-by-id/{id}")]
        [HttpGet]
        public object GetByID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "ID không được để rỗng"
                };
            }

            // Nếu id hợp lệ, tiếp tục lấy thông tin người dùng
            var user = _userBusiness.GetByID(id);
            if (user != null)
            {
                return new ApiResponse
                {
                    Success = true,
                    Message = "Tìm thấy user",
                    Data = user
                }; // Trả về đối tượng usersModel khi tìm thấy người dùng
            }
            else
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Không tìm thấy người dùng với ID đã cho"
                };
            }
        }


        [Route("search-user")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formdata)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formdata["page"].ToString());
                var pageSize = int.Parse(formdata["pageSize"].ToString());
                string hoten = "";
                if (formdata.Keys.Contains("hoten") && !string.IsNullOrEmpty(Convert.ToString(formdata["hoten"])))
                {
                    hoten = Convert.ToString(formdata["hoten"]);
                }
                string taikhoan = "";
                if (formdata.Keys.Contains("taikhoan") && !string.IsNullOrEmpty(Convert.ToString(formdata["taikhoan"])))
                {
                    taikhoan = Convert.ToString(formdata["taikhoan"]);
                }
                long total = 0;
                var data = _userBusiness.Search(page, pageSize, out total, hoten, taikhoan);
                response.TotalItems = total;
                response.data = data;
                response.PageIndex = page;
                response.PageSize = pageSize;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
