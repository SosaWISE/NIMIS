<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Level1.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Freedom SOS | Why Us
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="newLeadModalDlg2">
		<div class="closeModalDlg"></div>
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
	</div>

	<section id="content">
		<div class="container_24">
			<div class="wrapper">
				<h2>Why Us</h2>
			</div>
		</div>
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_12">
					<img src="/images/sick_person.png" alt="Sick man." style="width: 450px; height: 471px;" />
				</div>
				<div class="grid_12" style="text-align: center;">
					<h4 style="color:red">WHAT WOULD YOU DO?
					<br />
					<br />COULD YOU GET THE HELP YOU
					<br />NEED WHEN AN EMERGENCY
					<br />HAPPENS?</h4>
					<h6>“<span class="links" style="color: black; font-size: 36px; line-height: 1.5em; font-weight: bold;">I enjoy running every morning, but when I
					had a heart attack, I was left by myself with no
						help around.</span>”<br />-Allen, Palm Coast FL</h6>
				</div>
			</div>
		</div>
		<br />
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_8"><img src="/images/whyus_middle_left.png" alt="Why Us" style="width: 310px;" /></div>
				<div class="grid_8">
					<h6>“<span class="links" style="color: black; font-size: 30px; line-height: 1em; font-weight: bold;">My mother had a slip and fall accident. I
						was so worried when I found out she had been
						on the floor for hours before her neighbor
						stopped by to check on her. I had to work and
						can’t always be there for her. My mother has
						always been there for me and I want to do the
						same for her. Now her and I have peace of
						mind with her new Freedom SOS medical alert
						system.</span>”<br /><br />- Sarah , Orlando FL</h6>
				</div>
				<div class="grid_8"><img src="/images/whyus_middle_right.png" alt="Why Us" style="width: 310px; height: 326px;" /></div>
			</div>
		</div>
		<br />
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_12"><img src="/images/whyus_bottom_left.png" alt="Why Us" style="width: 470px; height: 345px;" /></div>
				<div class="grid_12">
					<h6>“<span class="links" style="color: black; font-size: 30px; line-height: 1em; font-weight: bold;">Fishing has always been a passion of
mine. I love going out to my secret spot to
relax and enjoy the day. If I get lucky I
might bring home dinner. A few years ago
my health has slowly kept getting worse
and now I have to stay home most of the
time. I missed having my freedom to go
where I want and enjoy the things I love. I
need to know if I go out anywhere I can get
the help I need, that’s why I have Freedom
SOS.</span>”<br /><br />- Bob, Charleston, SC</h6>
				</div>
			</div>
		</div>
		<br />
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_10"><img src="/images/whyus_page2_left.png" alt="Why Us" style="width: 390px; height: 333px;" /></div>
				<div class="grid_14">
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<h6 style="line-height: 1.7em;">Freedom SOS GPS Alert System can give you and
your family’s freedom back. You have instant twoway
voice communication with our monitoring station.
They can track the unit within 15 feet of the location
giving you peace of mind that help is just a push of a
button away.</h6>
				</div>
			</div>
		</div>
		<br />
		<div class="container_24">
			<div class="wrapper">
				<div class="grid_14" style="text-align: center;">
					<p>&nbsp;</p>
					<p>&nbsp;</p>
					<h2 style="line-height: 1.7em;color:blue;">SIGN UP FOR A LITTLE OVER
A DOLLAR A DAY.</h2>
				</div>
				<div class="grid_10" style="text-align: center; padding-top: 46px;"><a href="/Plans"><img src="/images/red-button-signup-sm.jpg" alt="Why Us" style="width: 150px; height: 139px; border: solid 0 transparent" /></a></div>
			</div>
		</div>

	</section>
</asp:Content>
