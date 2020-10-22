namespace Client.ViewModels
{
    public class PagerViewModel
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int BrandIdSelected { get; set; }
        public int TypeIdSelected { get; set; }
        public string SortSelected { get; set; }
        public string SearchSelected { get; set; }

        // public string QueryParams { get; set; }



    }
}
