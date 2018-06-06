<%@ Page Title="" Language="C#" MasterPageFile="~/PreLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="GrpTalk.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CssContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">
    <section class="awards">
        <div class="container">
            <div class="row">
                <div class="col-md-1 col-sm-2 col-xs-4">
                    <img src="images/bitmap-copy@3x.png" class="img-responsive" width="44" style="display: block; margin: 0px auto">
                </div>
                <div class="col-md-2 col-sm-3 col-xs-4" style="padding-left: 0px; padding-right: 0px;">
                    <img src="images/IE20@1x.png" class="img-responsive">
                </div>
                <div class="col-md-8 col-sm-5 col-xs-12 award-text hidden-xs">
                    SMSCountry named one of the top 20 Emerging Companies in
India by London & Partners for grptalk
                </div>
                <div class="col-md-1 col-sm-2 col-xs-4">
                    <img src="images/bitmap-right@3x.png" class="img-responsive" width="44" style="display: block; margin: 0px auto">
                </div>

            </div>
            <div class="award-text show-xs">
                SMSCountry named one of the top 20 Emerging Companies in
India by London & Partners for grptalk
            </div>
        </div>
    </section>
    <section class="no-pin-calls">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <h1>No Pin Out Bound Calls</h1>
                            <h3>Reduce the average joining time by 300% on our outbound conference calling app:</h3>
                            <p>
                                No need to enter long bridge or PINs to join calls
Dial-out ensures that calls begin with minimal delay<br>
                                Outbound calling ensures control over who joins
                            </p>
                        </div>

                        <div class="col-md-6 col-xs-12 col-sm-6">
                            <a href="https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&amp;mt=8" class="page-scroll btn btn-primary app-store">
                                <img src="images/apple-logo@3x.png" style="width: 20px; margin-top: -5px; margin-right: 10px;">App store</a>
                        </div>
                        <div class="col-md-6 col-xs-12 col-sm-6">
                            <a href="https://play.google.com/store/apps/details?id=com.mobile.android.grptalk" class="page-scroll btn btn-primary app-store">
                                <img src="images/bitmap@3x copy.png" style="width: 20px; margin-top: -5px; margin-right: 10px;">Google play</a>
                        </div>
                    </div>

                    <div class="col-md-6 text-center">
                        <img src="images/i-phone-6-silver-copy.png">
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section class="regular-landlines">
        <div class="container">
            <div class="row">
                <div class="col-md-12">


                    <div class="col-md-6 col-sm-6 col-md-push-6">
                        <div class="col-md-12">
                            <h1>Conferencing On Regular Landlines</h1>
                            <h3>Add 3-500 people on one call with complete member visibility and control:</h3>
                            <p>
                                Working with PSTN lines, superlative call quality is ensured<br>
                                Eliminate the need for expensive audio conferencing systems<br>
                                Handraise, Mute, Umute,  help manage the call better
                            </p>
                        </div>
                        <div class="col-md-12 text-left">
                          <%--  <a href="#feature" class="page-scroll btn btn-primary app-store">
                                <img src="images/shape@3x.png" style="width: 20px; margin-top: -5px; margin-right: 10px;">CONTACT SALES</a>--%>
                            <div class="free-trail-input" style="margin:0px; width:80%">
                                <input type="text" placeholder="Email" class="email-no" style="border-color:#fc5055;" id="txtEmail2" autocomplete="off">
                                <button class="btn btn-primary send-btn" value="Send" id="btnGetDemo2" style="height:55px;"><i class="fa fa-send-o"></i></button>
                            </div>
                        </div>

                    </div>
                    <div class=" col-md-6 col-sm-6 col-md-pull-6 text-center">
                        <img src="images/regular-landlines.png" class="img-responsive">
                    </div>


                </div>
            </div>
        </div>
    </section>








    <section class="qr-code">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-6 col-sm-6">
                        <div class="col-md-12">
                            <h1>Shift From Phone To Desktop Easily</h1>
                            <h3>Manage 500+ people on one call with our smart web interface:</h3>
                            <p>
                                Get in-depth call analytics for every minute of the call<br>
                                Use our detailed dashboard for easy call management<br>
                                Upload multiple contacts directly from Excel sheets
                            </p>
                        </div>
                        <div class="col-md-12">
                            <a href="/Login" class="page-scroll btn btn-primary app-store">
                                <img src="images/qr.png" style="width: 20px; margin-top: -5px; margin-right: 10px;">Scan QR Code</a>
                        </div>

                    </div>

                    <div class="col-md-6 col-sm-6 text-center">
                        <img src="images/laptop.png" class="img-responsive lapy_img">
                    </div>
                </div>
            </div>
        </div>
    </section>



    <section class="simplified-forthe-class">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h1>Simplified For The Participants </h1>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12 no-internet text-center">
                    <img src="images/noun-750001.png" class="img-responsive text-center">
                    <h4 class="text-center">No Interent</h4>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12 no-internet text-center">
                    <img src="images/noun-749980.png" class="img-responsive text-center">
                    <h4 class="text-center">No App</h4>
                </div>
                <div class="col-md-4 col-sm-4 col-xs-12 no-internet text-center">
                    <img src="images/noun-749625.png" class="img-responsive text-center">
                    <h4 class="text-center">Connect to Conference</h4>
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
                        <input type="text" placeholder="Email" class="email-no" maxlength="50" id="txtEmail" autocomplete="off" style="outline:none !important" >
                        <button class="btn btn-primary send-btn" type="button" value="Send" id="btnGetDemo"><i class="fa fa-send-o"></i></button>
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
                            calls with 10,000+ ministers in other multiple constituencies
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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

