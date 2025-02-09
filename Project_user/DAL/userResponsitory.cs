using DAL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class userResponsitory : IuserResponsitory
    {
        private readonly IDataBaseHelper _helper;

        public userResponsitory(IDataBaseHelper helper)
        {
            _helper = helper;
        }
        public bool Create(userModel users)
        {
            string msg = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out msg, "sp_user_create",
                    "@users_id", users.users_id,
                    "@hoten", users.hoten,
                    "@ngaysinh", users.ngaysinh,
                    "@gioitinh", users.gioitinh,
                    "diachi", users.diachi,
                    "email", users.email,
                    "@taikhoan", users.taikhoan,
                    "@matkhau", users.matkhau,
                    "@role", users.role,
                    "@img_url", users.img_url
                    );
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msg.ToString()))
                {
                    throw new Exception(Convert.ToString(result) + msg);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_user_delete",
                    "@users_id", id
                    );
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(mgs.ToString()))
                {
                    throw new Exception(Convert.ToString(result) + mgs);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public userModel GetByID(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_user_getID",
                    "@users_id", id
                    );
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<userModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public userModel GetUser(string name, string password)
        {
            string mgs = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_user_get_by_username_password",
                    "@taikhoan", name,
                    "@matkhau", password
                    );
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception($"Error database : {mgs}");
                }
                return dt.ConvertTo<userModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(userModel users)
        {
            string msg = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out msg, "sp_user_update",
                    "@users_id", users.users_id,
                    "@hoten", users.hoten,
                    "@ngaysinh", users.ngaysinh,
                    "@gioitinh", users.gioitinh,
                    "diachi", users.diachi,
                    "email", users.email,
                    "@taikhoan", users.taikhoan,
                    "@matkhau", users.matkhau,
                    "@role", users.role,
                    "@img_url", users.img_url
                    );
                if (!string.IsNullOrEmpty(msg) || result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(Convert.ToString(result) + msg);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
