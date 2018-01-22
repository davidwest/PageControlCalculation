
using System.Text;

namespace PageControlCalculation
{
	public class PageNavigationRenderer
	{
		private readonly IControlStringSelector _selector;

		private PageControlCalculator _calculator;
		
		public PageNavigationRenderer(IControlStringSelector selector)
		{
			_selector = selector;
		}
		
		public int Initialize(int pageSize, int totalItemCount, int sequentialWindowSize = 3)
		{
			if (pageSize < 1)
			{
				pageSize = 1;
			}

			if (totalItemCount < 0)
			{
				totalItemCount = 0;
			}

			if (sequentialWindowSize < 3)
			{
				sequentialWindowSize = 3;
			}

			_calculator = new PageControlCalculator(pageSize, totalItemCount, sequentialWindowSize);

			return _calculator.TotalPageCount;
		}

		public RenderedNavigationInfo RenderControlsAtPageIndex(int pageIndex)
		{
			var controlInfo = _calculator.CalculateAtPageIndex(pageIndex);
			
			return RenderControls(controlInfo);
		}

		public RenderedNavigationInfo RenderControlsAtItemIndex(int itemIndex)
		{
			var controlInfo = _calculator.CalculateAtItemIndex(itemIndex);

			return RenderControls(controlInfo);
		}

		private RenderedNavigationInfo RenderControls(PageControlInfo controlInfo)
		{
			if (controlInfo.ItemRange.TotalItemCount == 0)
			{
				return new RenderedNavigationInfo("", controlInfo.ItemRange, controlInfo.TotalPageCount);	
			}

			var builder = new StringBuilder();

			controlInfo.SequenceCanonicalControls(index => builder.Append(_selector.RenderPreviousControl(index)),
												  () => builder.Append(_selector.RenderGapMarker()),
												  (index, isSelected) => builder.Append(_selector.RenderOrdinalControl(index, isSelected)),
												  index => builder.Append(_selector.RenderNextControl(index)));

			var result = new RenderedNavigationInfo(builder.ToString(), controlInfo.ItemRange, _calculator.TotalPageCount);

			return result;
		}
	}
}
