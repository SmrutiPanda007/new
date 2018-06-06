<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="GrpTalk.Contacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="/css/jquery.jcrop.css" rel="stylesheet" />
    <style type="text/css">
        #listContactsModal li a{color:#404040;}
        #listContactsModal li.active a{color:#F95259;}
       .list.active {
            background-color: #1c252b;
        }
	.selected{position:relative;}
	.selected i.select_check{position:absolute; top:35px; right:5px; font-size:17px; color:#00cd67;}
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
	#mobileContacts .contacts, .contactDetails{width:32% ! important;}
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
            min-width: 150px !important;
        }

            .dropdown-menu li a {
                color: #000 !important;
            }
        /*.btn-group .btn {margin-left: 0 !important;}*/

        /*.btn-group .btn:hover {background-color:#0282c2 !important;}*/
        /*#list-group .btn {border-radius:0;border-bottom:1px solid #0678b0;background-color:#0a93d7;}*/
       
	   
	   #list-groupmodal .list,#list-group .list {
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #e6dede;
            background-color: #f3eded;
            height: 47px;
            color: #fff;
			cursor:pointer;
        }
      
            #list-groupmodal .list a,#list-group .list a {
                color: #606060;
                display: block;padding: 10px;
            }
            /*#list-group .highlight a{color:#fff !important;}*/
            #list-group .highlight a.contactList{color:#fff !important;}
        .tab-content {
            position: relative;border:0 !important;
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
            
        }
        .list:hover{
            background-color: #e9724f !important;
            
        }
      
         #list-groupmodal .list:hover a, #list-group .list:hover a{color:#fff;}
        .highlight{
            background-color: #e9724f !important;
        }
        a:focus {
            outline:none !important;
        }
        ul.dropdown-menu-right li a {
            padding:5px 10px !important;
        }
		  .fa-check {
            float: left;
            font-size: 20px;
            margin-left: 208px;
            margin-top: -32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row" style="min-height: 606px">
        <div class="col-md-3 col-sm-3 margin-top-20 margin-bottom-20" id="callLogs">
            <div class="row">
                <button type="button" class="btn btn-primary margin-bottom-20" id="createGrpList" style=" padding: 7px 12px; ! important"><span class="fa fa-plus" style="margin-right:10px"></span>Create New List</button>
                <div id="list-group">
                    <%--<div class="list">
                        <a href="javascript:;" id="getAllContacts">All Contacts</a><i class="fa fa-ellipsis-v dropdown dropdown-toggle" data-toggle="dropdown" aria-hidden="true"></i>
                        <ul class="dropdown-menu dropdown-menu-right">
                            <li class="addNewContact" listid="0"><a href="#">Add Contacts</a></li>
                        </ul>
                    </div>--%>
                </div>
                <!-- Phone Contacts Sidebar -->
                <button type="button" class="btn btn-primary margin-bottom-20" style="display: none; width: 100%; pointer-events: none; border: 0; background-color: #f95259; border-radius: 0; font-size: 18px; " id="phoneContactsDisplay">Phone Contacts</button>
                <div class="phnCntcts" style="min-height: 450px;display:none">
                    <div id="phoneContactsText" style="display: none; width: 85%; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); font-size: 18px; color: #909090;  text-align: center;">You can modify phone contacts from your mobile device only</div>
                </div>
                <!-- Phone Contacts Sidebar Ends -->

            </div>
        </div>
        <div class="col-md-9 col-sm-9 margin-top-20 margin-bottom-20">
            <div class="row" id="listTitle">
                <div class="col-md-12 col-sm-12">
                     <b id="listTitleName">Add Contacts</b>
                    <ul>
                        <li listid="0" unique="modal"><a href="javascript:;" data-toggle="modal" data-target="#addWebListContacts" listid="0"><i id="plusSymbol" aria-hidden="true" class="fa fa-plus-square" title="Add Contact"></i></a></li>
                    </ul>
                </div>
            </div>
            <div class="row margin-bottom-20">
                <ul class="nav nav-tabs padding-left-0 margin-bottom-20" id="contactsTab">
                    <li class="search" style="width: 74%;">
                        <form class="navbar-form" role="search" id="contactsSearch">
                            <div class="input-group" style="width: 100%;">
                                <input type="text" id="search-input" autocomplete="off" maxlength="20" class="form-control" placeholder="Search" />
                                <div class="input-group-btn" style="width: 5%;">
                                    <button class="btn btn-default" id="search" type="button"><i class="glyphicon glyphicon-search"></i></button>
                                </div>
                            </div>
                        </form>
                    </li>
                    <li class="active webContacts"><a data-toggle="tab" href="#webLists">Web Lists</a></li>
                    <li class="mobileContacts"><a data-toggle="tab" href="#phoneContacts">Phone Contacts</a></li>
                </ul>
				  <div style="text-align:center"><span id="searchCount"><br></span></div>
                <div class="tab-content">
                    <div id="webLists" class="tab-pane fade in active">
                    </div>
                    <div id="alphabetWebContacts" class="tab-pane fade">
                    </div>
                    <div id="phoneContacts" class="tab-pane fade">
                    </div>
                    <div id="alphabetMobileContacts" class="tab-pane fade">
                    </div>
                    <ul class="alphabet" style="display:none">
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



<!---- modal starts here for adding new conttact-------->
    <div id="selectDblContacts" class="modal fade" role="dialog" style="display: none;z-index:99999;" aria-hidden="false">
			
			<div class="modal-dialog">

				<!-- Modal content-->
				<div class="modal-content">
					<div class="modal-header">
						<h4 class="pull-center modal-title" style="margin-bottom: 0px; font-size:16px;  margin-top:0px; color:#606060;">
                            <span>We found the below contacts with other names in different web lists.<br /> Choose a name you want to use in the web list.</span></h4>
						<button class="close" data-dismiss="modal" style="position:absolute; right:15px; top:10px;outline: none !important" type="button">×</button>
					</div>
					<div  class="modal-body"  style="border-top:none; min-height:90px;">
                        <div style="max-height:450px; overflow:auto; font-size:14px;" id='excelDup'>
                       
					    </div>
                   </div>
					
					<div class="modal-footer text-center">
                        <button id="btnSave" type="button" class="btn btn-success" data-dismiss="modal">Save</button>
					<!-- <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button> -->
					</div>
				
				</div>
			</div>
	</div>
		
        <!-- Modal -->
       
        <div id="addWebListContacts" class="modal fade" role="dialog" data-keyboard="false" data-backdrop="static">
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
                            <li class="allContacts"><a data-toggle="tab" href="#allContacts">Web Lists</a></li>
                            <li class="mobileContacts"><a data-toggle="tab" href="#mobileContacts">Mobile Contacts</a></li>
                            <li class="excelContacts"><a data-toggle="tab" href="#excelFormBody">Excel Upload</a></li>
							 <button type="button" class="close" data-dismiss="modal" style="outline:none ! important">&times;</button>
                        </ul>
                        <div class="tab-content">
                            <div id="addListContactsModal" class="tab-pane fade in active">
                                <div class="row">
                                    <div class="col-sm-6">
                                    <label for="name">Name:</label>
                                    <input type="text" autocomplete="off" placeholder="Nick name" class="form-control" id="name" maxlength="20" />
                                    <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 13px;"></span>
                                </div>
                                     <div class="col-sm-6">
                                    <label for="mobile">Mobile Number ID:</label>
                                    <input type="text" placeholder="Mobile Number" class="form-control" id="mobileNumber" maxlength="12" onkeypress="return isNumber(event)"/>
                                    <span class="text-danger" id="errorDescForMobile" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
                                </div>
										 <div class="row" id="listItems" >
                                    <div class="col-sm-6">
                                    <label for="name">Choose a web list:</label>
                                    <select id="ddlWebList" class="form-control" style="outline: none ! important"></select>
                                    <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                                </div>
                                     <div class="col-sm-6">
                                    <label for="mobile">Create a Web List:</label>
                                    <input type="text" placeholder="Enter new Weblist Name" class="form-control" id="newWebList" maxlength="25" />
                                   
                                </div>
                                </div>
								<diV class="text-center text-danger " id="errorDescForWebList" style="font-size:13px; margin-top:15px; margin-bottom:15px;">
								
								</div>
                                <div class="row pull-center" style="margin-top:15px">
								<div class="col-sm-12">
                                    <label for="fileUpld">Image Upload:</label>
                                    <input type ="button" value = "Choose Image" onclick ="javascript: document.getElementById('imageUpld').click();"/>
                                    <input type="file" id="imageUpld" style="visibility:hidden"/>
									
									<img id="webContactProfile" alt="" style="width: 150px; height: 150px;" src="" />
                                </div>
								</div>
                            </div>
                            <div id="allContacts" class="tab-pane fade" style="background: #fff; padding: 5px; display: none"></div>
                            <div id="mobileContacts" class="tab-pane fade" style="background: #e3e3e3; overflow: hidden; padding: 5px;height: 300px; overflow-y: auto; display: none"">
                            </div>
                            <div id="excelFormBody" class="tab-pane fade" style="padding: 5px;">
                                <%-- <div class="container" id="page-container">--%>
								<div class="table-responsive">
								<table class="table no-border">
								<tr>
								<td class="col-sm-3" style="vertical-align:top !important;"><label style=min-width:130px;>Choose Contacts</label></td>
								<td>
								<input type ="button" value = "Choose File" class="btn btn-danger" onclick ="javascript: document.getElementById('excelUploadFile').click();"/>
                                <input type="file" accept=".xls,.xlsx" id="excelUploadFile" style="visibility:hidden;"/>
								<label id="upmsg" style="font-weight:normal; word-break:break-all;"></label>
								</td>
								</tr>
								<tr id="headerselect" style="display: none;">
								<td><label>Excel sheet has headers</label></td>
								<td>
								<label class="pull-left" style="margin-right:20px;">
								<input type="radio" value='1' class="css-checkbox radio_head" id="header_1" name="radiog_head" />
                                            <label class="css-rdb radiodchk" for="header_1">Yes</label>
								</label>
								<label class="pull-left">
								<input type="radio" class="css-checkbox radio_head" id="header_2" value='2' name="radiog_head" />
                                            <label class="css-rdb radiodchk" for="header_2">No</label>
								</label>
								</td>
								</tr>
								</table>
								</div>
                                
								
                               

                                    <div class="row">
                                        <div class="col-md-12 margin-top-15" id="xlinfo_div">
                                            <div class="scroller" id="xlinfo">
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
						<div class="modal-footer">
                        <button class="btn btn-danger" id="addCntctList">Add Contact</button>
                        <button type="button" class="btn btn-danger" id="saveExcelContacts" style="display: none;">Upload</button>
                        <button type="button" class="btn btn-danger" id="saveListContact" style="display: none;">Add To List</button>
                        <button type="button" class="btn btn-danger" id="updateContact" style="display: none">Update</button>
                        <button type="button" class="btn btn-default" id="closeList" data-dismiss="modal">Close</button>
                    </div>
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