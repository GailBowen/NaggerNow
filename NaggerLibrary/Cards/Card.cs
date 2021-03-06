﻿using Microsoft.ApplicationBlocks.Data;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace NaggerLibrary
{
    public class Card: ICard
    {
        
        #region Properties
            public int ID { get; set; }

            [Display(Name = "Column")]
            public int ColumnID { get; set; }

            public bool Mandated { get; set; }

            [Display(Name = "Board")]
            public int BoardID { get; set; }

            [Display(Name = "Frequency")]
            public int FrequencyID { get; set; }

            [Display(Name = "Location")]
            public int LocationID { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public DateTime Created { get; set; }

            public DateTime DueDate { get; set; }
        
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

        public virtual bool ProcessTransition(string fromColumn, ICard penultimateAction, ICard mostRecentAction)
        {
            return true;
        }

        public void Undo(string fromColumn, ICard penultimateAction)
        {
            DueDate = penultimateAction.DueDate;
            LastDone = penultimateAction.LastDone;

            if (fromColumn.ToLower() == "skip")
            {
                LastSkip = penultimateAction.LastSkip;
                SkipCount = penultimateAction.SkipCount;
            }
        }
    }
           
}
