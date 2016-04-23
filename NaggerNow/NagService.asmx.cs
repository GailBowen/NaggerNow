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
using NaggerLibrary.Mock;

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
                        frequencyID = card.FrequencyID,
                        dueDate = card.DueDate,
                        lastDone = card.LastDone,
                        skipCount = card.SkipCount
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
        public void ProcessCard(string Nag, string fromColumn, string toColumn)
        {
           
                    
            var ndl = new NaggerDataLinker();

            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, toColumn);
            ICard mostRecentAction = ndl.GetMostRecentAction(card.ID);
            ICard previousAction = ndl.GetPenultimateAction(card.ID);
            if (card.ProcessTransition(fromColumn, previousAction, mostRecentAction) == false)
            {
                log.InfoFormat("Error updating: {0}", Nag);

                var result = new { successMessage = "" };
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(result));
            }
            else
            {
                ndl.Update(card);
                log.InfoFormat("Card updated: {0}", Nag);

                var result = new { successMessage = "Card moved" };

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(result));
            }
          
            
        }

    }
}
