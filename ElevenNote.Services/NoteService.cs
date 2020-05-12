using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;
        
        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        //Create Note Method below
        public bool CreateNote(NoteCreate model)
        {
            var entity =
                new Note()
                {
                    OwnerId = _userId,//have to have OwnerId so it can communicate with Note and the Guid
                    //all the properties below are from NoteCreate model
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now//have to set it as Datetimeoffset.now so it records the date and time of note created.
                };
            using (var ctx = new ApplicationDbContext()) 
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //Get Note Method below-it has to be type NoteListItem since it is going to provide multiple notes

        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                            e =>
                            new NoteListItem
                            {
                                NoteId = e.NoteId,
                                Title = e.Title,
                                CreatedUtc = e.CreatedUtc
                            }
                        );
                return query.ToArray();
            }
        }


    }
}
