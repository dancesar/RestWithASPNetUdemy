using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.Filters.Utils;

namespace RestWithASPNETUdemy.Business;

public interface IPersonService
{
    PersonDTO Create(PersonDTO personDto);
    PersonDTO FindById(long id);
    List<PersonDTO> FindByName(string firstName, string lastName);
    PersonDTO Update(PersonDTO personDto);
    void Delete(long id);
    PersonDTO Diasable(long id);
    List<PersonDTO> FindAll();
    PagedSearchDTO<PersonDTO> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
}