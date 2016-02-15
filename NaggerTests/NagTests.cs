using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaggerLibrary;
using NaggerLibrary.Mock;
using NaggerLibrary.Cards;
using Newtonsoft.Json;

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

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.DueDate = SystemTime.Now.Invoke().AddDays(2);
            testCard.LastDone = SystemTime.Now.Invoke().AddDays(-2);
            testCard.FrequencyID = 60;

            string Nag = JsonConvert.SerializeObject(testCard);

            DateTime previousDueDate = testCard.DueDate;
            DateTime dueDate = testCard.DueDate.AddDays(testCard.FrequencyID);
                       

            CardManager mgr = new CardManager();
            ICard card = mgr.ProcessCard(Nag, "Could", "Done");


            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");

            Assert.AreEqual(previousDueDate, card.PreviousDueDate, "Previous due date");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");
            
        }

        [TestMethod]
        public void MoveFromCouldDoToSkipped()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.DueDate = SystemTime.Now.Invoke().AddDays(2);
            testCard.LastDone = SystemTime.Now.Invoke().AddDays(-2);
            testCard.FrequencyID = 60;
            testCard.SkipCount = 1;

            string Nag = JsonConvert.SerializeObject(testCard);

            DateTime previousDueDate = testCard.DueDate;
            DateTime dueDate = testCard.DueDate.AddDays(testCard.FrequencyID);
            int skipCount = testCard.SkipCount + 1;
            
            CardManager mgr = new CardManager();
            ICard card = mgr.ProcessCard(Nag, "Could", "Skip");

            
            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastSkip, "Last skipped date");
            Assert.AreEqual((int)ColumnType.colSkip, card.ColumnID, "Column ID");
            Assert.AreEqual(skipCount, card.SkipCount, "Skip count");


            Assert.AreEqual(previousDueDate, card.PreviousDueDate, "Previous due date");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");

        }


        [TestMethod]
        public void MoveFromDoneToCouldDo()
        { 
        
        
        }

        #endregion

        #region CouldDo

        [TestMethod]
        public void AssignCardToCouldDoColumn()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            var card = new Card();
            card.FrequencyID = (int)Frequency.NowAndThen;
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
            card.FrequencyID = (int)Frequency.NowAndThen;
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
            card.FrequencyID = (int)Frequency.NowAndThen;
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
            card.FrequencyID = (int)Frequency.Specific;
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
            card.FrequencyID = (int)Frequency.Daily;
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
            card.FrequencyID = (int)Frequency.NowAndThen;
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
            card.FrequencyID = (int)Frequency.Daily;
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
            card.FrequencyID = (int)Frequency.NowAndThen;
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
