using System.Diagnostics;

namespace PageControlCalculation.DemoConsole.Demos
{
    public static class DemoPageControlCalculator
    {
        public static void Start()
        {
            const int pageSize = 10;
            const int sequentialWindowSize = 5;
            const int itemCount = 3071;

            var calculator = new PageControlCalculator(pageSize, itemCount, sequentialWindowSize);

			for (var pageIndex = 1; pageIndex <= calculator.TotalPageCount; pageIndex++)
			{
				var controlInfo = calculator[pageIndex];

				Debug.WriteLine(controlInfo);
			}
		}
    }
}
