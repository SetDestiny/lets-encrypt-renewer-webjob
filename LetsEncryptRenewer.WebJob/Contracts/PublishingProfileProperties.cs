namespace LetsEncryptRenewer.WebJob.Contracts
{
    public class PublishingProfileProperties
    {
        public string Name { get; set; }
        public string PublishingUserName { get; set; }
        public string PublishingPassword { get; set; }
        public string PublishingPasswordHash { get; set; }
        public string PublishingPasswordHashSalt { get; set; }
        public string Metadata { get; set; }
        public string IsDeleted { get; set; }
        public string ScmUri { get; set; }
    }
}
