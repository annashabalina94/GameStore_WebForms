using GameStore_WebForms.Models;
using GameStore_WebForms.Models.Repository;
using GameStore_WebForms.Pages.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GameStore_WebForms.Pages
{
    public partial class CartView : System.Web.UI.Page
    {
        /*В этом классе определен метод и пара свойств, которые понадобятся для отображения 
         * содержимого корзины. В этом примере хорошо видно, как используется класс 
         * SessionHelper — методам GetCart() и Get<T>() передается значение свойства Session
         * для получения требуемых объектов данных сеанса, при этом не приходится беспокоиться
         * о приведении типов или создании объекта Cart, если он не существует.*/
        protected void Page_Load(object sender, EventArgs e)
        {
            /*файл отделенного кода CartView.aspx.cs с добавленной обработкой HTTP-запроса POST,
         * который поступает после щелчка на одной из кнопок Remove. С помощью Request.Form 
         * из формы извлекается значение remove — это даст идентификатор товара, который 
         * пользователь желает удалить. Данный идентификатор применяется для получения объекта
         * Game из хранилища, а затем получения объекта Cart и вызова метода RemoveLine():*/
            if (IsPostBack)
            {
                Repository repository = new Repository();
                int gameId;
                if (int.TryParse(Request.Form["remove"], out gameId))
                {
                    Game gameToRemove = repository.Games
                        .Where(g => g.GameId == gameId).FirstOrDefault();
                    if (gameToRemove != null)
                    {
                        SessionHelper.GetCart(Session).RemoveLine(gameToRemove);
                    }
                }
            }
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return SessionHelper.GetCart(Session).Lines;
        }

        public decimal CartTotal
        {
            get
            {
                return SessionHelper.GetCart(Session).ComputeTotalValue();
            }
        }

        public string ReturnUrl
        {
            get
            {
                return SessionHelper.Get<string>(Session, SessionKey.RETURN_URL);
            }
        }
    }
}