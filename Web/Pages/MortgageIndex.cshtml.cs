using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.Services;

namespace Web.Pages
{
public class MortgageIndex : PageModel
{
    private readonly ICustomerRepositoryAsync _customer;
        private readonly IPropertyRepositoryAsync _property;
        private readonly IMortgageRepositoryAsync _mortgage;
        private readonly IUnitOfWork _unitOfWork;
    private readonly IRazorRenderService _renderService;
    private readonly ILogger<IndexModel> _logger;

        public MortgageIndex(ILogger<IndexModel> logger, ICustomerRepositoryAsync customer, IPropertyRepositoryAsync property, IMortgageRepositoryAsync mortgage, IUnitOfWork unitOfWork, IRazorRenderService renderService)
        {
            _logger = logger;
            _customer = customer;
            _property = property;
            _mortgage = mortgage;
            _unitOfWork = unitOfWork;
            _renderService = renderService;
        }
    public IEnumerable<Customer> Customers { get; set; }
    public IEnumerable<Property> Properties { get; set; }
    public IEnumerable<Mortgage> Mortgages { get; set; }

       
        public async Task OnGetAsync()
        {
            Customers = await _customer.GetAllAsync();
            Properties = await _property.GetAllAsync();
            Mortgages = await _mortgage.GetAllAsync();

        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            Mortgages = await _mortgage.GetAllAsync();

            return new PartialViewResult
            {
                ViewName = "_ViewAllMortgages",
                ViewData = new ViewDataDictionary<IEnumerable<Mortgage>>(ViewData, Mortgages)
            };
        }

        public async Task<JsonResult> OnGetCreateOrEditMortgageAsync(int id = 0)
        {
            if (id == 0)
           // {
               // Customers = await _customer.GetAllAsync();
               // ViewBag.ChildEntities = new SelectList(_context.ChildEntities, "Id", "ChildName");
               // ViewData = new ViewDataDictionary<IEnumerable<Customer>>(ViewData, Customers)
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEditMortgage", new Mortgage()) });
                //return new JsonResult(new
                //{
                //    isValid = true,
                //    html = await _renderService.ToStringAsync("_CreateOrEditMortgage",
                //     new Mortgage
                //     {
                //         CustomerEntity= Customers.ToList<_customer>()

                //     }          //  Categories = _context.Categories.ToList()

                                          
                //});

       // }
            else
            {
                var thisMortgage = await _mortgage.GetByIdAsync(id);
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEditMortgage", thisMortgage) });
            }
        }
/*
        public IActionResult Create()
        {
            var viewModel = new ProductViewModel
            {
                Categories = _context.Categories.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the product to the database
                // ...

                return RedirectToAction("Index");
            }

            model.Categories = _context.Categories.ToList();
            return View(model);
        }

        */


        public async Task<JsonResult> OnPostCreateOrEditMortgageAsync(int id, Mortgage mortgage)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _mortgage.AddAsync(mortgage);
                    await _unitOfWork.Commit();
                }
                else
                {
                    //mortgage.CustomerEntitys = 
                    await _mortgage.UpdateAsync(mortgage);
                    await _unitOfWork.Commit();
                }
                // model.Categories = _context.Categories.ToList();
               // _mortgage.AddAsync(_customer);
                Mortgages = await _mortgage.GetAllAsync();
              
                var html = await _renderService.ToStringAsync("_ViewAllMortgages", Mortgages);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_CreateOrEditMortgage", Mortgages);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var mortgage = await _mortgage.GetByIdAsync(id);
            await _mortgage.DeleteAsync(mortgage);
            await _unitOfWork.Commit();
            Mortgages = await _mortgage.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", Mortgages);
            return new JsonResult(new { isValid = true, html = html });
        }
}
}
