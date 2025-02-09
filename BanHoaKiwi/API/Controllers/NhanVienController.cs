using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienBusiness _res;

        public NhanVienController(INhanVienBusiness nhanVien)
        {
            _res = nhanVien;
        }
        [Route("create-nhanvien")]
        [HttpPost]
        public IActionResult CreateNhanvien([FromBody] nhanVienModel nhanVienModel)
        {
            try
            {
                _res.Create(nhanVienModel);
                return StatusCode(StatusCodes.Status200OK, nhanVienModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Route("update-nhanvien")]
        [HttpPost]
        public IActionResult UpdateNhanVien([FromBody] nhanVienModel nhanVienModel)
        {
            try
            {
                _res.Update(nhanVienModel);
                return StatusCode(StatusCodes.Status200OK, nhanVienModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete-nhanvien/{id}")]
        [HttpPost]
        public IActionResult DeleteNhanVien(string id)
        {
            try
            {
                _res.Delete(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("nhanvien-get-byId/{id}")]
        [HttpGet]
        public nhanVienModel GetNhanVien(string id) => _res.GetId(id);


        [Route("nhanvien-getAll")]
        [HttpGet]
        public List<nhanVienModel> getALL() => _res.GetAll();
        
    }
}
