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
        #region MoveCards
        [TestMethod]
        public void MoveFromCouldDoToDone()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            DateTime dueDate = new DateTime(2016, 1, 21); //This is in the future

            var card = new Card();
            card.Frequency = (int)Frequency.NowAndThen;
            card.Mandated = false;
            card.DueDate = dueDate;
            card.ColumnID = (int)ColumnType.colCould;
            
            //Card gets done
            card.LastDone = SystemTime.Now.Invoke().Date;
            card.ColumnID = (int)ColumnType.colDone;
            card.PreviousDueDate = card.DueDate;
            card.DueDate = card.DueDate.AddDays(card.Frequency);

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone);
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID);
            Assert.AreEqual(dueDate, card.PreviousDueDate);
            Assert.AreEqual(dueDate.AddDays(180), card.DueDate);
            

        }

        #endregion

        #region CouldDo

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


        #endregion

        #region ShouldDo
        [TestMethod]
        public void AssignCardToShouldDoColumn_DueToday()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.NowAndThen;
            card.Mandated = false;
            card.DueDate = new DateTime(2016, 1, 20); //This is today
            card.SkipCount = 1;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colShould, colType);
        }
        #endregion

        #region MustDo
        [TestMethod]
        public void AssignCardToMustDoColumn_SkippedTwice()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.NowAndThen;
            card.Mandated = false;
            card.DueDate = new DateTime(2016, 1, 20); //This is today
            card.SkipCount = 2;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colMust, colType);
        }

        [TestMethod]
        public void AssignCardToMustDoColumn_SpecificDueToday()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.Specific;
            card.Mandated = false;
            card.DueDate = new DateTime(2016, 1, 20); //This is today
            card.SkipCount = 0;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colMust, colType);
        }

        [TestMethod]
        public void AssignCardToMustDoColumn_MandatedDueToday()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.Daily;
            card.Mandated = true;
            card.DueDate = new DateTime(2016, 1, 20); //This is today
            card.SkipCount = 0;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colMust, colType);
        }

        #endregion
        
        #region Done
        [TestMethod]
        public void AssignCardToDone()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.NowAndThen;
            card.Mandated = false;
            card.DueDate = new DateTime(2016, 1, 20);
            card.LastDone = new DateTime(2016, 1, 20); //Card was done today
            card.SkipCount = 2;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colDone, colType);
        }


        [TestMethod]
        public void AssignCardToMustDoColumn_MandatedDueToday_DoneToday()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.Daily;
            card.Mandated = true;
            card.DueDate = new DateTime(2016, 1, 20); 
            card.LastDone = new DateTime(2016, 1, 20); //This is today
            card.SkipCount = 0;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colDone, colType);
        }
        #endregion

        #region Skip
        [TestMethod]
        public void AssignCardToSkipped()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.Frequency = (int)Frequency.NowAndThen;
            card.Mandated = false;
            card.DueDate = new DateTime(2016, 1, 21); //Not due till tomorrow
            card.LastSkip = new DateTime(2016, 1, 20); //Card was skipped today
            card.SkipCount = 2;

            ColumnType colType = card.AssignColumn();

            Assert.AreEqual(ColumnType.colSkip, colType);
        }

        #endregion

    }
}
