<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-notification.aspx.cs" Inherits="settings_add_add_notification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Add Notification</title>
    <meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	    <!--[if lte IE 8]><script src="../../assets/js/ie/html5shiv.js"></script><![endif]-->
	<link rel="stylesheet" href="../../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../../assets/css/ie8.css" /><![endif]-->
    <style type="text/css">
        	@font-face {
	   	        font-family: 'SegoeMDL2';
	  	        src: url("../../assets/fonts/segmdl2-webfont.ttf");
	  	        font-weight: normal;
	  	        font-style: normal;
	        }

            .mi{
                font-family:'SegoeMDL2';
            }
    </style>
</head>
<body>
    <!-- Header -->
			<header id="header">
				<a href="../../default.aspx" class="title">Digital Display Admin</a>
				<nav>
					<ul>
						<li><a href="../../default.aspx">Home</a></li>
						<li><a href="../../default.aspx#wiki">Wiki</a></li>
						<li><a href="../../default.aspx#settings"  class="active">Settings</a></li>
					</ul>
				</nav>
			</header>

		<!-- Wrapper -->
			<div id="wrapper">

				<!-- Main -->
					<section id="main" class="wrapper">
						<div class="inner">
						    <asp:literal ID="pageH1" runat="server" />
                            <p>
                                <a href="../notification-manager.aspx" class="button small icon fa-angle-left">return to notifications</a>
                            </p>

                            <form method="post" runat="server" name="add-notification">
                                <div class="field">
                                    <label for="notificationSubject">Subject</label>
                                    <asp:TextBox ID="notificationSubject" runat="server" />
                                </div>

                                <div class="field half first">
                                    <label for="notificationGlyph">Glyph</label>
                                    <asp:DropDownList ID="notificationGlyph" CssClass="mi" runat="server">
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                        <asp:ListItem Text="" Value="" />
                                    </asp:DropDownList>
                                </div>
                              
                                <div class="field half">
                                    <div class="field half first">
                                        <label for="notificationStartDate">Start Date</label>
                                        <asp:TextBox ID="notificationStartDate" TextMode="Date" runat="server" />
                                    </div>

                                    <div class="field half">
                                        <label for="notificationEndDate">End Date</label>
                                        <asp:TextBox ID="notificationEndDate" TextMode="Date" runat="server" />
                                    </div>
                                </div>
                                
                                <div class="field">
                                    <label for="notificationMessage">Message</label>
                                    <p>The notification message field supports a subset of the Github's Markdown language. View the <a href="../../wiki/notification.aspx" target="_blank">Notification Wiki</a> to learn more.
                                    <br />Quick copy: __, **, ···, ··*</p>
                                    <asp:TextBox ID="notificationMessage" runat="server" TextMode="MultiLine" Rows="7" />
                                </div>
                                
                                <hr style="padding-bottom:10px;" />
                                
                                <asp:Button ID="SaveForm" Text="Save" CssClass="button special fit" runat="server" OnClick="FormSubmit_Click" />
                              </form>
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
			<script src="../../assets/js/jquery.min.js"></script>
			<script src="../../assets/js/jquery.scrollex.min.js"></script>
			<script src="../../assets/js/jquery.scrolly.min.js"></script>
			<script src="../../assets/js/skel.min.js"></script>
			<script src="../../assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="../../assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="../../assets/js/main.js"></script>
</body>
</html>
