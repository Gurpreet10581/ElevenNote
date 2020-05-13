using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]//This annotation makes it so that the views are accessible only to logged in users
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            //this was changed with below code-
            //var model = new NoteListItem[0];//
            //we are initializing a new instance of the NoteListItem as an IEnumerable with the [0] syntax. This will satisfy some of the requirements for our Index View. When we added the List template for our view, it created some IEnumerable requirements for our list view. More on that later.

            var userId = Guid.Parse(User.Identity.GetUserId());//method displays all the notes for the current user. by calling upon below method and services
            var service = new NoteService(userId);
            var model = service.GetNotes();
            return View(model);
        }

        //Create
        //Get-We are making a request to get the Create View
        public ActionResult Create()
        {
            return View();
        }
        
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)//method makes sure the model is valid, grabs the current userId, calls on CreateNote, and returns the user back to the index view.
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your note was created.";//if note is created this message will appear
                return RedirectToAction("Index");
            }
                ModelState.AddModelError("", "Note could not be created");// if note wasn't created this message will appear
            return View(model);
           
        }
        public ActionResult Details(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model =
                new NoteEdit
                {
                    NoteId = detail.NoteId,
                    Title = detail.Title,
                    Content = detail.Content
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Your note was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your note could not be updated.");
            return View();
        }
        public ActionResult Delete(int id)
        {
            var svc = CreateNoteService();
            var model = svc.GetNoteById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var serice = CreateNoteService();
            serice.DeleteNote(id);
            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}