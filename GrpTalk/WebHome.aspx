<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebHome.aspx.cs" Inherits="GrpTalk.WebHome" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <meta name="google-site-verification" content="m-zJg6Hzqs3xTIpoX7iBs-NlnfqgFYrSa1KxgNnkP8U" />

    <title>GrpTalk Web</title>
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <style type="text/css">
        body{background:#1c83c3; font-family:Calibri;}
        .main-box{background:#f4f4f4; border-radius:10px !important; padding:25px; margin:20px;}
        .margin-top-40{margin-top:40px;}
        .margin-top-30{margin-top:30px;}
        .margin-bottom-40{margin-bottom:40px;}
        .block{display:block;}
        img{page-break-inside:avoid}img{max-width:100%!important}
        .qr-code-box{background:#fff; padding:10px; border-radius:15px !important; width:auto;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
       <div class="main-box">
           <div class="row">
               <div class="col-sm-5">
                   <div class="text-center">
                       <label class="margin-top-40 margin-bottom-40"><img src="images/grp-web-logo.png" /></label>
                       <label><img src="images/grp-web-mob.png" /></label>
                   </div>
               </div>
               <div class="col-sm-7">
                   <div class="row margin-top-30">
                       <div class="col-sm-7">
                           <label class="qr-code-box">
                               <span id="qrChannel" style="display:none;"> </span>
                               <img id="imgQrCode" src="images/qr-code.jpg" alt="qr-code" />
                           </label>

                       </div>
                       <div class="col-sm-5 text-center">
                           <h4 style="color:#3680c0; line-height:23px;">Use grpTalk on your phone to scan the code</h4>
                           <label style="font-weight:normal; font-size:13px;">Open grpTalk <i class="fa fa-angle-right"></i> menu <i class="fa fa-angle-right"></i> grpTalk Web</label>
                       </div>
                   </div>

                   <hr />
                   <div class="row">
                       <div class="col-sm-6 text-center">
                           <label class="block">Download Android App</label>
                           <img src="images/g-play.png" alt="Play Store" />
                       </div>
                       <div class="col-sm-6 text-center">
                           <label class="block">Download IOS App</label>
                           <img src="images/app-store.png" alt="App store" />
                       </div>
                   </div>
               </div>
           </div>
       </div>
    </div>
    </form>
    <script src="js/jquery.js" type="text/javascript"></script>
	<script type="text/javascript" src="scripts/pusher.min.js"></script>
    <script type="text/javascript" src="scripts/Home.js"></script>
	<script type="text/javascript">
		var google_conversion_id = 923156976;
		var google_custom_params = window.google_tag_params;
		var google_remarketing_only = true;
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
