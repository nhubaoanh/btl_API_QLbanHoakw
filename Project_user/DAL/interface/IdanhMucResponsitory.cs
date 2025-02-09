using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IdanhMucResponsitory
    {
        List<danhMucModel> GetAll();

        danhMucModel GetByID(string id);
    }
}
