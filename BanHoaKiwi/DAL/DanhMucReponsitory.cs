using DAL.helper;
using DAL.helper.interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DanhMucReponsitory : IDanhMuc
    {
        // thêm cái database vào để sử dung gọi nó qua interface
        private IDataBaseHelper _helper;
        public DanhMucReponsitory(IDataBaseHelper dataBase)
        {
            _helper = dataBase;
        }
        public bool Create(danhMucSanPhamModel model)
        {
            string mgs = "";
            try
            {
              
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_danhmuc_create",
                "@danhmuc_id", model.danhmuc_id,
                "@danhmuc_name", model.danhmuc_name
                );
                // check điều kiên
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(Convert.ToString(result) + mgs);
                }
                return true;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            // chuỗi lỗi
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_danhmuc_delete",
                "@danhmuc_id", id
                );

                if((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(Convert.ToString(result) + mgs);
                }
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<danhMucSanPhamModel> GetAll()
        {
            string mgs = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_danhmuc_getAll");
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs); 
                }
                return dt.ConvertTo<danhMucSanPhamModel>().ToList();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public danhMucSanPhamModel GetByID(string id)
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_danhmuc_getId",
                    "@danhmuc_id", id);
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                return dt.ConvertTo<danhMucSanPhamModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool Update(danhMucSanPhamModel model)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_danhmuc_update",
                    "@danhmuc_id", model.danhmuc_id,
                    "@danhmuc_name", model.danhmuc_name
                    );
                if (!string.IsNullOrEmpty(mgs) || ((result != null) && !string.IsNullOrEmpty(result.ToString())))
                {
                    throw new Exception($"Error from database: {Convert.ToString(result)}  {mgs}");
                }
                return true;
            }
            catch(Exception ex) { throw ex; }
        }
    }
}
