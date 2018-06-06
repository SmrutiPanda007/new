var jsonResponse = new Array();
var mobileDropDownlist = "";
var global_mobileNumber = "";
var subContactsResponse = new Array();
var sortSubContactsResponse = new Array();
var pageIndex = 0;
var mobileContactsData = "";
var pageCount = 0;
var alphabetPageIndex = 0;
var global_alphabetMobileContactsData = "";
var global_searchndex = 0;
var global_addContactList = 0;
var alphabetMobileClick = 0; mobileClick = 0, tabClick = 0;
var listContactsCount = 0;
var enterClick = false;
var mobheight, webHeight = 0;
var alphabetCOntactsCount = 0,alphabetPageCount=0;
var global_alphabet = '';
var global_alphabetpageIndex=0;

$(document).ready(function () {
    pageIndex = 0;
    mobileContactsData = "";
    getMobileContacts(1);
    $('#phoneContacts').show();

    $("ul#contactsTab li").click(function () {

        //$("ul#contactsTab li").removeClass("active");
        if (!$(this).hasClass("search")) {
            if (alphabetMobileClick > 0)
            { $('#alphabetMobileContacts').parent().hide(); }
            if (alphabetWebClick > 0)
            { $('#alphabetWebContacts').parent().hide(); }
        }
        var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content

        if (activeTab == "#phoneContacts") {
            $('.phnCntcts,#phoneContactsText').show();

            $('#phoneContacts').parent().show(); $('#phoneContacts').show();
            $('#phoneContactsDisplay,#phoneContactsText,#phoneContactsList').show();

            $('#search-input').val('');
            $('span[id=searchCount]').html('<br>');

            $('#alphabetMobileContacts').hide();
            global_searchndex = 0;

            tabClick++;

        }

    });



    //$(document).on("click", "#btnDropdown", function (mobileDropDownlist) {
    //    var mobileNumber = $(this).attr("mobile");
    //    var contactId = $(this).attr("contactid");
    //    global_mobileNumber = mobileNumber;
    //    var select = $("ul#mobileNumber" + mobileNumber);
    //    var subContactsHtml = "";
    //    subContactsHtml += "<li><a id=primary-" + mobileNumber + " href='#'>" + mobileNumber + "</a></li>";
    //    for (var i = 0; i < subContactsResponse.length; i++) {
    //        for (var j = 0; j < subContactsResponse[i].length; j++) {
    //            if (contactId == subContactsResponse[i][j].subContactId) {
    //                var anchorId = subContactsResponse[i][j].contactType + '-' + subContactsResponse[i][j].contactNumber;
    //                subContactsHtml += "<li><a id=" + anchorId + " href='#'>" + subContactsResponse[i][j].contactNumber + "</a></li>";
    //            }

    //        }

    //    }
    //    select.html(subContactsHtml);

    //});



    $(document).on("click", "div ul.dropdown-menu li a", function () {
        var anchorValue = $(this).text();
        $('#phoneNumber' + global_mobileNumber).text(anchorValue);
    });



    $(document).on("click", "#alphabtnDropdown", function (mobileDropDownlist) {
        var mobileNumber = $(this).attr("mobile");
        var contactId = $(this).attr("contactid");
        global_mobileNumber = mobileNumber;
        var select = $("#subMobileNumber" + mobileNumber);
        var subContactsHtml = "";
        subContactsHtml += "<li><a id=primary-" + mobileNumber + " href='#'>" + mobileNumber + "</a></li>";
        for (var i = 0; i < sortSubContactsResponse.length; i++) {
            for (var j = 0; j < sortSubContactsResponse[i].length; j++) {
                if (contactId == sortSubContactsResponse[i][j].subContactId) {
                    var anchorId = sortSubContactsResponse[i][j].contactType + '-' + sortSubContactsResponse[i][j].contactNumber;
                    subContactsHtml += "<li><a id=" + anchorId + " href='#'>" + sortSubContactsResponse[i][j].contactNumber + "</a></li>";
                }

            }

        }
        select.html(subContactsHtml);

    });



    $(document).on("click", "div ul.dropdown-menu li a", function () {
        var anchorValue = $(this).text();
        $('#subPhoneNumber' + global_mobileNumber).text(anchorValue);
    });


});

$('#phoneContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight-10) {
        getMobileContacts(1);
        mobheight = 450 / pageIndex;
        $(this).css("top", mobheight);

    }
});

$('#alphabetMobileContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
        alphabeticalContactSorting(1, global_alphabetpageIndex, global_alphabet, 0, 2)
        mobheight = $(this).innerHeight() / pageIndex;
        $(this).css("top", mobheight);

    }
});

function getMobileContacts(src) {
    global_addContactList = 0;
    pageIndex++;
    if (pageIndex == 1 || pageIndex <= pageCount) {
		if(pageIndex==1)
		{
        $.blockUI({ message: '<h4> Loading...</h4>' })
		}
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 1,
                source: src,
                pageIndex: pageIndex,
                modeSp: 1,
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (contactResult) {
                pageCount = contactResult.pageCount;
                if (contactResult.Success == true) {
                    jsonResponse = contactResult;
                    if (contactResult.contactDetails.length != 0) {
                        for (var contactRows = 0; contactRows < contactResult.contactDetails.length ; contactRows++) {
                            if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                                mobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5">';
                                if (contactResult.contactDetails[contactRows].imagePath == null) {
                                    mobileContactsData += '<div id="profilePic" ><img  src="images/DefaultUser.png"  alt="Profile Pic"></div>';
                                }

                                else if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '') {
                                    mobileContactsData += '<div id="profilePic" ><img src="/' + contactResult.contactDetails[contactRows].imagePath + '" alt="Profile Pic"></div>';
                                }
                                else {
                                    mobileContactsData += '<div id="profilePic" ><img  src="images/DefaultUser.png"  alt="Profile Pic"></div>';
                                }
                                mobileContactsData += '<div id="profileDetails">';
                                if (contactResult.contactDetails[contactRows].name.length > 17) {
									
                                    mobileContactsData += '<p title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 14) + '...</p>';
                                }
                                else { 
									if(contactResult.contactDetails[contactRows].name == "")
									{
										mobileContactsData += '<p>' + contactResult.contactDetails[contactRows].mobileNumber + '</p>'; 
									}else{
										mobileContactsData += '<p>' + contactResult.contactDetails[contactRows].name + '</p>'; 
									}
								
								}
                                if ($.trim(contactResult.contactDetails[contactRows].subContactDeatils).length != 0) {                                   
                                    subContactsResponse.push(contactResult.contactDetails[contactRows].subContactDeatils);
                                    mobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                                    mobileContactsData+='<div class="dropdown" style="float:right;margin: -3px 0 0;"><a id="btnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="dropdown-toggle" style="padding: 2px 3px;" data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" >';
                                    mobileContactsData += "<li class='ddlList'><a id=primary-" + contactResult.contactDetails[contactRows].mobileNumber + " >" + contactResult.contactDetails[contactRows].mobileNumber + "</a></li>";
                                    for (var sub = 0; sub < contactResult.contactDetails[contactRows].subContactDeatils.length; sub++)
                                    {                                      
                                        var anchorId = contactResult.contactDetails[contactRows].subContactDeatils[sub].contactType + '-' + contactResult.contactDetails[contactRows].subContactDeatils[sub].contactNumber;
                                        mobileContactsData += "<li class='ddlList'><a id=" + anchorId + ">" + contactResult.contactDetails[contactRows].subContactDeatils[sub].contactNumber + "</a></li>";
                                    }
                                    mobileContactsData += '</ul></div>';

                                }
                                else {
                                    mobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                                }
                                mobileContactsData += '</div>';
                                mobileContactsData += '</div>';
                            }
                        }
                        $('#phoneContacts').html(mobileContactsData);
                        if ($('#alphabetMobileContacts').parent('.slimScrollDiv').length != 0) {
                            $('#alphabetMobileContacts').parent().hide();
                            $('#alphabetMobileContacts').hide();
                            $('#phoneContacts').parent().show(); $('#phoneContacts').show();
                        }
                    }
                    setTimeout($.unblockUI, 200);
                }
                if (contactResult.Success == false)
                { 
					$('#phoneContacts').html('You have not synchronized your mobile contacts yet. Sync manually from your app to start!<div class="row block text-center col-md-offset-1" style="padding-top: 90px;width:60%"><div class="col-md-3 text-right" style="border-right:1px solid #d2d2d2"><img src="images/android.png" width="50" height="50"></div><div class="col-md-8 pull-left" style="font-size:20px;">Open grpTalk app in your mobile, Select "+" from home page, Press menu and select "Sync Contacts"</div></div><br><div class="row block text-center margin-top-20 col-md-offset-1" style="padding-bottom: 90px;width:60%"><div class="col-md-3 text-right" style="border-right:1px solid #d2d2d2"><img src="images/IOS.png" width="50" height="50"></div><div class="col-md-8 pull-left" style="font-size:20px;">Open grpTalk app in your mobile, Select "+" from home page, Press menu and select "Sync Contacts"</div></div>')
					$('#phoneContacts').css('font-size','22px')
					$('#phoneContacts').css('color','#0082C3')
					$('#phoneContacts').attr('align','center');
					setTimeout($.unblockUI, 200);
				}
            },
            error: function (contactResult) {
                $.unblockUI();
            }


        });
    }
}

$(document).on('click','.ddlList',function(){
    var classList=$(this).attr('class');
    classList=(classList.substring(13, classList.length))
    //$("#phoneNumber" + classList).text($(this).children().text());
    $(this).parents('.dropdown').prev().html('<i class="fa fa-mobile" aria-hidden="true"></i> ' + $(this).children().text());
});
$("#search-input").click(function (e) {
    $('#search').css("border-color", "#66afe9");
    // if ($.trim($('#search-input').val()).length != 0) {
        
        // var sourceValue = 1;
      
      
            // $('#alphabetMobileContacts').parent().show();
            // $('#alphabetMobileContacts').show();
       
    // }
    e.stopPropagation();
});

$('#search-input').keyup(function(e)
{
 e.preventDefault();
    if (e.which != 1 && e.which != 17 && e.which != 20 && e.which != 16) {
        if ($(this).val().length >= 3) {
            $('#search').click();
        }
        else {
            if ($(this).val().length == 0) {
                $('#search').click();
            }
        }
    }


    e.stopPropagation();
	
});

$(document).on('click', '#search', function (e) {
	e.preventDefault();
    global_searchndex = 0;

    var alphabet = $('#search-input').val();
    if (alphabet.length != 0) {
        alphabet = alphabet.toLowerCase();
    }
    else {        
            $('span[id=searchCount]').html('<br>');
            pageIndex = 0;
            mobileContactsData = "";
			subContactsResponse = new Array();
            getMobileContacts(1);
        
        return;

    }
    global_addContactList = 0;
    
    global_alphabetMobileContactsData = "";

	global_alphabet= alphabet;
    var sourceValue = 0;
    alphabetMobileOrWebClick(global_searchndex, 2, alphabet);
	e.stopPropagation();
});

$('#specialCharSearch').click(function (e) {
	e.preventDefault();
    var clickCounter = $(this).data('clickCounter') || 0;
    var alphabet = $(this).text().toLowerCase();
    var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
    var sourceValue = 0; var spMode = 3;
    global_alphabetMobileContactsData = "";

    alphabetMobileOrWebClick(0, spMode, "");
	e.stopPropagation();
});
$(document).on('click', '.alphabet li[prop="alpha"]', function (e) {
	e.preventDefault();
    global_addContactList = 0;
    $('#search-input').val('');
    var clickCounter = $(this).data('clickCounter') || 0;
    if (clickCounter == 0) {
        clickCounter += 1;
        alphabetPageIndex = 0;
        $(this).data('clickCounter', 0);
        global_alphabetMobileContactsData = "";
    }
    

    var alphabet = $(this).text().toLowerCase();
global_alphabet= alphabet;
    alphabetMobileOrWebClick(alphabetPageIndex, 2, alphabet);
	e.stopPropagation();
});

function alphabetMobileOrWebClick(alphabetPageIndex, spMode, alphabet) {

    //$('#search-input').css("border-color","none");

    var sourceValue = 0;


    sourceValue = 1;
    $('#phoneContacts').hide();
    $('#alphabetMobileContacts').show();
    $('#alphabetWebContacts').hide();
    $('#phoneContacts').parent().hide();
    $('#alphabetMobileContacts').parent().show();
    if (alphabetMobileClick == 0) {

        $('#alphabetMobileContacts').slimScroll({
            allowPageScroll: false,
            height: '450'
        });
    }
    alphabetMobileClick++;
    listId = 0;

    alphabeticalContactSorting(sourceValue, alphabetPageIndex, alphabet, 0, spMode);
}


function alphabeticalContactSorting(sourceValue, alphabetPageIndex, alphabet, listId, modeSp) {
    alphabetPageIndex++;
    if (alphabetPageIndex == 1 || alphabetPageIndex <= alphabetPageCount) {
		global_alphabetpageIndex = alphabetPageIndex;
        if(alphabetPageIndex==1)
        {
            alphabetCOntactsCount = 0;
            sortSubContactsResponse = new Array();
        }
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 9,
                source: sourceValue,
                alphabetPageIndex: alphabetPageIndex,
                modeSp: modeSp,
                alphabetSort: alphabet,
                listID: listId,
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (contactResult) {
                alphabetPageCount = contactResult.pageCount;
                if (contactResult.Success == true) {
                    jsonResponse = contactResult;
                    if (contactResult.contactDetails.length != 0) {

                        for (var contactRows = 0; contactRows < contactResult.contactDetails.length ; contactRows++) {


                            if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                                global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5">';
                                if (contactResult.contactDetails[contactRows].imagePath == null) {

                                    global_alphabetMobileContactsData += '<div id="profilePic" ><img  src="images/DefaultUser.png" alt="Profile Pic"></div>';
                                }

                                else if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '') {
                                    global_alphabetMobileContactsData += '<div id="profilePic" ><img src="/' + contactResult.contactDetails[contactRows].imagePath + '" alt="Profile Pic"></div>';
                                }
                                else {
                                    global_alphabetMobileContactsData += '<div id="profilePic" ><img  src="images/DefaultUser.png" alt="Profile Pic"></div>';
                                }
                                global_alphabetMobileContactsData += '<div id="profileDetails">';
                                if (contactResult.contactDetails[contactRows].name.length > 17) { global_alphabetMobileContactsData += '<p title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 14) + '...</p>'; }
                                else
                                {
                                    global_alphabetMobileContactsData += '<p>' + contactResult.contactDetails[contactRows].name + '</p>';
                                }
                                if ($.trim(contactResult.contactDetails[contactRows].subContactDeatils).length != 0) {
                                    sortSubContactsResponse.push(contactResult.contactDetails[contactRows].subContactDeatils);
                                    global_alphabetMobileContactsData += '<p id="subPhoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p><div class="dropdown" style="float:right;margin: -3px 0 0;"><a id="alphabtnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="dropdown-toggle" style="padding: 2px 3px;" data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="subMobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" ></ul></div>';

                                }
                                else {
                                    global_alphabetMobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                                }
                                global_alphabetMobileContactsData += '</div></div>';

                            }

                        }
                        
                        $("span[id=searchCount]").text(contactResult.ContactsCount + " Contacts Found");
                        $('#alphabetMobileContacts').show();
                        $('#alphabetMobileContacts').addClass("active in");
                        $('#alphabetMobileContacts').html(global_alphabetMobileContactsData);
                    }



                }




                if (contactResult.Success == false) {
                    if (contactResult.ContactsCount == 0)
                        var text = '<br>';
                    { $("span[id=searchCount]").html('No Contacts Found'); }
                    global_alphabetMobileContactsData = "";
                    // global_alphabetMobileContactsData = "No Contacts Found ";


                    $('#alphabetMobileContacts').show();
                    $('#alphabetMobileContacts').addClass("active in");
                    $('#alphabetMobileContacts').html(global_alphabetMobileContactsData);



                }
            },
            error: function (contactResult) {
            }


        });
    }
}




$(document).click(function () {
    $('#search').css("border-color", "none");
});