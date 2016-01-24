using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;

namespace NaggerLibrary
{
    public class Card
    {
        public int ID { get; set; }

        public int ColumnID { get; set; }

        public bool Mandated { get; set; }

        public int BoardID { get; set; }

        public int CardType { get; set; }

        public int LocationID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime DueDate { get; set; }

        public int SkipCount { get; set; }

        public DateTime LastSkip { get; set; }

        public DateTime LastDone { get; set; }

        public bool Completed { get; set; }

        public Card()
        {

        }

        public Card(SqlDataReader rdr)
        {
            ID = Convert.ToInt32(rdr["ID"]);
            ColumnID = Convert.ToInt32(rdr["ColumnID"]);
            Mandated = Convert.ToBoolean(rdr["Mandated"]);
            BoardID = Convert.ToInt32(rdr["BoardID"]);
            CardType = Convert.ToInt32(rdr["CardType"]);
            LocationID = Convert.ToInt32(rdr["LocationID"]);
            Title = Convert.ToString(rdr["Title"]);
            Description = Convert.ToString(rdr["Description"]);
            Created = Convert.ToDateTime(rdr["Created"]);
            DueDate = Convert.ToDateTime(rdr["DueDate"]);
            SkipCount = Convert.ToInt16(rdr["SkipCount"]);
            LastSkip = rdr["LastSkip"] as DateTime? ?? default(DateTime);
            LastDone = rdr["LastDone"] as DateTime? ?? default(DateTime);
            Completed = Convert.ToBoolean(rdr["Completed"]);
        }


        public ColumnType GetColumn()
        {
            return CardCategorizerChain.GetColumn(this);
        }

        public void UpdateColumn()
        {
            string dbConnString = ConfigurationManager.ConnectionStrings["NaggerConn"].ConnectionString;

            string spName = "Card_Update";

            SqlParameter[] sqlParam = SqlHelperParameterCache.GetSpParameterSet(dbConnString, spName);
            sqlParam[0].Value = ID;
            sqlParam[1].Value = (int)GetColumn();

            SqlHelper.ExecuteNonQuery(dbConnString, CommandType.StoredProcedure, spName, sqlParam);
        }

    }
        
        public enum ColumnType
        {
            colNone = 1,
            colCould = 2,
            colShould = 3,
            colMust = 4,
            colDone = 5,
            colSkip = 6,
        }

    
}
