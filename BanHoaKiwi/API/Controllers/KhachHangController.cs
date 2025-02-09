using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangBusiness _res;

        public KhachHangController(IKhachHangBusiness khachHang)
        {
            _res = khachHang;
        }

        [Route("create-khachhang")]
        [HttpPost]
        public IActionResult CreateKhachhang([FromBody] khachHangModel model)
        {
            try
            {
                _res.create(model);
                return StatusCode(StatusCodes.Status200OK, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("update-khachhang")]
        [HttpPost]
        public IActionResult Updatekhachhang([FromBody] khachHangModel khachHangModel)
        {
            try
            {
                _res.update(khachHangModel);
                return StatusCode(StatusCodes.Status200OK, khachHangModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete-khachhang/{id}")]
        [HttpPost]
        public IActionResult Deletekhachhang(string id)
        {
            try
            {
                _res.delete(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("khachhang-get-byId/{id}")]
        [HttpGet]
        public khachHangModel GetNhanVien(string id) => _res.getId(id);


        [Route("khachhang-getAll")]
        [HttpGet]
        public List<khachHangModel> getALL() => _res.getAll();
    }
}
