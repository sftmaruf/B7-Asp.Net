using Autofac;
using Library.Areas.Admin.Models;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReaderController : Controller
    {
        private ILogger<ReaderController> _logger;
        private ILifetimeScope _scope;

        public ReaderController(ILogger<ReaderController> logger, ILifetimeScope scope)
        {
            _logger = logger;
            _scope = scope;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = _scope.Resolve<ReaderCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReaderCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.CreateReader();
            }
            return View(model);
        }

        public JsonResult GetReaderData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<ReaderListModel>();
            return Json(model.GetPagedBooks(dataTableModel));
        }
    }
}
