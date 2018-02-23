using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoActivity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace DojoActivity.Controllers
{
    public class HomeController : Controller
    {
        private ActivityContext _context;
        public HomeController(ActivityContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model){
            if(ModelState.IsValid)
            {
                if(_context.users.SingleOrDefault(user=>user.Email==model.Email)==null){
                User NewUser = new User{
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };
                _context.Add(NewUser);
                _context.SaveChanges();
                User CurrentUser  =  _context.users.SingleOrDefault(user => user.Email == NewUser.Email);
                return RedirectToAction("Home",CurrentUser);
                }else{
                    ViewBag.error = "Email Already in use";
                   return View("Index"); 
                } 
            }else{
                return View("Index");
            }
            
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email,string Password){
        User CurrentUser  =  _context.users.SingleOrDefault(user => user.Email == Email);
        if(CurrentUser == null){
            ViewBag.error = "No user with that email!";
            return View("Index");
        }else if(CurrentUser.Password != Password){
            ViewBag.error = "Password does not match!";
            return View("Index");
        }else{
            return RedirectToAction("Home", CurrentUser);
        }
        }
        [HttpGetAttribute]
        [Route("Home")]
        public IActionResult Home(User CurrentUser){
            User user = CurrentUser;
            List<Activity> allActivities = _context.activities.Include(w => w.Guests).ThenInclude(g=> g.user).ToList();
            ViewBag.activities = allActivities;
            return View("Home",user);
        }
        [HttpGetAttribute]
        [Route("New/{userId}")]
        public IActionResult PlanActivity(int userId){
            
            ViewBag.userId = userId;
            return View("PlanActivity");
        }
        [HttpPostAttribute]
        [Route("CreateActivity")]
        public IActionResult CreateActivity(Activity activity){
            User CurrentUser = _context.users.SingleOrDefault(user => user.userId == activity.UserId);
            TryValidateModel(activity);
            _context.activities.Add(activity);
            _context.SaveChanges();
            return RedirectToAction("Home",CurrentUser);
        }
        [HttpGetAttribute]
        [Route("Activity/{ActivityId}")]
        public IActionResult Activity(int ActivityId, int user1){
            Activity ViewActivity = _context.activities.Include(w=> w.Guests).SingleOrDefault(activity=> activity.ActivityId==ActivityId);
            
            ViewBag.user = user1;
            
            return View("Activity",ViewActivity);
        }
        [HttpPost]
        [Route("Reserve")]
        public IActionResult RSVP(int activity1,int user1){
            User CurrentUser = _context.users.SingleOrDefault(user => user.userId == user1);
            Activity CurrentActivity = _context.activities.SingleOrDefault(wedding => wedding.ActivityId == activity1);
            Guest newset = new Guest{
                user = CurrentUser,
                Activity = CurrentActivity
            }; 
            _context.guests.Add(newset);
            _context.SaveChanges();
            return RedirectToAction("Home",CurrentUser);
        }
        [HttpPost]
        [Route("Unreserve")]
        public IActionResult UNRSVP(int activity1,int user1){
            User CurrentUser = _context.users.SingleOrDefault(user => user.userId == user1);
            Guest RetrievedGuest = _context.guests.SingleOrDefault(guest => guest.UserId == user1 && guest.ActivityId == activity1);
            _context.guests.Remove(RetrievedGuest);
            _context.SaveChanges();
            return RedirectToAction("Home",CurrentUser);
        }
        [HttpPost]
        [Route("Delete")]
        public IActionResult Delete(int activity1, int user1){
            User CurrentUser = _context.users.SingleOrDefault(user => user.userId == user1);
            Activity RetrievedWedding = _context.activities.SingleOrDefault(wedding => wedding.ActivityId == activity1);
            List<Guest> allguests = _context.guests.Where(g => g.ActivityId == activity1).ToList();
            foreach(var g in allguests){
                _context.guests.Remove(g);
            }
            _context.activities.Remove(RetrievedWedding);
            _context.SaveChanges();
            return RedirectToAction("Home",CurrentUser);
        }
        [HttpPost]
        [Route("Activity/Reserve")]
        public IActionResult ActivityRSVP(int activity1,int user1){
            User CurrentUser = _context.users.SingleOrDefault(user => user.userId == user1);
            Activity CurrentActivity = _context.activities.SingleOrDefault(wedding => wedding.ActivityId == activity1);
            Guest newset = new Guest{
                user = CurrentUser,
                Activity = CurrentActivity
            }; 
            _context.guests.Add(newset);
            _context.SaveChanges();
            return RedirectToAction("Home",CurrentUser);
        }
        [HttpPost]
        [Route("Activity/Unreserve")]
        public IActionResult ActivityUNRSVP(int activity1,int user1){
            User CurrentUser = _context.users.SingleOrDefault(user => user.userId == user1);
            Guest RetrievedGuest = _context.guests.SingleOrDefault(guest => guest.UserId == user1 && guest.ActivityId == activity1);
            _context.guests.Remove(RetrievedGuest);
            _context.SaveChanges();
            return RedirectToAction("Home",CurrentUser);
        }

        
    }
}
