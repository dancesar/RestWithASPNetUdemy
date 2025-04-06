using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Business.Implementations;

public class BookServiceImplementation : IBooksService
{
    private readonly IRepository<Books> _repository;
    
    private readonly BooksConverter _converter;

    public BookServiceImplementation(IRepository<Books> repository)
    {
        _repository = repository;
        _converter = new BooksConverter();
    }

    public BooksDTO FindById(long id)
    {
        return _converter.Parse(_repository.FindById(id));
    }

    public List<BooksDTO> FindAll()
    {
        return _converter.Parse(_repository.FindAll());
    }

    public BooksDTO Create(BooksDTO booksDto)
    {
        var booksEntity = _converter.Parse(booksDto);
        booksEntity = _repository.Create(booksEntity);
        return _converter.Parse(booksEntity);
    }

    public BooksDTO Update(BooksDTO booksDto)
    {
        var booksEntity = _converter.Parse(booksDto);
        booksEntity = _repository.Update(booksEntity);
        return _converter.Parse(booksEntity);
    }

    public void Delete(long id)
    {
        _repository.Delete(id);
    }
}