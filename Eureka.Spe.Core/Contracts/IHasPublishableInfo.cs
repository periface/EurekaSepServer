namespace Eureka.Spe.Contracts
{
    public interface IHasPublishableInfo
    {
        string Title { get; set; }
        string Description { get; set; }
        string Content { get; set; }
        string Img { get; set; }
    }
}