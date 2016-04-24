using NaggerLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;

namespace NagManager.Models
{
    public class CardViewModel: Card
    {
        public IEnumerable<SelectListItem> Boards { get; set; }

        public IEnumerable<SelectListItem> Frequencies { get; set; }
    }

    public class Board
    {
        public int ID { get; set; }
        public String Description { get; set; }
    }
    
    public class Frequency
    {
        [Key]
        public int DayCount { get; set; }
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
        public virtual DbSet<Frequency> Frequencies { get; set; }
        //public virtual DbSet<Location> Locations { get; set; }


    }

}