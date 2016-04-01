<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-trainer.aspx.cs" Inherits="settings_add_add_trainer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Add Trainer</title>
    <meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	    <!--[if lte IE 8]><script src="../assets/js/ie/html5shiv.js"></script><![endif]-->
	<link rel="stylesheet" href="../../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../assets/css/ie8.css" /><![endif]-->
</head>
<body>
    <!-- Header -->
			<header id="header">
				<a href="index.html" class="title">Digital Display Admin</a>
				<nav>
					<ul>
						<li><a href="../../index.html">Home</a></li>
						<li><a href="../../index.html#wiki">Wiki</a></li>
						<li><a href="../../index.html#settings"  class="active">Settings</a></li>
					</ul>
				</nav>
			</header>

		<!-- Wrapper -->
			<div id="wrapper">

				<!-- Main -->
					<section id="main" class="wrapper">
						<div class="inner">
						    <h1 class="major">Add Trainer</h1>
                            <asp:Button ID="Button1" Text="Save" CssClass="button special fit" runat="server" OnClick="FormSubmit_Click" />
                            <form method="post" runat="server" name="add-trainer">
                                    <p>Name:
                                        <asp:TextBox ID="trainerName" runat="server" />
                                    </p>
                              
                                    <p>Degree:
                                        <asp:TextBox ID="trainerDegree" runat="server" />
                                    </p>
                              
                                    <p>Years Training:
                                        <asp:TextBox ID="trainerYears" runat="server" />
                                    </p>
                              
                                    <p>Years Training at Bellevue Club:
                                        <asp:TextBox ID="trainerYearsBC" runat="server" />
                                    </p>
                              
                                    <p>Areas of Expertise:
                                        <asp:TextBox ID="trainerExpertise" runat="server" TextMode="MultiLine" Rows="2" />
                                    </p>
                              
                                    <p>Reward:
                                        <asp:TextBox ID="trainerReward" runat="server" TextMode="MultiLine" Rows="3" />
                                    </p>
                              
                                    <p>What to Expect Out of a Training Session:
                                        <asp:TextBox ID="trainerSession" runat="server" TextMode="MultiLine" Rows="3" />
                                    </p>
                              
                                    <p>What is Your Biggest Athletic Accomplishment:
                                        <asp:TextBox ID="trainerAccomplishment" runat="server" TextMode="MultiLine" Rows="3" />
                                    </p>
                              
                                    <p>Photo URL:
                                        <asp:TextBox ID="trainerPhoto" runat="server" />
                                    </p>
                                
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
			<script src="../assets/js/jquery.min.js"></script>
			<script src="../assets/js/jquery.scrollex.min.js"></script>
			<script src="../assets/js/jquery.scrolly.min.js"></script>
			<script src="../assets/js/skel.min.js"></script>
			<script src="../assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="../assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="../assets/js/main.js"></script>
</body>
</html>
