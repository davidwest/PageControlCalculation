
namespace PageControlCalculation
{
    public class PageControlIndexMap
    {
        private PageControlIndexMap() { }

        internal static PageControlIndexMap NewNullMap() => new PageControlIndexMap();
        

        internal PageControlIndexMap(int previous, int next, int regressWindow, int advanceWindow, int first, int last)
        {
            Previous = previous;
            Next = next;

            AdvanceWindow = advanceWindow;
            RegressWindow = regressWindow;

            First = first;
            Last = last;
        }

        public int Previous { get; }
        public int Next { get; }

        public int AdvanceWindow { get; }
        public int RegressWindow { get; }

        public int First { get; }
        public int Last { get; }
    }
}
