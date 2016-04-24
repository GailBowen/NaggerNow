using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaggerLibrary;

using System.Data;

namespace NagManager.Controllers
{
    public class NagsController : Controller
    {
        // GET: Nags
        public ActionResult Index()
        {
            NaggerDataLinker ndl = new NaggerDataLinker();
            IEnumerable<Card> cards = ndl.GetCardCollection();
            
            return View(cards);
        }

        // GET: Nags/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Nags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nags/Create
        [HttpPost]
        public ActionResult Create(Card newCard)
        {
            //Card newCard = new Card();

            try
            {

                //newCard.ColumnID = Convert.ToInt16(collection["ColumnID"]);
                //newCard.BoardID = Convert.ToInt16(collection["BoardID"]);
                //newCard.FrequencyID = Convert.ToInt16(collection["FrequencyID"]);
                //var test = collection["Mandated"];
                //newCard.Mandated = Convert.ToBoolean(collection["Mandated"]);
                //newCard.LocationID = Convert.ToInt16(collection["LocationID"]);
                //newCard.Title = collection["Title"];
                //newCard.Description = collection["Description"];

                NaggerDataLinker ndl = new NaggerDataLinker();
                ndl.InsertCard(newCard);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                string bob = ex.Message;
                return View(newCard);
            }
        }

        // GET: Nags/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Nags/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Nags/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Nags/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
