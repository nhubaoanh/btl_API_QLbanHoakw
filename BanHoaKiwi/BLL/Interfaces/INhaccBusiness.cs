using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface INhaccBusiness
    {
        bool Create(nhaCungCapModel model);

        bool Update(nhaCungCapModel model);

        bool Delete(string id);

        // lấy hết nhà cc
        List<nhaCungCapModel> GetAll();

        // lấy thôn tin một nhà cc
        nhaCungCapModel Get(string id);
    }
}
