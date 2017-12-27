<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" 
    Inherits="GameStore_WebForms.Controls.CategoryList" %>

<%= CreateHomeLinkHtml() %>

<% foreach (string category in GetCategories()) { //не срабатывает
       Response.Write(CreateLinkHtml(category));       
}%>