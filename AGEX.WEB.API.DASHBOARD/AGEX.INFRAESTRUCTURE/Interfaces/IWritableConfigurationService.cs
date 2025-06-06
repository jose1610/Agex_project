namespace AGEX.INFRAESTRUCTURE.Interfaces
{
    public interface IWritableConfigurationService<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}
