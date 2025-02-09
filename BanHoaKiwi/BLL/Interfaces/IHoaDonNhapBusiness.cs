using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IHoaDonNhapBusiness
    {
        bool create(hoaDonNhapModel hoaDonNhapModel);

        bool delete(string id);

        bool update(hoaDonNhapModel hoaDonNhapModel);

        hoaDonNhapModel getId(string id);

        List<hoaDonNhapModel> getAll();

        List<hoaDonNhapModel> Search(int pageIndex, int pageSize, out long total, string hoten);
    }
}
