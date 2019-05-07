using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NETMVCWorkshop1.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            List<Models.BOOK_DATA> l_result = new List<Models.BOOK_DATA>();
            ViewBag.ResultMessage = TempData["ResultMessage"];
            using (Models.BooksManagementEntities db = new Models.BooksManagementEntities())
            {
                l_result = (from m in db.BOOK_DATA select m).ToList();
                return View(l_result);
            }
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Models.BOOK_DATA b_data)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.BooksManagementEntities db = new Models.BooksManagementEntities())
                {
                    b_data.BOOK_STATUS = "v";
                    db.BOOK_DATA.Add(b_data);
                    db.SaveChanges();
                    TempData["ResultMessage"] = string.Format("書籍[{0}]建立成功", b_data.BOOK_NAME);
                }
            }
            else
            {
                ViewBag.ResultMessage = string.Format("資料有誤,請檢查", b_data.BOOK_NAME);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? b_id)
        {

            using (Models.BooksManagementEntities db = new Models.BooksManagementEntities())
            {
                var l_result = (from m in db.BOOK_DATA where m.BOOK_ID == b_id select m).FirstOrDefault();
                if (l_result != default(Models.BOOK_DATA))
                {
                    return View(l_result);
                }
                else
                {
                    TempData["ResultMessage"] = string.Format("資料有誤,請檢查");
                    return RedirectToAction("Index");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.BOOK_DATA b_data)
        {
            if (this.ModelState.IsValid)
            {
                using (Models.BooksManagementEntities db = new Models.BooksManagementEntities())
                {
                    var l_result = (from m in db.BOOK_DATA where m.BOOK_ID == b_data.BOOK_ID select m).FirstOrDefault();
                    l_result.BOOK_NAME = b_data.BOOK_NAME;
                    l_result.BOOK_AUTHOR = b_data.BOOK_AUTHOR;
                    l_result.BOOK_PUBLISHER = b_data.BOOK_PUBLISHER;
                    l_result.BOOK_NOTE = b_data.BOOK_NOTE;
                    l_result.BOOK_BOUGHT_DATE = b_data.BOOK_BOUGHT_DATE;
                    l_result.BOOK_CLASS_ID = b_data.BOOK_CLASS_ID;
                    l_result.BOOK_STATUS = b_data.BOOK_STATUS;
                    l_result.BOOK_KEEPER = b_data.BOOK_KEEPER;
                    db.SaveChanges();
                    TempData["ResultMessage"] = string.Format("書籍[{0}]修改成功", b_data.BOOK_NAME);
                }
            }
            else
            {
                ViewBag.ResultMessage = string.Format("資料有誤,請檢查", b_data.BOOK_NAME);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? b_id)
        {

            using (Models.BooksManagementEntities db = new Models.BooksManagementEntities())
            {
                var l_result = (from m in db.BOOK_DATA where m.BOOK_ID == b_id select m).FirstOrDefault();
                if (l_result != default(Models.BOOK_DATA))
                {
                    db.BOOK_DATA.Remove(l_result);
                    db.SaveChanges();
                    TempData["ResultMessage"] = string.Format("書籍[{0}]刪除成功", l_result.BOOK_NAME);
                }
                else
                {
                    TempData["ResultMessage"] = string.Format("資料有誤,請檢查");
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Details(int? b_id)
        {

            using (Models.BooksManagementEntities db = new Models.BooksManagementEntities())
            {
                var l_result = (from m in db.BOOK_DATA where m.BOOK_ID == b_id select m).FirstOrDefault();
                if (l_result != default(Models.BOOK_DATA))
                {
                    return View(l_result);
                }
                else
                {
                    TempData["ResultMessage"] = string.Format("資料有誤,請檢查");
                    return RedirectToAction("Index");
                }
            }
        }
    }
}