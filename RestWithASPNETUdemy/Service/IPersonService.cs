using RestWithASPNETUdemy.Data.DTO;

namespace RestWithASPNETUdemy.Business;

public interface IPersonService
{
    PersonDTO Create(PersonDTO personDto);
    PersonDTO FindById(long id);
    PersonDTO Update(PersonDTO personDto);
    void Delete(long id);
    List<PersonDTO> FindAll();
}