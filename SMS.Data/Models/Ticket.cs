using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SMS.Data.Models
{
    // used in ticket search feature
    public enum TicketRange { OPEN, CLOSED, ALL }

    public class Ticket
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Issue { get; set; }

        // TBC - add Resolution string attribute and ResolvedOn DateTime (initialise with DateTime.Min)
        
        public DateTime CreatedOn { get; set; } = DateTime.Now;
       
        
        public bool Active { get; set; } = true;

        // ticket owned by a student
        public int StudentId { get; set; }      // foreign key
        public Student Student { get; set; }    // navigation property
    } 
}
