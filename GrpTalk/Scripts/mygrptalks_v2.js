/// <reference path="../assets/global/plugins/jquery-1.11.0.min.js" />
var history = []; btchID = '';
var global_GroupId = 0, global_BatchId = "";
var memebersDetails = "";
var jsonParticipants = new Array();
var grpID = 0, batchIDFirst = "", isSubscribed = 0;
var grpTalkHistory = "";
var grpIdSplit = new Array();
var isCreatedBy = 0;
var isStarted = 0;
var hostMobile = "";
var webPageCount = 0, webPageIndex = 1;
var schType;
var callDurationInSeconds = 0;
var pageIndex = 1, pageCount = 0, particpantMembers = 0;
var pusherChannel = "";
var mode = 0;
var isTimerStart = 0;
var totalFetchingMembers = 0;
var totalMembers = 0;
var inProgressPageNumber = 0;
var liveIndex = 0;
var mutePercentage = 0;
var participantLength = 0;
var currencyName= CurrencyName();
var host,connection;

$(document).ready(function () {

    getListOfGrpCalls();

    //$('.CallLogName').text($('#grpDetails:eq(0)').attr('grpcallname'));
    $('#grpTalkCallsList .list').removeClass('active');
    $('#grpCallMobileContacts').slimScroll({
        allowPageScroll: false,
        height: '410'
    });


   
	
	function switchIn()
	{
		$('.blink_me').toggle();
		$('.blink_me2').toggle();
		$('.blink_me2').text($('.timer').text());
		}
    setInterval(function(){switchIn();},1000)

    $('#searchByName').val('');
    //$('#leaveGroupCall, #cancelGroupCall').hide();

    $('#grpTalkCallsList').slimScroll({
        allowPageScroll: true,
        height: '480'
    });
    // $('#accordion').slimScroll({
    // allowPageScroll: true,
    // height: '250'
    // });


    $(document).on("click", "span[addMember=true]", function () {
        $('.list').removeClass("active");
        grpCallID = $(this).attr("grpID");
        $('#' + grpCallID).click();
        $('#contactsModalDiv').modal('show');
        $('#addCall').attr('grpCallID', grpCallID);
        mode = 2;

    });

    //--------------------------------------tabs click
    $("ul#reportsTab li").click(function () {
        memebersDetails = "";
        $('#more').html('');
        $("ul#reportsTab li").removeClass("active"); //Remove any "active" class

        $(".tab_content").hide(); //Hide all tab content
        var activeTab = $(this).find("a").attr("href");

        if (activeTab == "#members") {       //checking if memebers tab is active
            if ($('#members').is(':visible')) {
                $(this).addClass("active");

                return false;
            }
            else {
                $('#history,#more').hide();
                $('#inputSearch, #searchByName').show();
                $('#searchByName').val('');
                $('#memebersDetails').html('');

                for (var i = 0; i < jsonParticipants.Groups.length; i++) {
                    for (var k = 0; k < jsonParticipants.Groups[i].Participants.length; k++) {

                        if (jsonParticipants.Groups[i].Participants[k].GroupId == grpID) {
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].Participants[k].Name.length > 14)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 14) + '...</p><p><i class="fa fa-mobile" aria-hidden="true"></i>' + "  " + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>'; }

                            else
                            { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p><p><i class="fa fa-mobile" aria-hidden="true"></i>' + "  " + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>'; }
                        }
                    }
                }

                $('#members').html("");

                $('#members').html(memebersDetails).show();
            }

        }
        if (activeTab == "#history") {      // checking if history tab is active
            if ($('#history').is(':visible')) {
                $(this).addClass("active");

                return false;
            }
            else {

                $('#accordion').html("");
                $('#history').show();
                $('#inputSearch, #searchByName').hide();
                $('#members').hide();
                $('#accordion').show();
                $('.list').each(function () {
                    if ($(this).hasClass('active')) {
                        isCreatedBy = $(this).attr('isCreated');
                    }
                });
                if (grpIdSplit[1] == undefined) {
                    webPageIndex = 1
                    getGrpTalkHistoryDetails(grpID, isCreatedBy);
                }
                else {
                    webPageIndex = 1
                    getGrpTalkHistoryDetails(grpIdSplit[1], isCreatedBy);
                }

                $('#members').hide();
            }
        }
    });

    //--------------------------------------CLICK EVENT on Grptalk List clicks


    // inprogress scroll

    $('#memberSearch').keyup(function (e) {

        if ($('#memberSearch').val().length == 0) {

            if ($('.allMembers').hasClass('active')) {
                GetConferenceRoom(grpID, 1, "", 20, 1);
            }
            else if ($('.onCall').hasClass('active')) {
                GetConferenceRoom(grpID, 2, "", 20, 1);
            }
            else if ($('.callEnded').hasClass('active')) {
                GetConferenceRoom(grpID, 3, "", 20, 1);
            }
            else if ($('.muted').hasClass('active')) {
                GetConferenceRoom(grpID, 4, "", 20, 1);
            }
            else {
                GetConferenceRoom(grpID, 5, "", 20, 1);
            }

        }
    });

    $('#btnInProgressSearch').click(function (e) {
        e.preventDefault();
        var searchText = "";
        var grpCallId = 0;
        searchText = $('#memberSearch').val();
        grpCallId = $('#search').attr('grpid');
        if ($('.allMembers').hasClass('active')) {
            GetConferenceRoom(grpCallId, 1, searchText, 20, 1);
        }
        else if ($('.onCall').hasClass('active')) {
            GetConferenceRoom(grpCallId, 2, searchText, 20, 1);
        }
        else if ($('.callEnded').hasClass('active')) {
            GetConferenceRoom(grpCallId, 3, searchText, 20, 1);
        }
        else if ($('.muted').hasClass('active')) {
            GetConferenceRoom(grpCallId, 4, searchText, 20, 1);
        }
        else {
            GetConferenceRoom(grpCallId, 5, searchText, 20, 1);
        }


    });
    $('#searchByName').click(function (e) {
        e.stopPropagation();
        $('#searchBtn').css("border-color", "#66afe9");
    })
    $("#searchByName").keyup(function (event) {
        // var searchlength = $('#searchByName').val().length
        // if (searchlength == 0) {
        // $('#searchBtn').click();
        // }
        // if(event.which == 46)
        //{ 
        $('#searchBtn').click();
        //}
    });

    //--------------------------------------CLICK EVENT on Search filter for participants
    $('#searchBtn').on('click', function (e) {

        e.preventDefault();
        var searchcount = 0;
        memebersDetails = "";

        nameSearch = $('#searchByName').val().toLowerCase();
        for (var i = 0; i < jsonParticipants.Groups.length; i++) {
            for (var k = 0; k < jsonParticipants.Groups[i].Participants.length; k++) {
                if (nameSearch == "") {
                    if (jsonParticipants.Groups[i].Participants[k].GroupId == grpID) {

                        searchcount = 1;

                        memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                        memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                        if (jsonParticipants.Groups[i].Participants[k].Name.length > 14) { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 14) + '...</p>'; }
                        else { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p>'; }

                        memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>';

                    }


                }
                else {


                    if ((jsonParticipants.Groups[i].Participants[k].Name.toLowerCase()).indexOf(nameSearch) > -1) {
                        searchcount = 1;
                        memebersDetails += '<div class="col-md-3 contactDetails margin-right-5 margin-bottom-5">';
                        memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                        if (jsonParticipants.Groups[i].Participants[k].Name.length > 17) {
                            memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 16) + '...</p>';
                        }
                        else { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p>'; }
                        memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>';
                    }

                }
            }

            for (var l = 0; l < jsonParticipants.Groups[i].LeaveParticipants.length; l++) {
                if (jsonParticipants.Groups[i].GroupID == grpID) {
                    if (nameSearch == "") {
                        if (jsonParticipants.Groups[i].LeaveParticipants.length != 0) {

                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 14)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 14) + '...</p>'; }
                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                            }
                            memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                            memebersDetails += '<div class="ribbon-wrapper-green"><span class="ribbon-green">Left Group<span></div></div>';




                        }
                    }
                    else {
                        if ((jsonParticipants.Groups[i].LeaveParticipants[l].Name.toLowerCase()).indexOf(nameSearch) > -1) {
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 14)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 14) + '...</p>'; }
                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                            }
                            memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                            memebersDetails += '<div class="ribbon-wrapper-green"><span class="ribbon-green">Left Group<span></div></div>';
                        }
                    }
                }
            }
        }
        if (searchcount == 0) {
            memebersDetails += 'No Records Found With This Criteria';
        }
        $('#history').hide();
        //$("ul#reportsTab li").removeClass("active");
        // $("ul#reportsTab li").eq(0).addClass("active");
        $('#members').html("");
        $('#members').html(memebersDetails).show();
        e.stopPropagation();

    });

	$(document).delegate("#quickDial", "click", function (e) {
		$('.list').removeClass('active');
		$('#'+$(this).attr('grpId')).addClass('active');
		$('#dialGroupCall').click();
	});
    //--------------------------------------CLICK EVENT for dialing grptalk call from list
    $(document).delegate("#dialGroupCall", "click", function (e) {
        $.blockUI({ message: '<h4> Intiating grpTalk Call... </h4>' });
        //$('#dialGroupCall').on('click', function (e) {
        var grpCallId = $('.list.active').attr('id');
        $('#' + grpCallId + '').attr('isStarted', 1);


        var dialObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"True","MobileNumber":"","IsCallFromBonus":"0","IsMute" : "False"}';

        $.ajax({
            url: '/HandlersWeb/GroupCalls.ashx',
            type: 'POST',
            async: false,
            data: { type: 1, dialObj: dialObj },
            dataType: 'JSON',
            success: function (result) {
                if (result.Success == false) {
                    Notifier(result.Message, 2);
                    $.unblockUI();
                    return false;
                }
                $('#' + grpCallId).find('#date').html('<p style="font-family: Impact; font-size: 30px; line-height: 56px;color:#27bd7c;" class="blink_me">Live</p><p style="font-family: Impact; font-size: 15px; line-height: 56px;color:#27bd7c;display:none" class="blink_me2">'+$('.timer').text()+'</p>');
                //$('.list active').trigger('click');
                setTimeout(function () {
                    $("#" + grpCallId).trigger('click');
                }, 100);
                $.unblockUI();

            },
            error: function (result) {
                $.unblockUI();
                Notifier("Something Went Wrong", 2);
            }
        });
    });

    //--------------------------------------CLICK EVENT on deleting groupcall from lists
    $(document).delegate("#deleteGroupCall", "click", function (e) {
        //$('#deleteGroupCall').click(function () {
        var grpCallId = $('.list.active').attr('id');
        var x = confirm("All grptalk history of this group will be deleted");
        if (x == true) {
            $.ajax({
                url: '/HandlersWeb/Groups.ashx',
                type: 'POST',
                async: false,
                data: { type: 6, grpCallId: grpCallId },
                dataType: 'JSON',
                success: function (result) {
                    if (result.Success == true) {

                        $('.list.active').hide();
                        if ($('.list.active').next("div").length > 0) {
                            $('.list.active').next("div").addClass("active");
                        }
                        else {
                            $('.list.active').prev("div").addClass("active");
                        }
                        $('#' + grpCallId + '').remove();
                        $('.list.active').show();
                        $('.list.active').click();
                        alert("Deleted Successfully");


                    }
                },
                error: function (result) {
                    alert("Something Went Wrong");
                }
            });
        }
        else {
            return false;
        }
    });


    $(document).delegate('#moreHistory', 'click', function () {

        grpID = $(this).attr('groupid')

        isCreatedBy = $(this).attr('isCreatedBy');
        // webPageIndex++;
        getGrpTalkHistoryDetails(grpID, isCreatedBy);
        // alert(webPageIndex);
    });

    //--------------------------------------CLICK EVENT for subscription 
    $(document).delegate('.subscription', 'click', function () {

        var status = 1;
        $.ajax({
            url: '/HandlersWeb/Groups.ashx',
            type: 'POST',
            async: false,
            data: { type: 3, subscribeStatus: status },
            dataType: 'JSON',
            success: function (result) {
                if (result.Status == 1) {
                    $('.subscribeText').html('');
                    $('.subscribeText').append('Thank You For Subscribing For The Service. One Of Our Represultentative Will Contact You Soon');
                    $('.subscription').hide();
                }
            },
            error: function (result) {
                alert("Something Went Wrong");
            }
        });

    });

    $(document).on('click', '#createGrpTalk', function () {
        //document.location.href = "/CreateGroupTalk.aspx"
        document.location.href = "/CreateGroup.aspx"

    });

    //--------------------------------------CLICK EVENT for Leaving grptalk call from list
    $(document).on('click', '#leaveGroupCall', function () {
        //$('#leaveGroupCall').click(function () {

        var grpCallId = $('.list.active').attr('id');
        var x = confirm("Are You sure to you want to leave the Group call");
        if (x == true) {
            $.ajax({
                url: '/HandlersWeb/Groups.ashx',
                type: 'POST',
                async: false,
                data: { type: 7, grpCallId: grpCallId },
                dataType: 'JSON',
                success: function (result) {
                    if (result.Success == true) {
                        $('.list.active').hide();
                        if ($('.list.active').next("div").length > 0) {
                            $('.list.active').next("div").addClass("active");
                        }
                        else {
                            $('.list.active').prev("div").addClass("active");
                        }
                        $('#' + grpCallId + '').remove();
                        $('.list.active').show();
                        $('.list.active').click();
                        //alert("You have been left from Group Successfully");
                    }
                },
                error: function (result) {
                    alert("Something Went Wrong");
                }

            });
        }
        else {
            return false;
        }
    });

});

//--------------------------------------Function Calling for List of Grptalk calls
function getListOfGrpCalls() {

    $.blockUI({ message: '<h4> Fetching GrpTalk Conversations...</h4>' });
    $.ajax({
        url: "/HandlersWeb/Groups.ashx",
        data: { type: 1 },
        type: "POST",
        dataType: "JSON",
        success: function (result) {

            jsonParticipants = result;
            var grpTalkCallData = "", memebersDetails = "", groupName = "", participantNames = "", participantNumbers = ""; partcipants = "";
            var month = new Array(), count = new Array(), participantArray = new Array();
            var groupLength = result.Groups.length;
            var isStartedArray = new Array();
            isStartedArray = isSatrted(result.Groups);
            result.Groups = $.merge(isStartedArray, $.merge(getScheduleCalls(result.Groups), getPastCalls(result.Groups)));
            //result.Groups = getPastCalls(result.Groups);


            //alert(result.Groups.length);
            if (result.Groups.length > 0) {
                for (var i = 0; i < result.Groups.length; i++) {

                    grpID = result.Groups[0].GroupID;                      // setting gruop id to variable
                    $('.CallLogName').text(result.Groups[0].GroupName);
                    $('#' + grpID).addClass('active');

                    groupName = result.Groups[i].GroupName;
                    if (groupName.length > 15) { groupName = groupName.substring(0, 15) + '..' }

                    count = result.Groups[i].Participants.length;        // count of particpants 

                    participantArray = result.Groups[i].ParticipantNames.split(",");
                    month = result.Groups[i].SchduledDate.split(" ");
                    var participant1 = participantArray[0]
                    var participant2 = participantArray[1]
                    partcipantNames = ""; partcipantNames = ""; partcipants = "";
                    for (var k = 0; k < count; k++) {

                        partcipantNames = result.Groups[i].Participants[k].Name;
                        participantNumbers = result.Groups[i].Participants[k].Mobile;
                        partcipants += partcipantNames + "~" + participantNumbers + ",";
                    }
                    if (i == 0) {

                        
                        grpTalkCallData += '<div style="cursor: pointer;" class="list margin-bottom-1 active" startDateTime="' + result.Groups[i].StartDateTime + '" grpCallRoom="' + result.Groups[i].GrpCallRoom + '" isStarted="' + result.Groups[i].IsStarted + '" iscreated="' + result.Groups[i].IsCreated + '" id="' + result.Groups[i].GroupID + '" groupName="' + groupName + '" date="' + result.Groups[i].SchduledDate + '" participant="' + result.Groups[i].ParticipantNames + '" schType="' + result.Groups[i].SchType + '" schTime="' + result.Groups[i].SchduledTime + '" isMuteDail="' + result.Groups[i].IsMuteDial + '" weekDays="' + result.Groups[i].WeekDays + '" reminder="30" mobileNumbers="' + partcipants + '" participantLength="' + result.Groups[i].Participants.length + '">';
                    }
                    else {
                        grpTalkCallData += '<div style="cursor: pointer;" class="list margin-bottom-1" startDateTime="' + result.Groups[i].StartDateTime + '" grpCallRoom="' + result.Groups[i].GrpCallRoom + '" isStarted="' + result.Groups[i].IsStarted + '" iscreated="' + result.Groups[i].IsCreated + '" id="' + result.Groups[i].GroupID + '" groupName="' + groupName + '" date="' + result.Groups[i].SchduledDate + '" participant="' + result.Groups[i].ParticipantNames + '" schType="' + result.Groups[i].SchType + '" schTime="' + result.Groups[i].SchduledTime + '" isMuteDail="' + result.Groups[i].IsMuteDial + '" weekDays="' + result.Groups[i].WeekDays + '" reminder="30" mobileNumbers="' + partcipants + '" participantLength="' + result.Groups[i].Participants.length + '">';
                    }

                    grpTalkCallData += '<div class="row"> <div class="col-md-2 col-sm-2 padding-left-0 padding-right-0 calendar" id="date">';

                    if (result.Groups[i].IsStarted == 1) {  //-------live call
                        grpTalkCallData += '<p style="font-family: Impact; font-size: 30px; line-height: 56px;color:#27bd7c;" class="blink_me">Live</p><p style="font-family: Impact; font-size: 15px; line-height: 56px;color:#27bd7c;display:none" class="blink_me2">'+$('.timer').text()+'</p></div>';
                        


                    }
                    else if (result.Groups[i].SchType != 100) {
                        if (result.Groups[i].SchType == 0) {
                            if (new Date(result.Groups[i].StartDateTime) >= new Date()) {  //------upcoming call

                                grpTalkCallData += '<p style="font-family: Impact; font-size: 34px; line-height: 36px;color:orange;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                                grpTalkCallData += '<p style="font-family: Calibri; font-size: 18px; line-height: 20px;color:orange;">' + month[0] + '</p></div>';

                            }
                            else {  //---------past call
                                grpTalkCallData += '<p style="font-family: Impact; font-size: 34px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                                grpTalkCallData += '<p style="font-family: Calibri; font-size: 18px; line-height: 20px;">' + month[0] + '</p></div>';
                            }

                        }
                        else {  //---------upcoming call
                            grpTalkCallData += '<p style="font-family: Impact; font-size: 34px; line-height: 36px;color:orange;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                            grpTalkCallData += '<p style="font-family: Calibri; font-size: 18px; line-height: 20px;color:orange;">' + month[0] + '</p></div>';
                        }
                        //grpTalkCallData += '<div class="col-md-10 col-sm-10 padding-left-0 padding-right-0 participantsList" id="grpDetails" grpcallname="' + result.Groups[i].GroupName + '" >';
                        //grpTalkCallData += '<p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">' + groupName + '</p>';

                    }
                    else {  //--------past call
                        grpTalkCallData += '<p style="font-family: Impact; font-size: 34px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                        grpTalkCallData += '<p style="font-family: Calibri; font-size: 18px; line-height: 20px;">' + month[0] + '</p></div>';
                    }
                    grpTalkCallData += '<div class="col-md-8 col-sm-8  padding-right-0 participantnames" id="grpDetails" grpcallname="' + result.Groups[i].GroupName + '" style="position:relative">';
                    grpTalkCallData += '<p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">' + groupName + '</p>';

                    if (count > 2) {
                        var disp_array = new Array();
                        for (var j = 2; j < count; j++) {
                            disp_array += '' + result.Groups[i].Participants[j].Name + '';
                            var disp_cnt = count - 2;
                        }
                        grpTalkCallData += '<p style="font-family: Calibri;  font-size: 16px; line-height: 15px;">' + result.Groups[i].CreatedBy + '*, ' + participant1 + ', ' + participant2 + ',.. <span style="color:#428bca;"><b>' + disp_cnt + ' More...</b></p></div>';
                    }
                    else { grpTalkCallData += '<p style="font-family: Calibri;  font-size: 16px; line-height: 15px;">' + result.Groups[i].CreatedBy + '*, ' + result.Groups[i].ParticipantNames + '. </p></div>'; }
                    if ($.trim(result.Groups[i].IsCreated) == 1) {
                      
                            grpTalkCallData += '<div class="pull-left"><span id="quickDial" grpId="'+result.Groups[i].GroupID+'" class="fa fa-phone fa-2x"  style="text-align: left ! important; cursor: pointer; color:#81ADD9 ! important; margin-top:20px; padding-left:5px;"></span></div>';
                      

                    }

                    grpTalkCallData += '</div></div>';
                    //grpTalkCallData += '</div>';

                    for (var k = 0; k < count; k++) {

                        if (grpID == result.Groups[i].Participants[k].GroupId) {
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                            if (result.Groups[i].Participants[k].Name.length > 14)
                            { memebersDetails += '<div id="profileDetails"><p title="' + result.Groups[i].Participants[k].Name + '">' + result.Groups[i].Participants[k].Name.substring(0, 14) + '...</p>'; }
                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + result.Groups[i].Participants[k].Name + '</p>';
                            }
                            memebersDetails += '<p><i id="quickDial" class="fa fa-mobile" aria-hidden="true"></i> ' + result.Groups[i].Participants[k].Mobile + '</p></div>';
                            memebersDetails += '</div>';
                        }

                    }

                    if (result.Groups[i].LeaveParticipants.length != 0) {
                        if (result.Groups[i].GroupID == grpID) {
                            for (var l = 0; l < result.Groups[i].LeaveParticipants.length; l++) {
                                memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                                memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                                if (result.Groups[i].LeaveParticipants[l].Name.length > 14)
                                { memebersDetails += '<div id="profileDetails"><p title="' + result.Groups[i].LeaveParticipants[l].Name + '">' + result.Groups[i].LeaveParticipants[l].Name.substring(0, 14) + '...</p>'; }
                                else {
                                    memebersDetails += '<div id="profileDetails"><p>' + result.Groups[i].LeaveParticipants[l].Name + '</p>';
                                }
                                memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                                memebersDetails += '<div class="ribbon-wrapper-green"><span class="ribbon-green">Left Group<span></div></div>';


                            }
                        }

                    }
                }
                $.unblockUI();
                $('#members').html(memebersDetails);
                $('#grpTalkCallsList').html(grpTalkCallData);
                $(document).delegate(".list", "click", function () {

                    var Actions = "";
                    var grpCallName = "", inProgressStr = "", hostActions = "";
                    memebersDetails = "", participants = "";

                    $('#searchByName').val('');
                    $('#more').hide();
                    $('#inputSearch, #searchByName').show();
                    $('#grpTalkCallsList .list').removeClass('active');
                    $(this).addClass('active');

                    grpCallName = $(this).attr('groupname');
                    schType = $(this).attr('schType');
                    isCreatedBy = $(this).attr('iscreated');
                    participants = $(this).attr('participantLength');
                    participantLength = $(this).attr('participantlength');
                    $('#membersCount').html(participants + ' ')

                    if (isCreatedBy == 0) {
                        //$('#cancelGroupCall,#dialGroupCall, #editGroupCall, #deleteGroupCall').hide();
                        //$('#leaveGroupCall').show();
                        //$('#hostActions').remove();
                        Actions = '<li><img src="/images/leavegroup.png" id="leaveGroupCall" title="Leave Group" style="cursor: pointer;"/>';
                    }
                    else if (isCreatedBy == 1) {

                        $('#hostActions').show();
                        //if (participants >= 2) {
                        Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-trash-o" id="deleteGroupCall" title="Delete" style="cursor: pointer;"></i>';
                        if (schType == 0 || schType == 1) {
                            if (formatForDate($(this).attr('startDateTime')) + formatAMPM($(this).attr('startDateTime')) >= formatForDate(new Date()) + formatAMPM(new Date())) {
                                Actions += '<li><i aria-hidden="true" style="font-size:18px;" class="fa fa-calendar" id="cancelGroupCall" title="Cancel Schedule"></i></li>'
                            }

                        }
                        Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-pencil" id="editGroupCall" title="Edit" style="cursor: pointer;"></i></li>';
                        Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-phone" id="dialGroupCall" title="Dial" style="cursor: pointer;"></i></li>';

                        // }
                        // else {
                        // Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-trash-o" id="deleteGroupCall" title="Delete" style="cursor: pointer;"></i>';
                        // Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-pencil" id="editGroupCall" title="Edit" style="cursor: pointer;"></i></li>';

                        // }
                        //$('#editGroupCall, #deleteGroupCall').show();
                        //$('#leaveGroupCall').hide();




                    }

                    $('#groupCallButtons').html(Actions);
                    isStarted = $(this).attr('isstarted');

                    grpID = $(this).attr("id");
                    if (grpID == "352449") {
                        isStarted = 1;
                    }
					isStarted = 1;
                    if (isStarted == 1) {
                        
                        if (isCreatedBy == 0) {
                            $('.tab-content').slimScroll({
                                allowPageScroll: true,
                                height: '500'
                            });
                        }
                        else {
                            hostActions = "<div class='col-md-12 col-sm-12'><ul id='inProgressActions'>";
                            hostActions = hostActions + "<li id='mute' action='mute'><a href='javascript:;'><img title='Mute All' width='27' src='images/GroupMuteAll.png' alt='Mute' /></a></li>";
                            hostActions = hostActions + "<li id='addMember'><a href='javascript:;'><img width='27' alt='Add Member' title='Add Member' src='images/AddGroupCall.png' /></a></li>"
                            hostActions = hostActions + "<li id='hangup'><a class='text-center' href='#'><img width='27' alt='Hangup' title='Hangup All' src='images/GrouphangUpAll.png' /></a></li></ul></div>"
                            $('#hostActions').html(hostActions);
                            $('.tab-content').slimScroll({
                                allowPageScroll: true,
                                height: '360'
                            });
                        }

                        $('#search_member,#groupCallButtons,#membersLists').hide();
                        $('#inProgress').show();

                        $('.CallLogName').html(grpCallName);

                        $('#hangup,#addMember,#mute,.allMembers,.onCall,.callEnded,.muted,wantsToTalk,#search').attr('grpID', grpID);
                        $('.allMembers').addClass('active');
                        $('#listTitle1').css("background-color", "#25bd80 !important");
                        // pusherbind
                        var number = "";
                        var status = "";
                        var actionsBody = ""
						GetConferenceRoom(grpID, 1, '', 20, 1);

                        var conferenceroom = $(this).attr('grpCallRoom');
                        
						ConnectToWebSocket(conferenceroom,false);
						connection.onclose=function(){
								console.log("Connection Close");
								$('#wcreconnect').show();
								refreshIntervalId =setInterval(function(){ConnectToWebSocket(conferenceroom,true);},4000);
						}
						connection.onerror=function(){
							$('#wcreconnect').show();
							console.log("Connection Error");
							//refreshIntervalId =setInterval(function(){ConnectToWebSocket(conferenceroom);},4000);
						}
						connection.onmessage=function(message){
							onMessageDef(conferenceroom,message);
						}
                        
                       



                        return;
                    }
                    else {
                        //$('.tab-content').hide();
                        $('#listTitle').css("background-color", "#455C70 !important");
                        $('.CallLogName').text(grpCallName);
                        $('#membersLists,#groupCallButtons').show();
                        $('#inProgressDiv,#inProgress').hide();
                        memebersDetails = "<div class='tab-pane fade in active' id='members'>";
                        for (var i = 0; i < jsonParticipants.Groups.length; i++) {
                            for (var k = 0; k < jsonParticipants.Groups[i].Participants.length; k++) {

                                if (jsonParticipants.Groups[i].Participants[k].GroupId == grpID) {

                                    memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                                    memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                                    if (jsonParticipants.Groups[i].Participants[k].Name.length > 14) {
                                        memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 14) + '...</p>';
                                    }
                                    else { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p>'; }
                                    memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i>  ' + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>';
                                }
                            }



                            if (jsonParticipants.Groups[i].LeaveParticipants.length != 0) {

                                for (var l = 0; l < jsonParticipants.Groups[i].LeaveParticipants.length; l++) {
                                    if (jsonParticipants.Groups[i].GroupID == grpID) {
                                        memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                                        memebersDetails += '<div id="profilePic"><img src="images/DefaultUser1.png" alt="user" /></div>';
                                        if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 14)
                                        { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 14) + '...</p>'; }
                                        else {
                                            memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                                        }
                                        memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                                        memebersDetails += '<div class="ribbon-wrapper-green"><span class="ribbon-green">Left Group<span></div></div>';

                                    }
                                }

                            }
                        }

                        memebersDetails = memebersDetails + "</div><div class='tab-pane fade' id='history'><div id='accordion'></div><div id='more' style='display:none'></div></div>";

                        $('.tab-content').html('');
                        $('.tab-content').html(memebersDetails);
                        $('.members').show();

                        // $('#members').html("");
                        // $('#members').addClass('active in');

                        // $('#members').html(memebersDetails).show();

                    }
                    $('.tab-content').slimScroll({
                        allowPageScroll: true,
                        height: '420'
                    });
                    $('#accordion').html("");

                    $("ul#reportsTab li").removeClass("active");
                    $("ul#reportsTab li").eq(1).addClass("active");



                });
                $('.list:eq(0)').click();

            }
            else {
                $.unblockUI();
                $('#grpTalkCallsList').html("No Grp Talk Calls Found");
                $('#members').html("No Records Found");
            }

        },
        error: function (result) {
            $.unblockUI();
            alert("Something Went Wrong");
        }
    });
}

//--------------------------------------Function Calling on Grptalk call based history details
function getGrpTalkHistoryDetails(grpID, isCreatedBy) {
    var grpcallID = 0;
    var pageindex = 0;
    $.blockUI({
        message: '<h4> Fetching GrpTalk History Records...</h4>'
    });



    if (webPageIndex == 1 || webPageIndex <= webPageCount) {

        $.ajax({
            url: 'HandlersWeb/Groups.ashx',
            type: 'post',
            dataType: 'json',

            data: {
                type: 2,
                grpCallID: grpID,
                pageIndex: webPageIndex,
                pageSize: 4
            },
            success: function (result) {
                var grpTalkHistory = "";
                isSubscribed = result.IsCallSubscribed;
                webPageCount = result.PageCount;
                if (result.History.length > 0) {
                    for (var j = 0; j < result.History.length; j++) {
                        batchIDFirst = result.History[0].BatchID;
						
                        grpcallID = result.History[j].GrpCallID;
                        if (result.History[j].GrpCallID == grpID) {
							
                            if (j == 0) {
                                if (result.History[j].StartTime == "") {
                                    grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
                                    grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">Not Started</div>';
                                } else {
                                    grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
                                    grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">' + formatAMPM(result.History[j].StartTime) + ' on ' + formatForDate(result.History[j].StartTime) + '</div>';
                                }
                                grpTalkHistory += '<div class="col-md-4 text-center"><span style="font-weight: 600; color: #ff8f44; text-align: center">' + result.History[j].Invites + ' invited, ' + result.History[j].Connected + ' attended</span></div>';
                                grpTalkHistory += '<div class="col-md-4 text-right"><span style="color: #20bbff; font-weight: 600;">Duration : ' + result.History[j].Duration + ' minute(s)</span></div></div></h3>';
                                if (isCreatedBy == 1) {
                                    grpTalkHistory += '<div><div class="row accordioncontent" style="text-align:center">Please wait....</div></div>'
                                }
                                else {
                                    grpTalkHistory += '<div><div class="row accordioncontent" style="text-align:center">You dont have permission to view this report</div></div>'
                                }

                            }
                            else {
                                if (result.History[j].StartTime == "") {
                                    grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
                                    grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">Not Started</div>';
                                } else {
                                    grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
                                    grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">' + formatAMPM(result.History[j].StartTime) + ' on ' + formatForDate(result.History[j].StartTime) + '</div>';
                                }
                                grpTalkHistory += '<div class="col-md-4 text-center"><span style="font-weight: 600; color: #ff8f44; text-align: center">' + result.History[j].Invites + ' invited, ' + result.History[j].Connected + ' attended</span></div>';
                                grpTalkHistory += '<div class="col-md-4 text-right"><span style="color: #20bbff; font-weight: 600;">Duration : ' + result.History[j].Duration + ' minute(s)</span></div></div></h3>';
                                if (isCreatedBy == 1) {
                                    grpTalkHistory += '<div class="accordioncontent">Please wait....</div>'
                                }
                                else {
                                    grpTalkHistory += '<div class="accordioncontent" style="text-align:center">You dont have permission to view this report</div>'
                                }
                            }

                        }
                    }

                    if (++webPageIndex <= webPageCount) {

                        $('#more').html('<div id="moreHistory" class="text-center"  groupid="' + grpcallID + '" isCreatedBy="' + isCreatedBy + '" style="margin: 2px 0 0;width:100%;background-color:#323c41 !important;padding: 4px 0;cursor:pointer;">More</div>').show();
                    }
                    else {
                        $('#more').hide();
                    }
                    $.unblockUI();

                    $('#accordion').append(grpTalkHistory);
                    $("#accordion").accordion({
                        activate: function () {
                            global_GroupId = $('.ui-accordion-header-active').attr('groupid');
                            global_BatchId = $('.ui-accordion-header-active').attr('batchid');
                            if ($('.ui-accordion-content-active').length != 0) {
                                if (isCreatedBy == 1) {
                                    getDetailsByBatchID(global_GroupId, global_BatchId, isSubscribed, pageIndex, pageCount);
                                }

                            }
                        },
                        heightStyle: "content",
                        collapsible: true
                    });
                    $('#accordion').accordion('refresh');
                    if (isCreatedBy == 1) {
                        getDetailsByBatchID(grpID, batchIDFirst, isSubscribed, pageIndex, pageCount);
                    }

                }
                else {
                    $.unblockUI();
                    $('#accordion').html("");
                    $('#accordion').append('<br><br><p style="text-align:center;font-size:20px;">No History Found</p>');
                }
            },
            error: function (result) {
                $.unblockUI();
                alert("Something Went Wrong");
            }
        });

    }

}

//--------------------------------------Function Calling on Grptalk call and member details
function getDetailsByBatchID(groupId, batchId, isSubscribed, pageIndex, pageCount) {

    if (pageIndex == 1 || pageIndex <= pageCount) {
        //$('.accordioncontent').removeClass('ui-accordion-content');
        //$('.accordioncontent').html('');
        $.ajax({
            url: '/HandlersWeb/GrpTalkReports.ashx',
            type: 'POST',
            dataType: 'JSON',
            data: { type: 1, groupID: groupId, batchID: batchId, pageIndex: pageIndex },
            success: function (result) {
                pageCount = result.TotalPageCount;
                var participantInfo = "", callsInfoLength = 0; participantsLength = 0, callDetailsLength = 0, grpTalkCallsInfo = "", dataInfo = "";
                participantsLength = result.Items.length;
                callDetailsLength = result.Data2.length;
                var totalMinutes = 0;
                var totalPrice = 0.0;
                if (pageIndex == 1) {


                    grpTalkCallsInfo += ' <div class="col-md-12 margin-bottom-5">';
                    grpTalkCallsInfo += '<ul>';
                    grpTalkCallsInfo += ' <li class="col-md-3 pull-left"><img src="images/Time.png" alt="Time" />' + result.Data.TotalCallDuration + '</li>';
                    grpTalkCallsInfo += '<li class="col-md-4"><img src="images/Duration1.png" alt="Duration" /> Total Minutes Consumed: <span style="color:#27bd7c;font-weight:bold;">' + result.Data.TotalMinutes + '</span></li>';
                    grpTalkCallsInfo += '<li class="col-md-5 pull-left"><img src="images/Rupee.png" alt="Rupee" /> Total Amount Charged: <span style="color:#27bd7c;font-weight:bold;">' + result.Data.Currency + '  ' + result.Data.TotalCallPrice + '</span></li>';
                    grpTalkCallsInfo += '</ul>';
                    grpTalkCallsInfo += '</div></div>';
                    participantInfo += '<div class="col-md-12 margin-bottom-5" id="reportsTb"><div class="reportsTable"> <div class="scroll"><table class="table table-bordered margin-bottom-0" style="width: 70% !important;margin: 0px auto;"><thead><tr><th style="text-align:left !important; background-color: #0a93d7; color:#fff;">Member</th><th style="background-color: #0a93d7; color:#fff;">Duration in Minute(s)</th><th style="background-color: #0a93d7; color:#fff;">Amount Charged ('+currencyName+')</th></tr></thead> <tbody>';
                }
                var participantName = "";
                for (var i = 0; i < participantsLength; i++) {
                    participantName = result.Items[i].Member;
                    if (participantName == "") {
                        participantName = result.Items[i].MobileNumber
                    }
                    if (pageIndex == 1)
                    { particpantMembers = i; }
                    else { particpantMembers++; }
                    participantInfo += '<tr><td style="text-align:left !important; background-color: #f1f1f1;">' + participantName + '</td>';
                    
                    if (result.Items[i].DurationInHours == "") { participantInfo += '<td style="background-color: #f1f1f1;"> 0 </td>'; }
                    else { participantInfo += '<td style="background-color: #f1f1f1;">' + result.Items[i].Duration + '</td>'; }

                    var p = 0;
                    for (var l = 0; l < callDetailsLength ; l++) {
                        if (result.Items[i].MobileNumber == result.Data2[l].Number) {
                            p = p + result.Data2[l].CallPrice;

                        }
                    }

                    participantInfo += '<td style="background-color: #f1f1f1;">' + parseFloat(p) + '</td></tr>';
                }


                if (pageIndex == 1) {
                    participantInfo += ' </tbody></table></div></div></div>';
                    $('.accordioncontent').html(grpTalkCallsInfo + participantInfo);


                    $('.reportsTable').scroll(function () {
                        if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                            pageIndex++;
                            getDetailsByBatchID(grpID, batchIDFirst, isSubscribed, pageIndex, pageCount);
                            var divHeight = $(this).scrollTop();
                            $(this).css("top", divHeight);
                        }
                    });

                    $('.reportsTable').slimScroll({
                        allowPageScroll: true,
                        height: '200'

                    });
                }
                else { $('tbody').append(participantInfo); }
            },
            error: function (result) {
                alert("something WEnt Wrong");
            }
        });
    }
}
function formatAMPM(date) {
    var date1 = new Date(date);
    var hours = date1.getHours();
    var minutes = date1.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

function formatForDate(dateFormat) {
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var date = new Date(dateFormat)
    var day = date.getDate();
    var month = date.getMonth();
    var month_name = monthNames[month];
    var year = date.getFullYear();
    if (month < 10) {
        month = '0' + month;
    }
    return day + " " + month_name + " " + year;
}
// hangupall
$(document).delegate('#hangup', 'click', function (e) {
    //e.preventDefault();
    //$('#hangup').on('click', function (e) {
    $.blockUI({ message: '<h4> HangUp All....</h4>' });
    var grpCallId = 0;
    grpCallId = $(this).attr("grpID");
    var hangupObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"True","MobileNumber":""}';

    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'POST',
        dataType: 'JSON',
        data: { type: 3, hangUpObj: hangupObj },

        success: function (result) {
            if (result.Success == true) {
                window.location.reload();
            }
            $.unblockUI();
        },
        error: function (result) {

            alert("Something Went Wrong");
            $.unblockUI();
            return;
        }

    });


});



$(document).delegate('#mute', 'click', function (e) {
    e.preventDefault();

    var grpCallID = 0;
    var action = $(this).attr('action');

    grpCallID = $(this).attr("grpID");


    if (action == "mute") {
        $.blockUI({ message: '<h4> Mute All....</h4>' });
        muteUnMuteObj = '{"ConferenceID":"' + grpCallID + '","IsAll":"True","MobileNumber":"","IsMute":"True"}';

    }
    else {
        $.blockUI({ message: '<h4> Un Mute All....</h4>' });
        muteUnMuteObj = '{"ConferenceID":"' + grpCallID + '","IsAll":"True","MobileNumber":"","IsMute":"False"}';

    }

    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'POST',
        dataType: 'JSON',
        data: { type: 4, muteUnMuteObj: muteUnMuteObj },
        success: function (result) {

            if (action == "mute") {
                $('#mute').html('');

                //$('#mute').css('background-color', '#f49b01');
                $('#mute').html("<a href='javascript:;'><img width='27' src='images/GroupUnMuteAll.png' alt='Mute' /></a>");
                $('#mute').attr('action', 'unmute');
                if ($('.onCall').hasClass('active')) {
                    $('.table-bordered tr').each(function () {
                        if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
                            $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='UnMute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                        }
                    });
                }

                if ($('.allMembers').hasClass('active')) {

                    $('.table-bordered tr').each(function () {
                        if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
                            $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='UnMute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                        }

                    });
                }
                if ($('.muted').hasClass('active')) {

                    $('.muted').click();
                }
                if ($('.wantsToTalk').hasClass('active')) {

                    $('.wantsToTalk').click();
                }


            }
            else {
                $('#mute').html('');

                //$('#mute').css('background-color', '#1ac3ee');
                $('#mute').html("<a href='javascript:;'><img width='27' src='images/GroupMuteAll.png' alt='Mute' /></a>");
                $('#mute').attr('action', 'mute');
                if ($('.muted').hasClass('active')) {

                    $('.muted').click();
                }
                if ($('.wantsToTalk').hasClass('active')) {

                    $('.wantsToTalk').click();
                }
                if ($('.onCall').hasClass('active')) {
                    $('.table-bordered tr').each(function () {
                        if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
                            $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='Mute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                        }
                    });
                }
                if ($('.allMembers').hasClass('active')) {

                    $('.table-bordered tr').each(function () {
                        if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
                            $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='Mute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                        }

                    });
                }

            }
            $.unblockUI();

        },
        error: function (result) {

            $.unblockUI();


            alert("Something Went Wrong");
        }

    });




});

$('.allMembers').on('click', function (e) {

    GetConferenceRoom(grpID, 1, '', 20, 1);

});
$('.onCall').on('click', function (e) {

    GetConferenceRoom(grpID, 2, '', 20, 1);

});
$('.callEnded').on('click', function (e) {

    GetConferenceRoom(grpID, 3, '', 20, 1);

});
$('.muted').on('click', function (e) {

    GetConferenceRoom(grpID, 4, '', 20, 1);

});
$('.wantsToTalk').on('click', function (e) {

    GetConferenceRoom(grpID, 5, '', 20, 1);

});
// grproom
function GetConferenceRoom(grpCallId, Mode, searchText, pageSize, pageNumber) {
    callDurationInSeconds = 0;
    isTimerStart = 0;
    var tableBody = "";
    var tableHeader = "";
    var totalMembersCount = 0;
    var onCallCount = 0;
    var hangUpCount = 0;
    var muteCount = 0;
    var handraiseCount = 0;
    var hostData = "";
    isCreatedBy = $('#' + grpCallId).attr('isCreated');
    inProgressPageNumber = pageNumber;
    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'POST',
        dataType: 'JSON',
        data: { type: 2, grpCallId: grpCallId, mode: Mode, PageSize: pageSize, PageNumber: pageNumber, SearchText: searchText },
        success: function (result) {
            totalFetchingMembers = pageSize * pageNumber;
            if (isCreatedBy == 1) {
                tableHeader = "<table class='table table-bordered tbl_bg'><thead style='background-color: #455C70;color: #fff;'><tr><th>Name</th><th>Mobile Number</th><th>Status</th><th style='text-align:center;'>Actions</th></tr></thead><tbody>";
            } else {
                tableHeader = "<table class='table table-bordered tbl_bg'><thead style='background-color: #455C70;color: #fff;'><tr><th>Name</th><th>Mobile Number</th><th>Status</th></tr></thead><tbody>";
            }

            if (result.response.status == "true") {

                if (result.result.starttime != "") {

                    callDurationInSeconds = (new Date(result.result.servertime) - new Date(result.result.starttime)) / 1000;

                }
                else {

                    callDurationInSeconds = 0;
                }

                totalMembersCount = result.result.AllMembersCount;
                onCallCount = result.result.OnCallCount;
                hangUpCount = result.result.HangUpCount;
                muteCount = result.result.MuteCount;
                handraiseCount = result.result.HandRaiseCount;
                totalMembers = totalMembersCount;
                $('#allMembersCount').html('(' + totalMembersCount + ')');
                $('#onCallCount').html('(' + onCallCount + ')');
                $('#callEndedCount').html('(' + hangUpCount + ')');
                $('#mutedCount').html('(' + muteCount + ')');
                $('#handRaiseCount').html('(' + handraiseCount + ')');

                mutePercentage = (muteCount * 100) / onCallCount;
                if (mutePercentage >= 50) {
                    $('#mute').attr('action', 'unmute');
                    $('#mute').html('<img src="images/GroupUnMuteAll.png" alt="UnMute" width="27">')
                }
                else {
                    $('#mute').attr('action', 'mute');
                    $('#mute').html('<img src="images/GroupMuteAll.png" alt="UnMute" width="27">')
                }


                for (var i = 0; i < result.result.data.length; i++) {
                    if (result.result.data[i].type == "member") {
                        var stat = result.result.data[i].call_status;
                        if (stat == "default") {
                            stat = "completed"
                        }
                        tableBody = tableBody + "<tr><td>" + result.result.data[i].member + "</td>";
                        tableBody = tableBody + "<td>" + result.result.data[i].to_num + "</td>";
                        tableBody = tableBody + "<td>" + stat + "</td>";
                        if (isCreatedBy == 1) {
                            tableBody = tableBody + "<td style='text-align:center;'>"
                            if (result.result.data[i].call_status == "dialing" || result.result.data[i].call_status == "redialing") {
                                tableBody = tableBody + "<ul><li><a class='singleHangUp' grpCallID=" + grpCallId + " action='Mute' mobileNumber=" + result.result.data[i].to_num + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul></td>";
                            }
                            else if (result.result.data[i].call_status == "inprogress") {
                                if (result.result.data[i].mute == "false") {
                                    tableBody = tableBody + "<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallId + " action='Mute' mobileNumber=" + result.result.data[i].to_num + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li>";
                                }
                                else {
                                    tableBody = tableBody + "<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallId + " action='UnMute' mobileNumber=" + result.result.data[i].to_num + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li>";
                                }

                                tableBody = tableBody + "<li><a class='singleHangUp' grpCallID=" + grpCallId + " mobileNumber=" + result.result.data[i].to_num + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li>";
                                tableBody = tableBody + "</ul></td>"
                            }
                            else {
                                tableBody = tableBody + "<ul><li><a class='singleRedial' grpCallID=" + grpCallId + "  mobileNumber=" + result.result.data[i].to_num + " href='javascript:void(0);'><img width='22' alt='' src='images/Redial.png' /></a></li></ul></td>";
                            }
                        }
                        // else {
                        // tableBody = tableBody + "</td>";
                        // }

                        tableBody = tableBody + "</tr>";
                    }
                    else {
                        if (isCreatedBy == 1) {
                            hostData = "<table class='table host' style='margin-bottom:0;'><tbody>";
                            hostData = hostData + "<tr><td mobilenumber=" + result.result.data[i].to_num + " style='font-weight: bold; color: rgb(10, 147, 215); font-size: 18px;border-top:0;'>" + result.result.data[i].member + "( You )</td>";
                            if (result.result.data[i].call_status == "default") {
                                hostData = hostData + "<td style='text-align:right;border-top:0;'><ul><li><a href='javascript:void(0);' mobilenumber=" + result.result.data[i].to_num + " grpcallid=" + grpCallId + " class='singleRedial'><img width='22' src='images/Redial.png' alt=''></a></li></ul></td></tr></tbody></table>";
                            }
                            else {
                                hostData = hostData + "<td style='text-align:right;border-top:0;'><ul><li><a href='javascript:void(0);' mobilenumber=" + result.result.data[i].to_num + " grpcallid=" + grpCallId + " class='singleHangUp'><img width='22' src='images/individualhangup.png' alt=''></a></li></ul></td></tr></tbody></table>";
                            }
                        }
                        else {
                            hostData = "<table class='table host' style='margin-bottom:0;'><tbody>";
                            hostData = hostData + "<tr><td mobilenumber=" + result.result.data[i].to_num + " style='font-weight: bold; color: rgb(10, 147, 215); font-size: 18px;border-top:0;'>" + result.result.data[i].member + "( Host )</td><td></td>";

                        }

                    }

                }
                if (pageNumber == 1) {

                    $('.tab-content').html('');
                    $('.tab-content').html(tableHeader + tableBody);
                    if (Mode == 1) {
                        $('.moderator').html('');
                        $('.moderator').html(hostData);
                    }
                }
                else {

                    $('.tab-content tr:last').after(tableBody);
                }


                if (callDurationInSeconds == 0) {

                    // $('.timer').timer({
                    // format: '%H:%M:%S',
                    // seconds: "" + callDurationInSeconds
                    // });

                    //$('.timer').timer('pause');
                }
                else {

                    if (isTimerStart == 0) {

                        if (onCallCount > 0) {
                            $('.timer').timer('remove');
                            $('.timer').timer({
                                format: '%H:%M:%S',
                                seconds: "" + callDurationInSeconds
                            });
                            isTimerStart = 1;
                        }
                    }

                }
            }
            else {

                //tableBody += "";
                $('.tab-content').html('');
                $('.tab-content').html(tableHeader);
            }

        },
        error: function (result) {

            alert("Something Went Wrong");
        }


    });


}
$('.tab-content').scroll(function () {

    if ($(this).scrollTop() + $(this).innerHeight() + 1 >= $(this)[0].scrollHeight) {


        if (totalFetchingMembers < totalMembers) {
            inProgressPageNumber = inProgressPageNumber + 1;
            if ($('.allMembers').hasClass('active')) {
                GetConferenceRoom(grpID, 1, $('#memberSearch').val(), 20, inProgressPageNumber);
            }
            else if ($('.onCall').hasClass('active')) {
                GetConferenceRoom(grpID, 2, $('#memberSearch').val(), 20, inProgressPageNumber);
            }
            else if ($('.callEnded').hasClass('active')) {
                GetConferenceRoom(grpID, 3, $('#memberSearch').val(), 20, inProgressPageNumber);
            }
            else if ($('.muted').hasClass('active')) {
                GetConferenceRoom(grpID, 4, $('#memberSearch').val(), 20, inProgressPageNumber);
            }
            else {
                GetConferenceRoom(grpID, 5, $('#memberSearch').val(), 20, inProgressPageNumber);
            }
        }
        // pageIndex++;
        // getDetailsByBatchID(grpID, batchIDFirst, isSubscribed, pageIndex, pageCount);
        // var divHeight = $(this).scrollTop();
        // $(this).css("top", divHeight);
    }
});
$(document).delegate('.singleHangUp', 'click', function () {

    var grpCallId = 0;
    var mobileNumber = "";
    var hangUpObj = "";
    grpCallId = $(this).attr("grpCallId");
    mobileNumber = $(this).attr("mobileNumber");
    $(this).addClass('active');
    hangUpObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"False","MobileNumber":"' + mobileNumber + '"}';
    if (!$(this).parents('table').hasClass('host')) {
        if ($('.muted').hasClass('active') || $('.wantsToTalk').hasClass('active')) {
            $(this).parents('tr').remove();
        }
    }

    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'POST',
        dataType: 'JSON',
        data: { type: 3, hangUpObj: hangUpObj },
        success: function (result) {
            if (result.Success == true) {


            }

        },
        error: function (result) {
            $.unblockUI();

            alert("Something Went Wrong");
        }

    });



});

$(document).delegate('.singleMuteUnMute', 'click', function () {
    var grpCallId = 0;
    var mobileNumber = "";
    var muteUnMuteObj = "";
    var action = "";
    $(this).addClass('active');
    grpCallId = $(this).attr("grpCallId");
    mobileNumber = $(this).attr("mobileNumber");




    action = $(this).attr("action");
    if (action == "Mute") {
        muteUnMuteObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"False","MobileNumber":"' + mobileNumber + '","IsMute":"True"}';
    }
    if (action == "UnMute") {
        muteUnMuteObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"False","MobileNumber":"' + mobileNumber + '","IsMute":"False"}';
    }


    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'POST',
        dataType: 'JSON',
        data: { type: 4, muteUnMuteObj: muteUnMuteObj },
        success: function (result) {
            if (result.Success == true) {


                $('.singleMuteUnMute').each(function () {
                    if ($(this).hasClass('active')) {

                        if (action == "Mute") {
                            $(this).html("<img grpCallId=" + grpCallId + " mobileNumber=" + mobileNumber + " width='32' alt='' src='images/SingleUnMute.png' />")
                            $(this).attr('action', 'UnMute')
                        }
                        if (action == "UnMute") {
                            $(this).html("<img grpCallId=" + grpCallId + " mobileNumber=" + mobileNumber + " width='17' alt='' src='images/SingleMute.png' />")
                            $(this).attr('action', 'Mute')
                        }
                        $(this).removeClass('active');
                    }


                });

            }
        },
        error: function (result) {

            alert("Something Went Wrong");
        }

    });



});

$(document).delegate('.singleRedial', 'click', function () {
    var grpCallId = $(this).attr('grpCallId');
    var mobileNumber = $(this).attr("mobileNumber");
    var suscess = 0;
    var dialObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"False","MobileNumber":"' + mobileNumber + '","IsCallFromBonus":"0","IsMute" : "False"}';
    if (!$(this).parents('table').hasClass('host')) {
        $(this).parent().parent().parent('td').prev('td').html('dialing');
    }

    $(this).parent().parent().html("<li><a class='singleHangUp' grpCallID=" + grpCallId + "  mobileNumber=" + mobileNumber + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li>");

    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'POST',
        async: false,
        data: { type: 1, dialObj: dialObj },
        dataType: 'JSON',
        success: function (result) {

            success = 1;

        },
        error: function (result) {
            alert("Something Went Wrong");
        }
    });
    if ($('.callEnded').hasClass('active')) {
        if (success == 1) {
            $(this).parent().parent().parent().parent().remove();
        }
    }

});

$(document).click(function () {
    $('#searchBtn').css("border-color", "none");
});
$(document).delegate("#cancelGroupCall", "click", function () {

    var grpCallId = $('.list.active').attr('id');
    var x = confirm("Are you sure you want to cancel Group call");
    if (x == true) {
        $.ajax({
            url: '/HandlersWeb/Groups.ashx',
            type: 'POST',
            async: false,
            data: { type: 9, grpCallId: grpCallId },
            dataType: 'JSON',
            success: function (result) {
                if (result.Success == true) {
                    var month = new Array();
                    month = result.LatCallDate.split(" ");;
                    $('#cancelGroupCall').hide();
                    alert("You have been Cancel the Group Call Successfully");
                    $('.list.active[id=' + grpCallId + ']').find("#date").html('<p style="font-family: Impact; font-size: 40px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p><p style="font-family: Calibri; font-size: 18px; line-height: 20px;">' + month[0] + '</p>')


                }
            },
            error: function (result) {
                alert("Something Went Wrong");
            }

        });
    }
    else {
        return false;
    }
});

$(document).delegate('#addMember', 'click', function (e) {
    e.preventDefault();
    grpCallID = $(this).attr("grpID");

    $('#contactsModalDiv').modal('show');
    $('.mobileContacts').click();
    mode = 1;
    $('#addCall').attr('grpCallID', grpCallID);
    $('.contacts').removeClass('selected');
    $('.contacts').find('.fa-check').css('display', 'none');
    $('#selectedContacts').html('');
    countContacts = 0;
    $('.count').html('(' + countContacts + ')');
});

$('#addCall').on('click', function () {

    grpCallID = $(this).attr('grpCallID');
    //alert(grpCallID);
    //  {"ConferenceID":"186410","Participants":[{"praveen smsc":"8341962057"}],"WebListIds":""}
    memberName = "", memberMobile = "", resStr = "", response = "";
    participants = "";

    $('.contacts').each(function () {
        if ($(this).hasClass('selected')) {
            memberName = $(this).find('p:eq(0)').text();
            memberMobile = $(this).find('p:eq(1)').text();

            if ($(this).attr('listId') == "0") {
                listIds = "";
            }
            participants += '{"' + memberName + '":"' + memberMobile + '"}' + ",";
        }
    });

    resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";


    response = '{"ConferenceID":"' + grpCallID + '",' + resStr + ',"WebListIds":""}';

    addMemeberWhileOnCall(response, mode);
});

function addMemeberWhileOnCall(paramObj, modeValue) {

    $.ajax({
        url: '/HandlersWeb/Groups.ashx',
        method: 'POST',
        dataType: 'JSON',
        data: {
            type: 8,
            paramObj: paramObj
        },
        success: function (result) {
            if (result.Success == false) {
                Notifier(result.Message, 2);
                return;
            }
            else {
                if (modeValue == 1) {
                    $('#contactsModalDiv').modal('hide');
                    $('#dialGroupCall').click();
                }
                else {
                    setTimeout(window.location.reload(), 1000);
                }
            }

            // if (modeValue == 1) {
            // $('#contactsModalDiv').modal('hide');
            // $('#dialGroupCall').click();
            // }
            // else {
            // setTimeout(window.location.reload(), 1000);
            // }

        },
        error: function (result) {
            alert("Something Went Wrong");
        }
    });
}

$(document).delegate("#editGroupCall", "click", function () {
    //$('#editGroupCall').click(function () {
    var x = confirm("Are you sure to edit the Group call");
    if (x == true) {
        $('div .margin-bottom-1').each(function () {

            if ($(this).hasClass("active")) {
                var grpCallHighlight = $(this);
                var grpName = grpCallHighlight.attr("groupName");
                var grpId = grpCallHighlight.attr("id");
                var grpParticipants = grpCallHighlight.attr("participant");
                var grpCallDate = grpCallHighlight.attr("date");
                var schType = grpCallHighlight.attr("schType");
                var schTime = grpCallHighlight.attr("schTime");
                var isMuteDail = grpCallHighlight.attr("isMuteDail");
                var weekDays = grpCallHighlight.attr("weekDays");
                var reminder = grpCallHighlight.attr("reminder");
                var participants = grpCallHighlight.attr("mobileNumbers")
                var grpIdHistory = new Array();
                grpIdHistory = [grpId, grpName, grpCallDate, grpParticipants];

                var grpIdHistoryJson = '{"Groups": [{"GroupID":"' + grpID + '","GroupCallName":"' + grpName + '","SchType":"' + schType + '","SchduledTime":"' + schTime + '","SchduledDate":"' + grpCallDate + '","IsMuteDial":"' + isMuteDail + '","WeekDays":"' + weekDays + '","Reminder":"' + reminder + '","GrpParticipants":[{"Participants":"' + participants + '"}]}]}';

                localStorage.myArray = JSON.stringify(grpIdHistoryJson);
                //window.location.replace("/EditGroupTalk.aspx");
                window.location.replace("/EditGroup.aspx");
            }
        });

    }
    else {
        return false;
    }
});

function sortArray(groups) {

    var result = new Array();
    result = groups;

    //var  schTypeArray=new Array();
    //schTypeArray.push(result.grep(schType=0));
    // result.sort(function (a, b) {
    // if (a.SchType == 100 && b.SchType == 100) {
    // var dateA = new Date(a.StartDateTime), dateB = new Date(b.SchduledDate)
    // return dateB - dateA //sort by date ascending
    // }
    // })
    // result.sort(function (a, b) {
    // if (a.SchType != 100 && b.SchType != 100) {
    // var dateA = new  Date(a.SchduledDate + " " + a.SchduledTime), dateB = new Date(b.SchduledDate + " " + b.SchduledTime)
    // return dateB - dateA //sort by date ascending
    // }
    groups.sort(function (a, b) {
        return new Date(b.StartDateTime).getTime() - new Date(a.StartDateTime).getTime()

    });

    return groups;




}

function getScheduleCalls(array) {
    var scheduleCalls = $.grep(array, function (a, i) {
        if (a.IsStarted != 1) {
            return new Date() < new Date(a.StartDateTime);
        }
    });


    // return scheduleCalls;

    // scheduleCalls.sort(function(a,b){
    // return   b.IsStarted - a.IsStarted;

    // });
    return scheduleCalls;
}

function getPastCalls(array) {
    var pastCall = $.grep(array, function (a, i) {
        if (a.IsStarted != 1) {
            return new Date(a.StartDateTime) < new Date();

        }
    });


    return pastCall;

}

function isSatrted(array) {
    var pastCall = $.grep(array, function (a, i) {
        return a.IsStarted == 1;
    });

    return pastCall;
}

function ConnectToWebSocket(conferenceroom,isRetry){
		host = ((location.protocol === 'https:') ? 'wss://' : 'ws://') + location.host + ((location.port != '') ? (':' + location.port) : '') + '/ConferenceWebSocket.sub?room=';
		console.log(host)
		connection=new WebSocket(host+conferenceroom);
		
		connection.onopen=function(){
			console.log('open');
			$('#wcreconnect').hide();
			if (isRetry) {
				clearInterval(refreshIntervalId);
				connection.onclose=function(){
						console.log("Connection Close");
						$('#wcreconnect').show();
						refreshIntervalId =setInterval(function(){ConnectToWebSocket(conferenceroom,true);},4000);
				}
				connection.onerror=function(){
					$('#wcreconnect').show();
					console.log("Connection Error");
					//refreshIntervalId =setInterval(function(){ConnectToWebSocket(conferenceroom);},4000);
				}
				connection.onmessage=function(message){
					onMessageDef(conferenceroom,message);
				}
			}
			
			
		}
		
}

function onMessageDef(conferenceroom,message) {
			
}	


