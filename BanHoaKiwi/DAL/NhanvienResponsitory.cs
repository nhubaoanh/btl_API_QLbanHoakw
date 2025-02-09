using DAL.helper;
using DAL.helper.interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class NhanvienResponsitory : INhanVienResponsitory
    {
        private readonly IDataBaseHelper _helper;

        public NhanvienResponsitory(IDataBaseHelper dataBase)
        {
            _helper = dataBase;
        }
        public bool Create(nhanVienModel model)
        {
            string mgs = "";
            try
            {
                var reslut = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_nhanvien_create",
                    "@nhanvien_id", model.nhanvien_id,
                    "@users_id", model.users_id,
                    "@nhanvien_email", model.nhanvien_email,
                    "@nhanvien_password", model.nhanvien_password
                    );
                if((reslut !=  null) && (!string.IsNullOrEmpty(reslut.ToString()) || !string.IsNullOrEmpty(mgs))){
                    throw new Exception(Convert.ToString(reslut) + mgs);
                }
                return true;
            }catch (Exception e)
            {
                throw e;
            }
        }

        public bool Delete(string id)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_nhanvien_delete", "@nhanvien_id", id);
                if((!string.IsNullOrEmpty(mgs) || result != null && !string.IsNullOrEmpty(result.ToString())))
                {
                    throw new Exception($"ERROR is SERVER { Convert.ToString(result)}");
                }
                return true;
            }catch(Exception e)
            {
                throw e;
            }
        }

        public List<nhanVienModel> GetAll()
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_nhanvien_getAll");
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<nhanVienModel>().ToList();

            }catch (Exception e)
            {
                throw e ;
            }
        }

        public nhanVienModel GetId(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_nhanvien_getId", "@nhanvien_id", id);
                if (!string.IsNullOrEmpty(msg))
                {
                    throw new Exception(msg);
                }
                return dt.ConvertTo<nhanVienModel>().FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(nhanVienModel model)
        {
            string msg = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out msg, "sp_nhanvien_update","nhanvien_id", model.nhanvien_id,
                    "@nhanvien_email", model.nhanvien_email,
                    "@nhanvien_password", model.nhanvien_password
                    );
                if((!string.IsNullOrEmpty(msg) || result != null && !string.IsNullOrEmpty(result.ToString())))
                {
                    throw new Exception($"ERROR FROM DATABASE {Convert.ToString(result)}");
                }
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
