using DrinkVendingMachine.Aspects;
using DrinkVendingMachine.DAL;
using DrinkVendingMachine.DataService.Interface;
using DrinkVendingMachine.DTO;
using DrinkVendingMachine.Entity;
using System.Collections.Generic;
using System.Linq;

namespace DrinkVendingMachine.DataService
{
    [Transaction]
    public class AdminService : IAdminService
    {
        private readonly DbContext dbContext;
        public AdminService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<DrinkDTO> GetDrinks()
        {
            return dbContext.Drinks
                .OrderBy(t => t.Ord)
                .Select(d => new  DrinkDTO
                            {
                                Id = d.Id,
                                Cost = d.Cost,
                                Count = d.Count,
                                Ord = d.Ord
                            })
                .ToList();

        }

        public DrinkDTO GetDrink(long id)
        {
            if (id == 0)
                return new DrinkDTO { Count = 10 };

            return dbContext.Drinks
                .Select(d => new DrinkDTO
                            {
                                Id = d.Id,
                                Cost = d.Cost,
                                Count = d.Count,
                                Ord = d.Ord
                            })
                .FirstOrDefault(t => t.Id == id);
        }

        public void SaveDrink(DrinkDTO dto, byte[] image)
        {
            Drink entity = null;
            if (dto.Id == 0)
            {
                entity = new Drink();
                dbContext.Drinks.Add(entity);
            }
            else
            {
                entity = dbContext.Drinks.FirstOrDefault(t => t.Id == dto.Id);
            }

            entity.Cost = dto.Cost;
            entity.Count = dto.Count;
            entity.Ord = dto.Ord;

            if(image != null)
                entity.Image = image;

            dbContext.SaveChanges();
        }

        public void DeleteDrink(long id)
        {
            Drink entity = dbContext.Drinks.FirstOrDefault(t => t.Id == id);
            dbContext.Drinks.Remove(entity);
            dbContext.SaveChanges();
        }

        public IEnumerable<CoinDTO> GetCoins()
        {
            return dbContext.Coins
                .Select(c => new CoinDTO
                            {
                                Id = c.Id,
                                Denomination = (int)c.Denomination,
                                Lock = c.Lock,
                                Count = c.Count
                            })
                .OrderBy(c => c.Denomination)
                .ToList();
        }

        public CoinDTO GetCoin(long id)
        {
            return dbContext.Coins
                .Select(c => new CoinDTO
                            {
                                Id = c.Id,
                                Denomination = (int)c.Denomination,
                                Lock = c.Lock,
                                Count = c.Count
                            })
                .FirstOrDefault(t => t.Id == id);
        }

        public void UpdateCoin(CoinDTO dto)
        {
            Coin entity = dbContext.Coins.FirstOrDefault(t => t.Id == dto.Id);

            entity.Count = dto.Count;
            entity.Lock = dto.Lock;

            dbContext.SaveChanges();
        }

    }
}