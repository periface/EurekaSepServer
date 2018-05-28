namespace Eureka.Spe.Contracts
{
    public interface IHasDesignOptions
    {
        string TitlePosition { get; set; }
        string TitleColor { get; set; }
        string FontSize { get; set; }
        string FontWeight { get; set; }
    }
}