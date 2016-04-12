<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-menu.aspx.cs" Inherits="settings_add_add_menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Add Menu</title>
    <meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	    <!--[if lte IE 8]><script src="../../assets/js/ie/html5shiv.js"></script><![endif]-->
	<link rel="stylesheet" href="../../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../../assets/css/ie8.css" /><![endif]-->
</head>
<body>
    <!-- Header -->
			<header id="header">
				<a href="../../default.aspx" class="title">Digital Display Admin - Add Event</a>
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
                                <a href="../calendar-manager.aspx" class="button small icon fa-angle-left">return to events</a>
                            </p>

                            <form method="post" runat="server" name="add-trainer">
                                <div class="field">
                                    <label for="eventSubject">Name</label>
                                    <asp:TextBox ID="eventSubject" runat="server" />
                                </div>

                                <div class="field">
                                    <label for="eventLocation">Location</label>
                                    <asp:TextBox ID="eventLocation" runat="server" />
                                </div>

                                <div class="field half first">
                                    <label for="eventAllDay">All Day</label>
                                    <asp:CheckBox ID="eventAllDay" Text="All Day" runat="server" />
                                </div>

                                <div class="field half">
                                    <label for="eventPrice">Price</label>
                                    <asp:TextBox ID="eventPrice" runat="server" />
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
