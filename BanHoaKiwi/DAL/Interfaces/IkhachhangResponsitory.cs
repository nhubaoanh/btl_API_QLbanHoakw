using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IkhachhangResponsitory
    {
        bool create(khachHangModel khachHangModel);

        bool delete(string id);

        bool update(khachHangModel khachHangModel);

        khachHangModel getId(string id);

        List<khachHangModel> getAll();
    }
}
