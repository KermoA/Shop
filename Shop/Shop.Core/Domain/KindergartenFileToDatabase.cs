namespace Shop.Core.Domain
{
    public class KindergartenFileToDatabase
    {
        public Guid Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
