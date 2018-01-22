
namespace PageControlCalculation
{
    public class ItemRangeInfo
    {
        private ItemRangeInfo() { }

        internal static ItemRangeInfo NewEmptyRangeInfo() => new ItemRangeInfo();
        

        internal ItemRangeInfo(int totalItemCount, int startIndex, int endIndex)
        {
            TotalItemCount = totalItemCount;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public int TotalItemCount { get; private set; }
    }
}
