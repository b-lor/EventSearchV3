using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EventSearch.Data;
using EventSearch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;


namespace EventSearch.Controllers
{
    public class ImageController : Controller
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-EventSearch-6A073489-7CCD-43B0-9D27-03EA38933543;Trusted_Connection=True;MultipleActiveResultSets=true";
        private ApplicationDbContext _context;

        public IConfiguration _configuration;

        [BindProperty]
        public Image Image { get; set; }

        public ImageController(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            var model = FetchImageFromDB();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormCollection form)
        {
            string storePath = "wwwroot/Image/";
            if (form.Files == null || form.Files[0].Length == 0)
                return RedirectToAction("Index");


            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), storePath,
                        form.Files[0].FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await form.Files[0].CopyToAsync(stream);
            }

            StoreInDB(storePath + form.Files[0].FileName);

            return RedirectToAction("List");

        }


        public void StoreInDB(string path)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                using (var com = new SqlCommand("insert into Image(ImagePath) values('" + path + "')", con))
                {
                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }
        public List<string> FetchImageFromDB()
        {
            List<string> imagePath = new List<string>();

            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                using (var com = new SqlCommand("select * from Image", con))
                {
                    using (var reader = com.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                imagePath.Add(reader["ImagePath"].ToString());

                            }
                        }
                    }
                }
            }

            return imagePath;
        }

    }
}