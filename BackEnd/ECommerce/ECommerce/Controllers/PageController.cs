using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Repositories;
using ECommerce.DTOs;
using ECommerce.Models;
using ECommerce.Services;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/ecommerce/[controller]")]
    public class PageController : ControllerBase
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPageInfoAsync()
        {
            return Ok(await _pageService.GetPageInfoAsync());
           
        }

    }
}