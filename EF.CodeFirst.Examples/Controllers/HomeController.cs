using EF.CodeFirst.Examples.Data.Entities;
using System.Web.Mvc;
using EF.CodeFirst.Examples.Data.Repositories;

namespace EF.CodeFirst.Examples.Controllers
{
    public class HomeController : Controller
    {
        private readonly GenericRepository<Student> studentRepository = new GenericRepository<Student>();

        public ActionResult Index()
        {
            //using (var ctx = new OnlineShopDb())
            //{
            //    var stud = new Student() { StudentName = "Bill" };

            //    ctx.Students.Add(stud);
            //    ctx.SaveChanges();
            //}

            // studentRepository.Insert(new Student() { StudentName = "Peter" });
            var s = studentRepository.GetByID(1);
            studentRepository.Delete(s);
            s.StudentName = "Mary";
            studentRepository.Update(s);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}