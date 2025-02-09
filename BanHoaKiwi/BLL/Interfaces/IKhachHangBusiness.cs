using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IKhachHangBusiness
    {
        bool create(khachHangModel khachHangModel);

        bool delete(string id);

        bool update(khachHangModel khachHangModel);

        khachHangModel getId(string id);

        List<khachHangModel> getAll();
    }
}
