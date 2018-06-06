<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QrCodeLogin.aspx.cs" Inherits="GrpTalk.QrCodeLogin" %>

<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">
    <title>Grptalk</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/animate.min.css" rel="stylesheet" />
    <!-- Custom CSS -->
    <link href="css/qrcode.css" rel="stylesheet">


    <!-- Colors CSS -->
    <%--<link rel="stylesheet" type="text/css" href="css/color/green.css">--%>
    <!-- Custom Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Kaushan+Script" rel="stylesheet" type="text/css" />

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->

</head>

<body class="index" id="home">

    <!-- Start Home Page Slider -->
    <section id="page-top">
        <!-- Carousel -->
        <div id="main-slide" class="carousel slide" data-ride="carousel">

            <!-- Indicators 
            <ol class="carousel-indicators">
                <li data-target="#main-slide" data-slide-to="0" class=""></li>
                <li data-target="#main-slide" data-slide-to="1" class="active"></li>
                <li data-target="#main-slide" data-slide-to="2" class=""></li>
            </ol>
            Indicators end-->
            <!-- Navigation -->
            <nav class="navbar navbar-default navbar-shrink">
                <div class="container-fluid">
                    <!-- Brand and toggle get grouped for better mobile display -->
                    <div class="navbar-header page-scroll">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                        </button>
                        <a class="navbar-brand page-scroll" href="#page-top">
                            <img src="images/untitled-1@3x.png" style="width: 170px;"></a>
                    </div>

                    <!-- Collect the nav links, forms, and other content for toggling -->
                    <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                        <ul class="nav navbar-nav navbar-right">
                            <li class="hidden active">
                                <a href="#page-top"></a>
                            </li>
                            <li>
                                <a class="page-scroll" href="/Features">Features</a>
                            </li>
                            <li>
                                <a class="page-scroll" href="/Aboutus">About us</a>
                            </li>
                            <li>
                                <a class="page-scroll" href="/Features">Pricing</a>
                            </li>
                            <li>
                                <button class="free-trail-btn" type="button" >Free trail</button>
                            </li>
                        </ul>
                    </div>
                    <!-- /.navbar-collapse -->
                </div>
                <!-- /.container-fluid -->
            </nav>
            <!-- Carousel inner -->
            <div class="carousel-inner">

                <div class="item active">
                    <!-- <img class="img-responsive" src="images/header-back-copy.png" alt="slider">-->
                    <div class="slider-content">
                        <div class="container-fluid" style="padding: 0px 0px 0px 50px;">
                            <div class="row">
                                <div class="col-md-6 col-sm-5 col-xs-12 left-mobile-qrcode">
                                    <div class="qrcode-image" >
                                        <span id="qrChannel" style="display:none;"> </span>
                                        <img id="imgQrCode" src="/images/load.gif" style=" margin-top: 50px;height:185px;width:186px;" alt="QrCode Image not loaded">
                                    </div>

                                    <p class="text-center refresh-code"><a href="javascript:void(0);" id="regenerateQrCode">Refresh Code</a></p>
                                </div>
                                <div class="col-md-6 col-sm-7 col-xs-12 text-left right-cont-qrcode">
                                    <h1 class="animated3">
                                        <span>Use “Grp Talk” on your phone to scan code</span>
                                    </h1>
                                    <p class="animated2">A dial-out audio conferencing application which is an advanced yet simpler version conferencing feature which connects more than 500 people at a go. Available for Android, iOS, Desktop.</p>
                                      
                                    <a href="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&amp;mt=8" class="page-scroll btn btn-primary appstore-qrcode">
                                        <img src="images/apple-logo-white.png" width="24" height="25" style="margin-right: 10px;">
                                        App Store</a>
                                    <a href="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk" class="page-scroll btn btn-primary playstore-qrcode">
                                        <img src="images/apple-logo-white.png" width="24" height="25" style="margin-right: 10px;">
                                        Play Store</a>
                                  

                                    <%--<a href="#" class="chat chat-01">
                                        <img src="images/chat-icon.png" width="60" style="width: 60px; display: block; position: absolute; top: 120%;">
                                    </a>--%>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--/ Carousel item end -->
            </div>
            <!-- Carousel inner end-->
        </div>
        <!-- /carousel -->
    </section>
    <!-- End Home Page Slider -->



    <section class="simplified-forthe-class">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h1>How It Works </h1>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12 no-internet text-center">
                    <img src="images/create-grp-1x.png" class="img-responsive text-center">
                    <h4 class="text-center" style="color: #f19448;">Create Group</h4>
                    <span>Create a grpTalk account, Select the<br>
                        conference’s participants.</span>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12 no-internet text-center">
                    <img src="images/schedule-calll-1x.png" class="img-responsive text-center">
                    <h4 class="text-center" style="color: #f19448;">Initiate/Schedule A Call</h4>
                    <span>Start a call immediately or schedule the<br>
                        call based on your convenience.</span>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12 no-internet text-center">
                    <img src="images/connect-conference-1x.png" class="img-responsive text-center">
                    <h4 class="text-center" style="color: #f19448;">Connect to Conference</h4>
                    <span>Our server will call the participants of the<br>
                        specified group to connect and converse.</span>
                </div>
            </div>
        </div>

    </section>





    <section class="key-features-qrcode">
        <div class="container-fluid" style="padding-left: 0px;">
            <div class="row">
                <div class="col-md-12">
                    <h1>Key Features </h1>
                </div>
                <div class="col-md-5 col-sm-4 hidden-xs key-features-mobile-image">
                    <img src="images/key-features-banner.png" class="img-responsive">
                </div>
                <div class="col-md-7 col-sm-8 col-xs-12 key-features-body-cont">
                    <div class="col-md-6 col-sm-6 col-xs-12 key-features-body-innercont extra-space-bottom">
                        <img src="images/webaccess-1x.png" style="display: block;">
                        <h3>HAS WEB ACCESS</h3>
                        <p>grpTalk app on the desktop is an extension of your mobile account.</p>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12 key-features-body-innercont extra-space-bottom">
                        <img src="images/no-pins-1x.png" style="display: block;">
                        <h3>NO PINS REQUIRED</h3>
                        <p>Connect to your group without remembering confusing PINs or bridge numbers.</p>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12 key-features-body-innercont extra-space-bottom">
                        <img src="images/call-recording-1x.png" style="display: block;">
                        <h3>CALL RECORDING</h3>
                        <p>grpTalk records the conversations which can be used for future reference.</p>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12 key-features-body-innercont extra-space-bottom">
                        <img src="images/schedule-calls-1x.png" style="display: block;">
                        <h3>SCHEDULE CALLS</h3>
                        <p>Schedule conference calls with your group based on your convenience.</p>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12 key-features-body-innercont extra-space-bottom">
                        <img src="images/call-quality-1x.png" style="display: block;">
                        <h3>CRYSTAL CLEAR CALL QUALITY</h3>
                        <p>grpTalk works on PSTN, prevents call drops and provides clear call quality.</p>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12 key-features-body-innercont extra-space-bottom">
                        <img src="images/notification-1x.png" style="display: block;">
                        <h3>NOTIFICATION ALERTS</h3>
                        <p>SMS alerts will be sent to participants remind -ing them about the scheduled call.</p>
                    </div>
                </div>
            </div>
        </div>
    </section>



    <section class="free-trail">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h1>Free Trail</h1>
                    <h4>Free trial- leave your email address for specific<br>
                        packages as they differ based on country</h4>
                    <div class="free-trail-input">
                        <input type="text" value="Email" class="email-no">
                        <button class="btn btn-primary send-btn" value="Send">Send</button>
                    </div>
                </div>
            </div>
        </div>
    </section>



    <section class="testimonial-image">
        <div class="container-fluid" style="padding-left: 0px; padding-right: 0px;">
            <div class="row" style="padding-left: 0px; padding-right: 0px;">
                <div class="col-md-3 col-sm-6 col-xs-12 test-image-01">
                    <img src="images/group-12.png" class="img-responsive">
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 test-cont-01">
                    <div class="col-md-12 text-left">
                        <img src="images/tdp.png" class="img-responsive" width="50">
                        <h4>N. Chandrababu Naidu, </h4>
                        <h6>CM OF ANDHRA PRADESH</h6>
                        <p>
                            Helps me conduct scheduled<br>
                            calls with 10,000+ ministers in<br>
                            other multiple constituencies
                        </p>
                        <p><a href="#">VIEW CASE</a></p>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 test-image-01">
                    <img src="images/group-10.png" class="img-responsive">
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12 test-cont-01 test-cont-02">
                    <div class="col-md-12 text-left">
                        <img src="images/apgvb.png" class="img-responsive" width="50">
                        <h4>Anonymous, </h4>
                        <h6>CHAIRMAN, APGVB BANK</h6>
                        <p>No network issues like connectivity or call drops, allow me to communicate across 10+ branches</p>
                        <p><a href="#">VIEW CASE</a></p>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <!-- starts footer section6 -->

    <footer class="footer-sec">
        <div class="container">
            <div class="row">
                <div class="col-md-12 f-cont" style="padding-bottom: 20px;">
                    <div class="col-md-6 col-sm-6 col-xs-12 grp_logo">
                        <a href="#">
                            <img src="images/f-logo.png" style="padding-top: 15px;"></a>

                        <p>grpTalk is an audio conferencing call application available on Android, iOS and desktop . It enables as many as 500 (or even more) people to converse in a single conference call without the use of PINs. grpTalk is the brainchild of SMSCountry, which has been categorized as a Unified Communications as a Service (UCaaS) company since we also provide audio conferencing services.</p>
                    </div>
                    <div class="col-md-5 col-sm-6 col-xs-12 right-f-cont">
                        <div class="col-md-12 col-xs-12 social-connect">
                            CONNECT WITH US :<br>

                            <div class="col-md-12" style="padding-left: 0px; padding: 30px 0px 30px;">
                                <label style="padding-right: 10px;">
                                    <a href="https://www.facebook.com/grptalkconferencing/">
                                        <img src="images/facebook@3x.png" style="width: 48px;"></a></label><label style="padding-right: 10px;"><a href="https://twitter.com/grptalk"><img src="images/twitter@3x.png" style="width: 48px;"></a></label>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 col-xs-12 f-cont-menu">
                            <ul class="f-cont-li-01">
                                <li><a href="#">Guide</a></li>
                                <li><a href="#">Live training</a></li>
                                <li><a href="#">Feedback</a></li>
                            </ul>
                            <ul class="f-cont-li-02">
                                <li><a href="#">Careers</a></li>
                                <li><a href="#">Case studies</a></li>

                            </ul>

                        </div>

                    </div>
                </div>
                <div class="col-md-12 col-sm-12 col-xs-12 copyright text-center">
                    Copyright 2017 GRPTALK by sms country. all rights reserved.
                </div>
            </div>
        </div>

    </footer>
    <!-- ends footer section6 -->



    <div id="loader" style="display: none;">
        <div class="spinner">
            <div class="dot1"></div>
            <div class="dot2"></div>
        </div>
    </div>



    <!-- jQuery Version 2.1.1 -->
    <script src="js/jquery-1.11.3.min.js"></script>
    <script type="text/javascript" src="js/bowser.js"></script>
        <script src="js/jquery-ui.min.js" type="text/javascript"></script>
    <script src="scripts/qrCode.js"></script>
    <script type="text/javascript" src="scripts/pusher.min.js"></script>
    <script type="text/javascript" src="js/jquery.carouFredSel-6.2.1-packed.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>




</body>

</html>

