using System.Linq;
using Webapi.Common;
using Webapi.DbOperation;

namespace Webapi.BookOperations.GetBookById{
    
    public class GetBookById{

        private GetByIdModel Model{get;set;}
        private readonly BookStoreDbContext _context;
        public GetBookById(BookStoreDbContext context){
            _context = context;
        }
        public GetByIdModel Handle(int id){
            
            Model = new GetByIdModel();
            var book = _context.Books.Where(x => x.ID == id).FirstOrDefault();
            Model.Title= book.Title;
            Model.PageCount = book.PageCount;
            Model.Genre = ((GenreEnum)book.GenreId).ToString();
            Model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
            return Model;
            
            
        }
        
    }
    public class GetByIdModel{
        public string Title { get; set; }
        public int PageCount{get;set;}
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}