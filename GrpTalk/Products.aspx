<%@ Page Title="" Language="C#" MasterPageFile="~/LandingMaster.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="GrpTalk.Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title>grpTalk | Call Conferencing on App and Web</title>
    <meta name="description" content="With top of the line premium and basic versions, and an exciting range of features, grpTalk proves to be the complete, end-to-end audio conferencing solution." />
    <section class="header-strip" style="background: #2e2a2a; padding: 70px 0 100px;">
        <div class="freetrail-strip">
            <div class="container">
                <div class="row">
                    <div class="col-md-7 col-sm-12">Download the app & Get 300 minutes FREE everyday for 30 days<br>
                        <small style="font-weight: normal">(speak with up to 20 members for 15 minutes)</small></div>
                    <div class="col-md-5 col-sm-12">
                        <form autocomplete="on">
                            <div class="row">
                                <div class="col-md-7">
                                    <input type="text" id="txtNumber" onkeypress="return isNumber(event)" class="form-control subscribe-input input-lg" style="color: black !important; color: #555 !important; background-color: #fff !important; height: 39px !important; border-right: 1px; font-size: 12px ! important;" placeholder="Enter your mobile number" />
                                </div>
                                <div class="col-md-5">
                                    <div class="">
                                        <a id="subscribe-button" class="btn send-btn" style="height: 39px !important; font-size: 12px ! important;">Get the app</a>
                                    </div>

                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <!--grpTalk Plans Features starts-->
    <section style="background: #2e2a2a; padding: 100px 0px 30px;">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12" style="padding-bottom: 20px;">
                    <div class="section-title text-center">
                        <h3 style="color: #FFFFFF">grpTalk Plans &amp; Features</h3>
                        <div class="border"></div>
                        <div class="border1"></div>
                        <p style="color: #FFFFFF">grpTalk has two versions for use- Basic and Premium. The following chart briefly indicates the features available for both the versions. </p>
                    </div>
                </div>
            </div>
            <img src="img/pricing-02.png" class="img-responsive" style="margin: 0px auto; display: block" />
        </div>
    </section>
    <section class="reports" style="background: #f4f3f3; text-align: center">
        <div class="container">
            <div class="row">
                <div class="col-md-12 col-sm-12" style="padding-bottom: 50px;">
                    <div class="section-title text-center">
                        <h3>Detailed Call Reports</h3>
                        <div class="border"></div>
                        <div class="border1"></div>
                        <p>grpTalk enables users to view in-depth reports of the conference calls that they’ve had.  The reports include details like member information, call duration and cost.  The user can also choose to record conversations and download them. This helps to avoid the hassle of jotting down important information and is useful for future reference.</p>
                    </div>
                </div>
            </div>
            <img src="img/call analytics.png" class="img-responsive text-center" style="width: 80%; margin: 0 auto; display: block;" />
        </div>
    </section>

    <!-- jQuery -->
    <script src="https://code.jquery.com/jquery-2.1.4.min.js"></script>

    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

    <!-- Plugin JavaScript -->
    <script src="js/jquery.easing.min.js"></script>
    <script src="js/smoothscroll.js"></script>



    <!-- scroll JavaScript -->
    <script src="js/jquery.nicescroll.min.js"></script>

    <!-- Custom Theme JavaScript -->

    <script>

        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
                m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-1057679-2', 'auto');
        ga('send', 'pageview');
        $(document).ready(function (e) {
            $("#txtNumber2").attr('maxlength', $("#hdnMaxLength").val());
            if (localStorage.getItem("Trail") == null) {
                $('.header-strip').hide();
            }
            else {
                $('.header-strip').show();
                localStorage.clear();
            }
        });
    </script>


    <div id="ascrail2000" class="nicescroll-rails" style="width: 8px; z-index: 9999; position: fixed; top: 0px; height: 100%; right: 0px; cursor: default; opacity: 0;">
        <div style="position: relative; float: right; width: 8px; background-color: rgb(108, 182, 112); border: 0px; background-clip: padding-box; border-radius: 0px; height: 150px; top: 3px;"></div>
    </div>

    <div id="ascrail2000-hr" class="nicescroll-rails" style="height: 8px; z-index: 9999; position: fixed; left: 0px; width: 100%; bottom: 0px; cursor: default; display: none; opacity: 0;">
        <div style="position: relative; top: 0px; height: 8px; background-color: rgb(108, 182, 112); border: 0px; background-clip: padding-box; border-radius: 0px; width: 150px;"></div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
