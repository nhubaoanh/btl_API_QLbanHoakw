using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HoaDonNhapBusiness : IHoaDonNhapBusiness
    {
        private readonly IHoaDonNhapResponsitori _hoadonnhap;

        public HoaDonNhapBusiness(IHoaDonNhapResponsitori hoaDonNhap)
        {
            _hoadonnhap = hoaDonNhap;
        }
        public bool create(hoaDonNhapModel hoaDonNhapModel)
        {
            hoaDonNhapModel.hoadonnhap_id = Guid.NewGuid().ToString();
            if(hoaDonNhapModel.listjson_chitietnhap !=  null )
            {
                foreach(var item in hoaDonNhapModel.listjson_chitietnhap)
                {
                    item.hoadonnhap_id = hoaDonNhapModel.hoadonnhap_id;
                    item.chitietnhap_id = Guid.NewGuid().ToString();
                }
            }
            return _hoadonnhap.create(hoaDonNhapModel);
        }

        public bool delete(string id)
        {
            return _hoadonnhap.delete(id);
        }

        public List<hoaDonNhapModel> getAll()
        {
            return _hoadonnhap.getAll();
        }

        public hoaDonNhapModel getId(string id)
        {
            return _hoadonnhap.getId(id);
        }

        public List<hoaDonNhapModel> Search(int pageIndex, int pageSize, out long total, string hoten)
        {
            return _hoadonnhap.Search(pageIndex, pageSize, out total, hoten);
        }   

        public bool update(hoaDonNhapModel hoaDonNhapModel)
        {
            if (hoaDonNhapModel.listjson_chitietnhap != null)
            {
                foreach (var item in hoaDonNhapModel.listjson_chitietnhap)
                {
                    item.chitietnhap_id = Guid.NewGuid().ToString();
                }
            }
            return _hoadonnhap.update(hoaDonNhapModel);
        }
    }
}
