using Microsoft.ApplicationBlocks.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NaggerLibrary
{
    public class Card: ICard
    {
        
        #region Properties
        public int ID { get; set; }

        public int ColumnID { get; set; }

        public bool Mandated { get; set; }

        public int BoardID { get; set; }

        public int Frequency { get; set; }

        public int LocationID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PreviousDueDate { get; set; }

        public int SkipCount { get; set; }

        public DateTime LastSkip { get; set; }

        public DateTime LastDone { get; set; }

        public bool Completed { get; set; }
        #endregion

        public Card()
        {

        }
             

        public ColumnType AssignColumn()
        {
            return CardCategorizerChain.GetColumn(this);
        }
        
    }
           
}
