namespace MAP.Data.Infrastructure
{
    public interface IDataBaseFactory
    {
        ProjectContext DataContext { get;  }
    }
}
