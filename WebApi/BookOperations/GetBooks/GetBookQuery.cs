using System.Collections.Generic;
using System.Linq;
using WebApi.Comon;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks{
    public class GetBooksQuery{
        private readonly BookStoreDbContext _dbContext;
        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext =dbContext;
        }
        public List<BookViewModel> Handle()
        {
            var bookList=_dbContext.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BookViewModel> vm = new List<BookViewModel>();
            foreach (var item in bookList)
            {
                vm.Add(new BookViewModel(){
                  Title=item.Title,
                    Genre=((GenreEnum)item.GenreId).ToString(),
                    PublishDate=item.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount=item.PageCount
                });
            }
            return vm;
        }
    }
    public class BookViewModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}