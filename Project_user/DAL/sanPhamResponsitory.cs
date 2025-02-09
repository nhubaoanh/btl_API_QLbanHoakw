using DAL.Helper;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class sanPhamResponsitory : IsanPhamResponsitory
    {
        private IDataBaseHelper _helper;

        // phuong thuc khoi tao cáu này ở bên startup
        public sanPhamResponsitory(IDataBaseHelper baseHelper)
        {
            _helper = baseHelper;
        }
        public List<sanPhamModel> GetDataAll()
        {
            string msg = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out msg, "sp_sanpham_getAll");
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                return dt.ConvertTo<sanPhamModel>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public sanPhamModel GetDatabyID(string id)
        {
            string mgs = "";
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_sanpham_getID", "@sanpham_id", id);
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                return dt.ConvertTo<sanPhamModel>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<sanPhamModel> Search(int pageIndex, int pageSize, out long total, string sanPham_name)
        {
            string mgs = "";
            total = 0;
            try
            {
                var dt = _helper.ExcuteSProcedureReturnDataTable(out mgs, "sp_sanpham_search",
                    "@page_index", pageIndex,
                    "page_size", pageSize,
                    //"@danhmuc_id", danhMuc_id,
                    "@sanpham_name", sanPham_name
                    );
                if (!string.IsNullOrEmpty(mgs))
                {
                    throw new Exception(mgs);
                }
                if (dt.Rows.Count > 0 && dt.Rows[0]["RecordCount"] != DBNull.Value)
                {
                    total = Convert.ToInt64(dt.Rows[0]["RecordCount"]); // Chuyển đổi an toàn
                }
                return dt.ConvertTo<sanPhamModel>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(sanPhamModel model)
        {
            string mgs = "";
            try
            {
                var result = _helper.ExcuteScalarSprocedureWithTransaction(out mgs, "sp_sanpham_update",
                    "@sanpham_id", model.sanPham_id,
                    "@danhmuc_id", model.danhmuc_id,
                    "@sanpham_name", model.sanPham_name,
                    "@sanpham_color", model.sanPham_color,
                    "@sanpham_size", model.sanPham_size,
                    "@sanpham_img", model.sanPham_img,
                    "@sanpham_price", model.sanPham_price
                    );
                if (!string.IsNullOrEmpty(mgs) || result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception($"Error from database: {Convert.ToString(result)}  {mgs}");
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
