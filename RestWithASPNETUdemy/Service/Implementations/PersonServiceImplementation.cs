using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;
using RestWithASPNETUdemy.Repository;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Business.Implementations;

public class PersonServiceImplementation : IPersonService
{
    private readonly IPersonRepository _repository;
    
    private readonly PersonConverter _converter;

    public PersonServiceImplementation(IPersonRepository repository)
    {
        _repository = repository;
        _converter = new PersonConverter();
    }

    public PersonDTO FindById(long id)
    {
        return _converter.Parse(_repository.FindById(id));
    }

    public PersonDTO Diasable(long id)
    {
        var personEntity = _repository.Disable(id);
        return _converter.Parse(personEntity);
    }

    public List<PersonDTO> FindAll()
    {
        return _converter.Parse(_repository.FindAll());
    }

    public PersonDTO Create(PersonDTO personDto)
    {
        var personEntity = _converter.Parse(personDto);
        personEntity = _repository.Create(personEntity);
        return _converter.Parse(personEntity);
    }

    public PersonDTO Update(PersonDTO personDto)
    {
        var personEntity = _converter.Parse(personDto);
        personEntity = _repository.Update(personEntity);
        return _converter.Parse(personEntity);
    }

    public void Delete(long id)
    {
        _repository.Delete(id);
    }

}