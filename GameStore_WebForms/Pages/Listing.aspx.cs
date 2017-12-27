using GameStore_WebForms.Models;
using GameStore_WebForms.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using GameStore_WebForms.Pages.Helpers;

namespace GameStore_WebForms.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repository = new Repository();
        private int pageSize = 4;

        /*Мы находим значение идентификатора требуемого товара в получаемых данных формы и затем извлекаем соответствующий
         * объект Game из хранилища. С помощью класса SessionHelper мы получаем объект Cart, ассоциированный с сеансом пользователя,
         * и добавляем к нему выбранный товар.
         * Реагирование на отправку формы завершается перенаправлением пользователя на другой URL
с применением метода Response.Redirect(). При этом URL, на который перенаправляется браузер, генерируется на
основе конфигурации маршрутизации — при генерации этого URL по-прежнему передается много значений null, но данная
версия GetVirtualPath() создает URL из маршрута по имени cart. */
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                int selectedGameId;
                if (int.TryParse(Request.Form["add"], out selectedGameId))
                {
                    Game selectedGame = repository.Games
                        .Where(g => g.GameId == selectedGameId).FirstOrDefault();

                    if (selectedGame != null)
                    {
                        SessionHelper.GetCart(Session).AddItem(selectedGame, 1);
                        SessionHelper.Set(Session, SessionKey.RETURN_URL,
                            Request.RawUrl);

                        Response.Redirect(RouteTable.Routes
                            .GetVirtualPath(null, "cart", null).VirtualPath);
                    }
                }
            }
        }
        protected int CurrentPage
        {
            get
            {
                int page;
                page = GetPageFromRequest();
                return page > MaxPage ? MaxPage : page;
            }
        }

        protected int MaxPage
        {
            get
            {
                int prodCount = FilterGames().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }

        private int GetPageFromRequest()
        {
            int page;
            string reqValue = (string)RouteData.Values["page"] ??
                Request.QueryString["page"];
            return reqValue != null && int.TryParse(reqValue, out page) ? page : 1;
        }

      
        public IEnumerable<Game> GetGames()
        {
            /*Метод OrderBy() из LINQ обеспечивает обработку объектов Game 
             * в одном и том же порядке, метод Skip() дает возможность проигнорировать
             * объекты Game, встречающиеся перед желаемой страницей, а метод Take()
             * позволяет выбрать нужное количество объектов Game для отображения пользователю.*/
            return FilterGames()
                .OrderBy(g => g.GameId)
                .Skip((CurrentPage - 1) * pageSize)
                .Take(pageSize);
        }

        // Новый вспомогательный метод для фильтрации игр по категориям
        private IEnumerable<Game> FilterGames()
        {
            IEnumerable<Game> games = repository.Games;
            string currentCategory = (string)RouteData.Values["category"] ??
                Request.QueryString["category"];
            return currentCategory == null ? games :
                games.Where(p => p.Category == currentCategory);
        }

       
    }
}