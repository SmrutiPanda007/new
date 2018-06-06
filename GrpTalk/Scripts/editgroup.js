/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
var mobileContactsData = "", imgPath = "", defaultLines = 0;
var mobileContactsAppend = "", scheduleClick = 0;
var mobileDropDownlist = "";
var mobileNumberVal = "", nameSplit = [];
var subContactsResponse = new Array();
var webPageIndex = 0, searchValue = "", alphabetSort = "", pageIndex = 0;
var subContactsResponse = new Array();
var pageCount = 0, contactId = 0, countContacts = 0, grpTalkName = "";
var webPageCount = 0, global_alphabetMobileContactsData = "";
var selectedContacts = "", currentTime = "", currentDate = "", response = "";
var jsonContacts = new Array(), participants = new Array();
var alphabetPageIndex = 0;
var global_searchndex = 0;
var grpTalkEditArray = JSON.parse(localStorage.myArray);
var grpTalkEditJson = $.parseJSON(grpTalkEditArray);
var particpantsMobileArray = new Array();
var partcipants = "";
var selectedContactsCount = 0, selectBtnText = "", selectedBtnList = "", selectedBtnTextAll = "";
var selectedContactsArray = "", selectedContactsNumberArray = "";;
var global_listId = "", plugoutput = "", mdate = "", selectAllListId = 0, firstClick = 0, alphabetClick = 0;
var time = new Array();
var minDate = "", faCheckPic = "", selectedClass = "", mobileScroll = 0, webScroll = 0;
var editSchType = "", editWeekDays = "", selectedTabClick = 0;
var SelectedContactsTabArray = new Array();
var selectedMobileNos = new Array();
var nameArray = new Array();
var sliceNumber = 0, alphabetPageCount = 0;
var global_alphabet = '';
var global_alphabetpageIndex = 0;
var memberImageArray = new Array();
var sysTimeZone = -(new Date().getTimezoneOffset());
var userTimeZone = $('#hdnOffSet').val();
var confTimeZone = 0;

$(document).ready(function () {
    sliceNumberFn();
    var grpTalkResultJson = "";
    grpTalkResultJson = getConferenceDetails(grpTalkEditJson.Groups[0].GroupID);
    partcipants = grpTalkResultJson.Groups[0].GrpParticipants;
    editSchType = grpTalkResultJson.Groups[0].SchType;
    editWeekDays = grpTalkResultJson.Groups[0].WeekDays;
    confTimeZone = grpTalkResultJson.Groups[0].ConfTimeZone;
    
    var contId = 0;
   
    if (grpTalkResultJson.Groups[0].SchduledTime != "") {
        time[0] = grpTalkResultJson.Groups[0].SchduledTime;
    }
    var tdate = new Date($.trim(grpTalkResultJson.Groups[0].SchduledDate + " " + time[0]))
    tdate.setMinutes(tdate.getMinutes() + (-parseInt(confTimeZone) + parseInt(userTimeZone)));
    mdate = tdate.getDate() + '-' + (tdate.getMonth() + 1) + '-' + tdate.getFullYear() + ' ' + tdate.getHours() + ':' + addZero(tdate.getMinutes()) + ':00';

    // For Default selection of Grouip id contatcs in selected tab
    for (var j = 0; j < partcipants.length ; j++) {
        var name = ""; var mobileNo = "", listIDEdit = 0, contactIdEdit = 0;
        name = partcipants[j].Name;
        mobileNo = partcipants[j].MobileNumber;
        
        contactIdEdit = partcipants[j].ContactId;
        
        //nameSplit = (name).substring(0, 2).toUpperCase();
        selectedContacts += '<div class="contacts margin-right-5 margin-bottom-5 unselected" id="' + contId + '" listId="0">';
        //selectedContacts += '<div id="profilePic"><span style=" background: #add9ec none repeat scroll 0 0;border-radius: 50%;color: #428bca;font-weight: bold;height: 34px;padding: 9px;width: 32px;">' + nameSplit + '</span></div>';
        selectedContacts += '<div id="profilePic"><img alt="default user" src="images/avatar-img-5.jpg"></div>';
        selectedContacts += '<div listId="0" id="profileDetails">';
        if (name.length > 17) {
            selectedContacts += '<p name="' + name + '" title="' + name + '">' + name.substring(0, 14) + '...</p>';
        }
        else { selectedContacts += '<p name="' + name + '">' + name + '</p>'; }
        //selectedContacts += '<p><i class="fa fa-mobile" aria-hidden="true"></i>' + mobileNo + '<i class="fa fa-caret-down" aria-hidden="true"></i></p>';
        selectedContacts += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNo + '</p>';
        selectedContacts += '</div>';
        selectedContacts += '<i class="fa fa-check select_check" style="display:block"></i></div>';
        selectedContactsArray += contId + ",";
        global_listId += 0 + ",";
        selectedContactsNumberArray += mobileNo + ",";
        selectedMobileNos.push(mobileNo.replace(/\D/g, "").slice(sliceNumber));
        nameArray.push(name)
        memberImageArray.push('images/avatar-img-5.jpg');
        contId--;

    }
    $('#selectedContacts').html(selectedContacts);
    selectedContactsCount = partcipants.length ;
    $('.count').html('(' + selectedContactsCount + ')')
    if (selectedContactsCount == 0) {
        $('a[href="#selectedContacts"]').hide();
        $('a[href="#selectedContacts"]').parent('li').removeClass('active');
        $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
        MobileTabClick();
    }
    else { $('a[href="#selectedContacts"]').show(); }



    if (grpTalkResultJson.Groups.length != 0) {
        $('#conf_name').val(grpTalkResultJson.Groups[0].GroupCallName);
        $('#repeat').val(editWeekDays);
        datetimetotaltext = mdate.split(' ')
        datetotaltext = datetimetotaltext[0].split('-')
        timetotaltext = datetimetotaltext[1].split(':')
        selectdatemonth = datetotaltext[1] - 1
        outtimetext = formatDate(timetotaltext[0], timetotaltext[1]);
        outdatetext = montharr[selectdatemonth] + ' ' + datetotaltext[0] + ', ' + datetotaltext[2] + ' at ' + outtimetext
        $('#conf_datetime').val(outdatetext);
        $('#grpId').val(grpTalkResultJson.Groups[0].GroupID);
    }
    $('.phnCntcts,#phoneContactsDisplay').show();
    getMobileContacts(1);


});


$('#repeat').click(function (e) {
    e.preventDefault();
    editWeekDays = $('#repeat').val();
    $('.weekDay').removeClass("active");
    var weekDays = new Array();
    weekDays = editWeekDays.split(",");

    $('.weekDay').each(function () {
        for (var i = 0; i < weekDays.length; i++) {
            if (weekDays[i].toLowerCase() == $(this).val().toLowerCase()) {
                $(this).addClass("active");
            }
        }
    });

    $('#repeatCall').modal("show");

});

$('.weekDay').on('click', function () {
    if ($(this).hasClass("active")) {
        $(this).removeClass('active');
    }
    else {
        $(this).addClass('active');
    }
});



$('#setRepeat').on('click', function () {
    var global_day = "";
    $('.weekDay').each(function () {
        if ($(this).hasClass("active")) {
            global_day += $(this).val() + ",";
        }
    });

    $('#repeat').val(global_day.substring(0, global_day.length - 1));
    editWeekDays = $('#repeat').val();
});


//For knowing which tab has been clickd
$("ul#myTabList li").click(function () {
    $("ul#myTabList li").removeClass("active");

    var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
    $(this).addClass("active");
    $('#selectAll').hide();
    $('#selectedContacts').hide();
    if ($('#search-input').val().length != 0 || alphabetClick > 0) {
        $('#search-input').val('');
        $('#grpCallMobileContacts').html('');
        $('#grpCallMobileContacts').slimScroll({ destroy: true });
        $('#grpCallMobileContacts').slimScroll({
            allowPageScroll: false,
            height: '380'
        });
        selectedContactsTabClick("");
        pageIndex = 0;
        mobileContactsData = "";
        getMobileContacts(1);
        $('#search-input').val('');
        alphabetClick = 0;
        if (nameArray.length == 0)
        { $('#selectedContacts').html(''); }
        selectedContactsTabClick('');


    }

    if (activeTab == "#grpCallMobileContacts") {
        $('#listTitleName').text('Select Contacts')
        $('#grpCallMobileContacts,.alphabet').show();

        $('#grpCallMobileContacts').parent().show();

        if ($('#selectedContacts').parent('.slimScrollDiv').length != 0)
        { $('#selectedContacts').parent().hide(); }
        $('grpCallMobileContacts.slimScrollDiv').show();
        $('#grpCallMobileContacts').addClass("active in");
        $('#selectedContacts').removeClass("active in");
        //$('#mobileContacts').html(mobileContactsAppend);
    }
    else if (activeTab == "#selectedContacts") {
        console.log(activeTab);
        $('#grpCallMobileContacts,.alphabet').hide();
        if ($('#grpCallMobileContacts').parent('.slimScrollDiv').length != 0)
        { $('#grpCallMobileContacts').parent().hide(); }
        $('#selectedContacts').parent().show();
        $('#selectedContacts').show();


        $('#listTitleName').text('Select Contacts')
        $('.list').hide();
        $('#selectedContacts').addClass("active in");

        $('#grpCallMobileContacts').removeClass("active in");
        if (selectedTabClick == 0) {
            $('#selectedContacts').slimScroll({
                allowPageScroll: false,
                height: '319',
            });
        }
        selectedTabClick++;
    }


});
//$('#mobileNumber').keydown(function (e) {

//        var key = e.which;
//        if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
//            e.preventDefault();
//            return false;

//        }

//});

//$(document).on("click", "#btnDropdown", function (e) {
//    var mobileNumber = $(this).attr("mobile");

//    var contactId = $(this).attr("contactid");
//    mobileNumberVal = mobileNumber;
//    var select = $("#mobileNumber" + mobileNumber);
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
//    e.stopPropagation();
//}); // dropdown click event for mobile contacts

//$(document).on("click", "div ul.dropdown-menu li a", function (e) {
//    var anchorValue = $(this).text();
//    $('#phoneNumber' + mobileNumberVal).html('<i class="fa fa-mobile" aria-hidden="true"></i> '+anchorValue);
//    if ($('.dropdown').hasClass('open')) {
//        $('.dropdown').removeClass('open')
//    }
//    e.stopPropagation();
//});


$(document).on('click', '.ddlList', function () {
    var contactId2 = '';
    if ($(this).parents('.contacts').hasClass('selected')) {
        contactId2 = $(this).parents('.contacts').attr('id');
        $('#' + contactId2).removeClass('selected');       
    }
    else
        $('#' + contactId2).addClass('selected');
    var classList = $(this).attr('class');
    classList = (classList.substring(13, classList.length))
    $(this).parents('.dropdown').prev().html('<i class="fa fa-mobile" aria-hidden="true"></i> ' + $(this).children().text());
});



$(document).on("click", ".unselected", function () {
    var contactId = $(this).attr("id");
    var listId = $(this).attr("listid");
    selectedContacts = "";
    var mobileNo = $(this).find("p:eq(1)").text();
    var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
    selectedMobileNos.splice(index, 1);
    nameArray.splice(index, 1)
    memberImageArray.splice(index, 1);

    selectedContacts = "";
    if ($(this).hasClass("unselected")) {
        $(this).remove();
        selectedContactsCount = selectedContactsCount - 1;
        $('.count').html('(' + selectedContactsCount + ')');
    }
    $('.contacts').each(function () {
        var selectedDivMobileNo = $(this).find("p:eq(1)").text();
        if ($.trim(selectedDivMobileNo.slice(sliceNumber)) == $.trim(mobileNo.slice(sliceNumber))) {
            $(this).removeClass("selected");
            $(this).find('.fa-check').css('display', 'none');
        }
    })

    if (selectedContactsCount == 0) {
        $('a[href="#selectedContacts"]').hide();
        $('a[href="#selectedContacts"]').parent('li').removeClass('active');
        $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
        MobileTabClick();
    }
    else { $('a[href="#selectedContacts"]').show(); }



});



// For Special Character Search
$(document).on('click', '#specialCharSearch', function (e) {
    $('#search').css("border-color", "none");
    var clickCounter = $(this).data('clickCounter') || 0;
    var alphabet = $(this).text().toLowerCase(); alphabetClick++;
    var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
    var sourceValue = 0;
    global_alphabetMobileContactsData = "";
    var spMode = 3;


    sourceValue = 1;
    subContactsResponse = new Array();
    alphabetPageIndex = 0;
    alphabeticalContactSorting(sourceValue, 0, "", 0, spMode);
    e.stopPropagation();

});

// MobileContacts OffSet Scrrol Function
$('#grpCallMobileContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 1) {
        if (global_alphabetpageIndex == 0) {
            getMobileContacts(1);
            var height = $(this).scrollTop();
            $(this).css("top", height);
        }
        else {
            alphabeticalContactSorting(1, global_alphabetpageIndex, global_alphabet, 0, 2)
        }
    }
});

$('#search-input').keyup(function (e) {
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

$('#search-input').click(function (e) {
    e.stopPropagation();
    $('#search').css("border-color", "#66afe9");
})
//On Click Select Functionality for All Mobile and Web Contacts
$(document).delegate('.contacts', 'click', function () {
    var mobileNo = $(this).find("p:eq(1)").text();
    var conId = $(this).attr("id");
    var imgPath = $(this).find('#profilePic img').attr("src");
    if ($(this).hasClass('selected') && !$(this).hasClass('unselected')) {

        var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
        selectedMobileNos.splice(index, 1);
        nameArray.splice(index, 1);
        memberImageArray.splice(index, 1);

        selectedContactsCount = selectedContactsCount - 1;
        $('.count').html('(' + selectedContactsCount + ')');
        var divName = $(this).attr("id");
        $('.selected').each(function () {
            var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
            if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                $(this).removeClass("selected");
                $(this).find('.fa-check').css('display', 'none');
            }

        });
        $('#selectedContacts div.unselected').each(function () {
            var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
            if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                $(this).remove();
            }

        });

        if (selectedContactsCount == 0) {
            $('a[href="#selectedContacts"]').hide();
            $('a[href="#selectedContacts"]').parent('li').removeClass('active');
            $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
            MobileTabClick();
        }
        else { $('a[href="#selectedContacts"]').show(); }



    }
    else if (!$(this).hasClass('unselected')) {
        var boolResult = true;
        //secondary contacts           
        if ($('#selectedContacts div.unselected#' + this.id).hasClass('unselected'))
            countContacts--;
        $('#selectedContacts div.unselected#' + this.id).removeClass('unselected').removeClass('selected').hide();
        //secondary contacts  end
        if ($('#selectedContacts div.unselected').length >= defaultLines - 1) {
            var minimumCount = "You can't select more than " + parseInt(defaultLines - 1) + " members";
            Notifier(minimumCount, 2);
            return false;
        }
        if (nameArray.length == 0)
        { $('#selectedContacts').html(''); }
        listId = $(this).attr('listId');
        var name = $(this).find("p:eq(0)").attr('name');
        var mobileNo = $(this).find("p:eq(1)").text();

        $(".contacts #profileDetails [p=" + conId + "]").addClass("selected");
        $('#selectedContacts div.unselected').each(function () {
            var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
            if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                Notifier("This Contact No was already Selected", 2);
                $('.contacts[id=' + conId + ']').removeClass("selected");
                boolResult = false;
            }

        });
        if (boolResult) {
            selectedMobileNos.push($.trim(mobileNo.slice(sliceNumber)));
            nameArray.push(name);
            memberImageArray.push(imgPath);
            // $(this).addClass('selected')
            // $(this).find('.fa-check').css('display', 'block');
            selectedContactsCount = selectedContactsCount + 1;
            $('.count').html('(' + selectedContactsCount + ')');
            selectedContacts = '<div class="contacts margin-right-5 margin-bottom-5 unselected" id="0" listId="0"><div id="profilePic"><img alt="default user" src="' + imgPath + '"></div><div id="profileDetails">';
            if (name.length > 17) {
                selectedContacts += '<p name="' + name + '" title="' + name + '">' + name.substring(0, 14) + '...</p>';
            }
            else {
                selectedContacts += '<p name="' + name + '" >' + name + '</p>';
            }
            selectedContacts += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNo + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>';
            $('#selectedContacts').append(selectedContacts);
            $('.contacts').each(function () {
                var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                    $(this).addClass('selected')
                    $(this).find('.fa-check').css('display', 'block');
                }
            });

        }
        if (selectedContactsCount == 0) {
            $('a[href="#selectedContacts"]').hide();
            $('a[href="#selectedContacts"]').parent('li').removeClass('active');
            $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
            MobileTabClick();
        }
        else { $('a[href="#selectedContacts"]').show(); }


    }

});


// DatePicker Intitialization and setting for input Elemtn
$("#dtBox").DateTimePicker({
    isPopup: true,
    dateTimeFormat: "dd-MM-yyyy HH:mm:ss",
    defaultDate: myDateFormatter(mdate),
    formatHumanDate: function (date) {
        return date.day + ", " + date.month + " " + date.dd + ", " + date.yyyy;
    }
});


//For All Web C

//For Mobile Contacts
function getMobileContacts(src) {
    pageIndex++;
    global_alphabetpageIndex = 0;
    mobileScroll = 0;
    if (pageIndex == 1 || pageIndex <= pageCount) {
        if (pageIndex == 1) {
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
            success: function (result) {
                pageCount = result.pageCount;
                defaultLines = result.DefaultLines;
                nameSplit = "";
                mobileContactsData = "";
                if (result.Success == true) {
                    jsonResponse = result;
                    if (result.contactDetails.length != 0) {
                        if (pageIndex == 1) {
                            mobileContactsData += '<ul class="alphabet"><li id="specialCharSearch">#</li><li>A</li><li>B</li><li>C</li><li>D</li><li>E</li><li>F</li><li>G</li><li>H</li><li>I</li><li>J</li><li>K</li><li>L</li><li>M</li><li>N</li><li>O</li><li>P</li><li>Q</li><li>R</li><li>S</li><li>T</li><li>U</li><li>V</li><li>W</li><li>X</li><li>Y</li><li>Z</li></ul>';
                        }
                        for (var j = 0; j < result.contactDetails.length ; j++) {
                            selectedClass = ""; faCheckPic = ""; imgPath = "";
                            if ($.trim(result.contactDetails[j].source) == "1") {
                                //nameSplit = (result.contactDetails[j].name).substring(0, 2).toUpperCase();
                                for (var i = 0; i < selectedMobileNos.length; i++) {
                                    var responseMobileNo = $.trim(selectedMobileNos[i]).slice(sliceNumber);
                                    var contactsMobileNo = $.trim(result.contactDetails[j].mobileNumber).slice(sliceNumber);

                                    if (contactsMobileNo == responseMobileNo) {
                                        selectedClass += "selected"
                                        faCheckPic += "block";
                                    }
                                        //secondary contacts
                                    else if (result.contactDetails[j].subContactDeatils.length > 0) {
                                        for (var k = 0; k < result.contactDetails[j].subContactDeatils.length; k++)
                                        {
                                            if (result.contactDetails[j].subContactDeatils[k].contactNumber.slice(sliceNumber) == responseMobileNo.slice(sliceNumber))
                                            {
                                                selectedClass += "selected"
                                                faCheckPic += "block";
                                                break;
                                            }
                                        }                                       
                                    }//secondary contacts end
                                    else {
                                        selectedClass += "";
                                        faCheckPic += "";
                                    }
                                }

                                mobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + result.contactDetails[j].id + '" listId="m1" >';
                                //if ($.trim(result.contactDetails[j].imagePath) != '') {
                                //    if ($.trim(result.contactDetails[j].subContactDeatils).length != 0) {
                                //        subContactsResponse.push(result.contactDetails[j].subContactDeatils);
                                //        mobileContactsData += '<div id="profilePic" ><img src="/' + result.contactDetails[j].imagePath + '" style="height:28px;width:30px;border-radius:50% " alt="Profile Pic"></div><a id="btnDropdown"  contactid=' + result.contactDetails[j].id + ' mobile=' + result.contactDetails[j].mobileNumber + ' class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown"><span class="caret"></span></button> <ul class="dropdown-menu" id="mobileNumber' + result.contactDetails[j].mobileNumber + '" ></ul></div>';
                                //    }
                                //    else {
                                //        mobileContactsData += '<div id="profilePic" ><img src="/' + result.contactDetails[j].imagePath + '" style="height:28px;width:30px;border-radius:50% " alt="Profile Pic"></div>';
                                //    }
                                //}

                                //else {
                                //    if ($.trim(result.contactDetails[j].subContactDeatils).length != 0) {
                                //        mobileContactsData += '<div id="profilePic" ><span style=" background: #add9ec none repeat scroll 0 0;border-radius: 50%;color: #428bca;font-weight: bold;height: 34px;padding: 9px;width: 32px;">' + nameSplit + '</span> <button id="btnDropdown" contactid=' + result.contactDetails[j].id + ' mobile=' + result.contactDetails[j].mobileNumber + ' class="btn btn-primary dropdown-toggle" type="button" data-toggle="dropdown"><span class="caret"></span></button> <ul class="dropdown-menu" id="mobileNumber' + result.contactDetails[j].mobileNumber + '" ></ul></div>';
                                //    }
                                //    else {
                                //        mobileContactsData += '<div id="profilePic" ><img  src="images/Profile_Img.png" alt="Profile Pic"></div>';
                                //    }
                                //}
                                if ($.trim(result.contactDetails[j].imagePath) == null) {
                                    imgPath += 'images/avatar-img-5.jpg'
                                }
                                else if ($.trim(result.contactDetails[j].imagePath) != '')
                                { imgPath += result.contactDetails[j].imagePath; }
                                else {
                                    imgPath += 'images/avatar-img-5.jpg'
                                }
                                mobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                                mobileContactsData += '<div id="profileDetails">';
                                if (result.contactDetails[j].name.length > 17) {
                                    mobileContactsData += '<p name="' + result.contactDetails[j].name + '" title="' + result.contactDetails[j].name + '">' + result.contactDetails[j].name.substring(0, 14) + '...</p>';
                                }
                                else {
                                    mobileContactsData += '<p name="' + result.contactDetails[j].name + '">' + result.contactDetails[j].name + '</p>';
                                }
                                if ($.trim(result.contactDetails[j].subContactDeatils).length != 0) {
                                    subContactsResponse.push(result.contactDetails[j].subContactDeatils);
                                    mobileContactsData += '<p id="phoneNumber' + result.contactDetails[j].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.contactDetails[j].mobileNumber + '<div class="dropdown" style="float:right;margin: -3px 0 0;"> <a id="btnDropdown"  contactid=' + result.contactDetails[j].id + ' mobile=' + result.contactDetails[j].mobileNumber + ' class="dropdown-toggle"  data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + result.contactDetails[j].mobileNumber + '" >';
                                    mobileContactsData += "<li class='ddlList'><a id=primary-" + result.contactDetails[j].mobileNumber + ">" + result.contactDetails[j].mobileNumber + "</a></li>";
                                    for (var sub = 0; sub < result.contactDetails[j].subContactDeatils.length; sub++) {
                                        var anchorId = result.contactDetails[j].subContactDeatils[sub].contactType + '-' + result.contactDetails[j].subContactDeatils[sub].contactNumber;
                                        mobileContactsData += "<li class='ddlList'><a id=" + anchorId + ">" + result.contactDetails[j].subContactDeatils[sub].contactNumber + "</a></li>";
                                    }
                                   
                                    mobileContactsData += '</ul></div></p>';
                                }
                                else {
                                    mobileContactsData += '<p id="phoneNumber' + result.contactDetails[j].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.contactDetails[j].mobileNumber + '</p>';
                                }
                                if ($.trim(faCheckPic).length == 0) {
                                    faCheckPic = "none"
                                }
                                mobileContactsData += '</div><i class="fa fa-check select_check" style="display:' + faCheckPic + '"></i>';
                                mobileContactsData += '</div>';
                            }
                        }
                        mobileContactsAppend = mobileContactsData;
                        setTimeout($.unblockUI, 200);
                        $('#grpCallMobileContacts').append(mobileContactsData);

                    }
                }
                if (result.Success == false) {
                    $('#grpCallMobileContacts').html('You have not synchronized your mobile contacts yet. Sync manually from your app to start!<div class="row block text-center col-md-offset-1" style="padding-top: 90px;width:60%"><div class="col-md-3 text-right" style="border-right:1px solid #d2d2d2"><img src="images/android.png" width="50" height="50"></div><div class="col-md-8 pull-left" style="font-size:20px;">Open grpTalk app in your mobile, Select "+" from home page, Press menu and select "Sync Contacts"</div></div><br><div class="row block text-center margin-top-20 col-md-offset-1" style="padding-bottom: 90px;width:60%"><div class="col-md-3 text-right" style="border-right:1px solid #d2d2d2"><img src="images/IOS.png" width="50" height="50"></div><div class="col-md-8 pull-left" style="font-size:20px;">Open grpTalk app in your mobile, Select "+" from home page, Press menu and select "Sync Contacts"</div></div>')
                    $('#grpCallMobileContacts').css('font-size', '22px')
                    $('#grpCallMobileContacts').css('color', '#0082C3')
                    $('#grpCallMobileContacts').attr('align', 'center');
                    setTimeout($.unblockUI, 200);
                    $('#selectedContacts').html(''); selectedContactsCount = 0;
                    if (selectedContactsCount == 0) {
                        $('a[href="#selectedContacts"]').hide();
                        $('a[href="#selectedContacts"]').parent('li').removeClass('active');
                        $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
                        MobileTabClick();
                    }
                    else { $('a[href="#selectedContacts"]').show(); }
                }
            },
            error: function (result) {
                Notifier("Something Went Wrong", 2);
                $.unblockUI();
            }


        });
        $.unblockUI();
    }
}



// Alphabet CLick Funtionality
$(document).on('click', '.alphabet li', function (e) {
    $('#search-input').val('');
    $('#search').css("border-color", "none");
    var clickCounter = $(this).data('clickCounter') || 0;
    if (clickCounter == 0) {
        clickCounter += 1;
        global_searchndex = 0;
        $(this).data('clickCounter', 0);
        global_alphabetMobileContactsData = "";
    }
    alphabetClick++;
    //alphabetPageIndex++;

    global_alphabetMobileContactsData = "";
    var alphabet = $(this).text().toLowerCase();
    subContactsResponse = new Array();
    global_alphabet = alphabet;
    if (alphabet != '#') {
        alphabetOrSerachClick(global_searchndex, alphabet);
    }
    alphabetClick++;
    e.stopPropagation();
});




//Search Button Fnct
$(document).on('click', '#search', function (e) {
    e.preventDefault();
    global_searchndex = 0;
    var alphabet = $('#search-input').val().toLowerCase();
    var activeTab = $('ul#myTabList li.active').find("a").attr("href");
    if ($.trim(alphabet).length == 0) {
        if (activeTab == "#grpCallMobileContacts") {
            subContactsResponse = new Array();
            $('#grpCallMobileContacts').html('');
            pageIndex = 0;
            getMobileContacts(1);
            return;
        }
        else { selectedContactsTabClick(''); }
    }


    global_alphabetMobileContactsData = "";
    subContactsResponse = new Array();
    global_alphabet = alphabet;
    alphabetOrSerachClick(global_searchndex, alphabet);
    e.stopPropagation();


});


// FOR both Mobile and Web Active Tab Search 
function alphabetOrSerachClick(global_searchndex, alphabet) {
    var activeTab = $('ul#myTabList li.active').find("a").attr("href");
    var sourceValue = 0;
    var spMode = 2;
    if (activeTab == "#grpCallMobileContacts") {
        sourceValue = 1;

        alphabeticalContactSorting(sourceValue, global_searchndex, alphabet, 0, spMode);
    }
    else {
        selectedContactsTabClick(alphabet);
    }
}

//---------Selected Contacts Tab Search 
function selectedContactsTabClick(alphabet) {
    var matchArray = ""; var divSelected = "";
    var mobileMatchArray;

    for (var i = 0; i < nameArray.length; i++) {

        var profileName = nameArray[i].toLowerCase();
        var mobileNumber = selectedMobileNos[i];
        var myregex = new RegExp(alphabet.toLowerCase());
        matchArray = myregex.exec(profileName);
        mobileMatchArray = myregex.exec(mobileNumber);
        if (matchArray != null || mobileMatchArray != null) {
            divSelected += '<div class="contacts margin-right-5 margin-bottom-5 unselected" id="0" listId="0"><div id="profilePic"><img alt="default user" src="' + memberImageArray [i]+ '"></div><div id="profileDetails">';
            if (nameArray[i].length > 17) {
                divSelected += '<p name="' + nameArray[i] + '" title="' + nameArray[i] + '">' + nameArray[i].substring(0, 14) + '...</p><p><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedMobileNos[i] + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>';
            }
            else { divSelected += '<p name="' + nameArray[i] + '">' + nameArray[i] + '</p><p><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedMobileNos[i] + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>'; }
        }
    }
    if ($.trim(divSelected) == "")
    { divSelected = "No Contacts Found "; }
    $('#selectedContacts').html(divSelected);

}


//Ajax Calling for Alpabet click and Search Click
function alphabeticalContactSorting(sourceValue, alphabetPageIndex, alphabet, listId, spMode) {
    alphabetPageIndex++;
    if (alphabetPageIndex == 1 || alphabetPageIndex <= alphabetPageCount) {
        global_alphabetpageIndex = alphabetPageIndex;
        if (alphabetPageIndex == 1) {
            alphabetCOntactsCount = 0;
            sortSubContactsResponse = new Array();
        }


        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 9,
                source: sourceValue,
                alphabetPageIndex: alphabetPageIndex,
                modeSp: spMode,
                alphabetSort: alphabet,
                listID: listId,
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (contactResult) {
                alphabetPageCount = contactResult.pageCount;
                alphabetResponse(contactResult, sourceValue);
                setTimeout($.unblockUI, 200);
            },
            error: function (contactResult) {
                Notifier("Something Went Wrong", 2);

            }


        });
    }
}


//For Final Save Ajax Functionality
$('#saveGrpCall').click(function () {
    var repeats = $('#repeat').val();
    var grpTalkName = $('#conf_name').val();
    var grpID = $('#grpId').val();
    var memberName = "", memberMobile = "";
    var weekDays = "";
    var schduled_datetime = new Array();
    schduled_datetime = $('#conf_datetime').val().split("at");
    var schType = 0;
    var editType = 0;
    var participantsInEditCall = "";
    if ($.trim(repeats) == "") {
        weekDays = "";
        editType = 0;
        schType = 0;
    }
    else {
        weekDays = repeats;
        editType = 1;
        schType = 1;
    }
    minDate = new Date();
    minDate.setMinutes(minDate.getMinutes() + 10);

    var hostNumber = $('#hostNumber').val();
    var boolValue = true;
    $('#selectedContacts div.unselected').each(function () {
        memberName = $(this).find('p:eq(0)').attr('name');
        memberMobile = $(this).find('p:eq(1)').text();
        participantsInEditCall += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false"}' + ",";

        if (memberMobile.slice(sliceNumber) == hostNumber.slice(sliceNumber)) {
            //$('#grpCallErrorMessage').html('Please remove your number from Group').show();
            Notifier("Please remove your number from Group", 2);
            boolValue = false;
            return false;
        }

    });
    if (boolValue) {
        var participantsLenght = $('#selectedContacts').find("div.unselected").length;
        if ($.trim($('#conf_name').val()).length == 0) {
            //$('#grpCallErrorMessage').html('Please enter Conference Name').show();
            Notifier("Please Enter Group Name", 2);
            return false;
        }
        if ($.trim($('#conf_datetime').val()).length == 0) {
            //$('#grpCallErrorMessage').html('Please Select Schedule DateTime').show();
            Notifier("Please Select Schedule Date and Time", 2);
            return false;
        }
        if (participantsLenght == 0) {

            Notifier("Please Choose  Participants To Make A Call", 2);
            return false;
        }
        if (scheduleClick != 0) {

            if (editType == 0) {
                var selectedDate = "";
                var date = new Array();
                selectedDate = new Date(schduled_datetime[0] + " " + schduled_datetime[1]);
                if (Date.parse(selectedDate) < Date.parse(minDate)) {
                    //$('#grpCallErrorMessage').html('Please Select atleast 5 mins more than the current time').show();
                    Notifier("Please select 10 mins from now", 2);
                    return false;
                }
            }
        }

        $('#grpCallErrorMessage').html('').hide()
        response = '{"Type":"' + editType + '","IsMuteDial":"0","SchType":"' + schType + '","WebListIds":"","Reminder":"30","WeekDays":"' + weekDays + '","GroupID":"' + grpID + '","GroupCallName":"' + grpTalkName + '","SchduledTime":"' + schduled_datetime[1] + '","SchduledDate":"' + schduled_datetime[0] + '","Participants":[' + participantsInEditCall.replace(/,(?=[^,]*$)/, '') + ']}';
        editGroupCall(response);
    }
})

// $('#conf_datetime').click(function(e){

// e.stopPropagation();

// });
function editGroupCall(response) {
    $.ajax({
        url: 'HandlersWeb/Groups.ashx',
        type: 'post',
        async: false,
        dataType: 'json',
        data: {
            Type: 5,
            jsonObject: response,
        },
        success: function (result) {
            alert("Group call was edited successfully");
            window.location.href = "/Mygroup.aspx";

        },
        error: function (result) {
            Notifier("Failed To Edit your Call", 2);
        },
    });
}

$('#clearRepeat').click(function () {
    $('.weekDay').each(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
        }
    });

});



// Success Response for Special Chars, Alphabet and search box
function alphabetResponse(contactResult, sourceValue) {
    if (global_alphabetpageIndex == 1) {
        global_alphabetMobileContactsData += '<ul class="alphabet"><li id="specialCharSearch">#</li><li>A</li><li>B</li><li>C</li><li>D</li><li>E</li><li>F</li><li>G</li><li>H</li><li>I</li><li>J</li><li>K</li><li>L</li><li>M</li><li>N</li><li>O</li><li>P</li><li>Q</li><li>R</li><li>S</li><li>T</li><li>U</li><li>V</li><li>W</li><li>X</li><li>Y</li><li>Z</li></ul>';
    }
    if (contactResult.Success == true) {
        jsonResponse = contactResult;

        if (contactResult.contactDetails.length != 0) {
            webPageCount = contactResult.pageCount;
            for (var contactRows = 0; contactRows < contactResult.contactDetails.length ; contactRows++) {
                selectedClass = "", faCheckPic = "";
                var listIds = new Array();
                var contactIds = new Array();
                listIds = global_listId.split(","); contactIds = selectedContactsArray.split(",");
                for (var j = 0; j < selectedMobileNos.length; j++) {
                    var responseMobileNo = $.trim(selectedMobileNos[j]).slice(sliceNumber);
                    var contactsMobileNo = $.trim(contactResult.contactDetails[contactRows].mobileNumber).slice(sliceNumber);

                    if (contactsMobileNo == responseMobileNo) {
                        selectedClass += "selected"
                        faCheckPic += "block";

                    }
                    else {
                        selectedClass += "";
                        faCheckPic += "";
                    }
                }
                var imgPath = "";
                if ($.trim(contactResult.contactDetails[contactRows].imagePath) == null) {
                    imgPath += 'images/avatar-img-5.jpg'
                }
                else if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '')
                { imgPath += contactResult.contactDetails[contactRows].imagePath; }
                else {
                    imgPath += 'images/avatar-img-5.jpg'
                }

                if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                    global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + contactResult.contactDetails[contactRows].id + '" listId="m1" >';
                    //if ($.trim(contactResult.contactDetails[contactRows].imagePath) != '') {
                    //    global_alphabetMobileContactsData += '<div id="profilePic"><img src="/' + contactResult.contactDetails[contactRows].imagePath + '" style="height:30px;width:30px;border-radius:50% " alt="Profile Pic"></div>';
                    //}

                    //else {
                    //    global_alphabetMobileContactsData += '<div id="profilePic" ><img  src="images/Profile_Img.png" alt="Profile Pic" style="height:30px;width:30px;border-radius:50% "></div>';
                    //}
                    global_alphabetMobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    global_alphabetMobileContactsData += '<div id="profileDetails">';
                    if (contactResult.contactDetails[contactRows].name.length > 17)
                    { global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '" title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 14) + '...</p>'; }
                    else {
                        global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name + '</p>';
                    }

                    if ($.trim(contactResult.contactDetails[contactRows].subContactDeatils).length != 0) {
                        subContactsResponse.push(contactResult.contactDetails[contactRows].subContactDeatils);
                        global_alphabetMobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '<div class="dropdown" style="float:right;margin: -3px 0 0;"> <a id="btnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="dropdown-toggle"  data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" ></ul></div></p>';

                    }
                    else {
                        global_alphabetMobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                    }
                    //}
                    global_alphabetMobileContactsData += '</div>';
                    mobileScroll = 1;

                }

                if ($.trim(faCheckPic).length == 0) {
                    faCheckPic = "none";
                }
                global_alphabetMobileContactsData += '<i class="fa fa-check select_check" style="display:' + faCheckPic + '"></i></div>';
            }
        }
        else {

            global_alphabetMobileContactsData += "No Contacts Found";
        }


        $('#grpCallMobileContacts').show();
        $('#grpCallMobileContacts').html('');
        $('#grpCallMobileContacts').html(global_alphabetMobileContactsData);



    }

    if (contactResult.Success == false) {

        global_alphabetMobileContactsData += "No Contacts Found";
        $('#grpCallMobileContacts').show();
        $('#grpCallMobileContacts').html(global_alphabetMobileContactsData);



    }
}

function MobileTabClick() {
    $('#listTitleName').text('Select Contacts')
    $('#grpCallMobileContacts,.alphabet').show();

    $('#grpCallMobileContacts').parent().show();

    if ($('#selectedContacts').parent('.slimScrollDiv').length != 0)
    { $('#selectedContacts').parent().hide(); }
    $('grpCallMobileContacts.slimScrollDiv').show();
    $('#grpCallMobileContacts').addClass("active in");
    $('#selectedContacts').removeClass("active in");
}

function sliceNumberFn() {
    var countryId = $('#countryID').val();
    if (countryId == 108) {
        sliceNumber = -10;
    }
    else if (countryId == 19)
    { sliceNumber = -8; }
}

function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
$(document).click(function (e) {
    $('#search').css("border-color", "none");

});

function getConferenceDetails(grpId) {
    var retResult = "";
    $.ajax({
        url: "/HandlersWeb/Groups.ashx",
        data: {
            type: 11,
            grpCallId: grpId

        },
        method: "POST",
        async: false,
        dataType: "JSON",
        success: function (result) {
            if (result.Success == true) {
                retResult = result;
            }
            else if (result.Success == false) {
                alert('Invalid or Inactive GroupCall');
                window.location.href = "/Mygrptalks.aspx";
            }
        },
        error: function (result) {
            Notifier('Something Went Wrong', 2)
            return false

        }
    });

    return retResult;

}
