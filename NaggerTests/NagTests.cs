using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaggerLibrary;
using NaggerLibrary.Mock;


namespace NaggerTests
{
    [TestClass]
    public class NagTests
    {

        [TestMethod]
        public void AssignCardToCouldDoColumn()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.NowAndThen;
            card.Mandated = false;
            card.DueDate = new DateTime(2016, 1, 21); //This is in the future

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colCould, colType);
        }
    
    }
}
