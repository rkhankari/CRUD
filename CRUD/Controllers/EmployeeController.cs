using CRUD.Data;
using CRUD.Models;
using CRUD.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDBContext dBContext;
        public EmployeeController(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee
            {
                Name = viewModel.Name,
                Mobile = viewModel.Mobile,
                EmailId = viewModel.EmailId,
                Address = viewModel.Address,
                DateOfBirth = viewModel.DateOfBirth,
                City = viewModel.City,
            };
            await dBContext.Employees.AddAsync(employee);
            await dBContext.SaveChangesAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var employees= await dBContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
           var employee= await dBContext.Employees.FindAsync(Id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee viewModel)
        {
            var employee = await dBContext.Employees.FindAsync(viewModel.Id);
            if(employee is not null)
            {
                employee.Name= viewModel.Name;
                employee.Mobile= viewModel.Mobile;
                employee.EmailId= viewModel.EmailId;
                
                await dBContext.SaveChangesAsync();

            }
            return RedirectToAction("List", "Employee");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Employee viewModel)
        {
            var employee = await dBContext.Employees.AsNoTracking().FirstOrDefaultAsync(x=> x.Id== viewModel.Id);
            if(employee is not null)
            {
                dBContext.Employees.Remove(viewModel);
                await dBContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employee");
        }
    }
}
