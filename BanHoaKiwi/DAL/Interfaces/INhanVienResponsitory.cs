using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface INhanVienResponsitory
    {
        bool Create(nhanVienModel model);

        bool Delete(string id);

        bool Update(nhanVienModel model);

        List<nhanVienModel> GetAll();

        nhanVienModel GetId(string id);



    }
}
