namespace AspNetSimulator.Data.Contracts
{
    public interface IRoute
    {
        string DefaultController { get; set; }
        string DefaultAction { get; set; }
    }
}