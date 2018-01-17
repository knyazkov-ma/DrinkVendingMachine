namespace DrinkVendingMachine.DataService.Interface
{
    public interface IImageService
    {
       byte[] GetImageBody(long drinkId);
    }
}