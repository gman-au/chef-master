namespace Chef.Master.ViewModel
{
    public class StatusViewModel
    {
        public string Message { get; set; }

        public string Url { get; set; }

        public string CustomImageUrl { get; set; }

        public StagesViewModel Stages { get; set; }

        public string ConfirmationMessage { get; set; }

        public int? LastModelIndex { get; set; }
    }
}