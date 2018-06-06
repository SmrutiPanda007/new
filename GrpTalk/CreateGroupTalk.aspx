<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="CreateGroupTalk.aspx.cs" Inherits="GrpTalk.CreateGroupTalk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="css/DateTimePicker.css" rel="stylesheet" type="text/css" />
    
	<link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <style type="text/css">
			.blockOverlay {opacity:0.6 !important}
            .blockMsg {opacity:1 !important}
				.onoffswitch {
				position: relative; width: 90px;
				-webkit-user-select:none; -moz-user-select:none; -ms-user-select: none;
			}
			.onoffswitch-checkbox {
				display: none;
			}
			.onoffswitch-label {
				display: block; overflow: hidden; cursor: pointer;
				border: 2px solid #e8e8e8; border-radius: 20px;
			}
			.onoffswitch-inner {
				display: block; width: 200%; margin-left: -100%;
				transition: margin 0.3s ease-in 0s;
			}
			.onoffswitch-inner:before, .onoffswitch-inner:after {
				display: block; float: left; width: 50%; height: 30px; padding: 0; line-height: 30px;
				font-size: 14px; color: white; font-family: Trebuchet, Arial, sans-serif; font-weight: bold;
				box-sizing: border-box;
			}
			.onoffswitch-inner:before {
				content: "ON";
				padding-left: 10px;
				background-color: #fd5056; color: #FFFFFF;
			}
			.onoffswitch-inner:after {
				content: "OFF";
				padding-right: 10px;
				background-color: #ccc; color: #fff;
				text-align: right;
			}
			.onoffswitch-switch {
				display: block; width: 30px; height:30px; margin: 2px;
				background: #FFFFFF;
				position: absolute; top: 0; bottom: 0;
				right: 60px;
				border: 2px solid #ccc; border-radius: 50px;
				transition: all 0.3s ease-in 0s; 
			}
			.onoffswitch-checkbox:checked + .onoffswitch-label .onoffswitch-inner {
				margin-left: 0;
			}
			.onoffswitch-checkbox:checked + .onoffswitch-label .onoffswitch-switch {
				right: 0px; 
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

        #selectedContacts #profilePic img, #grpCallMobileContacts #profilePic img {
            width: 50px;
        }

        #selectedContacts .profileDetails p, #grpCallMobileContacts .profileDetails p {
            margin: 0;
            white-space: nowrap;
        }

            #selectedContacts .profileDetails p:last-child, #grpCallMobileContacts .profileDetails p:last-child {
                color: #0a93d7;
            }

        .highlight {
            background-color: #e9724f !important;
        }

       

        .modal-body {
            min-height: 206px;
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

        .contacts {
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

        .fa-ellipsis-v {
            cursor: pointer; /*position:absolute;top:7px;right:2px;float:right;*/
        }

        .fa-ellipsis-v {
            display: none !important;
        }

        #listTitle1 ul li a {
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
        /*#list-group1 .btn {border-radius:0;border-bottom:1px solid #0678b0;background-color:#0a93d7;}*/
        #list-group1 .list1 { /*overflow:hidden;*/
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #e6dede;
            background-color: #f3eded;
            height: 47px;
            padding: 10px;
            color: #fff;
        }

            #list-group1 .list1 a {
                color: #606060;
            }
            #list-group .highlight a{color:#fff !important;}
        .tab-content {
            position: relative;
        }
        <!-- .alphabet {
            list-style-type: none;
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
        -->
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
            border-bottom: 1px solid orange;
            width: 25%;
            text-align: center;
            padding: 7px 0 7px 0;
            border-top: 0;
            border-left: 0;
            border-right: 0;
        }

        #saveGrpCall, #setRepeat {
            background-color: #0082c3;
            border: 2px solid #0082c3;
        }

        #clearRepeat {
            border: 2px solid #ccc;
        }

        .datePicker, #repeatCall {
            text-align: center;
        }

        .weekDay {
            border: 2px solid #c1bebc !important;
            line-height: 17px;
            font-weight: 600; /*cursor:pointer;*/
            color: #c1bebc;
            margin: 0 10px;
            background-color: #e8e8e8;
            border-radius: 50% !important; /*padding: 10px;*/
            width: 40px;
            height: 40px;
            font-size: 16px;
            float: left;
        }

            .weekDay.active {
                border: 2px solid #da464c !important;
                line-height: 17px;
                font-weight: 600;
                color: #fff;
                background-color: #f95259;
            }

        

        #startNow {
            background-color: #e79e36;
            border: 2px solid #e79e36;
            margin: 0 10px;
        }

        /*#schedule {
            background-color: #63caed !important;
            border: 2px solid #63caed;
            color: #fff;
        }*/

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
            margin-left: 208px;
            margin-top: -32px;
        }

        #listTitle1 ul {
            list-style-type: none;
        }

            #listTitle1 ul li a {
                color: #fff;
            }

        .list1:hover {
            background-color: #e9724f !important;
            
        }
        #list-group1 .list1:hover a {
        color:#fff !important;
        }
        .highlight {
            background-color: #e9724f !important;
            
        }
        .highlight  a{color:#fff !important;}
        a:focus {
            outline: none !important;
        }
		.ui-autocomplete{z-index:9999;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row">
        <div class="col-md-3 col-sm-3 margin-top-20 margin-bottom-20" id="callLogs">
            <div class="row">
                <%--  <button type="button" class="btn btn-primary margin-bottom-20" id="createGrpList"><span class="fa fa-plus"></span>Create New List</button>--%>
                <div id="list-group1">
                    

                </div>
                <button id="phoneContactsDisplay" class="btn btn-primary margin-bottom-20" style="display: none; width: 100%; pointer-events: none; border: 0px none; background-color:#F95259; border-radius: 0px; font-size: 18px;" type="button">Phone Contacts</button>
                <div class="phnCntcts" style="min-height: 345px; display: none;">
                    <div id="phoneContactsText" style="width: 85%; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); font-size: 18px; color: #808080; text-align: center; display: block;">You can modify phone contacts from your mobile device only</div>
                </div>
            </div>
        </div>
        <div class="col-md-9 col-sm-9 margin-top-20 margin-bottom-20">
            <div class="row" id="listTitle1">
                <div class="col-md-12 col-sm-12">
                    <b id="selectDbl">Select Contacts</b>
                    <ul>
                        <li>
                            <a href="javascript:;" class="addNewContact"><i aria-hidden="true" class="fa fa-plus-square"></i></a></li>
                        <!--<a href="javascript:;" data-toggle="modal" data-target="#contactsModal" class="addNewContact"><i aria-hidden="true" class="fa fa-plus-square"></i></a></li>-->
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 padding-left-0 padding-right-0">
                    <form class="navbar-form" role="search" id="contactsSearch">
                        <div class="input-group" style="width: 100%;">
                            <input type="text" class="form-control" placeholder="Search" id="search-input" autocomplete="off" />
                            <div class="input-group-btn" style="width: 5%;">
                                <button class="btn btn-default" id="search" type="submit"><i class="glyphicon glyphicon-search"></i></button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <div class="row margin-bottom-20">
                <button style="float: right; right: 15px; display: block; border: 0; margin-top: 8px;" class="btn btn-danger select-all" id="selectAll">Select All</button>
                <button id="unSelectAll" class="btn btn-success select-all" style="float: right; right: 15px; display: none; border: 0px none; margin-top: 8px;">Unselect all</button>
                <ul class="nav nav-tabs padding-left-0 margin-bottom-20" id="myTabList">
                    <li class="active webContacts"><a data-toggle="tab" href="#grpCallWebContacts" listid="0" id="webList">Web Lists</a></li>
                    <li class="mobileContacts"><a data-toggle="tab" href="#grpCallMobileContacts">Phone Contacts</a></li>
                    <li class="selectedContacts"><a data-toggle="tab" href="#selectedContacts" style="display: none;">Selected Contacts<span class="count">(0)</span></a></li>
                </ul>
                <div class="tab-content">



                    <div id="grpCallWebContacts" class="tab-pane fade in active">
                    </div>

                    <div id="grpCallMobileContacts" class="tab-pane fade">
                    </div>

                    <div id="selectedContacts" class="tab-pane fade">
                    </div>

                    <!--<ul class="alphabet">
                         <li>#</li>
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
                    </ul>-->
                </div>
            </div>
			
			<div id="dialMutedial" class="modal fade" role="dialog" style="display: none;" aria-hidden="false">
			
			<div class="modal-dialog">

				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="pull-center" style="margin-bottom: 0px;  margin-top:0px; color:#606060;"><span>Are you sure you want to dial the call?</span></h4>
						<button class="close" data-dismiss="modal" style="position:absolute; right:15px; top:10px;" type="button">×</button>
					</div>
					
						
						  
					<div class="modal-footer text-center margin-top-20 margin-bottom-20" style="border-top:none; text-align:center;">
						<span class="alert alert-danger" id="ScheduleErrorMessage" style="display: none"></span>
						<button type="button" class="btn btn-success" style="width:100px;" data-dismiss="modal" id="dial">Dial</button>
						<button type="button" class="btn btn-danger" style="width:100px;" data-dismiss="modal" id="muteDial">Mute Dial</button>
						<button type="button" class="btn" style="width:100px;" id="cancel">Cancel</button>
					</div>
				
				</div>
			</div>
	</div>
            <!-- <div class="text-center">
                <div id="advancedSettings" class="active">
                    Advanced Settings <span class="glyphicon glyphicon-chevron-down"></span>
                </div>
                <div id="advancedFeatures" style="display: none;">
                    <div class="col-md-4 col-sm-4 margin-top-5 margin-bottom-5">
                        <input type="checkbox" id="chkOnlyDialIn"> Only DialIn</input>
                    </div>
                    <div class="col-md-4 col-sm-4 margin-top-5 margin-bottom-5">
                        <input type="checkbox" id="chkAllNonMembers"> Allow Non Members To Join</input>
                    </div>
                    <div class="col-md-4 col-sm-4 margin-top-5 margin-bottom-5">
                        <input type="checkbox" id="chkMuteDialAll"> Mute Dial All</input>
                    </div>

                </div>
            </div> -->
			<div id="duplicateContacts" class="modal fade" role="dialog" style="display: none;" aria-hidden="false">
			
			<div class="modal-dialog">

				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="pull-center modal-title" style="margin-bottom: 0px; font-size:16px;  margin-top:0px; color:#606060;"><span>Following Contacts are Already Added to Group</span></h4>
						<button class="close" data-dismiss="modal" style="position:absolute; right:15px; top:10px;" type="button">×</button>
					</div>
					<div  class="modal-body text-center margin-top-20 margin-bottom-20"  style="border-top:none; text-align:center; min-height:90px;">
					<div class="row font-sm" id='contatcsName' style="max-height:400px; overflow:auto;">
					
					</div>	
					</div>
					
					<div class="modal-footer text-center">
					<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
					</div>
				
				</div>
			</div>
	</div>
            <div class="startschedule" style="margin-top:15px;">
                <input type="text" class="margin-bottom-20" id="grpTalkName" placeholder="Enter grpTalk Name" maxlength="20" style="outline: none; !important" />

                <button id="startNow" class="btn btn-primary" value="Start Now" type="button">Start Now</button>
                <button type="button" value="Start" class="btn btn-success" id="saveGroupCall" style="border: 2px solid #45B6AF !important; margin: 0px 10px 0px 0px; width: 85px">Save</button>
                <button id="schedule" class="btn btn-danger" value="Schedule" type="button">Schedule</button>
            </div>
        </div>
    </div>


    <!---- modal starts here for adding new conttact-------->
    <div id="selectDblContacts" class="modal fade" role="dialog" style="display: none;" aria-hidden="false">
			
			<div class="modal-dialog">

				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="pull-center modal-title" style="margin-bottom: 0px; font-size:16px;  margin-top:0px; color:#606060;">
                            <span>We found the below contacts with other names ina a different web lists.<br /> Choose a name you want to use in the web list.</span></h4>
						<button class="close" data-dismiss="modal" style="position:absolute; right:15px; top:10px;" type="button">×</button>
					</div>
					<div  class="modal-body"  style="border-top:none; min-height:90px;">
                        <div style="max-height:450px; overflow:auto; font-size:14px;">
                        <div class="well well-sm margin-bottom-10">
                            <div class="row">
                                <div class="col-sm-2">9848022338</div>
                                <div class="col-sm-10">
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Shekimam</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Siva mani</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani</label>
                                </div>
                            </div>
                        </div>

                        <div class="well well-sm margin-bottom-10">
                            <div class="row">
                                <div class="col-sm-2">9848032919</div>
                                <div class="col-sm-10">
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Shekimam</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Siva mani</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani</label>
                                     <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani Siva</label>
                                </div>
                            </div>
                        </div>

                        <div class="well well-sm margin-bottom-10">
                            <div class="row">
                                <div class="col-sm-2">9848032919</div>
                                <div class="col-sm-10">
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Shekimam</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Siva mani</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani</label>
                                     <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani Siva</label>
                                      <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Shekimam</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Siva mani</label>
                                    <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani</label>
                                     <label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" /> Mani Siva</label>
                                </div>
                            </div>
                        </div>
					    </div>
                   </div>
					
					<div class="modal-footer text-center">
                        <button type="button" class="btn btn-success" data-dismiss="modal">Save</button>
					<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
					</div>
				
				</div>
			</div>
	</div>

    <div id="contactsModal" class="modal fade" role="dialog" style="display: none;">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content" style="width: 800px">
                <div class="modal-header">
				<h4 class="pull-left">Contacts</h4>
                    <button type="button" class="close pull-right" data-dismiss="modal">&times;</button>
                    <div class="clearfix"></div>
                </div>
                <div class="modal-body">
				<div>
                        <ul class="nav nav-tabs padding-left-0">
                            <li class="active"><a data-toggle="tab" id="newContact">Add Contacts</a></li>
                            <li><a data-toggle="tab" id="excelUpload">Excel Upload</a></li>
                        </ul>
                    </div>
                    <!-------------For adding Single Contacts in Web List--------------------->
					<!-- <div class="tab-content" style="min-height: 325px;"> -->
                    <div id="contactFormBody" style="display: block;">
                           <div class="row" style="margin-top:15px;">
                                    <div class="col-sm-6">
                                    <label for="name">Name:</label>
                                    <input type="text" placeholder="Nick name" class="form-control" id="name" maxlength="20" />
                                    <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
                                     <div class="col-sm-6">
                                    <label for="mobile">Mobile Number ID:</label>
                                    <input type="text" placeholder="Mobile Number" class="form-control" id="mobileNumber" maxlength="12" onkeypress="return isNumber(event)"/>
                                    <span class="text-danger" id="errorDescForMobile" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
                                </div>
								 <div class="row" >
                                    <div class="col-sm-6">
                                    <label for="name">Choose a web list:</label>
                                    <select id="ddlWebList" class="form-control" style="outline: none ! important"></select>

									</div>
                                  <div class="col-sm-6">
                                    <label for="newWebList">Create a Web List:</label>
                                    <input type="text" placeholder="Enter new Weblist Name" class="form-control" id="newWebList" maxlength="25" />
                                    
                                </div>
								<span class="text-danger" id="errorDescForWebList" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
								 <div class="row" style="margin-top:15px">
								 <div class="col-sm-12">
                                    <label for="fileUpld">Image Upload:</label>
                                    <input type ="button" value = "Choose Image" onclick ="javascript: document.getElementById('imageUpld').click();"/>
                                    <input type="file" id="imageUpld" style="visibility:hidden"/>
									
									<img id="webContactProfile" alt="profile pic" style="width: 150px; height: 150px;" src="" />
                                </div>
								</div>
                                


                    </div>
				 

                    <!-------------For adding Bulk Contacts through Excel Sheet in Web List--------------------->
                    <div id="excelFormBody" style="display: none;">
                        <%-- <div class="container" id="page-container">--%>
						<div class="table-responsive">
						<table class="table no-border">
						<tr>
						<td style="width:30%;">Choose File :</td>
						<td>
						<label><input type="button" value="Choose File" onclick="javascript: document.getElementById('excelUploadFile').click();" />
                               <input accept=".xls,.xlsx" type="file" id="excelUploadFile" style="display:none;" />
							   </label>
							   <span id="upmsg"></span>
						</td>
						</tr>
						<tr id="headerselect" style="display: none;">
						<td>Excel sheet has headers :</td>
						<td>
						<div class="pull-left" style="margin-right:20px;">
						<input type="radio" value='1' class="css-checkbox radio_head" id="header_1" name="radiog_head" />
                                    <label class="css-rdb radiodchk" for="header_1">Yes</label>
						</div>
						<div class="pull-left">
						<input type="radio" class="css-checkbox radio_head" id="header_2" value='2' name="radiog_head" />
                                    <label class="css-rdb radiodchk" for="header_2">No</label>
						</div>
						</td>
						</tr>
						</table>
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

                    </div>
					<div class="modal-footer">
                    <span class="text-success" id="successMessage"></span>
                    <span class="text-danger" id="errorMessage"></span>
                    <button type="button" class="btn btn-default" id="contactClose" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-danger" id="saveContact">Add Contact</button>
                    <button type="button" class="btn btn-danger" id="saveExcelContacts" style="display: none;">Upload</button>
                </div>
                </div>
                
            </div>

        </div>
    
	
    <!--- modal ends here for adding new conttact-->
<!-----------------Modal for Save Call ------------------------->
	<div id="saveModal" class="modal fade" role="dialog" style="display: none;">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" >
                    <h5 class="pull-center" style="margin-bottom: 0px;  margin-top:0"><span>Advanced Setting</span>
					<button class="close pull-right" data-dismiss="modal" type="button">×</button>
					</h5>
                    
                </div>
                      
                       <div class="modal-body">
								<div style="border-bottom:1px solid #ddd; margin-bottom:8px;">
                               <label style="text-align:left;" class="pull-left"> <b>Dial In Only</b>
							   <span style="display:block; font-weight:normal;">Auto Dial out will be stopped and members can dial in </span>
								</label>
                                
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="savedialinswitch" >
								<label class="onoffswitch-label" for="savedialinswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								</div>
									
									<div style="border-bottom:1px solid #ddd; margin-bottom:8px;">
                               <label style="text-align:left;" class="pull-left"> <b>Allow Non Members</b>
							   <span style="display:block; font-weight:normal;">Members who are not added to the group can also dial in using a PIN </span>
								</label>
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="saveallownonswitch" >
								<label class="onoffswitch-label" for="saveallownonswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								</div>
                                
                                <div style="margin-bottom:8px;border-bottom:1px solid #ddd;">
                               <label style="text-align:left;" class="pull-left"> <b>Mute Dial</b>
							   <span style="display:block; font-weight:normal;">All members will be joined the conference in mute mode </span>
								</label>
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="savemutedialswitch" >
								<label class="onoffswitch-label" for="savemutedialswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								</div>

                           <div style="margin-bottom:8px;" class="row">
                               <label style="text-align:left; padding-left:0px;" class="col-sm-10"> <b>Assign a call manager</b>
							   <span style="display:block; font-weight:normal;">You can nominate any member from this group to <b>manage the call</b>. The manager will get full access (Host Controls) of this group from their own grpTalk account </span>
								</label>
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="assignManager" >
								<label class="onoffswitch-label" for="assignManager">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
                              
								<div class="clearfix"></div>
								</div>
								      
                            <div id="selectManger" style="display:none" >
                               Search and select a member <input type="text" id="txtAssignManger" style="margin-left:8px;"/>
                               </div>                     
                    </div>
                <div class="modal-footer">
                    <span class="alert alert-danger" id="ScheduleErrorMessage" style="display: none"></span>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-danger" id="saveDate">Save Group</button>
                </div>
            
        </div>
    </div>
	</div>
	<!-------------------------------------------------------------->
    <!------------------------- modal for datepicker------------------------->
    <div id="datepickerModal" class="modal fade" role="dialog" style="display: none;">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" >
                    <h5 class="pull-left" style="margin-bottom: 0px;  margin-top:0"><span>Advanced Setting</span></h5>
                    <button class="close pull-right" data-dismiss="modal" type="button">×</button>
					<div class="clearfix"></div>
                </div>
                
                    

                        <div class="modal-body">
								<div style="border-bottom:1px solid #ddd; margin-bottom:8px;">
                               <label style="text-align:left;" class="pull-left"> <b>Dial In Only</b>
							   <span style="display:block; font-weight:normal;">Auto Dial out will be stopped and members can dial in </span>
								</label>
                                
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="dialinswitch" >
								<label class="onoffswitch-label" for="dialinswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								</div>
									
									<div style="border-bottom:1px solid #ddd; margin-bottom:8px;">
                               <label style="text-align:left;" class="pull-left"> <b>Allow Non Members</b>
							   <span style="display:block; font-weight:normal;">Members who are not added to the group can also dial in using a PIN </span>
								</label>
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="allownonswitch" >
								<label class="onoffswitch-label" for="allownonswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								</div>
                                
                                <div style="margin-bottom:8px;border-bottom:1px solid #ddd;">
                               <label style="text-align:left;" class="pull-left"> <b>Mute Dial</b>
							   <span style="display:block; font-weight:normal;">All members will be joined the conference in mute mode </span>
								</label>
								<label class="onoffswitch pull-right"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="mutedialswitch" >
								<label class="onoffswitch-label" for="mutedialswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								</div>

                               <div style="margin-bottom:10px;" class="row">
                               <div style="text-align:left; padding-left:0px;" class="col-sm-10">
                                   <b>Assign a call manager</b>

							   <span style="display:block; font-weight:normal;">You can nominate any member from this group to <b>manage the call</b>. The manager will get full access (Host Controls) of this group from their own grpTalk account </span>

								</div>
								<label class="onoffswitch"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="assignManagerSchedule" >
								<label class="onoffswitch-label" for="assignManagerSchedule">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
                              
								
                      
                              </div>
                            <div class="clearfix"></div>
                             <div id="selectMangerSchedule" style="display:none" >
                               Search and select a member <input type="text" id="txtAssignMangerSchedule" style="margin-left:8px;"/>
                               </div>

								 <h4 class="pull-left" style="margin-bottom: 0px;  margin-top:0"><p>Schedule grpTalk On</p></h4>
								 <div class="clearfix"></div>
								<div style="margin-bottom:10px;">
                                <label>Schedule Date:</label>
                                <input class="form-control form-control-inline date-picker custom-form" size="16" type="text" value="" data-field="datetime" placeholder="Date and Time" data-step="3" data-intro="When do you want to have this conference" id="datefield">
                                <div id="dtBox" class="dtpicker-overlay dtpicker-mobile" >
                                    <div class="dtpicker-bg">
                                        <div class="dtpicker-cont">
                                            <div class="dtpicker-content">
                                                <div class="dtpicker-subcontent"></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div style="margin-bottom:10px;border-bottom:1px solid #ddd; padding-bottom:15px;">
                                <label class="pull-left">Repeat</label>

                                <div class="row pull-left" style="width: 81%; margin: 0px auto;">
                                    <button type="button" class="btn weekDay" id="sun" value="Sunday">S</button>
                                    <button type="button" class="btn weekDay" id="mon" value="Monday">M</button>
                                    <button type="button" class="btn weekDay" id="tue" value="Tuesday">T</button>
                                    <button type="button" class="btn weekDay" id="wed" value="Wednesday">W</button>
                                    <button type="button" class="btn weekDay" id="thu" value="Thursday">T</button>
                                    <button type="button" class="btn weekDay" id="fri" value="Friday">F</button>
                                    <button type="button" class="btn weekDay" id="sat" value="Saturday">S</button>


                                </div>
								<div class="clearfix"></div>
												 <!-- <center style="color: #45aed6"><b>254856</b> is  your conference PIN</center> -->
                                                              
                               
								<div id="openlinecondition" style="display:none;<!-- border-top:1px solid #ddd;  -->">
                                
								<label style="text-align:left;" class="pull-left"> <b>Open Line Before 30 minutes</b>
							   <span style="display:block; font-weight:normal;"> Any member can dial in to the conference line 30 min <br> before the scheduled time </span>
								</label>
								<label class="onoffswitch pull-right" style="margin-top:10px;" id="openswitch" > <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="openlineswitch" disabled="true">
								<label class="onoffswitch-label" for="openlineswitch">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
								<div class="clearfix"></div>
								
                               
                         
                           
                            </div>
                        </div>
                            
                 
               
        </div>
                 <div class="modal-footer">
                    <span class="alert alert-danger" id="ScheduleErrorMessage" style="display: none"></span>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-danger" id="scheduleDate">Schedule</button>
                </div>
            
    </div>
	</div>
    <!------------------------- modal for datepicker code ends here---------->
	
	
    <!---------------------Modal for Weekdays repeat------------------------->
    <div id="repeatCall" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button class="close pull-right" data-dismiss="modal" type="button">×</button>
                    <%--<h4 class="modal-title">Modal Header</h4>--%>
                </div>
                <div class="modal-body">
                    <p>Repeat</p>
                    <div>
                        <div class="row" style="width: 81%; margin: 0px auto;">
                            <button type="button" class="btn weekDay" id="sun" value="Sunday">S</button>
                            <button type="button" class="btn weekDay" id="mon" value="Monday">M</button>
                            <button type="button" class="btn weekDay" id="tue" value="Tuesday">T</button>
                            <button type="button" class="btn weekDay" id="wed" value="Wednesday">W</button>
                            <button type="button" class="btn weekDay" id="thu" value="Thursday">T</button>
                            <button type="button" class="btn weekDay" id="fri" value="Friday">F</button>
                            <button type="button" class="btn weekDay" id="sat" value="Saturday">S</button>
                        </div>

                    </div>
                </div>
                <div class="modal-footer">
                    <div class="col-md-6 col-sm-6 text-right">
                        <button type="button" class="btn btn-primary" id="setRepeat" data-dismiss="modal">Set</button>
                    </div>
                    <div class="col-md-6 col-sm-6 text-left">
                        <button type="button" class="btn btn-default" id="clearRepeat">Clear</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
	
	
	
    <input type="hidden" id="hostNumber" value="<%= hostNumber %>" />
    <input type="hidden" id="countryID" value="<%= countryID %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagejs" runat="server">
 
    <script src="Scripts/DateTimePickerModified.js" type="text/javascript"></script>
	<script type="text/javascript" src="js/bootbox.min.js"></script>
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="Scripts/excelUpload.js"></script>
    <script type="text/javascript" src="Scripts/creategrouptalk.js?type=v1"></script>
	<script type="text/javascript" src="Scripts/addContact.js?type=v1"></script>
	<script src="assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script type="text/javascript">
	if($('#myonoffswitch').is(':checked'))
	{console.log('on');}
	else
	{console.log('off');}
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
            { return false; }
            return true;
        }
        $('#list-group1').slimScroll({
            allowPageScroll: false,
            height: '500',

        });
		

    </script>
</asp:Content>
