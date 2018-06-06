<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="MyGrpTalks.aspx.cs" Inherits="GrpTalk.MyGrpTalks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="css/dialpad.css" />
    <%--<link rel="stylesheet" href="css/jquery-ui.css" />--%>
    <style type="text/css">
         section#header .navbar-inverse, section#header, .navbar-inverse{
            background: #f9a944; /* For browsers that do not support gradients */
  background: -webkit-linear-gradient(left, #f9a944 , #d6335c) !important; /* For Safari 5.1 to 6.0 */
  background: -o-linear-gradient(right, #f9a944, #d6335c) !important; /* For Opera 11.1 to 12.0 */
  background: -moz-linear-gradient(right, #f9a944, #d6335c) !important; /* For Firefox 3.6 to 15 */
  background: linear-gradient(to right, #f9a944 , #d6335c) !important; /* Standard syntax */
        }
        
        /*#contactsModalDiv .contacts, .contactDetails {
            width: 24%;
        }*/
        #startTmier{color:#d6335c;}
        /*.list.active span#quickDial{color:#fff !important;}*/
        .CallLogName{font-weight:normal !important;}
        #contactsModalDiv {
            width: 24%;
        }

        .selected {
            position: relative;
        }

            .selected i.select_check {
                position: absolute;
                top: 35px;
                right: 5px;
                font-size: 17px;
                color: #00cd67;
            }

        #myTab li {
            width: 16.5% !important;
        }

        .digits p {
            margin: 0 0 0px !important;
            background: #defbff !important;
            text-align: center;
        }

            .digits p strong {
                color: #0a93d7 !important;
            }

        .dialpad .dials .digits p {
            border: 1px solid #81dcec !important;
        }

        .dialpad .number {
            font-size: 15px !important;
        }

        #dialPadFeature .form-control {
            background: #fff !important;
        }

        .number {
            padding: 15px 168px !important;
        }

        .unselected {
            position: relative;
        }

            .unselected i.select_check {
                position: absolute;
                top: 35px;
                right: 5px;
                font-size: 17px;
                color: #00cd67;
            }

        #contactsModalDiv .modal-dialog {
            width: 80% !important;
        }

        #contactsModalDiv .modal-body {
            overflow: hidden;
        }

        #selectedContacts #profilePic, #grpCallMobileContacts #profilePic, #contactsModalDiv #profilePic {
            float: left;
            margin-right: 8px;
        }

            #selectedContacts #profilePic img, #grpCallMobileContacts #profilePic img, #contactsModalDiv #profilePic img {
                width: 50px;
            }

        #selectedContacts .profileDetails, #grpCallMobileContacts .profileDetails, #contactsModalDiv #profileDetails {
            float: left;
        }

            #selectedContacts .profileDetails p, #grpCallMobileContacts .profileDetails p, #contactsModalDiv #profileDetails p {
                margin: 0;
                white-space: nowrap;
            }

                #selectedContacts .profileDetails p:last-child, #grpCallMobileContacts .profileDetails p:last-child, #contactsModalDiv #profileDetails p:last-child {
                    color: #0a93d7;
                }

        #contactsModalDiv #createGrpList .fa-plus {
            padding: 13px 32px;
        }

        #contactsModalDiv #listTitle1 ul li a {
            color: #fff;
        }

        .list.active {
            background-color: #fff;
        }

            .list.active #date {
                background-color: #fff;
                border-right:1px solid #ccc;
            }
        #date {
            border-right:1px solid #ccc;
        }
            .list.active .calendar {
                background: none;
            }

                .list.active .calendar p {
                    color: #fff;
                }

            /*.list.active .participantsList p:nth-child(2) {
                color: #fff !important;
            }

            .list.active .participantnames p:last-child{
                color: #fff !important;
            }*/

                /*.list.active .participantnames p:first-child {
                    color: #ffdbbd !important;
                }*/
        .participantnames p:last-child {
            color:#333;
        }
        .list1.active {
            background-color: #1c252b;
        }

            .list1.active .calendar {
                background: none;
            }

                .list1.active .calendar p {
                    color: #fff;
                }

            .list1.active .participantsList p:nth-child(2) {
                color: #fff !important;
            }

        #reportsSearch {
            margin: 0 0 0 0 !important;
        }

        #reportsTab li a {
            border: 1px solid #ccc;
            border-radius: 0;
            margin-right: -1px;
            color: #000;
        }

        #reportsTab li.active a {
            color: #f6454b;
        }

        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default {
            background: none !important;
        }

        .ui-accordion .ui-accordion-header {
            background-color: #e8e8e8 !important;
            color: #606060;
            border-radius: 0;
            font-size: 14px;
        }

        .ui-state-active .ui-icon, .ui-state-default .ui-icon {
            background-image: url("images/ui-icons_888888_256x240.png") !important;
        }

        .ui-widget-content {
            padding: 15px 30px;
        }

        #accordion .table th, #accordion .table td {
            font-size: 12px;
            text-align: center;
        }

        #accordion ul {
            list-style-type: none;
            font-size: 12px;
            padding: 0;
        }

            #accordion ul li {
                margin-bottom: 10px;
            }

                #accordion ul li:last-child {
                    margin-bottom: 0;
                }

        #subscribeNow {
            background-color: #0282c2;
            padding: 10px;
            color: #fff;
            font-size: 12px;
            text-align: center;
            overflow: hidden;
        }

        #subscribe {
            padding: 1px 10px;
            border: 0;
            color: #000;
            font-size: 13px;
        }

        #members {
            background-color: #e3e3e3;
            padding: 10px;
            overflow: hidden;
        }

        .contacts, .contactDetails {
            background-color: #fff;
            padding: 10px;
            width: 32.9%;
            float: left;
            position: relative;
        }

        #profilePic {
            float: left;
            margin-right: 8px;
        }

            #profilePic img {
                width: 50px;
            }

        #profileDetails {
            float: left;
        }

            #profileDetails p {
                margin: 0;
                white-space: nowrap;
            }

                #profileDetails p:last-child {
                    color: #909090;
                }

        .contact_actions {
            position: absolute;
            top: 0px;
            right: 10px;
        }

            .contact_actions a {
                margin: 0 5px;
            }

            .contact_actions .fa-trash-o {
                color: red;
            }

            .contact_actions .fa-edit {
                color: orange;
            }

        #contactsSearch {
            margin: 0 0 0 0 !important;
        }

        #contactsTab li a {
            border: 1px solid #ccc;
            border-radius: 0;
            margin-right: -1px;
            color: #000;
        }

        #contactsTab li.active a {
            color: #0082c3;
        }

        #webLists, #phoneContacts {
            background-color: #e3e3e3;
            padding: 10px;
            overflow: hidden;
        }

        .fa-ellipsis-v {
            cursor: pointer; /*position:absolute;top:7px;right:2px;float:right;*/
        }

        .fa-ellipsis-v {
            display: none !important;
        }


        #listTitle ul li a {
            color: #fff;
        }

        #listTitle1 ul li a {
            color: #fff;
        }

        .jcrop-circle-demo .jcrop-box:focus {
            outline: none;
        }

        .nogrpTalkCalls {
            width: 85%;
            position: absolute;
            top: 50% !important;
            left: 50% !important;
            transform: translate(-50%, -50%);
            font-size: 18px;
            color: #404040;
            font-weight: 600;
            text-align: center;
            height: none !important;
        }

        .highlight {
            background-color: #e9724f !important;
        }

        #webLists .list1, #list-group1 .list1 { /*overflow:hidden;*/
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #0678b0;
            background-color: #0a93d7;
            height: 47px;
            padding: 10px;
            color: #fff;
        }

            #webLists .list1:hover {
                background-color: #0282c2 !important;
            }

            #webLists .list1 a, #list-group1 .list1 a {
                color: #fff;
            }

        .alphabet {
            list-style-type: none;
            /*position: absolute;
            top: 5px;
            right: 10px;*/
            padding: 0;
            cursor: pointer;
            text-align: center;
            line-height: 15px;
            width: 1%;
            float: right;
        }

            .alphabet li {
                margin: 0;
                padding: 0;
                font-size: 11px;
                -moz-box-sizing: border-box;
                color: black;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                box-sizing: border-box;
                width: 3.84%;
            }

                .alphabet li:last-child {
                    border-right: none;
                }

                .alphabet li:hover {
                    color: green;
                    background-color: lightgrey;
                }

        .ribbon-wrapper-green {
            width: 83px;
            height: 85px;
            overflow: hidden;
            position: absolute;
            top: 0px;
            right: 0px;
        }
        <!--
        .ribbon-green {
            font: bold 12px Sans-Serif;
            color: #333;
            text-align: center;
            text-shadow: rgba(255,255,255,0.5) 0px 1px 0px;
            -webkit-transform: rotate(45deg);
            -moz-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            -o-transform: rotate(45deg);
            position: absolute;
            padding: 6px 0;
            left: -2px;
            top: 15px;
            width: 120px;
            background-color: #BFDC7A;
            background-image: -webkit-gradient(linear, left top, left bottom, from(#DC7A7A), to(#BF4545));
            background-image: -webkit-linear-gradient(top, #DC7A7A, #BF4545);
            background-image: -moz-linear-gradient(top, #DC7A7A, #BF4545);
            background-image: -ms-linear-gradient(top, #DC7A7A, #BF4545);
            background-image: -o-linear-gradient(top, #DC7A7A, #BF4545);
            color: #6a6340;
            -webkit-box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
            -moz-box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
            box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
        }
        -->

        .ribbon-green {
            font-size: 12px;
            text-align: center;
            position: absolute;
            padding: 0px 0;
            left: -1px;
            top: 8px;
            width: 128px;
            color: #ff0000;
            -webkit-box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
            -moz-box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
            box-shadow: 0px 0px 3px rgba(0,0,0,0.3);
            border: 1px solid #ff0000;
            background: #f9f9f9;
        }

            .ribbon-green:before, .ribbon-green:after {
                content: "";
                border-top: 3px solid #6e8900;
                border-left: 3px solid transparent;
                border-right: 3px solid transparent;
                position: absolute;
                bottom: -3px;
            }

            .ribbon-green:before {
                left: 0;
            }

            .ribbon-green:after {
                right: 0;
            }

        ​ .dropdown-menu {
            min-width: 115px !important;
        }

        .dropdown-menu li a {
            color: #000 !important;
        }

        .nav-tabs {
            border-bottom: 1px solid transparent !important;
        }

        #listTitle, #listTitle1 {
            margin-bottom: 0px;
            background:#fff !important;
            color:#404040 !important;
        }

            #listTitle ul, #listTitle1 ul {
                width: 100%;
            }
        /*.btn-group .btn {margin-left: 0 !important;}*/

        /*.btn-group .btn:hover {background-color:#0282c2 !important;}*/
        /*#list-group .btn {border-radius:0;border-bottom:1px solid #0678b0;background-color:#0a93d7;}*/
        #list-group .list { /*overflow:hidden;*/
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #e6dada;
            background-color: #f3eded;
            height: 47px;
            padding: 10px;
            color: #fff;
        }

            #list-group .list a {
                color: #fff;
            }

        .tab-content {
            position: relative;
        }

        #addCntct {
            background-color: #0a93d7;
            border: 0;
            border: 1px solid #0a93d7;
        }

        #close {
            border: 1px solid #ccc;
        }

        #addWebListContacts .modal-dialog {
            width: 800px;
        }

        .dropdown-menu-right {
            top: 35px !important;
        }

        #grpCallWebContacts, #grpCallMobileContacts, #selectedContacts, #alphabetMobileContacts, #alphabetWebContacts {
            background-color: #e3e3e3;
            overflow: hidden;
            padding: 10px;
        }

        .btn-success {
            background-color: #45b6af !important;
            border-color: #3ea49d;
            color: #fff;
        }

        .startschedule input[type="text"] {
            border-color: -moz-use-text-color -moz-use-text-color orange;
            border-style: none none solid;
            border-width: 0 0 1px;
            padding: 7px 0;
            text-align: center;
            width: 35%;
        }

        #startNow {
            background-color: #0082c3;
            border: 2px solid #0082c3;
        }

        #schedule {
            border: 2px solid #ccc;
        }

        .text-success {
            color: rgb(79, 138, 16) !important;
            font-size: 15px;
            font-weight: bold;
            float: left;
        }

        .text-danger {
            color: #a94442 !important;
            font-size: 15px;
            font-weight: bold;
            float: left;
        }

        .fa-check {
            float: left;
            font-size: 20px;
            margin-left: 160px;
            margin-top: -32px;
        }

        #listTitle1 {
            background-color: #263238;
            color: #fff;
            padding: 10px 0;
        }

            #listTitle1 ul {
                list-style-type: none;
            }

                #listTitle1 ul li a {
                    color: #fff;
                }

        #groupCallButtons > li {
            float: right;
            list-style: none;
            line-height: 35px;
            padding: 0px 5px;
        }

            #groupCallButtons > li.fa {
                width: 20px !important;
                height: 20px !important;
            }

        .tbl_bg {
            background-color: #fff;
        }

        #createGrpTalk, #createGrpList {
            line-height: 35px !important;
        }

            #createGrpTalk .fa-plus, #createGrpList .fa-plus {
                padding: 13px 7px !important;
            }

        #mute, #addMember, #hangup {
            padding: 0px !important;
            background-color: #fff !important;
        }

        #btnInProgressSearch {
            border-bottom-width: 0px !important;
            padding: 11px 16px !important;
            background-color: #ccc !important;
            border-radius: 0px !important;
        }

        .modal-header {
            padding: 5px 15px !important;
        }

        .modal-body {
            padding: 0px 20px !important;
        }

        ul#myTabList {
            border-bottom: 1px solid #e4e4e4 !important;
        }

        .modal-header .close {
            margin-top: 4px !important;
        }

        .reportsTable table th {
            background: #f1f1f1;
        }

        .clip-down:hover i {
            color: #38eb26 !important;
        }

        /*#grpTalkCallsList > div > table td:first-child {
            color: #e5e4e4;
        }*/
        /*#grpTalkCallsList > .list.active > table td:first-child {
            color: #fff;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row">
        <!-- page div starts here-->
        <div class="col-md-3 col-sm-12 margin-top-20 margin-bottom-20" id="callLogs">
            <!--- callLogsLists div starts here-->
            <div class="row" style="box-shadow: 1px 1px 3px #bbb;">
                <button type="button" class="btn btn-white" id="createGrpTalk">
                    <i class="fa fa-plus"></i>Create Group</button>
                <div id="grpTalkCallsList">
                </div>

                <%--<button type="button" class="btn btn-primary margin-top-2 margin-bottom-3" id="loadMore">LOAD MORE</button>--%>
            </div>
        </div>
        <!--- callLogsLists div ends here-->










        <div class="col-md-9 col-sm-12 margin-top-20 margin-bottom-20" id="membersLists">

            <div class="row" id="listTitle" style="background:#fff !important;">
                <!--call list header div starts here-->
                <div class="col-md-12 col-sm-12">
                    <div class="col-md-3 col-sm-2"><span class="CallLogName"></span></div>
                    <div align="center" class="col-md-6 col-sm-7"><span class="bschedule"></span></div>
                    <div class="col-md-3 col-sm-3">
                        <ul id="groupCallButtons">
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom-20">

                <ul class="nav nav-tabs padding-left-0 margin-bottom-20" id="reportsTab">
                    <li style="width: 75%;">
                        <form class="navbar-form" role="search" id="reportsSearch">
                            <div class="input-group" style="width: 100%;" id="txtSearch">
                                <input type="text" class="form-control" placeholder="Search" id="searchByName" />
                                <div class="input-group-btn" style="width: 3%;" id="inputSearch">
                                    <button class="btn btn-default" id="searchBtn" type="submit"><i class="glyphicon glyphicon-search"></i></button>
                                </div>
                            </div>
                        </form>
                    </li>
                    <li class="active" style="width: 13%;"><a id="mem" data-toggle="tab" href="#members">Members</a></li>
                    <li style="width: 12%;"><a data-toggle="tab" href="#history">History</a></li>
                </ul>
                <div class="tab-content" style="height: 437px;">
                    <div id="members" class="tab-pane fade in active">
                    </div>
                    <div id="history" class="tab-pane fade">
                        <div id="accordion">
                        </div>
                        <div id="more" style="display: none"></div>
                    </div>
                </div>
            </div>
            <div class="pinStrip row" style="background-color: #606060; text-align: center; color: #fff; display: none"></div>



        </div>

        <div class="col-md-9 col-sm-12 margin-top-20 margin-bottom-20" id="inProgress" style="display: none;">
            <div class="row" id="listTitle1">


                <div id="CallName" class="col-md-4 col-sm-4">
                    <b class="CallLogName" style="font-size: 18px; line-height: 35px">InProgress </b>

                </div>
                <div id="startTmier" class="col-md-4 col-sm-4 text-center" style="font-size: 25px; padding-top: 5px;">
                </div>
                <div class="col-md-4 col-sm-4">
                    <div id="hostActions" class="row pull-right"></div>

                </div>
            </div>
            <div class="moderator" style="width: 30%">
            </div>
            <div class="row margin-top-20">
                <div class="col-md-12 col-sm-12 padding-left-0 padding-right-0">
                    <ul class="nav nav-tabs padding-left-0" id="myTab">
                        <li class="allMembers"><a data-toggle="tab" href="">All Members<span id="allMembersCount"></span> </a></li>
                        <li class="onCall"><a data-toggle="tab" href="">On Call<span id="onCallCount"></span></a></li>
                        <li class="callEnded"><a data-toggle="tab" href="">Call Ended<span id="callEndedCount"></span></a></li>
                        <li class="muted"><a data-toggle="tab" href="">UnMuted<span id="mutedCount"></span></a></li>
                        <li class="wantsToTalk"><a data-toggle="tab" href="" class="margin-right-0">Wants To Talk<span id="handRaiseCount"></span></a></li>
                        <li class="privateroom"><a data-toggle="tab" href="" class="margin-right-0">Private Room<span id="privateroom"></span></a></li>
                    </ul>
                    <form class="navbar-form" role="search" id="inProgressSearch" style="margin-bottom: 0 !important;">
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-sm-12" id="searchDiv" style="padding-left: 0px;">
                                <div class="input-group margin-bottom-20" style="width: 100%;">
                                    <input type="text" id="memberSearch" class="form-control" placeholder="Search" autocomplete="off" />
                                    <div class="input-group-btn" style="width: 5%;">
                                        <button class="btn btn-default" id="btnInProgressSearch" href="javascript:void(0);"><i class="glyphicon glyphicon-search"></i></button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 text-right">
                                <button id="btnUnmuteAll" type="button" class="btn btn-danger" style="display: none;">Mute All</button>
                                <button id="btnUnmuteAllWT" type="button" style="display: none;" class="btn btn-danger">Unmute All</button>
                                <button id="btnDialAll" type="button" class="btn btn-danger" style="display: none;">Dial All</button>
                                <button id="btnClosePrivate" type="button" class="btn btn-danger" style="display: none;">Close Private Room</button>
                            </div>
                        </div>
                    </form>

                    <div class="tab-content" style="background: #fff; overflow-y: auto;" id="liveCallDetails">
                    </div>
                    <div class="pinStrip" style="background-color: #606060; text-align: center; color: #fff; display: none"></div>
                </div>
            </div>
        </div>
    </div>
    <!-- page div ends here-->

    <!--------DIAL PAD------->
    <div id="dialPadFeature" class="modal fade" role="dialog" style="width: 80% ! important; left: 112px; overflow: hidden; display: none;" aria-hidden="true">
        <div class="modal-dialog" style="height: 640px !important; width: 100% !important;">

            <div id="wrapper" role="dialog" style="display: block;" aria-hidden="false">
                <div class="dialpad compact modal-dialog" style="width: 420px ! important">
                    <div class="modal-header">
                        <button class="close" data-dismiss="modal" type="button" style="position: absolute; z-index: 99; top: 11px; right: 5px;">×</button>
                    </div>
                    <div class="modal-body" style="background: #fff; padding-bottom: 15px !important;">
                        <div class="row" style="margin-bottom: 15px; padding-top: 15px;">
                            <label class="col-sm-3" style="padding-left: 0px;">
                                <select class="form-control" id="ddlCountryList">
                                    <option value="10">+91</option>
                                    <option value="8">+973</option>
                                    <option value="10">+1</option>
                                    <option value="8">+965</option>
                                    <option value="8">+968</option>
                                    <option value="8">+974</option>
                                    <option value="9">+966</option>
                                    <option value="9">+971</option>

                                </select>
                            </label>
                            <label class="col-sm-9" style="padding-left: 0px; padding-right: 0px;">
                                <input type="text" class="form-control" id="number" maxlength="10" />
                            </label>
                        </div>

                        <div class="dials">
                            <ol>
                                <li class="digits">
                                    <p><strong>1</strong></p>
                                </li>
                                <li class="digits">
                                    <p><strong>2</strong><sup>abc</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>3</strong><sup>def</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>4</strong><sup>ghi</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>5</strong><sup>jkl</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>6</strong><sup>mno</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>7</strong><sup>pqrs</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>8</strong><sup>tuv</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>9</strong><sup>wxyz</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>*</strong></p>
                                </li>
                                <li class="digits">
                                    <p><strong>0</strong><sup>+</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong>#</strong></p>
                                </li>
                                <li class="digits">
                                    <p><strong><i class="fa fa-refresh"></i></strong><sup>Clear</sup></p>
                                </li>
                                <li class="digits">
                                    <p><strong><i class="fa fa-times"></i></strong><sup>Delete</sup></p>
                                </li>
                                <li class="digits pad-action">
                                    <p>
                                        <strong><i class="fa fa-phone"></i></strong><sup>Cal
						l</sup>
                                    </p>
                                </li>
                            </ol>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--------------Dial Pad ENDS---------->
    <div id="dialMutedial" class="modal fade" role="dialog" style="display: none;" aria-hidden="false" data-keyboard="false" data-backdrop="static">

        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" style="border-bottom: 0px ! important;">
                    <h4 class="pull-center" style="margin-bottom: 0px ! important; margin-top: 0px; color: #606060;"><span>Are you sure you want to dial the call?</span></h4>
                    <button class="close" data-dismiss="modal" style="position: absolute; right: 15px; top: 10px;" type="button">×</button>
                </div>



                <div class="modal-footer text-center margin-top-20 margin-bottom-20" style="border-top: none; text-align: center;">
                    <span class="alert alert-danger" id="ScheduleErrorMessage" style="display: none"></span>
                    <button type="button" class="btn btn-success" style="width: 100px;" data-dismiss="modal" id="dial1">Dial</button>
                    <button type="button" class="btn btn-danger" style="width: 100px;" data-dismiss="modal" id="muteDialdiv1">Mute Dial</button>
                    <button type="button" class="btn" style="width: 100px;" id="cancel">Cancel</button>
                </div>

            </div>
        </div>
    </div>
    <!-------- Modal Popup for Add memeber while call in inporgress--------------->
    <div id="contactsModalDiv" class="modal fade" role="dialog" style="display: none; width: 80% !important; left: 112px; overflow: hidden;">
        <div class="modal-dialog" style="height: 640px !important; width: 100% !important;">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class='pull-left' style="margin-bottom: 0px;">Add Member</h4>
                    <button class="close" data-dismiss="modal" type="button">×</button>
                    <div class='clearfix'></div>
                </div>
                <div class="modal-body" style="">
                    <%--<div class="container contactsContainer">--%>
                    <div class="row">

                        <div class="col-md-12 col-sm-12 margin-top-10 margin-bottom-20">
                            <!--  <div class="row" id="listTitleDiv">
               <div class="col-md-12 col-sm-12">
                    <b>Add Contacts</b>
                    <ul>
                        <li>
                            <a href="javascript:;"  class="addNewContact"><i aria-hidden="true" class="fa fa-plus-square"></i></a></li>
                        <li><a href="#tab2" data-toggle="tab">Section 2</a></li>
                    </ul>
                </div> 
            </div>-->

                            <div class="margin-bottom-20">

                                <form class="navbar-form" role="search" id="contactsSearch">
                                    <div class="input-group" style="width: 100%;">
                                        <input type="text" class="form-control" placeholder="Search" id="search-input" autocomplete="off" />
                                        <div class="input-group-btn" style="width: 5%;">
                                            <button class="btn btn-default" id="search" type="submit"><i class="glyphicon glyphicon-search"></i></button>
                                        </div>
                                    </div>
                                </form>

                            </div>
                            <div>


                                <div class="row margin-bottom-20">
                                    <button style="float: right; right: 15px; display: block; border: 0; margin-top: 8px;" class="btn btn-success select-all" id="selectAll">select all</button>
                                    <button id="unSelectAll" class="btn btn-success select-all" style="float: right; right: 15px; display: none; border: 0px none; margin-top: 8px;">Unselect all</button>
                                    <ul class="nav nav-tabs padding-left-0 margin-bottom-20 " id="myTabList">
                                        <!-- <li class="active webContacts"><a data-toggle="tab" href="#grpCallWebContacts" listID="0">Web Lists</a></li> -->
                                        <li class="active mobileContacts" id="phoneContact"><a data-toggle="tab" href="#grpCallMobileContacts">Phone Contacts</a></li>

                                        <li class="contactTab"><a data-toggle="tab" href="#webContacts">Web List</a></li>
                                        <li class="selectedContacts"><a data-toggle="tab" href="#selectedContacts" style="display: none">Selected Contacts<span class="count">(0)</span></a></li>
                                    </ul>
                                    <div class="tab-content1">
                                        <div id="grpCallMobileContacts" class="tab-pane fade active in">
                                        </div>

                                        <div id="webContacts" class="tab-pane">
                                            <div class="row">
                                                <div class="col-sm-3">
                                                    <div class="scroll" style="overflow: auto;" id="webLists">
                                                    </div>
                                                </div>
                                                <div class="col-sm-9" style="padding-right: 0px;">
                                                    <div id="grpCallWebContacts">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="selectedContacts" class="tab-pane ">
                                        </div>
                                        <!-- <ul class="alphabet">
                        <li>A</li>
                        <li>B</li>
                        <li>C</li>
                        <li>D</li>
                        <li>E</li>
                        <li>F</li>
                        <li>G</li>
                        <li>H</li>
                        <li>I</li>
                        <li>J</li>
                        <li>K</li>
                        <li>L</li>
                        <li>M</li>
                        <li>N</li>
                        <li>O</li>
                        <li>P</li>
                        <li>Q</li>
                        <li>R</li>
                        <li>S</li>
                        <li>T</li>
                        <li>U</li>
                        <li>V</li>
                        <li>W</li>
                        <li>X</li>
                        <li>Y</li>
                        <li>Z</li>
                    </ul> -->
                                    </div>
                                </div>
                            </div>
                            <!-- <div class="col-md-4 col-sm-4 margin-top-20 margin-bottom-20" id="callLog">
            <div class="row">
              
                <div id="list-group1">
                       <div class="list1">
							<div style="width: 100%; float: left;">
								<a style="display: block;" href="javascript:;">All Contacts</a>
							</div>
                        </div>
                    <div id="contactList">

                    </div>
                </div>
            </div>
        </div> -->
                        </div>


                    </div>

                    <div class="modal-footer" style="text-align: center;">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-danger" grpcallid="" id="addCall">Add Member</button>
                    </div>



                </div>
            </div>
        </div>
    </div>
    <div id="contactsModal" class="modal fade" role="dialog" style="display: none;">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content" style="width: 800px">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <div>
                        <ul class="nav nav-tabs padding-left-0">
                            <li class="active"><a data-toggle="tab" id="newContact">Add Contacts</a></li>
                        </ul>
                    </div>
                </div>
                <div class="modal-body">
                    <!-------------For adding Single Contacts in Web List--------------------->
                    <!-- <div class="tab-content" style="min-height: 325px;"> -->
                    <div id="contactFormBody" style="display: block;">

                        <div class="row">
                            <div class="col-sm-6">
                                <label for="name">Choose a web list:</label>
                                <select id="ddlWebList" class="form-control" style="outline: none ! important"></select>

                            </div>
                            <div class="col-sm-6">
                                <label for="newWebList">Create a Web List:</label>
                                <input type="text" placeholder="Enter new Weblist Name" class="form-control" id="newWebList" maxlength="20" />

                            </div>
                            <span class="text-danger" id="errorDescForWebList" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <label for="name">Name:</label>
                                <input type="text" placeholder="Nick name" class="form-control" id="name" maxlength="20" />
                                <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                            </div>
                            <div class="col-sm-6">
                                <label for="mobile">Mobile Number ID:</label>
                                <input type="text" placeholder="Mobile Number" class="form-control" id="mobileNumber" maxlength="12" onkeypress="return isNumber(event)" disabled />
                                <span class="text-danger" id="errorDescForMobile" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                            </div>
                        </div>

                        <div class="row" style="margin-top: 15px">
                            <div class="col-sm-12">
                                <label for="fileUpld">Image Upload:</label>
                                <input type="button" value="Choose Image" onclick="javascript: document.getElementById('imageUpld').click();" />
                                <input type="file" id="imageUpld" style="visibility: hidden" />

                                <img id="webContactProfile" alt="profile pic" style="width: 150px; height: 150px;" src="" />
                            </div>
                        </div>



                    </div>


                    <!-------------For adding Bulk Contacts through Excel Sheet in Web List--------------------->

                    <div class="modal-footer">
                        <span class="text-success" id="successMessage"></span>
                        <span class="text-danger" id="errorMessage"></span>
                        <button type="button" class="btn btn-default" id="contactClose" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-danger" id="saveContact">Add Contact</button>
                        <button type="button" class="btn btn-success" id="saveExcelContacts" style="display: none;">Upload</button>
                    </div>
                </div>
                <input type="hidden" id="hdnCountryId" value="<%= Session["CountryID"]%>" />
            </div>
        </div>
    </div>

    <!-------- Modal Popup for Add memeber while call in inporgress--------------->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagejs" runat="server">
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="scripts/pusher.min.js"></script>
    <script type="text/javascript" src="scripts/mygrptalks.js?v=123456"></script>
    <script type="text/javascript" src="scripts/addContact.js?type=v5"></script>
    <script type="text/javascript" src="scripts/creategrouptalk.js?type=v6"></script>
    <script src="js/timer.jquery.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            var sliceNumber = 0;
            $('sup').remove();
            $('.tab-content').slimScroll({
                allowPageScroll: false,
                height: '400'
            });
            $('#list-group1').slimScroll({
                allowPageScroll: false,
                height: '300'
            });

            $('#webLists').slimScroll({
                allowPageScroll: false,
                height: '440'
            });
            $("#liveCallDetails").slimscroll({
                allowPageScroll: false,
                height: '400'
            });

        });
        $(function () {

            var dials = $(".dials ol li");
            var index;
            var number = $("#number").val();

            var total;

            dials.click(function () {
                var numberValue = $("#number").val();
                index = dials.index(this);
                var intValue = $("#number").val();
                if (numberValue.length < parseInt($('#number').attr("maxlength"))) {

                    if (index == 9) {

                        return false;

                    } else if (index == 10) {

                        intValue += "0";

                    } else if (index == 11) {

                        return false;

                    }
                    else if (index != 13) {
                        intValue += (index + 1);
                    }
                    if ($('#hdnCountryId').val() == "108") {
                        if ($("#number").val().length == 0) {
                            if (index == 6 || index == 7 || index == 8) {
                                $("#number").val(intValue);
                            }
                            else {
                                return false;
                            }
                        }
                    }
                    $("#number").val(intValue);
                }
                if (index == 12) {
                    $("#number").val('');

                } else if (index == 13) {

                    total = $("#number").val();
                    total = total.slice(0, -1);
                    $("#number").val(total);

                }
                else if (index == 14) {

                    if ($("#number").val().length == 10) {
                        dialPadClick();
                        $('#dialPadFeature .close').click();
                    }
                    else {
                        var countryId = $.trim($('#hdnCountryId').val());
                        if (countryId == 173 || countryId == 19 || countryId == 124) {
                            if ($("#number").val().length == 8) {
                                dialPadClick();
                                $('#dialPadFeature .close').click();
                            }
                            else {
                                Notifier('Please enter a valid number', 2);
                                return false;
                            }
                        }
                        else {
                            Notifier('Please enter a valid number', 2);
                            return false;
                        }
                    }


                }

            });

            $('#number').keydown(function (e) {

                switch (e.which) {

                    case 96:

                        number.append("0");
                        break;

                    case 97:

                        number.append("1");
                        break;

                    case 98:

                        number.append("2");
                        break;

                    case 99:

                        number.append("3");
                        break;

                    case 100:

                        number.append("4");
                        break;

                    case 101:

                        number.append("5");
                        break;

                    case 102:

                        number.append("6");
                        break;

                    case 103:

                        number.append("7");
                        break;

                    case 104:

                        number.append("8");
                        break;

                    case 105:

                        number.append("9");
                        break;

                    case 8:

                        total = number;
                        total = total.slice(0, -1);
                        number.append(total);
                        break;

                    case 27:

                        number.empty();
                        break;

                    case 106:

                        number.append("*");
                        break;

                    case 35:

                        number.append("#");
                        break;

                    case 13:

                        $('.pad-action').click();
                        break;

                    default: return;
                }

                e.preventDefault();
            });
        });

        function dialPadClick() {
            var minlength = sliceNumberFn();
            var quickdial = true;
            $('.table-bordered tr').each(function (e) {
                if ($('.table-bordered tr td:eq(1)').text().slice(minlength) == $("#number").val().slice(minlength)) {
                    Notifier('Member already in Call', 2);
                    quickdial = false;
                }
            })
            if (quickdial) {
                participants = ""; resStr = "", response = "";
                participants += '{"' + $("#number").val() + '":"' + $("#number").val() + '","ListId":"0"}' + ",";
                resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";
                response = '{"ConferenceID":"' + grpCallID + '",' + resStr + ',"WebListIds":""}';
                addMemeberWhileOnCall(response, mode);
            }
        }
        function sliceNumberFn() {
            var countryId = $('#hdnCountryId').val()
            sliceNumber = 0;
            if (countryId == 108 || countryId == 241) {
                sliceNumber = -10;
            }
            else if (countryId == 19 || countryId == 124 || countryId == 173) {
                sliceNumber = -8;
            }
            else if (countryId == 199 || countryId == 239)
            { sliceNumber = -9; }
            else
            { sliceNumber = -7; }
            return sliceNumber;
        }

    </script>

</asp:Content>
