using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace NaggerLibrary
{
    public class Card
    {
        public int ID { get; set; }
        
        public int ColumnID { get; set; }

        public int BoardID { get; set; }

        public int CardType { get; set; }

        public int LocationID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime DueDate { get; set; }

        public int SkipCount { get; set; }

        public bool Completed { get; set; }

        public Card(SqlDataReader rdr)
        {
            ID = Convert.ToInt32(rdr["ID"]);
            ColumnID = Convert.ToInt32(rdr["ColumnID"]);
            BoardID = Convert.ToInt32(rdr["BoardID"]);
            CardType = Convert.ToInt32(rdr["CardType"]);
            LocationID = Convert.ToInt32(rdr["LocationID"]);
            Title = Convert.ToString(rdr["Title"]);
            Description = Convert.ToString("Description");
            Created = Convert.ToDateTime("Created");
            DueDate = Convert.ToDateTime("DueDate");
            SkipCount = Convert.ToInt16("SkipCount");
            Completed = Convert.ToBoolean("Completed");

        }

    }
}
