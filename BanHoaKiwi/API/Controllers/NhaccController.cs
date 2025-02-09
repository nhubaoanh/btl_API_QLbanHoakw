using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhaccController : ControllerBase
    {
        private INhaccBusiness _res;

        public NhaccController(INhaccBusiness nhaccBusiness)
        {
            _res = nhaccBusiness;
        }


        // tao
        [Route("create-nhacc")]
        [HttpPost]
        public IActionResult CreateNhacc([FromBody] nhaCungCapModel model)
        {
            try
            {
                model.nhacc_id = Guid.NewGuid().ToString();
                _res.Create(model);
                return StatusCode(StatusCodes.Status200OK, model);
            }
            catch 
            {
                return BadRequest();
            }
        }

        [Route("update-nhacc")]
        [HttpPost]
        public IActionResult UpdateNhacc([FromBody] nhaCungCapModel model)
        {
            try
            {
                _res.Update(model);
                return StatusCode(StatusCodes.Status200OK, model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("delete-nhacc/{id}")]
        [HttpPost]
        public IActionResult DeleteNhacc(string id)
        {
            try
            {
                _res.Delete(id);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch
            {
                return BadRequest();
            }
        }


        [Route("get-by-id/{id}")]
        [HttpGet]
        public nhaCungCapModel GetById(string id) => _res.Get(id);

        [Route("get-all")]
        [HttpGet]
        public List<nhaCungCapModel> GetAll()
        {
            return _res.GetAll();
        }
      
    }
}
