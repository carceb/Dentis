using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dentis.Core.Interfaces;

namespace Dentis.Controllers
{
    public class BudgetController : Controller
    {
        private IBudget _budget;
        private IClient _client;
		public BudgetController(IBudget budget, IClient client)
		{
            this._budget = budget;
            this._client = client; 
		}
        public IActionResult Index(int clientId, int budgetId)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                BudgetViweModel budgetViweModel = new BudgetViweModel();

                ViewBag.ClientId = clientId;
                ViewBag.ClientName = _client.GetClientById(clientId).Select(x => x.ClientName).FirstOrDefault();

                ViewBag.Quadrant = new SelectList(this._budget.GetQuadrants(), "QuadrantId", "QuadrantName");
                ViewBag.QuadrantTooth = new SelectList(this._budget.GetQuadrantTooth(1), "QuadrantToothId", "ToothNumber");
                ViewBag.ProcedureName = new SelectList(this._budget.GetProcedures(), "ProcedureId", "ProcedureName");

                return View(budgetViweModel);
            }
            else 
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        public IActionResult Add(BudgetViweModel model)
        {
            if (HttpContext.Session.GetString("SecurityUserId") != null)
            {
                if (ModelState.IsValid)
                {
                    int budgetId = _budget.SaveBudget(model);
                    if (budgetId > 0)
                    {
                        return RedirectToAction("Index", "Budget", new { clientId = model.ClientId, budgetId = budgetId });
                    }
                }
            }

            return RedirectToAction("Error", "Home");
        }

        public JsonResult InsertBudget(List<Customer> customers)
        {
            int budgetId = 0;
            if (customers == null)
            {
                customers = new List<Customer>();
            }

            //Loop and insert records.
            //foreach (BudgetViweModel customer in customers)
            //{
            //    budgetId = _budget.SaveBudget(customer);
            //}
            return Json(budgetId);
        }

        public JsonResult GetQuadrantTooth(int quadrantToothId)
        {
            List<SelectListItem> quadrantTooths = new List<SelectListItem>();

            foreach (var item in this._budget.GetQuadrantTooth(quadrantToothId))
            {
                quadrantTooths.Add(new SelectListItem { Text = item.ToothNumber.ToString(), Value = item.QuadrantToothId.ToString() });
            }

            return Json(new SelectList(quadrantTooths, "Value", "Text"));
        }
    }
}
