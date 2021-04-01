using Assignment10.Models;
using Assignment10.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }
        //route has parameter to be able to filter by teams!!
        
        public IActionResult Index(long? teamID, string teamName, int pagenum=0)
        {
            int pagesize = 5;

            return View(new IndexViewModel
            {
                //actual dataset returned
                Bowlers = (context.Bowlers
                .Where(b => b.TeamId == teamID || teamID == null)
                .OrderBy(b => b.BowlerLastName)
                //display the right data for pagination
                .Skip((pagenum - 1) * pagesize)
                .Take(pagesize)
                .ToList()),
                //paging info for the pagination
                PageNumberInfo = new PageNumberInfo
                {
                    //Gather this new informtaion 
                    NumItemsPerPage = pagesize,
                    CurrentPage = pagenum,
                    //if there isn't a team id, just do the total count
                    //otherwise only count the number from the team that was selected
                    TotalNumItems = (teamID == null ? context.Bowlers.Count() :
                    context.Bowlers.Where (x => x.TeamId == teamID).Count ())
                },
                TeamCat = teamName
            });
                
                




                //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamId = {teamID} OR {teamID} IS NULL ORDER BY BowlerFirstName")
                //.ToList()); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
