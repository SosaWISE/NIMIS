using System.Collections.Generic;
using WorldDomination.GeographyServices.Core.Entities.Google;

namespace WorldDomination.GeographyServices.Core.Services
{
    public interface IGeocodingService
    {
        string Name { get; }
        //ICollection<Location> Find(string query, CountryType countryType);
    }
}