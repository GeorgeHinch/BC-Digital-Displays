<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menu-manager.aspx.cs" Inherits="settings_menu_manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Menu Restore</title>
		<meta charset="utf-8" />
		<meta name="viewport" content="width=device-width, initial-scale=1" />
		<!--[if lte IE 8]><script src="../assets/js/ie/html5shiv.js"></script><![endif]-->
		<link rel="stylesheet" href="../assets/css/main.css" />
		<!--[if lte IE 9]><link rel="stylesheet" href="../assets/css/ie9.css" /><![endif]-->
		<!--[if lte IE 8]><link rel="stylesheet" href="../assets/css/ie8.css" /><![endif]-->
        <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css" />

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
						  <h1 class="major">Menu Manager</h1>
						  <span class="image fit"><img src="../images/menu-1040.jpg" alt="" /></span>
						  <h2>Menu Items</h2>
                            <p>Drag menu items to rearrange their order.</p>
                            <form id="menuForm" runat="server">
                                <div id="currentMenu">
                                    <asp:GridView id="menuOrder" AutoGenerateColumns="false" runat="server">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="5%" ItemStyle-ForeColor="White" ItemStyle-Font-Names="FontAwesome">
                                                <ItemTemplate>
                                                    <asp:literal runat="server">
                                                        <p class="fa-bars" style="margin:0;color:rgba(255, 255, 255, 0.1)"></p>
                                                        </asp:literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    <input type="hidden" name="orderID" value='<%# Eval("id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="menuItem" HeaderText="Menu Item" />
                                            <asp:HyperLinkField Text="hide" DataNavigateUrlFields="id" DataNavigateUrlFormatString="?remove={0}" ItemStyle-Width="10%" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button Text="Save Order" OnClick="saveOrder" runat="server" />
                                </div>
                            </form>
                            <h2>Hidden Menu Items</h2>
                            <div class="table-wrapper">
                                <asp:Literal ID="currentMenusTable" runat="server" />
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
			<script src="../../assets/js/jquery.min.js"></script>
			<script src="../../assets/js/jquery.scrollex.min.js"></script>
			<script src="../../assets/js/jquery.scrolly.min.js"></script>
			<script src="../../assets/js/skel.min.js"></script>
			<script src="../../assets/js/util.js"></script>
			<!--[if lte IE 8]><script src="../../assets/js/ie/respond.min.js"></script><![endif]-->
			<script src="../../assets/js/main.js"></script>
            <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
            <script type="text/javascript">
                $(function () {
                    $("[id*=menuOrder").sortable({
                        items: 'tr:not(thead tr)',
                        cursor: 'pointer',
                        axis: 'y',
                        dropOnEmpty: false,
                        start: function (e, ui) {
                            ui.item.addClass("selected");
                        },
                        stop: function (e, ui) {
                            ui.item.removeClass("selected");
                        },
                        receive: function (e, ui) {
                            $(this).find("tbody").append(ui.item);
                        }
                    });
                });
            </script>
</body>
</html>
