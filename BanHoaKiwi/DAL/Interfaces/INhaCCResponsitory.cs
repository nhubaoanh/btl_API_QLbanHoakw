using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface INhaCCResponsitory
    {
        bool Create(nhaCungCapModel model);

        bool Update(nhaCungCapModel model);

        bool Delete(string id);

        // lấy hết nhà cc
        List<nhaCungCapModel> GetAll();

        // lấy thôn tin một nhà cc
        nhaCungCapModel Get(string id);

        List<nhaCungCapModel> Search(int PageIndex, int pageSize, out long total, string hoten, string diachi);
        
    }
}
