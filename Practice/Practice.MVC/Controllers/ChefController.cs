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

        /* --------------------------------------- */
        // Get all Method (Find all)
        /* --------------------------------------- */
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
                ViewBag.ErrorMessage = "Chefs not found.";
                return View("Error");
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

        /* --------------------------------------- */
        // GetById Method - Get chef by id
        /* --------------------------------------- */
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                ChefModelDTO chef = await ChefService.GetByIdAsync(id);



                if (chef == null)
                {
                    ViewBag.ErrorMessage = "Chef not found.";
                    return View("Error");
                }

                ChefDetailsView chefDetailsView = new ChefDetailsView();

                chefDetailsView.Id = chef.Id;
                chefDetailsView.FirstName = chef.FirstName;
                chefDetailsView.LastName = chef.LastName;
                chefDetailsView.PhoneNumber = chef.PhoneNumber;
                chefDetailsView.HomeAddress = chef.HomeAddress;
                chefDetailsView.Certified = chef.Certified;
                chefDetailsView.OIB = chef.OIB;
                chefDetailsView.HireDate = chef.HireDate;


                return View(chefDetailsView);
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while deleting the chef. {ex}";
            }

            return View("Error");
           



        }

        /* --------------------------------------- */
        // Delete Method - Delete by id
        /* --------------------------------------- */

        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                bool chef = await ChefService.DeleteAsync(id);

                if (!chef)
                {
                    ViewBag.ErrorMessage = "The chef could not be deleted. Please check if the specified chef exists.";
                    return View("Error");
                }

                return RedirectToAction("FindAsync");
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while deleting the chef. {ex}";
            }

            return View("Error");
        }





        /* --------------------------------------- */
        // Post Method - Create new
        /* --------------------------------------- */

        public ActionResult PostAsync()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(ChefDetailsView chefDetailsView)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ChefModelDTO chef = new ChefModelDTO();

                    chef.FirstName = chefDetailsView.FirstName;
                    chef.LastName = chefDetailsView.LastName;
                    chef.PhoneNumber = chefDetailsView.PhoneNumber;
                    chef.HomeAddress = chefDetailsView.HomeAddress;
                    chef.Certified = chefDetailsView.Certified;
                    chef.OIB = chefDetailsView.OIB;
                    chef.HireDate = chefDetailsView.HireDate;

                    chef = await ChefService.PostAsync(chef);

                    if (chef != null)
                    {
                        return RedirectToAction("FindAsync");
                    }
                }

                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred while adding a new chef. {ex}";
                }

            }

            return View("Error");

        }


        /* --------------------------------------- */
        // PUT Method Id
        /* --------------------------------------- */


        public async Task<ActionResult> PutAsync(Guid Id)
        {
            try
            {
                ChefModelDTO chef = new ChefModelDTO();


                chef = await ChefService.GetByIdAsync(Id);


                ChefDetailsView chefDetailsView = new ChefDetailsView();

                chefDetailsView.FirstName = chef.FirstName;
                chefDetailsView.LastName = chef.LastName;
                chefDetailsView.PhoneNumber = chef.PhoneNumber;
                chefDetailsView.HomeAddress = chef.HomeAddress;
                chefDetailsView.Certified = chef.Certified;
                chefDetailsView.OIB = chef.OIB;
                chefDetailsView.HireDate = chef.HireDate;


                return View(chefDetailsView);
            }

            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"An error occurred while fetching the chef's information. {ex}";
            }

            return View("Error");

        }


        /* --------------------------------------- */
        // PUT Method Submit
        /* --------------------------------------- */


        [HttpPost]
        public async Task<ActionResult> PutAsync(Guid id, ChefDetailsView chefDetailsView)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    ChefModelDTO chef = new ChefModelDTO();

                    chef.FirstName = chefDetailsView.FirstName;
                    chef.LastName = chefDetailsView.LastName;
                    chef.PhoneNumber = chefDetailsView.PhoneNumber;
                    chef.HomeAddress = chefDetailsView.HomeAddress;
                    chef.Certified = chefDetailsView.Certified;
                    chef.OIB = chefDetailsView.OIB;
                    chef.HireDate = chefDetailsView.HireDate;


                    await ChefService.PutAsync(id, chef);


                    return RedirectToAction("FindAsync");
                }

                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred while updating the chef's information. {ex}";
                }

            }

            return View("Error");


        }




    }

}
