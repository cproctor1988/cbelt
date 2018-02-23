using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace DojoActivity.Models{
    public class ActivityViewModel : BaseEntity{
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
        [Required]
        public DateTime Date{get;set;}
        [Required]
        public string Duration {get;set;}
        [Required]
        public string DurationType{get;set;}
        [Required]
        public int UserId{get;set;}
         

    }}
    
