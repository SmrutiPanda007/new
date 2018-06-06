<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="CreateGroup.aspx.cs" Inherits="GrpTalk.CreateGroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="css/DateTimePicker.css" rel="stylesheet" type="text/css" />
    <link href="/css/jquery.jcrop.css" rel="stylesheet" type="text/css"/>
    <style type="text/css">
	.selected{position:relative;}
	.selected i.select_check{position:absolute; top:35px; right:5px; font-size:17px; color:#00cd67;}
	
	.unselected{position:relative;}
	.unselected i.select_check{position:absolute; top:35px; right:5px; font-size:17px; color:#00cd67;}
	#selectedContacts #profilePic img, #grpCallMobileContacts #profilePic img{width:50px;}
	#selectedContacts .profileDetails p, #grpCallMobileContacts .profileDetails p{margin:0;white-space: nowrap;}
	#selectedContacts .profileDetails p:last-child, #grpCallMobileContacts .profileDetails p:last-child{color:#0a93d7;}
		.highlight{
            background-color: #0282c2 !important;
        }
		.modal-header{border-bottom:0 !important;}
		.modal-body{min-height:206px;}
        .list1.active {background-color:#1c252b;}
        .list1.active .calendar {background:none;}
        .list1.active .calendar p{color:#fff;}
        .list1.active .participantsList p:nth-child(2) {color:#fff !important; }
        #contactsSearch {margin:0 0 0 0 !important;}
        #contactsTab li a{border:1px solid #ccc;border-radius:0;margin-right:-1px;color:#000;}
        #contactsTab li.active a{color:#0082c3;}
        #webLists,#phoneContacts {background-color:#e3e3e3;padding:10px;overflow: hidden;}
        .contacts {background-color:#fff;padding:10px;width:32.9%;float:left;position:relative;}
		.contacts, .contactDetails{width:19% ! important}
        #profilePic {float:left;margin-right:8px;}
        #profilePic img{width:50px;}
        #profileDetails {float:left;}
        #profileDetails p{margin:0;white-space: nowrap;}
        #profileDetails p:last-child{color:#0a93d7;}
        .contact_actions {position:absolute;top:0px;right:10px;}
        .contact_actions a{margin:0 5px;}
        .contact_actions .fa-trash-o{color:red;}
        .contact_actions .fa-edit{color:orange;}
        .fa-ellipsis-v{cursor:pointer;/*position:absolute;top:7px;right:2px;float:right;*/}
        .fa-ellipsis-v { display:none !important; }
        #listTitle1 ul li a {color:#fff;}
        .dropdown-menu {min-width: 115px !important;}
        .dropdown-menu li a{color:#000 !important;}
        /*.btn-group .btn {margin-left: 0 !important;}*/

        /*.btn-group .btn:hover {background-color:#0282c2 !important;}*/
        /*#list-group1 .btn {border-radius:0;border-bottom:1px solid #0678b0;background-color:#0a93d7;}*/
        #list-group1 .list1 {/*overflow:hidden;*/position:relative;border-radius:0;border-bottom:1px solid #0678b0;background-color:#0a93d7;height:47px;padding:10px;color:#fff;}
        #list-group1 .list1 a{color:#fff;}

        .tab-content {position:relative;}
        <!-- .alphabet {list-style-type: none;top:5px;right:10px;padding:0;cursor: pointer;text-align:center;line-height:15px;}
        .alphabet li {margin:0;padding:0;font-size: 11px;-moz-box-sizing:border-box;color:black;-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box;width:3.84%;}
        .alphabet li:last-child {border-right: none;}
        .alphabet li:hover {color:green;background-color: lightgrey;} -->
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
        #addCntct {background-color:#0a93d7;border:0;border:1px solid #0a93d7;}
        #close {border:1px solid #ccc;}
        #addWebListContacts .modal-dialog {width:800px;}
        .dropdown-menu-right {top:35px !important;}
        #grpCallMobileContacts,#selectedContacts, #alphabetMobileContacts{background-color:#e3e3e3;overflow: hidden;padding: 10px;}
        .btn-success {background-color: #45b6af !important;border-color: #3ea49d;color: #fff;}

          .startschedule input[type="text"]{border-bottom: 2px solid #ffbc5d; margin:10px 10px 0px 10px;width:25%;text-align:center;padding:7px 0 7px 0;border-top:0;border-left:0;border-right:0;}
        #saveGrpCall,#setRepeat {background-color:#0082c3;border:2px solid #0082c3;}
        #clearRepeat {border:2px solid #ccc;}

        .datePicker,#repeatCall {text-align:center;}
        .weekDay {border:2px solid #c1bebc !important;line-height:17px;font-weight:600;/*cursor:pointer;*/color:#c1bebc;margin:0 10px;background-color: #e8e8e8; border-radius: 50% !important; /*padding: 10px;*/ width: 45px; height: 45px; font-size: 16px;float:left;}
        .weekDay.active {border:2px solid #3b7cc2 !important;line-height:17px;font-weight:600;color:#fff !important;background-color: #6192c2;}
        .modal-header {border-bottom:0 !important;}
        #startNow {background-color:#e79e36;border:2px solid #e79e36;     margin:0px 10px;}
        /*#schedule {border:2px solid #63caed; background-color:#63caed !important;color: #fff;}*/
        .text-success {color: rgb(79, 138, 16) !important;font-size: 15px;font-weight: bold; float: left;}
        .text-danger { color: #a94442 !important; font-size: 15px;font-weight: bold; float: left;}
        .fa-check{  float: left;
    font-size: 20px;
    margin-left: 208px;
    margin-top: -32px;}
        #listTitle1 ul {
    list-style-type: none;
}
        #listTitle1 ul li a {
    color: #fff;
}
   .list1:hover {
            background-color: #0282c2 !important;
        }

        .highlight {
            background-color: #0282c2 !important;
        }

        a:focus {
            outline: none !important;
        }
		#listTitle1 {
    background-color: #263238;
    padding: 6px 0 !important;
    color: #fff;
}
.footer{padding:5px !important;}
#bg_color{background-color:#fff;     height: 503px; -webkit-box-shadow: 4px 4px 5px 0px rgba(214,214,214,1);
-moz-box-shadow: 4px 4px 5px 0px rgba(214,214,214,1);box-shadow: 4px 4px 5px 0px rgba(214,214,214,1);}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row">
      <!--   <div class="col-md-3 col-sm-3 margin-top-10 margin-bottom-10" id="callLogs">
            <div class="row" id="bg_color">
              <%--  <button type="button" class="btn btn-primary margin-bottom-20" id="createGrpList"><span class="fa fa-plus"></span>Create New List</button>--%>
                
					 <button id="phoneContactsDisplay" class="btn btn-primary margin-bottom-20" style="display:none;width: 100%; pointer-events: none; border: 0px none; background-color: rgb(10, 147, 215); border-radius: 0px; font-size: 18px; font-weight: bold; " type="button">Phone Contacts</button>
					<div class="phnCntcts" style="min-height: 345px;display:none;">
					<div id="phoneContactsText" style="width: 85%; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); font-size: 18px; color: rgb(66, 139, 202); font-weight: 600; text-align: center; display: block;">You can modify phone contacts from your mobile device only</div>
					</div>
                </div>
            </div> -->
         <div class="col-md-12 col-sm-12 margin-top-10 margin-bottom-10">
            <div class="row" id="listTitle1">
                <div class="col-md-12 col-sm-12">
                    <b>Select Contacts</b>
                    <ul>
                        <!--<li>
                            <a href="javascript:;"  class="addNewContact"><i aria-hidden="true" class="fa fa-plus-square"></i></a></li>
                        <a href="javascript:;" data-toggle="modal" data-target="#contactsModal" class="addNewContact"><i aria-hidden="true" class="fa fa-plus-square"></i></a></li>-->
                    </ul>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 padding-left-0 padding-right-0">
                    <form class="navbar-form" role="search" id="contactsSearch">
                        <div class="input-group" style="width: 100%;">
                            <input type="text" class="form-control" placeholder="Search" id="search-input" autocomplete="off"/>
                            <div class="input-group-btn" style="width: 5%;">
                                <button class="btn btn-default" id="search"><i class="glyphicon glyphicon-search"></i></button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
             <div class="row margin-bottom-20">
               
                <ul class="nav nav-tabs padding-left-0 margin-bottom-15" id="myTabList">
                    
                    <li class="mobileContacts active"><a data-toggle="tab" href="#grpCallMobileContacts" style="cursor:pointer">Phone Contacts</a></li>
                    <li class="selectedContacts"><a data-toggle="tab" href="#selectedContacts" style="display:none;cursor:pointer">Selected Contacts<span class="count">(0)</span></a></li>
                </ul>
                <div class="tab-content">
                    
                  

                    
                    
                      <div id="grpCallMobileContacts" class="tab-pane fade in active">
                     </div>
                     
                    <div id="selectedContacts" class="tab-pane fade">
                        </div>
                      
             </div>
            </div>
			<div class="startschedule text-center">
                <input type="text" class="margin-bottom-20" id="grpTalkName" placeholder="Enter grpTalk Name" maxlength="20" style=" outline: none; !important" />
                
                <button id="startNow" class="btn btn-primary" value="Start Now" type="button">Start Now</button>
				 <button type="button" value="Start" class="btn btn-success" id="saveGroupCall" style="background-color:#45b64e !important;border: 2px solid #45b64e !important; margin:0px 10px 0px 0px;width:85px" >Save</button>  
                <button id="schedule" class="btn btn-danger" value="Schedule" type="button">Schedule</button>
            </div>
		</div>
        </div>


        <!---- modal starts here for adding new conttact-------->
     

                   <!-------------For adding Bulk Contacts through Excel Sheet in Web List--------------------->
                   
        <!--- modal ends here for adding new conttact-->

      <!------------------------- modal for datepicker------------------------->
      <div id="datepickerModal" class="modal fade" role="dialog" style="display: none;">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="pull-left" style="margin-bottom:0px;">Schedule GrpTalk Call On</h4>
                        <button class="close" data-dismiss="modal" type="button" style="outline:none ! important">×</button>
                    </div>
					</br>
                    <div class="modal-body">
                        
                       <div>
                           <label>Schedule Date:</label>
                            <input class="form-control form-control-inline date-picker custom-form" size="16" type="text" value="" data-field="datetime" placeholder="Date and Time" data-step="3"
                           data-intro="When do you want to have this conference" />
                          <div id="dtBox"></div>
                       </div><br />
					   <div><label>Repeats On</label>
                            <input type="text" style="cursor:pointer" class="form-control form-control-inline repeatWeekDays custom-form" id="repeat" value="" placeholder="Every Sun, Mon, Tue, Wed, Thu, Fri, Sat" data-field="repeat weekdays"  size="16"  data-target="#repeatCall"  />
                        </div>
                    </div>
                    <div class="modal-footer">
                         <span class="alert alert-danger" id="ScheduleErrorMessage" style="display:none"></span>
                         <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-danger" id="scheduleDate">Schedule</button>
                    </div>
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
                    <button type="button" class="close" data-dismiss="modal" style="outline:none ! important">&times;</button>
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
                        <button type="button" class="btn btn-default" id="clearRepeat" >Clear</button>
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
    <script type="text/javascript" src="Scripts/jquery.fileupload.js"></script>
    <script type="text/javascript" src="Scripts/jquery.Jcrop.js"></script>
    <script src="Scripts/DateTimePickerModified.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/jquery.slimscroll.js"></script>
    <script type="text/javascript" src="Scripts/excelUpload.js"></script>
    <script type="text/javascript" src="Scripts/creategroup.js?type=v3"></script>
    <script type="text/javascript">
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
 $('#grpCallMobileContacts').slimScroll({
        allowPageScroll: false,
        height: '380'
    });
                
            
    </script>
</asp:Content>
