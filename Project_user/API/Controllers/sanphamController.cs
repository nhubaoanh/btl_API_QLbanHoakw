using BLL;
using DAL.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sanphamController : ControllerBase
    {
        // khởi tạo bus ở đây
        private IsanPhamBussiness _res;
        private string? _path;
        private FileHelper _fileHelper;

        public sanphamController(IsanPhamBussiness res, IConfiguration configuration)
        {
            _res = res;
            _path = configuration["AppSettings:PATH"];
            _fileHelper = new FileHelper(_path);

        }




        // Xử lý cập nhật ảnh
        [Route("update-sanpham")]
        [HttpPost]
        public IActionResult UpdateSanPham([FromBody] sanPhamModel model)
        {
            try
            {
                if (!string.IsNullOrEmpty(model.sanPham_img))
                {
                    var arrData = model.sanPham_img.Split(';');
                    if (arrData.Length == 3)
                    {
                        var savePath = $"assets/images/{arrData[0]}";
                        _fileHelper.SaveFileFromBase64String(savePath, arrData[2]);
                        model.sanPham_img = savePath;
                    }
                }
                _res.Update(model);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        

        //api hien thi san pham
        [Route("get-by-id/{id}")]
        [HttpGet]
        public sanPhamModel GetByID(string id)
        {
            return _res.GetDatabyID(id);
        }


        [Route("getAll-sanpham")]
        [HttpGet]
        public IActionResult GetDataAll()
        {
            try
            {
                // lấy danh sách 
                var products = _res.GetDataAll();
                //duyệt
                foreach (var sp in products)
                {
                    if (!string.IsNullOrEmpty(sp.sanPham_img))
                    {
                        // đường dẫn
                        var imagePath = _fileHelper.CreatePathFile(sp.sanPham_img);
                        // kiểm tra sự tồn tại
                        if (System.IO.File.Exists(imagePath))
                        {
                            // gọi hàm chuyển đổi ra
                            sp.sanPham_img = _fileHelper.ConvertImageToBase64(imagePath);
                        }
                        else
                        {
                            sp.sanPham_img = null;
                        }
                    }
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [Route("upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"upload/{file.FileName}";
                    var fullPath = _fileHelper.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không tìm thây");
            }
        }

        [Route("search-sanpham")]
        [HttpPost]
        public ResponseModel  Search([FromBody] Dictionary<string, object> formdata)
        {
            var response = new ResponseModel();
            try
            {
                var page = int.Parse(formdata["page"].ToString());
                var pageSize = int.Parse(formdata["pageSize"].ToString());
                //if (formdata.Keys.Contains("danhMuc_id") && !string.IsNullOrEmpty(Convert.ToString(formdata["danhMuc_id"])))
                //{
                //    danhMuc_id = Convert.ToString(formdata["danhMuc_id"]);
                //}
                string sanPham_name = "";
                if (formdata.Keys.Contains("sanPham_name") && !string.IsNullOrEmpty(Convert.ToString(formdata["sanPham_name"])))
                {
                    sanPham_name = Convert.ToString(formdata["sanPham_name"]);
                }
                long total = 0;
                var data = _res.Search(page, pageSize, out total, sanPham_name);
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
