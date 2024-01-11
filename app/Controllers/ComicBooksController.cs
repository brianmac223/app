using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComicBooksController : ControllerBase
    {
        private static List<ComicBook> _comicBooks = new List<ComicBook>
        {
            new ComicBook { Id = 1, Title = "Spider-Man", Author = "Stan Lee", Price = 9.99m },
            new ComicBook { Id = 2, Title = "Batman", Author = "Bob Kane", Price = 8.99m }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ComicBook>> Get()
        {
            return Ok(_comicBooks);
        }

        [HttpGet("{id}")]
        public ActionResult<ComicBook> GetById(int id)
        {
            var comicBook = _comicBooks.FirstOrDefault(c => c.Id == id);
            if (comicBook == null)
                return NotFound();

            return Ok(comicBook);
        }

        [HttpPost]
        public ActionResult<ComicBook> Post([FromBody] ComicBook newComicBook)
        {
            newComicBook.Id = _comicBooks.Count + 1;
            _comicBooks.Add(newComicBook);
            return CreatedAtAction(nameof(GetById), new { id = newComicBook.Id }, newComicBook);
        }

        [HttpPut("{id}")]
        public ActionResult<ComicBook> Put(int id, [FromBody] ComicBook updatedComicBook)
        {
            var existingComicBook = _comicBooks.FirstOrDefault(c => c.Id == id);
            if (existingComicBook == null)
                return NotFound();

            existingComicBook.Title = updatedComicBook.Title;
            existingComicBook.Author = updatedComicBook.Author;
            existingComicBook.Price = updatedComicBook.Price;

            return Ok(existingComicBook);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var comicBook = _comicBooks.FirstOrDefault(c => c.Id == id);
            if (comicBook == null)
                return NotFound();

            _comicBooks.Remove(comicBook);
            return NoContent();
        }
    }
}


