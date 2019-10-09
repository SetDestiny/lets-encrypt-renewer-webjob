namespace LetsEncryptRenewer.WebJob.Contracts
{
    public class PublishingCredentials
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
        public PublishingProfileProperties Properties { get; set; }
    }
}

