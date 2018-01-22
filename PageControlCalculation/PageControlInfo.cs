
using System.Collections.Generic;
using System.Linq;

namespace PageControlCalculation
{
    public class PageControlInfo
    {
        private PageControlInfo()
        {
            SequentialIndexWindow = new int[0];
            ItemRange = ItemRangeInfo.NewEmptyRangeInfo();
            ControlIndexMap = PageControlIndexMap.NewNullMap();
        }

        internal static PageControlInfo NewEmptyResult() => new PageControlInfo();
        
        internal PageControlInfo(int totalPageCount, 
                                 ItemRangeInfo itemRange, 
                                 int currentPageIndex,
                                 IReadOnlyList<int> sequentialIndexWindow, 
                                 PageControlIndexMap controlIndexMap)
        {
            TotalPageCount = totalPageCount;
            ItemRange = itemRange;
            CurrentPageIndex = currentPageIndex;
            SequentialIndexWindow = sequentialIndexWindow;
            ControlIndexMap = controlIndexMap;
        }

        public int TotalPageCount { get; }

        public ItemRangeInfo ItemRange { get; }

        public int CurrentPageIndex { get; }

        public IReadOnlyList<int> SequentialIndexWindow { get; }

        public PageControlIndexMap ControlIndexMap { get; }

        public bool IsNonPaging => TotalPageCount < 2;

        public bool IsEmpty => TotalPageCount < 1;

        public bool FirstIndexIsOutsideWindow => SequentialIndexWindow.First() != 1;

        public bool LastIndexIsOutsideWindow => SequentialIndexWindow.Last() != TotalPageCount;

        public int WindowLeftGapSize => SequentialIndexWindow.First() - ControlIndexMap.First;

        public int WindowRightGapSize => ControlIndexMap.Last - SequentialIndexWindow.Last();

        public override string ToString()
        {
            var message = $"Showing items {ItemRange.StartIndex}-{ItemRange.EndIndex} of {ItemRange.TotalItemCount}";

            if (IsNonPaging) return message;

            var prevStr = ControlIndexMap.Previous != 0 ? $"<[{ControlIndexMap.Previous}]" : "*";
            var nextStr = ControlIndexMap.Next != 0 ? $">[{ControlIndexMap.Next}]" : "*";

            var regressStr = ControlIndexMap.RegressWindow != 0 ? $"<<[{ControlIndexMap.RegressWindow}]" : "*";
            var advanceStr = ControlIndexMap.AdvanceWindow != 0 ? $">>[{ControlIndexMap.AdvanceWindow}]" : "*";
            
            var firstStr = ControlIndexMap.First != 0 ? $"First[{ControlIndexMap.First}]" : "*";
            var lastStr = ControlIndexMap.Last != 0 ? $"Last[{ControlIndexMap.Last}]" : "*";

            var prefixStr = $"{firstStr,-10} {regressStr,-10} {prevStr,-10}";
            var postfixStr = $"{nextStr,-10} {advanceStr,-10} {lastStr,-10}";

            var indexWindowStr = string.Empty;

            foreach (var index in SequentialIndexWindow)
            {
                var indexStr = index.ToString();

                if (CurrentPageIndex.Equals(index))
                {
                    indexStr = "[" + indexStr + "]";
                }
                else
                {
                    indexStr = " " + indexStr + " ";
                }

                indexStr = $"{indexStr,-7}";

                indexWindowStr += indexStr + " ";
            }

            var result = $"{prefixStr} [{indexWindowStr}]        {postfixStr}   '{message}'";

            return result;
        }
    }
}
