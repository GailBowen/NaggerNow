using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NaggerLibrary;
using System.Web.Mvc;
using System.Data.Entity;

namespace NagManager.Models
{
    public class CardViewModel: Card
    {
        public IEnumerable<SelectListItem> Boards { get; set; }
    }

    public class Board
    {
        public int ID { get; set; }
        public String Description { get; set; }
    }

  
    public partial class Nags : DbContext
    {
        public Nags()
            : base("name=Nags")
        {
            Database.SetInitializer<Nags>(null);
        }

        public virtual DbSet<Board> Boards { get; set; }

        //public virtual DbSet<Column> Columns { get; set; }
        //public virtual DbSet<Frequency> Frequencies { get; set; }
        //public virtual DbSet<Location> Locations { get; set; }


    }

}