using RestWithASPNETUdemy.model;
using RestWithASPNETUdemy.Repository.Generic;

namespace RestWithASPNETUdemy.Repository;

public interface IPersonRepository : IRepository<Person>
{
    Person Disable(long id);
    
    List<Person> FindByName(string firstName, string lastName);
}