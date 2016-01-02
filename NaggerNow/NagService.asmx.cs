using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using log4net;


namespace NaggerNow
{
    /// <summary>
    /// Summary description for NagService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class NagService : System.Web.Services.WebService
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetNags()
        {
            //GailToDo: Add security.

            ArrayList objs = new ArrayList();

            try
            {
                
                const string spName = "Cards_FetchAll_ImportantOrDue_Lite";


                string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;


                SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);



                IDataReader rdr = SqlHelper.ExecuteReader(dbConnString, CommandType.StoredProcedure, spName, sqlParam);


                while (rdr.Read())
                {
                    objs.Add(new
                    {
                        id = rdr["ID"],
                        title = rdr["Title"],
                        board = rdr["Board"],
                        list = rdr["List"],
                        cardtype = rdr["CardType"],
                        token = rdr["token"],
                        tokensawarded = rdr["tokensawarded"]
                    });
                }

            }
            catch (Exception ex)
            {
                if (log.IsDebugEnabled)
                {
                    log.DebugFormat("Error getting SLAs: {0}", ex.Message);
                }
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }

    }
}
