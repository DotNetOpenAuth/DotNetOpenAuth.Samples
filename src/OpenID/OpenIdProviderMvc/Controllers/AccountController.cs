namespace OpenIdProviderMvc.Controllers {
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.Linq;
	using System.Security.Principal;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Security;
	using System.Web.UI;
	using OpenIdProviderMvc.Code;

	[HandleError]
	public class AccountController : Controller {
		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <remarks>
		/// This constructor is used by the MVC framework to instantiate the controller using
		/// the default forms authentication and membership providers.
		/// </remarks>
		public AccountController()
			: this(null, null) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AccountController"/> class.
		/// </summary>
		/// <param name="formsAuth">The forms authentication service.</param>
		/// <param name="service">The membership service.</param>
		/// <remarks>
		/// This constructor is not used by the MVC framework but is instead provided for ease
		/// of unit testing this type. See the comments at the end of this file for more
		/// information.
		/// </remarks>
		public AccountController(IFormsAuthentication formsAuth, IMembershipService service) {
			this.FormsAuth = formsAuth ?? new FormsAuthenticationService();
			this.MembershipService = service ?? new AccountMembershipService();
		}

		public IFormsAuthentication FormsAuth { get; private set; }

		public IMembershipService MembershipService { get; private set; }

		public ActionResult LogOn() {
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "Needs to take same parameter type as Controller.Redirect()")]
		public ActionResult LogOn(string userName, string password, bool rememberMe, string returnUrl) {
			if (!this.ValidateLogOn(userName, password)) {
				return View();
			}

			this.FormsAuth.SignIn(userName, rememberMe);
			if (!string.IsNullOrEmpty(returnUrl)) {
				return Redirect(returnUrl);
			} else {
				return RedirectToAction("Index", "Home");
			}
		}

		public ActionResult LogOff() {
			this.FormsAuth.SignOut();

			return RedirectToAction("Index", "Home");
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext) {
			if (filterContext.HttpContext.User.Identity is WindowsIdentity) {
				throw new InvalidOperationException("Windows authentication is not supported.");
			}
		}

		private bool ValidateLogOn(string userName, string password) {
			if (string.IsNullOrEmpty(userName)) {
				ModelState.AddModelError("username", "You must specify a username.");
			}
			if (string.IsNullOrEmpty(password)) {
				ModelState.AddModelError("password", "You must specify a password.");
			}
			if (!this.MembershipService.ValidateUser(userName, password)) {
				ModelState.AddModelError("_FORM", "The username or password provided is incorrect.");
			}

			return ModelState.IsValid;
		}
	}
}
