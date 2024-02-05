using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ComparativeComber.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } // Added
    public string LastName { get; set; } // Added

}
