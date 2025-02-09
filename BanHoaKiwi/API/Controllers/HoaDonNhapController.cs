using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonNhapController : ControllerBase
    {
        private readonly IHoaDonNhapBusiness _res;

        public HoaDonNhapController(IHoaDonNhapBusiness hoaDonNhap)
        {
            _res = hoaDonNhap;
        }

        [Route("create-hoadonnhap")]
        [HttpPost]
        public IActionResult Createhoadon([FromBody] hoaDonNhapModel model)
        {
            if (model == null)
                return BadRequest("Dữ liệu không hợp lệ.");
            _res.create(model);
            return StatusCode(StatusCodes.Status200OK, model);
        }

        [Route("update-hoadonnhap")]
        [HttpPost]
        public IActionResult UpdateHoadonnhap([FromBody] hoaDonNhapModel model)
        {
            _res.update(model);
            return Ok(model);
        }

        [Route("delete-hoadonnhap/{id}")]
        [HttpPost]
        public IActionResult DeleteHoadonnhap(string id)
        {
            _res.delete(id);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = " xóa thành công"
            });
        }

        [Route("getId-hoadonnhap/{id}")]
        [HttpGet]
        public hoaDonNhapModel GetId(string id)
        {
            return _res.getId(id);
        }

        [Route("search-sanpham")]
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
                long total = 0;
                var data = _res.Search(page, pageSize, out total, hoten);
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
