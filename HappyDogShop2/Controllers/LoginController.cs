using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HappyDogShop2.Models;

namespace HappyDogShop2.Controllers
{
    public class LoginController : Controller
    {
        private MyDbContext db = new MyDbContext();
        // GET
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(HappyDogShop2.Models.User userModel)
        {
            // using (LoginDataBaseEntities db = new LoginDataBaseEntities())
            {
                var loginUser = db.Users.FirstOrDefault(user => user.Username == userModel.Username && user.Password == userModel.Password);
                if (loginUser == null)
                {
                    // userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else {
                    Session["userID"] = loginUser.UserId;
                    Session["userName"] = loginUser.Username;
                    Session["isAdmin"] = loginUser.IsAdmin;
                     // if(loginUser.IsAdmin) Session["admin"] = 1;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
           // Session["admin"] = 0;
            
            return RedirectToAction("Index","Login");
        }



    }
}