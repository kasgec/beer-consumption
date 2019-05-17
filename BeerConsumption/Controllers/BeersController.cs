using BeerConsumption.Models;
using BeerConsumption.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BeerConsumption.Controllers
{
    public class BeersController : Controller
    {
        private static readonly IApiService<Beer, BeerCreation> _apiService;
        static BeersController()
        {
            _apiService = new ApiService<Beer, BeerCreation>();
        }    

        // GET: Beers
        public async Task<ActionResult> Index()
        {
            try
            {
                var beers = await _apiService.GetAll("beers");
                return View(beers);
            }
            catch (Exception ex)
            {
                return View(new List<Beer>());
            }
        }

        // GET: Beers/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var beer = new BeerCreation();
            return View(beer);
        }

        // POST: Beers/Create
        [HttpPost]
        public async Task<ActionResult> Create(BeerCreation beer)
        {
            try
            {
                var created = await _apiService.Post(beer, "beers");
                
                if (created)
                {
                    return RedirectToAction("Index");
                }

                return View(beer);
            }
            catch (Exception ex)
            {
                return View(beer);
            }
        }

        // GET: Beers/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            try
            {
                var beer = await _apiService.Get($"beers/{id}");
                return View(beer);
            }
            catch (Exception ex)
            {
                return View("Index");
            }
        }

        // POST: Beers/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(string id, BeerCreation beer)
        {
            try
            {
                var updated = await _apiService.Put(beer, $"beers/{id}");

                if (updated)
                {
                    return RedirectToAction("Index");
                }
                
                return View(beer);
            }
            catch
            {
                return View(beer);
            }
        }

        // GET: Beers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await _apiService.Delete($"beers/{id}");
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
