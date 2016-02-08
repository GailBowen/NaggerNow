using log4net;
using Microsoft.ApplicationBlocks.Data;
using NaggerLibrary;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace NaggerNow
{
    
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
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
                var ndl = new NaggerDataLinker();

                List<Card> cards = ndl.GetCardCollection();

                foreach (var card in cards)
                {
                    objs.Add(new
                    {
                        id = card.ID,
                        title = card.Title,
                        columnID = card.ColumnID,
                        description = card.Description,
                        board = card.BoardID,
                        frequency = card.FrequencyID,
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
        public void UpdateCard(string Nag, string column)
        {

            CardFactory factory = new CardFactory();
            ICard card = factory.CreateInstance(column.ToLower());

            var type = card.GetType();
            
            card = (ICard)JsonConvert.DeserializeObject(Nag, card.GetType());

            card.ProcessTransition();

            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

            //card.Description = HttpUtility.HtmlDecode(card.Description);

            var ndl = new NaggerDataLinker();

            ndl.Update(card);

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

    }
}
