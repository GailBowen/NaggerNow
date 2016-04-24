using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NaggerLibrary;
using NagManager.Models;

using System.Data;

namespace NagManager.Controllers
{
    public class NagsController : Controller
    {

        private Nags db = new Nags();

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
            var model = new CardViewModel
            {
                Columns = GetColumns(),
                Boards = GetBoards(),
                Frequencies = GetFrequencies()
            };

            return View(model);
        }

        // POST: Nags/Create
        [HttpPost]
        public ActionResult Create(CardViewModel newCard)
        {
          
            try
            {
          
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

        private IEnumerable<SelectListItem> GetColumns()
        {

            var columns = db.Columns.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.Description
                                });

            return new SelectList(columns, "Value", "Text");
        }


        private IEnumerable<SelectListItem> GetBoards()
        {

            var boards = db.Boards.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.Description
                                });
            
            return new SelectList(boards, "Value", "Text");
        }


        private IEnumerable<SelectListItem> GetFrequencies()
        {

            var frequencies = db.Frequencies.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.DayCount.ToString(),
                                    Text = x.Description
                                });

            return new SelectList(frequencies, "Value", "Text");
        }

    }
}
