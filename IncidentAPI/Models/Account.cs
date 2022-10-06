namespace IncidentAPI.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ContactId { get; set; }

        // Possibly change on incident creation
        public Contact Contact { get; set; }

        public List<Incident> Incidents { get; set; } = new List<Incident> { };
    }
}
