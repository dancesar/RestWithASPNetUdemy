using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;

namespace RestWithASPNETUdemy.Business;

public interface IBooksService
{
    BooksDTO Create(BooksDTO booksDto);
    BooksDTO FindById(long id);
    BooksDTO Update(BooksDTO books);
    void Delete(long id);
    List<BooksDTO> FindAll();
}