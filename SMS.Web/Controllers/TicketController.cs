using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using SMS.Data.Models;
using SMS.Data.Services;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    [Authorize]
    public class TicketController : BaseController
    {
        private readonly IStudentService svc;
        public TicketController()
        {
            svc = new StudentServiceDb();
        } 

        // GET /ticket/index
        public IActionResult Index()
        {
            // return open tickets
            var tickets = svc.GetOpenTickets();

            return View(tickets);
        }
     
        
        public IActionResult Search(TicketSearchViewModel m)
        {                  
            // TBC - perform query using values in view model and assign 
            //       results to viewmodel Tickets property

            // TBC -- return the View and pass the viewmodel as a param
            return null;
        }        
             
        // GET/ticket/{id}
        public IActionResult Details(int id)
        {
            var ticket = svc.GetTicket(id);
            if (ticket == null)
            {
                Alert("Ticket Not Found", AlertType.warning);  
                return RedirectToAction(nameof(Index));             
            }

            return View(ticket);
        }

        // POST /ticket/close/{id}
        [HttpPost]
        [Authorize(Roles="admin,manager")]
        public IActionResult Close(/* TBC - use bind for Id and Resolution */ Ticket t)
        {
            // close ticket via service
            var ticket = svc.CloseTicket(t.Id); // TBC add resolution from the model */ ;
            if (ticket == null)
            {
                Alert("Ticket Not Found", AlertType.warning);                               
            }
            else
            {
                Alert($"Ticket {t.Id } closed", AlertType.info);  
            }

            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }
       
        // GET /ticket/create
        [Authorize(Roles="admin,manager")]
        public IActionResult Create()
        {
            var students = svc.GetStudents();
            // populate viewmodel select list property
            var tvm = new TicketCreateViewModel {
                Students = new SelectList(students,"Id","Name") 
            };
            
            // render blank form
            return View( tvm );
        }
       
        // POST /ticket/create
        [HttpPost]
        [Authorize(Roles="admin,manager")]
        public IActionResult Create(TicketCreateViewModel tvm)
        {
            if (ModelState.IsValid)
            {
                svc.CreateTicket(tvm.StudentId, tvm.Issue);
     
                Alert($"Ticket Created", AlertType.info);  
                return RedirectToAction(nameof(Index));
            }
            
            // redisplay the form for editing
            return View(tvm);
        }

    }
}
