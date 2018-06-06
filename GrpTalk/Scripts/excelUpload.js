var excelContacts='';
var eresponse;
var xlfile_path = "";
var regx = "";
regx = /([\<])([^\>]{1,})*([\>])/i;
$(document).ready(function () {
	
	 $('#close').click(function () {
        $('#contactsModal').modal('hide');
        $('#headerselect').hide();
        $('#sheeterr').hide();
        $("#newContact").show();

    });
	
    $(".radio_head").click(function () {
        var item = "";
        item = $(this).attr("value");
        if (xlfile_path == '') {
            $('#sheeterr').show();
            $('#sheettxt').text("Please upload Excel file");
            return false;

        }
        else {
            SetXlColumns(eresponse)
        }
    });
    
    $("#saveExcelContacts").click(function () {
		if ($('#upmsg').html().length == 0) {
            $('#sheeterr').show();
            $('#sheettxt').text("Please upload Excel file");
            return false;
        }
		
		
		if($('#ddlWebList option').filter(function () { return $(this).html().toLowerCase() == $('#xlNewWebList').val().toLowerCase(); }).text()!="")
		 {	
			 if($('#xlListItems').css('display') != 'none' && $.trim($("#xlNewWebList").val())!='')
			 {
			 Notifier('This Web list name is already existed', 2);
			 return false;
			 }
		 } 
		
        $('#extgrp').hide();
        $("#xlinfo_div").show();
		

        //SPinnerJs()
        creating();
    });
});
 $('input[type="file"]').css('color', 'none');
$("#excelUploadFile").fileupload({
    dataType: 'json',
    url: 'ExcelUpload.ashx',
    autoUpload: 'false',
    acceptFileTypes: /(\.|\/)(xls|xlsx)$/i,
    maxFileSize: 5000000, // 5 MB
    start: function (e) {
		$('input[name="radiog_head"]').prop('checked', false);
		 $("#headerselect").hide();
		 $('#upmsg').html('');
		 $('input[type="file"]').css('color', 'transparent');
        //$.blockUI({ message: "<h4>Excel File Is Uploading...</h4>" });
    },
    success: function (res) {
        if (res.Success) {
            $("#headerselect").show();
			$('#xlinfo_div').show();
            eresponse = res;
            SetXlColumns(res);
            xlfile_path = res.FilePath;
			$('#upmsg').html(xlfile_path);
        }
        else {
            $('#sheeterr').show();
            $('#sheettxt').text(res.Message);
			$('#upmsg').html('');
        }
    },

    error: function (e) { console.log(e); },
    done: function (e, data) {
        console.log(data);

        status = 0;
    },
    fail: function (e, data) {
        console.log(data);
        if (data.textStatus == 'error') {
            $("#err_xcel").attr("class", "alert alert-error");
            $("#err_xcel span").text("please upload files less than 5MB");
        }
    }

});


function SetXlColumns(response) {
    var options = ""; sheetsCount = 0, sheetsInfo = "", ddlOptions = "", sheetsInfo1 = "";
    var sheetscnt = {}, sheethead = {};
    sheetsCount = response.ExcelSheets.length
    sheetsxl = { "sheetsInfo": [] };
    var headerletters = ["A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O"]
    var header = $("input[name=radiog_head]:checked").val();
    if (header === undefined) {
        header = 1;
    }
    if (sheetsCount > 0) {
        //for (i = 0; i < sheetsCount; i++) {
            // if (response.ExcelSheets[0].SheetName.match(regx) != null || response.ExcelSheets[0].SheetName.indexOf("'") >= 0 || response.ExcelSheets[0].SheetName.indexOf(" ") >= 0) {
				if (response.ExcelSheets[0].SheetName.match(regx) != null){
                $('#sheeterr').show();
                $('#sheettxt').text("Sheet Name contains invalid characters or spaces,Please Rename the sheet then upload");
                xlfile_path = ''
                $(".radio_head").prop('checked', false);
                $(".radio_dept").prop('checked', false);
                $("#grpupload").show();
                $("#grpback").hide();
                return false;
            }
            else {
                $('#sheeterr').hide();
            }

            sheetscnt.sheetname = response.ExcelSheets[0].SheetName;
            sheetscnt.columnscount = response.ExcelSheets[0].ColumnsCount
            sheetsxl.sheetsInfo.push(sheetscnt);
            sheetscnt = {};
            ddlOptions = "";
            if (header == 1) {
                for (var k = 0; k < response.ExcelSheets[0].Header.length; k++) {
					
                    ddlOptions += "<option value='" + parseInt(k + 1) + "'>" + response.ExcelSheets[0].Header[k].header + "</option>";
                }
            }
            else {
                for (k = 0; k < response.ExcelSheets[0].Header.length; k++) {
					
                    ddlOptions += "<option value='" + parseInt(k + 1) + "'>Column " + headerletters[k] + "</option>";
                }
            }
			
			sheetsInfo += '<div>' + response.ExcelSheets[0].SheetName.replace(" ",'') + ' has ' + response.ExcelSheets[0].ColumnsCount + ' columns </div>';
			sheetsInfo += '<div class="row" style="margin-bottom:15px;"><div class="col-sm-6"><label>Name </label>';
			sheetsInfo += '<select name="select" id="ddlname_' + response.ExcelSheets[0].SheetName.replace(" ",'') + '" class="form-control" aria-required="true" aria-invalid="false" aria-describedby="select-error">';
			sheetsInfo += '<option value="0">Select</option>' + ddlOptions + '</select></div>';
			sheetsInfo += '<div class="col-sm-6"><label>Mobile <span class="required"> * </span></label>';
			sheetsInfo += '<select name="select" id="ddlmobile_' + response.ExcelSheets[0].SheetName.replace(" ",'') + '" class="form-control" aria-required="true" aria-invalid="false" aria-describedby="select-error">'
            sheetsInfo += "<option value='0'>Select</option>" + ddlOptions + "</select>"
			sheetsInfo += '</div></div>';
			
			sheetsInfo += '<div class="row" id="xlListItems" style="display:'+xlList+'"><div class="col-sm-6"><label class="control-label">Choose a Web List <span class="required"> * </span></label>';
			sheetsInfo += '<select name="select" id="ddlxlWebList" class="form-control" aria-required="true" aria-invalid="false" aria-describedby="select-error">'
            sheetsInfo +=  "</select></div>"
			sheetsInfo += '<div class="col-sm-6"><label class="control-label">Create a weblist<span class="required"> * </span></label>';
			sheetsInfo += '<input type="text" placeholder="Enter new Weblist Name" class="form-control" id="xlNewWebList" maxlength="25" /></div></div>';
            sheetsInfo += '<div class="text-center text-danger" style="margin-top:15px;" id="errorDescForXLWebList" font-size:13px;></div>';
            
            //sheetsInfo += ' <div class="form-group col-md-6"><label class="control-label col-md-3 col-xs-4">Email</span></label><div class="col-md-9  col-xs-8">'
            //sheetsInfo += '<select name="select" id="ddlemail_' + response.ExcelSheets[i].SheetName + '" class="form-control" aria-required="true" aria-invalid="false" aria-describedby="select-error">'

            // sheetsInfo1 += '<div class="form-group ddlgroup_sheet" id="ddlgroup_' + response.ExcelSheets[i].SheetName + '"><label class="control-label">Select Group Name From ' + response.ExcelSheets[i].SheetName + ': </span></label>'
            // sheetsInfo1 += '<select name="select" id="ddlgrpname_' + response.ExcelSheets[i].SheetName + '" class="form-control ddl_select" aria-required="true" aria-invalid="false" aria-describedby="select-error">'
            // sheetsInfo1 += "<option value='0'>Select</option>" + ddlOptions + "</select></div>"
            //sheetsInfo += "</div>"
			   
			
            options = "";

            $('#xlinfo').html(sheetsInfo);
			$("#ddlxlWebList").html($("#ddlWebList").html());
            //  $('#grp_column').html(sheetsInfo1);

            $('#grp_select').hide();
            if ($("#tree_4 li .jstree-wholerow").length == 1) {
                $("#radio1").hide()
                $("#radio1").next().hide()
            }
            else {
                $("#radio1").show()
                $("#radio1").next().show()
                $(".radio_dept").prop('checked', false);
            }
        //}
    }

}

function creating() {
   
    var header = $("input[name=radiog_head]:checked").val();

    if (header == '1') { }
    else if (header == '2') { }
    else {
        $('#sheeterr').show();
        $('#sheettxt').text("Please Select Header Type");
    
        return false;
    }
	

    var sheetname = "", email = "", cnt = 0, name = "", mobile = "", designation = "", department = "", location = "", group = "", finaldata = { "data": [] };
    var semidata = {}, deptfrom = "";
    for (var x = 0; x < sheetsxl.sheetsInfo.length; x++) {
        sheetname = "", email = "", cnt = 0, name = "", mobile = "", designation = "", department = "", location = "", group = "", deptfrom = "";
        semidata = {};
        cnt = sheetsxl.sheetsInfo[x].columnscount;
        sheetname = sheetsxl.sheetsInfo[x].sheetname.replace(" ",'');
       
        name = $('#ddlname_' + sheetname).val()
        mobile = $('#ddlmobile_' + sheetname).val()
        emial = $('#ddlemail_' + sheetname).val();
       
        if (name == "0") {
            $('#sheeterr').show();
            $('#sheettxt').text("please select name column for " + sheetname + " sheet");
           
            return false
        }
		else{
			$('#sheeterr').hide();
			$('#sheettxt').text("");
			}
        if (mobile == "0") {
            $('#sheeterr').show();
            $('#sheettxt').text("please select mobile column for " + sheetname + " sheet");
          
            return false
        }
		else{
			$('#sheeterr').hide();
			$('#sheettxt').text("");
		}
        if (name == mobile) {
            $('#sheeterr').show();
            $('#sheettxt').text("name and mobile columns should be different for " + sheetname + " sheet");
            return false
        }
		else{
			$('#sheeterr').hide();
			$('#sheettxt').text("");
			}
		if(xlList == "block")	
		{
			listId = $('#ddlxlWebList').val();
			newList = $('#xlNewWebList').val();
			
			if(parseInt(listId) == 0 && $.trim(newList) =='')
				{
						IsValid= false
						$('#errorDescForXLWebList').html("You must add this contact to an existing weblist or create a new list");
						return false;
				}
				else{
					$('#errorDescForXLWebList').html('');
					
				}
		}
		else
		{newList="";}
        semidata.sheetname = sheetsxl.sheetsInfo[x].sheetname;
        semidata.columnscount = cnt;
        semidata.name = name;
        semidata.mobile = mobile;
      
        finaldata.data.push(semidata);
    }

    var header = $("input[name=radiog_head]:checked").val();
    console.log(finaldata);
	
    $.ajax({
        url: '/HandlersWeb/Contacts.ashx',
        method: 'post',
        dataType: "json",
        //    data: { path: xlfile_path, moderator: $("#moderator option:selected").attr("number"), ConferenceId: ConferenceId },
        data: { type:10, path: '/' + xlfile_path, semidata: JSON.stringify(finaldata), header: header,listID: listId,listName: newList,mode:0},
        success: function (AjaxResponse) {
            // window.location.reload();
			if(AjaxResponse.Success ==  true)
			{
				if(parseInt(listId) == 0 && AjaxResponse.Details.length==0)
				{Notifier(AjaxResponse.Message,1);	}
			else if(parseInt(listId) == 0)
				{Notifier('New List Created Successfully',1);	}
			else{				
				
				$("#count"+AjaxResponse.Details[0].ListId).text(AjaxResponse.Details[0].ListCount);
				Notifier(AjaxResponse.Message,1);
				if($(".excelContacts").hasClass('active')==true)
				{
					
					$(".list").removeClass('highlight');
					
					$(".list#"+AjaxResponse.Details[0].ListId+"").addClass("highlight active");
					
				   listClickContactsDisplay(AjaxResponse.Details[0].ListId);
				}
				
				}
				$('#addWebListContacts').modal("hide");
				$('#contactsModal').modal("hide");
				if(xlList == "block")
				{
					//if( $(".excelContacts").hasClass('active')!=true)
					{	
				if($.trim(newList) !='' && parseInt(listId) == 0)
				{
					
					if(parseInt(creategropTalk) == 0)//contact page and edit page
					{	
					listId = AjaxResponse.Details[0].ListId ;
                    var ContactsWebList1 = '<div class="list" id="' + AjaxResponse.Details[0].ListId + '" >'; 
					ContactsWebList1 += '<div style="width:93%;float:left;"><a  id="' + AjaxResponse.Details[0].ListId + '"  href="javascript:;" class="contactList" lcount="'+AjaxResponse.Details[0].ListCount+'" lname="'+newList+'"><span  id="' + AjaxResponse.Details[0].ListId + '">' + newList + '</span>(<span id="count' + AjaxResponse.Details[0].ListId + '">' + AjaxResponse.Details[0].ListCount + '</span>)</a></div>';
					if($(".excelContacts").hasClass('active')==true)
					{
					ContactsWebList1 += '<div style="width:7%;float:right;text-align:center;"><a style="display: block;" href="javascript:;" data-toggle="dropdown" class=" dropdown dropdown-toggle"><i class="fa fa-ellipsis-v" aria-hidden="true"></i></a>';
					ContactsWebList1 += '<ul class="dropdown-menu dropdown-menu-right">';
					ContactsWebList1 += '<li class="addNewContact" listId="' + AjaxResponse.Details[0].ListId + '" unique="modal" ><a href="#" data-toggle="modal" listId="' + AjaxResponse.Details[0].ListId + '" data-target="#addWebListContacts">Add Contacts</a></li>';
					ContactsWebList1 += '<li class="editListName" listName="' + newList + '" id="' + AjaxResponse.Details[0].ListId + '" editListId="' + AjaxResponse.Details[0].ListId + '"><a href="#">Edit</a></li>';
					ContactsWebList1 += '<li class="deleteListName" id="' + AjaxResponse.Details[0].ListId + '"><a href="#">Delete</a></li>';
					ContactsWebList1 += ' </ul>';
					ContactsWebList1 += ' </div>';				
					}
					ContactsWebList1 += ' </div>';	
                    $('#list-group').append(ContactsWebList1);	
					$(".list").removeClass('highlight');						
					$(".list#"+AjaxResponse.Details[0].ListId+"").addClass("highlight active");
						if($(".excelContacts").hasClass('active')==true)//contact page
						 {	//if(($("a[lname='"+newList+"']").hasClass("contactList"))==false)
							listClickContactsDisplay(AjaxResponse.Details[0].ListId);
						 }
						 else // edit page
						 { webPageIndex=0;
							 getAllWebLists(AjaxResponse.Details[0].ListId)
						 }
					}
					else
					$('#list-group1').append('<div class="list1"><div style="width: 100%; float: left;"><a data-target="#" href="javascript:void(0);" style="display: block;" id="'+AjaxResponse.Details[0].ListId+'" class="contactList1">'+newList+'('+AjaxResponse.Details[0].ListCount+')</a> </div></div>')
				}
				else
				{
					$('.contactList1').each(function(e){
						var contactLis= $('.contactList1')[e];
						if($(contactLis).attr("id") == AjaxResponse.Details[0].ListId)
						{
							$(contactLis).html(AjaxResponse.Details[0].ListName+'('+AjaxResponse.Details[0].ListCount+')');
						}
					});
					if(parseInt(creategropTalk) == 0)
					{
						// $('.contactList').each(function(e){
							// var contactLis= $('.contactList')[e];
							// if($(contactLis).attr("id") == AjaxResponse.Details[0].ListId)
							// {
								// 
								// //$(contactLis).html(AjaxResponse.Details[0].ListName+'('+AjaxResponse.Details[0].ListCount+')');
							// }
						// });
						$("span[id='count" + listId + "']").text(AjaxResponse.Details[0].ListCount);
					}
				}
				}
				}
				if(AjaxResponse.Duplicates.length>0){
					  excelContacts=''
                        var str='',SameNameId='',SameName='',firstNumber='';
						var contactIdAppend=0;var bool=true,hideCount=0;
						var sortArray=new Array();
                        for(var i=0;i<AjaxResponse.Duplicates.length;i++)	
						{
							
						  if(AjaxResponse.Duplicates[i].ContactId==0)
							{                                SameNameId='';
								contactIdAppend =0;
								bool=true;
								firstNumber=AjaxResponse.Duplicates[i].Mobile;
								SameName=AjaxResponse.Duplicates[i].Name.toLowerCase();
								str ='';
								str+='<div class="well well-sm margin-bottom-10">';
								str+='<div class="row"><div class="col-sm-2">'+AjaxResponse.Duplicates[i].Mobile+'</div>';
								
								str+='<div class="col-sm-10" id="'+firstNumber+'"></div></div></div>';
								if(i==0)
						       $("#excelDup").html(str);
								else
								$("#excelDup").append(str);	
							   str ='';
							}							
							
							if(SameName==AjaxResponse.Duplicates[i].Name.toLowerCase() && AjaxResponse.Duplicates[i].ContactId!=0)
                                SameNameId=AjaxResponse.Duplicates[i].ContactId;	
					            else
								{
								if(AjaxResponse.Duplicates[i].ContactId==0)
									str+='<label class="pull-left margin-right-15"><input type="radio" class="margin-right-5 duplicateContacts" checked="checked" name="repeat'+firstNumber+'" id="'+AjaxResponse.Duplicates[i].ContactId+'" value="'+AjaxResponse.Duplicates[i].Name+'"/>'+AjaxResponse.Duplicates[i].Name+'</label>';
								else		
								str+='<label class="pull-left margin-right-15"><input type="radio" class="margin-right-5 duplicateContacts" name="repeat'+firstNumber+'" id="'+AjaxResponse.Duplicates[i].ContactId+'" value="'+AjaxResponse.Duplicates[i].Name+'"/>'+AjaxResponse.Duplicates[i].Name+'</label>';
								}
                             
							 if(i == AjaxResponse.Duplicates.length-1 || AjaxResponse.Duplicates[i+1].ContactId==0)
							 {
							    $("#"+firstNumber).html(str);
						    	if(SameNameId!='')
							    $('#'+firstNumber+' label input:eq(0)').attr('id',SameNameId)
							    if($('#'+firstNumber).children().length==1)
								{ 
							        var singleContactId=$('#'+firstNumber+' label input:eq(0)').attr('id');
							        excelContacts+='{"contactid":'+singleContactId+',"name":"","mobilenumber":""},';
									$('#'+firstNumber).parent().parent().hide();
									hideCount++;
									
								}								
									
							 }			
						
							
						}							
						                       
						//$("#excelDup").html(str);
						//if($("input[name='repeat']").length==1)
						{							
							// $("input[name='repeat']:first").attr("checked",true);
							// $("#btnSave").click();
						// }
						// else
						// {
						$("#addWebListContacts").modal('hide');
						$("#btnSave,#btnSaveExcel").show();
						$("#btnSave").attr('id','btnSaveExcel');
						if(hideCount!=$('.duplicateContacts').length)
						$("#selectDblContacts").modal('show');
					      else
						$("#btnSaveExcel").click();	  
						
						}
						
							
					}
			}
        else
		Notifier(AjaxResponse.Message,2);
	//$('#addWebListContacts').modal("hide");
		},
        error: function () {
			alert("Something Went Wrong");
        }
    });
	

}

$(document).on('click','#btnSaveExcel',function(){
	excelContacts ="";
	$('.duplicateContacts').each(function() {
		if ($(this).prop("checked"))	    
	    excelContacts+='{"contactid":"'+this.id+'","name":"'+this.value+'","mobilenumber":"'+$(this).parent().parent().attr('id')+'"},';
	});	
	excelContacts='{"excelDuplicates":['+excelContacts.slice(0,-1)+']}';
	
	excelContacts = excelContacts;
	
	  $.ajax({
        url: '/HandlersWeb/Contacts.ashx',
		type: 'POST',
		async: false,
		dataType: 'JSON',
        //    data: { path: xlfile_path, moderator: $("#moderator option:selected").attr("number"), ConferenceId: ConferenceId },
        data: { type:10, listID: listId,listName: '',excelContacts: excelContacts,mode:1},
        success: function (AjaxResponse) {
		if(AjaxResponse.Status == 1)
		{
			Notifier(AjaxResponse.Message,1);
			listClickContactsDisplay(listId);
			}
			
		},
		error: function(AjaxResponse)
		{},
	  });

});

$(document).on("change","#ddlxlWebList",function(e){	
	var selectedValue=$('#ddlxlWebList').val();
	
		if(parseInt(selectedValue) != 0 )
		{
			if($('#xlNewWebList').val().length > 0)
			{
				Notifier('You can select either Weblist or you can create a new list',2);
				$('#xlNewWebList').val('');
			
			}
			
			
			$('#xlNewWebList').attr('disabled',true);
			$('#errorDescForWebList').html('');
		}
		else{
				$('#xlNewWebList').attr('disabled',false);
				$('#errorDescForWebList').html('');
			}
});