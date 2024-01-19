using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
//using Infrastructure.Migrations;
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
        public CustomerInfo _customerInfo;
        public PropertyInfo _propertyInfo;

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

        public List<CustomerInfo> CustomersInfo { get; set; }
        public List<PropertyInfo> PropertiesInfo { get; set; }



        public void customer()
        {

            PropertiesInfo = new List<PropertyInfo>();

            CustomersInfo = new List<CustomerInfo>();

            foreach (Customer info in Customers)
            {
                _customerInfo = new CustomerInfo();
                _customerInfo.CustomerId = info.Id;
                _customerInfo.CustomerName = info.Name;
                CustomersInfo.Add(_customerInfo);
            }
            foreach (Property info in Properties)
            {
                _propertyInfo = new PropertyInfo();
                _propertyInfo.PropertyId = info.Id;
                _propertyInfo.AddressName = info.Address;
                PropertiesInfo.Add(_propertyInfo);
            }
                        
        }
        public async Task OnGetAsync()
        {
            Customers = await _customer.GetAllAsync();
            Properties = await _property.GetAllAsync();
            customer();
            Mortgages = await _mortgage.GetAllAsync();

            foreach ( Mortgage mortgage in Mortgages)
            {
                mortgage.PropertyInfo = PropertiesInfo;
                mortgage.CustomerInfo = CustomersInfo;

            }            
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
            Customers = await _customer.GetAllAsync();
            Properties = await _property.GetAllAsync();

            customer();
            Mortgage mortgage = new Mortgage
            {
                PropertyInfo = PropertiesInfo,
                CustomerInfo = CustomersInfo
            };
            if (id == 0)
                 return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEditMortgage",   mortgage) });
            else
            {
                var thisMortgage = await _mortgage.GetByIdAsync(id);
                thisMortgage.CustomerInfo = CustomersInfo;
                thisMortgage.PropertyInfo = PropertiesInfo;
                return new JsonResult(new { isValid = true, html = await _renderService.ToStringAsync("_CreateOrEditMortgage", thisMortgage) });
            }
        }
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
                    await _mortgage.UpdateAsync(mortgage);
                    await _unitOfWork.Commit();
                }
                Mortgages = await _mortgage.GetAllAsync();

                var html = await _renderService.ToStringAsync("_ViewAllMortgages", Mortgages);
                return new JsonResult(new { isValid = true, html = html });
            }
            else
            {
                var html = await _renderService.ToStringAsync("_ViewAllMortgages", Mortgages);
                return new JsonResult(new { isValid = false, html = html });
            }
        }

        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var mortgage = await _mortgage.GetByIdAsync(id);
            await _mortgage.DeleteAsync(mortgage);
            await _unitOfWork.Commit();
            Mortgages = await _mortgage.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAllMortgages", Mortgages);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}
