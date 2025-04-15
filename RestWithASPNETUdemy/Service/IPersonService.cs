using RestWithASPNETUdemy.Data.DTO;
using RestWithASPNETUdemy.model;

namespace RestWithASPNETUdemy.Business;

public interface IPersonService
{
    PersonDTO Create(PersonDTO personDto);
    PersonDTO FindById(long id);
    PersonDTO Update(PersonDTO personDto);
    void Delete(long id);
    PersonDTO Diasable(long id);
    List<PersonDTO> FindAll();
}