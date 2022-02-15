using Dentis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
                ViewBag.ConsultingName = HttpContext.Session.GetString("ClinicConsultingName").ToString();
                budgetViweModel.ClientId = clientId;
                budgetViweModel.ClinicConsultingId = (int)HttpContext.Session.GetInt32("ClinicConsultingId");

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
                    //int budgetId = _budget.SaveBudget(model);
                    //if (budgetId > 0)
                    //{
                    //    return RedirectToAction("Index", "Budget", new { clientId = model.ClientId, budgetId = budgetId });
                    //}
                }
            }

            return RedirectToAction("Error", "Home");
        }

        public JsonResult InsertBudget(string customers)
        {
            int budgetId = 0;

            if (customers != "[]")
            {
                var list = JsonExtensions.FromDelimitedJson<BudgetViweModel>(new StringReader(customers.Replace("[", string.Empty).Replace("]", string.Empty))).ToList();

                budgetId = _budget.SaveBudget(list);
            }

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

        public static partial class JsonExtensions
        {
            public static IEnumerable<T> FromDelimitedJson<T>(TextReader reader, JsonSerializerSettings settings = null)
            {
                using (var jsonReader = new JsonTextReader(reader) { CloseInput = false, SupportMultipleContent = true })
                {
                    var serializer = JsonSerializer.CreateDefault(settings);

                    while (jsonReader.Read())
                    {
                        if (jsonReader.TokenType == JsonToken.Comment)
                            continue;
                        yield return serializer.Deserialize<T>(jsonReader);
                    }
                }
            }
        }
    }

}
