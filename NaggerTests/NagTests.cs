﻿using System;
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
                        
            DateTime dueDate = SystemTime.Now.Invoke().Date.AddDays(testCard.FrequencyID);
            
            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");
            card.ProcessTransition("Could", new Card(), new Card());
            
            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done date");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");
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
                    
            DateTime dueDate = testCard.DueDate.AddDays(testCard.FrequencyID);
            int skipCount = testCard.SkipCount + 1;
            
            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Skip");
            card.ProcessTransition("Could", new Card(), new Card());
                        
            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastSkip, "Last skipped date");
            Assert.AreEqual((int)ColumnType.colSkip, card.ColumnID, "Column ID");
            Assert.AreEqual(skipCount, card.SkipCount, "Skip count");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");

        }

        [TestMethod]
        public void MoveFromShouldDoToDone()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.LastDone = SystemTime.Now.Invoke().AddDays(-59);
            testCard.FrequencyID = 60;

            string Nag = JsonConvert.SerializeObject(testCard);

            DateTime dueDate = SystemTime.Now.Invoke().Date.AddDays(testCard.FrequencyID);

            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");
            card.ProcessTransition("Should", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done date");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");
        }

        [TestMethod]
        public void MoveFromShouldDoToSkipped()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.LastDone = SystemTime.Now.Invoke().AddDays(-2);
            testCard.FrequencyID = 60;
            testCard.SkipCount = 1;

            string Nag = JsonConvert.SerializeObject(testCard);
                    
            DateTime dueDate = testCard.DueDate.AddDays(testCard.FrequencyID);
            int skipCount = testCard.SkipCount + 1;

            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Skip");
            card.ProcessTransition("Should", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastSkip, "Last skipped date");
            Assert.AreEqual((int)ColumnType.colSkip, card.ColumnID, "Column ID");
            Assert.AreEqual(skipCount, card.SkipCount, "Skip count");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");

        }

        [TestMethod]
        public void MoveFromMustDoToDone()
        {
            SystemTime.Now = () => new DateTime(2016, 1, 20, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.LastDone = SystemTime.Now.Invoke().AddDays(-1);
            testCard.FrequencyID = 1;

            string Nag = JsonConvert.SerializeObject(testCard);

            DateTime dueDate = SystemTime.Now.Invoke().Date.AddDays(testCard.FrequencyID);

            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");
            card.ProcessTransition("Must", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done date");
            Assert.AreEqual(dueDate, card.DueDate, "Due date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");
        }

        [TestMethod]
        public void MoveFromCouldDoToDoneThenBackToCouldDo()
        {
            SystemTime.Now = () => new DateTime(2016, 02, 01, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.LastDone = new DateTime(2016, 01, 26, 6, 36, 0);
            testCard.DueDate = new DateTime(2016, 02, 02, 6, 36, 0);
            testCard.FrequencyID = 7;
            testCard.ColumnID = (int)ColumnType.colCould;

            DateTime originalLastDone = testCard.LastDone;
            DateTime originalDueDate = testCard.DueDate;

            string Nag = JsonConvert.SerializeObject(testCard);


            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");

            card.ProcessTransition("Could", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done");
            Assert.AreEqual(SystemTime.Now.Invoke().Date.AddDays(7), card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");

            Nag = JsonConvert.SerializeObject(card);

            card = mgr.DeserializeCard(Nag, "Could");
            card.ProcessTransition("Done", testCard, new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate, card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colCould, card.ColumnID, "ColumnID");
        
        }

        [TestMethod]
        public void MoveFromShouldDoToDoneThenBackToShouldDo()
        {
            SystemTime.Now = () => new DateTime(2016, 02, 01, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.LastDone = new DateTime(2016, 01, 26, 6, 36, 0);
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.FrequencyID = 7;
            testCard.ColumnID = (int)ColumnType.colShould;

            DateTime originalLastDone = testCard.LastDone;
            DateTime originalDueDate = testCard.DueDate;

            string Nag = JsonConvert.SerializeObject(testCard);
            
            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");
            card.ProcessTransition("Should", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done");
            Assert.AreEqual(SystemTime.Now.Invoke().Date.AddDays(7), card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");

            Nag = JsonConvert.SerializeObject(card);

            card = mgr.DeserializeCard(Nag, "Should");
            card.ProcessTransition("Done", testCard, new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate, card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colShould, card.ColumnID, "ColumnID");
        }

        [TestMethod]
        public void MoveFromMustDoToDoneThenBackToMustDo()
        {
            SystemTime.Now = () => new DateTime(2016, 02, 01, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.LastDone = new DateTime(2016, 01, 26, 6, 36, 0);
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.FrequencyID = 7;
            testCard.ColumnID = (int)ColumnType.colMust;

            DateTime originalLastDone = testCard.LastDone;
            DateTime originalDueDate = testCard.DueDate;

            string Nag = JsonConvert.SerializeObject(testCard);

            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");
            card.ProcessTransition("Must", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done");
            Assert.AreEqual(SystemTime.Now.Invoke().Date.AddDays(7), card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");

            Nag = JsonConvert.SerializeObject(card);

            card = mgr.DeserializeCard(Nag, "Must");
            card.ProcessTransition("Done", testCard, new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate, card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colMust, card.ColumnID, "ColumnID");
        }

        [TestMethod]
        public void MoveFromCouldDoToSkipThenBackToCouldDo()
        {
            SystemTime.Now = () => new DateTime(2016, 02, 01, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.LastDone = new DateTime(2016, 01, 26, 6, 36, 0);
            testCard.DueDate = new DateTime(2016, 02, 02, 6, 36, 0);
            testCard.FrequencyID = 7;
            testCard.SkipCount = 1;
            testCard.LastSkip = new DateTime(2016, 01, 01, 6, 36, 0);
            testCard.ColumnID = (int)ColumnType.colCould;

            DateTime originalLastDone = testCard.LastDone;
            DateTime originalDueDate = testCard.DueDate;
            DateTime originalLastSkip = testCard.LastSkip;
            int originalSkipCount = testCard.SkipCount;

            string Nag = JsonConvert.SerializeObject(testCard);


            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Skip");
            card.ProcessTransition("Could", new Card(), new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate.AddDays(testCard.FrequencyID), card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colSkip, card.ColumnID, "ColumnID");
            Assert.AreEqual(originalSkipCount + 1, card.SkipCount, "Skip Count");
            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastSkip, "Skip date");

            Nag = JsonConvert.SerializeObject(card);

            card = mgr.DeserializeCard(Nag, "Could");
            card.ProcessTransition("Skip", testCard, new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate, card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colCould, card.ColumnID, "ColumnID");
            Assert.AreEqual(originalSkipCount, card.SkipCount, "Skip Count");
            Assert.AreEqual(originalLastSkip, card.LastSkip, "Skip date");
        }

        [TestMethod]
        public void MoveFromShouldDoToSkipThenBackToShouldDo()
        {
            SystemTime.Now = () => new DateTime(2016, 02, 01, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.LastDone = new DateTime(2016, 01, 26, 6, 36, 0);
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.FrequencyID = 7;
            testCard.SkipCount = 1;
            testCard.LastSkip = new DateTime(2016, 01, 01, 6, 36, 0);
            testCard.ColumnID = (int)ColumnType.colShould;

            DateTime originalLastDone = testCard.LastDone;
            DateTime originalDueDate = testCard.DueDate;
            DateTime originalLastSkip = testCard.LastSkip;
            int originalSkipCount = testCard.SkipCount;

            string Nag = JsonConvert.SerializeObject(testCard);


            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Skip");
            card.ProcessTransition("Should", new Card(), new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate.AddDays(testCard.FrequencyID), card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colSkip, card.ColumnID, "ColumnID");
            Assert.AreEqual(originalSkipCount + 1, card.SkipCount, "Skip Count");
            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastSkip, "Skip date");

            Nag = JsonConvert.SerializeObject(card);

            card = mgr.DeserializeCard(Nag, "Should");
            card.ProcessTransition("Skip", testCard, new Card());

            Assert.AreEqual(originalLastDone, card.LastDone, "Last done");
            Assert.AreEqual(originalDueDate, card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colShould, card.ColumnID, "ColumnID");
            Assert.AreEqual(originalSkipCount, card.SkipCount, "Skip Count");
            Assert.AreEqual(originalLastSkip, card.LastSkip, "Skip date");
        }
               
        #endregion

        #region DoNotMoveCards
        [TestMethod]
        public void DoNotMoveFromMustDoToDoneThenToCouldDo()
        {
            SystemTime.Now = () => new DateTime(2016, 02, 01, 6, 36, 0);

            Card testCard = new Card();
            testCard.ID = 1;
            testCard.LastDone = new DateTime(2016, 01, 26, 6, 36, 0);
            testCard.DueDate = SystemTime.Now.Invoke().Date;
            testCard.FrequencyID = 7;
            testCard.ColumnID = (int)ColumnType.colMust;

            DateTime originalLastDone = testCard.LastDone;
            DateTime originalDueDate = testCard.DueDate;

            string Nag = JsonConvert.SerializeObject(testCard);

            CardManager mgr = new CardManager();
            ICard card = mgr.DeserializeCard(Nag, "Done");
            card.ProcessTransition("Must", new Card(), new Card());

            Assert.AreEqual(SystemTime.Now.Invoke().Date, card.LastDone, "Last done");
            Assert.AreEqual(SystemTime.Now.Invoke().Date.AddDays(7), card.DueDate, "Due Date");
            Assert.AreEqual((int)ColumnType.colDone, card.ColumnID, "ColumnID");

            Nag = JsonConvert.SerializeObject(card);

            card = mgr.DeserializeCard(Nag, "Could");
            bool result = card.ProcessTransition("Done", testCard, new Card());

            Assert.AreEqual(false, result, "Process transition result");
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
