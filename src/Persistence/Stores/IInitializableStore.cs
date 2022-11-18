namespace DucksAndDogs.Persistence.Stores;

public interface IInitializableStore
{
    public Task InitializeStoreAsync();
}