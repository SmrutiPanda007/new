<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="InProgress.aspx.cs" Inherits="GrpTalk_New.InProgress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagetitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagecss" runat="server">
    <style type="text/css">
        .list.active {background-color:#1c252b;}
        .list.active .calendar {background:none;}
        .list.active .calendar p{color:#fff;}
        .list.active .participantsList p:nth-child(2) {color:#fff !important; }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" runat="server">
    <div id="banner">
        <span class="blink_me">Monthly sales meet (55 Members) Call in Progress - 30:21:15</span>
    </div>
    <div class="row">
        <div class="col-md-4 col-sm-4 margin-top-20 margin-bottom-20" id="callLogs">
            <div class="row" style="box-shadow: 1px 1px 3px #000000;">
                <button type="button" class="btn btn-primary" id="createGrpTalk"><span class="fa fa-plus"></span>Create grpTalk</button>
                <div class="list margin-bottom-1">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 padding-left-0 padding-right-0 calendar">
                            <p style="font-family: Impact; font-size: 30px; line-height: 56px;color:#27bd7c;" class="blink_me">Live</p>
                        </div>
                        <div class="col-md-10 col-sm-10 padding-left-0 padding-right-0 participantsList">
                            <p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">Monthly Sales Meet</p>
                            <p style="font-family: Calibri; color: #263238; font-size: 16px; line-height: 15px;">Kamal Smsc, Krishna, Harshitha, and 2 more...</p>
                            <p style="font-family: Calibri; color: #27bd7c; font-size: 15px; font-weight: bold;line-height: 18px;">Call in Progress - 30:21:15</p>
                        </div>
                    </div>
                </div>
                <div class="list margin-bottom-1">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 padding-left-0 padding-right-0 calendar">
                            <p style="font-family: Impact; font-size: 40px; line-height: 36px;">13</p>
                            <p style="font-family: Calibri; font-size: 18px; line-height: 20px;">May</p>
                        </div>
                        <div class="col-md-10 col-sm-10 padding-left-0 padding-right-0 participantsList">
                            <p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">Monthly Sales Meet</p>
                            <p style="font-family: Calibri; color: #263238; font-size: 16px; line-height: 15px;">Kamal Smsc, Krishna, Harshitha, Sagar,  Anusha and 2 more...</p>
                        </div>
                    </div>
                </div>
                <div class="list margin-bottom-1">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 padding-left-0 padding-right-0 calendar">
                            <p style="font-family: Impact; font-size: 40px; line-height: 36px;">14</p>
                            <p style="font-family: Calibri; font-size: 18px; line-height: 20px;">May</p>
                        </div>
                        <div class="col-md-10 col-sm-10 padding-left-0 padding-right-0 participantsList">
                            <p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">Monthly Sales Meet</p>
                            <p style="font-family: Calibri; color: #263238; font-size: 16px; line-height: 15px;">Kamal Smsc, Krishna, Harshitha, Sagar,  Anusha and 2 more...</p>
                        </div>
                    </div>
                </div>
                <div class="list margin-bottom-1">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 padding-left-0 padding-right-0 calendar">
                            <p style="font-family: Impact; font-size: 40px; line-height: 36px;">15</p>
                            <p style="font-family: Calibri; font-size: 18px; line-height: 20px;">May</p>
                        </div>
                        <div class="col-md-10 col-sm-10 padding-left-0 padding-right-0 participantsList">
                            <p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">Monthly Sales Meet</p>
                            <p style="font-family: Calibri; color: #263238; font-size: 16px; line-height: 15px;">Kamal Smsc, Krishna, Harshitha, Sagar,  Anusha and 2 more...</p>
                        </div>
                    </div>
                </div>
                <button type="button" class="btn btn-primary margin-top-2 margin-bottom-3" id="loadMore">LOAD MORE</button>
            </div>
        </div>
        <div class="col-md-8 col-sm-8 margin-top-20 margin-bottom-20">
            <div class="row" id="listTitle">
                <div class="col-md-12 col-sm-12">
                    <b>Monthly Sales Meet</b>
                    <%--<ul>
                        <li><i aria-hidden="true" class="fa fa-phone"></i></li>
                        <li><i aria-hidden="true" class="fa fa-pencil"></i></li>
                        <li><i aria-hidden="true" class="fa fa-trash-o"></i></li>
                    </ul>--%>
                </div>
            </div>
            <div class="row margin-bottom-20" id="inProgressTimer">
                <p style="font-size: 20px; color: #27bd7c; font-weight: 600;" class="margin-bottom-0">Call in Progress</p>
                <div class="timer margin-bottom-30"></div>
                <div class="row">
                    <div class="col-md-12 col-sm-12">
                        <ul id="inProgressActions">
                            <li id="mute"><a href="javascript:void(0);"><img width="20" src="images/Mute1.png" alt="Mute" /></a></li>
                            <li id="addMember"><a href="javascript:;"><img width="28" alt="Add Member" src="images/AddMember1.png" /></a></li>
                            <li id="hangup"><a class="text-center" href="javascript:void(0);"><img width="28" alt="Hangup" src="images/Hangup1.png" /></a></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row margin-top-20">
                <div class="col-md-12 col-sm-12 padding-left-0 padding-right-0">
                    <ul class="nav nav-tabs padding-left-0" id="myTab">
                        <li class="active allMembers"><a data-toggle="tab" href="#allMembers">All Members</a></li>
                        <li class="onCall"><a data-toggle="tab" href="#onCall">On Call</a></li>
                        <li class="callEnded"><a data-toggle="tab" href="#callEnded">Call Ended</a></li>
                        <li class="muted"><a data-toggle="tab" href="#muted">Muted</a></li>
                        <li class="wantsToTalk"><a data-toggle="tab" href="#wantsToTalk" class="margin-right-0">Wants To Talk</a></li>
                    </ul>
                    <form class="navbar-form" role="search" id="inProgressSearch">
                        <div class="input-group" style="width:100%;">
                            <input type="text" class="form-control" placeholder="Search" name="q" />
                            <div class="input-group-btn" style="width:5%;">
                                <button class="btn btn-default" id="search" type="submit"><i class="glyphicon glyphicon-search"></i></button>
                            </div>
                        </div>
                    </form>
                    <div class="tab-content">
                        <div id="allMembers" class="tab-pane fade in active">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Mobile Number</th>
                                        <th>Status</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Participant1</td>
                                        <td>0123456789</td>
                                        <td></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td><span style="color: red;">Participant2</span></td>
                                        <td>9874563210</td>
                                        <td><span style="color: green;">Wants to Talk</span></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="30" alt="" src="images/MuteIcon.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/Call.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Participant3</td>
                                        <td>8523697401</td>
                                        <td></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/Redial.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Participant4</td>
                                        <td>8523697401</td>
                                        <td></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="18" alt="" src="images/Unmute.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/Call.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Participant5</td>
                                        <td>8523697401</td>
                                        <td></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="30" alt="" src="images/MuteIcon.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Participant 6</td>
                                        <td>8523697401</td>
                                        <td></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="18" alt="" src="images/Unmute.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/Call.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                                
                            </table>
                        </div>
                        <div id="onCall" class="tab-pane fade">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Mobile Number</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Participant1</td>
                                        <td>1234569870</td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="18" alt="" src="images/Unmute.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Participant2</td>
                                        <td>9874563210</td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="30" alt="" src="images/MuteIcon.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Participant3</td>
                                        <td>8523697401</td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="18" alt="" src="images/Unmute.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="callEnded" class="tab-pane fade">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Mobile Number</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Participant3</td>
                                        <td>8523697401</td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/Redial.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="muted" class="tab-pane fade">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Mobile Number</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Participant2</td>
                                        <td>8523697401</td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="30" alt="" src="images/MuteIcon.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="22" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div id="wantsToTalk" class="tab-pane fade">
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Mobile Number</th>
                                        <th>Status</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td><span style="color: red;">Participant2</span></td>
                                        <td>1234569870</td>
                                        <td><span style="color: green;">Wants to Talk</span></td>
                                        <td>
                                            <ul>
                                                <li><a href="javascript:void(0);">
                                                    <img width="30" alt="" src="images/MuteIcon.png" /></a></li>
                                                <li><a href="javascript:void(0);">
                                                    <img width="20" alt="" src="images/IndividualHangup.png" /></a></li>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pagejs" runat="server">
    <script src="js/timer.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".list").click(function () {
                $(".list").removeClass("active");
                $(this).addClass("active");
            });
            function blinker() {
                $('.blink_me').fadeOut(600);
                $('.blink_me').fadeIn(600);
            }
            setInterval(blinker, 500);
        });
        $(function () {
            $('.timer').timer({
                format: '%H:%M:%S'
            });
        });
        $(function () {
            $('.tab-content').slimScroll({
                allowPageScroll: true,
                height: '165'
               
            });
        });
    </script>
</asp:Content>
