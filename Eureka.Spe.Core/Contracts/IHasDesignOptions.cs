namespace Eureka.Spe.Contracts
{
    public interface IHasDesignOptions
    {
        string TitlePosition { get; set; }
        string TitleColor { get; set; }
        string FontSize { get; set; }
        string FontWeight { get; set; }
        bool DisplayTitleInList { get; set; }
        bool DisplayTitleInDetails { get; set; }
    }
}