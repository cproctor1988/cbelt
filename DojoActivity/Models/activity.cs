using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace DojoActivity.Models{
    public class Activity : BaseEntity{
        [Key]
        public int ActivityId {get;set;}
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Title{get;set;}
        [Required]
        [MinLength(10)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Description {get;set;}
        public DateTime Date{get;set;}
        public string Duration {get;set;}
        public string DurationType{get;set;}
        public int UserId{get;set;}
        public User EventCord{get;set;}

        [InverseProperty("Activity")]
        public List<Guest> Guests{get;set;} 

    public Activity()
    {
        Guests = new List<Guest>();
    }
    public List<int> getUsers(){
            List<int> guests = new List<int>();
            foreach(var g in Guests){
                guests.Add(g.UserId);
            }
            return guests;
    }
    }
}