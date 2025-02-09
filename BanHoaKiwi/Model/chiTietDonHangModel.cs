using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class chiTietDonHangModel
    {
        public string chitiet_id { get; set; }
        public string order_id {  get; set; }
        public string sanpham_id {  get; set; }
        public string sanpham_name {  get; set; }
        public int soluong {  get; set; }
        public decimal dongia {  get; set; }
        public string status { get; set; }

    }
}
