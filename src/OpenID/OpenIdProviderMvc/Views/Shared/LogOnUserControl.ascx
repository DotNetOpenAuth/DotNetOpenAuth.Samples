<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="collapse navbar-collapse navbar-right" id="dnoa-navbar-collapse-1">
    <%
        if (Request.IsAuthenticated)
        {
    %>
    <div class="navbar-text">Welcome <b><%= Html.Encode(Page.User.Identity.Name) %></b>!</div>
    <ul class="nav navbar-nav">
        <li class="active"><%= Html.ActionLink("Log Off", "LogOff", "Account") %></li>
    </ul>
    <%
        }
        else
        {
    %>
    <ul class="nav navbar-nav">
        <li class="active"><%= Html.ActionLink("Log On", "LogOn", "Account") %></li>
    </ul>
    <%
        }
    %>
</div>
