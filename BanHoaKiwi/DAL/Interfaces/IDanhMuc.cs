using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDanhMuc
    {
        bool Create(danhMucSanPhamModel model);

        bool Delete(string id);

        bool Update(danhMucSanPhamModel model);

        List<danhMucSanPhamModel> GetAll();

        danhMucSanPhamModel GetByID(string id);
    }
}
