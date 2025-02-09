using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class orderModel
    {
        public string order_id {  get; set; }
        public DateTime ngaymua { get; set; }
        public string khachhang_name {  get; set; }
        public string nhanvien_id {  get; set; }
        public string trangthai {  get; set; }
        public float chietkhau {  get; set; }
        public decimal tongtien { get; set; }
        public string sodienthoai {  get; set; }
        public string diachi { get; set; }
        public List<chiTietDonHangModel> listjson_chitiet {  get; set; }
    }
}
