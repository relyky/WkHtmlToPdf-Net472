namespace WkHtmlToPdfDotNet
{
    public class TableOfContentsSettings : ObjectSettings
    {
        [WkHtml("isTableOfContent")]
        public bool IsTableOfContent { get; set; }

        public TableOfContentsSettings()
        {
            IsTableOfContent = true;
        }
    }
}
