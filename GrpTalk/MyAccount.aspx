<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="GrpTalk.MyAccount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="/css/jquery.jcrop.css" rel="stylesheet" />
    <style type="text/css">
        #userProfile {
            min-height: 415px;
            text-align: center;
        }

        .margin-left-20 {
            margin-left: 20px;
        }

        #userPic {
            background-color: #07080b; /*background-image:url('images/Tulips.jpg') no-repeat;background-size:cover;*/
            padding: 30px 0;
            position: relative;
        }
        /*#blurBG{opacity:0.6;width:100%;height:100%;filter:blur(5px);background-color:#000;-webkit-filter: blur(5px);-moz-filter: blur(5px);-o-filter: blur(5px);filter: blur(5px);position:absolute;top:0;left:0;}*/
        #userDetails {
            padding: 30px 0 30px 0;
            color: #606060;
            font-size: 17px;
            background-color: #f3eded;
        }

        #mobileNumber, #emailId {
            color: #1aae88;
        }

        #changeProfileDetails {
            width: 100%;
            border-radius: 0;
            background-color: #f95259;
            border-bottom: 5px solid #e6434a;
        }

        .webBal, .totalChannels {
            color: #2e5a70;
        }

        .appBal, .maxHits {
            color: #f4a440;
        }

        .tab-content .fa-user, .tab-content .fa-money {
            width: 27px;
            text-align: center;
            background-color: #4989ab;
            padding: 5px 5px 5px 5px;
            color: #fff;
            font-weight: 300;
        }

        .tab-content .fa-envelope-o, .tab-content .fa-file-image-o {
            width: 27px;
            text-align: center;
            background-color: #1aae88;
            padding: 5px 5px 5px 5px;
            color: #fff;
            font-weight: 300;
        }

        #accountTabs {
            margin: 0;
            padding: 0;
            border-bottom: 0 !important;
            background-color: #e8e8e8;
            color: #606060;
        }

            #accountTabs li a {
                color: #fff;
                border: 0;
            }

            #accountTabs li.active a {
                color: #fff;
                border-radius: 0px !important;
                background-color: #909090 !important;
            }

        #saveData {
            background-color: #0082c3;
            border: 1px solid #0082c3;
        }

        #close {
            border: 1px solid #ccc;
        }

        jcrop-circle-demo .jcrop-box {
            position: absolute;
            top: 0px;
            left: 50px;
            width: 300%;
            height: 300%;
            border: 1px rgba(255, 255, 255, 0.4) solid;
            -webkit-box-shadow: 1px 1px 26px #000000;
            -moz-box-shadow: 1px 1px 26px #000000;
            box-shadow: 1px 1px 26px #000000;
            overflow: hidden;
        }

        .jcrop-circle-demo .jcrop-box:focus {
            outline: none;
        }
        /*#avatar_first {width:100%;}*/

        #prof_img {
            border-radius: 50%;
            width: 170px;
            display: inline-block;
        }

        circle, #prntdmem {
            cursor: default !important;
        }

        .nav > li > a:hover, .nav > li > a:focus {
            text-decoration: none;
            color: #fff !important;
        }

        #bg-color {
            background-color: #fff !important;
            margin-top: 15px;
        }

        .footer {
            padding: 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">

    <div class="row">
        <div class="col-md-3 col-sm-3 margin-top-20 margin-bottom-20" id="userProfile">
        </div>
        <div class="col-md-9 col-sm-9 margin-top-20 margin-bottom-20">
            <div class="text-right margin-bottom-20" style="background:#fff;">


                <label style="margin-right: 10px; font-weight:normal;">Selected Time Zone: </label>
                <label class="AllTimeZones" style="width: auto; display: inline;margin-right: 10px; font-weight:normal;">
                </label>


                <div class="clearfix"></div>
            </div>
            <div class="row" id="listTitle" style="background:#e8e8e8; color:#606060;">
                <div class="col-md-6 col-sm-6 text-center">Account Balance</div>
                <div class="col-md-6 col-sm-6 text-center">Lines</div>
            </div>
            <div style="background-color: #FFF; font-size: 13px;" align="center">grpTalk calls you and your members from <span style="color: #39A9D1" id="callerID"></span></div>
            <div class="row" id="bg-color" style="margin-top: 0px !important;">
                <div class="col-md-6 col-sm-6 text-center">
                    <div id="chartContainer" style="height: 250px; width: 100%;">
                    </div>

                    <span class="font-blue-madison font-md webBal" data-step="6" data-intro="Maximum number of participants allowed to be on a conference call together">
                        <i class="glyphicon glyphicon-stop"></i>Web :  <span id="web_inr"></span>Used</span>
                    &nbsp;
                    <span class="font-md font-orange appBal" data-step="7" data-intro="Maximum  number of participants that were  ‘actually present’ during a conference call">
                        <i class="glyphicon glyphicon-stop"></i>App :  <span id="app_inr"></span>Used</span>
                </div>
                <div class="col-md-6 col-sm-6 text-center">
                    <div id="chartContainer1">


                        <div class="dynamic" style="margin: 38px 0 0 0; min-height: 210px;"></div>

                        <span class="font-blue-madison font-md totalChannels" data-step="6" data-intro="Maximum number of participants allowed to be on a conference call together">
                            <i class="glyphicon glyphicon-stop"></i>Available Lines : <span id="tot_channels"></span>
                        </span>
                        &nbsp;
                        <span class="font-md font-orange maxHits" data-step="7" data-intro="Maximum  number of participants that were  ‘actually present’ during a conference call">
                            <i class="glyphicon glyphicon-stop"></i>Maximum Used : <span id="maxhits"></span>Used
                        </span>
                    </div>
                </div>
            </div>

            <div class="row" id="bg-color" style="background:#e8e8e8; color:#606060;">
                <ul class="nav nav-tabs" id="accountTabs">
                    <li>
                        <p style="margin: 7px 0 0 10px; text-align: left;">Account Activities</p>
                    </li>
                    <li class="pull-right" id="recharge" style="display: none"><a data-toggle="tab" href="#sectionB">Recharges</a></li>

                    <li class="active pull-right"><a data-toggle="tab" href="#sectionA">All</a></li>

                </ul>
                <div class="tab-content">
                    <div id="sectionA" class="tab-pane fade in active">
                    </div>
                    <div id="sectionB" class="tab-pane fade">
                    </div>

                </div>
            </div>
        </div>



        <!-- Modal -->
        <div id="editprofile" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" style="outline: none !important">&times;</button>
                        <h4 class="modal-title">Edit Profile</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="name">Name:</label>
                            <input type="text" placeholder="Nick name" class="form-control" id="userName" maxlength="20" />
                            <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                        </div>
                        <div class="form-group">
                            <label for="email">Email ID:</label>
                            <input type="text" placeholder="Mail Id" class="form-control" id="emailID" maxlength="50" />
                            <span class="text-danger" id="errorDescForEmail" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                        </div>
                        <div class="form-group">
                            <label for="file">Profile Pic:</label>
                            <div style="" id="imageCrop">
                                <img src="images/avatar-img-1.png" id="avatar_first" alt="Smiley face" height="200" width="190" /><br />
                            </div>
                            <input type="file" id="profileUpload" name="userfile" value="Choose File" style="visibility: hidden" />
                            <input type="button" value="Choose image" onclick="javascript: document.getElementById('profileUpload').click();" />
                            <%--<input type="file" id="fup_html_dup" style="display: none" name="userfile" value="Choose File" />--%>

                            <%--<a href="javascript:" class="btn default fileupload-exists" data-dismiss="fileupload" id="btnremove">Remove</a>--%>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-danger" id="saveData">Save</button>
                        <button class="btn btn-default" id="close" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagejs" runat="server">

    <script src="scripts/jquery.plugin.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/jquery.Jcrop.js"></script>
    <script type="text/javascript" src="Scripts/jquery.fileupload.js"></script>
    <script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/underscore-min.js"></script>
    <script type="text/javascript" src="Scripts/venn.js"></script>
    <script type="text/javascript" src="Scripts/d3.v2.min.js"></script>
    <!--<script type="text/javascript" src="http://canvasjs.com/assets/script/canvasjs.min.js"></script>-->
    <script src="Scripts/canvasjs.min.js"></script>
    <script src="Scripts/blurIt.js"></script>
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="Scripts/myaccount.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            //window.onload = function () {
            //    CanvasJS.addColorSet("greenShades",
            //        [//colorSet Array
            //        "#cccccc",
            //        "#63c5ae",
            //        "#2e5a70"
            //        ]);
            //    var chart = new CanvasJS.Chart("chartContainer", {
            //        theme: "theme2",//theme1
            //        colorSet: "greenShades",
            //        title: {
            //            text: "INR " + crntBalInRupees + " Current Balance",
            //            verticalAlign: "center",
            //            fontSize: "16",
            //            wrap: true,
            //            dockInsidePlotArea: true,
            //            maxWidth: "120",
            //            fontColor: "#37acd7"
            //        },
            //        legend: {
            //            verticalAlign: "bottom",
            //            horizontalAlign: "left",
            //            fontSize: "13"

            //        },
            //        animationEnabled: false,
            //        // change to true
            //        data: [
            //        {
            //            // Change type to "bar", "area", "spline", "pie",etc.
            //            type: "doughnut",
            //            showInLegend: false,
            //            radius: "70%",
            //            innerRadius: "80%",
            //            dataPoints: [
            //           { y: wBal, toolTipContent: webBalInRupees },
            //           { y: crnBal, toolTipContent: crntBalInRupees },
            //           { y: apBal, toolTipContent: appBalInRupees },

            //            ]
            //        }
            //        ]
            //    });
            //    chart.render();
            //}
        });





    </script>

</asp:Content>
