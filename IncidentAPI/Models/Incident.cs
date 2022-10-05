using System.ComponentModel.DataAnnotations;

namespace IncidentAPI.Models
{
    public class Incident
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int AccountId { get; set; }

        public Account Account { get; set; }

        public Incident()
        {
            Name = new Guid().ToString();
        }
    }
}
