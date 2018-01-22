
using System;

namespace PageControlCalculation
{
    public static class PageControlInfoExtensions
    {
        public static void SequenceCanonicalControls(this PageControlInfo controlInfo,
                                                     Action<int?> onPrevious,
                                                     Action onGap,
                                                     Action<int, bool> onOrdinal,
                                                     Action<int?> onNext)
        {
            var controlIndexMap = controlInfo.ControlIndexMap;
            var centralWindow = controlInfo.SequentialIndexWindow;
            var currentPageIndex = controlInfo.CurrentPageIndex;

            onPrevious(controlIndexMap.Previous != default(int) ? (int?)controlIndexMap.Previous : null);

            if (controlInfo.FirstIndexIsOutsideWindow)
            {
                onOrdinal(1, false);
            }

            var leftGapSize = controlInfo.WindowLeftGapSize;

            if (leftGapSize == 2)
            {
                onOrdinal(2, false);
            }
            else if (leftGapSize > 2)
            {
                onGap();
            }

            foreach (var index in centralWindow)
            {
                onOrdinal(index, index == currentPageIndex);
            }

            var rightGapSize = controlInfo.WindowRightGapSize;

            if (rightGapSize > 2)
            {
                onGap();
            }
            else if (rightGapSize == 2)
            {
                var index = controlIndexMap.Last - 1;
                onOrdinal(index, false);
            }

            if (controlInfo.LastIndexIsOutsideWindow)
            {
                onOrdinal(controlIndexMap.Last, false);
            }

            onNext(controlIndexMap.Next != default(int) ? (int?)controlIndexMap.Next : null);
        }
    }
}
