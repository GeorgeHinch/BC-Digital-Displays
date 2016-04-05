<%@ Page Language="C#" AutoEventWireup="true" CodeFile="equipment-manager.aspx.cs" Inherits="settings_equipment_manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Equipment Manager</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<!--[if lte IE 8]><script src="../assets/js/ie/html5shiv.js"></script><![endif]-->
		<link rel="stylesheet" href="../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../assets/css/ie8.css" /><![endif]-->
</head>
<body>
    <!-- Header -->
			<header id="header">
				<a href="../default.aspx" class="title">Digital Display Admin</a>
				<nav>
					<ul>
						<li><a href="../default.aspx">Home</a></li>
						<li><a href="../default.aspx#wiki">Wiki</a></li>
						<li><a href="../default.aspx#settings"  class="active">Settings</a></li>
					</ul>
				</nav>
			</header>

    		<!-- Wrapper -->
			<div id="wrapper">

				<!-- Main -->
					<section id="main" class="wrapper">
						<div class="inner">
						    <h1 class="major">Equipment Manager</h1>
						    <span class="image fit"><img src="../images/equip-1040.jpg" alt="" /></span>
						    <h2>Add Equipment to Studio:</h2>
                       	  
                            <ul class="actions fit">
                            	<li><a href="#" class="button fit">Studio 1</a></li>
                                <li><a href="#" class="button fit">Studio 2</a></li>
                                <li><a href="#" class="button fit">Studio 3</a></li>
                                <li><a href="#" class="button fit">Studio 4</a></li>
                            </ul>
                          
                          <hr />
                          
						  <h2>Studio 1</h2>
						  <div class="table-wrapper">
                              <asp:Literal ID="studio1HTMLTable" runat="server" />
						  </div>
                          
                          <h2>Studio 2</h2>
						  <div class="table-wrapper">
								<asp:Literal ID="studio2HTMLTable" runat="server" />
						  </div>
                          
                          <h2>Studio 3</h2>
						  <div class="table-wrapper">
								<asp:Literal ID="studio3HTMLTable" runat="server" />
						  </div>
                          
                          <h2>Studio 4</h2>
						  <div class="table-wrapper">
								<asp:Literal ID="studio4HTMLTable" runat="server" />
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
			<script src="../assets/js/jquery.min.js"></script>
			<script src="../assets/js/jquery.scrollex.min.js"></script>
			<script src="../assets/js/jquery.scrolly.min.js"></script>
			<script src="../assets/js/skel.min.js"></script>
			<script src="../assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="../assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="../assets/js/main.js"></script>

</body>
</html>
