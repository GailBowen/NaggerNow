﻿using System;
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
    public class NaggerDataLinker: INaggerDataLinker
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public Card Fetch(IDataReader rdr)
        {
            var card = new Card();
            card.ID = Convert.ToInt32(rdr["ID"]);
            card.ColumnID = Convert.ToInt32(rdr["ColumnID"]);
            card.Mandated = Convert.ToBoolean(rdr["Mandated"]);
            card.BoardID = Convert.ToInt32(rdr["BoardID"]);
            card.Frequency = Convert.ToInt32(rdr["frequency"]);
            card.LocationID = Convert.ToInt32(rdr["LocationID"]);
            card.Title = Convert.ToString(rdr["Title"]);
            card.Description = Convert.ToString(rdr["Description"]);
            card.Created = Convert.ToDateTime(rdr["Created"]);
            card.DueDate = Convert.ToDateTime(rdr["DueDate"]);
            card.SkipCount = Convert.ToInt16(rdr["SkipCount"]);
            card.LastSkip = rdr["LastSkip"] as DateTime? ?? default(DateTime);
            card.LastDone = rdr["LastDone"] as DateTime? ?? default(DateTime);
            card.Completed = Convert.ToBoolean(rdr["Completed"]);

            return card;
        }

        public IDataReader FetchAll()
        {   

                const string spName = "[dbo].[Cards_FetchAll]";


                string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;


                SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
                
                return SqlHelper.ExecuteReader(dbConnString, CommandType.StoredProcedure, spName, sqlParam);               

        }

        public List<Card> GetCardCollection()
        {
            List<Card> cards = new List<Card>();

            using (IDataReader rdr = FetchAll())
            {
                while (rdr.Read())
                {
                    cards.Add(Fetch(rdr));
                }
            }

            return cards;
        }

        public void Update(Card card)
        {
            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

            string spName = "Card_Update";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = card.ID;
            sqlParam[1].Value = card.ColumnID;
            sqlParam[2].Value = card.Description;

            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);
        }

    }
}