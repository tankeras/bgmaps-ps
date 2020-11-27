using Mapsps.Data.Common.Models;

namespace Mapsps.Data.Models
{
    public class Nickname : BaseModel<int>
    {

        public string Name { get; set; }

        public Cat Cat { get; set; }

        public int Votes { get; set; }
    }
}