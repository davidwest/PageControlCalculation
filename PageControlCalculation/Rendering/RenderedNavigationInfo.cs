
namespace PageControlCalculation
{
	public class RenderedNavigationInfo
	{
		internal RenderedNavigationInfo(string renderedControls, ItemRangeInfo itemRange, int totalPageCount)
		{
			RenderedControls = renderedControls;
			ItemRange = itemRange;
			TotalPageCount = totalPageCount;
		}

		public string RenderedControls { get; }
		public ItemRangeInfo ItemRange { get; }

		public int TotalPageCount { get; }
		public bool IsNonPaging => TotalPageCount <= 1;
	}
}
