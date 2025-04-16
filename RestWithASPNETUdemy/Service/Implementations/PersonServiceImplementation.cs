using RestWithASPNETUdemy.Data.Converter.Implementations;
using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Filters.Utils;
using RestWithASPNETUdemy.Repository;

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

    public List<PersonDTO> FindByName(string firstName, string lastName)
    {
        return _converter.Parse(_repository.FindByName(firstName, lastName));
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

    public PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page)
    {
        var sort = (!string.IsNullOrWhiteSpace(sortDirection)) && !sortDirection.Equals("desc") ? "asc" : "desc";
        var size = (pageSize < 1) ? 10 : pageSize;
        var offset  = page > 0 ? (page - 1) * size : 0;

        string query = @"SELECT * FROM person p WHERE 1=1 ";
        if (!string.IsNullOrWhiteSpace(name)) query = query + $"AND p.first_name LIKE '%{name}%' ";
        query += $"ORDER BY p.first_name {sort} LIMIT {size} OFFSET {offset}";
        
        string countQuery = @"SELECT COUNT(*) FROM person p WHERE 1 = 1 ";
        if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $"AND p.first_name LIKE '%{name}%'";
        
        var persons = _repository.FindWithPagedSearch(query);
        int totalRresults = _repository.GetCount(countQuery);
        
        return new PagedSearchDTO<PersonDTO>()
        {
            CurretPage = page,
            List = _converter.Parse(persons),
            PageSize = size,
            SortDirections = sort,
            TotalResults = totalRresults,
        };
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