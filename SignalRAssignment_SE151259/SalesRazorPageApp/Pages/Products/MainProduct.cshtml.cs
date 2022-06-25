using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace SignalRAssignment.Pages.Products
{
    public class MainProductModel : PageModel
    {
        public PaginatedList<Product> Product { get;set; }

        private readonly IConfiguration Configuration;

        private IProductRepository productRepository = new ProductRepository();

        private IMemberRepository memberRepository = new MemberRepository();

        [BindProperty]
        public bool IsAdmin { get; set; }

        public MainProductModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult OnGet(int? pageIndex)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("username")))
            {
                return RedirectToPage("/Index", "Session");
            }

            IsAdmin = memberRepository.CheckIsAdmin();

            IList<Product> proListQuery = productRepository.GetProducts();
            var PageSize = Configuration.GetValue("PageSize", 4);
            Product = PaginatedList<Product>.Create(proListQuery, pageIndex ?? 1, PageSize);
            return Page();
        }
    }

    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public bool HasPreviousPage()
        {
            return (PageIndex > 1);
        }

        public bool HasNextPage()
        {
            return (PageIndex < TotalPages);
        }

        public static PaginatedList<T> Create(
            IList<T> source, int pageIndex, int pageSize )
        {
            var count = source.Count();
            var items = source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

    }    
}
