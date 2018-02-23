using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DojoActivity.Models{
    public class Guest : BaseEntity{
        [Key]
        public int GuestId {get;set;}
        public int UserId{get;set;}
        
        public User user{get;set;}
        
        public int ActivityId{get;set;}

        public Activity Activity{get;set;}

    }
}