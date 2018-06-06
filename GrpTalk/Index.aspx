<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GrpTalk.Index" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="grpTalk is a cloud-based dial-out audio conferencing app that is adaptable, inexpensive and allows 100+ people on one conference call. Learn more about grpTalk today.">
    <meta name="author" content="">
    <%--<meta name="twitter:card" content="summary" />
    <meta name="twitter:site" content="@grptalk" />
    <meta name="twitter:title" content="grpTalk | Audio-Conferencing App" />
    <meta name="twitter:description" content="grpTalk is a cloud-based dial-out audio conferencing app that is adaptable, inexpensive and allows 100+ people on one conference call. Learn more about grpTalk today." />
    <meta name="twitter:image" content="https://www.grptalk.com/img/logo.png" />--%>



    <meta name="twitter:card" content="app">
    <meta name="twitter:site" content="@grptalk">
    <meta name="twitter:description" content="grpTalk is a cloud-based dial-out audio conferencing app that is adaptable, inexpensive and allows 100+ people on one conference call. Learn more about grpTalk today.">
    <meta name="twitter:app:country" content="In">
    <meta name="twitter:app:name:iphone" content="grpTalk">
    <meta name="twitter:app:id:iphone" content="1074172134">
    <meta name="twitter:app:url:iphone" content="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&mt=8">
    <meta name="twitter:app:name:ipad" content="grpTalk">
    <meta name="twitter:app:id:ipad" content="1074172134">
    <meta name="twitter:app:url:ipad" content="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&mt=8">
    <meta name="twitter:app:name:googleplay" content="grpTalkl">
    <meta name="twitter:app:id:googleplay" content="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk">
    <meta name="twitter:app:url:googleplay" content="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk">
    <meta name="msvalidate.01" content="6A3F758A6084F90EC4C6E4BEA3F97F3B" />
    <!-- OGP TAgs -->
    <meta property="og:site_name" content="grpTalk" />
    <meta property="og:type" content="product" />
    <meta property="og:title" content="grpTalk | Audio-Conferencing App" />
    <meta property="og:url" content="https://www.grptalk.com/" />
    <meta property="og:description" content="grpTalk is a cloud-based dial-out audio conferencing app that is adaptable, inexpensive and allows 100+ people on one conference call. 
Learn more about grpTalk today." />
    <!-- OGP TAgs ENd -->
    <title>grpTalk | Audio-Conferencing App</title>

    <link href="css/ninja-slider.css" rel="stylesheet">
    <script src="js/ninja-slider.js"></script>
    <link rel="shortcut icon" href="images/ico/favicon.ico" />
    <link rel="stylesheet" href="css/bootstrap.min.css">

    <link href="css/custom.css" rel="stylesheet">

    <link href="css/owl.carousel.css" rel="stylesheet">
    <link rel="canonical" href="https://www.grptalk.com" />


    <style>
        .navbar-custom.top-nav-collapse.dropdown-menu li img {
            width: 40% !important;
        }

        .navbar-brand {
            padding: 0px 15px 10px !important;
        }
    </style>
    <!-- Custom Fonts -->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css">

    <link href="https://fonts.googleapis.com/css?family=Abel" rel="stylesheet">
    <script type="application/ld+json">
       { "@context" : "http://schema.org",
         "@type" : "Organization",
         "name" : "grpTalk",
         "url" : "https://www.grptalk.com/",
         "logo" : "https://www.grptalk.com/img/logo.png",
         "address" :  {
               "@type" : "PostalAddress",
               "streetAddress": "2nd Floor, Ektha Towers",
               "addressLocality": "White Fields",
               "addressRegion": "Kondapur",
               "postalCode": "500084",
               "telephone" : "91-40-3936-3900"
       },
          "sameAs" : ["https://www.facebook.com/grptalk-275496672856200/", "https://twitter.com/grptalk", "https://web.grptalk.com/" ]
       }
    </script>

    <!-- Google Tag Manager -->
    <script>
        (function (w, d, s, l, i) {
            w[l] = w[l] || []; w[l].push({
                'gtm.start':
                new Date().getTime(), event: 'gtm.js'
            }); var f = d.getElementsByTagName(s)[0],
            j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
            'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
        })(window, document, 'script', 'dataLayer', 'GTM-KXJF8CG');

    </script>
    <!-- End Google Tag Manager -->
</head>

<body id="page-top" data-spy="scroll" data-target=".navbar-fixed-top">

    <!-- Google Tag Manager (noscript) -->
    <noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-KXJF8CG" height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <!-- End Google Tag Manager (noscript) -->

    <div class="cta hidden-xs">
        <i class="fa fa-phone"></i><span>For More Information<br>
            Give Missed Call To 				
					<span class="font-conf-blue bold"><%=missedCallNumber%></span>

        </span>
    </div>

    <!-- Navigation -->
    <div class="header-area">
        <div class="container">
            <div class="row">
                <div class="col-md-8  col-sm-7 col-xs-12">
                    <div class="user-menu">
                        <ul>
                            <li><a href='tel:<%=supportsNumber %>' style="cursor: default !important"><i class="fa fa-phone-square" aria-hidden="true"></i><%=supportsNumber%></a></li>
                            <li><a href="mailto:hello@grpTalk.com"><i class="fa fa-envelope" aria-hidden="true"></i>hello@grpTalk.com</a></li>

                        </ul>
                    </div>
                </div>

                <div class="col-md-4 col-sm-5 col-xs-12">
                    <div class="header-right">
                        <ul class="list-unstyled list-inline">

                            <li><a href="#" style="font-weight: bold;">
                                <img src="img/dubai.png" alt="dubai" title="Bahrain"></a></li>

                            <li><a href="#" style="font-weight: bold;">
                                <img src="img/uae.png" alt="UAE" title="UAE"></a></li>

                            <li><a href="#" style="font-weight: bold;">
                                <img src="img/behrain.png" alt="Bahrain" title="Saudi"></a></li>

                            <li><a href="#" style="font-weight: bold;">
                                <img src="img/india.png" alt="India" title="India"></a></li>

                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End header area -->

    <nav class="navbar navbar-custom navbar-fixed-top" role="navigation">


        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-main-collapse">
                    <i class="fa fa-bars fa-2x"></i>
                </button>
                <a class="navbar-brand page-scroll" href="https://www.grptalk.com">
                    <img src="img/logo.png" class="img-responsive" alt="grpTalk Conference Calls" />
                </a>
            </div>
            <!-- Collect the nav links, forms, and other content for toggling -->
            <div class="collapse navbar-collapse navbar-right navbar-main-collapse">
                <ul class="nav navbar-nav">
                    <!-- Hidden li included to remove active class from about link when scrolled up past about section -->
                    <li class="hidden active">
                        <a href="#page-top"></a>
                    </li>
                    <li>
                        <a href="https://www.grptalk.com"><i class="fa fa-home"></i>HOME</a>
                    </li>
                    <li>
                        <a href="aboutus.aspx"><i class="fa fa-info"></i>About us</a>
                    </li>
                    <li>
                        <a href="how-it-works.aspx">How it works ?</a>
                    </li>
                    <li>
                        <a href="products.aspx"><i class="fa fa-product-hunt" aria-hidden="true"></i>Products</a>
                    </li>


                    <li>
                        <a class="page-scroll" id="freetrail" href="products.aspx">FREE TRIAL</a>
                    </li>

                </ul>
            </div>
        </div>
    </nav>

    <!-- Navigation ends-->

    <!-- Intro Header -->
    <header class="intro">
        <div class="intro-body">
            <div class="container">
                <div class="col-md-12 text-center">
                    <input type="hidden" id="hdnCountryId" value="<%=countryId%>" />
                    <input type="hidden" id="hdnMaxLength" value="<%=maxLength%>" />
                    <input type="hidden" id="hdnMinLength" value="<%=minLength%>" />
                    <h1 class="brand-heading" style="color: #fff;">“ Conference Calling Simplified ”</h1>
                </div>

            </div>
            <div class="container-fluid requestademo">
                <div class="container">
                    <div class="col-md-12 text-center requestademo-01">
                        <div class="requestdemo-body">
                            <div class="col-md-6 col-sm-12 col-xs-12 contact-form">
                                <input type="text" maxlength="<%=maxLength%>" onkeypress="return isNumber(event)" name="mobile" id="txtNumber" placeholder="Please Enter Your Mobile Number" class="form-control subscribe-input input-lg" />

                            </div>
                            <!-- SUBSCRIBE BUTTON -->
                            <div class="col-md-6 col-sm-12 col-xs-12 text-left demo-btn">
                                <button type="submit" id="subscribe-button" class="btn btn-primary btn-lg">Get the App  <i class="fa fa-caret-right hidden-xs"></i></button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </header>


    <!-- simple grp calling section starts -->

    <section id="about-us" class="about-us-section-1">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="section-title text-center">
                        <h3>About <span>Us</span></h3>
                        <div class="border"></div>
                        <div class="border1"></div>
                        <p>We conceptualized grpTalk, a mobile conferencing app, to replace expensive conferencing equipment, and reinvent group calling as you know it. With grpTalk, everyone joins the business audio-conference call at the same time without using PINs or bridge numbers. grpTalk is available on Android, iOS, and desktop.</p>
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-md-4 col-sm-12 col-xs-12">
                    <div class="welcome-section text-center">
                        <img src="img/nopins.png" class="img-responsive" alt="No Pins Required" />
                        <h4>No PINs</h4>
                        <p>
                            You don’t have to remember PINs<br>
                            to connect to the call
                        </p>
                    </div>
                </div>

                <div class="col-md-4 col-sm-12 col-xs-12">
                    <div class="welcome-section text-center">
                        <img src="img/confference.png" class="img-responsive" alt="Instant Conference Calls">
                        <h4>Instantaneous Conference Calls</h4>
                        <p>Participants can join the conference immediately due to the dial­out option.</p>
                    </div>
                </div>

                <div class="col-md-4 col-sm-12 col-xs-12">
                    <div class="welcome-section text-center">

                        <img src="img/crystal.png" class="img-responsive" alt="Good Call Quality" />
                        <h4>Crystal Clear Call Quality</h4>
                        <p>
                            The app prevents network issues<br>
                            and call drops
                        </p>
                    </div>
                </div>

            </div>
            <!-- /.row -->

        </div>
        <!-- /.container -->
    </section>
    <section class="text-center section-title">
        <div class="section-title text-center" style="margin-bottom: 40px;">
            <h3>Is this your regular conference call?</h3>
            <div class="border"></div>
            <div class="border1"></div>
        </div>

        <video controls="controls" height="440" poster="/images/grpTalkVideoBanner.png">
            <source src="/images/grpTalk.mp4" type="video/mp4" />
        </video>
        <div class="text-center" style="margin-top: 10px;">
            <a href="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&amp;mt=8" target="_blank">
                <img src="/images/AppStore_sm.png" alt="" style="margin-right: 5px;" /></a>
            <a href="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk" target="_blank">
                <img src="/images/PlayStore_sm.png" alt="" /></a>
        </div>
    </section>

    <!-- slider starts -->

    <section class="slider-wrapper">
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="section-title text-center">
                        <h3 class="hidden-xs">Traditional Conference Calling Turned Inside out</h3>
                        <h3 class="hidden-lg hidden-md hidden-sm">Conference Calling</h3>
                        <div class="border"></div>
                        <div class="border1"></div>

                    </div>
                </div>
            </div>
            <div id="bg-asset"></div>
            <div id="ninja-slider">
                <div class="slider-inner">
                    <ul style="padding-top: 25%; min-height: 360px;">
                        <li>
                            <div class="content">
                                <img src="img/LargeConferenceCalls.png" alt="Large Conference Calls" />
                                <h3>Large Conference Calls</h3>
                                <p>grpTalk enables you to connect to as many as 500 people (or even more!) through a single conference call. This makes communication easier and saves ample time.</p>
                            </div>
                        </li>
                        <li>
                            <div class="content">
                                <img src="img/syncwebapp.png" alt="Sync With Web App" />
                                <h3>Sync Web and App</h3>
                                <p>
                                    grpTalk app can be accessed on the mobile as well as the desktop. It is an extended version of your phone, where in your phone data is automatically transferred to your web account.
                                </p>
                            </div>
                        </li>
                        <li>
                            <div class="content">
                                <img src="img/call-analytics.png" alt="Call Analytics" />
                                <h3>Call Analytics</h3>
                                <p>grpTalk provides detailed report for every conference call. It includes the names of the participants, call duration, minutes consumed, billing and also the list of the participants who didn’t join the conference.</p>
                            </div>
                        </li>

                    </ul>
                    <div class="fs-icon" title="Expand/Close"></div>
                </div>
            </div>
            <!--end-->
        </div>
    </section>
    <!-- testimonials Section stars-->
    <section id="carousel">
        <div class="container">
            <div class="row">
                <h3 class="text-center" style="font-size: 35pt; color: #fff;">grpTalk Users Speak</h3>
                <div class="border"></div>
                <div class="border1"></div>
                <div class="col-md-10 col-md-offset-1">
                    <div class="carousel slide" id="fade-quote-carousel" data-ride="carousel" data-interval="3000">
                        <!-- Carousel indicators -->
                        <ol class="carousel-indicators">

                            <li data-target="#fade-quote-carousel" data-slide-to="1" class=""></li>
                            <li data-target="#fade-quote-carousel" data-slide-to="2" class=""></li>
                            <li data-target="#fade-quote-carousel" data-slide-to="3" class="active"></li>

                        </ol>
                        <!-- Carousel items -->
                        <div class="carousel-inner">

                            <div class="item">
                                <!-- <div class="profile-circle" style="background-color: rgba(77,5,51,.2);"><img src="img/chandrababu-naidu.png" style="border-radius:50%"></div> -->
                                <blockquote>
                                    <p>grpTalk is one of the best apps I have ever found in terms of call quality. Unlike many other conference calling apps, there are no issues like network connectivity or call drops. It also helps me haveconfidential discussions with my team, thanks to the crystal clear call quality.  </p>
                                    <h4>District Cooperative Central Bank </h4>
                                </blockquote>
                            </div>
                            <div class="item">
                                <div class="profile-circle" style="background-color: rgba(145,169,216,.2);">
                                    <img src="img/chandrababu-naidu.png" style="border-radius: 50%">
                                </div>
                                <blockquote>
                                    <p>It is important for me to coordinate with my ministers, who live in their respective constituencies.For this, I use the grpTalk app to discuss important issues on days when time is a constraint. I make it a point to hold one conference call every week to brief them about the plan of action. </p>
                                    <h4>N. Chandrababu Naidu, Chief Minister of Andhra Pradesh</h4>
                                </blockquote>
                            </div>
                            <div class="item active">
                                <!-- <div class="profile-circle" style="background-color: rgba(77,5,51,.2);"><img src="img/chandrababu-naidu.png" style="border-radius:50%"></div> -->
                                <blockquote>
                                    <p>With so many events lined up every day, it is necessary that we have reminders for calls. With grpTalk app, every morning I schedule my calls and I’m sorted for the day. It sends notifications to the group members on the scheduled time. I have conference calls with my employees using grpTalk and allot them work, whenever necessary. </p>
                                    <h4>Mera Events</h4>
                                </blockquote>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!-- logo sliders-->

    <section id="partner" style="background: #f4f3f3;">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="section-title text-center">
                        <h3 class="text-center" style="font-size: 35pt; color: #333;">Our Customers</h3>
                        <div class="border"></div>
                        <div class="border1"></div>
                    </div>
                </div>
            </div>

            <div class="row logogs">
                <div class="col-md-12">
                    <img class="col-md-2 col-sm-4 col-xs-6 logo img-responsive" src="img/dccb-logo.png" alt="DCCB Bank">
                    <img class="col-md-2 col-sm-4 col-xs-6 logo img-responsive" src="img/kolkatta-police-logo.png" alt="Kolkata Police">
                    <img class="col-md-2 col-sm-4 col-xs-6 logo img-responsive" src="img/tdp-logo.png" alt="TDP Party">
                    <img class="col-md-2 col-sm-4 col-xs-6 logo img-responsive" src="img/mera-events-logo.png" alt="Mera Events">
                    <img class="col-md-2 col-sm-4 col-xs-6 logo img-responsive" src="img/heritage-logo.png" alt="Heritage Company">
                    <img class="col-md-2 col-sm-4 col-xs-6 logo img-responsive" src="img/continental-logo.png" alt="Continental Hospitals">
                </div>
            </div>


        </div>

        <script>
            //$('.carousel').carousel();
        </script>
    </section>


    <!-- contact us -->

    <section id="about-us" class="about-us-section-1">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="section-title text-center">
                        <h3>Contact Us</h3>
                        <div class="border"></div>
                        <div class="border1"></div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="advantage-wrapper">
                    <div class="col-md-4 col-sm-12 col-xs-12">
                        <div class="welcome-section-01 text-center">
                            <div class="col-md-12">
                                <div class="welcome-section-body">
                                    <i class="fa fa-phone fa-5x" aria-hidden="true" style="display: inline-block; vertical-align: middle; width: 100%; padding-top: 38px;"></i>
                                </div>
                            </div>
                            <h4>Call us</h4>
                            <p>
                                Call us on <%=supportsNumber%> or give a missed call.
                                <br>
                                Our team will get back to you.
                            </p>
                            <p>Call: <a href='tel:<%=supportsNumber %>' style="color: #428bca; font-size: 18px; cursor: default !important"><%=supportsNumber%></a></p>
                        </div>
                    </div>

                    <div class="col-md-4 col-sm-12 col-xs-12">
                        <div class="welcome-section-01 text-center">
                            <div class="col-md-12">
                                <div class="welcome-section-body">
                                    <i class="fa fa-laptop fa-5x" aria-hidden="true" style="display: inline-block; vertical-align: middle; width: 100%; padding-top: 38px;"></i>
                                </div>
                            </div>
                            <h4>Request a Demo</h4>
                            <input type="text" id="txtEmail" class="form-control emailid" placeholder="Enter your Email ID">
                            <button type="button" id="btnGetDemo" class="btn btn-primary btn-lg buttons">Request a Demo <span style="text-align: right; padding-left: 20px;">> </span></button>

                        </div>
                    </div>

                    <div class="col-md-4 col-sm-12 col-xs-12">
                        <div class="welcome-section-01 text-center">
                            <div class="col-md-12">
                                <div class="welcome-section-body">
                                    <i class="fa fa-mobile fa-5x" aria-hidden="true" style="display: inline-block; vertical-align: middle; width: 100%; padding-top: 38px;"></i>
                                </div>
                            </div>
                            <h4>Get the app</h4>
                            <input type="text" maxlength="<%=maxLength%>" onkeypress="return isNumber(event)" id="txtNumber2" class="form-control phonenumber" placeholder="Enter your mobile number">
                            <button type="button" id="btnSubscribe" class="btn btn-primary btn-lg buttons">
                                Get the App <span style="text-align: right; padding-left: 20px;">> 
                                </span>
                            </button>
                        </div>
                    </div>

                </div>
                <!-- /.row -->
            </div>
        </div>
        <!-- /.container -->
    </section>



    <!-- footer -->

    <footer id="footer" class="footer">
        <div class="container">
            <div class="row">

                <div class="col-md-6 col-sm-6 col-xs-12 wow fadeInUp animated footer-010" data-wow-duration="500ms">
                    <div class="footer-single">
                        <img src="img/logo.png" alt="" style="width: 30%;">
                        <p>grpTalk is an audio conferencing call application available on Android, iOS and desktop . It enables as many as 500 (or even more) people to converse in a single conference call without the use of PINs. grpTalk is the brainchild of SMSCountry, which has been categorized as a Unified Communications as a Service (UCaaS) company since we also provide audio conferencing services.</p>
                    </div>
                    <div class="contact-details">
                        <!--<div class="con-info clearfix">
								<i class="fa fa-home fa-lg"></i>
								<span><%=address%></span>
							</div>-->

                        <div class="con-info clearfix">
                            <a href='tel:<%=supportsNumber %>'><i class="fa fa-phone fa-lg"></i>
                                <span>Phone: <%=supportsNumber %></span></a>
                        </div>


                        <div class="con-info clearfix">
                            <i class="fa fa-envelope fa-lg"></i>
                            <a href="mailto:hello@grpTalk.com">&nbsp;Email: hello@grpTalk.com</a>
                        </div>
                    </div>
                </div>




                <div class="col-md-3 col-sm-6 col-xs-12 wow fadeInUp animated hidden-xs" data-wow-duration="500ms" data-wow-delay="900ms">
                    <div class="footer-single">
                        <h6>Support</h6>
                        <ul>
                            <li><a href="terms-conditions.aspx">Terms & Conditions</a></li>
                            <li><a href="faqs.aspx">FAQ</a></li>

                        </ul>
                    </div>
                </div>

                <div class="col-md-3 col-sm-6 col-xs-12 wow fadeInUp animated hidden-xs" data-wow-duration="500ms" data-wow-delay="300ms">
                    <div class="footer-single">
                        <h6>get the App </h6>

                        <p style="width: 100%; display: block;">
                            <input type="text" id="txtEmail2" class="form-control phonenumber" placeholder="Enter your Email ID" style="width: 90%; float: left; display: block; border-radius: 0px; margin-bottom: 10px; padding-left: 10px; background: #222222; border: 1px solid #222;">
                        </p>
                        <button type="button" id="btnGetDemo2" class="btn btn-primary btn-lg buttons" style="width: 90%; height: 39px; background: #428bca; border: 1px solid #3778b1; font-size: 12px; font-weight: bold; margin-bottom: 20px;">
                            Request a Demo <span style="text-align: right; padding-left: 20px;">> </span>
                        </button>
                    </div>
                    <div class="contact-details">
                        <div class="con-info clearfix">
                            <a href="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk"><i class="fa fa-android fa-3x" aria-hidden="true" style="color: #68b445;"></i>
                                <span style="margin-top: 15px;">100000 Downloads</span></a>
                        </div>
                        <div class="con-info clearfix">
                            <a href="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&mt=8"><i class="fa fa-apple fa-3x" aria-hidden="true"></i>
                                <span style="margin-top: 15px;">10000+ Downloads</span></a>
                        </div>
                    </div>
                </div>


            </div>

        </div>
    </footer>

    <div class="copyrights">
        <div class="col-md-12">
            <p class="copyright text-center">
                Copyright © 2017 <a href="https://www.smscountry.com/">SMSCountry Networks Pvt Ltd</a>. All rights reserved. Designed & developed by <a href="https://www.smscountry.com/">SMSCountry</a>
            </p>
        </div>
    </div>

    <a id="gotoTop" class="go2top scroll" style="display: block;"><i class="fa fa-arrow-up"></i></a>
    <div id="modalAlert" class="modal fade" role="dialog" style="display: none;" aria-hidden="false" data-keyboard="false" data-backdrop="static">

        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-bottom: 0px ! important;">

                    <button class="close" data-dismiss="modal" style="position: absolute; right: 15px; top: 10px; outline: none ! important" type="button">×</button>
                </div>
                <div class="modal-body" style="background: #fff; padding-bottom: 15px !important;">
                    <span class="pull-center" style="margin-bottom: 0px ! important; margin-top: 0px; color: #606060;" id="alertMsg"></span>
                </div>
                <div class="modal-footer text-center margin-top-20 margin-bottom-20" style="border-top: none; text-align: center;">
                    <button class="btn btn-primary" data-dismiss="modal" type="button">Ok</button>
                </div>

            </div>
        </div>
    </div>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-2.1.4.min.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/jquery.countdown.min.js"></script>
    <script src="js/smoothscroll.js"></script>



    <!-- scroll JavaScript -->
    <script src="js/jquery.nicescroll.min.js"></script>
    <script src="js/owl.carousel.min.js"></script>
    <!-- Custom Theme JavaScript -->
    <script src="js/lovehub.js"></script>
    <script src="scripts/index.js"></script>

    <script>
        $("#freetrail").click(function (e) {
            localStorage.setItem("Trail", 0);
        });

        $(document).ready(function () {
            $('.carousel').carousel();

            $("#gotoTop").click(function (e) {
                window.scrollTo(0, 0);
            });

        });
    </script>


</body>
</html>
