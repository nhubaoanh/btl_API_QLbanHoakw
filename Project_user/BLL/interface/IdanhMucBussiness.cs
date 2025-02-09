using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IdanhMucBussiness
    {
        List<danhMucModel> GetAll();

        danhMucModel GetByID(string id);
    }
}
