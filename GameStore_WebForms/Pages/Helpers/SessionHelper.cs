using GameStore_WebForms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace GameStore_WebForms.Pages.Helpers
{
    /*В файле было определено перечисление (enum) с именем SessionKey,
     * которое содержит значения для типов данных, сохраняемых в сеансе. 
     * В перечислении определены значения CART (будет использоваться для 
     * объектов Cart) и RETURN_URL (будет применяться для URL, на который
     * пользователи возвращаются в результате щелчка на кнопке "Продолжить 
     * покупку", гарантируя сохранение значений категории и разбиения на страницы).*/
    public enum SessionKey
    {
        CART,
        RETURN_URL
    }
    /*Класс SessionHelper содержит метод Set(), предназначенный для помещения нового объекта данных
     * в состояние сеанса с использованием значения SessionKey. Метод Get<T>() принимает значение 
     * SessionKey и возвращает соответствующий объект данных. Метод Get() имеет параметр обобщенного
     * типа, который применяется для обеспечения того, что ожидаемый тип данных совпадает с типом 
     * сохраненных данных. На основе методов Get<T>() и Set() построен метод GetCart(), который 
     * решает проблемы дублирования кода и управляет объектом Cart для пользователя в единственном месте.*/
    public class SessionHelper
    {
        public static void Set(HttpSessionState session, SessionKey key, object value)
        {
            session[Enum.GetName(typeof(SessionKey), key)] = value;
        }

        public static T Get<T>(HttpSessionState session, SessionKey key)
        {
            object dataValue = session[Enum.GetName(typeof(SessionKey), key)];
            if (dataValue != null && dataValue is T)
            {
                return (T)dataValue;
            }
            else
            {
                return default(T);
            }
        }

        public static Cart GetCart(HttpSessionState session)
        {
            Cart myCart = Get<Cart>(session, SessionKey.CART);
            if (myCart == null)
            {
                myCart = new Cart();
                Set(session, SessionKey.CART, myCart);
            }
            return myCart;
        }
    }
}