using System.Collections.Generic;
using UpravljanjeVozilima.Models;

namespace UpravljanjeVozilima.Dal.TravelManager
{
    public interface ITravelRepo
    {
        IEnumerable<TravelStatus> GetAllTravelStatuses();
        int AddTravelWarrant(Travel travel);
        IList<Travel> GetAllTravels();
        Travel GetTravelWarrant(int id);
        int DeleteTravelWarrant(int id);
    }
}