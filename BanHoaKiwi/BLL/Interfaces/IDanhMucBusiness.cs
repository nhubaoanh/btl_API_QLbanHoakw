using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDanhMucBusiness
    {
        bool Create(danhMucSanPhamModel model);
        bool Update(danhMucSanPhamModel model);

        bool Delete(string id);

        List<danhMucSanPhamModel> GetAll();

        danhMucSanPhamModel GetByID(string id);
    }
}
