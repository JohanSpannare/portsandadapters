namespace Store.Domain.Stores
{
  public class StoreDao
  {
    public string StoreId { get; set; }
    public string Name { get; set; }
    public string[] ProductIds { get; set; }
  }
}