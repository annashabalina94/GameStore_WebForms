using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace GameStore_WebForms.App_Start
{
    public class RouteConfig
    {
        /*Класс RouteConfig применяется для определения новой схемы URL для приложения GameStore.Параметр routes,
            передаваемый методу RegisterRoutes(), представляет собой объект RouteCollection.Его метод MapPageRoute() используется
            для создания маршрутов.Маршрут сообщает среде ASP.NET Framework, каким образом обрабатывать URL,
            который не соответствует файлу веб-формы.aspx на диске.*/
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(null, "list/{category}/{page}",
                                        "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list/{page}", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "", "~/Pages/Listing.aspx");
            routes.MapPageRoute(null, "list", "~/Pages/Listing.aspx");

            routes.MapPageRoute("cart", "cart", "~/Pages/CartView.aspx");
        }
    }
}