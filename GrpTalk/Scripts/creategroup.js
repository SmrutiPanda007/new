/// <reference path="../assets/global/plugins/jquery-1.11.0.min.js" />
/// <reference path="../assets/global/plugins/jquery-1.11.0.min.js" />
/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
var listId = 0, mobileContactsData = "", defaultLines = 0;
var mobileContactsAppend = "", mobileNumberVal = "", nameSplit = [];
var subContactsResponse = new Array();
var searchValue = "", alphabetSort = "", alphabetPageIndex = 0, pageIndex = 0;
var pageCount = 0, contactId = 0, countContacts = 0, grpTalkName = "", webListsAppend = "";
var webPageCount = 0, global_alphabetMobileContactsData = "", selectedContacts = "", currentTime = "", currentDate = "", response = "", webListContacts = "";
var jsonContacts = new Array(), participants = new Array(), contactIDs = new Array(), global_listId = "", selectedContactsArray = "", listClick = 0;
var global_alphabetMobileContactsData = "", global_searchndex = 0, plugoutput = '', txtDate = '', memberName = "", memberMobile = "", resStr = "", response = "";
var listIds = new Array();
var contactIds = new Array();
var particpantsMobileArray = new Array();
var partcipants = "", mobileNosInSelectedTab = "", response = "";
var participantsArray = [];
var date = new Date();
var mdate = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + ' ' + date.getHours() + ':' + date.getMinutes() + ':00';
var firstClick = 0; faCheckPic = "", alphabetClick = 0, selectedTabClick = 0;
var selectAllListId = 0, selectedClass = "", mobileScroll = 0, webScroll = 0; gName = "", selectedTabSearch = 0;
var SelectedContactsTabArray = new Array();
var selectedMobileNos = new Array();
var nameArray = new Array();
var memberImageArray = new Array();
var participantLength = 0;
var scheduleClick = 0;
var sliceNumber = 0, alphabetPageCount = 0;
var global_alphabet = '';
var global_alphabetpageIndex = 0;
var liClick = 0;

$(document).ready(function () {
    sliceNumberFn();
    $('#phoneContactsDisplay,.phnCntcts').hide();
    $('.searchByMember,#grpTalkName').val('');
    $('#selectAll,#unSelectAll').hide();
    $('#successMessage,#errorMessage').html("");
    $('#phoneContactsDisplay,.phnCntcts').show();


    getMobileContacts(1);       //-----Fetching All Mobile Contacts

    if (countContacts == 0) {
        $('a[href="#selectedContacts"]').hide();
        $('a[href="#selectedContacts"]').parent('li').removeClass('active');
        $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
        MobileTabClick();
    }
    else { $('a[href="#selectedContacts"]').show(); }





    //--------------------------------------Scroll event on Mobile contacts
    $('#grpCallMobileContacts').scroll(function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 1) {
            if (global_alphabetpageIndex == 0) {
                getMobileContacts(1);

            }
            else {
                alphabeticalContactSorting(1, global_alphabetpageIndex, global_alphabet, 0, 2)
            }
        }
    });


    //--------------------------------------Tabs click
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
            if ($.trim(participantLength) == 0) {
                $('.list').hide();
            }
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


    //--------------------------------------Filtering contacts by alphabets

    $(document).on('click', '.alphabet li', function (e) {
        $('#search').css("border-color", "none");
        $('#search-input').val('');
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


    $('#search-input').keyup(function (e) {
        e.preventDefault();
        if (e.which != 1 && e.which != 17 && e.which != 20 && e.which != 16) {
            if ($(this).val().length >= 3) {
                $('#search').click();
            }
            else {
                if ($(this).val().length == 0) {
                    $('#grpCallMobileContacts').html('');
                    $('#search').click();
                    alphabetClick++;
                }
            }
        }

        e.stopPropagation();
    });
    $('#search-input').click(function (e) {
        e.stopPropagation();
        $('#search').css("border-color", "#66afe9");
    })

    //--------------------------------------filtering contacts through search filter
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

    function selectedContactsTabClick(alphabet) {
        var nameMatchArray = ""; var divSelected = "";
        var mobileMatchArray;

        for (var i = 0; i < nameArray.length; i++) {

            var profileName = nameArray[i].toLowerCase();
            var mobileNumber = selectedMobileNos[i];
            var myregex = new RegExp(alphabet.toLowerCase());
            nameMatchArray = myregex.exec(profileName);
            mobileMatchArray = myregex.exec(mobileNumber);
            if (nameMatchArray != null || mobileMatchArray != null) {
                divSelected += '<div class="contacts margin-right-5 margin-bottom-5 unselected" id="0" listId="0"><div id="profilePic"><img alt="default user" src="' + memberImageArray[i] + '"></div><div id="profileDetails">';
                if (nameArray[i].length > 17) {
                    divSelected += '<p name="' + nameArray[i] + '" title="' + nameArray[i] + '">' + nameArray[i].substring(0, 14) + '...</p><p><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedMobileNos[i] + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>';
                }
                else { divSelected += '<p name="' + nameArray[i] + '">' + nameArray[i] + '</p><p><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedMobileNos[i] + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>'; }
            }
        }
        if ($.trim(divSelected) == "")
        { divSelected = "No Contacts Found"; }
        $('#selectedContacts').html(divSelected);

    }


    //--------------------------------------Sorting with Special Character SEarch
    $(document).on('click', '#specialCharSearch', function (e) {
        $('#search').css("border-color", "none");
        var clickCounter = $(this).data('clickCounter') || 0;
        var alphabet = $(this).text().toLowerCase(); alphabetClick++;

        var sourceValue = 0;
        global_alphabetMobileContactsData = "";

        sourceValue = 1;
        $('#grpCallMobileContacts').parent().show();
        $('#grpCallMobileContacts').show();
        global_alphabetMobileContactsData
        subContactsResponse = new Array();
        specialCharSearch(sourceValue, listId);



    });

    //$(document).on("click", "#btnDropdown", function (e) {
    //    var mobileNumber = $(this).attr("mobile");

    //    var contactId = $(this).attr("contactid");
    //    mobileNumberVal = mobileNumber;
    //    //var select = $("#mobileNumber" + mobileNumber);
    //    //var subContactsHtml = "";
    //    //subContactsHtml += "<li><a id=primary-" + mobileNumber + " href='#'>" + mobileNumber + "</a></li>";
    //    //for (var i = 0; i < subContactsResponse.length; i++) {
    //    //    for (var j = 0; j < subContactsResponse[i].length; j++) {
    //    //        if (contactId == subContactsResponse[i][j].subContactId) {
    //    //            var anchorId = subContactsResponse[i][j].contactType + '-' + subContactsResponse[i][j].contactNumber;
    //    //            subContactsHtml += "<li><a id=" + anchorId + " href='#'>" + subContactsResponse[i][j].contactNumber + "</a></li>";
    //    //        }

    //    //    }

    //    //}
    //    //select.html(subContactsHtml);
    //    //e.stopPropagation();
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
            //$('#selectedContacts div.unselected').each(function () {
            //    if (this.id == contactId2) {
            //        $(this).removeClass('unselected').removeClass('selected');
            //        $(this).hide();
            //    }
            //});
            //alert($('.unselected.selected#' + contactId2).attr('id'));
            //$('.unselected#2089898').removeClass('unselected');
            //$('.unselected').each(function () {
            //    if (this.id == contactId2) {
            //        $(this).removeClass('selected');
            //        $(this).removeClass('unselected');
            //    }
            //});
           // $('div .unselected #' + contactId2).removeClass('unselected');
        }
        else
            $('#'+ contactId2).addClass('selected');
        var classList = $(this).attr('class');
        classList = (classList.substring(13, classList.length))
        $(this).parents('.dropdown').prev().html('<i class="fa fa-mobile" aria-hidden="true"></i> ' + $(this).children().text());      
    });

    //--------------------------------------Click  event on participants selection
    var isSelected = 0;
    $(document).delegate('.contacts', 'click', function () {
        var contactId=($(this).attr('id'));
        memberName = ""; memberMobile = ""; nameSplit = [];

        memberName = $(this).find('p:eq(0)').text();
        var imgPath = $(this).find('#profilePic img').attr("src");
        if ($(this).hasClass('selected') && !$(this).hasClass('unselected')) {
            nameSplit = [];

            var mobileNo = $(this).find("p:eq(1)").text();
            var conId = $(this).attr("id");
            if ($(this).hasClass('selected')) {

                var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
                selectedMobileNos.splice(index, 1);
                nameArray.splice(index, 1)
                memberImageArray.splice(index, 1);

                countContacts = countContacts - 1;
                $('.count').html('(' + countContacts + ')');
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
                    if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                        $(this).remove();
                    }

                });



            }
            if ($.trim(participantLength) == 0) {
                var grpName = $('#grpTalkName').val().toUpperCase();
            }
            if (countContacts == 0) {
                $('#grpTalkName').val('');

            }
            else {

                grpName = grpName.replace(memberName.substring(0, 2).toUpperCase(), '');
                $('#grpTalkName').val(grpName)

            }

            if (countContacts == 0) {
                $('a[href="#selectedContacts"]').hide();
                $('a[href="#selectedContacts"]').parent('li').removeClass('active');
                $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
                MobileTabClick();
            }
            else { $('a[href="#selectedContacts"]').show(); }
        }
        else if (!$(this).hasClass('unselected')) {
            //secondary contacts           
            if ($('#selectedContacts div.unselected#' + this.id).hasClass('unselected')) {
                countContacts--;
                $('#selectedContacts div.unselected#' + this.id).removeClass('unselected').removeClass('selected').hide();                
            }
            //secondary contacts  end
            if ($.trim(participantLength) != 0) {
                if (parseInt($('#selectedContacts div.unselected').length) + parseInt(participantLength) >= defaultLines - 1) {
                    var minimumCount = "You can't select more than " + parseInt(parseInt(defaultLines) - 1) + " members";
                    Notifier(minimumCount, 2);
                    return false;
                }
            }
            else {
                if ($('#selectedContacts div.unselected').length >= defaultLines - 1) {
                    var minimumCount = "You can't select more than " + parseInt(parseInt(defaultLines) - 1) + " members";
                    Notifier(minimumCount, 2);
                    return false;
                }
            }

            var boolResult = true;
            if (nameArray.length == 0)
            { $('#selectedContacts').html(''); }

            var conId = $(this).attr("id");
            listId = $(this).attr('listId');
            var mobileNo = $(this).find("p:eq(1)").text();
            var name = $(this).find("p:eq(0)").attr('name');

            $(".contacts #profileDetails [p=" + conId + "]").addClass("selected");
            $('#selectedContacts div.unselected').each(function () {                
                var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                    //alert("This Contact No was already Selected");
                    Notifier("This Contact No was already Selected", 2);
                    $('.contacts[id=' + conId + ']').removeClass("selected");
                    boolResult = false;
                }
               
            });
            if (boolResult) {
                selectedMobileNos.push($.trim(mobileNo.slice(sliceNumber)));
                nameArray.push(name);
                memberImageArray.push(imgPath);
                $(this).addClass('selected')
                $(this).find('.fa-check').css('display', 'block');
                countContacts = countContacts + 1;
                $('.count').html('(' + countContacts + ')');
                selectedContacts = '<div class="contacts margin-right-5 margin-bottom-5 unselected" id="' + contactId + '" listId="0"><div id="profilePic"><img alt="default user" src="' + imgPath + '"></div><div id="profileDetails">';
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
            if (countContacts == 0) {
                $('a[href="#selectedContacts"]').hide();
                $('a[href="#selectedContacts"]').parent('li').removeClass('active');
                $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
                MobileTabClick();
            }
            else { $('a[href="#selectedContacts"]').show(); }

            gName = "";
            if ($.trim(participantLength) == 0) {
                gName = $('#grpTalkName').val();
            }
            var grpName = "";
            grpName = $('#grpTalkName').val();
            if (gName.length == 0) {
                $('#grpTalkName').val(name.substring(0, 4).toUpperCase());
            }

            else if (countContacts == 2) {

                $('#grpTalkName').val(grpName.substring(0, 2) + name.substring(0, 2).toUpperCase());

            }
            else if (grpName.length < 6)
            { $('#grpTalkName').val(grpName + name.substring(0, 2).toUpperCase()); }


        }

    });

    //$(document).on('click', ".dropdown-menu", function () {
    //teja
    //});

    //--------------------------------------Starting Group call 
    $(document).on('click', '#startNow', function () {
        memberName = "", memberMobile = "", resStr = "", response = "", participants = "";
        nameSplit = [];
        $.blockUI({ message: '<h4> Creating Group </h4>' });
        $('.contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                memberName = $(this).find('p:eq(0)').attr('name');
                memberMobile = $(this).find('p:eq(1)').text();
                if ($(this).attr('listId') == "0") {
                    listIds = "";
                }

                participants += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false"}' + ",";
                participantsArray.push(memberName);

            }

        });

        resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";
        var participantsLenght = $('#selectedContacts').find("div.unselected").length;
        displayCurrentTime();
        displayCurrentDate();
        //if (participantsArray.length != 0) {
        //    $('#grpTalkName').val(nameSplit);
        //}
        grpTalkName = $('#grpTalkName').val();
        if (grpTalkName == "") {
            //$('#grpCallErrorMessage').html("Please Enter Group Call Name").show();
            Notifier("Please Enter Group Call Name", 2);
            $.unblockUI();
            return false;
        }

        if (participantsLenght == 0) {
            //$('#grpCallErrorMessage').html("Please Choose  Participants To Make A Call").show();
            Notifier("Please Choose  Participants To Make A Call", 2);
            $.unblockUI();
            return false;
        }
        // if (participantsLenght == 1) {
        // //$('#grpCallErrorMessage').html("Please Choose Atleast 2 Participants To Make A Call").show();
        // Notifier("Please Choose Atleast 2 Participants To Make A Call", 2);
        // $.unblockUI();
        // return false;
        // }


        $('#selectedContacts .contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                mobileNosInSelectedTab = $(this).find('p:eq(1)').text();

                if ($('#hostNumber').val() == mobileNosInSelectedTab.slice(sliceNumber)) {

                    //$('#grpCallErrorMessage').html("This contact(" + mobileNosInSelectedTab + ") has already selected").show();
                    Notifier("This contact(" + mobileNosInSelectedTab + ") has already selected", 2);
                    $('#selectedContacts div[id="' + contactId + '"]').remove();
                    $.unblockUI();
                    return false;

                }
            }

        });

        response = '{"GroupCallName":"' + grpTalkName + '","SchType":"100","SchduledTime":"' + currentTime + '","SchduledDate":"' + currentDate + '","IsMuteDial":"0","WeekDays":"","Reminder":"30","WebListIds":"' + listIds + '",' + resStr + '}';
        createGroupCall(response, 1);
        console.log(response);
        $.unblockUI();
    });

    $('.weekDay').on('click', function () {
        if ($(this).hasClass("active")) {
            // $('.weekDay.active').removeClass('active');
            $(this).removeClass('active');
            //global_day = ;
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
    });

    //--------------------------------------Click event on Schedule to select weekdays and DatePicker starts here

    $('#schedule').click(function () {

        var currentDate = getGrpTalkUserDateTime();
        var dateNew = new Date(currentDate.getTime() + (5 * 60 * 1000));
        //mdate = (dateNew.getMonth() + 1) + '-' + dateNew.getDate() + '-' + dateNew.getFullYear() + ' ' + dateNew.getHours() + ':' + dateNew.getMinutes() + ':00'
        $('.contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                memberName = $(this).find('p:eq(0)').text();
                memberMobile = $(this).find('p:eq(1)').text();
                if ($(this).attr('listId') == "0") {
                    listIds = "";
                }

                participants += '{"' + memberName + '":"' + $.trim(memberMobile) + '","IsDndFlag":"false"}' + ",";
                participantsArray.push(memberName);

            }

        });
        grpTalkName = $('#grpTalkName').val();
        var participantsLenght = $('#selectedContacts').find("div.unselected").length;

        if (grpTalkName == "") {
            //$('#grpCallErrorMessage').html("Please Enter Group Call Name").show();
            Notifier("Please Enter Group Call Name", 2);
            return false;
        }

        if (participantsLenght == 0) {
            //$('#grpCallErrorMessage').html("Please Choose  Participants To Make A Call").show();
            Notifier("Please Choose  Participants To Make A Call", 2);
            return false;
        }
        // if (participantsLenght == 1) {
        // //$('#grpCallErrorMessage').html("Please Choose Atleast 2 Participants To Make A Call").show();
        // Notifier("Please Choose Atleast 2 Participants To Make A Call", 2);
        // return false;
        // }

        $('#selectedContacts .contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                mobileNosInSelectedTab = $(this).find('p:eq(1)').text();

                if ($('#hostNumber').val() == mobileNosInSelectedTab.slice(sliceNumber)) {

                    //$('#grpCallErrorMessage').html("This contact(" + mobileNosInSelectedTab + ") has already selected").show();
                    Notifier("This contact(" + mobileNosInSelectedTab + ") has already selected", 2);
                    $('#selectedContacts div[id="' + contactId + '"]').remove();


                }
            }

        });

        //$('#datepickerModal').modal('show');
        $('#datepickerModal').modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });


        $("#dtBox").DateTimePicker({
            isPopup: true,
            dateTimeFormat: "dd-MM-yyyy HH:mm:ss",
            defaultDate: new Date(currentDate.getTime() + (10 * 60 * 1000)),
            formatHumanDate: function (date) {
                return date.day + ", " + date.month + " " + date.dd + ", " + date.yyyy;
            }
        });
    });

    $('#scheduleDate').on('click', function (e) {
        //$('.modal-content').block({ message: '<h4> Scheduling Group </h4>' });
        e.preventDefault();
        var repeats = $('#repeat').val();
        var weekDays = "", schType = 0, editType = 0;

        response = "";

        grpTalkName = $('#grpTalkName').val();



        txtDate = $('.date-picker').val().toString();
        var scheduleDateTime = "";
        scheduleDateTime = txtDate.split("at");
        currentDate = scheduleDateTime[0];
        //currentDate="08-30-2016";
        currentTime = scheduleDateTime[1];
        if ($.trim(repeats) == "") {
            weekDays = "";
            schType = 0;
        }
        else {
            weekDays = repeats;
            schType = 1;
            // var addminutes = new Date(scheduleDateTime[0] + " " + scheduleDateTime[1]);
            // currentTime = new Date(addminutes.getTime()+5*60000);

        }

        if (currentDate == "") {
            //$('#ScheduleErrorMessage').html('Please Select Schedule Datetime').show();
            Notifier('Please select schedule date and time', 2);
            //$('.modal-content').unblock();
            return false;
        }
        var minDate = getGrpTalkUserDateTime();
        minDate.setMinutes(minDate.getMinutes() + 9.2);
        var selectedDate = "";
        selectedDate = new Date(scheduleDateTime[0] + " " + scheduleDateTime[1]);
        if (schType == 0) {
            if (Date.parse(selectedDate) < Date.parse(minDate)) {
                //$('#ScheduleErrorMessage').html('Please Select 5 mins from now').show();
                Notifier("Please Select 10 mins from now", 2);
                //$('.modal-content').unblock();
                return false;

            }
        }


        $('#ScheduleErrorMessage').html('').hide();
        resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";

        response = '{"GroupCallName":"' + grpTalkName + '","SchType":"' + schType + '","SchduledTime":"' + currentTime + '","SchduledDate":"' + currentDate + '","IsMuteDial":"0","WeekDays":"' + weekDays + '","Reminder":"30","WebListIds":"' + listIds + '",' + resStr + '}';
        createGroupCall(response, 2);

        //$('.modal-content').unblock();

    });

    $('#clearRepeat').click(function () {
        $('.weekDay').each(function () {
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
            }
        });

    });

    $('#repeat').click(function (e) {
        e.preventDefault();
        $('.weekDay').removeClass("active");
        var weekDaysSelected = $('#repeat').val();
        var weekDays = new Array();
        weekDays = weekDaysSelected.split(",");
        $('.weekDay').each(function () {
            for (var i = 0; i < weekDays.length; i++) {
                if (weekDays[i].toLowerCase() == $(this).val().toLowerCase()) {
                    $(this).addClass("active");
                }
            }
        });
        //$('#repeatCall').modal("show");
        $('#repeatCall').modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });

    });


});

//---------------------------------------------Click Event for Unselected class from Selected Tab

$(document).on("click", ".unselected", function (e) {
    e.stopPropagation();
    var contactId = $(this).attr("id");
    var listId = $(this).attr("listID");
    var mobileNo = $(this).find("p:eq(1)").text();
    var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
    selectedMobileNos.splice(index, 1);
    nameArray.splice(index, 1);
    memberImageArray.splice(index, 1);
    selectedContacts = "";
    if ($(this).hasClass("unselected")) {
        nameSplit = [];
        $(this).remove();
        countContacts = countContacts - 1;
        $('.count').html('(' + countContacts + ')');
        $('.contacts').each(function () {
            if ($(this).hasClass('unselected')) {
                memberName = $(this).find('p:eq(0)').text();
                memberMobile = $(this).find('p:eq(1)').text();
                memberName = $(this).find('p:eq(0)').text().substring(0, 2).toUpperCase();
                nameSplit += memberName.replace();
                $('#grpTalkName').val(nameSplit);
                e.stopPropagation();

            }

        });

        var length = $('.unselected').length;
        if (length == 0) {
            nameSplit += '';
            $('#grpTalkName').val(nameSplit);

        }
    }


    $('.selected').each(function () {

        var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
        if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
            $(this).removeClass("selected");
            $(this).find('.fa-check').css('display', 'none');
        }
        e.stopPropagation();
    })

    if (countContacts == 0) {
        $('a[href="#selectedContacts"]').hide();
        $('a[href="#selectedContacts"]').parent('li').removeClass('active');
        $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
        MobileTabClick();
    }
    else { $('a[href="#selectedContacts"]').show(); }


});



$(document).click(function () {
    $('#search').css("border-color", "none");
});



//---------------------------------------------Fetching All Mobile Contacts
function getMobileContacts(src) {
    pageIndex++;
    global_alphabetpageIndex = 0;
    mobileScroll = 0;
    if (pageIndex == 1 || pageIndex <= pageCount) {
        if (pageIndex == 1) {
            $('#grpCallMobileContacts').html('');
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
                                    for (var sub = 0; sub < result.contactDetails[j].subContactDeatils.length; sub++)
                                    {                                      
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


//--------------------------------------------Alphabetical sorting on web contacts and mobile contacts
function alphabeticalContactSorting(sourceValue, alphabetPageIndex, alphabet, listId) {

    alphabetPageIndex++;
    if (alphabetPageIndex == 1 || alphabetPageIndex <= alphabetPageCount) {

        global_alphabetpageIndex = alphabetPageIndex;
        if (alphabetPageIndex == 1) {
            global_alphabetMobileContactsData = "";
            alphabetCOntactsCount = 0;
            sortSubContactsResponse = new Array();
        }
        // $.blockUI({ message: '<h4> Loading...</h4>' })
        $.ajax({
            url: "/HandlersWeb/Contacts.ashx",
            data: {
                type: 9,
                source: sourceValue,
                alphabetPageIndex: alphabetPageIndex,
                modeSp: 2,
                alphabetSort: alphabet,
            },
            // async: false,
            method: "POST",
            dataType: "JSON",
            success: function (contactResult) {
                alphabetPageCount = contactResult.pageCount;

                alphabetResponse(contactResult, sourceValue);
                setTimeout($.unblockUI, 400);
            },
            error: function (contactResult) {
                //alert("Something Went Wrong");
                Notifier("Something Went Wrong", 2);
                // $.unblockUI();
            }


        });
    }
}


//-------------------------------------------Special Character Sorting Filter 
function specialCharSearch(sourceValue, listId) {
    global_alphabetMobileContactsData = "";
    global_alphabetpageIndex = 1;
    sortSubContactsResponse = new Array();
    $.ajax({
        url: "/HandlersWeb/Contacts.ashx",
        data: {
            type: 9,
            source: sourceValue,
            alphabetPageIndex: 1,
            modeSp: 3,
            alphabetSort: '',
        },
        async: false,
        method: "POST",
        dataType: "JSON",
        success: function (contactResult) {
            alphabetResponse(contactResult, sourceValue);
        },
        error: function (contactResult) {
            //alert("Soemthing Went Wrong")
            Notifier("Something Went Wrong", 2);
        }


    });
}

function alphabetResponse(contactResult, sourceValue) {

    if (global_alphabetpageIndex == 1) {
        global_alphabetMobileContactsData = '';
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

function displayCurrentTime() {
    currentTime = "";
    var date = getGrpTalkUserDateTime();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    currentTime = hours + ':' + minutes + ' ' + ampm;
    return currentTime;

}
function displayCurrentDate() {
    currentDate = ""

    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

    var date = getGrpTalkUserDateTime();
    //date = date.toISOString();
    var day = date.getDate();
    var month = date.getMonth() + 1;
    var month_name = monthNames[month];
    var year = date.getFullYear();
    if (month < 10) {
        month = '0' + month;
    }
    currentDate = month + "-" + day + "-" + year;
    return currentDate;

}

//-------------------------------------------Create Group call
function createGroupCall(response, mode) {

    $.blockUI({ message: '<h4> Creating Group </h4>' })
    $.ajax({
        url: 'HandlersWeb/Groups.ashx',
        type: 'post',
        async: false,
        data: { type: 4, paramObj: response },
        dataType: 'json',
        success: function (result) {
            if (result.Success == true) {
                if (mode == 1) {
                    var grpCallId = 0;
                    grpCallId = result.GroupCallDetails.GroupID;
                    groupCallDial(grpCallId);



                    setTimeout(function () {
                        window.location.href = "/MyGroup.aspx";
                    }, 2000);

                }
                if (mode == 2) {
                    Notifier('Schedule Call created successfully', 1);
                    window.location.href = "/MyGroup.aspx";
                }
                if (mode == 3) {
                    Notifier('Group call saved successfully', 1);
                    setTimeout(function () {
                        window.location.href = "/MyGroup.aspx";
                    }, 2000);
                }

            }
            else {
                Notifier(result.Message, 2);
            }
            setTimeout($.unblockUI(), 200);

        },
        error: function (result) {
            $.unblockUI();
            //alert("Something Went Wrong");
            Notifier("Something Went Wrong", 2);
        }
    });
}

function groupCallDial(grpCallId) {
    $.blockUI({ message: '<h4> Initialing grpTalk call </h4>' })
    var dialObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"True","MobileNumber":"","IsCallFromBonus":"0","IsMute" : "False"}';
    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'post',
        data: { type: 1, dialObj: dialObj },
        dataType: 'json',
        success: function (result) {
            $.unblockUI();
            // alert(result.Message);
        },
        error: function (result) {
            $.unblockUI();
            //alert(result.Message);
            Notifier(result.Message, 2);
        }
    });
}


function MobileTabClick() {
    $('#grpCallMobileContacts,.alphabet').show();
    $('#grpCallMobileContacts').parent().show();
    $('#listName').text("All Web Contacts");
    if ($('#selectedContacts').parent('.slimScrollDiv').length != 0)
    { $('#selectedContacts').parent().hide(); }
    $('.addNewContact').hide();
    $('.list1').hide();
    $('grpCallMobileContacts.slimScrollDiv').show();
    $('#grpCallMobileContacts').addClass("active in");
    $('#grpCallWebContacts,#selectedContacts').removeClass("active in");
    firstClick++;
}

$('#saveGroupCall').click(function (e) {
    memberName = "", memberMobile = "", resStr = "", response = "", participants = "";
    nameSplit = [];
    $.blockUI({ message: '<h4> Creating Group </h4>' });
    $('.contacts').each(function () {
        contactId = $(this).attr('id');
        if ($(this).hasClass('unselected')) {
            memberName = $(this).find('p:eq(0)').attr('name');
            memberMobile = $(this).find('p:eq(1)').text();
            if ($(this).attr('listId') == "0") {
                listIds = "";
            }

            participants += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false"}' + ",";
            participantsArray.push(memberName);

        }

    });

    resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";
    var participantsLenght = $('#selectedContacts').find("div.unselected").length;
    displayCurrentTime();
    displayCurrentDate();
    //if (participantsArray.length != 0) {
    //    $('#grpTalkName').val(nameSplit);
    //}
    grpTalkName = $('#grpTalkName').val();
    if (grpTalkName == "") {
        //$('#grpCallErrorMessage').html("Please Enter Group Call Name").show();
        Notifier("Please Enter Group Call Name", 2);
        $.unblockUI();
        return false;
    }

    if (participantsLenght == 0) {
        //$('#grpCallErrorMessage').html("Please Choose  Participants To Make A Call").show();
        Notifier("Please Choose  Participants To Make A Call", 2);
        $.unblockUI();
        return false;
    }
    // if (participantsLenght == 1) {
    // //$('#grpCallErrorMessage').html("Please Choose Atleast 2 Participants To Make A Call").show();
    // Notifier("Please Choose Atleast 2 Participants To Make A Call", 2);
    // $.unblockUI();
    // return false;
    // }


    $('#selectedContacts .contacts').each(function () {
        contactId = $(this).attr('id');
        if ($(this).hasClass('unselected')) {
            mobileNosInSelectedTab = $(this).find('p:eq(1)').text();

            if ($('#hostNumber').val() == mobileNosInSelectedTab.slice(sliceNumber)) {

                //$('#grpCallErrorMessage').html("This contact(" + mobileNosInSelectedTab + ") has already selected").show();
                Notifier("This contact(" + mobileNosInSelectedTab + ") has already selected", 2);
                $('#selectedContacts div[id="' + contactId + '"]').remove();
                $.unblockUI();
                return false;

            }
        }

    });

    response = '{"GroupCallName":"' + grpTalkName + '","SchType":"100","SchduledTime":"' + currentTime + '","SchduledDate":"' + currentDate + '","IsMuteDial":"0","WeekDays":"","Reminder":"30","WebListIds":"' + listIds + '",' + resStr + '}';
    createGroupCall(response, 3);
    console.log(response);
    $.unblockUI();
});


function saveGrpCall(response) {
    $.blockUI({ message: '<h4> Creating Group </h4>' })
    $.ajax({
        url: 'HandlersWeb/Groups.ashx',
        type: 'post',
        async: false,
        data: { type: 4, paramObj: response },
        dataType: 'json',
        success: function (result) {
            if (result.Success == true) {


                setTimeout($.unblockUI(), 200);

                setTimeout(function () {
                    window.location.href = "/MyGroup.aspx";
                }, 2000);

            }


            else {
                Notifier(result.Message, 2);
                $.unblockUI();
            }


        },
        error: function (result) {
            $.unblockUI();
            //alert("Something Went Wrong");
            Notifier("Something Went Wrong", 2);
        }
    });
}




function sliceNumberFn() {
    var countryId = $('#countryID').val();
    if (countryId == 108) {
        sliceNumber = -10;
    }
    else if (countryId == 19)
    { sliceNumber = -8; }
}
$(document).click(function (e) {
    $('#search').css("border-color", "none");

});