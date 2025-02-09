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
    public class NhaCCReponsitory : INhaCCResponsitory
    {
        private readonly IDataBaseHelper _helper;


        // gọi ra để sử dụng
        public NhaCCReponsitory(IDataBaseHelper helper)
        {
            _helper = helper;
        }
        public bool Create(nhaCungCapModel model)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_nhacc_create",
                    "@nhacc_id", model.nhacc_id,
                    "@nhacc_name", model.nhacc_name,
                    "@sanphamcc", model.sanphamcc,
                    "@diachi", model.diachi,
                    "@email", model.email,
                    "@sodienthoai", model.sodienthoai
                    );
                if((result != null &&  !string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs))){
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public bool Delete(string id)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_nhacc_delete", "@nhacc_id", id);
                if((result != null && !string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs)))
                {
                    throw new Exception(Convert.ToString(result));
                }
                return true;
            }
            catch( Exception ex )
            {
                throw ex;
            }
        }

        public nhaCungCapModel Get(string id)
        {
            string mgs = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_nhacc_getId", "@nhacc_id", id);
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                return dt.ConvertTo<nhaCungCapModel>().FirstOrDefault();
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public List<nhaCungCapModel> GetAll()
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_nhacc_getAll");
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                return result.ConvertTo<nhaCungCapModel>().ToList();
            }
            catch ( Exception ex )
            {
                throw ex;
            }
        }

        public List<nhaCungCapModel> Search(int PageIndex, int pageSize, out long total, string hoten, string diachi)
        {
            string mgs = "";
            total = 0;
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_nhacc_search",
                    "@page_index", PageIndex,
                    "@page_size", pageSize,
                    "@hoten", hoten,
                    "@diachi", diachi
                    );
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                if (dt.Rows.Count > 0)
                    total = (long)dt.Rows[0]["RecorCount"];
                return dt.ConvertTo<nhaCungCapModel>().ToList();
            }
            catch (Exception ex )
            {
                throw ex;
            }
        }

        //public bool Update(nhaCungCapModel model)
        //{
        //    string mgs = "";
        //    try
        //    {
        //        var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_nhacc_update",
        //            "@nhacc_id", model.nhacc_id,
        //            "@nhacc_name", model.nhacc_name,
        //            "@sanphamcc", model.sanphamcc,
        //            "@diachi", model.diachi,
        //            "@email", model.email,
        //            "@sodienthoai", model.sodienthoai
        //            );
        //        if((result != null) && !string.IsNullOrEmpty(result.ToString()) || !string.IsNullOrEmpty(mgs))
        //        {
        //            throw new Exception(Convert.ToString(result));

        //        }
        //        return true;
        //    }catch ( Exception ex )
        //    {
        //        throw ex;
        //    }

        //}

        public bool Update(nhaCungCapModel model)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(
                    out mgs,
                    "sp_nhacc_update",
                    "@nhacc_id", model.nhacc_id,
                    "@nhacc_name", model.nhacc_name,
                    "@sanphamcc", model.sanphamcc,
                    "@diachi", model.diachi,
                    "@email", model.email,
                    "@sodienthoai", model.sodienthoai
                );

                if (!string.IsNullOrEmpty(mgs) || (result != null && !string.IsNullOrEmpty(result.ToString())))
                {
                    throw new Exception($"Error from database: {Convert.ToString(result)}");
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log lỗi trước khi throw
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
        }

    }
}
