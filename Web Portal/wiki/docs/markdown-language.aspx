<%@ Page Language="C#" AutoEventWireup="true" CodeFile="markdown-language.aspx.cs" Inherits="wiki_wiki" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Digital Display Admin - Markdown Language Wiki</title>
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
				<a href="../default.aspx" class="title">Digital Display Admin</a>
				<nav>
					<ul>
						<li><a href="../../default.aspx">Home</a></li>
                        <li><a href="../../default.aspx#settings">Settings</a></li>
						<li><a href="../wiki.aspx"  class="active">Wiki</a></li>
					</ul>
				</nav>
			</header>

		<!-- Wrapper -->
			<div id="wrapper">

                <!-- Main -->
					<section id="main" class="wrapper">
						<div class="inner">
                            <span class="image fit"><img src="../../images/trainer-1040.jpg" alt="" /></span>

                            <p>
                                <a href="../wiki.aspx" class="button small icon fa-angle-left">return to wiki</a>
                            </p>

						    <h1 class="major">Markdown Language Wiki</h1>

                            <p>This is intended as a quick reference and showcase. For more complete info, see <a href="http://daringfireball.net/projects/markdown/">Gruber's original spec</a> and the <a href="http://github.github.com/github-flavored-markdown/">Github-flavored Markdown</a> info page.</p>

                            <p>You can also check out more <a href="https://github.com/adam-p/markdown-here/wiki/Other-Markdown-Tools">Markdown tools</a> to learn more about markdown and possible extensions of the implementation.</p>

                            <section style="padding-bottom: 35px;">
                                <h2>Page Usage</h2>
                                <ul>
                                    <li><a href="../../settings/notification-manager.aspx">Notification Manager</a></li>
                                </ul>
                            </section>

                            <section style="padding-bottom: 35px;">
                                <h2>Emphasis and Strong Emphasis</h2>
						        <p>Ephasis, or italicized text can be added by adding two asterisks (**) before and after the text you want empasised.</p>
                                <p>Strong ephasis, or bold text can be added by adding two underscores (__) before and after the text you want empasised.</p>
                                <pre><code>This is a sample sentence with **italic text** within.<br />This is a sample sentence with __bold text__ within.</code></pre>

                                <p>Creates: <br />
                                    This is a sample sentence with <em>italic text</em> within.<br />
                                    This is a sample sentence with <b>bold text</b> within.
                                </p>
                            </section>

                            <section style="padding-bottom: 35px;">
                                <h2>Identations</h2>
                                <p>Tab characters can be added to the begining of text blocks by adding three bullet characters (···) at the start of a line.</p>
                                <pre><code>···This is an indented sentence.</code></pre>
                                <p style="text-indent: 50px;">This is an indented sentence.</p>
                            </section>

                            <section style="padding-bottom: 35px;">
                                <h2>Bullets</h2>
                                <p>Bullets can be added to the begining of text blocks by adding two bullet characters and an asterisk (··*) at the start of a line.</p>
                                <pre><code>This is a bulleted list: <br />··*Bulleted Item 1<br />··*Bulleted Item 2<br />··*Bulleted Item 3</code></pre>
                                <p>This is a bulleted list:<br />
                                <ul>
                                    <li>Bulleted Item 1</li>
                                    <li>Bulleted Item 2</li>
                                    <li>Bulleted Item 3</li>
                                </ul>
                                </p>
                            </section>
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
