namespace OMS.Domain.Interfaces
{
    public interface IProfile
    {
        int Id { get; }
        string Name { get; }
        string Picture { get; }
        string ContactInfo { get; }
        bool IsSocialAccount { get; }
    }
}
