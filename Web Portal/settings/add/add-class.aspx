<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-class.aspx.cs" Inherits="settings_add_add_class" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Add Class</title>
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
				<a href="../../default.aspx" class="title">Digital Display Admin - Add Class</a>
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
                                <asp:literal ID="returnToClasses" runat="server" />
                            </p>

                            <form method="post" runat="server" name="add-trainer">
                                <div class="field">
                                    <label for="className">Name</label>
                                    <asp:TextBox ID="className" runat="server" />
                                </div>

                                <div class="field half first"> 
                                    <label for="classAgeRange">Age Range</label>
                                    <asp:TextBox ID="classAgeRange" runat="server" />
                                </div>   
                              
                                <div class="field half">
                                    <div class="field half first"> 
                                        <label for="classAgeMin">Minimum Age</label>
                                        <asp:TextBox ID="classAgeMin" TextMode="Number" runat="server" />
                                    </div> 

                                    <div class="field half">
                                        <label for="classAgeMax">Max Age</label>
                                        <asp:TextBox ID="classAgeMax" TextMode="Number" runat="server" />
                                    </div>
                                </div>
                              
                                <div class="field">
                                    <label for="classMonday">Class Days</label>
                                    <span>
                                        <asp:CheckBox ID="classMonday" Text="Monday" runat="server" />
                                        <asp:CheckBox ID="classTuesday" Text="Tuesday" runat="server" />
                                        <asp:CheckBox ID="classWednesday" Text="Wednesday" runat="server" />
                                        <asp:CheckBox ID="classThursday" Text="Thursday" runat="server" />
                                        <asp:CheckBox ID="classFriday" Text="Friday" runat="server" />
                                        <asp:CheckBox ID="classSaturday" Text="Saturday" runat="server" />
                                        <asp:CheckBox ID="classSunday" Text="Sunday" runat="server" />
                                    </span>
                                </div>

                                <div class="field half first">
                                    <div class="field half first"> 
                                        <label for="classTime">Start Time</label>
                                        <asp:TextBox ID="classStartTime" TextMode="Time" runat="server" />
                                    </div>

                                    <div class="field half"> 
                                        <label for="classTime">End Time</label>
                                        <asp:TextBox ID="classEndTime" TextMode="Time" runat="server" />
                                    </div>
                                </div>   
                              
                                <div class="field half">
                                    <div class="field half first">
                                        <label for="classLocations">Location</label>
                                        <asp:TextBox ID="classLocation" runat="server" />
                                    </div>

                                    <div class="field half">
                                        <label for="classCategory">Class Category</label>
                                        <asp:DropDownList ID="classCategory" runat="server" >
                                            <asp:ListItem Text="Select Category" Value="0" />
                                            <asp:ListItem Text="Family Events" Value="1" />
                                            <asp:ListItem Text="School Breaks" Value="2" />
                                            <asp:ListItem Text="Recreation" Value="3" />
                                            <asp:ListItem Text="Tennis" Value="4" />
                                            <asp:ListItem Text="Swim" Value="5" />
                                            <asp:ListItem Text="Basketball" Value="6" />
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="field">
                                    <label for="classSession1">Class Sessions</label>
                                    <span>
                                        <asp:CheckBox ID="classSession1" Text="Session 1" runat="server" />
                                        <asp:CheckBox ID="classSession2" Text="Session 2" runat="server" />
                                        <asp:CheckBox ID="classSession3" Text="Session 3" runat="server" />
                                        <asp:CheckBox ID="classSession4" Text="Session 4" runat="server" />
                                        <asp:CheckBox ID="classSession5" Text="Session 5" runat="server" />
                                    </span>
                                </div>

                                <div class="field">
                                    <label for="classDescription">Description</label>
                                    <asp:TextBox ID="classDescription" TextMode="MultiLine" runat="server" />
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
