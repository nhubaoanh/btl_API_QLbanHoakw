using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class orderController : ControllerBase
    {
        private readonly IorderBussiness _bus;

        public orderController(IorderBussiness orders)
        {
            _bus = orders;
        }

        [Route("create-orders")]
        [HttpPost]
        public IActionResult Create(orderModel model)
        {
            var result = _bus.create(model);

            if (result == null)
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

        [Route("getAll-order")]
        [HttpGet]
        public List<orderModel> GetAll()
        {
            return _bus.getAll();
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
    }
}
