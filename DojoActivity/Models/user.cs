using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DojoActivity.Models{
    public class User : BaseEntity{
        [Key]
        public int userId {get;set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}

        [DataType(DataType.Password)]
        public string Password {get; set;}

        [InverseProperty("user")]
        public List<Guest> ActivitiesImAttending {get;set;}
    public User()
    {
        ActivitiesImAttending = new List<Guest>();
    }
}
}