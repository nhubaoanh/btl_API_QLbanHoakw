using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUser
    {
        usersModel GetUser(string name, string password);

        usersModel GetByID(string id);

        bool Create(usersModel users);

        bool Update(usersModel users);

        bool Delete(string id);

        List<usersModel> Search(int pageIndex, int pageSize, out long total, string hoten, string taikhoan);
    }
}
