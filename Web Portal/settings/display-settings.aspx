<%@ Page Language="C#" AutoEventWireup="true" CodeFile="display-settings.aspx.cs" Inherits="display_settings" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Display Settings</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<!--[if lte IE 8]><script src="../assets/js/ie/html5shiv.js"></script><![endif]-->
		<link rel="stylesheet" href="../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../assets/css/ie8.css" /><![endif]-->

    <script type="text/javascript">
        function loadPage() {
            hideMessage(null);
            hideLineType(null);
        }
        function hideMessage(obj) {
            if (document.getElementById("<%=settingsMessageActive.ClientID%>").checked) {
                document.getElementById("messageDiv").style.display = 'block';
            }
            else {
                document.getElementById("messageDiv").style.display = 'none';
            }
        }

        function hideLineType(obj) {
            if (document.getElementById("<%=settingsRadioSingle.ClientID%>").checked) {
                document.getElementById("singlelineDiv").style.display = 'block';
                document.getElementById("multilineDiv").style.display = 'none';
            } else if (document.getElementById("<%=settingsRadioMulti.ClientID%>").checked) {
                document.getElementById("singlelineDiv").style.display = 'none';
                document.getElementById("multilineDiv").style.display = 'block';
            }
        }

        window.onload = loadPage;

    </script>
</head>
<body>
    <!-- Header -->
			<header id="header">
				<a href="../../default.aspx" class="title">Digital Display Admin</a>
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
						    <h1 class="major">Display Settings</h1>
						    <span class="image fit"><img src="../images/gear-1040.jpg" alt="" /></span>
                            <form method="post" runat="server" name="display-settings">
                                <h2>Images</h2>
                              
                                <div class="field">
                                    <label for="settingsLogo">Logo</label>
                                    <asp:TextBox ID="settingsLogo" runat="server" />
                                </div>

                                <div class="field">
                                    <label for="settings-background">Background Type</label>
                                    <span>
                                        <asp:RadioButton GroupName="settings-background" ID="settingsRadioBgImg" Text="Image" runat="server" Checked="true" />
                                        <asp:RadioButton GroupName="settings-background" ID="settingsRadioBgVid" Text="Video" runat="server" />
                                    </span>
                                </div>

                                <div class="field">
                                    <label for="settingsBgUrl">Background</label>
                                    <asp:TextBox ID="settingsBgUrl" runat="server" />
                                </div>
                              
                                <h2>Password</h2>
                              
                                <div class="field">
                                    <asp:TextBox ID="settingsPassword" TextMode="Number" runat="server" />
                                </div>
                              
                                <h2>Theme</h2>
                              
                                <div class="field">
                                    <label for="settings-theme">Set to dark for backgrounds, light for light backgrounds.</label>
                                    <span>
                                        <asp:RadioButton GroupName="settings-theme" ID="settingsRadioLight" Text="Light" runat="server" Checked="true" />
                                        <asp:RadioButton GroupName="settings-theme" ID="settingsRadioDark" Text="Dark" runat="server" />
                                    </span>
                                </div>
                                
                                <h2>Welcome Message</h2>
                                
                                <asp:CheckBox ID="settingsMessageActive" Text="Active?" Checked="true" runat="server" onClick="hideMessage(this)" />
                                
                                <div id="messageDiv" runat="server">
                                    <div class="field">
                                        <asp:RadioButton GroupName="settingsMessageType" ID="settingsRadioSingle" Text="Single Line" runat="server" Checked="true" onClick="hideLineType(this)" />
                                        <asp:RadioButton GroupName="settingsMessageType" ID="settingsRadioMulti" Text="Multi-Line" runat="server" onClick="hideLineType(this)" />
                                    </div>

                                    <div id="singlelineDiv" runat="server">
                                        <asp:TextBox ID="settingsMessageOneline" runat="server" TextMode="SingleLine" />
                                    </div>
                                    <div id="multilineDiv" runat="server">
                                        <asp:TextBox ID="settingsMessageMultiline" runat="server" TextMode="MultiLine" />
                                    </div>
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
			<script src="../assets/js/jquery.min.js"></script>
			<script src="../assets/js/jquery.scrollex.min.js"></script>
			<script src="../assets/js/jquery.scrolly.min.js"></script>
			<script src="../assets/js/skel.min.js"></script>
			<script src="../assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="../assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="../assets/js/main.js"></script>
</body>
</html>
