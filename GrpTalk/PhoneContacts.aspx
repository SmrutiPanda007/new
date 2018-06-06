<%@ Page Title="" Language="C#" MasterPageFile="~/PostLoginMasterPage.Master" AutoEventWireup="true" CodeBehind="PhoneContacts.aspx.cs" Inherits="GrpTalk.PhoneContacts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="pagecss" runat="server">
    <link href="/css/jquery.jcrop.css" rel="stylesheet" />
    <style type="text/css">
	.contacts, .contactDetails{
		width: 19% !important;
		height: 16% !important;
	}
	p {
    margin: 0 0 1px !important;
}
       .list.active {
            background-color: #1c252b;
        }
	.selected{position:relative;}
	.selected i.select_check{position:absolute; top:35px; right:5px; font-size:17px; color:#00cd67;}
       
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
        #list-group .list {
            position: relative;
            border-radius: 0;
            border-bottom: 1px solid #0678b0;
            background-color: #0a93d7;
            height: 47px;
            color: #fff;
        }

            #list-group .list a {
                color: #fff;
                display: block;padding: 10px;
            }

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
            float: left;
        }
        .list:hover{
            background-color: #0282c2 !important;
        }
        .highlight{
            background-color: #0282c2 !important;
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
		.footer{padding:5px !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="maincontent" runat="server">
    <div class="row" style="min-height: 577px">
        
        <div class="col-md-12 col-sm-12 margin-top-10 margin-bottom-10">
            <div class="row" id="listTitle">
                <div class="col-md-12 col-sm-12">
                     <b id="listTitleName">Phone Contacts</b>
                    
                </div>
            </div>
            <div class="row margin-bottom-10">
                <ul class="nav nav-tabs padding-left-0 margin-bottom-15" id="contactsTab">
                    <li class="search" style="width: 100% !important;">
                        <form class="navbar-form" role="search" id="contactsSearch">
                            <div class="input-group" style="width: 100%;">
                                <input type="text" id="search-input" autocomplete="off" class="form-control" placeholder="Search" />
                                <div class="input-group-btn" style="width: 5%;">
                                    <button class="btn btn-default" id="search" ><i class="glyphicon glyphicon-search"></i></button>
                                </div>
                            </div>
                        </form>
                    </li>
                    
                    <!-- <li class="mobileContacts"><a data-toggle="tab" href="#phoneContacts">Phone Contacts</a></li> -->
                </ul>
				  <div style="text-align:center"><span id="searchCount"><br/></span></div>
                <div class="tab-content">
                    
                    
                    <div id="phoneContacts" class="tab-pane fade in">
                    </div>
                    <div id="alphabetMobileContacts" class="tab-pane fade">
                    </div>
                    <ul class="alphabet" style="display:block">
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




        
        </div>
        <input type="hidden" id="countryID" value="<%= countryID %>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="pagejs" runat="server">
    <script type="text/javascript" src="Scripts/phonecontacts.js"></script>
    
    <script type="text/javascript" src="Scripts/infinite-scoll.js"></script>
    <script type="text/javascript">
                   
            $('#phoneContacts').slimScroll({
                allowPageScroll: false,
                height: '480'
            });
        

    </script>
</asp:Content>