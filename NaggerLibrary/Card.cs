using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
