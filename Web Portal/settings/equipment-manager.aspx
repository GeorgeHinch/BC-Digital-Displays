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

    <script type="text/javascript">
        function studio1Btn(obj) {
            document.getElementById("s1Div").style.display = 'block';
            document.getElementById("btn1").className = "button special fit";
            document.getElementById("s2Div").style.display = 'none';
            document.getElementById("btn2").className = "button fit";
            document.getElementById("s3Div").style.display = 'none';
            document.getElementById("btn3").className = "button fit";
            document.getElementById("s4Div").style.display = 'none';
            document.getElementById("btn4").className = "button fit";
        }
        function studio2Btn(obj) {
            document.getElementById("s1Div").style.display = 'none';
            document.getElementById("btn1").className = "button fit";
            document.getElementById("s2Div").style.display = 'block';
            document.getElementById("btn2").className = "button special fit";
            document.getElementById("s3Div").style.display = 'none';
            document.getElementById("btn3").className = "button fit";
            document.getElementById("s4Div").style.display = 'none';
            document.getElementById("btn4").className = "button fit";
        }
        function studio3Btn(obj) {
            document.getElementById("s1Div").style.display = 'none';
            document.getElementById("btn1").className = "button fit";
            document.getElementById("s2Div").style.display = 'none';
            document.getElementById("btn2").className = "button fit";
            document.getElementById("s3Div").style.display = 'block';
            document.getElementById("btn3").className = "button special fit";
            document.getElementById("s4Div").style.display = 'none';
            document.getElementById("btn4").className = "button fit";
        }
        function studio4Btn(obj) {
            document.getElementById("s1Div").style.display = 'none';
            document.getElementById("btn1").className = "button fit";
            document.getElementById("s2Div").style.display = 'none';
            document.getElementById("btn2").className = "button fit";
            document.getElementById("s3Div").style.display = 'none';
            document.getElementById("btn3").className = "button fit";
            document.getElementById("s4Div").style.display = 'block';
            document.getElementById("btn4").className = "button special fit";
        }
        function hideAll(obj) {
            document.getElementById("s1Div").style.display = 'none';
            document.getElementById("btn1").className = "button fit";
            document.getElementById("s2Div").style.display = 'none';
            document.getElementById("btn2").className = "button fit";
            document.getElementById("s3Div").style.display = 'none';
            document.getElementById("btn3").className = "button fit";
            document.getElementById("s4Div").style.display = 'none';
            document.getElementById("btn4").className = "button fit";
        }
    </script>
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
                            	<li><a href="#" id="btn1" class="button fit" onClick="studio1Btn(this)">Studio 1</a></li>
                                <li><a href="#" id="btn2" class="button fit" onClick="studio2Btn(this)">Studio 2</a></li>
                                <li><a href="#" id="btn3" class="button fit" onClick="studio3Btn(this)">Studio 3</a></li>
                                <li><a href="#" id="btn4" class="button fit" onClick="studio4Btn(this)">Studio 4</a></li>
                            </ul>
                            <form method="post" runat="server" name="add-equipment">
                                <div id="s1Div" runat="server" style="display: none;">
                                    <div class="field">
                                        <label for="studio1Tb">Add to Studio 1</label>
                                        <asp:TextBox ID="studio1Tb" runat="server" />
                                    </div>
                                    <asp:Button ID="SaveForm1" Text="Save" CssClass="button special fit" runat="server" OnClientClick="hideAll()" OnClick="FormSubmit_Click" />
                                </div>
                                <div id="s2Div" runat="server" style="display: none;">
                                    <div class="field">
                                        <label for="studio2Tb">Add to Studio 2</label>
                                        <asp:TextBox ID="studio2Tb" runat="server" />
                                    </div>
                                    <asp:Button ID="SaveForm2" Text="Save" CssClass="button special fit" runat="server" OnClientClick="hideAll()" OnClick="FormSubmit_Click" />
                                </div>
                                <div id="s3Div" runat="server" style="display: none;">
                                    <div class="field">
                                        <label for="studio3Tb">Add to Studio 3</label>
                                        <asp:TextBox ID="studio3Tb" runat="server" />
                                    </div>
                                    <asp:Button ID="SaveForm3" Text="Save" CssClass="button special fit" runat="server" OnClientClick="hideAll()" OnClick="FormSubmit_Click" />
                                </div>
                                <div id="s4Div" runat="server" style="display: none;">
                                    <div class="field">
                                        <label for="studio4Tb">Add to Studio 4</label>
                                        <asp:TextBox ID="studio4Tb" runat="server" />
                                    </div>
                                    <asp:Button ID="SaveForm4" Text="Save" CssClass="button special fit" runat="server" OnClientClick="hideAll()" OnClick="FormSubmit_Click" />
                                </div>
                            </form>
                            
                          
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
