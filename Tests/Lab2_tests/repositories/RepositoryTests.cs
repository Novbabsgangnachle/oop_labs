namespace DefaultNamespace;

public class RepositoryTests
{
    [Fact]
    public void Add_ExistingEntity_ShouldThrowArgumentException()
    {
        RepositoryBase<MockEntity> repo = GetRepository();
        var entity = new MockEntity();
        repo.Add(entity);

        Assert.Throws<ArgumentException>(() => repo.Add(entity));
    }

    [Fact]
    public void Add_ValidEntity_ShouldAddSuccessfully()
    {
        RepositoryBase<MockEntity> repo = GetRepository();
        var entity = new MockEntity();

        repo.Add(entity);

        MockEntity retrieved = repo.GetById(entity.Id);
        Assert.Equal(entity.Id, retrieved.Id);
    }

    [Fact]
    public void GetById_NonExistentId_ShouldThrowKeyNotFoundException()
    {
        RepositoryBase<MockEntity> repo = GetRepository();
        var nonExistentId = Guid.NewGuid();

        Assert.Throws<KeyNotFoundException>(() => repo.GetById(nonExistentId));
    }

    [Fact]
    public void Remove_NonExistentId_ShouldThrowKeyNotFoundException()
    {
        RepositoryBase<MockEntity> repo = GetRepository();
        var nonExistentId = Guid.NewGuid();

        Assert.Throws<KeyNotFoundException>(() => repo.Remove(nonExistentId));
    }

    [Fact]
    public void Remove_ExistingId_ShouldRemoveSuccessfully()
    {
        RepositoryBase<MockEntity> repo = GetRepository();
        var entity = new MockEntity();
        repo.Add(entity);

        repo.Remove(entity.Id);

        Assert.Throws<KeyNotFoundException>(() => repo.GetById(entity.Id));
    }

    [Fact]
    public void Singleton_Instance_ShouldBeSameAcrossCalls()
    {
        var instance1 = RepositoryBase<MockEntity>.Instance();
        var instance2 = RepositoryBase<MockEntity>.Instance();

        Assert.Same(instance1, instance2);
    }

    private RepositoryBase<MockEntity> GetRepository()
    {
        var instance = RepositoryBase<MockEntity>.Instance();

        FieldInfo? storageField =
            typeof(RepositoryBase<MockEntity>).GetField("_storage", BindingFlags.NonPublic | BindingFlags.Instance);
        var storage = storageField?.GetValue(instance) as Dictionary<Guid, IEntity>;
        storage?.Clear();
        return instance;
    }

    private class MockEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}