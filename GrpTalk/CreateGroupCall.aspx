<%@ Page Title="" Language="C#" MasterPageFile="~/WebMasterPage.Master" AutoEventWireup="true" CodeBehind="CreateGroupCall.aspx.cs" Inherits="GrpTalk.CreateGroupCall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagetitle" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="pagecss" runat="server">
     <link href="assets/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" type="text/css" />
    <link href="/css/jquery.jcrop.css" rel="stylesheet" />
    <style type="text/css">
        #profilePic, .profileDetails {float: left;}
        #profilePic img {width: 30px;}
        .profileDetails {margin: 0 0 0 10px;}
        .profileDetails p {margin: 0 0 0 0px;font-size: 11px;}
        .members {padding: 10px;border: 1px solid #ccc;border-radius: 5px;width: 18%; /*overflow:hidden;*/position: relative;background-color: #eaeaea;float: left;margin: 0 10px 10px 0;}
         ul {list-style-type:none;}
        .btn {font-weight:bold;}
        .clear {clear:both;}
        .navbar-form {padding:0 0 0 0;}
        .input-group .btn-default,#srch-term {border-color:#ccc !important;}
        #search-member{width:100%;}
        #myTab {margin-top:8px;}
        .ui-accordion .ui-accordion-header{/*box-shadow: 0 -2px grey;*/-webkit-box-shadow: 0 -5px 6px -6px black;-moz-box-shadow: 0 -5px 6px -6px black;box-shadow: 0 -5px 6px -6px black;}
        .ui-accordion .ui-accordion-header:first-child {box-shadow:none;}
        .ui-state-default, .ui-widget-content .ui-state-default, .ui-widget-header .ui-state-default {background:none !important;}
        .ui-accordion .ui-accordion-header.class2 {background-color : #ccc !important;font-weight: 600;height:35px;}
            .ui-accordion .ui-accordion-header.class2 ui-accordion-header-icon ui-icon ui-icon-triangle-1-e {
                color:#fff !important;
            }
        .ui-accordion .ui-accordion-header.class1 {background-color : #808080 !important;font-weight: 600;height:35px;}
        .table td,.table th {text-align:center;}
        .ui-accordion .ui-accordion-header {margin:-3px 0 0 0;font-size:75%;border-bottom-left-radius: 0;border-bottom-right-radius: 0;}
        #profilePic, .profileDetails {float:left;}
        .profileDetails {margin:0 0 0 10px;}
        .profileDetails p{margin:0 0 0 0px;}
        #accordion .members {padding:10px;border:1px solid #ccc;border-radius:5px;width:18%;/*overflow:hidden;*/position:relative;background-color: #eaeaea;float:left;margin:0 10px 10px 0}
        #accordion #profilePic, .profileDetails {float:left;}
        #accordion #profilePic img {width:30px;}
        #accordion .profileDetails {margin:0 0 0 10px;}
        #accordion .profileDetails p{margin:0 0 0 0px;font-size:11px;}
        /*.fa-plus-square {float:left;}*/
        i.fa-plus-square {font-size:30px;color: #357ebd;margin-top: 7px;}
        .startschedule {width:100%;text-align:center;padding:20px 0 20px 0;}
        .startschedule input{border-bottom:1px solid orange;width:50%;text-align:center;padding:20px 0 0 0;border-top:0;border-left:0;border-right:0;}
        .list-group h3.list-group-item-info {color: #31708f !important;}
        .list-group .list-group-item-info {background-color: #d9edf7 !important;color: #31708f !important;}
        /*.accordionContent {
            height: 100px;overflow-y:scroll;
        }*/
         jcrop-circle-demo .jcrop-box {position: absolute;top: 0px;left: 50px;width: 300%;height: 300%;border: 1px rgba(255, 255, 255, 0.4) solid;border-radius: 50%;-webkit-box-shadow: 1px 1px 26px #000000;-moz-box-shadow: 1px 1px 26px #000000;box-shadow: 1px 1px 26px #000000;overflow: hidden;}

        .jcrop-circle-demo .jcrop-box:focus {outline: none;}

        #upload_contacts {float:left;display:none;}
        input[type="file"] {display: none;}
        input[type="text"] {padding:0 0 0 10px;border:1px solid #ccc;font-size:12px;}
        .custom-file-upload {border: 1px solid #ccc;display: inline-block;padding: 5px 12px;cursor: pointer;font-size:13px;}
        #grpCallMobileContacts {height:150px;overflow-y:scroll;}
        .accordionContent {height:180px;overflow-y:scroll;}
        .text-success {
              color: rgb(79, 138, 16) !important;
              font-size: 15px;
              font-weight: bold;
              float: left;
        }
        .text-danger {
              color: rgb(79, 138, 16) !important;
              font-size: 15px;
              font-weight: bold;
              float: left;
        }

    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" runat="server">
    <div class="page-container">
        <div class="page-content" style="min-height: 531px;">
            <div class="container grpTalkContactsContainer">
                 <div class="row margin-top-10">
                    <div class="col-md-12">
                        <div class="portlet light padding-bottom-0">
                             <div class="row margin-top-5">
                                   <div id="contactsDiv">
                                         <ul class="nav nav-tabs" id="myTab">
                                            <li><a data-toggle="tab" href="#grpCallWebContacts">Web Lists</a></li>
                                            <li><a data-toggle="tab" href="#grpCallMobileContacts">Mobile Contacts</a></li>
                                            <li><a data-toggle="tab" href="#selectedContacts">Selected Contacts<span class="count">(0)</span></a></li>
                                          </ul>

                                        <div class="tab-content">
                                            <div id="grpCallWebContacts" class="tab-pane fade in active">
                                              <div id="accordion">

                                              </div>
                                            </div>
                                             <div id="grpCallMobileContacts" class="tab-pane fade">
                                                 <div id="mobileContacts"></div>
                                            </div>
                                            <div id="selectedContacts" class="tab-pane fade">
                                                
                                            </div>
                                         </div>
                                    </div>
                                    <div class="startschedule">
                                    <input type="text" placeholder="Enter grpTalk Name" class="margin-bottom-20"/><br />
                                    <button type="button" value="Start Now" class="btn btn-primary" id="startnow">Start Now</button>
                                    <button type="button" value="Schedule" class="btn btn-default" id="schedule">Schedule</button>
                                </div>
                              </div>
                        </div>
                    </div>
                 </div>
            </div>
        </div><!-- page content div ends here -->
    </div><!---- page container div ends here-->

     <!---- modal starts here for adding new conttact-------->
     <div id="contactsModal" class="modal fade" role="dialog" style="display: none;">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <div>
                            <ul class="nav nav-tabs">
                              <li class="active"><a data-toggle="tab" id="newContact">Add Contacts</a></li>
                              <li><a data-toggle="tab" id="excelUpload">Excel Upload</a></li>
                            </ul>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div id="contactFormBody" style="display:block;">
                        <div class="form-group">
                            <label for="name">Name:</label>
                            <input type="text" class="form-control" id="name" name="name" />
                            <span class="text-danger" id="errorDescForName" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                        </div>
                        <div class="form-group">
                            <label for="mobileNumber">Mobile:</label>
                            <select class="form-control" id="prefix" style="width: 80px;">
                                <option value="+971">971</option>
                                <option value="+91">91</option>
                                <option value="+973">973</option>
                                <option value="+1">1</option>
                            </select>
                            <input type="text" class="form-control" id="mobileNumber" name="mobileNumber" />
                            <span class="text-danger" id="errorDescForMobile" style="margin: 0px; padding: 0px; font-size: 12px;"></span>
                        </div>
                    <div class="form-group">
                        <label for="imageUpload">Image Upload:</label>
                        <img id="webContactProfile" alt="profile pic" style="width: 150px; height: 150px;" src="" /><br />
                        <input type="file" id="profileUpload" value="Choose File" />
                    </div>
                        
                    </div>
                    <div id="excelFormBody" style="display:none;">
                        <input type="file" id="xlUpload" style="display:block;" />
                    </div>
                    </div>
                    <div class="modal-footer">
                        <span class="text-success" id="successMessage"></span>
                         <span class="text-danger" id="errorMessage"></span>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" class="btn btn-primary" id="saveContact">Add Contact</button>
                        <button type="button" class="btn btn-success" id="saveExcelContacts" style="display: none;">Upload</button>
                    </div>
                </div>

            </div>
        </div>
        <!--- modal ends here for adding new conttact-->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="pagejs" runat="server">
   
    
     <script type="text/javascript" src="Scripts/jquery.fileupload.js"></script>
    <script type="text/javascript" src="Scripts/jquery.Jcrop.js"></script>
    <script type="text/javascript" src="Scripts/infinite-scoll.js"></script>
    <script type="text/javascript" src="Scripts/createGroupCall.js"></script>
    <script type="text/javascript">
        
    </script>
</asp:Content>

