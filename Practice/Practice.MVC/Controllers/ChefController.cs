using Practice.Common;
using Practice.Dal;
using Practice.Model;
using Practice.MVC.Models;
using Practice.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Practice.MVC.Controllers
{
    public class ChefController : Controller
    {

        protected IChefService ChefService;

        public ChefController(IChefService chefService)
        {
            ChefService = chefService;
        }

        // GET: All Chefs
        public async Task<ActionResult> FindAsync(int page = 1, int itemsPerPage = 10, string sortBy = "Id", string sortOrder = "asc", string firstName = "", string lastName = "", DateTime? hireDate = null)
        {

            Paging paging = new Paging
            {
                Page = page,
                ItemsPerPage = itemsPerPage
            };

            Sorting sorting = new Sorting
            {
                SortBy = sortBy,
                SortOrder = sortOrder
            };


            ChefFilter filteringChef = new ChefFilter()
            {
                FirstName = firstName,
                LastName = lastName,
                HireDate = hireDate
            };


            List<ChefModelDTO> chefs = await ChefService.FindAsync(paging, sorting, filteringChef);

            List<ChefView> mappedChefs = new List<ChefView>();



            if (chefs == null)
            {
                return View();
            }



            foreach (ChefModelDTO chef in chefs)
            {
                ChefView chefView = new ChefView();

                chefView.FirstName = chef.FirstName;
                chefView.LastName = chef.LastName;
                chefView.HireDate = chef.HireDate;
                chefView.Id = chef.Id;

                mappedChefs.Add(chefView);

            }


            return View(mappedChefs);


        }

        // GetById: Get chef by id
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {


            ChefModelDTO chef = await ChefService.GetByIdAsync(id);



            if (chef == null)
            {
                //add notfound
                return View();
            }

            ChefView chefView = new ChefView();

            chefView.FirstName = chef.FirstName;
            chefView.LastName = chef.LastName;
            chefView.HireDate = chef.HireDate;


            return View(chefView);



        }

        public async Task<ActionResult> DeleteAsync(Guid id)
        {



            bool chef = await ChefService.DeleteAsync(id);


            if (!chef)
            {
                //add notfound
                return View();
            }

            return RedirectToAction("FindAsync");
        }


       


        public async Task<ActionResult> PutAsync(Guid Id)
        {


            ChefModelDTO chef = new ChefModelDTO();
                  
            chef = await ChefService.GetByIdAsync(Id);

            ChefView chefView = new ChefView();

            chefView.FirstName = chef.FirstName;
            chefView.LastName = chef.LastName;
            chefView.HireDate = chef.HireDate;

            return View(chefView);
        }


        [HttpPost]
        public async Task<ActionResult> PutAsync(ChefView chefView)
        {

            ChefModelDTO chef = new ChefModelDTO();

            chef.FirstName = chefView.FirstName;
            chef.LastName = chefView.LastName;
            chef.HireDate = chefView.HireDate;


            bool chefCheck = await ChefService.PutAsync(chef);

            return RedirectToAction("Index");
        }




    }

}
