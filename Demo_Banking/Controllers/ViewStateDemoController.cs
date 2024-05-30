using Demo_Banking.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Demo_Banking.Controllers
{
    public class ViewStateDemoController : Controller
    {
        public IActionResult ViewModelDemo()
        {
            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel { Id = 10, Name = "First Employee" },
                new EmployeeViewModel { Id = 20, Name = "Second Employee" },
                new EmployeeViewModel { Id = 30, Name = "Third Employee" },
                new EmployeeViewModel { Id = 40, Name = "Fourth Employee" },
                new EmployeeViewModel { Id = 50, Name = "Fifth Employee" },
            };

            // return View(viewName: "ViewModelDynamicDemo", model: employees);
            return View(viewName: "ViewModelTypeSafeDemo", model: employees);
        }

        public IActionResult ViewBagDemo()
        {
            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel { Id = 10, Name = "First Employee" },
                new EmployeeViewModel { Id = 20, Name = "Second Employee" },
                new EmployeeViewModel { Id = 30, Name = "Third Employee" },
                new EmployeeViewModel { Id = 40, Name = "Fourth Employee" },
                new EmployeeViewModel { Id = 50, Name = "Fifth Employee" },
            };

            ViewBag.Employees = employees;

            // return View();
            return RedirectToAction("ViewBagDemo2");
        }

        public IActionResult ViewBagDemo2()
        {
            return View(viewName: "ViewBagDemo");
        }


        public IActionResult ViewDataDemo()
        {
            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel { Id = 10, Name = "First Employee" },
                new EmployeeViewModel { Id = 20, Name = "Second Employee" },
                new EmployeeViewModel { Id = 30, Name = "Third Employee" },
                new EmployeeViewModel { Id = 40, Name = "Fourth Employee" },
                new EmployeeViewModel { Id = 50, Name = "Fifth Employee" },
            };

            ViewData["Employees"] = employees;

            // return View();
            return RedirectToAction("ViewDataDemo2");
        }

        public IActionResult ViewDataDemo2()
        {
            return View(viewName: "ViewDataDemo");
        }

        public IActionResult TempDataDemo()
        {
            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel { Id = 10, Name = "First Employee" },
                new EmployeeViewModel { Id = 20, Name = "Second Employee" },
                new EmployeeViewModel { Id = 30, Name = "Third Employee" },
                new EmployeeViewModel { Id = 40, Name = "Fourth Employee" },
                new EmployeeViewModel { Id = 50, Name = "Fifth Employee" },
            };

            TempData["Employees"] = employees;

            return View();

            // NOTE: TempData can store only simple types,
            //       results in HTTP 500 Serialization failure error running the below code!
            // return RedirectToAction("TempDataDemo2");
        }

        public IActionResult TempDataDemo2()
        {
            return View(viewName: "TempDataDemo");
        }


        public IActionResult TempDataSerializedDemo()
        {
            var employees = new List<EmployeeViewModel>
            {
                new EmployeeViewModel { Id = 10, Name = "First Employee" },
                new EmployeeViewModel { Id = 20, Name = "Second Employee" },
                new EmployeeViewModel { Id = 30, Name = "Third Employee" },
                new EmployeeViewModel { Id = 40, Name = "Fourth Employee" },
                new EmployeeViewModel { Id = 50, Name = "Fifth Employee" },
            };

            // TempData can store only simple types - strings, boolean and numeric types ONLY!
            // Hence, serialize the data to access the same across action methods.
            TempData["Employees"] = System.Text.Json.JsonSerializer.Serialize(employees);

            // return View();
            return RedirectToAction("TempDataSerializedDemo2");
        }

        public IActionResult TempDataSerializedDemo2()
        {
            return View(viewName: "TempDataSerializedDemo");
        }
    }
}
