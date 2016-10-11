<%@ Page Language="C#" AutoEventWireup="true" CodeFile="add-event.aspx.cs" Inherits="settings_add_add_event" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Add Event</title>
    <meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	    <!--[if lte IE 8]><script src="../../assets/js/ie/html5shiv.js"></script><![endif]-->
	<link rel="stylesheet" href="../../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../../assets/css/ie8.css" /><![endif]-->
    <script>
        var isChanged = false;
        $(function () {
            $('#<%=eventDepartment.ClientID%>').focusin(function () {
                if (!isChanged) {
                // this removes the first item which is your placeholder if it is never changed
                $(this).find('option:first').remove();
            }
        });
        $('#<%=eventDepartment.ClientID%>').change(function () {
            // this marks the selection to have changed
            isChanged = true;
        });
        $('#<%=eventDepartment.ClientID%>').focusout(function () {
            if (!isChanged) {
                // if the control loses focus and there is no change in selection, return the first item
                $(this).prepend('<option selected="selected" value="0">Select the Department</option>');
            }});
        });
    </script>
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
                                    <label for="eventDepartment">Department</label>
                                    <asp:DropDownList ID="eventDepartment" runat="server" >
                                        <asp:ListItem Text="Select Department" Value="0" />
                                        <asp:ListItem Text="Aquatics" Value="1" />
                                        <asp:ListItem Text="Fitness" Value="2" />
                                        <asp:ListItem Text="Food & Beverage" Value="3" />
                                        <asp:ListItem Text="Member Events" Value="4" />
                                        <asp:ListItem Text="Recreation" Value="5" />
                                        <asp:ListItem Text="Tennis" Value="6" />
                                    </asp:DropDownList>
                                </div>
                              
                                <div class="field half">
                                    <label for="eventInstructor">Instructor</label>
                                    <asp:TextBox ID="eventInstructor" runat="server" />
                                </div>

                                <div class="field half first">
                                    <label for="eventAllDay">All Day</label>
                                    <asp:CheckBox ID="eventAllDay" Text="All Day" runat="server" />
                                </div>

                                <div class="field half">
                                    <label for="eventPrice">Price</label>
                                    <asp:TextBox ID="eventPrice" runat="server" />
                                </div>

                                <div class="field half first"> 
                                    <label for="eventStartDate">Start Date</label>
                                    <div class="field half first">
                                        <asp:TextBox ID="eventStartDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <asp:TextBox ID="eventStartTime" TextMode="Time" runat="server" />
                                    </div>
                                </div>   
                              
                                <div class="field half">
                                    <label for="eventEnd">End Date</label>
                                    <div class="field half first">
                                        <asp:TextBox ID="eventEndDate" TextMode="Date" runat="server" />
                                    </div>
                                    <div class="field half">
                                        <asp:TextBox ID="eventEndTime" TextMode="Time" runat="server" />
                                    </div>
                                </div>

                                <div class="field">
                                    <label for="eventDescription">Description</label>
                                    <asp:TextBox ID="eventDecription" runat="server" TextMode="MultiLine" Rows="3" />
                                </div>

                                <div class="field">
                                    <label for="eventFlier">Flier URL</label>
                                    <asp:TextBox ID="eventFlier" runat="server" />
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
