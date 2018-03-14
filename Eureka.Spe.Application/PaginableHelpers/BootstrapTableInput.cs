namespace Eureka.Spe.PaginableHelpers
{
    public class BootstrapTableInput
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public string order { get; set; }
        public string search { get; set; }
        public string sort { get; set; }
    }
}
