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

    public class Card
    {
        public int id { get; set; }
        public string description { get; set; }
    }
    
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
        public void GetMandatedNags()
        {
            //GailToDo: Add security.

            ArrayList objs = new ArrayList();

            try
            {

                const string spName = "[dbo].[Cards_FetchMandated_Today]";


                string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;


                SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
                

                IDataReader rdr = SqlHelper.ExecuteReader(dbConnString, CommandType.StoredProcedure, spName, sqlParam);


                while (rdr.Read())
                {
                    objs.Add(new
                    {
                        id = rdr["ID"],
                        title = rdr["Title"],
                        description = rdr["Description"],
                        board = rdr["Board"],
                        cardType = rdr["CardType"],
                        token = rdr["token"],
                        tokensAwarded = rdr["tokensawarded"],
                        lastDone = rdr["lastdone"]
                    });
                }

            }
            catch (Exception ex)
            {
                if (log.IsDebugEnabled)
                {
                    log.DebugFormat("Error getting Nags: {0}", ex.Message);
                }
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }


        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NagDone(string Nag)
        {
            var result = JsonConvert.DeserializeObject<Card>(Nag);

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

            if (result.id == -1)
            {
                //ToDo
            }
            else
            {
                string spName = "NagDone";

                SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
                sqlParam[0].Value = result.id;
               
                SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);

                log.InfoFormat("Card updated: {0}", Nag);
            }

            ArrayList objs = new ArrayList();
            objs.Add(new
            {
                Test = "test",
            });

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }

    }
}
