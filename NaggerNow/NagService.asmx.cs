using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Web.Services;
using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using log4net;
using NaggerLibrary;

namespace NaggerNow
{

    public class CurrentCard
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
        public void GetAllNags()
        {

            ArrayList objs = new ArrayList();

            try
            {

                List<Card> cards = NaggerProvider.FetchCards();

                foreach (var card in cards)
                {
                    objs.Add(new
                    {
                        id = card.ID,
                        title = card.Title,
                        columnID = card.ColumnID,
                        description = card.Description,
                        board = card.BoardID,
                        cardType = card.CardType,
                        token = 0,
                        tokensAwarded = 0,
                        lastDone = card.LastDone
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
        public void GetMandatedNags()
        {
          
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
        public void GetOptionalNags()
        {
            ArrayList objs = new ArrayList();

            try
            {

                const string spName = "[dbo].[Cards_FetchOptional_Today]";


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
                    log.DebugFormat("Error getting Optional Nags: {0}", ex.Message);
                }
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetSkippedNags()
        {
            ArrayList objs = new ArrayList();

            try
            {

                const string spName = "[dbo].[Cards_FetchSkipped_Today]";


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
                    log.DebugFormat("Error getting skipped Nags: {0}", ex.Message);
                }
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetDoneTodayNags()
        {
            ArrayList objs = new ArrayList();

            try
            {

                const string spName = "[dbo].[Cards_FetchDone_Today]";


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
        public void NagMovedToDone(string Nag)
        {
            var result = JsonConvert.DeserializeObject<Card>(Nag);

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

            
            string spName = "NagMovedToDone";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = result.ID;
               
            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);

            log.InfoFormat("Card updated: {0}", Nag);
            

            ArrayList objs = new ArrayList();
            objs.Add(new
            {
                Test = "test",
            });

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void UpdateCard(string Nag)
        {
            var result = JsonConvert.DeserializeObject<Card>(Nag);

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;


            string spName = "NagMovedToDone";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = result.ID;

            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);

            log.InfoFormat("Card updated: {0}", Nag);


            ArrayList objs = new ArrayList();
            objs.Add(new
            {
                Test = "test",
            });

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }



        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NagMovedToSkipped(string Nag)
        {
            var result = JsonConvert.DeserializeObject<Card>(Nag);

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

          
            string spName = "NagMovedToSkipped";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = result.ID;

            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);

            log.InfoFormat("Card skipped: {0}", Nag);
          

            ArrayList objs = new ArrayList();
            objs.Add(new
            {
                Test = "test",
            });

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NagMovedToOptional(string Nag)
        {
            var result = JsonConvert.DeserializeObject<Card>(Nag);

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

         
            string spName = "NagMovedToOptional";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = result.ID;

            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);

            log.InfoFormat("Card moved back to optional: {0}", Nag);
         

            ArrayList objs = new ArrayList();
            objs.Add(new
            {
                Test = "test",
            });

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(objs));

        }
        
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void NagMovedToMandated(string Nag)
        {
            var result = JsonConvert.DeserializeObject<Card>(Nag);

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

            string spName = "NagMovedToMandated";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = result.ID;

            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);

            log.InfoFormat("Card moved to mandated: {0}", Nag);
            

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
