using ElevenNote.Models;
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
            var model = new NoteListItem[0];//we are initializing a new instance of the NoteListItem as an IEnumerable with the [0] syntax. This will satisfy some of the requirements for our Index View. When we added the List template for our view, it created some IEnumerable requirements for our list view. More on that later.
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
        public ActionResult Create(NoteCreate model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}