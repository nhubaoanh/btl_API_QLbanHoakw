using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class StoreParameterInfo
    {
        public string StoreProcedureName { get; set; }
        public List<Object> StoreProcedureParams { get; set; }

    }
    public interface IDataBaseHelper
    {
        void SetConnectionString(string connectionString);
        /// <summary>
        /// ham mo ket noi
        /// </summary>
        string OpenConnection();

        /// <summary>
        /// ham mo ket noi va bat dau giao dich
        /// </summary>
        /// <returns></returns>
        string OpenConnectionAndBeginTransaction();
        /// <summary>
        /// dong ket noi
        /// </summary>
        string CloseConnection();
        /// <summary>
        /// ham thuc thi mot thu tuc tra ve mot datatable
        /// </summary>
        /// <param name="mgsError"></param>
        /// <param name="sprocedureName"></param>
        /// <param name="paramObjects"></param>
        /// <returns></returns>
        DataTable ExcuteSProcedureReturnDataTable(out string mgsError, string sprocedureName, params object[] paramObjects);
        /// <summary>
        /// chi thuc thi duy nhat mot thu tuc voi giao dich
        /// </summary>
        /// <param name="mgsError"></param>
        /// <param name="sprocedure"></param>
        /// <param name="paramObjects"></param>
        /// <returns></returns>
        object ExcuteScalarSprocedureWithTransaction(out string mgsError, string sprocedure, params object[] paramObjects);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgErrors"></param>
        /// <param name="storeParameterInfos"></param>
        /// <returns></returns>
        List<object> ExecuteScalarSProcedure(out List<string> msgErrors, List<StoreParameterInfo> storeParameterInfos);

    }
}
