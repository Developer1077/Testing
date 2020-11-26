using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IBasketService
    {
       // Basket BasketSource { get; set; }
        Basket GetBasket(string id);
        Basket SetBasket(Basket basket);
        Basket GetCurrentBasketValue();
        void AddItemToBasket(int productId, int quantity = 1);

        BasketTotals CalculateTotals();

        void IncrementItemQuantity(int id);
        void DecrementItemQuantity(int id);

        void RemoveItemFromBasket(int id);
        void DeleteBasket(Basket basket);
    }
}
