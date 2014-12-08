using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using WebUI.Models.ForActivation;

namespace WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
            {
                if (returnUrl != null)
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Имя пользователя или пароль указаны неверно.");
            return View(model);
        }

        //
        // POST: /Account/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Попытка зарегистрировать пользователя
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                        propertyValues:
                            new
                            {
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Email = model.Email,
                                Phone = model.Phone,
                                Address = model.Address
                            });//что блин ему о5 не нравится
                    Roles.AddUserToRole(model.UserName,
                        model.UserName == "admin" ? SiteRole.Admin.ToString() : SiteRole.Client.ToString());

                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        #region

        //public class AccountController : Controller
        //{
        //    #region Тут всё работает

        //    public ActionResult Login()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    public ActionResult Login(AccountModel.LogOnModel model, string returnUrl)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            if (Membership.ValidateUser(model.UserName, model.Password))
        //            {
        //                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
        //                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
        //                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
        //                {
        //                    return RedirectToAction(returnUrl);
        //                }
        //                else
        //                {
        //                    return RedirectToAction("Index", "Home");
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Имя пользователя либо пароль не верны.");
        //            }

        //        }
        //        return View(model);
        //    }

        //    public ActionResult LogOff()
        //    {
        //        FormsAuthentication.SignOut();

        //        return RedirectToAction("Index", "Home");
        //    }

        //    public ActionResult Register()
        //    {
        //        return View();
        //    }

        //    [HttpPost]
        //    [AllowAnonymous]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Register(AccountModel.RegisterModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //var dbContext = new AccountModel.UsersContext();
        //            try
        //            {
        //                WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new{FirstName = model.FirstName, LastName = model.LastName, Address = model.Address, Phone = model.Phone});
        //                //var userProfile = WebSecurity.CreateUserAndAccount(model.UserName, model.Password, null, false);
        //                //var user = dbContext.UserProfiles.SingleOrDefault(u => u.UserName == model.UserName);
        //                //if (user != null)
        //                //{
        //                //    var userDetail = new AccountModel.UserDetail
        //                //    {
        //                //        FirstName = model.FirstName,
        //                //        LastName = model.LastName,
        //                //        Phone = model.Phone,
        //                //        Address = model.Address,
        //                //        UserProfile = user
        //                //    };
        //                //    dbContext.UserDetails.Add(userDetail);
        //                //    dbContext.SaveChanges();
        //                //}
        //                //if (model.UserName == "administrator")
        //                //{
        //                //    Roles.AddUserToRole("administrator", "admin");
        //                //}
        //                //else
        //                //{
        //                //    Roles.AddUserToRole(model.UserName, "client");
        //                //}
        //                WebSecurity.Login(model.UserName, model.Password);
        //                return RedirectToAction("Index", "Home");
        //            }
        //            catch (MembershipCreateUserException e)
        //            {
        //                ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        //            }
        //        }
        //        return View(model);
        //    }

        #endregion

        #region Сообщения ошибок

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Введенный вами логин занят. Пожалуйста, измените логин.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Введенный вами адрес электронной почты занят. Пожалуйста,измените адрес.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Введенный пароль не верен. Пожалуйста, измените пароль";

                case MembershipCreateStatus.InvalidEmail:
                    return "Введеннный вами адрес электронной почты не верен. Пожалуйста, измените адрес.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Введеннный вами логин не верен. Пожалуйста, измените логин.";

                case MembershipCreateStatus.ProviderError:
                    return
                        "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return
                        "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return
                        "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
    }
}
