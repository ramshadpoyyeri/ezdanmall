using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace EzdanMall.Controllers
{
    public class AdminController : Controller
    {
        #region Declarations
        public static String SqlConnectionString = "";
        public String SecretKey = ""; // Key Used for encrypted/decryption (passwords/cookies/etc..)
        public string WebRootPath = ""; //Website absolute path
        #endregion Declarations

        #region Constructor
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public AdminController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment; _configuration = configuration;
            SqlConnectionString = configuration.GetConnectionString("SqlConnectionString");
            SecretKey = configuration.GetValue<String>("SecretKey");
            WebRootPath = _webHostEnvironment.WebRootPath;
        }
        #endregion Constructor   


        #region Views/Pages
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                if (db.IsUserLoggedIn(HttpContext))
                {
                    return Redirect("/admin");
                }
                return View();
            }
        }
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                String Username = form["Username"];
                String Password = form["Password"];
                String kpString = form["keeplogin"];
                bool keeplogin = (kpString == null) ? false : true;
                String result = db.CheckLogin(Username, Password, HttpContext, keeplogin);
                if (result.Equals("Active"))
                {
                    //TempData["Message"] = "Login Success";
                    return Redirect("/admin");
                }
                else if (result.Equals("InActive"))
                {
                    TempData["Message"] = "User login is InActive.!";
                }
                else
                {
                    TempData["Message"] = "Invalid User credentials.!";
                }
                return View();
            }
        }
        [Route("admin/forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [Route("admin/forgot-password")]
        public IActionResult ForgotPassword(IFormCollection form)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                String toemail = form["email"];
                string path = Path.Combine(WebRootPath, "email_forgetpassword.html");
                String emailBody = "";

                using (StreamReader reader = new StreamReader(path))
                {
                    emailBody = reader.ReadToEnd();
                }
                emailBody = emailBody.Replace("{#toemail#}", toemail);
                string SiteImageUrl = db.GetHomeBaseURL(HttpContext); //"https://localhost:7238/admin/";

                String qString = db.EncryptText("toemail=" + toemail + "&dt=" + db.GetUniqueID());

                emailBody = emailBody.Replace("{#SITEURL#}", SiteImageUrl);
                emailBody = emailBody.Replace("{#UrlLink#}", SiteImageUrl + "/admin/new-password?q=" + qString);
                db.SendEmail(toemail, "noreply.ezdan@gmail.com", emailBody);


            }
            TempData["Message"] = "Email has been sent.Please check your email";

            return View();
        }

        public JsonResult isEmailExist(string email)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@email", email));
                DataSet dsResult = db.SqlDataSetResult("USP_Admin_EmailExistOrNot", sqlParams);
                Int32 count = 0;
                if (dsResult.Tables[0].Rows.Count > 0)
                {
                    count = Convert.ToInt32(dsResult.Tables[0].Rows[0]["countexist"].ToString());
                }


                if (count == 0)
                {
                    return Json(false);
                }
                else
                {
                    return Json(true);
                }
            }
        }

        [Route("admin/new-password")]
        public IActionResult NewPassword(String q)
        {
            return View();
        }
        [HttpPost]
        [Route("admin/new-password")]
        public IActionResult NewPassword(IFormCollection form)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                String qString = db.DecryptText(Request.Query["q"]);
                String? email = HttpUtility.ParseQueryString(qString).Get("toemail");

                String password = form["Password"];

                List<SqlParameter> sqlParams = new List<SqlParameter>();
                sqlParams.Add(new SqlParameter("@email", email));
                sqlParams.Add(new SqlParameter("@password", password));
                sqlParams.Add(new System.Data.SqlClient.SqlParameter("@SecretKey", SecretKey));
                DataSet dsResult = db.SqlDataSetResult("USP_Password_Update", sqlParams);
                if (dsResult.Tables.Count > 0)
                {
                    TempData["Message"] = "Password reset successfully";
                    return Redirect("Login");

                }
                else
                {
                    TempData["EMessage"] = "Please try again";
                    return Redirect("NewPassword");

                }



            }
        }

        [Route("admin/EntryForm")]
        public IActionResult EntryForm(string doctype, String docno = "")
        {
            return View();
        }
        [HttpPost]
        [Route("admin/EntryForm")]
        public IActionResult EntryForm(IFormCollection form, EzdanLibrary.EntryFormClass model)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                String? USR_DocNo = HttpContext.Session.GetString("UserId");
                DataSet ds = db.SaveRecord(form: form, webRootPath: WebRootPath, ModelClass: model, USR_DocNo: USR_DocNo, imgMaxWidth: 900, imgMaxHeight: 900);

                TempData["Message"] = ds.Tables[0].Rows[0]["Message"]; //set message from dataset..
                String viewUrl = "/admin/" + model.PostFromFormName + "?doctype=" + model.doctype + (String.IsNullOrEmpty(Request.Query["docno"]) ? "" : "&docno=" + Request.Query["docno"]);
                return Redirect(viewUrl);
            }
        }

        public IActionResult MasterList(String doctype)
        {
            return View();
        }
        public JsonResult deleterecord(String doctype, string docno)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                String? userid = HttpContext.Session.GetString("UserId");
                DataSet ds = db.DeleteRecord(doctype, docno, userid);
                //TempData["Message"] = ds.Tables[0].Rows[0]["Message"];
                String Message = ds.Tables[0].Rows[0]["Message"] + "";

                return Json(Message);
            }
        }

        public JsonResult deletefile(String doctype, String docno, String columnName, String imageURL)
        {
            using (EzdanLibrary.DAL db = new EzdanLibrary.DAL(_configuration))
            {
                String? userid = HttpContext.Session.GetString("UserId");
                //DataSet ds = db.DeleteRecord(doctype, docno, userid);
                //TempData["Message"] = ds.Tables[0].Rows[0]["Message"];
                //String Message = ds.Tables[0].Rows[0]["Message"] + "";

                System.IO.File.Delete(imageURL);
                List<SqlParameter> sqlPrm = new List<SqlParameter>();
                sqlPrm.Add(new SqlParameter("@DocType", doctype));
                sqlPrm.Add(new SqlParameter("@USR_DocNo", userid));
                DataSet ds = db.SqlDataSetResult("USP_STG_GetSaveSettings", sqlPrm);
                String tableName = ds.Tables[0].Rows[0]["TableName"].ToString();
                String formName = ds.Tables[0].Rows[0]["FormName"].ToString();
                String TablePrefix = ds.Tables[0].Rows[0]["TablePrefix"].ToString();

                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                String sqlQuery = "";
                sqlParameters.Add(new SqlParameter(TablePrefix + "_DocNo", docno));
                sqlQuery = "UPDATE " + tableName + " SET ";
                sqlQuery += TablePrefix + "_" + columnName + "=''";
                sqlQuery += " WHERE " + TablePrefix + "_DocNo=@" + TablePrefix + "_DocNo";
                sqlQuery += " SELECT '" + formName + " Deleted' as Message";

                DataSet dataSet = db.SqlDataSetResult(sqlQuery, sqlParameters, false);
                return Json("true");
            }
        }
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return Redirect("/admin/login");
        }


        #endregion Views/Pages
    }
}
