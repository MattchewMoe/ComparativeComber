
namespace ComparativeComber.Entities;
    
        public class RegisterRequest
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int OrganizationId { get; set; }  // Changed from string Organization to int OrganizationId
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            // Add other fields if necessary
        }
    



