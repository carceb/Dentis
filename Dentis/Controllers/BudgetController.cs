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
            try
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

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
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

        public JsonResult AddBudget(string customers)
        {
            try
            {
                int budgetId = 0;

                if (customers != "[]")
                {
                    var list = JsonExtensions.FromDelimitedJson<BudgetViweModel>(new StringReader(customers.Replace("[", string.Empty).Replace("]", string.Empty))).ToList();

                    budgetId = _budget.AddOrEdit(list);
                }

                return Json(budgetId);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public IActionResult PrintBudget(int budgetId, int clinicConsultingId)
        {
            try
            {
                if (budgetId > 0)
                {
                    var hostName = (Request.Host.Value.Contains("localhost:80") ? "localhost/Dentis" : Request.Host.Value);
                    ViewBag.ShareLink = $"{Request.Scheme}://{hostName}{Request.Path.Value}/PrintBudget?budgetId={budgetId}&clinicConsultingId={clinicConsultingId}";
                    var model = _budget.GetBudgetDetailByBudgetIdAndClinicConsultingId(budgetId, clinicConsultingId);
                    return View(model);
                }

                return RedirectToAction("Error", "Home", new { errorMessage = "Presupuesto no existe" });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
        }

        public IActionResult ListBudgets(int clientId)
        {
            try
            {
                int clinicConsultingId = 0;
                if (clientId > 0)
                {
                    if (HttpContext.Session.GetString("SecurityUserId") != null)
                    {
                        clinicConsultingId = (int)HttpContext.Session.GetInt32("ClinicConsultingId");

                        var model = _budget.GetBudgetDetailByClientIdAndClinicConsultingId(clientId, clinicConsultingId);
                        if (model.Any())
                        {
                            return View(model);
                        }
                    }
                }

                return RedirectToAction("Error", "Home", new { errorMessage = "Cliente no existe" });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
            }
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
