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
        public IActionResult Index(int clientId)
        {
            BudgetViweModel budgetViweModel = new BudgetViweModel();
            budgetViweModel.ClientId = clientId;
            var clientName = _client.GetClientById(clientId);
            ViewBag.ClientName = clientName.Select(x => x.ClientName).FirstOrDefault();

            ViewBag.Quadrant = new SelectList(this._budget.GetQuadrants(), "QuadrantId", "QuadrantName");
            ViewBag.QuadrantTooth = new SelectList(this._budget.GetQuadrantTooth(1), "QuadrantToothId", "ToothNumber");
            ViewBag.ProcedureName = new SelectList(this._budget.GetProcedures(), "ProcedureId", "ProcedureName");

            return View(budgetViweModel);
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
