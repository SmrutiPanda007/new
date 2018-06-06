<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="EditGroupTalk.aspx.cs" Inherits="GrpTalk.EditGroupTalk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="css/DateTimePicker.css" rel="stylesheet" type="text/css" />
	<link href="/css/jquery.jcrop.css" rel="stylesheet" type="text/css" />
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
		.selected{position:relative;}
		.selected i.select_check{position:absolute; top:35px; right:5px; font-size:17px; color:#00cd67;}
		#selectedContacts #profilePic img, #grpCallMobileContacts #profilePic img{width:50px;}
		#selectedContacts .profileDetails p, #grpCallMobileContacts .profileDetails p{margin:0;white-space: nowrap;}
		#selectedContacts .profileDetails p:last-child, #grpCallMobileContacts .profileDetails p:last-child{color:#0a93d7;}
		
		.unselected{position:relative;}
	.unselected i.select_check{position:absolute; top:35px; right:5px; font-size:17px; color:#00cd67;}
        .list.active {
            background-color: #1c252b;
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

        #webLists, #phoneContacts {
            background-color: #e3e3e3;
            padding: 10px;
            overflow: hidden;
        }
        .list a:hover, .list:hover a{
            background-color: #e9724f;
            color:#fff;
        }
        .highlight{
            background-color: #e9724f !important;
        }
        a:focus {
            outline:none !important;
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
            margin-right:8px;
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

        #listTitle ul li a {
            color: #fff;
            
        }

        .dropdown-menu {
            min-width: 115px !important;
        }

            .dropdown-menu li a {
                color: #000 !important;
            }

        #list-group .list { /*overflow:hidden;*/
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #e6dede;
            background-color: #f3eded;
            height: 47px;
            /*padding: 10px;*/
            color: #fff;
        }

            #list-group .list a {
                color: #606060;
                 padding: 10px;
            }
            #list-group .list a:hover{
                 color:#fff;

            }
        .tab-content {
            position: relative;
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

        #grpCallWebContacts, #grpCallMobileContacts, #selectedContacts {
            background-color: #e3e3e3;
            overflow: auto;
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

        #datePicker, #repeatCall {
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
            width: 45px;
            height: 45px;
            font-size: 16px;
            float: left;
        }

            .weekDay.active {
                border: 2px solid #3b7cc2 !important;
                line-height: 17px;
                font-weight: 600;
                color: #fff;
                background-color: #6192c2;
            }

        

        .fa-check {
            float: left;
            font-size: 20px;
            margin-left: 208px;
            margin-top: -32px;
        }

        .dtpicker-components .dtpicker-compValue {
            width:60% !important;margin:0.4em auto;
        }
		   .list:hover {
            background-color: #0282c2 !important;
        }

        .highlight {
            background-color: #e9724f !important;
        }
           #list-group .highlight a {
                color:#fff;
            }
        a:focus {
            outline: none !important;
        }
		
		.pad-left-0{padding-left:0px !important;}
        .ui-autocomplete{z-index:9999;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row">
        <div class="col-md-3 col-sm-3 margin-top-20 margin-bottom-20" id="callLogs">
            <div class="row" style="min-height:450px">
                <%--<button type="button" class="btn btn-primary margin-bottom-20" id="createGrpList"><span class="fa fa-plus"></span>Create New List</button>--%>
                <div id="list-group">
                </div>

               
                <button id="phoneContactsDisplay"  type="button" class="btn btn-primary margin-bottom-20" style="width:100%;pointer-events:none;border:0;background-color:#fd5056;border-radius:0;font-size:18px;display:none">Phone Contacts</button>
                <div class="phnCntcts" style="min-height:450px;display:none;">
                    <div style="width:85%;position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%);font-size:18px;color:#428bca;font-weight:600;text-align:center;">You can modify phone contacts from your mobile device only</div>
                </div>
               
            </div>
        </div>
        <div class="col-md-9 col-sm-9 margin-top-20 margin-bottom-20">
            <div class="row" id="listTitle">
                <div class="col-md-12 col-sm-12">
                    <b id="listTitleName">Select Contacts</b>
                    <ul>
                        <li><a href="javascript:;" data-toggle="modal" data-target="#contactsModal"><i aria-hidden="true" id="plusSymbol" class="fa fa-plus-square"></i></a></li>
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 padding-left-0 padding-right-0">
                    <form class="navbar-form" role="search" id="contactsSearch">
                        <div class="input-group" style="width: 100%;">
                            <input type="text" id="search-input" class="form-control" placeholder="Search" autocomplete="off" />
                            <div class="input-group-btn" style="width: 5%;">
                                <button class="btn btn-default" id="search"><i class="glyphicon glyphicon-search"></i></button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <div class="row margin-bottom-20">
                <button style="float: right; right: 15px; display: block; border: 0; margin-top: 8px;" class="btn btn-success select-all" id="selectAll">Select All</button>
                <ul class="nav nav-tabs padding-left-0 margin-bottom-20" id="myTabList">
                    <li class="active webContacts"><a data-toggle="tab" href="#grpCallWebContacts" id="webList">Web Lists</a></li>
                    <li class="mobileContacts"><a data-toggle="tab" href="#grpCallMobileContacts">Phone Contacts</a></li>
                    <li class="selectedContacts"><a data-toggle="tab" href="#selectedContacts">Selected Contacts<span class="count">(0)</span></a></li>
                </ul>
                <div class="tab-content" style="min-height:250px;background:#e3e3e3;">



                    <div id="grpCallWebContacts" class="tab-pane fade in active">
                    </div>
                    <div id="grpCallMobileContacts" class="tab-pane fade">
                    </div>

                    <div id="selectedContacts" class="tab-pane fade">
                    </div>
                    <%--<ul class="alphabet">
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
                    </ul>--%>
                </div>
            <div class="startschedule" style="margin-top:15px;">
                <form class="form-inline">
                    
						<div class="row" style="margin-bottom:20px;">
						<div class="col-sm-5 padding-left-0">
						<input id="grpId" type="hidden" name="grpId" value="">
                        <input type="text" name="name" placeholder="Enter grpCall Name" id="conf_name" class="custom-form" style="outline:none ! important; width:100%" maxlength="20"/>
						</div>
						<div class="col-sm-1 text-center">
						<span data-intro="When do you want to have this conference" data-step="3">On</span>
						</div>
						<div class="col-sm-6" id="dateClick">
						<input type="text" placeholder="Date and Time" style="outline:none ! important; width:100%;" data-field="datetime" value="" size="16" id="conf_datetime" class="form-control-inline date-picker custom-form" readonly/><br />
                        <div id="dtBox"></div>
						</div>
                        </div>
                        
                        
						
						<div class="row" style="margin-bottom:10px;">
                        <div class="col-sm-6 padding-left-0">
						<!-- <span data-intro="When do you want to have this conference" data-step="3" style="display:block; font-weight:bold"></span> -->
						<div class="input-group" style="width:100%;">
						<label class="input-group-addon" style="background:#fff; width:27%; border-bottom:1px solid orange; border-top:none; border-left:none; color:#c1462c;">Repeats Every :</label>
						<input type="text"  style="background:#fff; width:100%; text-align:left;" placeholder="Sun, Mon, Tue, Wed, Thu, Fri, Sat" value="" size="16" data-field="repeat weekdays" data-target="#repeatCall" class="form-control-inline repeatWeekDays custom-form" id="repeat" style="outline:none ! important; width:100%;" readonly/>
						</div>
                        
						</div>
						<div class="col-sm-6">
						<input type="text" placeholder="Advanced Settings" value="" size="16" data-field="settings" data-target="#advancedSettings" class="form-control-inline custom-form" id="adSettings" style="outline:none ! important; width:100%;"/>
						</div>
						</div>
                    
                    <span id="grpCallErrorMessage" class="alert alert-danger" style="display: none"></span>
                    <button type="button" value="Schedule" class="btn btn-danger margin-top-10" id="saveGrpCall">Save grpCall</button>

                </form>
            </div>
			</div>
			
        </div>

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
		
        <div id="contactsModal" class="modal fade" data-keyboard="false" data-backdrop="static" role="dialog" style="display: none;" aria-hidden="false">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content" style="width: 800px">
                    <div class="modal-header">
					<h4 class="pull-left">Contacts</h4>
                        <button type="button" class="close pull-right" data-dismiss="modal" style="outline:none ! important">&times;</button>
                        <div class="clearfix"></div>
						<div>
                            
                        </div>
                    </div>
                    <div class="modal-body">
					<ul class="nav nav-tabs padding-left-0">
                              <li class="active"><a data-toggle="tab" id="newContact">Add Contacts</a></li>
                              <li><a data-toggle="tab" id="excelUpload">Excel Upload</a></li>
                            </ul>
                   <!-------------For adding Single Contacts in Web List--------------------->
                     <div id="contactFormBody" style="display:block;">
                        <div class="row">
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
								<div class="text-center"><span class="text-danger" id="errorDescForWebList" style="margin: 0px; padding: 0px; font-size: 12px;"></span></div>
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
                    <div id="excelFormBody" style="display:none;">
                       <%-- <div class="container" id="page-container">--%>
					   <div class="table-responsive" style="margin-top:10px;">
					   <table class="table no-border" style="margin-bottom:0px;">
					   <tr>
					   <td class="col-sm-3">Choose Contacts :</td>
					   <td>
					   <input class="col-md-3" type = "button" value = "Choose File" onclick ="javascript: document.getElementById('excelUploadFile').click();"/>
                       <input type="file" accept=".xls,.xlsx" id="excelUploadFile" style="display:none;"/>
					   <p id="upmsg" class="col-md-6">my_241116121044.xlsx</p>
					   </td>
					   </tr>
					   <tr id="headerselect" style="display: none;">
					   <td>Excel sheet has headers :</td>
					   <td>
					   <div class="margin-top-10">
					   <label class="pull-left" style="margin-right:20px;">
					   <input type="radio" value='1' class="css-checkbox radio_head" id="header_1" name="radiog_head" />
                                        <label class="css-rdb radiodchk" for="header_1">Yes</label>
					   </label>
					   <label class="pull-left">
					   <input type="radio" class="css-checkbox radio_head" id="header_2" value='2' name="radiog_head" />
                                        <label class="css-rdb radiodchk" for="header_2">No</label>
					   </label>
					   </div>
					   </td>
					   </tr>
					   </table>
					   </div>
                           
                            <div >
                                
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
		
     <!---------Advanced Settings---->
	  <div id="advancedSettings" class="modal fade" role="dialog" style="display: none;" data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header" >
                    <h5 class="pull-left" style="margin-bottom: 0px;  margin-top:0"><span>Advanced Setting</span></h5>
                    <button class="close pull-right" data-dismiss="modal" type="button" style="outline: none ! important">×</button>
					<div class="clearfix"></div>
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
                                
                                <div style="border-bottom:1px solid #ddd; margin-bottom:8px;">
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
								<div id="openlinecondition" style="display:none; border-bottom:1px solid #ddd; margin-bottom:10px;">
                                
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
                            <div id="editManager">
								  <div style="margin-bottom:8px;" class="row" >
                               <div style="text-align:left; padding-left:0px;" class="col-sm-10">
                                   <b>Assign a call manager</b>
							   <span style="display:block; font-weight:normal;">You can nominate any member from this group to <b>manage the call</b>. The manager will get full access (Host Controls) of this group from their own grpTalk account </span>
								</div>
								<label class="onoffswitch" style="margin-left:3px;"> <input type="checkbox" name="onoffswitch" class="onoffswitch-checkbox" id="assignManagerEdit" >
								<label class="onoffswitch-label" for="assignManagerEdit">
									<span class="onoffswitch-inner"></span>
									<span class="onoffswitch-switch"></span>
								</label>
								</label>
                              
								</div> 
                             <div class="clearfix"></div>
                             <div id="selectMangerEdit" style="display:none" >
                               Search and select a member <input type="text" id="txtAssignMangerEdit" style="margin-left:8px;"/>
                               </div> 
                                </div>                      
                    </div>
                <div class="modal-footer">
                    <span class="alert alert-danger" id="ScheduleErrorMessage" style="display: none"></span>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-danger" id="okBtn" data-dismiss="modal">Save</button>
                </div>
            
        </div>
    </div>
	</div>
	 <!------------------------------>
        <!--- modal ends here for adding new conttact-->

        <!-- Modal -->
        <div id="repeatCall" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" style="outline:none ! important">&times;</button>
                        <%--<h4 class="modal-title">Modal Header</h4>--%>
                    </div>
                    <div class="modal-body">
                        <p>Repeat</p>
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
                    <div class="modal-footer">
                        <div class="col-md-6 col-sm-6 text-right">
                            <button type="button" class="btn btn-danger" id="setRepeat" data-dismiss="modal">Set</button>
                        </div>
                        <div class="col-md-6 col-sm-6 text-left">
                            <button type="button" class="btn btn-default" id="clearRepeat">Clear</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal -->

    </div>

    <div class="row">
        <div class="col-md-8 col-sm-12 text-center margin-bottom-20 pull-right">
            
        </div>
        <input type="hidden" id="hostNumber" value="<%= hostNumber %>" />
         <input type="hidden" id="countryID" value="<%= countryID %>" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagejs" runat="server">
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
	<script type="text/javascript" src="Scripts/jquery.fileupload.js"></script>
    <script type="text/javascript" src="Scripts/excelUpload.js"></script>
    <script type="text/javascript" src="Scripts/jquery.Jcrop.js"></script>
	<script type="text/javascript" src="js/bootbox.min.js"></script>
    <script type="text/javascript" src="js/DateTimePickerModified.js?type=v7"></script>
	<script type="text/javascript" src="Scripts/jquery.fileupload.js"></script>
    <script type="text/javascript" src="Scripts/editgrouptalk.js?type=v7"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#grpCallWebContacts').slimScroll({
                allowPageScroll: false,
                height: '250',

            });
            //$('.tab-content').slimScroll({
            //    allowPageScroll: false,
            //    height: '250',
            //});

            $('#alphabetWebContacts,#alphabetMobileContacts').slimScroll({
                allowPageScroll: false,
                height: '250'
            });
			$('#list-group').slimScroll({
                allowPageScroll: false,
                height: '500',

            });
        });
		 function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
            { return false; }
            return true;
        }
    </script>
</asp:Content>
