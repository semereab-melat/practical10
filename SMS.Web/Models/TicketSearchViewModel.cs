
using System.Collections.Generic;
using SMS.Data.Models;

namespace SMS.Web.Models
{   
    public class TicketSearchViewModel
    {
        // TBC - complete view model to contain 


        // results which is a list of Ticket

        public IList<Ticket> Tickets { get; set; } = new List<Ticket>();

        // query which is a string  
        public string Query { get; set; } = string.Empty;

        // range - a ticket range that defaults to TicketRange.ALL
        public TicketRange Range { get; set; } = TicketRange.ALL;

    }
}
