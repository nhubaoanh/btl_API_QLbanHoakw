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
    public class KhachHangBusiness : IKhachHangBusiness
    {
        private readonly IkhachhangResponsitory _bus;

        public KhachHangBusiness(IkhachhangResponsitory khachhang)
        {
            _bus = khachhang;
        }
        public bool create(khachHangModel khachHangModel)
        {
            khachHangModel.khachhang_id = Guid.NewGuid().ToString();
            khachHangModel.users_id = khachHangModel.khachhang_id;
            return _bus.create(khachHangModel);
        }

        public bool delete(string id)
        {
            return _bus.delete(id);
        }

        public List<khachHangModel> getAll()
        {
            return _bus.getAll();
        }

        public khachHangModel getId(string id)
        {
            return _bus.getId(id);
        }

        public bool update(khachHangModel khachHangModel)
        {
            return _bus.update(khachHangModel);
        }
    }
}
