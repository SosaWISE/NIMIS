<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Level1.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Freedom SOS | Careers
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="newDealerModalDlg">
		<div class="closeModalDlg"></div>
		<div class="downloadModalDlg">
			<p>Click the icon below to initiate the download process.</p>
			<p style="text-align: center;">
				<br />
				<a id="downloadBrochureAnchor" href="/DownloadBrochure.ashx"><img src="/images/icon-download.png" width="50" height="50" style="border-width: 0" alt="Download brochure." /></a>
				<h4 style="text-align: center;">Click Here to Download</h4>
			</p>
		</div>
		<h4>Download Brochure</h4>
		<h6>Please provide the following to download a brochure</h6>
		<br />
		<br />
		<form id="careers-form" style="width: 437px;">
			<input id="dealerTypeId" type="hidden" />
			<div class="success"> Thank you! &nbsp;Click on the green button to download. --></div>
			<div class="failure"> There was an error please call us.</div>
			<fieldset>
				<label class="name">
					<input type="text" value="First Name:" />
					<span class="error">*This is not a valid name.</span>
					<span class="empty">*This field is required.</span>
				</label>
				<label class="lastname">
					<input type="text" value="Last Name:" />
					<span class="error">*This is not a valid last name.</span>
					<span class="empty">*This field is required.</span>
				</label>
				<label class="email">
					<input type="text" value="E-mail:" />
					<span class="error">*This is not a valid email address.</span>
					<span class="empty">*This field is required.</span>
				</label>
				<label class="phone">
					<input type="text" value="Phone:" />
					<span class="error">*This is not a valid phone number.</span>
					<span class="empty">*This field is required.</span>
				</label>
<%--				<label class="address">
					<input type="text" value="Street:" />
					<span class="error">*This is not a valid street.</span>
					<span class="empty">*This field is required.</span>
				</label>--%>
				<label class="city">
					<input type="text" value="City:" />
					<span class="error">*This is not a valid city.</span>
					<span class="empty">*This field is required.</span>
				</label>
				<label class="state">
					<input type="text" value="State:" />
					<span class="error">*This is not a valid state.</span>
					<span class="empty">*This field is required.</span>
				</label>
				<label class="postal">
					<input type="text" value="Postal Code:" />
					<span class="error">*This is not a valid postal code.</span>
					<span class="empty">*This field is required.</span>
				</label>
<%--				<label class="message">
					<textarea>Enter Your Message:</textarea>
					<span class="error">*The message is too short.</span>
					<span class="empty">*This field is required.</span>
				</label>--%>
				<div class="btns"><a class="button" data-type="reset">Reset</a><a class="button" data-type="submit">Submit</a></div>
			</fieldset>
		</form>
	</div>

	<section id="content">
		<div class="container_24">
<%--			<div class="wrapper">
				<h4>Careers</h4>
			</div>--%>
		</div>
		<div class="container_24">
			<div class="wrapper">
				<div class="wrapper careerTitulo"></div>
				<div class="grid_21">
					<br />
					<div class="grid_1" style="float: right;">
						<img src="/images/job_opps_ladder.png" alt="Job Opps" style="width: 160px;" />
					</div>
					<h4 style="margin-bottom: 10px;">Are you looking for a brand new career?</h4>
					<p>How about one where you really can make a difference
						in someone’s life. By joining our team
						you will be providing state of the art safety and
						security to people who really need it. You will be
						able to earn a great income and have the financial
						freedom you deserve.</p>
					<br />
					<br />
					<h4 style="margin-bottom: 10px;">Would you like to have your own dealership?</h4>
					<p>
						We have a complete turn-key dealership package
					along with constant weekly training and marketing
					support. Our goal is to make you successful in this
					exciting industry. Your success is ours.
					</p>
				</div>
			</div>
		</div>
		<br />
		<br />
		<div class="container_24">
			<div class="wrapper">
			<div id="linkSalesBrochure" class="grid_12" style="text-align: center"><img class="clickHereDownload" src="/images/ClickHere-SalesInformation.png" /></div>
			<div id="linkDealrBrochure" class="grid_12" style="text-align: center"><img class="clickHereDownload" src="/images/ClickHere-DealerInformation.png" /></div>
			</div>
		</div>
	</section>
</asp:Content>
