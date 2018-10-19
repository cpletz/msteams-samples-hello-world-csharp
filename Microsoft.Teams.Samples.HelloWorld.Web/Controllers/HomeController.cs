using Charts;
using Microsoft.Teams.Samples.HelloWorld.Web.ViewModels;
using System.Web.Mvc;

namespace Microsoft.Teams.Samples.HelloWorld.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("hello")]
        public ActionResult Hello()
        {
            return View("Index");
        }

        [Route("first")]
        public ActionResult First()
        {
            return View(new ChartViewModel(
                FakeCharts.performance(),
                FakeCharts.marks(),
                FakeCharts.nextTests()));
        }

        [Route("second")]
        public ActionResult Second()
        {
            return View(new ChartViewModel(FakeCharts.optimalTestDates()));
        }

        [Route("third")]
        public ActionResult Third()
        {
            return View(new ChartViewModel(
                FakeCharts.performance(),
                FakeCharts.marks(),
                FakeCharts.nextTests()));
        }

        [Route("configure")]
        public ActionResult Configure()
        {
            return View();
        }
    }
}
