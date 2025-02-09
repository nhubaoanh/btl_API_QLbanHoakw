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
    public class NhanVienBusiness : INhanVienBusiness
    {
        private readonly INhanVienResponsitory _nhanvien;

        public NhanVienBusiness(INhanVienResponsitory nhanVien)
        {
            _nhanvien = nhanVien;
        }
        public bool Create(nhanVienModel model)
        {
            model.nhanvien_id = Guid.NewGuid().ToString();
            model.users_id = model.nhanvien_id;
            return _nhanvien.Create(model);
        }

        public bool Delete(string id)
        {
            return _nhanvien.Delete(id);
        }

        public List<nhanVienModel> GetAll()
        {
            return _nhanvien.GetAll();
        }

        public nhanVienModel GetId(string id)
        {
           return _nhanvien.GetId(id);
        }

        public bool Update(nhanVienModel model)
        {
            return _nhanvien.Update(model);
        }
    }
}
