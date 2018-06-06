/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
var regEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var regMobile = /^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[9|8|7][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$/;
var reg = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
var listId = 0, imgPath = "", webContactsData = "", mobileContactsData = "", defaultLines = 0, scheduleClick = 0;
var imgName = "", roundCropX = 0; roundCropY = 0; roundCropW = 0; roundCropH = 0;
var webContactsAppend = "";
var mobileDropDownlist = "";
var mobileNumberVal = "";
var subContactsResponse = new Array();
var webPageIndex = 0, searchValue = "", alphabetSort = "", pageIndex = 0;
var subContactsResponse = new Array();
var pageCount = 0, contactId = 0, countContacts = 0, grpTalkName = "", webListsAppend = "";
var webPageCount = 0, global_alphabetMobileContactsData = "";
var selectedContacts = "", currentTime = "", currentDate = "", response = "";
var jsonContacts = new Array(), participants = new Array();
var alphabetPageIndex = 0;
var global_alphabetMobileContactsData = "";
var global_searchndex = 0;
var grpTalkEditArray = JSON.parse(localStorage.myArray);
var grpTalkEditJson = $.parseJSON(grpTalkEditArray);
var partcipants = "";
var selectedContactsCount = 0, selectBtnText = "";
var listClick = 0;
var global_listId = "", mdate = "", firstClick = 0, alphabetWebClick = 0, alphabetMobileClick = 0;
var isLineOpen = 0, isAllowNonMems = 0, onlyDialIn = 0, isMuteDial = 0;
var imgName = "", roundCropX = 0; roundCropY = 0; roundCropW = 0; roundCropH = 0, G_img_name = "";
var minDate = "", faCheckPic = "", selectedClass = "", mobileScroll = 0, webScroll = 0;
var editSchType = "", editWeekDays = "", selectedTabClick = 0, selectedTabSearch = 0;
var selectedMobileNos = new Array();
var sliceNumber = 0;
var nameArray = new Array();
var alphabetPageCount = 0;
var global_alphabet = '';
var global_alphabetpageIndex = 0;
var listIdArray = new Array();
var contactIdArray = new Array();
var creategropTalk = 0;
var xlList = "block";
var clearDate = new Date();
var isDateTab = 0;
var isSecondaryModerator = 0, secondaryModerator = '', managerMobile, hostNumber = '';
var sysTimeZone = -(new Date().getTimezoneOffset());
var userTimeZone = $('#hdnOffSet').val();
var confTimeZone = 0;
$(document).ready(function () {
    var time = new Array();

    CircleSel = function () { };

    // Set the custom selection's prototype object to be an instance
    // of the built-in Selection object
    CircleSel.prototype = new $.Jcrop.component.Selection();

    // Then we can continue extending it
    $.extend(CircleSel.prototype, {
        zoomscale: 1,
        attach: function () {
            this.frame.css({
                //background: 'url(' + $('#img_for_crop')[0].src.replace('750','750') + ')'
            });
        },
        positionBg: function (b) {
            var midx = (b.x + b.x2) / 2;
            var midy = (b.y + b.y2) / 2;
            var ox = (-midx * this.zoomscale) + (b.w / 2);
            var oy = (-midy * this.zoomscale) + (b.h / 2);
            //this.frame.css({ backgroundPosition: ox+'px '+oy+'px' });
            this.frame.css({ backgroundPosition: -(b.x + 1) + 'px ' + (-b.y - 1) + 'px' });
        },
        redraw: function (b) {

            // Call original update() method first, with arguments
            $.Jcrop.component.Selection.prototype.redraw.call(this, b);

            this.positionBg(this.last);
            return this;
        },
        prototype: $.Jcrop.component.Selection.prototype
    });
    var grpTalkResultJson = "";
    grpTalkResultJson = getConferenceDetails(grpTalkEditJson.Groups[0].GroupID);
    sliceNumberFn();

    if (grpTalkResultJson.Groups[0].IsSecondaryModerator == true)
        isSecondaryModerator = 1;
    if (grpTalkResultJson.Groups[0].SecondaryModerator != null)
        secondaryModerator = grpTalkResultJson.Groups[0].SecondaryModerator;
    hostNumber = grpTalkResultJson.Groups[0].HostNumber;
    partcipants = grpTalkResultJson.Groups[0].GrpParticipants;
    editSchType = grpTalkResultJson.Groups[0].SchType;
    editWeekDays = grpTalkResultJson.Groups[0].WeekDays;
    isLineOpen = grpTalkResultJson.Groups[0].IsLineOpen;
    isAllowNonMems = grpTalkResultJson.Groups[0].isAllowNonMembers;
    onlyDialIn = grpTalkResultJson.Groups[0].isOnlyDialIn;
    isMuteDial = grpTalkResultJson.Groups[0].IsMuteDial;
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
        listIDEdit = partcipants[j].ListId;
        contactIdEdit = partcipants[j].ContactId;
        selectedContacts += addContact(name, mobileNo, contactIdEdit, listIDEdit, "unselected", "images/avatar-img-5.jpg", "block");
        selectedMobileNos.push(mobileNo.replace(/\D/g, "").slice(sliceNumber));
        listIdArray.push(listIDEdit);
        nameArray.push(name);
        contactIdArray.push(contactIdEdit);
    }

    $('#selectedContacts').html(selectedContacts);
    selectedContactsCount = partcipants.length;
    $('.count').html('(' + selectedContactsCount + ')')
    if (selectedContactsCount == 0) {
        $('a[href="#selectedContacts"]').hide();
        $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
        $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
        WebTabClick();
    }
    else { $('a[href="#selectedContacts"]').show(); }
    $('#repeat').val(editWeekDays);
    if (grpTalkResultJson.Groups.length != 0) {
        $('#conf_name').val(grpTalkResultJson.Groups[0].GroupCallName);
        datetimetotaltext = mdate.split(' ')
        datetotaltext = datetimetotaltext[0].split('-')
        timetotaltext = datetimetotaltext[1].split(':')
        selectdatemonth = datetotaltext[1] - 1
        outtimetext = formatDate(timetotaltext[0], timetotaltext[1]);
        outdatetext = montharr[selectdatemonth] + ' ' + datetotaltext[0] + ', ' + datetotaltext[2] + ' at ' + outtimetext
        $('#conf_datetime').val(outdatetext);
        $('#grpId').val(grpTalkResultJson.Groups[0].GroupID);
    }

    $('#successMessage,#errorMessage').html("");
    getMobileContacts(1);
    getAllWebLists(0);
    $('#getAllContacts').parents('.list').addClass("highlight");
    if ($('#grpCallWebContacts div.contacts').length > 0) {
        $('#selectAll').show();
    }
    listId = $('.list .highlight').attr("id");

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

    $('#selectedContacts').hide();
    $('#phoneContactsDisplay,#phnCntcts').hide();
    $('#selectAll').hide();
    tabClickFetchContacts()
    if (activeTab == "#grpCallWebContacts") {
        WebTabClick();
        if ($('#grpCallWebContacts div.contacts').length > 0) {
            $('#selectAll').show();
        }

    }
    else if (activeTab == "#grpCallMobileContacts") {
        $('#selectAll').hide();
        if (firstClick == 0) {
            $('#grpCallMobileContacts').slimScroll({
                allowPageScroll: false,
                height: '250',
            });
        }

        $('#listTitleName').text('Select Contacts')
        $('#grpCallMobileContacts,.alphabet').show();
        $('#grpCallWebContacts,#list-group').parent().hide();
        $('#grpCallWebContacts').hide();
        $('#grpCallMobileContacts').parent().show();
        $('#phoneContactsDisplay,#phnCntcts').show();
        $('#listName').text("All Web Contacts");

        $('.addNewContact,#plusSymbol').hide();
        $('.list').hide();

        if ($('#selectedContacts').parent('.slimScrollDiv').length != 0)
        { $('#selectedContacts').parent().hide(); }
        $('grpCallMobileContacts.slimScrollDiv').show();
        $('#grpCallMobileContacts').addClass("active in");
        $('#grpCallWebContacts,#selectedContacts').removeClass("active in");
        firstClick++;

    }
    else if (activeTab == "#selectedContacts") {
        $('#alphabetWebContacts,#alphabetMobileContacts').hide();
        $('#grpCallWebContacts,#list-group').parent().hide();
        $('#grpCallMobileContacts,.alphabet').hide();
        if ($('#grpCallMobileContacts').parent('.slimScrollDiv').length != 0)
        { $('#grpCallMobileContacts').parent().hide(); }
        $('#grpCallWebContacts,#plusSymbol').hide();
        $('#selectedContacts').parent().show();
        $('#selectedContacts').show();
        $('#phoneContactsList').hide();
        // $('#webContactList').hide();
        $('#listTitleName').text('Select Contacts')
        $('.addNewContact').hide();
        $('.list').hide();
        $('#selectedContacts').addClass("active in");

        $('#grpCallWebContacts,#grpCallMobileContacts').removeClass("active in");
        if (selectedTabClick == 0) {
            $('#selectedContacts').slimScroll({
                allowPageScroll: false,
                height: '250',
            });
        }
        selectedTabClick++;
    }


});

$(document).on('click', '.addNewContact', function (e) {
    e.preventDefault();
    listId = $(this).attr('listId');
    $('#name, #mobileNumber').val('');
    $('#webContactProfile').attr('src', '');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
    $('#errorDescForName, #errorDescForMobile').html('');
    $('#successMessage,#errorMessage').html("");
    $('#contactsModal').modal('show');

});

$('#newContact').on('click', function () {
    $('#successMessage,#errorMessage,#errorDescForName,#errorDescForMobile').html("");
    $('#excelFormBody').hide();
    $('#contactFormBody').show();
    $('#saveExcelContacts').hide();
    $('#saveContact').show();
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
});

$('#savedialinswitch').change(function (e) {
    e.stopPropagation();

    if ($('#savedialinswitch').is(':checked')) {
        $('#openlinecondition').show();
    }
    else {
        $('#openlinecondition').hide();
        $('#openlineswitch').attr("checked", false);
    }
})



$('#openswitch').click(function (e) {

    if ($('#conf_datetime').val().length == 0) {
        Notifier('Please select date and time', 2);
        return false;
    }
    if ($('#conf_datetime').val().length != 0) {
        openLineSwitchTimeCheck();
    }


});



$('#adSettings').click(function (e) {
    $('#advancedSettings').modal("show");
    // $("#txtAssignMangerEdit").val('');
    //CALL MANAGER   

    if (isSecondaryModerator != 1) {
        $("#editManager").show();
        if (secondaryModerator == null)
            secondaryModerator = '';
        if (secondaryModerator != '') {
            $('#assignManagerEdit').attr("checked", true);
            $("#selectMangerEdit").show();

            var index = selectedMobileNos.indexOf(secondaryModerator.slice(sliceNumber));
            $("#txtAssignMangerEdit").val(nameArray[index]);
        }
        else {
            $('#assignManagerEdit').attr("checked", false);
            $("#selectMangerEdit").hide();
        }

        if (managerMobile != '' && managerMobile != undefined) {
            $('#assignManagerEdit').attr("checked", true);
            $("#selectMangerEdit").show();
        }

        //auto complete for manager  

        var autoArray = selectedMobileNos.concat(nameArray)
        $("#txtAssignMangerEdit").autocomplete({
            source: autoArray
        });
        //auto complete for manager end
    }
    else {
        $("#editManager").hide();
    }

    //CALL MANAGER END

    if (parseInt(onlyDialIn) == 1) {
        $('#savedialinswitch').attr("checked", true);
        $('#openlinecondition').show();
        if (parseInt(isLineOpen) == 1)
            $('#openlineswitch').attr("checked", true);
        else
            $('#openlineswitch').attr("checked", false);
    }
    else {
        $('#savedialinswitch').attr("checked", false);
        $('#openlinecondition').hide();
    }
    if (parseInt(isAllowNonMems) == 1)
        $('#saveallownonswitch').attr("checked", true);
    else
        $('#saveallownonswitch').attr("checked", false);

    if (parseInt(isMuteDial) == 1)
        $('#savemutedialswitch').attr("checked", true);
    else
        $('#savemutedialswitch').attr("checked", false);


});

$(document).on('click', "#assignManagerEdit", function () {
    if ($('#assignManagerEdit').is(':checked'))
        $("#selectMangerEdit").show();
    else
        $("#selectMangerEdit").hide();

});

$('#okBtn').click(function (e) {
    if ($('#savedialinswitch').is(':checked'))
        onlyDialIn = 1;
    else
        onlyDialIn = 0;
    if ($('#saveallownonswitch').is(':checked'))
        isAllowNonMems = 1;
    else isAllowNonMems = 0;
    if ($('#savemutedialswitch').is(':checked'))
        isMuteDial = 1;
    else
        isMuteDial = 0;

    if ($('#openlineswitch').is(':checked'))
        isLineOpen = 1;
    else
        isLineOpen = 0;
    //auto complete for manager  

    if (isSecondaryModerator != 1) {
        if ($('#assignManagerEdit').is(':checked')) {
            var manager = $("#txtAssignMangerEdit").val();
            var nameCount = 0; managerMobile = '';
            for (var i = 0; i < nameArray.length; i++) {
                if (manager == nameArray[i]) {
                    managerMobile = selectedMobileNos[i];
                    nameCount++;
                }
                if (manager == selectedMobileNos[i]) {
                    managerMobile = selectedMobileNos[i];
                }
            }
            if (manager == secondaryModerator)
                managerMobile = secondaryModerator;
            if (managerMobile == '') {
                alert("Please select mobile number for manager");
                return false;
            }
            else if (nameCount > 1) {
                alert("Please select mobile number, same name is there more than one time");
                $("#txtAssignMangerEdit").val('');
                return false;
            }

        }
    }
    //auto complete for manager end


});
$(document).on("focusin", "#conf_datetime", function (e) {

    isDateTab++;
})

$('#conf_datetime').focusout(function (e) {
    scheduleClick++;
    if ($('#conf_datetime').val.length != 0) {
        if ($('#openlineswitch').is(':checked')) {
            if ($('.weekDay .active').length == 0)
                openLineSwitchTimeCheck();


        }
    }
});

$('#imageUpld').fileupload({

    url: 'SaveProfileImageHandler.ashx?pic=small',
    add: function (e, data) {
        var uploadErrors = [];
        var acceptFileTypes = /^image\/(gif|jpe?g|png)$/i;
        if (data.originalFiles[0]['type'].length && !acceptFileTypes.test(data.originalFiles[0]['type'])) {
            uploadErrors.push('Not an accepted file type');
        }

        if (uploadErrors.length > 0)
            alert(uploadErrors.join("\n"));
        else
            data.submit();

    },
    done: function (e, data) {

        G_img_name = data.result;
        $('input[type="file"]').css('color', 'transparent');
        $('.jcrop-circle-demo').css('display', 'block');

        $("#webContactProfile").attr("src", "/Temp_crop_Images/" + G_img_name);

        load_crop_thumb();

    },
    error: function (e, data) {
        alert(data);

    }

});


$('#excelUpload').on('click', function () {
    $('#successMessage,#errorMessage,#errorDescForName,#errorDescForMobile,#errorDescForWebList').html("");
    $('#contactFormBody').hide();
    $('#excelFormBody').show();
    $('#saveContact').hide();
    $('#saveExcelContacts').show();
    $('#name,#mobileNumber,#newWebList').val('');
    $('#ddlWebList').val(0);
    $('.jcrop-circle-demo').css('display', 'none');
    $('#sheeterr,#headerselect').hide();
    $('#upmsg').html('');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');

});



$(document).on('click', '#saveContact', function (e) {
    e.preventDefault();

    var name = $('#name').val();
    var mobileNumber = $('#mobileNumber').val();
    var prefix = $('#prefix').val();
    listId = $('#ddlWebList').val();
    newList = $('#newWebList').val();
    var IsValid = true;
    var countryID = $('#countryID').val();
    if (name.toString() == "" || name == null) {
        IsValid = false;
        $('#name').closest('.form-group').addClass('has-error').removeClass('has-success')
        $('#errorDescForName').html('Please Enter Name');
    }
    else {
        $('#name').closest('.form-group').removeClass('has-error').addClass('has-success')
        $('#errorDescForName').html('');
    }

    if (mobileNumber == null || mobileNumber == "") {
        IsValid = false;
        $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
        $('#errorDescForMobile').html('Please Enter Mobile number');
    }
    if (countryID == 108) {
        //Iregx = /^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[9|8|7][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$/;
        var retVal = MobileValidator(mobileNumber);
        if (retVal == 0) {
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html("Please Enter Valid Mobile Number");
            return false;
        }
        else {
            $('#mobileNumber').closest(".form-group").removeClass('has-error').addClass('has-success')
            $('#errorDescForMobile').html('');
        }

    }
    else if (countryID == 241) {

        Iregx = /1?\d{10}/;

        if (mobileNumber == null || mobileNumber == "") {
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html('Please Enter Mobile number');
        }
        else if (!$.isNumeric(mobileNumber)) {
            //  IsValid = false;
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html("Please Enter only numeric values");
        }
        else if (!Iregx.test(mobileNumber)) {
            IsValid = false;
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html("Please Enter Valid Mobile Number");
        }

        else {
            $('#mobileNumber').closest(".form-group").removeClass('has-error').addClass('has-success')
            $('#errorDescForMobile').html('');
        }

    }

    if (parseInt(listId) == 0 && $.trim(newList) == '') {
        IsValid = false
        $('#errorDescForWebList').html("You must add this contact to an existing weblist or create a new list");
    }
    else {
        $('#errorDescForWebList').html('');
    }
    if (!IsValid) {
        return false;
    }
    if (parseInt(listId) != 0 && listId != null)
        manageWebContacts(1, 0, listId, name, mobileNumber);
    else
        newListAddContact(1, 0, newList, name, mobileNumber);

});


$('body').on('focusout', '#name', function (e) {
    var name = $('#name').val();
    if (name == "" || name == null) {
        $('#name').closest('.form-group').addClass('has-error').removeClass('has-success')
        $('#errorDescForName').html('Please Enter Name');
    }
    else {
        $('#name').closest('.form-group').removeClass('has-error').addClass('has-success')
        $('#errorDescForName').html('');
    }
});

$('body').on('focusout', '#newWebList', function (e) {
    if (parseInt($('#ddlWebList').val()) == 0) {
        var name = $('#newWebList').val();
        if (name == "" || name == null) {

            $('#errorDescForWebList').html('You must add this contact to an existing weblist or create a new list');
        }
        else {

            $('#errorDescForWebList').html('');
        }
    }
});

$('#ddlWebList').change(function (e) {
    var selectedValue = $('#ddlWebList').val();
    if (parseInt(selectedValue) != 0) {
        if ($('#newWebList').val().length > 0) {
            Notifier('You can select either Weblist or you can create a new list', 2);
            $('#newWebList').val('');

        }
        $('#newWebList').attr('disabled', true);
        $('#errorDescForWebList').html('');
    }
    else {
        $('#newWebList').attr('disabled', false);
    }
});
//--------------------------Validation while focusing on textbox
$('body').on('focusout', '#mobileNumber', function (e) {
    var mobileNumber = $('#mobileNumber').val();
    IsValid = true;

    var countryID = $('#countryID').val();
    if (countryID == 108) {
        //Iregx = /^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[9|8|7][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$/;
        var retVal = MobileValidator(mobileNumber);
        if (retVal == 0) {
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html("Please Enter Valid Mobile Number");
            return false;
        }
        else {
            $('#mobileNumber').closest(".form-group").removeClass('has-error').addClass('has-success')
            $('#errorDescForMobile').html('');
        }

    }
    else if (countryID == 241) {

        Iregx = /1?\d{10}/;

        if (mobileNumber == null || mobileNumber == "") {

            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html('Please Enter Mobile number');
        }
        else if (!$.isNumeric(mobileNumber)) {
            IsValid = false;
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html("Please Enter only numeric values");
        }
        else if (!Iregx.test(mobileNumber)) {
            IsValid = false;
            $('#mobileNumber').closest(".form-group").addClass('has-error').removeClass('has-success')
            $('#errorDescForMobile').html("Please Enter Valid Mobile Number");
        }

        else {
            $('#mobileNumber').closest(".form-group").removeClass('has-error').addClass('has-success')
            $('#errorDescForMobile').html('');
        }

    }

    if (!IsValid) {
        return false;
    }
});

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
    var mobileNo = $(this).find("p:eq(1)").text();
    var x = false;
    if (secondaryModerator.slice(sliceNumber) == mobileNo.slice(sliceNumber)) {
        x = confirm("This member is assigned as a call manager to this group. Do you want to delete this member and disable “assign call manager” setting?");
    }
    else
        x = true;
    if (hostNumber.slice(sliceNumber) == mobileNo.slice(sliceNumber)) {
        alert("You can not remove Host");
        x = false;
    }

    if (x == true) {
        var contactId = $(this).attr("id");
        var listId = $(this).attr("listid");
        selectedContacts = "";

        var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
        selectedMobileNos.splice(index, 1);
        nameArray.splice(index, 1);
        listIdArray.splice(index, 1);
        contactIdArray.splice(index, 1);

        selectedContacts = "";
        if ($(this).hasClass("unselected")) {
            $(this).remove();
            selectedContactsCount = selectedContactsCount - 1;
            $('.count').html('(' + selectedContactsCount + ')');
        }
        $('.contacts').each(function () {
            var selectedDivMobileNo = $(this).find("p:eq(1)").text();
            if (selectedDivMobileNo.slice(sliceNumber) == mobileNo.slice(sliceNumber)) {
                $(this).removeClass("selected");
                $(this).find('.fa-check').css('display', 'none');
            }
        })
        if (selectedContactsCount == 0) {
            $('a[href="#selectedContacts"]').hide();
            $('a[href="#selectedContacts"]', 'a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
            $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
            WebTabClick();
        }
        else { $('a[href="#selectedContacts"]').show(); }
    }


});

//For Web List Click
$(document).on('click', '.contactList', function () {
    $('.list').removeClass("highlight");
    $("#search-input").val('');
    $(this).parents('.list').addClass("highlight");
    listId = $(this).attr('id');
    listClick++;
    $('#selectAll').show();
    $('#grpCallWebContacts').html('');
    webPageIndex = 0;
    getAllWebLists(listId);
    $('#webList').html($(this).html());

});

$(document).click(function () {
    $('#search').css("border-color", "none");
});


// For Special Character Search
$(document).on('click', '#specialCharSearch', function () {
    var clickCounter = $(this).data('clickCounter') || 0;
    var alphabet = $(this).text().toLowerCase();
    var activeTab = $('ul#contactsTab li.active').find("a").attr("href");
    var sourceValue = 0;
    global_alphabetMobileContactsData = "";
    var spMode = 3;
    if (activeTab == "#webLists") {
        sourceValue = 2;
        alphabetWebClick++;
        alphabeticalContactSorting(sourceValue, 1, "", listId, spMode);
    }
    if (activeTab == "#phoneContacts") {
        sourceValue = 1;
        alphabetMobileClick++;
        alphabeticalContactSorting(sourceValue, 1, "", listId, spMode);

    }
});

// WebCOntacts OffSet Scrrol Function
$('#grpCallWebContacts').scroll(function () {
    if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 1) {
        if (global_alphabetpageIndex == 0) {
            webContactsOffsetScroll(listId);
            var height = $(this).scrollTop();
            $(this).css("top", height);
        }
        else {
            alphabeticalContactSorting(2, global_alphabetpageIndex, global_alphabet, 0, 2);
        }

    }
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
            alphabeticalContactSorting(1, global_alphabetpageIndex, global_alphabet, 0, 2);
        }
    }
});

//Input Search Element Enter Function
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

$('#search-input').click(function (e) {
    e.stopPropagation();
    $('#search').css("border-color", "#66afe9");
})


//On Click Select Functionality for All Mobile and Web Contacts
$(document).delegate('.contacts', 'click', function () {
    var mobileNo = $(this).find("p:eq(1)").text();
    var conId = $(this).attr("id");

    if ($(this).hasClass('selected') && !$(this).hasClass('unselected')) {
        var x = false;
        if (secondaryModerator.slice(sliceNumber) == mobileNo.slice(sliceNumber)) {
            x = confirm("This member is assigned as a call manager to this group. Do you want to delete this member and disable “assign call manager” setting?");
        }
        else
            x = true;

        if (hostNumber.slice(sliceNumber) == mobileNo.slice(sliceNumber)) {
            alert("You can not remove Host");
            x = false;
        }
        if (x == true) {
            var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
            selectedMobileNos.splice(index, 1);
            nameArray.splice(index, 1);
            listIdArray.splice(index, 1);
            contactIdArray.splice(index, 1);

            selectedContactsCount = selectedContactsCount - 1;
            $('.count').html('(' + selectedContactsCount + ')');
            var divName = $(this).attr("id");
            $('.selected').each(function () {
                var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                    $(this).removeClass("selected");
                    $(this).find('.fa-check').css('display', 'none');
                }

            });
            $('#selectedContacts div.unselected').each(function () {
                var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                    $(this).remove();
                }

            });
            if (selectedContactsCount == 0) {
                $('a[href="#selectedContacts"]').hide();
                $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
                $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
                WebTabClick();
            }
            else { $('a[href="#selectedContacts"]').show(); }


        }

    }
    else if (!$(this).hasClass('unselected')) {
        //secondary contacts           
        if ($('#selectedContacts div.unselected#' + this.id).hasClass('unselected')) {
            var mobileNo2 = $('#selectedContacts div.unselected#' + this.id).find("p:eq(1)").text();
            $('#selectedContacts div.unselected#' + this.id).removeClass('unselected').removeClass('selected').remove();
            var index = selectedMobileNos.indexOf(mobileNo2.slice(sliceNumber));

            selectedMobileNos.splice(index, 1);
            nameArray.splice(index, 1);
            listIdArray.splice(index, 1);
            contactIdArray.splice(index, 1);
            selectedContactsCount--;
        }
        //secondary contacts  end
        var boolResult = true;
        if ($('#selectedContacts div.unselected').length >= defaultLines - 1) {
            var minimumCount = "You can't select more than " + parseInt(defaultLines - 1) + " members";
            Notifier(minimumCount, 2);
            return false;
        }

        var listIdpush = $(this).attr('listid');
        var name = $(this).find("p:eq(0)").text();
        if (nameArray.length == 0)
        { $('#selectedContacts').html(''); }

        $(".contacts #profileDetails [p=" + conId + "]").addClass("selected");
        $('#selectedContacts div.unselected').each(function () {
            var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
            if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                Notifier("This Contact No was already Selected", 2);
                $('.contacts[id=' + conId + ']').removeClass("selected");
                boolResult = false;
            }

        });
        if (boolResult) {
            selectedMobileNos.push($.trim(mobileNo.replace(/\D/g, "").slice(sliceNumber)));
            nameArray.push(name);
            listIdArray.push(listIdpush);
            contactIdArray.push(conId);
            $(this).addClass('selected')
            $(this).find('.fa-check').css('display', 'block');
            selectedContactsCount = selectedContactsCount + 1;
            $('.count').html('(' + selectedContactsCount + ')');
            selectedContacts = addContact(name, mobileNo, conId, listIdpush, "unselected", "images/avatar-img-5.jpg", "block")
            $('#selectedContacts').append(selectedContacts);

        }

        if (selectedContactsCount == 0) {
            $('a[href="#selectedContacts"]').hide();
            $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
            $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
            WebTabClick();
        }
        else { $('a[href="#selectedContacts"]').show(); }
    }

});


// DatePicker Intitialization and setting for input Elemtn
$("#dtBox").DateTimePicker({
    isPopup: true,
    dateTimeFormat: "dd-MM-yyyy HH:mm:ss",
    defaultDate: myDateFormatter(mdate),
    formatHumanDate: function (clearDate) {
        return clearDate.day + ", " + clearDate.month + " " + clearDate.dd + ", " + clearDate.yyyy;
    }
});


//For All Web COntacts Method
function getAllWebLists(listId) {
    global_alphabetpageIndex = 0;
    webScroll = 0;
    webPageIndex++;

    if (webPageIndex == 1 || webPageIndex <= webPageCount) {
        if (webPageIndex == 1) {
            $.blockUI({ message: '<h4> Loading...</h4>' })
        }
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 2,
                pageIndex: webPageIndex,
                listId: listId,

            },
            method: "POST",
            async: false,
            dataType: "JSON",
            success: function (result) {
                jsonContacts = result.contactsData;
                webPageCount = result.pageCount;
                var contactList = "";
                webContactsData = "", selectedCount = 0;
                $('#grpCallWebContacts').html('');
                if (result.Data.length > 0) {
                    if (webPageIndex == 1) {
                        webContactsData += '<ul class="alphabet"><li id="specialCharSearch">#</li><li>A</li><li>B</li><li>C</li><li>D</li><li>E</li><li>F</li><li>G</li><li>H</li><li>I</li><li>J</li><li>K</li><li>L</li><li>M</li><li>N</li><li>O</li><li>P</li><li>Q</li><li>R</li><li>S</li><li>T</li><li>U</li><li>V</li><li>W</li><li>X</li><li>Y</li><li>Z</li></ul>';
                    }
                    for (var i = 0; i < result.Data.length; i++) {
                        selectedClass = "", faCheckPic = "", imgPath = "";

                        for (var j = 0; j < contactIdArray.length; j++) {

                            var responseContactId = $.trim(contactIdArray[j]);
                            var contactsIdNo = $.trim(result.Data[i].ContactId);

                            if (responseContactId == contactsIdNo) {
                                selectedClass += "selected"
                                faCheckPic += "block";
                                selectedCount++;
                            }
                            else {
                                selectedClass += "";
                                faCheckPic += "";
                            }


                        }

                        if ($.trim(result.Data[i].ImagePath) == null)
                            imgPath += 'images/avatar-img-5.jpg';
                        else if ($.trim(result.Data[i].ImagePath) != '')
                            imgPath += result.Data[i].ImagePath;
                        else
                            imgPath += 'images/avatar-img-5.jpg';
                        webContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + result.Data[i].ContactId + '" listId="' + result.Data[i].ListId + '">'; 
                        webContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                        webContactsData += '<div listId="0" id="profileDetails">';
                        if (result.Data[i].Name.length > 25)
                        { webContactsData += '<p name="' + result.Data[i].Name + '" title="' + result.Data[i].Name + '">' + result.Data[i].Name.substring(0, 25) + '..</p>'; }
                        else
                        { webContactsData += '<p name="' + result.Data[i].Name + '">' + result.Data[i].Name + '</p>'; }

                        webContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Data[i].MobileNumber + '</p>';
                        if ($.trim(faCheckPic).length == 0) {
                            faCheckPic = "none"
                        }
                        webContactsData += '</div><i class="fa fa-check select_check" style="display:' + faCheckPic + '"></i>';
                        webContactsData += '</div></div>';

                    }
                    $('#grpCallWebContacts').html('');
                    $('#grpCallWebContacts').html(webContactsData).show();
                    $('#selectAll').show();
                    if (selectedCount == $('#grpCallWebContacts .contacts').length)
                    { $('#selectAll').text("Unselect All"); }
                    else {
                        $('#selectAll').text("Select All");
                    }
                }
                else {

                    $('#grpCallWebContacts').html('No contacts found').show();
                    $('#selectAll').hide();
                }

                if (listClick == 0) {
                    if (result.ContactList.length > 0) {

                        var ContactsList = "";

                        for (var j = 0; j < result.ContactList.length; j++) {
                            if (result.ContactList[j].Source == 2) {

                                if (j == 0) {
                                    ContactsList += '<div class="list highlight">';
                                    listId = result.ContactList[j].Id;
                                    $('#webList').html(result.ContactList[j].ListName + '(' + result.ContactList[j].listCount + ')');
                                }
                                else {
                                    ContactsList += '<div class="list">';
                                }

                                ContactsList += '<div style="width: 100%; float: left;"><a data-target="#" href="javascript:void(0);" style="display: block;" id="' + result.ContactList[j].Id + '"  class="contactList" lname="' + result.ContactList[j].ListName + '" lcount="' + result.ContactList[j].listCount + '">' + result.ContactList[j].ListName + '(' + result.ContactList[j].listCount + ')</a>';
                                ContactsList += ' </div></div>';
                            }
                        }

                        $('#list-group').html(ContactsList);

                    }

                    else if (result.ContactList.length == 0) {
                        $("#selectAll").hide()

                        $('#grpCallWebContacts').html('No Lists Found').show();

                    }
                }
                setTimeout($.unblockUI, 200);

            },
            error: function (result) {
                Notifier("Something Went Wrong", 2);
                $.unblockUI();
            }
        });
    }
}

//For Mobile Contacts
function getMobileContacts(src) {
    pageIndex++;
    global_alphabetpageIndex = 0;
    mobileScroll = 0;
    if (pageIndex == 1 || pageIndex <= pageCount) {

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
                mobileContactsData = "";
                if (result.Success == true) {
                    jsonResponse = result;
                    if (result.contactDetails.length != 0) {
                        if (pageIndex == 1) {
                            mobileContactsData += '<ul class="alphabet"><li id="specialCharSearch">#</li><li>A</li><li>B</li><li>C</li><li>D</li><li>E</li><li>F</li><li>G</li><li>H</li><li>I</li><li>J</li><li>K</li><li>L</li><li>M</li><li>N</li><li>O</li><li>P</li><li>Q</li><li>R</li><li>S</li><li>T</li><li>U</li><li>V</li><li>W</li><li>X</li><li>Y</li><li>Z</li></ul>';
                        }
                        for (var j = 0; j < result.contactDetails.length ; j++) {
                            selectedClass = ""; faCheckPic = "", imgPath = "";
                            if ($.trim(result.contactDetails[j].source) == "1") {
                                for (var i = 0; i < contactIdArray.length; i++) {

                                    var responseContactId = $.trim(contactIdArray[i]);
                                    var contactsIdNo = $.trim(result.contactDetails[j].id);

                                    if (responseContactId == contactsIdNo) {
                                        selectedClass += "selected"
                                        faCheckPic += "block";
                                    }
                                    else {
                                        selectedClass += "";
                                        faCheckPic += "";
                                    }

                                }

                                if ($.trim(result.contactDetails[j].imagePath) == null) {
                                    imgPath += 'images/avatar-img-5.jpg'
                                }
                                else if ($.trim(result.contactDetails[j].imagePath) != '')
                                { imgPath += result.contactDetails[j].imagePath; }
                                else {
                                    imgPath += 'images/avatar-img-5.jpg'
                                }
                             mobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + result.contactDetails[j].id + '" listId="m1" >'; 
                                mobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                                mobileContactsData += '<div id="profileDetails">';
                                if (result.contactDetails[j].name.length > 25)
                                { mobileContactsData += '<p name="' + result.contactDetails[j].name + '" title="' + result.contactDetails[j].name + '">' + result.contactDetails[j].name.substring(0, 25) + '..</p>'; }
                                else { mobileContactsData += '<p name="' + result.contactDetails[j].name + '">' + result.contactDetails[j].name + '</p>'; }


                                if ($.trim(result.contactDetails[j].subContactDeatils).length != 0) {
                                    subContactsResponse.push(result.contactDetails[j].subContactDeatils);
                                    var selectedNumber = '';
                                    if (contactIdArray.indexOf(result.contactDetails[j].id) != -1)
                                        selectedNumber = (selectedMobileNos[contactIdArray.indexOf(result.contactDetails[j].id)]);
                                    else
                                        selectedNumber = result.contactDetails[j].mobileNumber;

                                    mobileContactsData += '<p id="phoneNumber' + result.contactDetails[j].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedNumber + '<div class="dropdown" style="float:right;margin-top: -3px 0 0;"> <a id="btnDropdown"  contactid=' + result.contactDetails[j].id + ' mobile=' + result.contactDetails[j].mobileNumber + ' class="dropdown-toggle" type="button" data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + result.contactDetails[j].mobileNumber + '" >';
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

                        $('#grpCallMobileContacts').append(mobileContactsData);
                        $(".selectedselected").removeClass('selectedselected').addClass('selected');//find anothe sol

                    }
                }
                if (result.Success == false)
                { }
            },
            error: function (result) {
                Notifier("Something Went Wrong", 2);
                //$.unblockUI();
            }


        });
    }
}


function manageWebContacts(mode, contactID, listId) {
    var imageName = G_img_name.replace("/ContactImages/", "")

    $.ajax({
        url: "/HandlersWeb/Contacts.ashx",
        data: {
            type: 4,
            mode: mode,
            contactID: contactID,
            name: $('#name').val(),
            listID: listId,
            mobileNumber: $('#mobileNumber').val(),
            prefix: "",
            cropX: parseInt(roundCropX),
            cropY: parseInt(roundCropY),
            cropW: parseInt(roundCropW),
            cropH: parseInt(roundCropH),
            imgName: imageName
        },
        dataType: "json",
        method: "POST",
        success: function (result) {
            $('#successMessage,#errorMessage').html("");
            if (result.Status == 1) {
                $('#successMessage').html(result.Message);
                if (result.Message != 'Contact Already Exist in List') {
                    if (parseInt(listId) == parseInt($('.list.highlight  .contactList').attr("id"))) {
                        name = $('#name').val();
                        if ($.trim(imageName).length == 0)
                            imageName = "images/avatar-img-5.jpg";
                        else
                            imageName = "ContactImages/" + imgName;

                        var contactsAdd = addContact($('#name').val(), $('#mobileNumber').val(), result.ContactId, listId, "", imageName, "none");
                        $('#grpCallWebContacts').append(contactsAdd);

                    }
                    $('.contactList').each(function (e) {
                        var contactLis = $('.contactList')[e];
                        if ($(contactLis).attr("id") == listId) {
                            var listName = $(contactLis).attr("lname");
                            var lcount = parseInt($(contactLis).attr("lcount")) + 1;
                            $(contactLis).html(listName + '(' + parseInt(lcount) + ')');
                        }
                    });
                    $('#contactsModal').modal("hide");
                }
                else {
                    $('#contactsModal').modal("hide");
                    Notifier(result.Message, 2);
                }
            }

        },

        error: function (img) { Notifier("Something Went Wrong", 2); }
    });
}

function uploadContactsThroughExcel(fileName) {
    $.ajax({
        url: '/ExcelUpload.ashx',
        type: 'post',
        data: { type: 2, fileName: fileName },
        dataType: 'json',
        success: function (result) {
            $('#successMessage,#errorMessage').html("");
            if (result.Status == 1) {
                setTimeout(function () {
                    $('#successMessage').html(result.Message)
                    window.location.reload();
                }, 4000);
            }
            else {
                setTimeout(function () {
                    $('#errorMessage').html(result.Message)
                    window.location.reload();
                }, 4000);
            }
        },
        error: function (result) {
            Notifier("Something Went Wrong", 2);
        }
    });

}


//WebContacts Offset Scroll
function webContactsOffsetScroll(listId) {
    webPageIndex++;
    if (webPageIndex == 1 || webPageIndex <= webPageCount) {
        if (webPageIndex == 1) {
            $.blockUI({ message: '<h4> Loading...</h4>' })
        }
        $.ajax({
            url: '/HandlersWeb/Contacts.ashx',
            type: 'post',
            //async: false,
            dataType: 'json',
            data: {
                type: 2, listId: listId,
                pageIndex: webPageIndex,
            },
            success: function (result) {
                webContactsData = "";
                webContactsData = $('#grpCallWebContacts').html();

                for (var i = 0; i < result.Items.length; i++) {

                    selectedClass = ""; faCheckPic = ""; imgPath = "";

                    for (var j = 0; j < contactIdArray.length; j++) {
                        if ((listIdArray[j]) != 0) {
                            var responseContactId = $.trim(contactIdArray[j]);
                            var contactsIdNo = $.trim(result.Items[i].ContactId);

                            if (responseContactId == contactsIdNo) {
                                selectedClass += "selected"
                                faCheckPic += "block";
                            }
                            else {
                                selectedClass += "";
                                faCheckPic += "";
                            }
                        }
                    }

                    if ($.trim(result.Items[i].ImagePath) == null)
                        imgPath += 'images/avatar-img-5.jpg';
                    else if ($.trim(result.Items[i].ImagePath) != '')
                        imgPath += result.Items[i].ImagePath;
                    else
                        imgPath += 'images/avatar-img-5.jpg';
                 
                        webContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + result.Items[i].ContactId + '" listId="' + listId + '">';
                                        webContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    webContactsData += '<div listId="0" id="profileDetails">';
                    if (result.Items[i].Name.length > 25)
                    { webContactsData += '<p name="' + result.Items[i].Name + '" title="' + result.Items[i].Name + '">' + result.Items[i].Name.substring(0, 25) + '..</p>'; }
                    else { webContactsData += '<p name="' + result.Items[i].Name + '">' + result.Items[i].Name + '</p>'; }

                    webContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Items[i].MobileNumber + '</p>';
                    if ($.trim(faCheckPic).length == 0) {
                        faCheckPic = "none";
                    }

                    webContactsData += '</div><i class="fa fa-check select_check" style="display:' + faCheckPic + '"></i>';
                    webContactsData += '</div>';

                }
                setTimeout($.unblockUI, 200);
                $('#grpCallWebContacts').html(webContactsData);

            },
            error: function (result) { Notifier("Something Went Wrong", 2); $.unblockUI(); }
        });
    }
}


// Alphabet CLick Funtionality
$(document).on('click', '.alphabet li', function (e) {
    e.preventDefault();
    $('#search-input').val('');
    var clickCounter = $(this).data('clickCounter') || 0;
    if (clickCounter == 0) {
        clickCounter += 1;
        global_searchndex = 0;
        $(this).data('clickCounter', 0);
        global_alphabetMobileContactsData = "";
    }

    //alphabetPageIndex++;
    global_alphabet = alphabet;
    global_alphabetMobileContactsData = "";
    var alphabet = $(this).text().toLowerCase();
    alphabetOrSerachClick(global_searchndex, alphabet);
    e.stopPropagation();
});


//Search Button Fnct
$(document).on('click', '#search', function (e) {
    e.preventDefault()
    global_searchndex = 0;
    var alphabet = $('#search-input').val().toLowerCase();
    //if ($.trim(alphabet).length == 0) {
    //}

    global_alphabet = alphabet;
    global_alphabetMobileContactsData = "";
    alphabetOrSerachClick(global_searchndex, alphabet);
    e.stopImmediatePropagation();
});


// FOR both Mobile and Web Active Tab Search 
function alphabetOrSerachClick(global_searchndex, alphabet) {
    var activeTab = $('ul#myTabList li.active').find("a").attr("href");
    var sourceValue = 0;
    var spMode = 2;
    if (alphabet == '#')
        spMode = 3;

    if (activeTab == "#grpCallWebContacts") {
        sourceValue = 2;
        if ($('.list.highlight .contactList').length == 0)
        { $('#grpCallWebContacts').html('No Contacts Found'); }
        else {
            var listIdSearch = $('.list.highlight .contactList').attr("id");
            alphabetWebClick++;
            alphabeticalContactSorting(sourceValue, global_searchndex, alphabet, listIdSearch, spMode);
        }
    }
    else if (activeTab == "#grpCallMobileContacts") {
        alphabetMobileClick++;
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
            divSelected += addContact(nameArray[i], selectedMobileNos[i], contactIdArray[i], listIdArray[i], "unselected", "images/avatar-img-5.jpg", "block")
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
                $.unblockUI();
            }

        });
    }
}


//For Final Save Ajax Functionality
$('#saveGrpCall').click(function () {
    $.blockUI({ message: '<h4> Saving Changes...</h4>' })
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
    var webListId = '';
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
        if ($(this).hasClass('unselected')) {
            memberName = $(this).find('p:eq(0)').attr('name');
            memberMobile = $(this).find('p:eq(1)').text();
            if ($(this).attr('listId') == "m1") {
                listIds = "0";
            }
            else {
                listIds = $(this).attr('listId');
                webListId = 0;
            }
            participantsInEditCall += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false","ListId":"' + listIds + '"}' + ",";

            if (memberMobile.slice(sliceNumber) == hostNumber.slice(sliceNumber)) {
                Notifier("Please remove your number from Group", 2);
                boolValue = false;
                $.unblockUI();
                return false;
            }
        }

    });
    if (boolValue) {
        var participantsLenght = $('#selectedContacts').find("div.unselected").length;

        if ($.trim($('#conf_name').val()).length == 0) {
            Notifier("Please Enter Group Name", 2);
            $.unblockUI();
            return false;
        }

        if ($.trim($('#conf_datetime').val()).length == 0) {
            Notifier("Please Select Schedule DateTime", 2);
            $.unblockUI();
            return false;
        }

        if (!(onlyDialIn == 1 && isAllowNonMems == 1)) {
            if ($('#selectedContacts .contacts').length == 0) {
                Notifier("Please choose participants to make a group call", 2);
                $.unblockUI();
                return false;
            }
        }

        if (editType == 0) {
            var selectedDate = "";
            var date = new Array();
            selectedDate = new Date(schduled_datetime[0] + " " + schduled_datetime[1]);
            if (isDateTab != 0) {
                if (Date.parse(selectedDate) < Date.parse(minDate)) {
                    Notifier("Please select 10 mins from now", 2);
                    $.unblockUI();
                    return false;
                }
            }

        }

        $('#grpCallErrorMessage').html('').hide()
        response = '{"Type":"' + editType + '","IsMuteDial":"' + isMuteDial + '","SchType":"' + schType + '","WebListIds":"' + webListId + '","Reminder":"30","IsOnlyDialIn":"' + onlyDialIn + '","IsAllowNonMembers":"' + isAllowNonMems + '","OpenLineBefore":"' + isLineOpen + '","WeekDays":"' + weekDays + '","GroupID":"' + grpID + '","GroupCallName":"' + grpTalkName + '","SchduledTime":"' + schduled_datetime[1] + '","SchduledDate":"' + schduled_datetime[0] + '","managerMobile":' + managerMobile + ',"Participants":[' + participantsInEditCall.replace(/,(?=[^,]*$)/, '').replace('/', '//').replace('\\', '\\\\') + ']}';
        editGroupCall(response);
    }
})
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
            Notifier("Group Call was Edited Successfully", 1);
            window.location.href = "/Mygrptalks.aspx";

        },
        error: function (result) {
            Notifier("Failed To Edit your Call", 2);
        },
    });
}

$('#selectAll').click(function () {
    var selectAllText = $.trim($('#selectAll').text());
    var booleanAdd = true; var number = 0;

    if (selectAllText == "Select All") {
        var duplicateArray = new Array();
        var str = '';
        $('#grpCallWebContacts .contacts').each(function () {
            if (!$(this).hasClass('selected')) {

                if ($('#selectedContacts div.unselected').length >= defaultLines - 1) {
                    var minimumCount = "You can't select more than " + parseInt(parseInt(defaultLines) - 1) + " members";
                    selectBtnText = "Unselect All";
                    Notifier(minimumCount, 2);
                    return false;
                }

                booleanAdd = true;
                var listIdPush = $(this).attr('listId');
                var contactId = $(this).attr('id');
                var mobileNumberVal = $(this).find("p:eq(1)").text();
                var name = $(this).find("p:eq(0)").attr('name');
                $('#selectedContacts div.unselected').each(function () {
                    var selectedTabMobileNos = $(this).find("p:eq(1)").text();
                    if (mobileNumberVal.slice(sliceNumber) == selectedTabMobileNos.slice(sliceNumber)) {
                        //alert(mobileNumberVal + " is already selected");
                        duplicateArray.push($(this).find("p:eq(0)").text());
                        duplicateArray.push(selectedTabMobileNos);
                        booleanAdd = false;
                        number--;
                    }
                });
                if (booleanAdd) {
                    selectedMobileNos.push(mobileNumberVal.replace(/\D/g, "").slice(sliceNumber));
                    listIdArray.push(listIdPush);
                    nameArray.push(name);
                    contactIdArray.push(contactId);
                    $(this).removeClass('selected');
                    $(this).addClass('selected');
                    $(this).find('.fa-check').css('display', 'block');
                    selectedContactsCount++;
                    $('.count').html('(' + selectedContactsCount + ')');
                    selectedContacts = addContact(name, mobileNumberVal, contactId, listIdPush, "unselected", "images/avatar-img-5.jpg", "block")
                    $('#selectedContacts').append(selectedContacts);
                }
            }
        });

        if ($('#grpCallWebContacts .selected').length == 0) {
            selectBtnText = "Select All"
        }
        else if ($('#grpCallWebContacts .selected').length == ($('#grpCallWebContacts .contacts').length + number)) {
            selectBtnText = "Unselect All";
        }
        else { selectBtnText = "Select All"; }
        if (selectedContactsCount == 0) {
            $('a[href="#selectedContacts"]').hide();
            $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
            $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
            WebTabClick();
        }
        else { $('a[href="#selectedContacts"]').show(); }

        if (duplicateArray.length > 0) {
            $("#duplicateContacts").modal('show');
            for (var i = 0; i < duplicateArray.length / 2; i++) {
                str += '<div class="col-sm-6"><div class="dbl-contacts"><label>' + duplicateArray[i * 2] + ' -';
                str += '<span>' + duplicateArray[i * 2 + 1] + '</span></div></div>';
            }
            $('#contatcsName').html(str);
        }
    }
    else if (selectAllText == "Unselect All") {
        selectedContacts = "";

        global_listId = "";
        $('#grpCallWebContacts .contacts').each(function () {
            if ($(this).hasClass('selected')) {
                listId = $(this).attr('listId');
                contactId = $(this).attr('id');
                var mobileNo = $(this).find("p:eq(1)").text();
                var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
                selectedMobileNos.splice(index, 1);
                nameArray.splice(index, 1);
                contactIdArray.splice(index, 1);
                listIdArray.splice(index, 1);
                $(this).removeClass('selected')
                $(this).find('.fa-check').css('display', 'none');
                selectedContactsCount = selectedContactsCount - 1;
                $('.count').html('(' + selectedContactsCount + ')');
                $('#selectedContacts div[id="' + contactId + '"]').remove()
                if (selectedContactsCount == 0) {
                    $('a[href="#selectedContacts"]').hide();
                    $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
                    $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
                    WebTabClick();
                }
                else { $('a[href="#selectedContacts"]').show(); }
            }
        });
        selectBtnText = "Select All"
    }
    if ($.trim(selectBtnText) != "") {
        $('#selectAll').text(selectBtnText);
    }


});

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
                selectedClass = "", faCheckPic = "", imgPath = "";
                var contactsIdNo = "";

                for (var j = 0; j < contactIdArray.length; j++) {
                    var responseContactId = $.trim(contactIdArray[j]);
                    if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                        contactsIdNo = $.trim(contactResult.contactDetails[contactRows].id);
                    }
                    else {
                        contactsIdNo = $.trim(contactResult.contactDetails[contactRows].ContactId);
                    }

                    if (responseContactId == contactsIdNo) {
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
                    if (contactResult.contactDetails[contactRows].name.length > 25) {
                        global_alphabetMobileContactsData += '<div title="' + contactResult.contactDetails[contactRows].name + '" class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + contactResult.contactDetails[contactRows].id + '" listId="m1" >';
                    }
                    else {
                        global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + contactResult.contactDetails[contactRows].id + '" listId="m1" >';
                    }
                    global_alphabetMobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    global_alphabetMobileContactsData += '<div id="profileDetails">';
                    if (contactResult.contactDetails[contactRows].name.length>25)
                        global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '" title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '..</p>';
                    else
                        global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name + '</p>';
                 

                    // global_alphabetMobileContactsData += '<p id="subphoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
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
                    global_alphabetMobileContactsData += '</div>';
                    mobileScroll = 1;

                }
                else {
                    webScroll = 1
                  global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + contactResult.contactDetails[contactRows].ContactId + '" listId="' + listId + '" >'; 
                    global_alphabetMobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    global_alphabetMobileContactsData += '<div  id="profileDetails">';
                    if (contactResult.contactDetails[contactRows].name.length > 25) {
                        global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '" title="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '..</p>';
                    }
                    else {
                        global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name + '</p>';
                    }

                    global_alphabetMobileContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '</p>';
                    global_alphabetMobileContactsData += '</div>';

                }
                if ($.trim(faCheckPic).length == 0) {
                    faCheckPic = "none";
                }
                global_alphabetMobileContactsData += '<i class="fa fa-check select_check" style="display:' + faCheckPic + '"></i></div>';
            }
        }
        else {

            global_alphabetMobileContactsData += "No Data Found With this Criteria";
        }
        if (sourceValue == 2) {
            $('#grpCallWebContacts').show();
            $('#grpCallWebContacts').html('');
            $('#grpCallWebContacts').html(global_alphabetMobileContactsData);
        }
        else {
            $('#grpCallMobileContacts').show();
            $('#grpCallMobileContacts').html('');
            $('#grpCallMobileContacts').html(global_alphabetMobileContactsData);
        }

    }

    if (contactResult.Success == false) {
        global_alphabetMobileContactsData += "No Data Found With this Criteria";
        if (sourceValue == 2) {
            $('#grpCallWebContacts').show();
            $('#grpCallWebContacts').html('');
            $('#grpCallWebContacts').html(global_alphabetMobileContactsData);
        }
        else {
            $('#grpCallMobileContacts').show();
            $('#grpCallMobileContacts').html('');
            $('#grpCallMobileContacts').html(global_alphabetMobileContactsData);
        }
    }
}


function addContact(nameAdd, mobileNoAdd, contactIdAdd, listIdAdd, selectedClassAdd, imgAdd, display) {
    var addcontact = "";
    $('#contactsModal').modal("hide");
    addcontact += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClassAdd) + '" id="' + contactIdAdd + '" listId="' + listIdAdd + '">'; 
    addcontact += '<div id="profilePic"><img alt="default user" src="' + imgAdd + '"></div>';
    addcontact += '<div listId="' + listIdAdd + '" id="profileDetails">';
    if (nameAdd.length > 25) {
        addcontact += '<p name="' + nameAdd + '" title="' + nameAdd + '">' + nameAdd.substring(0, 25) + '..</p>';
    }
    else { addcontact += '<p name="' + nameAdd + '">' + nameAdd + '</p>'; }
    addcontact += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNoAdd + '</p>';
    addcontact += '</div><i class="fa fa-check select_check" style="display:' + display + '"></i>';
    addcontact += '</div>';
    return addcontact;
}

$('#plusSymbol').click(function () {
    $('.jcrop-circle-demo').css('display', 'none');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
    $('#errorDescForName,#errorDescForMobile,#successMessage,#errorMessage,#errorDescForWebList').html(''); $('#name, #mobileNumber,#newWebList').val('');
    ListNames();
    $('#ddlWebList').val(0);
    $('#newWebList').removeAttr("disabled");
});


function sliceNumberFn() {
    var countryId = $('#countryID').val();
    if (countryId == 108)
        sliceNumber = -10;
    else if (countryId == 19)
        sliceNumber = -8;
}

function WebTabClick() {
    $('#grpCallMobileContacts').hide();
    if ($('#grpCallMobileContacts').parent('.slimScrollDiv').length != 0)
    { $('#grpCallMobileContacts').parent().hide(); }
    if ($('#selectedContacts').parent('.slimScrollDiv').length != 0)
    { $('#selectedContacts').parent().hide(); }
    $('#grpCallWebContacts,.alphabet').show();
    $('#grpCallWebContacts,#list-group').parent().show();
    $('#contactList').show();
    $('.addNewContact,#plusSymbol').show();
    $('.list').show();

    $('#listTitleName').text('Select Contacts')
    $('#selectedContacts').hide();

    $('#alphabetMobileContacts,#alphabetWebContacts').hide();
    $('#grpCallWebContacts').addClass("active in");
    $('#grpCallMobileContacts,#selectedContacts').removeClass("active in");
    $('#phoneContactsDisplay').hide();

}
function addZero(i) {
    if (i < 10)
        i = "0" + i;

    return i;
}

function openLineSwitchTimeCheck() {

    var txtDate = $('.date-picker').val().toString();
    var scheduleDateTime = "";
    scheduleDateTime = txtDate.split("at");
    currentDate = scheduleDateTime[0];
    currentTime = scheduleDateTime[1];

    var minDate = new Date();
    minDate.setMinutes(minDate.getMinutes() + 30);
    var selectedDate = "";
    selectedDate = new Date(scheduleDateTime[0] + " " + scheduleDateTime[1]);
    var repeats = $('.weekDay.active').length;
    if (parseInt(repeats) == 0) {

        if (Date.parse(selectedDate) < Date.parse(minDate)) {
            $('#openlineswitch').attr("disabled", false);
            $('#openlineswitch').attr("checked", false);
            Notifier('Please select 30 mins from now', 2);
            return false;
        }
        else {
            $('#openlineswitch').attr("disabled", false);
        }
    }
    else {
        $('#openlineswitch').attr("disabled", false);
    }
}

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


function load_crop_thumb() {

    $('#webContactProfile').Jcrop({
        bgOpacity: 0.5,
        bgColor: 'white',
        allowResize: true,
        boxWidth: 280,
        boxHeight: 200,
        selectionComponent: CircleSel,
        applyFilters: ['constrain', 'extent', 'backoff', 'ratio', 'round'],
        aspectRatio: 1,
        setSelect: [100, 100, 100, 100],
        handles: ['n', 's', 'e', 'w'],
        dragbars: [],
        borders: [],
        onChange: storeCoords,
        onSelect: storeCoords

    }, function () {
        this.container.addClass('jcrop-circle-demo');
    });
}

function storeCoords(c) {
    roundCropX = c.x;
    roundCropY = c.y;
    roundCropW = c.w;
    roundCropH = c.h;
}

function newListAddContact(mode, contactID, newList, name, mobileNumber) {
    var imageName = G_img_name.replace("/ContactImages/", "")

    if (($("a[lname='" + newList + "']").hasClass("contactList")) == false)
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 4,
                mode: 6,
                contactID: contactID,
                name: name,
                listID: 0,
                listName: newList,
                mobileNumber: mobileNumber,
                prefix: "",
                cropX: parseInt(roundCropX),
                cropY: parseInt(roundCropY),
                cropW: parseInt(roundCropW),
                cropH: parseInt(roundCropH),
                imgName: imageName
            },
            dataType: "json",
            method: "POST",
            success: function (result) {
                $('#successMessage,#errorMessage').html("");
                if (result.Status == 1) {
                    var highlightClass = '';
                    $('#successMessage').html(result.Message);
                    Notifier(result.Message, 1);
                    $('#contactsModal').modal("hide");
                    if ($.trim($('#list-group').html()) == 'No Lists Found') {
                        $('#list-group').html('');
                        highlightClass = 'highlight';
                    }
                    $('#list-group').append('<div class="list ' + highlightClass + '"><div style="width: 100%; float: left;"><a data-target="#" href="javascript:void(0);" style="display: block;" id="' + result.List[0].ListId + '" class="contactList" lname="' + newList + '" lcount="1">' + newList + '(' + result.List[0].ListCount + ')</a> </div></div>')
                    if ($.trim(imageName).length == 0) {
                        imageName = "images/avatar-img-5.jpg";
                    }
                    else { imageName = "ContactImages/" + imgName; }
                    if (parseInt(result.List[0].ListId) == parseInt($('.list.highlight .contactList').attr("id"))) {
                        $('.contactList').click();
                    }
                }
                else {
                    $('#errorMessage').html(result.Message);
                    setTimeout(function () {
                        window.location.reload();
                    }, 1000);
                }
            },
            error: function (img) { alert("Something Went Wrong"); }
        });
    else
        Notifier('This Web list name is already existed', 2);
}

function tabClickFetchContacts() {
    if (alphabetMobileClick > 0) {

        $('#grpCallMobileContacts').html('');
        pageIndex = 0;
        $('#grpCallMobileContacts').slimScroll({ destroy: true });
        $('#grpCallMobileContacts').slimScroll({
            allowPageScroll: false,
            height: '250',
        });
        getMobileContacts(1);
        alphabetMobileClick = 0;
    }
    if (alphabetWebClick > 0) {

        $('#grpCallWebContacts').html('');
        webPageIndex = 0;
        $('#grpCallWebContacts').slimScroll({ destroy: true });
        $('#grpCallWebContacts').slimScroll({
            allowPageScroll: false,
            height: '250',

        });
        getAllWebLists($('.list.highlight .contactList').attr("id"));
        alphabetWebClick = 0;
        if (nameArray.length == 0)
            $('#selectedContacts').html('');

    }
    $('#search-input').val('');
    selectedContactsTabClick('');
}


function ListNames() {
    var weblistNames = "";
    $.ajax({
        url: '/HandlersWeb/Contacts.ashx',
        type: 'post',
        async: false,
        dataType: 'json',
        data: {
            type: 2, listId: 0,
            pageIndex: 1,
        },
        success: function (result) {


            if (result.ContactList.length > 0) {

                for (var j = 0; j < result.ContactList.length; j++) {
                    if (j == 0)
                        weblistNames = '<option value="0">Select List</option>';

                    weblistNames += '<option value="' + result.ContactList[j].Id + '">' + result.ContactList[j].ListName + '</option>';
                }
                $('#ddlWebList').html(weblistNames);

            }
            else {
                $('#ddlWebList').html('<option value="0">No Lists Found</option>');
            }

            setTimeout($.unblockUI, 200);
        },
        error: function (jqXHR, exception) { }
    });

}

function MobileValidator(mobile) {

    var ret = 1;
    var filter = /^[0-9]*$/;
    if (!(filter.test(mobile))) {
        ret = 0;
    }
    if (mobile.length > 10) {
        if (mobile.length == 11) {
            if (mobile.match("^0")) {
                if (mobile.charAt(1) == 7 || mobile.charAt(1) == 8 || mobile.charAt(1) == 9) {
                    ret = 1;
                } else {
                    ret = 0;
                }
            } else {
                ret = 0;
            }

        } else if (mobile.length == 12) {
            if (mobile.match("^91")) {
                if (mobile.charAt(2) == 7 || mobile.charAt(2) == 8 || mobile.charAt(2) == 9) {
                    ret = 1;
                } else {
                    ret = 0;
                }
            } else {
                ret = 0;
            }

        } else {
            ret = 0;
        }



    }
    else if (mobile.length < 10) {
        ret = 0;
    } else {
        if (mobile.length == 10) {
            if (mobile.charAt(0) == 7 || mobile.charAt(0) == 8 || mobile.charAt(0) == 9) {
                ret = 1;
            } else {
                ret = 0;
            }
        }

    }
    return ret;
}