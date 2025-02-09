using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class hoaDonNhapModel
    {
        public string hoadonnhap_id {  get; set; }
        public string nhacc_id { get; set; }
        public string nhacc_name { get; set; }
        public string manhanvien {  get; set; }
        public string tennhanvien {  get; set; }
        public DateTime ngaynhap { get; set; }
        public decimal sotien {  get; set; }
        public string trangthai {  get; set; }
        public List<chiTietHoaDonNhapModel> listjson_chitietnhap { get; set; }
    }
}
