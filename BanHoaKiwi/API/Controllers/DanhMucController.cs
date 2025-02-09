using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DanhMucController : ControllerBase
    {
        private readonly IDanhMucBusiness _danhmucBus;

        public DanhMucController(IDanhMucBusiness danhMucBusiness)
        {
            _danhmucBus = danhMucBusiness;
        }

        [Route("create-danhmuc")]
        [HttpPost]
        public danhMucSanPhamModel CreateDanhMuc([FromBody] danhMucSanPhamModel model)
        {
            model.danhmuc_id = Guid.NewGuid().ToString();
            _danhmucBus.Create(model);
            return model;

        }

        [Route("update-danhmuc")]
        [HttpPost]
        public danhMucSanPhamModel UpdateDanhMuc([FromBody] danhMucSanPhamModel model)
        {
            _danhmucBus.Update(model);
            return model;
        }

        //[Route("delete-danhmuc/{id}")]
        //[HttpPost]
        //public IActionResult Delete([FromBody] Dictionary<string, object> formData)
        //{
        //    string danhmuc_id = "";
        //    if (formData.Keys.Contains("sanpham_id") && !string.IsNullOrEmpty(Convert.ToString(formData["danhmuc_id"])))
        //    {
        //        danhmuc_id = Convert.ToString(formData["danhmuc_id"]);
        //    }
        //    _danhmucBus.Delete(danhmuc_id);
        //    return StatusCode(200);
        //}

        [Route("delete-danhmuc/{id}")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _danhmucBus.Delete(id);
            }
            return StatusCode(200);
        }


        // trả tất cả danh mục la là danh sách các danh muc
        [Route("get-All-danhmuc")]
        [HttpGet]
        public IEnumerable<danhMucSanPhamModel> GetAll()
        {
            return _danhmucBus.GetAll();
        }

        [Route("get-by-id/{id}")]
        [HttpGet]
        public danhMucSanPhamModel GetByid(string id)
        {
            return _danhmucBus.GetByID(id);
        }


    }
}
