﻿<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SOS.Services.Mcv.Signals.Models.TxtWirePostInfo>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>Title</title>
	</head>
	<body>
		<form action="/Execute/121" method="POST">
			<br /><input type="text" id="title" value="This is title"/>
			<br /><input type="text" id="code" value="This is code"/>
			<br /><input type="text" id="shortcode" value="This is shortcode"/>
			<br /><input type="text" id="message" value="This is message"/>
			<br /><input type="text" id="phone" value="This is phone"/>
			<br /><input type="text" id="carrier" value="This is carrier"/>
			<br /><input type="text" id="keyword" value="This is keyword"/>
			<br /><input type="text" id="group" value="This is group"/>
			<br /><input type="text" id="custom_ticket" value="This is custom_ticket"/>
			<br /><input type="text" id="default_keyword" value="This is default_keyword"/>

			<br /><input type="text" id="user_name" value="This is user_name"/>
			<br /><input type="text" id="password" value="This is password"/>
			<br /><input type="text" id="api_key" value="This is api_key"/>
			<br />
			<input type="submit" value="Submit me" />
		</form>

	</body>
</html>
