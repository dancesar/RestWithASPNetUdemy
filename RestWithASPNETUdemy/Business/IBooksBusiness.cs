using RestWithASPNETUdemy.model;

namespace RestWithASPNETUdemy.Business;

public interface IBooksBusiness
{
    Books Create(Books books);
    Books FindById(long id);
    Books Update(Books books);
    void Delete(long id);
    List<Books> FindAll();
}