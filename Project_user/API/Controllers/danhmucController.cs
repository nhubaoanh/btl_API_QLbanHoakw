using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class danhmucController : ControllerBase
    {
        private readonly IdanhMucBussiness _Bus;

        public danhmucController(IdanhMucBussiness danhMucBusiness)
        {
            _Bus = danhMucBusiness;
        }

        // trả tất cả danh mục la là danh sách các danh muc
        [Route("get-All-danhmuc")]
        [HttpGet]
        public IEnumerable<danhMucModel> GetAll()
        {
            return _Bus.GetAll();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public danhMucModel GetByid(string id)
        {
            return _Bus.GetByID(id);
        }
    }

    
}
