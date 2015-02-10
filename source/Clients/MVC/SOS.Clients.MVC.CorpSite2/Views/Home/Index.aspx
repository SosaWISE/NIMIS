<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Root.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">FREEDOM SOS</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section id="content_root">
	<div class="container_24">
		<h4>Satellite GPS Medical Monitoring 24 hours a day 7 days a week.</h4>
		<img src="../../images/Root_Blob.png" alt="" />
		<%--<iframe width="420" height="315" src="http://www.youtube.com/embed/1vinS3HZH5c" frameborder="0" allowfullscreen></iframe>--%>
		<iframe src="http://player.vimeo.com/video/47028775?autoplay=1" width="500" height="375" frameborder="0" webkitAllowFullScreen mozallowfullscreen allowFullScreen></iframe>
	</div>
</section>
</asp:Content>