var jsonResponse = new Array();
var mobileDropDownlist = "";
var global_mobileNumber = "";
var subContactsResponse = new Array();
var sortSubContactsResponse = new Array();
var pageIndex = 1;
var mobileContactsData = "";
var pageCount = 0;
var alphabetPageIndex = 0;
var global_alphabetMobileContactsData = "";
var global_searchndex = 0;
var global_addContactList = 0;
var editListId = 0; var editlistName = "";
var alphabetMobileClick = 0; alphabetWebClick = 0, mobileClick = 0, tabClick = 0;
var listContactsCount = 0;
var enterClick = false;
var mobheight, webHeight = 0;
var alphabetContactCount = 0;
var modalPageIndex = 1;
var isModalMobile = 0;
var IsFirstForModal = 0;
var creategropTalk = 0;
$(document).ready(function () {
    mobileContactsData = "";
    getMobileContacts(1, pageIndex);
    $('#phoneContacts').hide();
    $('#webLists').show();
    $('#searchCount').html('<br>');
    modalPageIndex = 1;
    // isModalMobile = 1;
    // getMobileContacts(1,modalPageIndex);
    $("ul#contactsTab li").click(function () {

        //$("ul#contactsTab li").removeClass("active");
        if (!$(this).hasClass("search")) {
            if (alphabetMobileClick > 0)
            { $('#alphabetMobileContacts').parent().hide(); }
            if (alphabetWebClick > 0)
            { $('#alphabetWebContacts').parent().hide(); }
        }
        var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
        if (activeTab == "#webLists") {
            $('#webLists,#list-group').parent().show();
            $('#createGrpList,#webContactList,#webLists').show();
            $('#phoneContactsDisplay,#phoneContactsText, #phoneContacts').hide();
            if (tabClick != 0) {
                $('#phoneContacts').parent().hide();
            }

            //$('#webContacts').html(webContactsAppend);
            $('#phoneContactsList').hide();
            if ($('#webLists .contacts').length != 0)
            { $('.alphabet').show(); }
            else { $('.alphabet').hide(); }

            $('#contactList').show();
            $('.addNewContact,#plusSymbol').show();
            $('.list').show();
            $('#listTitleName').text("Add Contacts");
            $('#search-input').val('');
            $('span[id=searchCount]').html('<br>');
            $('#alphabetWebContacts').hide();
            $('#alphabetMobileContacts').hide();
            global_searchndex = 0;
            $('.phnCntcts').hide();
        }
        if (activeTab == "#phoneContacts") {
            $('.phnCntcts,#phoneContactsText').show();
            $('.alphabet').show();
            $('#phoneContacts').parent().show(); $('#phoneContacts').show();
            $('#webLists,#list-group').parent().hide();
            $('#phoneContactsDisplay,#phoneContactsText,#phoneContactsList').show();
            $('#createGrpList, #webContactList').hide();
            $('#contactList').hide();
            $('.addNewContact,#plusSymbol').hide();
            $('.list').hide();
            $('#search-input').val('');
            $('span[id=searchCount]').html('<br>');
            $('#alphabetWebContacts').hide();
            $('#alphabetMobileContacts').hide();
            global_searchndex = 0;
            $('#listTitleName').text("Phone Contacts");
            if (mobileClick == 0) {
                $('#phoneContacts').slimScroll({
                    allowPageScroll: false,
                    height: '450'
                });
            }
            mobileClick++;
            tabClick++;

        }

    });

    $("ul#myWebTab li").click(function () {
        $("ul#myWebTab li").removeClass("active");
        var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
        if (activeTab == "#addListContacts") {
            $('#addListContacts').show();
            $('#allListContacts').hide();
            $('#saveListContact').hide()
            $('#saveContact').show();
            $('#updateContact').hide();
            $('#excelFormBody').hide();
            $('#saveExcelContacts').hide();
            //$('#saveExcelContacts').hide();
        }
        else if (activeTab == "#allListContacts") {
            $('#addListContacts').hide();
            $('#allListContacts').show();
            $('#saveListContact').show()
            $('#saveContact').hide();
            $('#updateContact').hide();
            $('#excelFormBody').hide();
            $('#saveExcelContacts').hide();
        }
        else if (activeTab == "#excelFormBody") {

            $('#addListContacts').hide();
            $('#allListContacts').hide();
            $('#saveListContact').hide()
            $('#saveContact').hide();
            $('#updateContact').hide();
            $('#excelFormBody').show();
            $('#saveExcelContacts').show();
        }
    });

    $(document).on("click", "input[identity='editLisItemInput']", function (e)
    { e.stopPropagation(); });


    $(document).on('click', '.ddlList', function () {
        var classList = $(this).attr('class');
        classList = (classList.substring(13, classList.length))
        $(this).parents('.dropdown').prev().html('<i class="fa fa-mobile" aria-hidden="true"></i> ' + $(this).children().text());
    });

    $(document).on("click", "div ul.dropdown-menu li a", function () {
        var anchorValue = $(this).text();
        $('#phoneNumber' + global_mobileNumber).text(anchorValue);
    });




});

$('#webLists').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
        webPageInde++;
        webContactsOffsetScroll(0, webPageInde, webPageCount, webContactsData);
        $('#webLists').html(webContactsData);
        webHeight = 450 / webPageInde;
        $(this).css("top", webHeight);

    }
});

$('#phoneContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
        setTimeout(function () { getMobileContacts(1, ++pageIndex) }, 200);
        isModalMobile = 0;
        mobheight = 450 / pageIndex;
        $(this).css("top", mobheight);
    }
});


$('#alphabetMobileContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
        setTimeout(function () { alphabeticalContactSorting(1, ++alphabetPageIndex, $('#search-input').val(), 0, 2) }, 200);
        mobheight = 450 / pageIndex;
        $(this).css("top", mobheight);
    }
});

$('#mobileContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
        setTimeout(function () { getMobileContacts(1, ++modalPageIndex) }, 200);
        isModalMobile = 1;
        mobheight = 450 / pageIndex;
        $(this).css("top", mobheight);
    }
});

$('#closeList').click(function (e) {
    $('.close').click();
});

function getMobileContacts(src, mpageIndex) {
    global_addContactList = 0;

    if (mpageIndex == 1 || mpageIndex <= pageCount) {
        if (mpageIndex == 1 && isModalMobile == 0) {
            $.blockUI({ message: '<h4> Loading...</h4>' })
        }
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 1,
                source: src,
                pageIndex: mpageIndex,
                modeSp: 1,
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (contactResult) {
                pageCount = contactResult.pageCount;
                mobileContactsData = "";
                if (contactResult.Success == true) {
                    jsonResponse = contactResult;
                    if (contactResult.contactDetails.length != 0) {
                        for (var contactRows = 0; contactRows < contactResult.contactDetails.length ; contactRows++) {
                            if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                                mobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5" id=' + contactResult.contactDetails[contactRows].id + '>';
                                if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '') {
                                    mobileContactsData += '<div id="profilePic" ><img src="/' + contactResult.contactDetails[contactRows].imagePath + '" alt="Profile Pic"></div>';
                                }
                                else {
                                    mobileContactsData += '<div id="profilePic" ><img  src="images/avatar-img-5.jpg" alt="Profile Pic"></div>';
                                }

                                mobileContactsData += '<div id="profileDetails">';
                                if (contactResult.contactDetails[contactRows].name.length > 25) {
                                    mobileContactsData += '<p title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '...</p>';
                                }
                                else { mobileContactsData += '<p>' + contactResult.contactDetails[contactRows].name + '</p>'; }
                                if ($.trim(contactResult.contactDetails[contactRows].subContactDeatils).length != 0) {
                                    sortSubContactsResponse.push(contactResult.contactDetails[contactRows].subContactDeatils);
                                    mobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '<div class="dropdown" style="float:right;margin: -3px 0 0;"><a id="btnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" >';
                                    mobileContactsData += "<li class='ddlList'><a id=primary-" + contactResult.contactDetails[contactRows].mobileNumber + " >" + contactResult.contactDetails[contactRows].mobileNumber + "</a></li>";
                                    for (var sub = 0; sub < contactResult.contactDetails[contactRows].subContactDeatils.length; sub++) {
                                        var anchorId = contactResult.contactDetails[contactRows].subContactDeatils[sub].contactType + '-' + contactResult.contactDetails[contactRows].subContactDeatils[sub].contactNumber;
                                        mobileContactsData += "<li class='ddlList'><a id=" + anchorId + ">" + contactResult.contactDetails[contactRows].subContactDeatils[sub].contactNumber + "</a></li>";
                                    }
                                    mobileContactsData += '</ul></div>';

                                }
                                else {
                                    mobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                                }
                                mobileContactsData += '</div>';
                                mobileContactsData += '<i class="fa fa-check select_check" style="display:none"></i></div>';
                            }
                        }
                        if (isModalMobile == 1) {
                            $('#mobileContacts').append(mobileContactsData);
                        }
                        else {
                            $('#phoneContacts').append(mobileContactsData);
                            if (IsFirstForModal == 0)
                            { $('#mobileContacts').append(mobileContactsData); }
                            IsFirstForModal++;
                        }

                        if ($('#alphabetMobileContacts').parent('.slimScrollDiv').length != 0) {
                            $('#alphabetMobileContacts').parent().hide();
                            $('#alphabetMobileContacts').hide();
                            $('#phoneContacts').parent().show(); $('#phoneContacts').show();
                        }
                    }
                    setTimeout($.unblockUI, 200);
                }
                if (contactResult.Success == false)
                { }
            },
            error: function (contactResult) {
                $.unblockUI();
            }


        });
    }
}
$("#search-input").click(function (e) {
    $('#search').css("border-color", "#66afe9");
    if ($.trim($('#search-input').val()).length != 0) {
        var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
        var sourceValue = 0;
        if (activeTab == "#webLists") {
            sourceValue = 2;
            $('#alphabetWebContacts').show();
            $('#alphabetWebContacts').parent().show();
        }
        if (activeTab == "#phoneContacts") {
            $('#alphabetMobileContacts').parent().show();
            $('#alphabetMobileContacts').show();
        }
    }
    e.stopPropagation();
});

$("#search-input").keydown(function (e) {
    console.log($("#search-input").val())

    if (e.which == 13) {
        e.preventDefault();
        $('#search').click();
        $(this).blur();
    }
    if (e.which == 8) {
        if ($(this).val().length == 1) {
            $(this).val('');
            var activeTab = $("ul#contactsTab li.active").find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
            if (activeTab == "#webLists") {
                $('#createGrpList,.list').removeClass("highlight");
                $('a[id=' + listId + ']').parents('.list').addClass("highlight");
                listClickContactsDisplay(listId);
                $('span[id=searchCount]').html('<br>');
            }
            else if (activeTab == "#phoneContacts") {
                $('span[id=searchCount]').html('<br>');
                pageIndex = 1;
                mobileContactsData = "";
                $('#phoneContacts').html('');
                getMobileContacts(1, pageIndex);
            }
        }

    }

    e.stopPropagation();
});

$('#search-input').keyup(function (e) {
    e.preventDefault();
    if (e.which != 1 && e.which != 17 && e.which != 20 && e.which != 16) {
        if ($(this).val().length >= 3) {
            $('#search').click();
        }
        else {
            if ($(this).val().length == 0 && e.which != 13) {
                $('#search').click();
            }
        }
    }
    e.stopPropagation();
});

$(document).on('click', '#search', function (e) {
    global_searchndex = 0;

    var alphabet = $('#search-input').val();
    if (alphabet.length != 0) {
        alphabet = alphabet.toLowerCase();
    }
    else {
        var activeTab = $("ul#contactsTab li.active").find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
        if (activeTab == "#webLists") {
            $('#createGrpList,.list').removeClass("highlight");
            // if($('.list.highlight').length==0)
            // {listClickContactsDisplay(0);}
            // else{
            $('a[id=' + listId + ']').parents('.list').addClass("highlight");
            listClickContactsDisplay(listId);
            //}
            $('span[id=searchCount]').html('<br>');

        }
        else if (activeTab == "#phoneContacts") {
            $('span[id=searchCount]').html('<br>');
            pageIndex = 1;
            mobileContactsData = "";
            $('#phoneContacts').html('');
            getMobileContacts(1, pageIndex);
        }
        return;

    }
    global_addContactList = 0;
    global_searchndex++;
    global_alphabetMobileContactsData = "";

    var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
    var sourceValue = 0;
    alphabetMobileOrWebClick(global_searchndex, 2, alphabet);
    e.stopPropagation();
});

$('#specialCharSearch').click(function () {
    var clickCounter = $(this).data('clickCounter') || 0;
    var alphabet = $(this).text().toLowerCase();
    var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
    var sourceValue = 0; var spMode = 3;
    global_alphabetMobileContactsData = "";
    alphabetMobileOrWebClick(1, spMode, "");
});

$(document).on('click', '.alphabet li[prop="alpha"]', function () {
    global_addContactList = 0;
    $('#search-input').val('');
    var clickCounter = $(this).data('clickCounter') || 0;
    if (clickCounter == 0) {
        clickCounter += 1;
        alphabetPageIndex = 0;
        $(this).data('clickCounter', 0);
        global_alphabetMobileContactsData = "";
    }
    alphabetPageIndex++;

    var alphabet = $(this).text().toLowerCase();

    alphabetMobileOrWebClick(alphabetPageIndex, 2, alphabet);
});

function alphabetMobileOrWebClick(alphabetPageIndex, spMode, alphabet) {

    //$('#search-input').css("border-color","none");
    var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
    var sourceValue = 0;
    if (activeTab == "#webLists") {
        sourceValue = 2;
        $('#webLists').hide();
        $('#alphabetWebContacts').show();
        $('#alphabetMobileContacts').hide();
        $('#webLists').parent().hide();
        $('#alphabetWebContacts').parent().show();
        if (alphabetWebClick == 0) {
            $('#alphabetWebContacts').slimScroll({
                allowPageScroll: false,
                height: '450'
            });
        }
        alphabetWebClick++;
        listId = $('.list.highlight').attr("id");
        if ($('.list.highlight').length == 0) {
            $('#alphabetWebContacts').addClass("active in");
            $('#alphabetWebContacts').show();
            $('#alphabetWebContacts').html('');
            $('#alphabetMobileContacts').removeClass("active in");
            $('#searchCount').text('No Contacts are found');
            return;
        }

    }
    if (activeTab == "#phoneContacts") {
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
    }
    alphabeticalContactSorting(sourceValue, alphabetPageIndex, alphabet, listId, spMode);
}

function alphabeticalContactSorting(sourceValue, alphabetPageIndex, alphabet, listId, modeSp) {
    if (alphabetPageIndex == 1 || alphabetPageIndex <= alphabetContactCount) {
        sortSubContactsResponse = new Array();
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
                if (contactResult.Success == true) {
                    jsonResponse = contactResult;
                    alphabetContactCount = contactResult.pageCount;
                    if (contactResult.contactDetails.length != 0) {

                        for (var contactRows = 0; contactRows < contactResult.contactDetails.length ; contactRows++) {


                            if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                                global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5">';
                                if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '') {
                                    global_alphabetMobileContactsData += '<div id="profilePic" ><img src="/' + contactResult.contactDetails[contactRows].imagePath + '" alt="Profile Pic"></div>';
                                }

                                else {
                                    global_alphabetMobileContactsData += '<div id="profilePic" ><img  src="images/avatar-img-5.jpg" alt="Profile Pic"></div>';
                                }
                                global_alphabetMobileContactsData += '<div id="profileDetails">';
                                if (contactResult.contactDetails[contactRows].name.length > 25) { global_alphabetMobileContactsData += '<p>' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '...</p>'; }
                                else
                                {
                                    global_alphabetMobileContactsData += '<p>' + contactResult.contactDetails[contactRows].name + '</p>';
                                }
                                if ($.trim(contactResult.contactDetails[contactRows].subContactDeatils).length != 0) {
                                    sortSubContactsResponse.push(contactResult.contactDetails[contactRows].subContactDeatils);
                                    // global_alphabetMobileContactsData += '<p id="subPhoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p><div class="dropdown" style="float:right;margin: -3px 0 0;"><button id="alphabtnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="btn dropdown-toggle" style="padding: 2px 3px;" type="button" data-toggle="dropdown"><span class="caret"></span></button> <ul class="dropdown-menu" id="subMobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" ></ul></div>';
                                    global_alphabetMobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p><div class="dropdown" style="float:right;margin: -3px 0 0;"><a id="btnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="dropdown-toggle" data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" >';
                                    global_alphabetMobileContactsData += "<li class='ddlList'><a id=primary-" + contactResult.contactDetails[contactRows].mobileNumber + " >" + contactResult.contactDetails[contactRows].mobileNumber + "</a></li>";
                                    for (var sub = 0; sub < contactResult.contactDetails[contactRows].subContactDeatils.length; sub++) {
                                        var anchorId = contactResult.contactDetails[contactRows].subContactDeatils[sub].contactType + '-' + contactResult.contactDetails[contactRows].subContactDeatils[sub].contactNumber;
                                        global_alphabetMobileContactsData += "<li class='ddlList'><a id=" + anchorId + ">" + contactResult.contactDetails[contactRows].subContactDeatils[sub].contactNumber + "</a></li>";
                                    }
                                    global_alphabetMobileContactsData += '</ul></div>';
                                }
                                else {
                                    global_alphabetMobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                                }
                                if (listId != 0) {
                                    global_alphabetMobileContactsData += '<div class="contact_actions"><a href="javascript:void(0);" data-toggle="modal" data-target="#addWebListContacts"><a href="javascript:void(0);" class="delete" id="' + contactResult.contactDetails[contactRows].id + '"><i class="fa fa-trash-o"></i></a></div></div>';
                                }
                                global_alphabetMobileContactsData += '</div>';

                            }
                            else {
                                var imgPath = "";
                                if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '') {
                                    // path = result.Items[i].Imagepath.split('\\ContactImages\\');
                                    imgPath = contactResult.contactDetails[contactRows].imagePath
                                }
                                else {
                                    imgPath = 'images/avatar-img-5.jpg';
                                }
                                if (contactResult.contactDetails[contactRows].name.length > 25) {
                                    global_alphabetMobileContactsData += '<div class="contacts margin-bottom-5 margin-right-5" title="' + contactResult.contactDetails[contactRows].name + '" idNo="' + contactResult.contactDetails[contactRows].id + '" id="' + contactResult.contactDetails[contactRows].id + '" class="edit"  userName="' + contactResult.contactDetails[contactRows].name + '">';
                                }
                                else { global_alphabetMobileContactsData += '<div class="contacts margin-bottom-5 margin-right-5" idNo="' + contactResult.contactDetails[contactRows].id + '" id="' + contactResult.contactDetails[contactRows].id + '" class="edit"  userName="' + contactResult.contactDetails[contactRows].name + '">'; }
                                global_alphabetMobileContactsData += '<div id="profilePic">';
                                global_alphabetMobileContactsData += '<img alt="user" src="' + imgPath + '"></div>';
                                global_alphabetMobileContactsData += '<div id="profileDetails">';
                                if (contactResult.contactDetails[contactRows].name.length > 25)
                                { global_alphabetMobileContactsData += '<p title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '..</p>'; }
                                else { global_alphabetMobileContactsData += '<p>' + contactResult.contactDetails[contactRows].name + '</p>'; }

                                global_alphabetMobileContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                                global_alphabetMobileContactsData += '</div>';
                                global_alphabetMobileContactsData += '<div class="contact_actions"><a href="javascript:void(0);" data-toggle="modal" data-target="#addWebListContacts"><i class="fa fa-edit" id="' + contactResult.contactDetails[contactRows].id + '"  userName="' + contactResult.contactDetails[contactRows].name + '" mobile = "' + contactResult.contactDetails[contactRows].mobileNumber + '" imgPath="' + imgPath + '"></i></a><a href="javascript:void(0);" class="delete" id="' + contactResult.contactDetails[contactRows].id + '"><i class="fa fa-trash-o"></i></a></div></div>';

                            }

                            global_alphabetMobileContactsData += '</div>';
                        }
                    }


                    $("span[id=searchCount]").text(contactResult.ContactsCount + " Contacts Found");

                    if (sourceValue == 2) {
                        $('#alphabetWebContacts').addClass("active in");
                        $('#alphabetWebContacts').show();
                        $('#alphabetWebContacts').html(global_alphabetMobileContactsData);
                        $('#alphabetMobileContacts').removeClass("active in");

                    }
                    else {
                        $('#alphabetWebContacts').removeClass("active in");
                        $('#alphabetMobileContacts').show();
                        $('#alphabetMobileContacts').addClass("active in");
                        $('#alphabetMobileContacts').html(global_alphabetMobileContactsData);
                    }


                }

                if (contactResult.Success == false) {
                    if (contactResult.ContactsCount == 0)
                        if (sourceValue == 2) {
                            { $("span[id=searchCount]").text("No Contacts are found"); }
                            $('#alphabetWebContacts').addClass("active in");
                            $('#alphabetWebContacts').show();
                            $('#alphabetWebContacts').html(global_alphabetMobileContactsData);
                            $('#alphabetMobileContacts').removeClass("active in");
                        }
                        else {
                            { $("span[id=searchCount]").text("No Contacts are found"); }
                            $('#alphabetWebContacts').removeClass("active in");
                            $('#alphabetMobileContacts').show();
                            $('#alphabetMobileContacts').addClass("active in");
                            $('#alphabetMobileContacts').html(global_alphabetMobileContactsData);
                        }


                }
            },
            error: function (contactResult) {
            }


        });
    }
}



$('#createGrpList').click(function (e) {
    e.stopPropagation();
    $('div').removeClass("open");
    $('.list').removeClass("highlight");
    $(this).addClass("highlight");
    if (global_addContactList == 0) {
        if ($("input[identity='editLisItemInput']").length != 0) {
            $("input[identity='editLisItemInput']").remove();
            $("a[id='" + editListId + "']").html('<span id="' + editListId + '" lcount="' + listContactsCount + '" lname="' + editlistName + '">' + editlistName + '</span>(<span id="count' + editListId + '">' + listContactsCount + '</span>)');
            global_addContactList = 0;
        }
        var webListAdding = "";
        webListAdding += '<div class="list" id="addItem"><a data-target="#"><input type="text" placeholder="Enter List Name" class="form-control" id="addLisItem" style="width:100%;height:27px !important;background-color:#fff !important;padding:2px 12px !important;" maxlength="25"></input></a></div>';
        $('#list-group').append(webListAdding);
        $('#addLisItem').focus();
        global_addContactList = 1;
    }
    else {
        $('#addLisItem').focus();
        return false;
    }

});

$(document).on("click", "#addLisItem", function (e) {

    e.stopPropagation();
});
//$(document).click(function (e) {
//    var keyCode = e.which;
//    //escapePress();
//    //enterClick()
//});

function enterClickForList(e) {
    var listName = $('#addLisItem').val();
    if ($.trim($('#addLisItem').length) != 0) {
        var keycode = e.which;

        if (keycode == 13) {
            if (listName.length != 0) {
                addWebListItem(listName, 3, 6, 0);
                enterClick = true;
                $("#createGrpList").removeClass("highlight");
            }
            else {

                Notifier("Please enter list Name", 2);
                return false;
            }
        }
    }

    var editListName = $("input[identity='editLisItemInput']").val();
    if ($.trim($("input[identity='editLisItemInput']")).length != 0) {
        var id = $("input[identity='editLisItemInput']").attr("unique");
        var editedListName = $('#editLisItemInput' + id).val();
        var keycode = (e.keyCode ? e.keyCode : e.which);

        if (keycode == 13) {
            if (editListName.length != 0) {
                addWebListItem(editedListName, 1, 7, id, listContactsCount);
            }
            else {
                Notifier("Please enter list Name", 2);
                return false;
            }
        }
    }
}

function escapePress() {
    if ($("#list-group #addItem").length != 0) {
        $("#list-group #addItem").remove();
        global_addContactList = 0;
    }
    if ($("input[identity='editLisItemInput']").length != 0) {
        $("input[identity='editLisItemInput']").remove();
        $("a[id='" + editListId + "']").html('<span id="' + editListId + '" lcount="' + listContactsCount + '" lname="' + editlistName + '">' + editlistName + '</span>(<span id="count' + editListId + '">' + listContactsCount + '</span>)');
        global_addContactList = 0;
    }
    listClickContactsDisplay(listId)
    $('.list#' + listId).addClass("highlight");
}

$(document).keydown(function (e) {
    if (e.which == 13)
        enterClickForList(e);
    if (e.which == 27)
        escapePress();
});


function addWebListItem(listName, mode, type, listId, count) {
    var isExisted = 0;
    $(".contactList").each(function () {
        if ($(this).children().html().toLowerCase() == listName.toLowerCase()) {
            alert("List with " + listName + " already existed");
            isExisted = 1;
            escapePress();
            return;
        }
    });
    // if($("#editLisItemInput"+listId).val()==listName && isExisted==0)
    // escapePress();
    // return;
    var appendListItem = "";
    if (isExisted == 0)
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            method: "post",
            dataType: "JSON",
            data: {
                modeSp: mode,
                type: type,
                listName: listName,
                listId: listId
            },
            async: false,
            success: function (webListResult) {
                //var response = JSON.parse(webListResult);.
                if (webListResult.Success == true) {

                    if (mode == 3) {
                        $("#list-group #addItem").remove();

                        appendListItem += '<div class="list highlight" id="' + webListResult.listIdOut + '"><div style="width:93%;float:left;"><a  id="' + webListResult.listIdOut + '"  class="contactList"><span  id="' + webListResult.listIdOut + '" lname="' + listName + '" lcount="0">' + listName + '</span>(<span id="count' + webListResult.listIdOut + '">0</span>)</a></div>';
                        appendListItem += '<div style="width:7%;float:right;text-align:center;"><a style="display: block;" href="javascript:;" data-toggle="dropdown" class=" dropdown dropdown-toggle"><i class="fa fa-ellipsis-v" aria-hidden="true"></i></a>';
                        appendListItem += '<ul class="dropdown-menu dropdown-menu-right">';
                        appendListItem += '<li class="addNewContact" listId="' + webListResult.listIdOut + '" unique="modal" ><a href="#" data-toggle="modal" data-target="#addWebListContacts">Add Contacts</a></li><li  id="' + webListResult.listIdOut + '" listname="' + listName + '" class="editListName" editlistid="' + webListResult.listIdOut + '"><a href="#">Edit</a></li><li id="' + webListResult.listIdOut + '" class="deleteListName"><a href="#">Delete</a></li> </ul></div></div>';
                        $('#list-group').append(appendListItem);
                        listClickContactsDisplay(webListResult.listIdOut);
                    }
                    else if (mode == 1) {
                        $("input[id='" + listId + "']").remove();
                        $("a[id='" + listId + "']").html('<span id="' + listId + '">' + listName + '</span>(<span id="count' + listId + '">' + count + '</span>)');
                        $('.list').removeClass('highlight');
                        listClickContactsDisplay(listId);

                    }
                    if (parseInt($('#list-group .list').length) == 1) {
                        $('#ddlWebList').html('<option value="0">Select List</option><option value="' + webListResult.listIdOut + '">' + listName + '</option>');
                        $('.contactList').click();
                    }
                    global_addContactList = 0;
                }
                // else {
                // if (mode == 3) {
                // alert("List with " + listName + " already existed");
                // return;
                // }
                // else if (mode == 1) {
                // alert("List with " + listName + " already existed");
                // return;
                // }
                // }

            },
            error: function () {
            }
        });
}


$(document).on('click', '.editListName', function (e) {
    $("#list-group #addItem").remove();
    global_addContactList = 0;

    if ($("input[identity='editLisItemInput']").length != 0) {
        $("input[identity='editLisItemInput']").remove();
        $("a[id='" + editListId + "']").html('<span id="' + editListId + '">' + editlistName + '</span>');
        global_addContactList = 0;
    }

    var id = $(this).attr("editListid");
    var listName = $("span[id='" + id + "']").text();
    editListId = id;
    listContactsCount = $("span[id='count" + editListId + "']").text();
    editlistName = listName;
    $("span[id='" + id + "']").remove();
    $("a[id='" + id + "']").html('<input type="text" placeholder="Enter List Name" class="form-control" identity="editLisItemInput" unique="' + id + '"  id="editLisItemInput' + id + '" style="width:100%;height:27px !important;background-color:#fff !important;padding:2px 12px !important;" value="' + listName + '" maxlength="25"></input>');
    var inputValue = $("input[identity='editLisItemInput']");
    inputValue.focus();
    var inputLength = inputValue.val().length;
    inputValue[0].setSelectionRange(inputLength, inputLength);
    $(this).parents().removeClass("open");
    e.stopPropagation();

});



$(document).click(function () {
    $('#search').css("border-color", "none");
    //editLisItemInput265
    // if ($.trim($('#addLisItem').val()).length == 0) 
    // alert('Please Enter List Name');
    if ($.trim($('#addLisItem').val()).length != 0) {
        var listName = $('#addLisItem').val();

        enterClick = false;
        // alert('Enter Is Pressed');
        if (enterClick == false) {
            addWebListItem(listName, 3, 6, 0, 0);
        }
    }
    else {
        if ($("#list-group #addItem").length != 0) {
            $("#list-group #addItem").remove();
            global_addContactList = 0;
            if (listId == 0)
            { $('#getAllContacts').click(); }
            else {
                $('#createGrpList,.list').removeClass("highlight");
                $('a[id=' + listId + ']').parents('.list').addClass("highlight");
                listClickContactsDisplay(listId);
            }
        }
    }

    $('#createGrpList').removeClass("highlight");

    if ($.trim($("input[identity='editLisItemInput']").val()).length != 0) {
        var editListName = $("input[identity='editLisItemInput']").val();
        var id = $("input[identity='editLisItemInput']").attr("unique");
        var editedListName = $('#editLisItemInput' + id).val();

        if (editedListName.length == 0) {
            alert('Please Enter List Name');
            return false;
        }
        addWebListItem(editedListName, 1, 7, id, listContactsCount);

    }
});

$(document).on('click', '#mobileContacts .contacts', function (e) {
    if (!$(this).hasClass('selected')) {
        $(this).addClass('selected');
        $(this).find('.fa-check').show();
    }
    else {
        $(this).removeClass('selected');
        $(this).find('.fa-check').hide();
    }
})