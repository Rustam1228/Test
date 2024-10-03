using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context;
        public StudentController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        //ввноз данных в бд 
        
        public async Task <IActionResult> Add(AddStudentViewModels studentViewModels)
        {
            var student = new Student
            {
                Name = studentViewModels.Name,
                Email = studentViewModels.Email,
                Phone = studentViewModels.Phone,
                Subscribed = studentViewModels.Subscribed,

            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return View();
        }
        // показать таблицу
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var student = await context.Students.ToListAsync();
            return View(student);
        }
        [HttpGet]
        
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await context.Students.FindAsync(id);

            return View(student);
        }
        [HttpPost]
        //редактировать
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await context.Students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;

                await context.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await context.Students.FindAsync(viewModel.Id);
            if (student is not null)
            {
                context.Students.Remove(student);
                await context.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
    }
           
       
       
}
