using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using log4net;

namespace NaggerLibrary
{
    public class NaggerProvider
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static SqlDataReader FetchAll()
        {

                const string spName = "[dbo].[Cards_FetchAll]";


                string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;


                SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
                
                return SqlHelper.ExecuteReader(dbConnString, CommandType.StoredProcedure, spName, sqlParam);               

        }

        public static void FetchCards()
        {
            using (SqlDataReader rdr = FetchAll())
            {
                while (rdr.Read())
                {
                    Console.WriteLine(rdr["Title"]);
                }
            }

        }
    }
}
