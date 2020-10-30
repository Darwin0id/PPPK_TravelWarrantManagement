using System.Collections.Generic;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Dal.PersonManager
{
    public interface IPersonRepo
    {
        IList<Person> GetAllPersons();
        int DeletePerson(int id);
        int AddPerson(Person person);
        Person GetPerson(int id);
        int EditPerson(Person person);
    }
}