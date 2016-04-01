﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="trainer-manager.aspx.cs" Inherits="settings_trainer_manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Trainer Manager</title>
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
						  <h1 class="major">Trainer Manager</h1>
						  <span class="image fit"><img src="../images/trainer-1040.jpg" alt="" /></span>
                          <h2>Current Trainers</h2>
                          <div class="table-wrapper">
										<table>
											<thead>
												<tr>
													<th style="width:5%;">Num.</th>
												  <th style="width:85%;">Name</th>
												  <th style="width:10%;"></th>
												</tr>
											</thead>
											<tbody>
												<tr>
													<td>1</td>
													<td>Justin Ehling</td>
													<td><a href="#">edit</a></td>
												</tr>
												<tr>
													<td>2</td>
													<td>Cameron Court</td>
													<td><a href="#">edit</a></td>
												</tr>
												<tr>
													<td>3</td>
													<td>Tyler Greer</td>
													<td><a href="#">edit</a></td>
												</tr>
												<tr>
													<td>4</td>
													<td>Mary Worley</td>
													<td><a href="#">edit</a></td>
												</tr>
												<tr>
													<td>5</td>
													<td>Cory Patterson</td>
													<td><a href="#">edit</a></td>
												</tr>
											</tbody>
										</table>
									</div>
                                    
                          <h2>New Trainer</h2>
						  <a href="add/add-trainer.aspx" class="button">Add Trainer</a>
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