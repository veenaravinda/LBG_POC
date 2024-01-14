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
public class IndexModel : PageModel
{
    private readonly ICustomerRepositoryAsync _customer;
        private readonly IPropertyRepositoryAsync _property;
        private readonly IMortgageRepositoryAsync _mortgage;
        private readonly IUnitOfWork _unitOfWork;
    private readonly IRazorRenderService _renderService;
    private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ICustomerRepositoryAsync customer, IPropertyRepositoryAsync property, IMortgageRepositoryAsync mortgage, IUnitOfWork unitOfWork, IRazorRenderService renderService)
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

        public void OnGet()
        {
        }
    public async Task<PartialViewResult> OnGetViewAllPartial()
    {
        Customers = await _customer.GetAllAsync();
        return new PartialViewResult
        {
            ViewName = "_ViewAll",
            ViewData = new ViewDataDictionary<IEnumerable<Customer>>(ViewData, Customers)
        };
    }
    public async Task<JsonResult> OnGetCreateOrEditAsync(int id = 0)
    {
        if (id == 0)
            return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", new Customer()) });
        else
        {
            var thisCustomer = await _customer.GetByIdAsync(id);
            return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEdit", thisCustomer) });
        }
    }
    public async Task<JsonResult> OnPostCreateOrEditAsync(int id, Customer customer)
    {
        if (ModelState.IsValid)
        {
            if (id == 0)
            {
                await _customer.AddAsync(customer);
                await _unitOfWork.Commit();
            }
            else
            {
                await _customer.UpdateAsync(customer);
                await _unitOfWork.Commit();
            }
            Customers = await _customer.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAll", Customers);
            return new JsonResult(new { isValid = true, html = html });
        }
        else
        {
            var html = await _renderService.ToStringAsync("_CreateOrEdit", customer);
            return new JsonResult(new { isValid = false, html = html });
        }
    }

        public async Task<JsonResult> OnGetCreateOrEditPropertyAsync(int id = 0)
        {
            if (id == 0)
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEditProperty", new Property()) });
            else
            {
                var thisProperties = await _property.GetByIdAsync(id);
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEditProperty", thisProperties) });
            }
        }
        public async Task<JsonResult> OnPostCreateOrEditPropertyAsync(int id, Property property)
        {
            if (ModelState.IsValid)
            {
                if (id == 0)
                {
                    await _property.AddAsync(property);
                    await _unitOfWork.Commit();
                }
                else
                {
                    await _property.UpdateAsync(property);
                    await _unitOfWork.Commit();
                }
                Properties = await _property.GetAllAsync();
                var html = await _renderService.ToStringAsync("_ViewAllProperties", Properties);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_ViewAllProperties", Properties);
                return new JsonResult(new { isValid = false, html = html });
            }
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
        var customer = await _customer.GetByIdAsync(id);
        await _customer.DeleteAsync(customer);
        await _unitOfWork.Commit();
        Customers = await _customer.GetAllAsync();
        var html = await _renderService.ToStringAsync("_ViewAll", Customers);
        return new JsonResult(new { isValid = true, html = html });
    }
}
}
