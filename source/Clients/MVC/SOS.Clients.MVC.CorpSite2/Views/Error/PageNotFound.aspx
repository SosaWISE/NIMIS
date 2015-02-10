<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Level1.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Freedom SOS | Error 404</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section id="content">
	<div class="container_24">
        <article class="grid_16">
            <img class="img-404" src="/images/404.png" alt="">
        </article>
        <article class="grid_8">
            <h3 class="reg">Sorry!</h3>
            <h4 class="p2">Page not found</h4>
            <p>The page you are looking for might have been removed, had its name changed, or is temporarily unavailable.</p>
            <p class="p3">Please try using our search box below to look for information on the internet.</p>
            <form id="search" method="post">
                <input type="text">
                <a class="button" onClick="document.getElementById('search').submit()">Search</a>
            </form>
        </article>
        <div class="clear"></div>
    </div>
</section>
</asp:Content>
