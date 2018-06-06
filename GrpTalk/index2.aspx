<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index2.aspx.cs" Inherits="GrpTalk.index2" %>


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="google-site-verification" content="m-zJg6Hzqs3xTIpoX7iBs-NlnfqgFYrSa1KxgNnkP8U" />

    <title>GrpTalk</title>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/font-awesome.min.css" rel="stylesheet">
    <link href="css/animate.min.css" rel="stylesheet">
    <link href="css/owl.carousel.css" rel="stylesheet">
    <link href="css/prettyPhoto.css" rel="stylesheet">
    <link href="css/main.css" rel="stylesheet">
    <link href="css/responsive.css" rel="stylesheet">
    <%--<link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">--%>

    <link rel="shortcut icon" href="images/ico/favicon.ico" />
    <style type="text/css">
        .cta {
            background: none repeat scroll 0 0 #eee;
            position: fixed;
            right: -259px;
            bottom: 15%;
            z-index: 99999;
            transition: right 0.5s ease 0s, left 0.5s ease 0s;
            border: 1px solid #ddd;
        }

            .cta i {
                background: none repeat scroll 0 0 #0e699a;
                color: hsl(0, 0%, 100%);
                float: left;
                font-size: 25px;
                margin-top: 0;
                padding: 13px 12px 11px;
            }

            .cta > span {
                color: hsl(0, 0%, 0%);
                float: right;
                font-size: 14px;
                font-weight: 600;
                line-height: 20px;
                padding: 0px 10px;
            }

            .cta:hover {
                right: 0;
            }

        .african-union {
            background-position: 0px -25px;
        }

        .arab-league {
            background-position: 0px -241px;
        }

        .asean {
            background-position: 0px -337px;
        }

        .caricom {
            background-position: 0px -961px;
        }

        .commonwealth {
            background-position: 0px -1153px;
        }

        .european-union {
            background-position: 0px -1729px;
        }

        .islamic-conference {
            background-position: 0px -2401px;
        }

        .nato {
            background-position: 0px -3433px;
        }

        .northern-cyprus {
            background-position: 0px -3673px;
        }

        .olimpic-movement {
            background-position: 0px -3745px;
        }

        .red-cross {
            background-position: 0px -4105px;
        }

        .scotland {
            background-position: 0px -4321px;
        }

        .opec {
            background-position: 0px -3793px;
        }

        .somaliland {
            background-position: 0px -4561px;
        }

        .united-nations {
            background-position: 0px -5305px;
        }

        .IN {
            background-position: 0px -2281px;
        }

        .AE {
            background-position: 0px -5257px;
        }

        .BH {
            background-position: 0px -457px;
        }

        .US {
            background-position: 0px -5329px;
        }

        input#txtPhone {
            border: 1px solid #ccc;
            border-radius: 3px;
            height: 29px;
            margin: 10px 0 0;
            width: 212px;
        }

        input#txtOtp, input#txtOtpViaSms {
            border: 1px solid #ccc;
            border-radius: 3px;
            padding: 5px 10px 5px 10px;
            width: 190px;
        }

        .dropdown-menu {
            text-align: center;
            width: 240px;
            padding: 17px;
        }

        #ddlCountry:hover {
            color: #000 !important;
        }

        .nav > li > a:hover, .nav > li > a:focus {
            color: rgb(231, 157, 54);
            background-color: none !important;
        }

        .page-header {
            margin: 0 0 0 0 !important;
            padding-bottom: 0;
        }

        .pre-scrollable .container {
            margin: 0 0 15px 0;
        }

        p.intro {
            padding: 0 0 0 0;
            margin: 0 0 0 0;
            color: #000;
            font-size: 18px;
        }

        .pull-left i.glyphicon-record {
            color: red !important;
        }

        .media.service-box:hover .pull-left i.glyphicon-record {
            color: white !important;
        }

        .media-body ul {
            padding-left: 16px;
        }

        #appstore h2 {
            margin-top: 0;
            font-weight: 400 !important;
        }

        #appstore {
            padding-top: 0 !important;
            padding-bottom: 0 !important;
        }

        #features img {
            float: right;
        }

        .modal-dialog {
            width: 900px;
        }

        .modal-header, .modal-footer {
            border: 0;
        }

        .intro {
            font-size: 20px;
            padding-bottom: 0.3em;
        }

        h4 {
            font-weight: normal;
            margin-bottom: 0;
        }

        ul.ver-inline-menu {
            list-style-type: none;
            padding: 0;
        }

        .ver-inline-menu li.active a {
            border-left: 3px solid hsl(208, 56%, 35%);
        }

        .ver-inline-menu li.active a, .ver-inline-menu li.active i {
            background: none repeat scroll 0 0 hsl(208, 56%, 45%);
        }

        .ver-inline-menu li.active a, .ver-inline-menu li:hover a {
            font-size: 14px;
        }

        .ver-inline-menu li.active a, .ver-inline-menu li.active i {
            color: #fff;
            background: #43688e;
            text-decoration: none;
        }

        .ver-inline-menu li.active a {
            border-left: solid 2px #43688e;
        }

        .ver-inline-menu li a {
            padding: 10px 0 10px 10px;
        }

        .ver-inline-menu li a {
            font-size: 14px;
            font-weight: 300;
            color: #557386;
            display: block;
            background: #f0f6fa;
            border-left: solid 2px #c4d5df;
        }

        ul.faq-content {
            list-style-type: none;
            margin: 0 0 0 0;
            padding: 0;
        }

            ul.faq-content li {
                padding: 8px 8px 8px 16px;
                margin: 0px 0px 0px 0px;
                font-size: 15px;
            }

                ul.faq-content li a {
                    color: #000;
                    text-decoration: none;
                    outline: none;
                }

            ul.faq-content li {
                background: url('images/Arrow.png') no-repeat left 17px;
            }

        p.faq-answer {
            color: #43688e;
        }

        .media.service-box #call {
            background: url('images/6.png') no-repeat left 13px center;
            border-radius: 100%;
            box-shadow: 0 0 0 1px #d7d7d7 inset;
            -webkit-box-shadow: 0 0 0 1px #d7d7d7 inset;
            -moz-box-shadow: 0 0 0 1px #d7d7d7 inset;
            height: 64px;
            line-height: 64px;
            position: relative;
            text-align: center;
            transition: background-color 50ms, background-color 50ms;
            width: 64px;
        }

        .media.service-box:hover #call {
            background: #2caab3 url('images/7.png') no-repeat left 13px center;
            border-radius: 100%;
            box-shadow: inset 0 0 0 5px rgba(255, 255, 255, 0.8);
            -webkit-box-shadow: inset 0 0 0 5px rgba(255, 255, 255, 0.8);
            -moz-box-shadow: inset 0 0 0 5px rgba(255, 255, 255, 0.8);
            height: 64px;
            line-height: 64px;
            position: relative;
            text-align: center;
            transition: background-color 50ms, background-color 50ms;
            width: 64px;
        }

        @media only screen and (max-width: 320px) {
            #main-slider .slider-inner h2 {
                margin-top: 40px;
            }

            .carousel-content img {
                margin-top: 10px !important;
                width: 100%;
            }

            .carousel-content h2 {
                font-size: 6em !important;
                text-align: left !important;
            }

            .carousel-content p {
                font-size: 1.5em !important;
                text-align: left !important;
            }

            .text-left, .text-right {
                text-align: center !important;
            }

            #features img {
                width: 100%;
                margin: 0 0 35px 0;
            }

            #appstore h2 {
                font-size: 25px;
            }

            #appstore .col-sm-6 {
                margin-bottom: 30px;
                border-right: 0 !important;
            }

            #appstore {
                padding-bottom: 0 !important;
            }

            .modal-dialog {
                width: 93% !important;
            }

            .section-header .section-title {
                font-size: 20px !important;
            }
        }

        @media only screen and (min-width: 321px) and (max-width: 640px) {
            #main-slider .slider-inner h2 {
                margin-top: 40px;
            }

            .carousel-content img {
                margin-top: 10px !important;
                width: 100%;
            }

            .carousel-content h2 {
                font-size: 6em !important;
                text-align: left !important;
            }

            .carousel-content p {
                font-size: 1.5em !important;
                text-align: left !important;
            }

            .text-left, .text-right {
                text-align: center !important;
            }

            #features img {
                width: 100%;
                margin: 0 0 35px 0;
            }

            #appstore h2 {
                font-size: 25px;
            }

            #appstore .col-sm-6 {
                margin-bottom: 30px;
                border-right: 0 !important;
            }

            #appstore {
                padding-bottom: 0 !important;
            }

            .modal-dialog {
                width: 93% !important;
            }
        }

        @media only screen and (min-width: 641px) and (max-width: 768px) {
            .modal-dialog {
                width: 90% !important;
            }
        }

        .list_carousel {
            margin: 0 0 30px 60px;
        }

            .list_carousel ul {
                margin: 0;
                padding: 0;
                list-style: none;
                display: block;
            }

            .list_carousel li {
                text-align: justify;
                width: 48%;
                padding: 0;
                margin: 6px;
                display: block;
                float: left;
                height: 170px;
            }

            .list_carousel.responsive {
                width: auto;
                margin-left: 0;
            }

        .clearfix {
            float: none;
            clear: both;
        }

        #countries_msdd {
            width: 82px !important;
        }
    </style>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', 'https://www.google-analytics.com/analytics.js', 'ga');
        ga('create', 'UA-76951198-1', 'auto');
        ga('send', 'pageview');
    </script>
    <script>
        (function (h, o, t, j, a, r) {
            h.hj = h.hj || function () { (h.hj.q = h.hj.q || []).push(arguments) };
            h._hjSettings = { hjid: 197088, hjsv: 5 };
            a = o.getElementsByTagName('head')[0];
            r = o.createElement('script'); r.async = 1;
            r.src = t + h._hjSettings.hjid + j + h._hjSettings.hjsv;
            a.appendChild(r);
        })(window, document, '//static.hotjar.com/c/hotjar-', '.js?sv=');
    </script>


</head>



<body id="home" class="homepage">
    <div class="cta">
        <i class="fa fa-phone"></i>
        <span style="margin-top: 6px;">For More Information<br />
            Give Missed Call To 
				
					<span class="bold" style="color: #2388c5;">918099975975</span>


        </span>
    </div>

    <section id="header">
        <div class="page-header">
            <div class="page-header-top fixed">
                <div class="container">
                    <div class="row">

                        <div class="col-md-9">
                            <a href="javascript:;" class="menu-toggler"></a>
                            <div class="page-header-menu">
                                <div class="hor-menu ">
                                    <ul class="nav navbar-nav">
                                        <li class="active" id="conference-li">
                                            <a href="/index.aspx">Home</a>
                                        </li>
                                        <li class="active" id="conference-li">
                                            <a href="https://web.grptalk.com" target="_blank">Web</a>
                                        </li>
                                        <li>
                                            <a href="Faq.aspx" class="faq">FAQ</a>
                                        </li>
                                        <li>
                                            <a href="#tab_4" data-target="#myModal" data-toggle="modal" class="contact">Contact Us</a>
                                        </li>
                                        <li>
                                            <a href="#tab_3" data-target="#myModal" data-toggle="modal" class="terms">Terms of Use</a>
                                        </li>
                                        <!-- <li class="" id="dashboard-li">
                                            <a href="/Conference-Calling.aspx">Conference Calling</a>
                                        </li>
                                        <li class="" id="contacts-li">
                                            <a href="/Audio-Conference.aspx">Audio Conference</a>
                                        </li> -->

                                    </ul>
                                </div>


                            </div>

                        </div>

                        <div class="col-md-3">
                            <ul class="nav navbar-nav" style="float: right; padding: 8px 0 0 0;">
                                <li class="active" id="conference-li" style="margin: 0 10px 0 0;">
                                    <ul class="nav pull-right">
                                        <li class="dropdown" id="menuLogin">
                                            <button class="btn btn-primary dropdown-toggle" id="btnLoginMenu" type="button" data-toggle="dropdown">Login</button>



                                            <div class="dropdown-menu" id="ddlMenu">

                                                <form class="form" id="formLogin">
                                                    <div class="ErrorMsg" style="color: red; font-size: 12px;">
                                                        <br />
                                                    </div>


                                                    <div style="border: 1px solid rgb(204, 204, 204); border-radius: 3px; height: 29px; margin: 0 4px 0 0; padding: 2px 5px; width: 212px;">
                                                        <select class="IN" style="border-color: gray; border: none; background-image: url(images/flags24.png); background-repeat: no-repeat; float: left; height: 24px; width: 200px; color: black; padding: 0 0 0 23px; font-size: 12px" id="ddlCountry">
                                                            <option value="IN" selected="selected" class="IN" style="background-image: url(images/flags24.png); background-repeat: no-repeat; float: left; height: 24px; padding: 5px 0 0 30px;">India(+91)</option>
                                                            <option value="AE" class="AE" style="background-image: url(images/flags24.png); background-repeat: no-repeat; float: left; height: 24px; color: black; padding: 5px 0 0 30px;">United Arab Emirates(+971)</option>
                                                            <option value="BH" class="BH" style="background-image: url(images/flags24.png); background-repeat: no-repeat; float: left; height: 24px; color: black; padding: 5px 0 0 30px;">Bahrain(+973)</option>
                                                            <option value="US" class="US" style="background-image: url(images/flags24.png); background-repeat: no-repeat; float: left; height: 24px; color: black; padding: 5px 0 0 30px;">United States(+1)</option>
                                                             <option value="UK" class="UK" style="background-image: url(images/uk-flag.png); background-repeat: no-repeat; float: left; height: 24px; color: black; padding: 5px 0 0 30px;">United Kingdom(+44)</option>
                                                        </select>

                                                    </div>
                                                    <input type="text" maxlength="10" value="" placeholder="Enter Your Mobile Number" autocomplete="on" id="txtPhone">
                                                    <button style="margin: 10px 0 0 0;" type="button" id="btnSentOtp" class="btn btn-primary">Send OTP</button>


                            </div>
                            <!-- SUBSCRIBE BUTTON -->
                            <%--<div class="col-md-6 col-sm-12 col-xs-12 text-left demo-btn">
                                <button id="subscribe-button" >Get the App  <i class="fa fa-caret-right hidden-xs"></i></button>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>


                                                </form>

                                                <form class="form" id="formOTP" style="display: none;">
                                                    <div class="ErrorMsg" style="color: red; font-size: 12px;"></div>
                                                    <input type="text" id="txtOtp" autocomplete="on" placeholder="Enter Your OTP" maxlength="6" />
                                                    <p class="WaitMsg" style="margin: 10px 0 10px 0;">
                                                        OTP has been sent to your Mobile App via notification.
                                                        <br />
                                                        <br />
                                                        <span class="WaitMsg">Please wait for <span id="timer" data-countdown='30' style="font-weight: bold; font-size: 15px; text-align: center; color: rgb(231, 157, 54);">30</span> sec</span>
                                                    </p>
                                                    <p class="CallUsMsg" style="margin: 10px 0 10px 0; color: #c443cf; display: none;" id="callUs">Please give a missed call to <b>040-39363923</b> to verify your number.</p>
                                                    <button class="btn btn-primary" id="btnSentOtpViaSms" style="display: none;" type="button">Send OTP via SMS</button>
                                                </form>


                                                <form style="display: none;" id="formOTPviaSMS" class="form">
                                                    <input type="text" maxlength="6" placeholder="Enter Your OTP" autocomplete="on" id="txtOtpViaSms">
                                                    <p class="WaitMsg2" style="margin: 10px 0 10px 0;">
                                                        Your OTP has been sent via SMS to your registered mobile number.<br>
                                                        <span class="WaitMsg2">Please wait for <span style="font-weight: bold; font-size: 15px; text-align: center; color: rgb(231, 157, 54);" id="timer3">60</span> sec</span>
                                                    </p>

                                                </form>





                                            </div>


                                        </li>
                                    </ul>
                                </li>
                                <li class="" id="dashboard-li">
                                    <%--<button class="btn btn-primary dropdown-toggle" id="menu2" type="button" data-toggle="dropdown">Sign up</button>--%>
                                    <ul class="dropdown-menu dropdown-menu-right" role="menu" aria-labelledby="menu2" style="padding: 15px; width: 330px; text-align: center;">
                                        <li role="presentation">
                                            <p>Haven't downloaded grpTalk App yet? </p>
                                            <p>
                                                Please give a missed call to
                                                <br />
                                                <span style="font-weight: bold; font-size: 20px; text-align: center; color: rgb(231, 157, 54);">8099 975 975</span>
                                                <br />
                                                to get App Download link for Android and iOS
                                            </p>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </section>


    <section id="main-slider">
        <div class="owl-carousel" style="display: block;">
            <div class="item" style="background-image: url(images/slider/Banner2.png);">
                <div class="slider-inner">
                    <div class="container">
                        <div class="row">
                            <div class="col-sm-6 text-right">
                                <div class="carousel-content">
                                    <h2 style="font-style: italic;"><span style="color: #fff;">grp</span><span style="color: #e79e36;">Talk</span></h2>
                                    <p style="font-size: 1.75em;">Group Calling Simplified</p>
                                </div>
                            </div>
                            <div class="col-sm-6 text-left">
                                <div class="carousel-content">
                                    <img src="images/1.png" alt="Phn" style="margin-top: 70px;" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--/.item-->
        </div>
        <!--/.owl-carousel-->
    </section>
    <!--/#main-slider-->

    <section id="features">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title text-center wow fadeInDown">TRADITIONAL CONFERENCE CALLING TURNED INSIDE OUT</h2>
            </div>
            <div class="row">
                <div class="col-sm-6 wow fadeInLeft text-right">
                    <img class="img-responsive margin-top-40" src="images/Inp1.png" alt="" width="270">
                </div>
                <div class="col-sm-6">
                    <div class="media service-box wow fadeInRight">
                        <div class="pull-left">
                            <i class="glyphicon glyphicon-th"></i>
                        </div>
                        <div class="media-body">
                            <h4 class="media-heading">No PINs required</h4>
                            <ul>
                                <li>all grp members receive a call</li>
                                <li>receivers need not install the app</li>
                            </ul>

                        </div>
                    </div>

                    <div class="media service-box wow fadeInRight">
                        <div class="pull-left">
                            <i class="fa fa-user"></i>
                        </div>
                        <div class="media-body">
                            <h4 class="media-heading">Who you're talking to</h4>
                            <ul>
                                <li>know who's on the call</li>
                                <li>see pics and social profiles</li>
                            </ul>

                        </div>
                    </div>

                    <div class="media service-box wow fadeInRight">
                        <div class="pull-left">
                            <i class="fa fa-microphone-slash"></i>
                        </div>
                        <div class="media-body">
                            <h4 class="media-heading">Control your calls</h4>
                            <ul>
                                <li>mute members on the fly</li>
                                <li>add new members in call</li>
                            </ul>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section id="services">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title text-center wow fadeInDown">Simple group calling on your mobile</h2>
            </div>
            <div class="row">
                <div class="features">
                    <div class="col-md-4 col-sm-6 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="0ms">
                        <div class="media service-box">
                            <div class="pull-left">
                                <i class="fa" id="call"></i>
                            </div>
                            <div class="media-body">
                                <h4 class="media-heading">Crystal Clear Call Quality</h4>
                                <p>Traditional landline calls, so no call drops </p>
                            </div>
                        </div>
                    </div>
                    <!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="100ms">
                        <div class="media service-box">
                            <div class="pull-left">
                                <i class="fa fa-calendar"></i>
                            </div>
                            <div class="media-body">
                                <h4 class="media-heading">Plan your group calls</h4>
                                <p>setup an instant call or schedule for later</p>
                            </div>
                        </div>
                    </div>
                    <!--/.col-md-4-->

                    <div class="col-md-4 col-sm-6 wow fadeInUp" data-wow-duration="300ms" data-wow-delay="200ms">
                        <div class="media service-box">
                            <div class="pull-left">
                                <i class="glyphicon glyphicon-record"></i>
                            </div>
                            <div class="media-body">
                                <h4 class="media-heading">Call Recording</h4>
                                <p>recording enabled and disabled on the fly</p>
                            </div>
                        </div>
                    </div>
                    <!--/.col-md-4-->
                </div>
            </div>
            <!--/.row-->
        </div>
        <!--/.container-->
    </section>
    <!--/#services-->
    <!-- Reviews -->
    <section id="reviews">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title text-center wow fadeInDown">Reviews</h2>
            </div>

            <div class="row">
                <div class="list_carousel responsive">
                    <ul id="foo4">
                        <li>
                            <img src="Reviews/1.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/2.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/3.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/4.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/5.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/6.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/7.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/8.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/9.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/10.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/11.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/12.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/13.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/14.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/15.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/16.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/17.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/18.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/19.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/20.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/21.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/22.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/23.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/24.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/25.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/26.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/27.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/28.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/29.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/30.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/31.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/32.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/33.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/34.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/35.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/36.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/37.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/38.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/39.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/40.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/41.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/42.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/43.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/44.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/45.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/46.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/47.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/48.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/49.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/50.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/51.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/52.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/53.jpg" alt="" /></li>
                        <li>
                            <img src="Reviews/54.jpg" alt="" /></li>
                    </ul>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </section>
    <!-- Reviews End -->
    <section id="appstore">
        <div class="container">
            <div class="text-center">

                <div class="row" style="margin-top: 40px; margin-bottom: 40px">
                    <h2>Available on</h2>
                    <div class="col-sm-6 col-md-6" style="border-right: 1px solid #aaa;">

                        <a href="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk" target="_blank">
                            <img src="images/PlayStore.png" alt="Play Store" /></a>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <%--<h2>Available on</h2>--%>
                        <a href="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&mt=8" target="_blank">
                            <img src="images/AppStore.png" alt="App Store" /></a>
                    </div>
                </div>

            </div>
        </div>
    </section>

    <footer id="footer">
        <div class="container">
            <div class="row">
                <div class="col-sm-6 text-left">
                    &copy; 2015 SMSCountry Networks Pvt Ltd.
                </div>
                <div class="col-sm-6 text-right">
                    <ul>
                        <li><a href="javascript:void(0);" class="home">Home</a></li>
                        <li><a href="Conference-Calling.aspx" class="about">Conference Calling</a></li>
                        <li><a href="Faq.aspx" class="faq">FAQ</a></li>
                        <!--<li><a href="#tab_2" data-target="#myModal" data-toggle="modal" class="faq">FAQ</a></li>-->
                        <li><a href="#tab_4" data-target="#myModal" data-toggle="modal" class="contact">Contact Us</a></li>
                        <li><a href="#tab_3" data-target="#myModal" data-toggle="modal" class="terms">Terms of Use</a></li>
                        <li><a href="Audio-Conference.aspx" class="adwords">Audio Conference</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </footer>
    <!--/#footer-->

    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-3">
                            <ul class="ver-inline-menu tabbable margin-bottom-10">
                                <li><a href="#tab_4" data-toggle="tab">Contact Us</a><span class="after"></span></li>
                                <li><a href="#tab_3" data-toggle="tab">Terms of Use </a></li>

                            </ul>
                        </div>
                        <div class="col-md-9">
                            <div class="tab-content">
                                <div id="tab_3" class="tab-pane">
                                    <div class="pre-scrollable">
                                        <h4 class="font-grey-gallery bold-600">grpTalk's End User License Agreement and Terms of Use</h4>
                                        <p><b>1. Your Agreement</b></p>
                                        <p>The grpTalk mobile and desktop application, software, design, servers, data, user input, content, website available at www.grpTalk.com (“Website”), and associated services (collectively “Services” or “App”) are owned and operated by SMS Country Networks Pvt. Ltd. (“grpTalk”) and are licensed to you in accordance with the terms of this End User License Agreement (“Agreement”).</p>
                                        <p>The App hereinafter cumulatively referred to as “Service” or “Services” and SMS Country Networks Pvt. Ltd. and grpTalk referred to hereinafter as "grpTalk"</p>
                                        <p>The App is © 2015 grpTalk.</p>
                                        <p>YOU ACKNOWLEDGE AND AGREE THAT YOU HAVE A DUTY TO READ THIS AGREEMENT BEFORE YOU INSTALL AND RUN THIS APP ON YOUR DEVICE. YOUR INSTALLATION, USE, OR OPERATION OF THIS APP CONSTITUTES YOUR MANIFESTATION OF ASSENT TO AND ACCEPTANCE OF THE TERMS OF THIS AGREEMENT. IF YOU DO NOT AGREE TO THE TERMS OF THIS AGREEMENT, YOU ARE EXPRESSLY PROHIBITED FROM USING THE APP AND MUST TERMINATE YOUR USE OF AND UNINSTALL THE APP IMMEDIATELY. THIS AGREEMENT DOES NOT REPLACE BUT IS IN ADDITION TO ANY ITUNES AGREEMENT REQUIRED BY APPLE AND ANY GOOGLE PLAY AGREEMENT REQUIRED BY GOOGLE IN ORDER TO DOWNLOAD AND USE THE APP.</p>
                                        <p>No Emergency Calls.</p>
                                        <p>It is important that you understand that the App is not a replacement for your mobile phone. The Services are not meant to and do not support or carry calls to emergency services of any kind. grpTalk will not be liable for any attempted emergency calls.</p>
                                        <p>grpTalk and you both acknowledge and agree that Apple, Google and its subsidiaries, are third party beneficiaries of the Agreement, and that, upon your acceptance of this Agreement, Apple and Google will have the right (and will be deemed to have accepted the right) to enforce the Agreement against you as a third party beneficiary thereof. grpTalk and you both further acknowledge and agree that this Agreement is between us only, and not Apple or Google, and that maintenance and support, warranties, product claims, intellectual property infringement disputes are not the responsibility of Apple and Google.</p>
                                        <p>grpTalk reserves the right, in its sole and absolute discretion, to discontinue the App or to modify, change, replace, or discontinue this Agreement. If grpTalk modifies, changes, or discontinues the terms of this Agreement, the Effective Date, located above, will change. Your installation or use of the App after a change in the Effective Date of this Agreement constitutes your acceptance of and manifestation of assent to any modification, change, or replacement.</p>
                                        <p><b>2. The grpTalk App</b></p>
                                        <p><b>Callback flow</b></p>
                                        <p>grpTalk provides inexpensive call rates for your audio conferencing needs through a callback or call out flow. Upon initiating a call from grpTalk you will receive a callback from our servers. grpTalk will then dial another call out to your destination number and connect the two on a conference bridge. If your service provider charges you for incoming calls you will be responsible for those charges.</p>
                                        <p><b>Other terms</b></p>
                                        <p>By downloading and using the App, you agree that grpTalk, and our designees and agents, may contact you by any available means, including, but not limited to, by email, telephone, text messages, push notifications or messages within the Services.</p>
                                        <p>You acknowledge and agree that grpTalk is a service provider and will not be held liable for any costs or fees incurred by you through your use of the App, including but not limited to mobile carrier fees, SMS or other text message fees, or payment provider fees.</p>
                                        <p>grpTalk reserves the right to cancel or suspend services to or for any specific number ranges, telecommunications service providers, countries, states, or geographic regions, or any other number grouping or service location, at any time and for any duration at our sole discretion.</p>
                                        <p>The App is free to download. You only pay call out rates. When downloading and/or using the Services, you may provide personal information that will be used consistent with the Privacy Policy, which is incorporated into this Agreement. You agree to provide grpTalk with, and maintain, accurate, up to date, and complete information. It is your responsibility to keep your mobile phone or other device, as well as access to your personal information made available in grpTalk, secures from unauthorized access and you accept full responsibility for any and all use.</p>
                                        <p><b>3. Your Warranties and Representations</b></p>
                                        <p>You warrant and agree that you have the right, power, and legal capacity to enter into this Agreement and to adhere to the terms and conditions hereunder. You further warrant and agree that you are not prohibited from entering into this Agreement or prohibited from downloading or using the Services by any preexisting agreement or otherwise. You warrant and agree that you are a human individual that is at least eighteen (18) years of age or older. If you are under the age of eighteen (18), you should review this Agreement with your parent or guardian to make sure that you or your parent or guardian understand it and allows for your use of the App. You agree to comply, in good faith, with the terms of this Agreement.</p>
                                        <p>All users of the Services further warrant the following:</p>
                                        <p>You agree to comply with the terms of this Agreement in good faith.</p>
                                        <p>You will not use the Services outside of the uses specifically provided for under this Agreement, including any and all licenses.</p>
                                        <p>You will not use the Services in any way that violates the rights of third parties, and you agree to comply with any and all applicable local, national, state, provincial, and international laws, treaties, and regulations.</p>
                                        <p>You understand that grpTalk cannot guarantee preservation of records and may delete or modify information without notice at its sole discretion.</p>
                                        <p>You will not make any derivative works of the Services or delete or modify, in any way, any copyright, trademark, or other proprietary notices that appear on the Services.</p>
                                        <p>You will not use the Services to communicate any false, misleading, personal, or defamatory content.</p>
                                        <p>You will not use, modify, copy, distribute, frame, reproduce, republish, download, scrape, display, post, mine, transmit, or sell the Services in any form or by any means, in whole or in party, without the prior written consent of grpTalk.</p>
                                        <p>grpTalk does not warrant or guarantee that compliance with this Agreement will be sufficient to comply with your obligations hereunder, under applicable law or with third party rights. Given the global nature of the Internet, you agree to comply with all laws and rules where you reside or where you use Services. Services are operated worldwide and grpTalk makes no representation that its Services are appropriate, lawful, or available for use in your location. grpTalk does not offer Services where prohibited by law. If you are in a jurisdiction in which use of the Services are prohibited by law, you may not use the Services.</p>
                                        <p><b>4. Limited License Grant</b></p>
                                        <p>The App and related materials, including but not limited to any text, names, marks, statistics, graphics, photos, images, sounds, music, videos, software, scripts and interactive features, as well as its associated data and services generated by us, is the property of, owned by and licensed through grpTalk. grpTalk grants you a limited, non-exclusive, royalty free, non-sub licensable, non-transferrable, and non-assignable license to install and use one copy of the App in executable object code form to be used on a single mobile or portable device for non-commercial, personal purposes.</p>
                                        <p>You are prohibited from copying, making derivative works of, modifying, publicly performing, publicly displaying, streaming, exploiting, broadcasting, decompiling, adapting, distributing, reproducing, republishing, scraping, transmitting, selling, posting, or hacking the App, in whole or in part, without the prior written consent of grpTalk. You are prohibited from using the trademarks, service marks, design marks, and logos of grpTalk, or any colorable imitation thereof, or any mark not owned or licensed by you, including, without limitation the terms “GRPTALK” or the grpTalk logo(s), as an indicator of source, as a part of a domain name, or in any way that is likely to cause confusion without the prior written consent of grpTalk. The App is subject to all intellectual property laws, including but not limited to trademark, copyright, patent and other privacy and proprietary laws. All trademarks, images, copyrights or rights of publicity displayed in connection with your use of the App are the property of their respective owners.</p>
                                        <p>You are prohibited from using the App for any use not explicitly stated in this Agreement, absent separate written agreement signed by grpTalk and you pursuant to a binding agreement. Such unauthorized uses may include:</p>
                                        <p>Any use inconsistent with or in violation of this Agreement or any local, state, provincial, national, or international law, regulation, statute, ordinance, or treaty.</p>
                                        <p>Any commercial use, such as the resale or republication of the App;</p>
                                        <p>Any modification of the App, including, but not limited to, translation into another computer language or the creation of derivative works from the App;</p>
                                        <p>Any use of the App outside of its customary or intended purposes;</p>
                                        <p>Any use of the App to defraud, to impersonate, to harass, to violate the rights of third parties, or to collect personal or personally identifiable information from users of the App without their knowledge or consent;</p>
                                        <p>Any use of the App that involves internet relay chat servers (“IRCs”) or IRC bots;</p>
                                        <p>Any use of the App to cheat, exploit, or otherwise interfere with any lawful activity;</p>
                                        <p>Any other activity that disrupts the App or its associated services, including, but not limited to, through hacking or denial of service attacks; or</p>
                                        <p>Any use of the App after you have been removed by grpTalk or previously banned.</p>
                                        <p>The Services may contain licensed materials from third parties and those third parties may enforce their rights in the event of a breach of this Agreement.</p>
                                        <p><b>5. Registration</b></p>
                                        <p>Downloading the grpTalk App is free. In order to download and/or use the Services, you must provide certain information to register your mobile phone or additional phone(s) and device(s). As part of account registration, you will enter your name and mobile number. You agree to provide grpTalk with accurate, up to date, and complete information. Registered users understand and agree that they have an ongoing duty to update their personal information if and when it changes. Registered users agree to keep their accounts and devices secure from unauthorized access. Registered users alone are responsible for their accounts and devices, and accept full responsibility for any and all use of their accounts and devices, whether authorized or unauthorized. In the case of unauthorized access to an account, you agree to contact grpTalk immediately. You are responsible for any charges to your account until grpTalk is able to terminate or suspend your account.</p>
                                        <p>grpTalk does not endorse you or discriminate based upon any information provided by you. Information you provide will be used consistent with the Privacy Policy.</p>
                                        <p>By creating an account, you agree that grpTalk, and our designees and agents, may contact you by any available means, including, but not limited to, by email, telephone, text messages, push notifications or messages within the Services.</p>
                                        <p>grpTalk reserves the right to restrict access to, suspend, disable, or delete your account at any time, in its sole discretion, and without prior warning. You are expressly prohibited from selling, leasing, lending, assigning, or otherwise transferring your account.</p>
                                        <p>You understand and agree that grpTalk provides software and related services and takes no responsibility and cannot be held liable or responsible for any interaction between users of the Services, whether with advertisers, through accounts, with third-parties, or otherwise. grpTalk makes no representations or warranties as to the truth or falsity of any information submitted to the App or provided by grpTalk or any user of the App, or the legality, quality, or safety of the services offered through the App.</p>
                                        <p>grpTalk may establish and modify, in its own discretion, at any time and without prior notice, practices and limits concerning use of the App, including, without limitation, limits on the amount of time data is retained, limits on amount of data that may be submitted to the Services, limits on the length of calls places using the Services, limits on the amount of times a user may access the App. grpTalk reserves the right to log off and/or delete accounts that are inactive for an extended period of time.</p>
                                        <p>If you have installed the App on your device, you agree that grpTalk may automatically install and update any bug fixes, enhanced functions, new modules or completely new versions of the App directly on your device without your prior permission.</p>
                                        <p>The App is free to download, but grpTalk reserves the right to charge a fee to download and/or use the App and any part thereof in the future.</p>
                                        <p><b>6. Charges and Payment</b></p>
                                        <p>Our calling rates and charges are provided within the App and are incorporated into this Agreement by reference. The rates and charges depend on several factors, including, but not limited to, location and type of destination phone number, the user’s location and the type of phone or device used to make the call, whether and what type of base or add-on phone plan the user registered for, and any current promotions that may be available. You understand that you will pay any additional charges you incur if you connect to the services through a phone number provided by grpTalk while you are in a country that is not the same country associated with your network or mobile phone provider (“Roaming Charges”). Roaming Charges will be charged in addition to any charges you may incur when using the App from another country. You are responsible for checking all applicable rates and charges prior to placing a call with the App. We may change the billing period or the rates and charges at any time by posting changes on the website. Any change that is required by law or governmental authority will be effective immediately.</p>
                                        <p>Charges for calls are measured in whole minutes and fractions of a minute will be rounded up to the next whole minute. Timing of the call begins when the call is answered by your contact, including voicemail or an automatic reply. Timing of the call ends when the user hangs up or when grpTalk receives a signal that the call has terminated from the terminating carrier.</p>
                                        <p>All calls are pre-paid. You may add to your account balance at any time on the Website or through the App. When a call is charged against your account, we will charge against available balance purchased by you. You agree to pay all applicable taxes, fees and other charges that we bill for the Services. You agree to pay for the Services at the rates and charges listed on Website. You acknowledge that calls in progress may be terminated if your account balance is insufficient to cover the cost of continuing the call.</p>
                                        <p>You agree that you will notify us of any changes to your payment information, including card expiration dates. You understand and agree that if for any reason your account balance becomes negative, you hereby authorize us to charge the negative account balance to any credit or debit card on file with your account without any additional confirmation. We reserve the right to retain any payment information and to charge any card or other payment information for any outstanding amounts on your account so long as your account remains active and for a reasonable period after an account is terminated.</p>
                                        <p>All rates and charges are reflected in Indian Rupee amounts and all payments must be made in INR. </p>
                                        <p>You are responsible for reviewing your billing information. If you believe that grpTalk has charged your account in error, you agree to notify us of any disputed charges within ninety (90) days after the date the alleged error appears in your account history, in writing in English via email to: hello@grpTalk.com</p>
                                        <p>If we determine that a billing adjustment is appropriate, we will credit your account. If you fail to timely notify us of a billing dispute, you understand that you hereby waive all rights to bring any claim regarding a charge to your account.</p>
                                        <p>Amounts used are nonrefundable. You acknowledge and agree that if we discontinue the services while your account is active, we are not obligated to refund you any of the prepaid amounts you purchase. Only make prepayments to your account if you believe that you will use the balance.</p>
                                        <p>Prepaid amounts you purchase will remain credited to your account and will not expire so long as your account remains active. If your account expires, you acknowledge that we are not obligated to refund to you any prepaid amounts purchased by you.</p>
                                        <p><b>7. Account Termination</b></p>
                                        <p>You may choose to close your account by notifying us by email at <a href="mailto:hello@grpTalk.com">hello@grpTalk.com</a>. We will then terminate your account and you will lose access to your account. You acknowledge that you will not be entitled to a refund of any unused prepaid balance in your account as of the date you terminate your account. You also acknowledge that you will remain responsible for payment of all charges for Services up through termination.</p>
                                        <p>If your account balance is depleted and you do not replenish your account balance within 6 months, we may elect, in our sole discretion, to terminate your account. If your account remains inactive for 12 months, we may elect, at our sole discretion, to terminate your account. If you wish to keep your account active, you may request an extension of this expiration period by notifying us by email at <a href="mailto:hello@grpTalk.com">hello@grpTalk.com</a>. We may, at our sole discretion, grant an extension request. We are not obligated to provide an extension nor are we obligated to refund any prepaid balance remaining in your account if your account terminates. We may charge, at our sole discretion, a reactivation fee to reactivate a terminated account or to keep an account from expiring.</p>
                                        <p>We reserve the right, at our sole discretion and for any reason, including, but not limited to, your breach of this Agreement or your unlawful use of the Services, to suspend, restrict, modify or terminate your account and your access to and use of the Services. If your account is suspended, restricted, modified, or terminated, you understand and agree that you are still responsible for any charges that accrue through the date that we fully process any such suspension, restriction, modification or termination. You agree to reimburse us for any reasonable costs we incur in collecting charges owed to us, including attorneys’ fees.</p>
                                        <p>We also reserve the right, at our sole discretion and for any reason, to suspend, modify, restrict, or discontinue the Services at any time.</p>
                                        <p><b>8. Customer Service</b></p>
                                        <p>If you have any questions, concerns or complaints about our Services, you may contact our Customer Service through the feature provided within our App and on our website. Please allow up to 72 hours for a response email.</p>
                                        <p><b>9. Terms and Conditions of Third Parties</b></p>
                                        <p>You agree to comply with all terms and conditions of any third party whose software or services are used in conjunction with the App, including but not limited to any vendor which provides access and download services (e.g., iTunes, Google Play), any network provider (e.g., Airtel, Reliance), any platform provider (e.g., iOS), or any hardware manufacturer (e.g., Apple iPhone, Samsung, HTC).</p>
                                        <p><b>10. We Do Not Endorse Any Product or Service</b></p>
                                        <p>grpTalk may allow advertisers to display advertisements within our Services. However, grpTalk does not endorse or recommend any commercial product, process, or service. The views and opinions of users, contributors, and others expressed through the App do not necessarily state or reflect those of grpTalk and are not intended to be used for advertising or product endorsement purposes.</p>
                                        <p><b>11. Feedback and Support</b></p>
                                        <p>grpTalk encourages its users to submit comments, suggestions, error reports, or support inquiries to grpTalk using the feedback function of the App or using the website. You acknowledge and agree that any feedback submitted to grpTalk, including, but not limited to, any intellectual property or other proprietary information contained within that feedback, will become the exclusive property of grpTalk. You agree to assign all right and title in or to any and all feedback that you submit to grpTalk and execute any and all documents necessary to assign your rights to any and all feedback to grpTalk upon grpTalk’s request, including but not limited to any documents necessary to perfect grpTalk’s rights in and to intellectual property or proprietary rights.</p>
                                        <p><b>12. Disclaimer and Limitation of Liability</b></p>
                                        <p>You acknowledge and agree that the nothing within the App will be construed to create a warranty of any kind. You acknowledge and agree that grpTalk takes no responsibility for, is not obligated to monitor and cannot be held liable for all the information contained within or communicated through the App as provided by you, third parties, information sent to grpTalk by third parties, and information intercepted by third parties. You agree to hold grpTalk harmless for any and all inaccuracies, omissions, errors, loss of data, corruption of data, failure of hardware, failure of the App, or misuse of the App.</p>
                                        <p>GRPTALK PROVIDES THE APP ON AN AS-IS BASIS AND WITHOUT WARRANTY OF ANY KIND, WHETHER EXPRESS, IMPLIED, OR STATUTORY, INCLUDING BUT NOT LIMITED TO WARRANTIES OF MERCHANTABILITY, QUALITY, FITNESS FOR A PARTICULAR PURPOSE, NON-INFRINGEMENT OF PATENT, TRADEMARK, COPYRIGHT, TRADESECRETS OR ANY OTHER INTELLECTUAL PROPERTY OR PROPRIETARY RIGHTS, OR ACCURACY. YOU ACKNOWLEDGE AND AGREE THAT YOU USE THE APP AT YOUR OWN RISK AND THAT GRPTALK WILL NOT BE HELD LIABLE FOR ANY DEFECTS, ERRORS, OMISSIONS, BUGS, OR DOWNTIME. ANY ATTEMPT BY GRPTALK TO MODIFY THE APP WILL NOT BE DEEMED TO BE A WAIVER OF THIS LIMITATION OF LIABILITY. GRPTALK WILL NOT BE HELD LIABLE FOR ANY CONTENT CONTAINED WITHIN OR COMMUNICATED THROUGH THE APP.</p>
                                        <p>GRPTALK WILL NOT BE HELD LIABLE FOR ANY DAMAGES, INCLUDING WITHOUT LIMITATION CONSEQUENTIAL DAMAGES, INCIDENTAL DAMAGES, PUNITIVE DAMAGES, SPECIAL DAMAGES, EXEMPLARY DAMAGES, INDIRECT DAMAGES, LOST PROFITS, LOST SAVINGS, BUSINESS INTERRUPTION, OR LOST INFORMATION ARISING OUT OF THE USE, WHETHER PROPER OR IMPROPER, OF THE APP, EVEN IF GRPTALK HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. GRPTALK DOES NOT ASSUME RESPONSIBILITY FOR ANY ERROR IN, OMISSION OF, INTERRUPTION OF, DELETION OF, DEFECT IN, DESTRUCTION OF, UNAUTHORIZED ACCESS TO, OR ALTERATION OF ANY DATA, AUTHENTICATION INFORMATION, OR SERVICES. YOU BEAR THE SOLE RESPONSIBILITY TO PROTECT AND BACKUP YOUR OWN DATA, NETWORK, HARDWARE, SYSTEMS, SERVERS, SOFTWARE, COMPUTERS, PHONES, AND SECURITY.</p>
                                        <p>UNDER ANY CIRCUMSTANCES, YOU ACKNOWLEDGE AND AGREE THAT GRPTALK’S MAXIMUM LIABILITY UNDER THIS AGREEMENT WILL BE LIMITED TO ANY AMOUNT THAT YOU MAY HAVE PAID FOR THE APP OR RELATED SERVICES. SOME JURISDICTIONS DO NOT ALLOW THE WAIVE OF IMPLIED WARRANTIES AND THE EXCLUSIONS REGARDING IMPLIED WARRANTIES MAY NOT APPLY TO YOU. THE LIMITATIONS, EXCLUSIONS, AND DISCLAIMERS LISTED IN THIS SECTION WILL APPLY TO THE MAXIMUM EXTENT PERMITTED BY LAW, EVEN IF ANY REMEDY FAILS OF ITS ESSENTIAL PURPOSE.</p>
                                        <p><b>13. Indemnification</b></p>
                                        <p>You agree to indemnify, hold harmless, and defend grpTalk, its officers, members, employees, agents, and directors from and against any and all claims, demands, causes of action, debts, liabilities, damages, costs, or expenses, including costs and reasonable attorney’s fees, arising out of or in relation to your use of the App, your violation of a term or provision of this Agreement, or your violation of the rights of a third party. You agree that your obligation to hold harmless, defend, and indemnify grpTalk will survive the termination or failure of this Agreement and your use of the App. You acknowledge and agree that your obligation to defend grpTalk will not provide you with the right to control grpTalk’s defense and you expressly agree that grpTalk has the right to direct and control its defense regardless of your obligation to defend grpTalk. You shall not enter into any settlement of compromise of any indemnifiable claim without grpTalk’s written consent.</p>
                                        <p><b>14. Assignment</b></p>
                                        <p>You are expressly prohibited from assigning your rights or obligations under this Agreement without grpTalk’s prior written consent. grpTalk may assign its rights or obligations under this Agreement at any time, including but not limited to in a sale of the grpTalk business or in a sale of the App.</p>
                                        <p><b>15. Integration</b></p>
                                        <p>This Agreement together with the list of rates and charges on the Website and the Privacy Policy, which are incorporated into this Agreement, constitutes the entire agreement between the parties relating to the subject matter of this Agreement and hereby supersedes all prior agreements, statements, or representations. This Agreement may only be modified by a writing signed by both parties or by grpTalk alone, who reserves the right to alter this Agreement and will notify you of any alterations so as to allow you to stop using the Services if you do not agree to the alterations. Your use of Services after any alteration signifies your acceptance of the terms and conditions of this Agreement and you are bound by them.</p>
                                        <p><b>16. Choice of Laws and Resolution of Disputes</b></p>
                                        <p>You agree that for all legal and non-legal purposes, the App is located in India. You agree that the App does not give rise to personal jurisdiction over grpTalk in jurisdictions other than India, except where otherwise agreed. </p>
                                        <p>You and grpTalk agree that in an effort to resolve any dispute that may arise under this Agreement or in connection with the Services, the parties will make a good faith effort to resolve any dispute by discussion prior to referring any matter to arbitration. If the parties are unable to resolve any dispute through discussion prior to arbitration within fifteen (15) days of commencing discussions, the dispute shall be referred to arbitration.</p>
                                        <p>YOU AND GRPTALK AGREE THAT ARBITRATION WILL BE THE EXCLUSIVE FORUM AND REMEDY AT LAW FOR ANY DISPUTES ARISING OUT OF OR IN RELATION TO THIS AGREEMENT OR CONCERNING THE VALIDITY, INTERPRETATION, BREACH, VIOLATION, OR TERMINATION OF THIS AGREEMENT. THIS ARBITRATION WILL BE HELD IN INDIA AND WILL BE HELD IN ACCORDANCE WITH THE MOST RECENTLY EFFECTIVE COMMERCIAL ARBITRATION RULES OF THE ARBITRATION ACT OR THE INTERNATIONAL ARBITRATION ACT AS APPROPRIATE. THE ARBITRATOR WILL DECIDE THE CLAIM ON THE BASIS OF THE LEGAL PRINCIPLES AND LAWS OF INDIA AND WILL HAVE THE DISCRETION TO AWARD ALL COSTS AND ATTORNEYS’ FEES. THE LOSING PARTY WILL BE REQUIRED TO PAY THE PREVAILING PARTY’S REASONABLE ATTORNEYS’ FEES. YOU AND GRPTALK AGREE THAT THE DETERMINATION OR AWARD OF THIS ARBITRATOR MAY BE ENTERED AS A JUDGMENT IN ANY COURT SITTING ANYWHERE THAT HAS JURISDICTION OVER THE SUBJECT MATTER OF THE DISPUTE. YOU AND GRPTALK AGREE THAT THE PARTIES WILL BE REQUIRED TO BE PRESENT WITHIN THE REPUBLIC OF SINGAPORE IN ORDER TO PERFORM THEIR OBLIGATIONS UNDER THIS AGREEMENT. YOU AND GRPTALK HEREBY AGREE TO SUBMIT TO THE PERSONAL JURISDICTION OF ANY SUCH ARBITRATOR OR ARBITRATION PROCEEDING.</p>
                                        <p>grpTalk may, but is not obligated to participate in any dispute between users. IF YOU HAVE A DISPUTE WITH ANOTHER USER, YOU RELEASE GRPTALK FROM ANY AND ALL CLAIMS, DEMANDS AND DAMAGES, KNOWN OR UNKNOWN. </p>
                                        <p><b>17. Additional Provisions</b></p>
                                        <p>No waiver of rights under this Agreement by either party will be recognized unless made in writing and signed by the party to be charged. This Agreement is solely between grpTalk and you and will not confer any rights or remedies upon any third party, including third party beneficiaries. A finding that any term or provision of this Agreement is invalid or unenforceable will not affect the validity or enforceability of this Agreement. Any term or provision of this Agreement that is found to be invalid or unenforceable will be reformed to the extent necessary to make it valid and enforceable.</p>
                                        <p>YOU AND GRPTALK AGREE THAT ANY CAUSE OF ACTION ARISING OUT OF OR IN CONNECTION WITH THIS AGREEMENT OR THE SERVICES PROVIDED UNDER THIS AGREEMENT MUST COMMENCE WITHIN ONE YEAR AFTER THE CAUSE OF ACTION ACCRUED. FAILURE TO ASSERT SAID CAUSE OF ACTION WITHIN ONE YEAR WILL PERMANENTLY BAR ANY AND ALL RELIEF.</p>
                                        <p>YOU WILL ONLY BE PERMITTED TO PURSUE CLAIMS AGAINST GRPTALK ON AN INDIVIDUAL BASIS, NOT AS A PLAINTIFF OR CLASS MEMBER IN ANY CLASS OR REPRESENTATIVE ACTION OR PROCEEDING AND YOU WILL ONLY BE PERMITTED TO SEEK RELIEF (INCLUDING MONTEARY, INJUNCTIVE, AND DECLARATORY RELIEF) ON AN INDIVIDUAL BASIS.</p>
                                        <p>The failure of or delay by either party to insist on strict performance of any provision of this Agreement shall not be construed as a waiver of that or any other breach of this Agreement. No waiver shall be effective unless in writing and signed by the party to be bound.</p>
                                        <p><b>18. Notice</b></p>
                                        <p>Any notice under this Agreement or other contact must be sent via certified mail or via email to: hello@grpTalk.com</p>
                                    </div>
                                </div>
                                <!-- tab_3 -->

                                <div id="tab_4" class="tab-pane">
                                    <div class="pre-scrollable">
                                        <h4>Contact us</h4>
                                        <br />
                                        <address class="">
                                            <i class="fa fa-phone fa-fw"></i>91-40-3936-3996<br />
                                            <br />
                                            <i class="fa fa-envelope fa-fw"></i><a href="mailto:hello@grpTalk.com">hello@grpTalk.com</a><br />
                                            <br />
                                            <i class="fa fa-building-o fa-fw"></i>2nd Floor, Ektha Towers, White Fields, Kondapur,Hyderabad-500084.
                                        </address>
                                    </div>
                                </div>
                                <!-- tab_4 -->

                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal-footer"></div>
            </div>
        </div>
    </div>
    <!-- Modal Ends -->
    
    <script type="text/javascript" src="js/jquery.js"></script>
    <script src="scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="https://twemoji.maxcdn.com/twemoji.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <%--<script src="https://maps.google.com/maps/api/js?sensor=true"></script>--%>
    <script src="js/owl.carousel.min.js"></script>
    <script src="js/mousescroll.js"></script>
    <script src="js/smoothscroll.js"></script>
    <script src="js/jquery.prettyPhoto.js"></script>
    <script src="js/jquery.isotope.min.js"></script>
    <script src="js/jquery.inview.min.js"></script>
    <script src="js/wow.min.js"></script>
    <script src="js/main.js"></script>
    <script src="scripts/jquery.plugin.js" type="text/javascript"></script>
    <script src="assets/global/scripts/jquery.countdown.js" type="text/javascript"></script>
    <script src="assets/global/scripts/jquery.countdown.min.js" type="text/javascript"></script>
    <script src="js/jquery.js" type="text/javascript"></script>

    <script src="js/jQueryblockUI.js" type="text/javascript"></script>
    <script src="Scripts/index2.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery.carouFredSel-6.2.1-packed.js"></script>
    <script type="text/javascript">

	
 

    </script>
    <!-- Google Code for Remarketing Tag -->
    <!--------------------------------------------------
Remarketing tags may not be associated with personally identifiable information or placed on pages related to sensitive categories. See more information and instructions on how to setup the tag on: http://google.com/ads/remarketingsetup
--------------------------------------------------->
    <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 923156976;
        var google_custom_params = window.google_tag_params;
        var google_remarketing_only = true;
        /* ]]> */
    </script>
    <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
    </script>
    <noscript>
        <div style="display: inline;">
            <img height="1" width="1" style="border-style: none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/923156976/?value=0&amp;guid=ON&amp;script=0" />
        </div>
    </noscript>
</body>
</html>
