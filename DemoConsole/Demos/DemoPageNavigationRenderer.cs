using System.Diagnostics;

namespace PageControlCalculation.DemoConsole.Demos
{
	public static class DemoPageNavigationRenderer
	{
		public static void Start()
		{
			var renderer = new PageNavigationRenderer(new DemoControlStringSelector());
			var totalPageCount = renderer.Initialize(10, 3071, 5);

			for (var i = 1; i <= totalPageCount; i++)
			{
				var info = renderer.RenderControlsAtPageIndex(i);

				Debug.WriteLine($"{info.RenderedControls, -100} Showing items {info.ItemRange.StartIndex} - {info.ItemRange.EndIndex} of {info.ItemRange.TotalItemCount}");
			}
		}
	}

	public class DemoControlStringSelector : IControlStringSelector
	{
		public string RenderPreviousControl(int? index) => $" < ({(index.HasValue ? $"{index.Value}" : "X")}) ";

		public string RenderGapMarker() => "...";

		public string RenderOrdinalControl(int index, bool isSelected) => $"[{index}{(isSelected ? "*" : "")}]";
		
		public string RenderNextControl(int? index) => $" > ({(index.HasValue ? $"{index.Value}" : "X")})";
	}
}
