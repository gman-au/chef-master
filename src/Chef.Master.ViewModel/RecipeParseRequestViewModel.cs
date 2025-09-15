namespace Chef.Master.ViewModel
{
    public class RecipeParseRequestViewModel
    {
        public string Url { get; set; }

        public string CustomImageUrl { get; set; }

        public bool IncludeQrCode { get; set; }

        public string Style { get; set; }

        public int? LastModelIndex { get; set; }
    }
}