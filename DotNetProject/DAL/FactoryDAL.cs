namespace DAL
{
    public class FactoryDAL
    {
        public static Database Instance { get => Database.Instance; }
    }
}
