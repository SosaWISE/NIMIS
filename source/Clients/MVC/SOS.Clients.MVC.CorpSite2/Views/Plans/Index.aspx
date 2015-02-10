<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" MasterPageFile="~/Views/Shared/Level1.Master" %>

<asp:Content runat="server" ID="Title" ContentPlaceHolderID="TitleContent">
	Freedom SOS | Plans</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
	<section id="content_root">
		<br />
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_24">
					<h2><span class="links" style="color:red;">Enjoy piece of mind for a little over $1 a day.</span></h2>
				</div>
			</div>
		</div>
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_12">
					<p>&nbsp;</p>
					<ul class="list-1" style="text-align: left; padding-left: 100px;">
						<li><a href="#">24/7/365 US-based Emergency Monitoring Service</a></li>
						<li><a href="#">Two-Way contact with our Monitoring Station</a></li>
						<li><a href="#">Emergency response operators</a></li>
						<li><a href="#">GPS satellite tracking technology</a></li>
						<li><a href="#">Closest available emergency responders dispatched to your location</a></li>
						<li><a href="#">AT&T cellular voice and data network with nationwide connection to Emergency</a></li>
						<li><a href="#">60 min talk time</a></li>
						<li><a href="#">Operator will stay on the line until emergency personnel arrive</a></li>
						<li><a href="#">3yr Agreement</a></li>
						<li><a href="#">Geo-fence Technology</a></li>
					</ul>
					<h4 style="color: blue;">$49.95 mo</h4>
				</div>
				<div class="grid_12">
					<img src="/images/WatchOnly_wBubble.png" alt="What is your emergency?" />
					<p style="text-align: right">
					One-time $99.00 Activation<br />
					$399.00 Watch</p>

				</div>
			</div>
		</div>
		<div class="container_24">
			<div class="wrapper">
			<div class="links grid_18 prefix_2" style="color:red; font-size: 36px; line-height: 1.5em; text-align: left;"><strong>Contact dealers for special offers</strong></div>
			<article class="grid_18 prefix_4" style="text-align: left;">
				<%--<h4>Signup Form</h4>--%>
				<h6>Enter your information and a dealer will<br />contact you directly.</h6>

				<form id="enroll-form">
					<input id="sourceId" type="hidden" value="11" />
					<div class="success"> Signup form submitted!<br>We will be contacting you soon.</div>
					<div class="failure"> There was an error please call us.</div>
					<fieldset>
						<label class="name">
							<input type="text" value="Enter Your First Name:" maxlength="50" />
							<span class="error">*This is not a valid first name.</span>
							<span class="empty">*This field is required.</span>
						</label>
						<label class="lastname">
							<input type="text" value="Enter Your Last Name:" maxlength="50" />
							<span class="error">*This is not a valid last name.</span>
							<span class="empty">*This field is required.</span>
						</label>
						<label class="address">
							<input type="text" value="Enter Your Address:" maxlength="50" />
							<span class="error">*This is not a valid address.</span>
							<span class="empty">*The address is required.</span>
						</label>
						<label class="city">
							<input type="text" value="Enter Your City:" maxlength="50" />
							<span class="error">*This is not a valid city.</span>
							<span class="empty">*The city is required.</span>
						</label>
						<label class="state">
							<input type="text" value="Enter Your State:" maxlength="50" />
							<span class="error">*This is not a valid state.</span>
							<span class="empty">*The state is required.</span>
						</label>
						<label class="postal">
							<input type="text" value="Enter Your Postal Code:" maxlength="10" />
							<span class="error">*This is not a valid postal code.</span>
							<span class="empty">*The postal code is required.</span>
						</label>
						<label class="email">
							<input type="text" value="Enter Your E-mail:" maxlength="250" />
							<span class="error">*This is not a valid email address.</span>
							<span class="empty">*This field is required.</span>
						</label>
						<label class="phone">
							<input type="text" value="Enter Your Phone:" maxlength="30" />
							<span class="error">*This is not a valid phone number.</span>
							<span class="empty">*This field is required.</span>
						</label>
						<label class="message">
							<textarea>Enter Your Message:</textarea>
							<span class="error">*The message is too short.</span>
							<span class="empty">*This field is required.</span>
						</label>
						<div class="btns"><a class="button" data-type="reset">Reset</a><a class="button" data-type="submit">Submit</a></div>
					</fieldset>
				</form>
			</article>
			</div>
		</div>
	</section>
</asp:Content>
