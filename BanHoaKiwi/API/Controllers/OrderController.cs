using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrdersBusiness _bus;

        public OrderController(IOrdersBusiness orders)
        {
            _bus = orders;
        }

        [Route("create-orders")]
        [HttpPost]
        public IActionResult Create(orderModel model)
        {
            var result =  _bus.create(model);

            if(result == null)
            {
                return Ok(new ApiResponse
                {
                    Success = false,
                    Message = "add not faul"
                });
            }
            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Add order success",
                Data = model
            });
        }

        [Route("update-order")]
        [HttpPost]
        public IActionResult UpdateOrder([FromBody] orderModel model)
        {
            _bus.update(model);
            return Ok(model);
        }

        [Route("delete-order/{id}")]
        [HttpPost]
        public IActionResult DeleteOrder(string id)
        {
            _bus.delete(id);
            return Ok(new ApiResponse
            {
                Success = true,
                Message = " xóa thành công"
            });
        }

        [Route("getId-order/{id}")]
        [HttpGet]
        public orderModel GetId(string id)
        {
            return _bus.getId(id);
        }

        [Route("getAll-order")]
        [HttpGet]
        public List<orderModel> GetAll()
        {
            return _bus.getAll();
        }


        [Route("search-order")]
        [HttpPost]
        public ResponseModel Search([FromBody] Dictionary<string, object> formdata)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formdata["page"].ToString());
                var pageSize = int.Parse(formdata["pageSize"].ToString());
                string hoten = "";
                if(formdata.Keys.Contains("hoten") && !string.IsNullOrEmpty(Convert.ToString(formdata["hoten"])))
                {
                    hoten = Convert.ToString(formdata["hoten"]);
                }
                string diachi = "";
                if (formdata.Keys.Contains("diachi") && !string.IsNullOrEmpty(Convert.ToString(formdata["diachi"])))
                {
                    diachi = Convert.ToString(formdata["diachi"]);
                }
                long total = 0;
                var data = _bus.Search(page, pageSize, out total, hoten, diachi);
                response.TotalItems = total;
                response.data = data;
                response.PageIndex = page;
                response.PageSize = pageSize;

            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return response;
        }
    }
}
