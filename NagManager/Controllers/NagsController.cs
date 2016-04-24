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
            var model = new List<CardViewModel>();

            NaggerDataLinker ndl = new NaggerDataLinker();
            List<Card> cards = ndl.GetCardCollection();

            var viewModel =
                from c in cards
                join m in db.Columns on c.ColumnID equals m.ID
                join l in db.Locations on c.LocationID equals l.ID
                join b in db.Boards on c.BoardID equals b.ID
                join f in db.Frequencies on c.FrequencyID equals f.DayCount
                select new CardViewModel {
                    ColumnDescription = m.Description,
                    BoardDescription = b.Description,
                    LocationDescription = l.Description,
                    FrequencyDescription = f.Description,
                    Mandated = c.Mandated,
                    BoardID = c.BoardID,
                    FrequencyID = c.FrequencyID,
                    LocationID = c.LocationID,
                    Title = c.Title,
                    Description = c.Description
                };
                
            return View(viewModel);
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
                Frequencies = GetFrequencies(),
                Locations = GetLocations()
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

        private IEnumerable<SelectListItem> GetLocations()
        {

            var locations = db.Locations.Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.Description
                                });

            return new SelectList(locations, "Value", "Text");
        }
    }
}
