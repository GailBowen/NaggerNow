using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace NaggerLibrary
{
    public interface ICard
    {
        #region Properties
             int ID { get; set; }

             int ColumnID { get; set; }

             bool Mandated { get; set; }

             int BoardID { get; set; }

             int FrequencyID { get; set; }

             int LocationID { get; set; }

             string Title { get; set; }

             string Description { get; set; }

             DateTime Created { get; set; }

             DateTime DueDate { get; set; }

             int SkipCount { get; set; }

             DateTime LastSkip { get; set; }

             DateTime LastDone { get; set; }

             bool Completed { get; set; }
                   
                      
        #endregion
        
        
        ColumnType AssignColumn();

        bool ProcessTransition(string fromColumn, ICard penultimateAction, ICard mostRecentAction);
    }
}
