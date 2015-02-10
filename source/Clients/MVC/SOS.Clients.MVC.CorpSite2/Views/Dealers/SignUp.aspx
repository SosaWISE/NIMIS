<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Level1.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Freedom SOS | Dealer Signup</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<section id="content">
    <div class="container_24">
        <div class="wrapper">
        	<article class="grid_16">
            	<h4>Stay In Touch</h4>
                <iframe id="map_canvas" src="http://maps.google.com/maps?f=q&amp;source=s_q&amp;hl=en&amp;geocode=&amp;q=Brooklyn,+New+York,+NY,+United+States&amp;aq=0&amp;sll=37.0625,-95.677068&amp;sspn=61.282355,146.513672&amp;ie=UTF8&amp;hq=&amp;hnear=Brooklyn,+Kings,+New+York&amp;ll=40.649974,-73.950005&amp;spn=0.01628,0.025663&amp;z=14&amp;iwloc=A&amp;output=embed"></iframe>
                <div class="extra-wrap">
                    Working hours:<br>
                    Sun-Thurs 9:30 am - 9:30 pm
                    <p>Fri-Sat 10:30 am - 8:30 pm</p>
                </div>
                <ul class="icons">
                	<li><img src="/images/phone.png" alt=""><span class="">Phone: +1 888-642-5277</span></li>
                	<li><img src="/images/mail.png" alt=""><a href="mailto:ContactUs@FreedomSOS.com">contactus@freedomsos.com</a></li>
                	<li><img src="/images/skype.png" alt=""><a href="callto:Freedom.SOS">Freedom.SOS</a></li>
                </ul>
            </article>
            <article class="grid_8">
                <h4>Contact Form</h4>
                <form id="contact-form">
                    <div class="success"> Contact form submitted!<br>We will be in touch soon.</div>
                    <div class="failure"> There was an error please call us.</div>
                    <fieldset>
                        <label class="name">
                            <input type="text" value="Enter Your Name:">
                            <span class="error">*This is not a valid name.</span>
                            <span class="empty">*This field is required.</span>
                        </label>
                        <label class="email">
                            <input type="text" value="Enter Your E-mail:">
                            <span class="error">*This is not a valid email address.</span>
                            <span class="empty">*This field is required.</span>
                        </label>
                        <label class="phone">
                            <input type="text" value="Enter Your Phone:">
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
