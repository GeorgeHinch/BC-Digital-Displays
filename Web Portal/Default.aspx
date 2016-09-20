<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<!--[if lte IE 8]><script src="assets/js/ie/html5shiv.js"></script><![endif]-->
		<link rel="stylesheet" href="assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="assets/css/ie8.css" /><![endif]-->
</head>
<body>
    <!-- Sidebar -->
			<section id="sidebar">
				<div class="inner">
					<nav>
						<ul>
							<li><a href="#intro">Welcome</a></li>
							<li><a href="#wiki">Wiki</a></li>
							<li><a href="#settings">Settings</a></li>
							<li><a href="#contact">Get in touch</a></li>
						</ul>
					</nav>
				</div>
			</section>

		<!-- Wrapper -->
			<div id="wrapper">

				<!-- Intro -->
					<section id="intro" class="wrapper style1 fullscreen fade-up">
						<div class="inner">
						  <h1>Digital Display Admin</h1>
						  <p>The Bellevue Club Digital Display Admin site houses the management component to the servie as well as the <a href="">user guides</a>.</p>
						  <ul class="actions">
							<li><a href="#one" class="button scrolly">Learn more</a></li>
							</ul>
						</div>
					</section>

				<!-- One -->
					<section id="wiki" class="wrapper style2 spotlights">
						<section>
							<a href="#" class="image"><img src="images/cal-438.jpg" alt="" data-position="center center" /></a>
							<div class="content">
								<div class="inner">
								  <h2>Using The Calendar</h2>
								  <p>Phasellus convallis elit id ullamcorper pulvinar. Duis aliquam turpis mauris, eu ultricies erat malesuada quis. Aliquam dapibus.</p>
									<ul class="actions">
										<li><a href="#" class="button">Learn more</a></li>
									</ul>
								</div>
							</div>
						</section>
						<section>
							<a href="#" class="image"><img src="images/gear-438.jpg" alt="" data-position="top center" /></a>
							<div class="content">
								<div class="inner">
								  <h2>What's In the Settings</h2>
								  <p>Phasellus convallis elit id ullamcorper pulvinar. Duis aliquam turpis mauris, eu ultricies erat malesuada quis. Aliquam dapibus.</p>
									<ul class="actions">
										<li><a href="#" class="button">Learn more</a></li>
									</ul>
								</div>
							</div>
						</section>
						<section>
							<a href="#" class="image"><img src="images/trainer-438.jpg" alt="" data-position="25% 25%" /></a>
							<div class="content">
								<div class="inner">
								  <h2>Adding Trainers</h2>
								  <p>Phasellus convallis elit id ullamcorper pulvinar. Duis aliquam turpis mauris, eu ultricies erat malesuada quis. Aliquam dapibus.</p>
									<ul class="actions">
										<li><a href="#" class="button">Learn more</a></li>
									</ul>
								</div>
							</div>
						</section>
					</section>

				<!-- Two -->
					<section id="settings" class="wrapper style3 fade-up">
						<div class="inner">
						  <h2>Make Some Changes</h2>
						  <p>Phasellus convallis elit id ullamcorper pulvinar. Duis aliquam turpis mauris, eu ultricies erat malesuada quis. Aliquam dapibus, lacus eget hendrerit bibendum, urna est aliquam sem, sit amet imperdiet est velit quis lorem.</p>
							<div class="features">
								<section><a href="settings/menu-manager.aspx">
									<span class="icon major fa-list"></span>
									<h3>Menu Manager</h3>
									<p>Phasellus convallis elit id ullam corper amet et pulvinar. Duis aliquam turpis mauris, sed ultricies erat dapibus.</p>
								</a></section>
								<section><a href="settings/trainer-manager.aspx">
									<span class="icon major fa-user"></span>
								  <h3>Trainer Manager</h3>
									<p>Phasellus convallis elit id ullam corper amet et pulvinar. Duis aliquam turpis mauris, sed ultricies erat dapibus.</p>
								</a></section>
								<section><a href="settings/equipment-manager.aspx">
									<span class="icon major fa-server"></span>
									<h3>Equipment Manager</h3>
									<p>Phasellus convallis elit id ullam corper amet et pulvinar. Duis aliquam turpis mauris, sed ultricies erat dapibus.</p>
								</a></section>
								<section><a href="settings/calendar-manager.aspx">
									<span class="icon major fa-calendar"></span>
									<h3>Calendar Manager</h3>
									<p>Phasellus convallis elit id ullam corper amet et pulvinar. Duis aliquam turpis mauris, sed ultricies erat dapibus.</p>
								</a></section>
								<section><a href="settings/display-settings.aspx">
									<span class="icon major fa-cog"></span>
								  <h3>Display Settings</h3>
									<p>Phasellus convallis elit id ullam corper amet et pulvinar. Duis aliquam turpis mauris, sed ultricies erat dapibus.</p>
								</a></section>
                                <section class="hide">
								</section>
							</div>
						</div>
					</section>

				<!-- Three -->
					<section id="contact" class="wrapper style1 fade-up">
						<div class="inner">
							<h2>Get in touch</h2>
							<p>Something break? Questions? Feature requests?<br/> Feel free to contact me and I'll let you know what I can do. </p>
							<div class="split style1">
								<section>
									<form method="post" runat="server">
										<div class="field half first">
											<label for="emailName">Name</label>
                                            <asp:TextBox ID="emailName" runat="server" />
										</div>
										<div class="field half">
											<label for="emailEmail">Email</label>
											<asp:TextBox ID="emailEmail" TextMode="Email" runat="server" />
										</div>
										<div class="field">
											<label for="emailMessage">Message</label>
                                            <asp:TextBox ID="emailMessage" TextMode="MultiLine" Rows="5" runat="server" />
										</div>

                                        <asp:Button ID="SendEmail" CssClass="button submit" Text="Send Message" runat="server" OnClick="FormSubmit_Click" />
									</form>
								</section>
								<section>
									<ul class="contact">
										<li>
											<h3>Email</h3>
											<a href="mailto:georgeh@bellevueclub.com">georgeh@bellevueclub.com</a><br/>
                                            <a href="mailto:george@georgehinch.com">george@georgehinch.com</a>
										</li>
										<li>
											<h3>Phone</h3>
											<span>(425) 688-3161</span>
										</li>
										<li>
											<h3>Social</h3>
											<ul class="icons">
												<li><a href="http://www.twitter.com/geohinch" class="fa-twitter"><span class="label">Twitter</span></a></li>
												<li><a href="https://github.com/GeorgeHinch" class="fa-github"><span class="label">GitHub</span></a></li>
                                                <li><a href="http://georgehinch.com" class="fa-chrome"><span class="label">GitHub</span></a></li>
											</ul>
										</li>
									</ul>
								</section>
							</div>
						</div>
					</section>

			</div>

		<!-- Footer -->
			<footer id="footer" class="wrapper style1-alt">
				<div class="inner">
					<ul class="menu">
						<li>&copy; Bellevue Club. All rights reserved.</li><li>Contact: <a href="http://www.georgehinch.com">George Hinchliffe</a></li>
					</ul>
				</div>
			</footer>

		<!-- Scripts -->
			<script src="assets/js/jquery.min.js"></script>
			<script src="assets/js/jquery.scrollex.min.js"></script>
			<script src="assets/js/jquery.scrolly.min.js"></script>
			<script src="assets/js/skel.min.js"></script>
			<script src="assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="assets/js/main.js"></script>
</body>
</html>
