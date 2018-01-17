using DrinkVendingMachine.DAL;
using DrinkVendingMachine.DataService.Interface;
using System.Linq;

namespace DrinkVendingMachine.DataService
{
    public class ImageService : IImageService
    {
        private readonly DbContext dbContext;
        public ImageService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
                
        public byte[] GetImageBody(long drinkId)
        {
            return dbContext.Drinks
                .Where(t => t.Id == drinkId)
                .Select(t => t.Image)
                .FirstOrDefault();
        }
    }
}