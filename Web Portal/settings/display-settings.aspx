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

    function Show_Hide_Display() {
        var div1 = document.getElementById("<%=messagePanel.ClientID%>");
            if (div1.style.display == "" || div1.style.display == "block") {
                div1.style.display = "none";
            }
            else {
                div1.style.display = "block";
            }

        return false;
}    

</script>
</head>
<body>
    <!-- Header -->
			<header id="header">
				<a href="index.html" class="title">Digital Display Admin</a>
				<nav>
					<ul>
						<li><a href="../index.html">Home</a></li>
						<li><a href="../index.html#wiki">Wiki</a></li>
						<li><a href="../index.html#settings"  class="active">Settings</a></li>
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
                              
                              <p>Logo:
                              <input type="text" name="settings-logo" id="settings-logo" value="" placeholder="Logo URL" />
                              </p>
                              
                              <p>Background Type:<br/>
                              <span>
								<input type="radio" id="settings-radio-image" name="settings-background" checked="checked" />
								<label for="settings-radio-image">Image</label>
								<input type="radio" id="settings-radio-video" name="settings-background" />
								<label for="settings-radio-video">Video</label></span></p>
                              
                              <p>Background:
                              <input type="text" name="settings-bgimg" id="settings-bgimg" value="" placeholder="Background URL" />
                              </p>
                              
                              <h2>Password</h2>
                              
                              <p><input type="number"  name="settings-password" id="settings-password" value="" /></p>
                              
                              <h2>Theme</h2>
                              
                              <p>Set to dark for backgrounds, light for light backgrounds.<br/>
                              <span>
								<input type="radio" id="settings-radio-light" name="settings-theme" checked="checked" />
								<label for="settings-radio-light">Light</label>
								<input type="radio" id="settings-radio-dark" name="settings-theme" />
								<label for="settings-radio-dark">Dark</label></span></p>
                                
                                <h2>Welcome Message</h2>
                                
                                <asp:CheckBox ID="settingsMessageActive" Text="Active?" Checked="true" OnCheckedChanged="chkShowHideDiv_CheckedChanged" runat="server" />
                                
                              <div id="myDiv" runat="server">
                                <asp:Panel ID="messagePanel" runat="server">
                                    <asp:RadioButton GroupName="settingsMessageType" ID="settingsRadioSingle" Text="Single Line" runat="server" Checked="true" OnCheckedChanged="settingsRadioSingle_CheckedChanged"  />
                                    <asp:RadioButton GroupName="settingsMessageType" ID="settingsRadioMulti" Text="Multi-Line" runat="server" OnCheckedChanged="settingsRadioMulti_CheckedChanged" />

                                    <asp:TextBox ID="settingsMessageOneline" runat="server" TextMode="SingleLine" />
                                    <asp:TextBox ID="settingsMessageMultiline" runat="server" TextMode="MultiLine" Visible="false" />
                                </asp:Panel>
                              </div>
                                
                                <hr />
                                
                                <p>
                                	<a href="#" class="button special fit">Save</a>
                                </p>
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
