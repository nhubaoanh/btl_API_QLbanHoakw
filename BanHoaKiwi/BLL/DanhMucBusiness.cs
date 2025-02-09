using BLL.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DanhMucBusiness : IDanhMucBusiness
    {
        private readonly IDanhMuc _danhmuc;

        public DanhMucBusiness(IDanhMuc danhMuc)
        {
            _danhmuc = danhMuc;

        }
        public bool Create(danhMucSanPhamModel model)
        {
            return _danhmuc.Create(model);
        }

        public bool Delete(string id)
        {
            //if(id == null)
            //{
            //    return false;
            //}
            return _danhmuc.Delete(id);
        }

        public List<danhMucSanPhamModel> GetAll()
        {
            return _danhmuc.GetAll();
        }

        public danhMucSanPhamModel GetByID(string id)
        {
            return _danhmuc.GetByID(id);
        }

        public bool Update(danhMucSanPhamModel model)
        {
            return _danhmuc.Update(model);
        }
    }
}
