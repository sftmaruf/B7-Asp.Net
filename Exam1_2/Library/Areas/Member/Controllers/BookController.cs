using Autofac;
using Library.Areas.Member.Models;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Areas.Member.Controllers
{
    [Area("Member")]
    public class BookController : Controller
    {
        private ILogger<BookController> _logger;
        private ILifetimeScope _scope;

        public BookController(ILogger<BookController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetBookData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<BookListModel>();
            return Json(model.GetPagedBooks(dataTableModel));
        }
    }
}
