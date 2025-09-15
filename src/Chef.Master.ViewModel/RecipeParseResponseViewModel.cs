namespace Chef.Master.ViewModel
{
    public class RecipeParseResponseViewModel
    {
        public bool Success { get; set; }

        public RecipeViewModel Recipe { get; set; }

        public StatusViewModel Status { get; set; }

        public string QrCodeBase64 { get; set; }
    }
}