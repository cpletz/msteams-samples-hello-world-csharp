namespace Microsoft.Teams.Samples.HelloWorld.Web.ViewModels
{
    public class ChartViewModel
    {
        public ChartViewModel(params string[] charts)
        {
            Charts = charts;
        }
        public string[] Charts { get; }
    }
}