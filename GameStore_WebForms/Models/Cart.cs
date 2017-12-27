using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore_WebForms.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Game game, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Game.GameId == game.GameId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Game = game,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Game game)
        {
            lineCollection.RemoveAll(l => l.Game.GameId == game.GameId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Game.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
    /*Класс Cart использует класс CartLine, определенный в том же самом файле, для представления выбранного пользователем товара 
     * и приобретаемого количества единиц этого товара. Мы определили методы для добавления элемента в корзину, удаления из
     * корзины ранее добавленного элемента, вычисления общей стоимости элементов в корзине и сброса корзины за счет удаления всех 
     * помещенных в нее элементов. Мы также предоставили свойство, которое обеспечивает доступ к содержимому корзины, используя IEnumerble<CartLine>.
     * Все это легко реализуется на C# с небольшой долей LINQ.*/
    public class CartLine
    {
        public Game Game { get; set; }
        public int Quantity { get; set; }
    }
}