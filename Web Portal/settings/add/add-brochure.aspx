<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-brochure.aspx.cs" Inherits="settings_add_add_brochure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Add Brochure</title>
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
				<a href="../../default.aspx" class="title">Digital Display Admin - Add Brochure</a>
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
                                <a href="../youth-manager.aspx" class="button small icon fa-angle-left">return to brochures</a>
                            </p>

                            <form method="post" runat="server" name="add-trainer">
                                <div class="field mainField">
                                    <label for="brochurerName">Name</label>
                                    <asp:TextBox ID="brochureName" runat="server" />
                                </div>

                                <div class="field spaceBelow">
                                    <label for="session1Name">Session Name</label>
                                    <asp:TextBox ID="session1Name" runat="server" />
                                    <div class="field half first">
                                        <label for="session1StartDate">Start Date</label>
                                        <asp:TextBox ID="session1StartDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <label for="session1EndDate">End Date</label>
                                        <asp:TextBox ID="session1EndDate" TextMode="Date" runat="server" />
                                    </div>
                                </div>

                                <div class="field spaceBelow">
                                    <label for="session2Name">Session Name</label>
                                    <asp:TextBox ID="session2Name" runat="server" />
                                    <div class="field half first">
                                        <label for="session2StartDate">Start Date</label>
                                        <asp:TextBox ID="session2StartDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <label for="session2EndDate">End Date</label>
                                        <asp:TextBox ID="session2EndDate" TextMode="Date" runat="server" />
                                    </div>
                                </div>

                                <div class="field spaceBelow">
                                    <label for="session3Name">Session Name</label>
                                    <asp:TextBox ID="session3Name" runat="server" />
                                    <div class="field half first">
                                        <label for="session3StartDate">Start Date</label>
                                        <asp:TextBox ID="session3StartDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <label for="session3EndDate">End Date</label>
                                        <asp:TextBox ID="session3EndDate" TextMode="Date" runat="server" />
                                    </div>
                                </div>

                                <div class="field spaceBelow">
                                    <label for="session4Name">Session Name</label>
                                    <asp:TextBox ID="session4Name" runat="server" />
                                    <div class="field half first">
                                        <label for="session4StartDate">Start Date</label>
                                        <asp:TextBox ID="session4StartDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <label for="session4EndDate">End Date</label>
                                        <asp:TextBox ID="session4EndDate" TextMode="Date" runat="server" />
                                    </div>
                                </div>

                                <div class="field spaceBelow">
                                    <label for="session5Name">Session Name</label>
                                    <asp:TextBox ID="session5Name" runat="server" />
                                    <div class="field half first">
                                        <label for="session5StartDate">Start Date</label>
                                        <asp:TextBox ID="session5StartDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <label for="session5EndDate">End Date</label>
                                        <asp:TextBox ID="session5EndDate" TextMode="Date" runat="server" />
                                    </div>
                                </div>
                                
                                <hr style="padding-bottom:10px;" />
                                
                                <asp:Button ID="SaveForm" Text="Save" CssClass="button special fit" runat="server" OnClick="FormSubmit_Click"/>
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
