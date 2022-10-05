﻿namespace IncidentAPI.Models
{
    public class Contact
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}
