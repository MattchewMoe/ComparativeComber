
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CompComber.Shared;
public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } // Added
    public string LastName { get; set; } // Added
    public string Username { get; set; }
    public string Email { get; set; }

    [JsonIgnore]
    public string Password { get; set; }
    // Add a foreign key for Organization
    public int OrganizationId { get; set; }

    // Navigation property
    [ForeignKey("OrganizationId")]
    public virtual Organization? Organization { get; set; }
}