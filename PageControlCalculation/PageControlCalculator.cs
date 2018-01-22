using System;
using System.Collections.Generic;
using System.Linq;

namespace PageControlCalculation
{
    public class PageControlCalculator
    {
        private readonly int _sequentialWindowSize;
        private readonly int _pageSize;
        private readonly int _totalItemCount;

        private readonly int _defaultLeftPadding;
        private readonly int _defaultRightPadding;

        public PageControlCalculator(int pageSize, int totalItemCount, int sequentialWindowSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentException("Must specify a page size >= 1", nameof(pageSize));
            }

            if (sequentialWindowSize < 1)
            {
                throw new ArgumentException("Must specify a sequential window size >= 1", nameof(sequentialWindowSize));
            }

            if (totalItemCount < 0)
            {
                throw new ArgumentException("Cannot have a negative item count", nameof(totalItemCount));
            }

            _pageSize = pageSize;
            _sequentialWindowSize = sequentialWindowSize;
            _totalItemCount = totalItemCount;

            TotalPageCount = (int)Math.Ceiling(_totalItemCount / (float)_pageSize);

            _defaultLeftPadding = (int)Math.Floor(_sequentialWindowSize / (float)2);
            _defaultRightPadding = (int)Math.Ceiling(_sequentialWindowSize / (float)2) - 1;
        }


        public int TotalPageCount { get; }


        public PageControlInfo this[int pageIndex] => CalculateAtPageIndex(pageIndex);


        public PageControlInfo CalculateAtPageIndex(int pageIndex)
        {
            if (pageIndex < 1 || TotalPageCount == 0)
            {
                return PageControlInfo.NewEmptyResult();
            }

            return pageIndex > TotalPageCount
                    ? GetResultFrom(TotalPageCount)
                    : GetResultFrom(pageIndex);
        }
        

        public PageControlInfo CalculateAtItemIndex(int itemIndex)
        {
            var pageIndex = ConvertItemIndexToPageIndex(itemIndex);

            return CalculateAtPageIndex(pageIndex);
        }


        private int ConvertItemIndexToPageIndex(int itemIndex)
        {
            if (itemIndex > _totalItemCount || itemIndex < 1) return 0;

            return (int)Math.Ceiling(itemIndex / (float)_pageSize);
        }


        private PageControlInfo GetResultFrom(int pageIndex)
        {
            var itemIndexRange = GetItemIndexRange(pageIndex);
            var indexWindow = GetSequentialIndexWindow(pageIndex);
            var controlIndexMap = GetControlIndexMap(pageIndex);

            return new PageControlInfo(TotalPageCount, itemIndexRange, pageIndex, indexWindow, controlIndexMap);
        }


        private ItemRangeInfo GetItemIndexRange(int pageIndex)
        {
            var startItemIndex = (pageIndex - 1) * _pageSize + 1;
            var endItemIndex = startItemIndex + _pageSize - 1;

            endItemIndex = endItemIndex > _totalItemCount ? _totalItemCount : endItemIndex;

            return new ItemRangeInfo(_totalItemCount, startItemIndex, endItemIndex);
        }


        private PageControlIndexMap GetControlIndexMap(int pageIndex)
        {            
            var previousIndex = GetPreviousIndex(pageIndex);
            var nextIndex = GetNextIndex(pageIndex);

            var regressWindowIndex = GetRegressWindowIndex(pageIndex);
            var advanceWindowIndex = GetAdvanceWindowIndex(pageIndex);

            var firstIndex = GetFirstIndex(pageIndex);
            var lastIndex = GetLastIndex(pageIndex);

            return new PageControlIndexMap(previousIndex, nextIndex, 
                                           regressWindowIndex, advanceWindowIndex, 
                                           firstIndex, lastIndex);
        }

        
        private IReadOnlyList<int> GetSequentialIndexWindow(int pageIndex)
        {
            if (TotalPageCount < _sequentialWindowSize)
            {
                // truncated window size
                return Enumerable.Range(1, TotalPageCount).ToArray();
            }

            // full window size

            // While there is room to adjust right, the start index is the maximum of 1 and (index - leftPad)
            // If there is no longer room to adjust right, the start index is the total page count - windowSize + 1

            var startIndex = TotalPageCount - pageIndex >= _defaultRightPadding 
                                ? Math.Max(1, pageIndex - _defaultLeftPadding) 
                                : TotalPageCount - _sequentialWindowSize + 1;

            return Enumerable.Range(startIndex, _sequentialWindowSize).ToArray();
        }


        private static int GetPreviousIndex(int pageIndex)
        {
            var previousIndex = pageIndex - 1;
            return previousIndex < 1 ? 0 : previousIndex;
        }

        private int GetNextIndex(int pageIndex)
        {
            var nextIndex = pageIndex + 1;
            return nextIndex > TotalPageCount ? 0 : nextIndex;
        }

        private int GetRegressWindowIndex(int pageIndex)
        {
            var regressWindowIndex = pageIndex - _sequentialWindowSize;
            return regressWindowIndex < 1 ? 0 : regressWindowIndex;
        }

        private int GetAdvanceWindowIndex(int pageIndex)
        {
            var advanceWindowIndex = pageIndex + _sequentialWindowSize;
            return advanceWindowIndex > TotalPageCount ? 0 : advanceWindowIndex;
        }

        private int GetLastIndex(int pageIndex) => pageIndex == TotalPageCount ? 0 : TotalPageCount;
        
        private static int GetFirstIndex(int pageIndex) => pageIndex == 1 ? 0 : 1;
    }
}
