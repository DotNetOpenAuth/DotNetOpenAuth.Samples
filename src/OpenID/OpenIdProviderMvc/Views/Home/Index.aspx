<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<%@ Register Assembly="DotNetOpenAuth.OpenId.UI" Namespace="DotNetOpenAuth" TagPrefix="openauth" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
	Home Page
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="HeadContent">
	<openauth:XrdsPublisher ID="XrdsPublisher1" runat="server" XrdsUrl="~/Home/xrds"
		XrdsAutoAnswer="false" />
</asp:Content>
