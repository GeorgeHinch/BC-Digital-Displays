﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-class.aspx.cs" Inherits="settings_add_add_class" %>

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
                                    <label for="classMonday">Monday</label>
                                    <asp:CheckBox ID="classMonday" Text="Monday" runat="server" />

                                    <label for="classTuesday">Tuesday</label>
                                    <asp:CheckBox ID="classTuesday" Text="Tuesday" runat="server" />

                                    <label for="classWednesday">Wednesday</label>
                                    <asp:CheckBox ID="classWednesday" Text="Wednesday" runat="server" />

                                    <label for="classThursday">Thursday</label>
                                    <asp:CheckBox ID="classThursday" Text="Thursday" runat="server" />

                                    <label for="classFriday">Friday</label>
                                    <asp:CheckBox ID="classFriday" Text="Friday" runat="server" />

                                    <label for="classSaturday">Saturday</label>
                                    <asp:CheckBox ID="classSaturday" Text="Saturday" runat="server" />

                                    <label for="classSunday">Sunday</label>
                                    <asp:CheckBox ID="classSunday" Text="Sunday" runat="server" />
                                </div>

                                <div class="field half first"> 
                                    <label for="classTime">Time</label>
                                    <asp:TextBox ID="classTime" TextMode="Time" runat="server" />
                                </div>   
                              
                                <div class="field half">
                                    <label for="classLocations">Location</label>
                                    <asp:TextBox ID="classLocation" runat="server" />
                                </div>

                                <div class="field">
                                    <label for="trainerReward">Reward</label>
                                    <asp:TextBox ID="trainerReward" runat="server" TextMode="MultiLine" Rows="3" />
                                </div>
                              
                                <div class="field">
                                    <label for="trainerSession">What to Expect Out of a Training Session</label>
                                    <asp:TextBox ID="trainerSession" runat="server" TextMode="MultiLine" Rows="3" />
                                </div>

                                <div class="field">
                                    <label for="trainerAccomplishment">What is Your Biggest Athletic Accomplishment</label>
                                    <asp:TextBox ID="trainerAccomplishment" runat="server" TextMode="MultiLine" Rows="3" />
                                </div>

                                <div class="field">
                                    <label for="trainerPhoto">Photo URL</label>
                                    <asp:TextBox ID="trainerPhoto" runat="server" />
                                </div>

                                <div class="field">
                                    <label for="trainerReflections">Reflections Article URL</label>
                                    <asp:TextBox ID="trainerReflections" runat="server" />
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
