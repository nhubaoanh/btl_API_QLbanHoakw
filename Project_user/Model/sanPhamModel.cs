using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class sanPhamModel
    {
        public string sanPham_id { get; set; }
        public string danhmuc_id { get; set; }
        public string sanPham_name { get; set; }
        public string sanPham_color { get; set; }
        public string sanPham_size { get; set; }
        public string sanPham_img { get; set; }
        public decimal? sanPham_price { get; set; }
    }
}
