using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CRUDelicious.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private DishContext dbContext;
        public HomeController(DishContext context)
        {
            dbContext = context;
        }
       
        [HttpGet("")] // INDEX PAGE: DISPLAYS ALL DISHES
           public IActionResult Index()
        {
            List<Dish> Dishes = dbContext.dish.OrderByDescending(d=>d.created_at).ToList();
            
            
            return View(Dishes);
        }
        [HttpPost("addDish")] //ADD DISH FROM NewDish FORM
        public IActionResult CreateDish(Dish newDish)
        {   
            if(ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewDish");
        }
        [HttpGet("new")] //CREATE NEW DISH FORM
        public IActionResult NewDish()
        {
            return View();
        }

        [HttpGet("{dishID}")]   //VIEW ONE DISH
        public IActionResult ViewDish(int dishID)
        {
            Dish Dish = dbContext.dish.FirstOrDefault(d=>d.ID == dishID);
            ;
            return View(Dish);
        }

        [HttpGet("/update/{dishID}")] //EDIT ONE DISH
        public IActionResult UpdateDish(int dishID)
        {
            Dish Dish = dbContext.dish.FirstOrDefault(d=>d.ID ==dishID);
            
            return View(Dish);
        }
        [HttpPost("/edit/{dishID}")] //SUBMIT EDIT DISH FORM
        public IActionResult EditDish(Dish upDish, int dishID)
        {
            if(ModelState.IsValid)
            {
                Dish thisDish = dbContext.dish.FirstOrDefault(d=>d.ID ==dishID);
                thisDish.ChefName = upDish.ChefName;
                thisDish.Calories = upDish.Calories;
                thisDish.DishName = upDish.DishName;
                thisDish.Description = upDish.Description;
                thisDish.Tastiness = upDish.Tastiness;
                thisDish.updated_at = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("ViewDish", thisDish.ID);
            }
            return View("UpdateDish", dishID);
        }
        [HttpGet("/delete/{dishID}")]
        public IActionResult Destroy(int dishID)
        {
            Dish thisDish = dbContext.dish.FirstOrDefault(d=> d.ID == dishID);
            dbContext.dish.Remove(thisDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
