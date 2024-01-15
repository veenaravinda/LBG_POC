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
    public class PropertyIndex : PageModel
    {
        private readonly IPropertyRepositoryAsync _property;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRazorRenderService _renderService;
        private readonly ILogger<IndexModel> _logger;

        public PropertyIndex(ILogger<IndexModel> logger, IPropertyRepositoryAsync property, IUnitOfWork unitOfWork, IRazorRenderService renderService)
        {
            _logger = logger;
            _property = property;
            _unitOfWork = unitOfWork;
            _renderService = renderService;
        }
        public IEnumerable<Property> Properties { get; set; }

        public async Task OnGetAsync()
        {
            Properties = await _property.GetAllAsync();
        }

        public async Task<PartialViewResult> OnGetViewAllPartial()
        {
            Properties = await _property.GetAllAsync();

            return new PartialViewResult
            {
                ViewName = "_ViewAllProperties",
                ViewData = new ViewDataDictionary<IEnumerable<Property>>(ViewData, Properties)
            };
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

        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            var property = await _property.GetByIdAsync(id);
            await _property.DeleteAsync(property);
            await _unitOfWork.Commit();
            Properties = await _property.GetAllAsync();
            var html = await _renderService.ToStringAsync("_ViewAllProperties", Properties);
            return new JsonResult(new { isValid = true, html = html });
        }
    }
}

