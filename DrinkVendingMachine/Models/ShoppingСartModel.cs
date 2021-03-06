﻿using DrinkVendingMachine.DTO;
using System.Collections.Generic;

namespace DrinkVendingMachine.Models
{
    public class ShoppingСartModel
    {
        public ShoppingСartModel(
            IEnumerable<ShowcaseDrinkDTO> drinks, 
            IEnumerable<ShowcaseCoinDTO> coins, 
            IEnumerable<SurrenderCoinDTO> surrender,
            bool canbeSurrender,
            int shoppingСartTotalPayment, 
            decimal shoppingСartTotalCost)
        {
            Drinks = drinks;
            Coins = coins;
            Surrender = surrender;
            CanbeSurrender = canbeSurrender;
            ShoppingСartTotalPayment = shoppingСartTotalPayment;
            ShoppingСartTotalCost = shoppingСartTotalCost;
        }

        public IEnumerable<ShowcaseDrinkDTO> Drinks { get; private set; }
        public IEnumerable<ShowcaseCoinDTO> Coins { get; private set; }

        public IEnumerable<SurrenderCoinDTO> Surrender { get; private set; }

        public bool CanbeSurrender { get; private set; }

        public int ShoppingСartTotalPayment { get; private set; }

        public decimal ShoppingСartTotalCost { get; private set; }
    }
}