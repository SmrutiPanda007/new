<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeFile="MyContacts.aspx.cs" Inherits="GrpTalk.MyContacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="/css/jquery.jcrop.css" rel="stylesheet" />
    <style type="text/css">
        .list.active {
            background-color: #1c252b;
        }

       jcrop-circle-demo .jcrop-box {
            position: absolute;
            top: 0px;
            left: 50px;
            width: 300%;
            height: 300%;
            border: 1px rgba(255, 255, 255, 0.4) solid;
            border-radius: 50%;
            -webkit-box-shadow: 1px 1px 26px #000000;
            -moz-box-shadow: 1px 1px 26px #000000;
            box-shadow: 1px 1px 26px #000000;
            overflow: hidden;
        }

        .jcrop-circle-demo .jcrop-box:focus {
            outline: none;
        }

        .custom-shade {
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            opacity: 0.4;
            width: 100%;
            height: 100%;
        }


        .list.active .calendar {
            background: none;
        }

            .list.active .calendar p {
                color: #fff;
            }

        .list.active .participantsList p:nth-child(2) {
            color: #fff !important;
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

        .selected {
            border: 1px solid green !important;
        }

        #webLists, #phoneContacts, #alphabetWebContacts, #alphabetMobileContacts {
            background-color: #e3e3e3;
            padding: 10px;
            overflow: hidden;
        }

        .contacts {
            background-color: #fff;
            padding: 10px;
            width: 32.9%;
            float: left;
            position: relative;
        }

        #profilePic {
            float: left;
            width: 21%;
        }

            #profilePic img {
                width: 50px;
            }

        #profileDetails {
            float: right;
            width: 77%;
        }

            #profileDetails p {
                margin: 0;
                white-space: nowrap;
            }

                #profileDetails p:last-child {
                    color: #0a93d7;
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

        /*.fa-ellipsis-v {
            cursor: pointer;
            position: absolute;
            top: 7px;
            right: 2px;
            float: right;
        }*/

        #listTitle ul li a {
            color: #fff;
        }

        .dropdown-menu {
            min-width: 115px !important;
        }

            .dropdown-menu li a {
                color: #000 !important;
            }
        /*.btn-group .btn {margin-left: 0 !important;}*/

        /*.btn-group .btn:hover {background-color:#0282c2 !important;}*/
        /*#list-group .btn {border-radius:0;border-bottom:1px solid #0678b0;background-color:#0a93d7;}*/
        #list-group .list {
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #0678b0;
            background-color: #0a93d7;
            height: 47px;
            padding: 10px;
            color: #fff;
        }

            #list-group .list a {
                color: #fff;
                display: block;
            }

        .tab-content {
            position: relative;
        }

        .alphabet {
            list-style-type: none;
            position: absolute;
            top: 5px;
            right: 10px;
            padding: 0;
            cursor: pointer;
            text-align: center;
            line-height: 15px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row">
        <div class="col-md-4 col-sm-4 margin-top-20 margin-bottom-20" id="callLogs">
            <div class="row">
                <button type="button" class="btn btn-primary margin-bottom-20" id="createGrpList"><span class="fa fa-plus"></span>Create New List</button>
                <div id="list-group">
                    <%--<div class="list">
                        <a href="javascript:;" id="getAllContacts">All Contacts</a><i class="fa fa-ellipsis-v dropdown dropdown-toggle" data-toggle="dropdown" aria-hidden="true"></i>
                        <ul class="dropdown-menu dropdown-menu-right">
                            <li class="addNewContact" listid="0"><a href="#">Add Contacts</a></li>
                        </ul>
                    </div>--%>
                </div>
                <!-- Phone Contacts Sidebar -->
                <button type="button" class="btn btn-primary margin-bottom-20" style="display: none; width: 100%; pointer-events: none; border: 0; background-color: #0a93d7; border-radius: 0; font-size: 18px; font-weight: bold;" id="phoneContactsDisplay">Phone Contacts</button>
                <div class="phnCntcts" style="min-height: 450px;">
                    <div id="phoneContactsText" style="display: none; width: 85%; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); font-size: 18px; color: #428bca; font-weight: 600; text-align: center;">You can only modify phone contacts from your mobile device only</div>
                </div>
                <!-- Phone Contacts Sidebar Ends -->

            </div>
        </div>
        <div class="col-md-8 col-sm-8 margin-top-20 margin-bottom-20">
            <div class="row" id="listTitle">
                <div class="col-md-12 col-sm-12">
                    <b>Add Contacts</b>
                    <ul>
                        <li listid="0" unique="modal"><a href="javascript:;" data-toggle="modal" data-target="#addWebListContacts" listid="0"><i aria-hidden="true" class="fa fa-plus-square"></i></a></li>
                    </ul>
                </div>
            </div>
            <div class="row margin-bottom-20">
                <ul class="nav nav-tabs padding-left-0 margin-bottom-20" id="contactsTab">
                    <li style="width: 74%;">
                        <form class="navbar-form" role="search" id="contactsSearch">
                            <div class="input-group" style="width: 100%;">
                                <input type="text" id="search-input" class="form-control" placeholder="Search" name="q" />
                                <div class="input-group-btn" style="width: 5%;">
                                    <button class="btn btn-default" id="search" type="button"><i class="glyphicon glyphicon-search"></i></button>
                                </div>
                            </div>
                        </form>
                    </li>
                    <li class="active webContacts"><a data-toggle="tab" href="#webLists">Web Lists</a></li>
                    <li class="mobileContacts"><a data-toggle="tab" href="#phoneContacts">Phone Contacts</a></li>
                </ul>
                <div class="tab-content">
                    <div id="webLists" class="tab-pane fade in active">
                    </div>
                    <div id="alphabetWebContacts" class="tab-pane fade">
                    </div>
                    <div id="phoneContacts" class="tab-pane fade">
                    </div>
                    <div id="alphabetMobileContacts" class="tab-pane fade">
                    </div>
                    <ul class="alphabet">
                        <li id="specialCharSearch">#</li>
                        <li prop="alpha">A</li>
                        <li prop="alpha">B</li>
                        <li prop="alpha">C</li>
                        <li prop="alpha">D</li>
                        <li prop="alpha">E</li>
                        <li prop="alpha">F</li>
                        <li prop="alpha">G</li>
                        <li prop="alpha">H</li>
                        <li prop="alpha">I</li>
                        <li prop="alpha">J</li>
                        <li prop="alpha">K</li>
                        <li prop="alpha">L</li>
                        <li prop="alpha">M</li>
                        <li prop="alpha">N</li>
                        <li prop="alpha">O</li>
                        <li prop="alpha">P</li>
                        <li prop="alpha">Q</li>
                        <li prop="alpha">R</li>
                        <li prop="alpha">S</li>
                        <li prop="alpha">T</li>
                        <li prop="alpha">U</li>
                        <li prop="alpha">V</li>
                        <li prop="alpha">W</li>
                        <li prop="alpha">X</li>
                        <li prop="alpha">Y</li>
                        <li prop="alpha">Z</li>
                    </ul>
                </div>
            </div>
        </div>




        <!-- Modal -->
        <div id="addWebListContacts" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <%--<div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Add Contacts</h4>
                    </div>--%>
                    <div class="modal-body">
                        <ul id="listContactsModal" class="nav nav-tabs padding-left-0 margin-bottom-20" id="reportsTab">
                            <li class="active addContacts"><a data-toggle="tab" href="#addListContactsModal">Add Contacts</a></li>
                            <li class="allContacts"><a data-toggle="tab" href="#allContacts">All Contacts</a></li>
                            <%--<li class="mobileContacts"><a data-toggle="tab" href="#mobileContacts">Mobile Contacts</a></li>--%>
                            <li class="excelContacts"><a data-toggle="tab" href="#excelFormBody">Excel Upload</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="addListContactsModal" class="tab-pane fade in active">
                                <div class="form-group">
                                    <label for="name">Name:</label>
                                    <input type="text" placeholder="Nick name" class="form-control" id="name" maxlength="20" />
                                    <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group">
                                    <label for="mobile">Mobile Number ID:</label>
                                    <input type="text" placeholder="Mobile Number" class="form-control" id="mobileNumber" maxlength="10" />
                                    <span class="text-danger" id="errorDescForMobile" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
                                <div class="clearfix"></div>
                                <div class="form-group">
                                    <label for="fileUpld">Image Upload:</label>
                                    <img id="webContactProfile" alt="profile pic" style="width: 150px; height: 150px;" src="" />
                                    <input type="file" id="imageUpld" />
                                </div>
                            </div>
                            <div id="allContacts" class="tab-pane fade" style="background: #e3e3e3; overflow: hidden; padding: 5px; height: 200px; overflow-y: auto; display: none"></div>
                            <div id="mobileContacts" class="tab-pane fade" style="background: #e3e3e3; overflow: hidden; padding: 5px;">
                            </div>
                            <div id="excelFormBody" class="tab-pane fade" style="overflow: hidden; padding: 5px;">
                                <%-- <div class="container" id="page-container">--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <p class="margin-bottom-15 col-md-5">
                                            <input type="file" id="excelUploadFile" style="display: block;" />
                                        </p>
                                        <p class="col-md-6" id="upmsg"></p>
                                    </div>
                                </div>
                                <div id="headerselect" style="display: none;">
                                    <div class="row">
                                        <div class="col-md-5">Excel sheet has headers </div>
                                        <div class="col-md-1">
                                            <input type="radio" value='1' class="css-checkbox radio_head" id="header_1" name="radiog_head" />
                                            <label class="css-rdb radiodchk" for="header_1">Yes</label>
                                        </div>
                                        <div class="col-md-1 margin-bottom-10">
                                            <input type="radio" class="css-checkbox radio_head" id="header_2" value='2' name="radiog_head" />
                                            <label class="css-rdb radiodchk" for="header_2">No</label>
                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 margin-top-15" id="xlinfo_div">
                                            <div class="scroller" style="height: 190px" id="xlinfo">
                                                <div class="row margin-bottom-10">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="row">
                                    <div class="col-md-12">
                                        <div id="sheeterr" class="alert alert-danger" style="display: none"><span id="sheettxt"></span></div>
                                    </div>
                                    <input type="hidden" value="0" id="ddlgrpname" />
                                </div>
                                <%-- <div class="row margin-top-15">
                                <div class="col-md-12 text-right">
                                    <button type="button" class="btn btn-primary" id="upload">Save</button>
                                    <button type="button" class="btn default modal_close" data-dismiss="modal">Cancel</button>
                                </div>
                            </div>--%>
                                <%-- </div>--%>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button class="btn btn-primary" id="addCntctList">Add Contact</button>
                        <button type="button" class="btn btn-primary" id="saveExcelContacts" style="display: none;">Upload</button>
                        <button type="button" class="btn btn-primary" id="saveListContact" style="display: none;">Add To List</button>
                        <button type="button" class="btn btn-primary" id="updateContact" style="display: none">Update</button>
                        <button class="btn btn-default" id="closeList" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" id="countryID" value="<%= countryID %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagejs" runat="server">
    <script type="text/javascript" src="Scripts/contacts.js"></script>
    <script type="text/javascript" src="Scripts/webcontacts.js"></script>
    <script type="text/javascript" src="Scripts/jquery.Jcrop.js"></script>
    <script type="text/javascript" src="Scripts/jquery.fileupload.js"></script>
    <script type="text/javascript" src="Scripts/excelUpload.js"></script>
    <script type="text/javascript" src="Scripts/infinite-scoll.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#webLists').slimScroll({
                allowPageScroll: false,
                height: '450',

            });
            //$('.tab-content').slimScroll({
            //    allowPageScroll: false,
            //    height: '450',
            //    display:none
            //});
            $('#phoneContacts').slimScroll({
                allowPageScroll: false,
                height: '450'
            });
            $('#alphabetWebContacts,#alphabetMobileContacts').slimScroll({
                allowPageScroll: false,
                height: '450'
            });

        });
    </script>
</asp:Content>
