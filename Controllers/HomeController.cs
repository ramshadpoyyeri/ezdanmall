using EzdanMall.Models;
using Microsoft.AspNetCore.Mvc;

using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace EzdanMall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [Route("")]
        [Route("/home")]
        
        public IActionResult Index()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_HomePageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("shop")]
        public IActionResult Shop()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_ShopPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("shop-info/{UniqueKey}-{id}")]
        public IActionResult ShopInfo(String id)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_ShopDetailsPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("dine")]
        public IActionResult Dine()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_DinePageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("dine-info/{UniqueKey}-{id}")]
        public IActionResult DineInfo(String UniqueKey, String id)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_DineDetailsPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("enjoy")]
        public IActionResult Enjoy()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_EnjoyPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("enjoy-info/{UniqueKey}-{id}")]
        public IActionResult EnjoyInfo(String UniqueKey, String id)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_EnjoyDetailsPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("promotions")]
        public IActionResult Promotions()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_PromotionPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("news")]
        public IActionResult News()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_MediaPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("News-info/{UniqueKey}-{id}")]
        public IActionResult NewsInfo(String UniqueKey, String id)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@id", id));
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_NewsDetailsPageInfo", sqlParams);

                return View(ds);
            }
        }
        [Route("timings")]
        public IActionResult Timings()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_MallTimingPageInfo", sqlParams);

                return View(ds);
            }
        }

        [Route("about")]
        public IActionResult About()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_AboutPageInfo", sqlParams);

                return View(ds);
            }
        }

        [Route("contact")]
        public IActionResult Contact()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_ContactPageInfo", sqlParams);

                return View(ds);
            }
        }
        public IActionResult SendEnquiryEmail(string Name, string Email, string Message)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@Name", Name));
                sqlParams.Add(new SqlParameter("@Email", Email));
                //sqlParams.Add(new SqlParameter("@Phone", Phone));
                sqlParams.Add(new SqlParameter("@Message", Message));
                DataSet dsResult = db.SqlDataSetResult("sp_EnquiryDetil_Insert", sqlParams);



                //SqlCommand cmd = new SqlCommand("sp_EnquiryDetil_Insert @Name,@Email,@Message");
                //cmd.Parameters.AddWithValue("@Name", Name);
                //cmd.Parameters.AddWithValue("@Email", Email);               
                //cmd.Parameters.AddWithValue("@Message", Message);
                //string path = Path.Combine(WebRootPath, "email_forgetpassword.html");
                String emailBody = "";
                //emailBody += "Your received Enquiry Mail from ociuz.com";
                emailBody += Environment.NewLine + "Name : " + Name;

                emailBody += Environment.NewLine + "Email : " + Email;               

                emailBody += Environment.NewLine + "Message : " + Message;

                String Subject = "Enquiry";

                //var m = WebRootPath + @"/Email_Pages/email_forgetpassword.html";
                //var j = @m;
                //var fileName = "Email_Pages"+"\\"+"email_forgetpassword.html";
                //emailBody = emailBody.Replace("{#toemail#}", toemail);
                //string SiteImageUrl = db.GetHomeBaseURL(HttpContext); //"https://localhost:7238/admin/";

                //String qString = db.EncryptText("toemail=" + toemail + "&dt=" + db.GetUniqueID());                    
                db.SendEmail("retailleasing@ezdanholding.qa", " noreply.ezdan@gmail.com", emailBody);
                TempData["Message"] = "Enquiry has been Succesfully sent";
            }
            return RedirectToAction("Contact");
        }
   
        [Route("locations")]
        public IActionResult Locations()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                System.Data.DataSet ds = db.SqlDataSetResult("USP_Web_LocationPageInfo", sqlParams);

                return View(ds);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}