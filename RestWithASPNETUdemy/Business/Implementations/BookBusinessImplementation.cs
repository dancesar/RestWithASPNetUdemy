using RestWithASPNETUdemy.model;
using RestWithASPNETUdemy.Repository;

namespace RestWithASPNETUdemy.Business.Implementations;

public class BookBusinessImplementation : IBooksBusiness
{
    private readonly IBooksRepository _repository;

    public BookBusinessImplementation(IBooksRepository repository)
    {
        _repository = repository;
    }

    public Books FindById(long id)
    {
        return _repository.FindById(id);
    }

    public List<Books> FindAll()
    {
        return _repository.FindAll();
    }

    public Books Create(Books books)
    {
        return _repository.Create(books);
    }

    public Books Update(Books books)
    {
        return _repository.Update(books);
    }

    public void Delete(long id)
    {
        _repository.Delete(id);
    }
}