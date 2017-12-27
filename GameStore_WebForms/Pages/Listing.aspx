<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="GameStore_WebForms.Pages.Listing" 
    MasterPageFile="~/Pages/Store.Master"%>
<%@ Import Namespace="System.Web.Routing" %>

 <asp:Content ContentPlaceHolderID="bodyContent" runat="server">  
        <div id="content">
            <%
                foreach (GameStore_WebForms.Models.Game game in GetGames())
                {
                   Response.Write(String.Format(@"
                        <div class='item'>
                            <h3>{0}</h3>
                            {1}
                            <h4>{2:c}</h4>
                            <button name='add' type='submit' value='{3}'>
                                Добавить в корзину
                            </button>
                        </div>",
                    game.Name, game.Description, game.Price, game.GameId)); 
                }
            %>
        </div>
    <div class="pager">
        <%
            for (int i = 1; i <= MaxPage; i++)
            {
                string path = RouteTable.Routes.GetVirtualPath(null, null,
                    new RouteValueDictionary() { { "page", i } }).VirtualPath;
                Response.Write(
                    String.Format("<a href='{0}' {1}>{2}</a>",
                        path, i == CurrentPage ? "class='selected'" : "", i));
            }
        %>

        </div>
</asp:Content>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Listing.aspx.cs" Inherits="GameStore_WebForms.Pages.Listing"
    MasterPageFile="~/Pages/Store.Master" %>

<%@ Import Namespace="System.Web.Routing" %>

<asp:Content ContentPlaceHolderID="bodyContent" runat="server">
    <div id="content">
        <asp:Repeater ItemType="GameStore_WebForms.Models.Game"
            SelectMethod="GetGames" runat="server">
            <ItemTemplate>
                <div class="item">
                    <h3><%# Item.Name %></h3>
                    <%# Item.Description %>
                    <h4><%# Item.Price.ToString("c") %></h4>
                    <button name="add" type="submit" value="<%# Item.GameId %>">
                        Добавить в корзину
                    </button>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="pager">
        <%
            for (int i = 1; i <= MaxPage; i++)
            {
                string category = (string)Page.RouteData.Values["category"]
                    ?? Request.QueryString["category"];
                
                string path = RouteTable.Routes.GetVirtualPath(null, null,
                    new RouteValueDictionary() { {"category", category}, { "page", i } }).VirtualPath;
                Response.Write(
                    String.Format("<a href='{0}' {1}>{2}</a>",
                        path, i == CurrentPage ? "class='selected'" : "", i));
            }
        %>
    </div>
</asp:Content>
