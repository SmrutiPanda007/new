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

$(document).ready(function () {

    getListOfGrpCalls();
	
    //$('.CallLogName').text($('#grpDetails:eq(0)').attr('grpcallname'));
    $('#grpTalkCallsList .list').removeClass('active');
    $('#grpCallMobileContacts').slimScroll({
        allowPageScroll: false,
        height: '410'
    });


   

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
	
	$(document).delegate("#quickDial", "click", function (e) {
		$('.list').removeClass('active');
		$('#'+$(this).attr('grpId')).addClass('active');
		$('#dialGroupCall').click();
		$(this).remove();
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
                            memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].Participants[k].Name.length > 14)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 14) + '...</p><p><i class="fa fa-mobile" aria-hidden="true"></i>' + "  " + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>'; }

                            else
                            { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p><p><i class="fa fa-mobile" aria-hidden="true"></i>' + "  " + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>'; }
                        
					
						}
						
                    }
					 for (var l = 0; l < jsonParticipants.Groups[i].LeaveParticipants.length; l++) {
							
							 if (jsonParticipants.Groups[i].GroupID == grpID) {
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 9)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 9) + '...</p>'; }
                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                            }
                            memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                            memebersDetails += '<label class="left-label"><img src="images/left-group.png" title="Left" alt="left"></label></div>';
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
                        memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                        if (jsonParticipants.Groups[i].Participants[k].Name.length > 14) { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 14) + '...</p>'; }
                        else { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p>'; }

                        memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>';

                    }


                }
                else {

					if (jsonParticipants.Groups[i].Participants[k].GroupId == grpID) {
						if ((jsonParticipants.Groups[i].Participants[k].Name.toLowerCase()).indexOf(nameSearch) > -1 || (jsonParticipants.Groups[i].Participants[k].Mobile).indexOf(nameSearch) > -1) {
							searchcount = 1;
							memebersDetails += '<div class="col-md-3 contactDetails margin-right-5 margin-bottom-5">';
							memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
							if (jsonParticipants.Groups[i].Participants[k].Name.length > 17) {
								memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].Participants[k].Name + '">' + jsonParticipants.Groups[i].Participants[k].Name.substring(0, 16) + '...</p>';
							}
							else { memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].Participants[k].Name + '</p>'; }
							memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].Participants[k].Mobile + '</p></div></div>';
						}
					}

                }
            }

            for (var l = 0; l < jsonParticipants.Groups[i].LeaveParticipants.length; l++) {
                if (jsonParticipants.Groups[i].GroupID == grpID) {
                    if (nameSearch == "") {
                        if (jsonParticipants.Groups[i].LeaveParticipants.length != 0) {
									searchcount = 1;
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 9)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 9) + '...</p>'; }
                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                            }
                            memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                            memebersDetails += '<label class="left-label"><img src="images/left-group.png" title="Left" alt="left"></label></div>';




                        }
                    }
                    else {
                        if ((jsonParticipants.Groups[i].LeaveParticipants[l].Name.toLowerCase()).indexOf(nameSearch) > -1 || (jsonParticipants.Groups[i].LeaveParticipants[l].Mobile).indexOf(nameSearch) > -1) {
							searchcount = 1;
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                            if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 9)
                            { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 9) + '...</p>'; }                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                            }
                            memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                            memebersDetails += '<label class="left-label"><img src="images/left-group.png" title="Left" alt="left"></label></div>';
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
                //$('#' + grpCallId).find('#date').html('<p style="font-family: Impact; font-size: 30px; line-height: 56px;color:#27bd7c;" class="'+grpCallId+'blink_me">Live</p><p style="font-family: Impact; font-size: 15px; line-height: 56px;color:#27bd7c;display:none" class="'+grpCallId+'blink_me2">'+$('.'+grpCallId+'timer').text()+'</p>');
                
                $('#' + grpCallId).find('#date').html('<div style="height:65px;padding-top:22px"><label style="border-radius:3px; background:#F95259; font-size: 12px; padding:5px; color:#fff; font-weight:normal;" class="' + grpCallId + 'blink_me">Live</label></div>');
				displayInprogressBar =1;
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
                        Notifier("Deleted Successfully",1);
						if($('.list').length == 0)
						{
							$('#listTitle').html('');
						}

                    }
                },
                error: function (result) {
                    Notifier("Something Went Wrong",2);
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
                        if($('.list').length == 0)
						{
							$('#listTitle').html('');
						}
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

            userName=result.UserName;
			
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
                    var participant1 = lengthTrim(participantArray[0])					
                    var participant2 = lengthTrim(participantArray[1]);
					var participant3 = lengthTrim(participantArray[2]);
                    partcipantNames = ""; partcipantNames = ""; partcipants = "";
                    for (var k = 0; k < count; k++) {
                        partcipantNames = result.Groups[i].Participants[k].Name;
                        participantNumbers = result.Groups[i].Participants[k].Mobile;
                        partcipants += partcipantNames + "~" + participantNumbers + "‰";
                    }
                    if (i == 0) {


                        grpTalkCallData += '<div style="cursor: pointer;" class="list margin-bottom-1 active" isAllowNonMembers = "'+result.Groups[i].IsAllowNonMembers+'" isOnlyDialIn="'+result.Groups[i].IsOnlyDialIn+'" startDateTime="' + result.Groups[i].StartDateTime + '" grpCallRoom="' + result.Groups[i].GrpCallRoom + '" isStarted="' + result.Groups[i].IsStarted + '" iscreated="' + result.Groups[i].IsCreated + '" id="' + result.Groups[i].GroupID + '" groupName="' + groupName + '" date="' + result.Groups[i].SchduledDate + '" participant="' + result.Groups[i].ParticipantNames + '" schType="' + result.Groups[i].SchType + '" schTime="' + result.Groups[i].SchduledTime + '" isMuteDail="' + result.Groups[i].IsMuteDial + '" weekDays="' + result.Groups[i].WeekDays + '" reminder="30" mobileNumbers="' + partcipants + '" participantLength="' + result.Groups[i].Participants.length + '" isOnlyDialIn="' + result.Groups[i].IsOnlyDialIn + '" isAllowNonMems="' + result.Groups[i].IsAllowNonMembers + '" pin="' + result.Groups[i].GroupCallPin + '" openLineBefore="' + result.Groups[i].OpenLineBefore + '" createdBy="'+result.Groups[i].CreatedByMobile+'">';
                    }
                    else {
                        grpTalkCallData += '<div style="cursor: pointer;" class="list margin-bottom-1" isAllowNonMembers = "'+result.Groups[i].IsAllowNonMembers+'" isOnlyDialIn="'+result.Groups[i].IsOnlyDialIn+'" startDateTime="' + result.Groups[i].StartDateTime + '" grpCallRoom="' + result.Groups[i].GrpCallRoom + '" isStarted="' + result.Groups[i].IsStarted + '" iscreated="' + result.Groups[i].IsCreated + '" id="' + result.Groups[i].GroupID + '" groupName="' + groupName + '" date="' + result.Groups[i].SchduledDate + '" participant="' + result.Groups[i].ParticipantNames + '" schType="' + result.Groups[i].SchType + '" schTime="' + result.Groups[i].SchduledTime + '" isMuteDail="' + result.Groups[i].IsMuteDial + '" weekDays="' + result.Groups[i].WeekDays + '" reminder="30" mobileNumbers="' + partcipants + '" participantLength="' + result.Groups[i].Participants.length + '" isOnlyDialIn="' + result.Groups[i].IsOnlyDialIn + '" isAllowNonMems="' + result.Groups[i].IsAllowNonMembers + '" pin="' + result.Groups[i].GroupCallPin + '" openLineBefore="' + result.Groups[i].OpenLineBefore + '" createdBy="'+result.Groups[i].CreatedByMobile+'">';
                    }
					$('.tab-content').css("height","420px !important");
					$('.tab-content').parents('.slimScrollDiv').css("height","420px !important");
                    //grpTalkCallData += '<div class="row"> <div class="col-md-2 col-sm-2 padding-left-0 padding-right-0 calendar" id="date">';
					grpTalkCallData += '<table class="table margin-bottom-0"><tr><td id="date" style="width:63px; verticle-align:middle; text-align:center;">';

                    if (result.Groups[i].IsStarted == 1) {  //-------live call
                        //grpTalkCallData += '<div style="height:65px;padding-top:22px"><p style="font-family: Impact; font-size: 1.4em; height:40px; color:#fff;" class="' + result.Groups[i].GroupID + 'blink_me">Live</p><p style="font-family: Impact; font-size: 12px;color:#fff;display:none;height:40px;" class="' + result.Groups[i].GroupID + 'blink_me2">' + $('.' + result.Groups[i].GroupID + 'timer').text() + '</p></div></td>';
                       
                        grpTalkCallData += '<div style="height:65px;padding-top:22px"><label style="border-radius:3px; background:#F95259; font-size: 12px; padding:5px; color:#fff; font-weight:normal;" class="' + result.Groups[i].GroupID + 'blink_me">Live</label></div>';
						$('.tab-content').css("height","350px !important");
						}
                    else if (result.Groups[i].SchType != 100) {
                        if (result.Groups[i].SchType == 0) {
                            if (new Date(result.Groups[i].StartDateTime) >= getGrpTalkUserDateTime()) {  //------upcoming call

                                grpTalkCallData += '<p style="font-size: 28px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                                grpTalkCallData += '<p style="font-size: 15px; line-height: 20px;">' + month[0] + '</p></td>';

                            }
                            else {  //---------past call
                                grpTalkCallData += '<p style="font-size: 28px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                                grpTalkCallData += '<p style="font-size: 15px; line-height: 20px;">' + month[0] + '</p></td>';
                            }

                        }
                        else {  //---------upcoming call
                            grpTalkCallData += '<p style="font-size: 28px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                            grpTalkCallData += '<p style="font-size: 15px; line-height: 20px;">' + month[0] + '</p></td>';
                        }
                        //grpTalkCallData += '<div class="col-md-10 col-sm-10 padding-left-0 padding-right-0 participantsList" id="grpDetails" grpcallname="' + result.Groups[i].GroupName + '" >';
                        //grpTalkCallData += '<p style="font-family: Calibri; color: #0a93d7; font-size: 18px; font-weight: bold;">' + groupName + '</p>';

                    }
                    else {  //--------past call
                        grpTalkCallData += '<p style="font-size: 28px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p>';
                        grpTalkCallData += '<p style="font-size: 15px; line-height: 20px;">' + month[0] + '</p></td>';
                    }
                    grpTalkCallData += '<td style="width:150px;" class="grpList_'+result.Groups[i].GroupID+'" id="grpDetails" grpcallname="' + result.Groups[i].GroupName + '" >';
                    grpTalkCallData += '<p style="line-height:18px;">' + groupName + '</p>';

                    if (count > 2) {
                        var disp_array = new Array();
                        for (var j = 2; j < count; j++) {
                            disp_array += '' + result.Groups[i].Participants[j].Name + '';
                            var disp_cnt = count - 1;
                        }						
                        grpTalkCallData += '<p>';
						 
						if(result.Groups[i].IsCreated==0)
						{
							grpTalkCallData+= lengthTrim(result.Groups[i].CreatedBy) + '*, '+ participant1.substring(0, 20) + ',.. <span style="font-size:12px;">' + disp_cnt + ' More...</p></td>';
						}
						else{
                          disp_cnt -=1 ;
						  grpTalkCallData+= participant1+ ', ' + participant2 + ',.. <span style="font-size:12px;">' + disp_cnt + ' More...</p></td>';
						}
                    }
                    else {
                    var moreText=''; 						
                    grpTalkCallData += '<p style="font-size:12px;">';
					if(result.Groups[i].IsCreated==0)
					{
						if(participant2!='')
							moreText+=",.. 1 More...";
					grpTalkCallData += lengthTrim(result.Groups[i].CreatedBy) + '*, ';
					}
				    if(participant1=='')
						grpTalkCallData+= ' </p></td>';
					else if(participant2=='' || (result.Groups[i].IsCreated==0))
    				grpTalkCallData+= participant1 + moreText+' </p></td>';
			     	else
			    	grpTalkCallData+= participant1 + ', '+participant2 + ' </p></td>';
				
					}
                    if ($.trim(result.Groups[i].IsCreated) == 1 && $.trim(result.Groups[i].IsStarted) != 1) {

                        grpTalkCallData += '<td style="text-align:right;"><span title="Quick Dial" id="quickDial" title="Quick Dial" grpId="' + result.Groups[i].GroupID + '"  style="text-align: left ! important; cursor: pointer;  font-size:1.5em;"><img src="images/call-green-border.png" width="30" alt=""/></span></td>';


                    }
					else
						grpTalkCallData += '<td></td>';

                    grpTalkCallData += '</tr></table>';
                    grpTalkCallData += '</div>';


                    for (var k = 0; k < count; k++) {

                        if (grpID == result.Groups[i].Participants[k].GroupId) {
                            memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                            memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                            if (result.Groups[i].Participants[k].Name.length > 14)
                            { memebersDetails += '<div id="profileDetails"><p title="' + result.Groups[i].Participants[k].Name + '">' + result.Groups[i].Participants[k].Name.substring(0, 14) + '...</p>'; }
                            else {
                                memebersDetails += '<div id="profileDetails"><p>' + result.Groups[i].Participants[k].Name + '</p>';
                            }
                            memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Groups[i].Participants[k].Mobile + '</p></div>';
                            memebersDetails += '</div>';
                        }

                    }

                    if (result.Groups[i].LeaveParticipants.length != 0) {
                        if (result.Groups[i].GroupID == grpID) {
                            for (var l = 0; l < result.Groups[i].LeaveParticipants.length; l++) {
                                memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                                memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                                if (result.Groups[i].LeaveParticipants[l].Name.length > 9)
                                { memebersDetails += '<div id="profileDetails"><p title="' + result.Groups[i].LeaveParticipants[l].Name + '">' + result.Groups[i].LeaveParticipants[l].Name.substring(0, 9) + '...</p>'; }
                                else {
                                    memebersDetails += '<div id="profileDetails"><p>' + result.Groups[i].LeaveParticipants[l].Name + '</p>';
                                }
                                memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                                memebersDetails += '<label class="left-label"><img src="images/left-group.png" title="Left" alt="left"></label>,/div>';


                            }
                        }

                    }
                }
				$('.grpList_'+grpID).addClass('participantnames');
                $.unblockUI();
				function switchIn()
				{
					//$('.'+grpID+'blink_me').toggle();
					//$('.'+grpID+'blink_me2').toggle();
					//$('.'+grpID+'blink_me2').text($('.'+grpID+'timer').text());
				}
				setInterval(function(){switchIn();},1000)
                $('#members').html(memebersDetails);
                $('#grpTalkCallsList').html(grpTalkCallData);
                $(document).delegate(".list", "click", function () {
					$('.bschedule').html('');
					$('.participantnames').removeClass('participantnames');
                    var Actions = "";
                    var grpCallName = "", inProgressStr = "", hostActions = "";
                    memebersDetails = "", participants = "";

                    $('#searchByName').val('');
                    $('#more').hide();
                    $('#inputSearch, #searchByName').show();
                    $('#grpTalkCallsList .list').removeClass('active');
                    $(this).addClass('active');
					$(this).find('#grpDetails').addClass('participantnames')

                    grpCallName = $(this).attr('groupname');
                    schType = $(this).attr('schType');
                    isCreatedBy = $(this).attr('iscreated');
                    participants = $(this).attr('participantLength');
                    participantLength = $(this).attr('participantlength');
                    $('#membersCount').html(participants + ' ')
					if(participants == 1)
					{
						$('#mem').html(participants + ' Member');
					}else{
						$('#mem').html(participants + ' Members');
					}
					if(schType == 1){
						
					}
					
                    if (isCreatedBy == 0) {
                        
                        Actions = '<li><img src="/images/leavegroup.png" id="leaveGroupCall" title="Leave Group" style="cursor: pointer;"/>';
						if(schType == 0)
						{
							$('.bschedule').html($(this).attr('date')+' @ '+ $(this).attr('schtime'));
						}
						if(schType == 1)
						{
							var array = $(this).attr('weekdays').split(",");
								var repeatString="";
								$.each(array,function(i){
									var str=array[i];
									repeatString=repeatString + str.substring(0,1)+" ";
								   
								});
								$('.bschedule').html($(this).attr('date')+' @ '+ $(this).attr('schtime')+' - '+repeatString);
						}
                    }
                    else if (isCreatedBy == 1) {

                        $('#hostActions').show();
                        
                        Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-trash-o" id="deleteGroupCall" title="Delete" style="cursor: pointer;"></i>';
                        if (schType == 0 || schType == 1) {
							if(schType == 0)
							{
							    if (new Date($(this).attr('startDateTime')) > getGrpTalkUserDateTime()) {
									Actions += '<li><i aria-hidden="true" style="font-size:18px;" class="fa fa-calendar" id="cancelGroupCall" title="Cancel Schedule"></i></li>'
									$('.bschedule').html($(this).attr('date')+' @ '+ $(this).attr('schtime'));
								}
								
							}
							else{
							    if (new Date(formatForDate($(this).attr('startDateTime')) + " " + formatAMPM($(this).attr('startDateTime'))) >= new Date(formatForDate(getGrpTalkUserDateTime()) + " " + formatAMPM(getGrpTalkUserDateTime()))) {
									Actions += '<li><i aria-hidden="true" style="font-size:18px;cursor:pointer" class="fa fa-calendar" id="cancelGroupCall" title="Cancel Schedule"></i></li>'
								}
								var array = $(this).attr('weekdays').split(",");
								var repeatString="";
								$.each(array,function(i){
									var str=array[i];
									repeatString=repeatString + str.substring(0,1)+" ";
								   
								});
								$('.bschedule').html($(this).attr('date')+' @ '+ $(this).attr('schtime')+' - '+repeatString);
							}
							
                            

                        }
                        Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-pencil" id="editGroupCall" title="Edit" style="cursor: pointer;"></i></li>';
                        Actions += '<li><i aria-hidden="true" style="font-size:21px;" class="fa fa-phone" id="dialGroupCall" title="Dial" style="cursor: pointer;"></i></li>';

                    }

                    $('#groupCallButtons').html(Actions);
                    isStarted = $(this).attr('isstarted');

                    grpID = $(this).attr("id");
                    if (grpID == "352449") {
                        isStarted = 1;
                    }
                    if (isStarted == 1) {
						$('#startTmier').html('<div class="'+grpID+'timer">00:00:00</div>');
						$('#myTab li').removeClass('active');
                        //var pusher = new Pusher("ed522d982044e2680be6");
                        var pusher = new Pusher("ed522d982044e2680be6");
                        //alert(pusherChannel);
                        if (pusherChannel != "") {

                            pusher.unsubscribe(pusherChannel)
                        }
                        //isCreatedBy =1;
                        if (isCreatedBy == 0) {
							$('#inProgressActions').remove()
                            $('.tab-content').slimScroll({
                                allowPageScroll: true,
                                height: '500'
                            });
                        }
                        else {
							hostActions="";
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
                        $('#listTitle1').css("background-color", "#fff !important");
                        // pusherbind
                        var number = "";
                        var status = "";
                        var actionsBody = ""


                        var channel_name = $(this).attr('grpCallRoom');
                        pusherChannel = channel_name;
                        var channel = pusher.subscribe(channel_name.toString());
                        var redialreason = "";
                        var grpCallID = 0;

                        channel.bind('call_status', function (res) {


                            grpCallID = res.conf_id;
                            console.log(res);
                            number = res.to_num;
                            status = res.call_status;
                            //$('#' + grpCallID).attr('isStarted', 1);
                            //$('#' + grpCallID).find('#date').html('');
                            //$('#' + grpCallID).find('#date').html('<p style="font-family: Impact; font-size: 30px; line-height: 56px;color:#27bd7c;" class="blink_me">Live</p>');
                            //setTimeout(function () {
                            //    $('#' + grpCallID).trigger('click');
                            //}, 100);
                            $('#allMembersCount').html('(' + res.AllMembersCount + ')');
                            $('#onCallCount').html('(' + res.OnCallCount + ')');
                            $('#callEndedCount').html('(' + res.HangUpCount + ')');
                            $('#mutedCount').html('(' + res.MuteCount + ')');
                            $('#handRaiseCount').html('(' + res.HandRaiseCount + ')');


                            if (res.isinprogress == 0) {
                                setTimeout(function () {
                                    window.location.reload();
                                }, 10000);

                            }
                            mutePercentage = (res.MuteCount * 100) / res.OnCallCount;
                            if (mutePercentage >= 50) {
                                $('#mute').attr('action', 'unmute');
                                $('#mute').html('<img src="images/GroupUnMuteAll.png" alt="UnMute" width="27">')
                            }
                            else {
                                $('#mute').attr('action', 'mute');
                                $('#mute').html('<img src="images/GroupMuteAll.png" alt="UnMute" width="27">')
                            }

                            if (status.toLowerCase() == "inprogress") {
                                if (isTimerStart == 0) {
                                    $('.'+grpCallID+'timer').timer('remove');
                                    $('.'+grpCallID+'timer').timer({
                                        format: '%H:%M:%S',
                                        seconds: "" + 0
                                    });
                                    isTimerStart = 1;
                                }
                            }

                            if ($('.host').find('td:eq(0)').attr('mobilenumber') == number) {
                                if (isCreatedBy == 1) {
                                    if (status.toLowerCase() == "completed") {
                                        actionsBody = "<ul><li><a class='singleRedial' grpCallID=" + res.conf_id + "  mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/Redial.png' /></a></li></ul>";
                                    }
                                    else {
                                        actionsBody = "<ul><li><a class='singleHangUp' grpCallID=" + res.conf_id + "  mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>";

                                    }
                                    $('.host').find('td:eq(1)').html(actionsBody);
                                }
                            }




                            $('.table-bordered tr').each(function () {

                                var responseNumber = $(this).find('td:eq(1)').text();

                                if ($.trim(responseNumber) == number) {

                                    $(this).find('td:eq(2)').text(status);

                                    if (isCreatedBy == 1) {

                                        if (status.toLowerCase() == "inprogress") {
                                            if ($('.callEnded').hasClass('active')) {
                                                $(this).remove();
                                            }

                                            else {

                                                if (res.mute == false) {
                                                    actionsBody = "<ul><li><a class='singleMuteUnMute' grpCallID=" + res.conf_id + " action='Mute' mobileNumber=" + number + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li>";
                                                }
                                                else {

                                                    actionsBody = "<ul><li><a class='singleMuteUnMute' grpCallID=" + res.conf_id + " action='UnMute' mobileNumber=" + number + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li>"
                                                }
                                                actionsBody = actionsBody + "<li><a class='singleHangUp' grpCallID=" + res.conf_id + " mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>"
                                            }
                                        }
                                        if (status.toLowerCase() == "redial") {

                                            actionsBody = "<ul><li><a class='singleHangUp' grpCallID=" + res.conf_id + " mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>"
                                        }
                                    }
                                    if (status.toLowerCase() == "completed") {
                                        if ($('.allMembers ').hasClass('active')) {
                                            actionsBody = "<ul><li><a class='singleRedial' grpCallID=" + res.conf_id + "  mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/Redial.png' /></a></li></ul>";
                                        }
                                        if ($('.onCall').hasClass('active')) {
                                            $(this).remove();
                                        }
										if ($('.wantsToTalk').hasClass('active')) {
                                            $(this).remove();
                                        }
										if ($('.muted').hasClass('active')) {
                                            $(this).remove();
                                        }
                                        if ($('.callEnded').hasClass('active')) {
                                            if (isCreatedBy == 1) {
                                                $('table table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td><td style="text-align:center;"><ul><li><a class="singleRedial" grpCallID="' + res.conf_id + '"  mobileNumber="' + number + '" href="javascript:void(0)";><img width="22" alt="" src="images/Redial.png" /></a></li></ul></td>');
                                                actionsBody = "<ul><li><a class='singleRedial' grpCallID=" + res.conf_id + "  mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/Redial.png' /></a></li></ul>";
                                            }
                                            else {
                                                $('table table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td>');
                                            }
                                        }

                                    }

                                    $(this).find('td:eq(3)').html('');
                                    $(this).find('td:eq(3)').html(actionsBody);
                                }



                            });

                            var loop = 0;
                            $('.table-bordered tr').each(function () {

                                var responseNumber = $(this).find('td:eq(1)').text();

                                if ($.trim(responseNumber) == number) {
                                    loop = 1;
                                }

                            });
                            if (res.plivo_event == "hangup") {

                                if (loop == 0) {
                                    if ($('.callEnded').hasClass('active')) {
                                        if (isCreatedBy == 1) {

                                            $('.table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td><td style="text-align:center;" ><ul><li><a class="singleRedial" grpCallID="' + res.conf_id + '"  mobileNumber="' + number + '" href="javascript:void(0)";><img width="22" alt="" src="images/Redial.png" /></a></li></ul></td>');
                                        }
                                        else {
                                            $('.table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td>');
                                        }
                                    }
                                }

                            }

                            //111111111111
                            if (status.toLowerCase() == "inprogress") {
                                if (loop == 0) {
									if(res.call_status.toLowerCase() == "inprogress")
									{
										if($('.allMembers').hasClass('active') || $('.onCall').hasClass('active'))
										{

											if ($('#hdnHostmobile').val() != res.to_num) {
												if (isCreatedBy == 1) {
													$('.table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td><td style="text-align:center;"><ul><li><a class="singleMuteUnMute" grpCallID="' + res.conf_id + '" action="Mute" mobileNumber="' + number + ' " href="javascript:void(0)"><img width="17" alt="" src="images/SingleMute.png" /></a></li><li><a class="singleHangUp" grpCallID="' + res.conf_id + '" mobileNumber="' + number + '" href="javascript:void(0)"><img width="22" alt="" src="images/IndividualHangup.png" /></a></li></td>');
												}
												else {
													$('.table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td><td></td>');
												}
											}
										}
									}
                                    if (res.mute == true) {
                                        if (res.conf_digits == 0) {
											if ($('.wantsToTalk').hasClass('active')) {
												if (isCreatedBy == 1) {
													$('.table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td><td style="text-align:center;"><ul><li><a class="singleMuteUnMute" grpCallID="' + res.conf_id + '" action="UnMute" mobileNumber="' + number + ' " href="javascript:void(0)"><img width="32" alt="" src="images/SingleUnMute.png" /></a></li><li><a class="singleHangUp" grpCallID="' + res.conf_id + '" mobileNumber="' + number + '" href="javascript:void(0)"><img width="22" alt="" src="images/IndividualHangup.png" /></a></li></td>');
												}
												else {
													$('.table-bordered').append('<tr><td>' + res.member + '</td><td>' + number + '</td><td>' + res.call_status + '</td><td></td>');
												}
											}
                                        }

                                    }
									
                                }
                                else {
                                    if ($('.callEnded').hasClass('active')) {
                                        $('.table-bordered tr').each(function () {

                                            var responseNumber = $(this).find('td:eq(1)').text();

                                            if ($.trim(responseNumber) == number) {
                                                this.remove();
                                            }

                                        });
                                    }

                                }

                            }





                        });
                        channel.bind('CallActions', function (res) {
                            console.log(res);
                            $('#allMembersCount').html('(' + res.AllMembersCount + ')');
                            $('#onCallCount').html('(' + res.OnCallCount + ')');
                            $('#callEndedCount').html('(' + res.HangUpCount + ')');
                            $('#mutedCount').html('(' + res.MuteCount + ')');
                            $('#handRaiseCount').html('(' + res.HandRaiseCount + ')');
                            number = res.to_number;
                            mutePercentage = (res.MuteCount * 100) / res.OnCallCount;
                            if (mutePercentage >= 50) {
                                $('#mute').attr('action', 'unmute');
                                $('#mute').html('<img src="images/GroupUnMuteAll.png" alt="UnMute" width="27">')
                            }
                            else {
                                $('#mute').attr('action', 'mute');
                                $('#mute').html('<img src="images/GroupMuteAll.png" alt="UnMute" width="27">')
                            }
                            $('.table-bordered tr').each(function () {
                                var responseNumber = $(this).find('td:eq(1)').text();

                                if ($.trim(responseNumber) == number) {
                                    if (isCreatedBy == 1) {
                                        if ($('.onCall').hasClass('active')) {
                                            grpCallID = $('.onCall').attr('grpid');
                                            if (res.conf_action == "mute_member") {
                                                $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='UnMute' mobileNumber=" + number + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                                            }

                                            else {
                                                $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='Mute' mobileNumber=" + number + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                                            }

                                        }
                                        if ($('.allMembers').hasClass('active')) {
                                            grpCallID = $('.allMembers').attr('grpid');
                                            if (res.conf_action == "mute_member") {
                                                $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='UnMute' mobileNumber=" + number + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                                            }
                                            else {
                                                $(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + grpCallID + " action='Mute' mobileNumber=" + number + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + number + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
                                            }

                                        }
                                    }
                                    if ($('.muted,.wantsToTalk').hasClass('active')) {
                                        $(this).remove();

                                    }

                                }
                            });
                            var loop = 0;
                            $('.table-bordered tr').each(function () {

                                var responseNumber = $(this).find('td:eq(1)').text();

                                if ($.trim(responseNumber) == number) {
                                    loop = 1;
                                }

                            });
                            if (res.conf_action == "mute_member") {
                                if (loop == 0) {
                                    if ($('.muted').hasClass('active')) {
                                        if (isCreatedBy == 1) {
                                            $('.table-bordered').append('<tr><td>' + res.member_name + '</td><td>' + res.to_number + '</td><td> Muted </td><td style="text-align:center;"><ul><li><a class="singleMuteUnMute" grpCallID="' + $('.muted').attr('grpid') + '" action="UnMute" mobileNumber="' + res.to_number + '" href="javascript:void(0)";><img width="32" alt="" src="images/SingleUnMute.png" /></a></li><li><a class="singleHangUp" grpCallID="' + $('.muted').attr('grpid') + '" mobileNumber="' + res.to_number + '" href="javascript:void(0)";><img width="22" alt="" src="images/IndividualHangup.png" /></a></li></ul></td>');
                                        }
                                        else {
                                            $('.table-bordered').append('<tr><td>' + res.member_name + '</td><td>' + res.to_number + '</td><td> Muted </td>');
                                        }
                                    }

                                }

                            }
                            if (res.conf_action == "mute_all") {
								
                                if (res.is_all == 1) {

                                    if ($('.muted').hasClass('active')) {
                                        $('.muted').click();
                                    }
                                }
								if ($('.onCall').hasClass('active')) {
									$('.table-bordered tr').each(function () {
										if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
											$(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + $('.muted').attr('grpid') + " action='UnMute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
										}
									});
								}

								if ($('.allMembers').hasClass('active')) {

									$('.table-bordered tr').each(function () {
										if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
											$(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + $('.muted').attr('grpid') + " action='UnMute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='32' alt='' src='images/SingleUnMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
										}

									});
								}
                            }
							if (res.conf_action == "unmute_all") {
								
                                if (res.is_all == 1) {

                                    if ($('.muted').hasClass('active')) {
                                        $('.muted').click();
                                    }
                                }
								if ($('.onCall').hasClass('active')) {
									$('.table-bordered tr').each(function () {
										if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
											$(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + $('.muted').attr('grpid') + " action='Mute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
										}
									});
								}
								if ($('.allMembers').hasClass('active')) {

									$('.table-bordered tr').each(function () {
										if ($(this).find('td:eq(2)').text().toLowerCase() == "inprogress") {
											$(this).find('td:eq(3)').html("<ul><li><a class='singleMuteUnMute' grpCallID=" + $('.muted').attr('grpid') + " action='Mute' mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='17' alt='' src='images/SingleMute.png' /></a></li><li><a class='singleHangUp' grpCallID=" + grpCallID + " mobileNumber=" + $(this).find('td:eq(1)').text() + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li></ul>")
										}

									});
								}

                            }
							

                        });
						

                        GetConferenceRoom(grpID, 1, '', 20, 1);



                        return;
                    }
                    else {
                        //$('.tab-content').hide();
                        $('#listTitle').css("background-color", "#fff !important");
                        $('.CallLogName').text(grpCallName);
                        $('#membersLists,#groupCallButtons').show();
                        $('#inProgressDiv,#inProgress').hide();
                        memebersDetails = "<div class='tab-pane fade in active' id='members'>";
                        for (var i = 0; i < jsonParticipants.Groups.length; i++) {
                            for (var k = 0; k < jsonParticipants.Groups[i].Participants.length; k++) {

                                if (jsonParticipants.Groups[i].Participants[k].GroupId == grpID) {

                                    memebersDetails += '<div class="col-md-4 contactDetails margin-right-5 margin-bottom-5">';
                                    memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
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
                                        memebersDetails += '<div id="profilePic"><img src="images/avatar-img-5.jpg" alt="user" /></div>';
                                        if (jsonParticipants.Groups[i].LeaveParticipants[l].Name.length > 9)
                                        { memebersDetails += '<div id="profileDetails"><p title="' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '">' + jsonParticipants.Groups[i].LeaveParticipants[l].Name.substring(0, 9) + '...</p>'; }
                                        else {
                                            memebersDetails += '<div id="profileDetails"><p>' + jsonParticipants.Groups[i].LeaveParticipants[l].Name + '</p>';
                                        }
                                        memebersDetails += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + jsonParticipants.Groups[i].LeaveParticipants[l].Mobile + '</p></div>';
                                        memebersDetails += '<label class="left-label"><img src="images/left-group.png" title="Left" alt="left"></label></div>';

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
        message: '<h4> Fetching grpTalk history</h4>'
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
				var destinationDate;
				var currentDate;
				var StartDate;
                isSubscribed = result.IsCallSubscribed;
                webPageCount = result.PageCount;
                if (result.History.length > 0) {
                    for (var j = 0; j < result.History.length; j++) {
                        batchIDFirst = result.History[0].BatchID;
                        grpcallID = result.History[j].GrpCallID;
                        if (result.History[j].GrpCallID == grpID) {
							
							StartDate = new Date(result.History[j].StartTime);
							destinationDate=new Date(StartDate.getFullYear(),StartDate.getMonth(),StartDate.getDate());
							
							currentDate = new Date().setHours(0,0,0,0);
							
							//console.log(dateToCompare);
                            if (j == 0) {
                                if (result.History[j].StartTime == "") {
                                    grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
                                    grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">Not Started</div>';
                                } else {
									grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
									
									if(currentDate == Date.parse( destinationDate))
									{
										grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">' + formatAMPM(result.History[j].StartTime) + ' Today</div>';
									}
									else{
										grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">' + formatAMPM(result.History[j].StartTime) + ' on ' + formatForDate(result.History[j].StartTime) + '</div>';
									}
                                    
                                    
                                }
								if(result.History[j].Connected != 0)
								{
								    grpTalkHistory += '<div class="col-md-4 text-center"><span style="color: #ec7d4d; text-align: center">' + result.History[j].Invites + ' invited, ' + result.History[j].Connected + ' attended</span></div>';
								}
								else{
								    grpTalkHistory += '<div class="col-md-4 text-center"><span style="color: #ec7d4d; text-align: center">No member answered</span></div>';
								}
                                grpTalkHistory += '<div class="col-md-4 text-right"><span>Duration : ' + result.History[j].Duration + ' minute(s)</span></div></div></h3>';
                                if(result.History[j].Connected !=0){
									if (isCreatedBy == 1) {
										grpTalkHistory += '<div><div class="row accordioncontent" style="text-align:center">Please wait....</div></div>'
									}
									else {
										grpTalkHistory += '<div><div class="row accordioncontent" style="text-align:center">You dont have permission to view this report</div></div>'
									}
								}
								else{
									if (isCreatedBy == 1) {
										grpTalkHistory += '<div><div class="row accordioncontent" style="text-align:center">Please wait....</div></div>'
									}
									else {
										grpTalkHistory += '<div><div class="row accordioncontent" style="text-align:center">You dont have permission to view this report</div></div>'
									}
								}

                            }
                            else {
                                if (result.History[j].StartTime == "") {
                                    grpTalkHistory += '<h3  groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
                                    grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">Not Started</div>';
                                } else {
                                    grpTalkHistory += '<h3 groupid="' + result.History[j].GrpCallID + '" batchid="' + result.History[j].BatchID + '">';
									if(currentDate == Date.parse( destinationDate))
									{
										grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">' + formatAMPM(result.History[j].StartTime) + ' Today</div>';
									}else{
										grpTalkHistory += '<div class="row"><div class="col-md-4 text-left">' + formatAMPM(result.History[j].StartTime) + ' on ' + formatForDate(result.History[j].StartTime) + '</div>';
									}
                                }
								if(result.History[j].Connected !=0){
								    grpTalkHistory += '<div class="col-md-4 text-center"><span style="color: #ec7d4d; text-align: center">' + result.History[j].Invites + ' invited, ' + result.History[j].Connected + ' attended</span></div>';
								}
								else{
								    grpTalkHistory += '<div class="col-md-4 text-center"><span style="color: #ec7d4d; text-align: center">No member answered</span></div>';
								}
                                grpTalkHistory += '<div class="col-md-4 text-right"><span>Duration : ' + result.History[j].Duration + ' minute(s)</span></div></div></h3>';
								if(result.History[j].Connected !=0){
									if (isCreatedBy == 1) {
										grpTalkHistory += '<div class="accordioncontent" style="text-align:center">Please wait....</div>'
									}
									else {
										grpTalkHistory += '<div class="accordioncontent" style="text-align:center">You dont have permission to view this report</div>'
									}
								}
								else{
									if (isCreatedBy == 1) {
										grpTalkHistory += '<div><div class="row accordioncontent remove" style="text-align:center">Please wait....</div></div>'
									}
									else {
										grpTalkHistory += '<div><div class="row accordioncontent remove" style="text-align:center">You dont have permission to view this report</div></div>'
									}
								}
                            }

                        }
                    }

                    if (++webPageIndex <= webPageCount) {

                        $('#more').html('<div id="moreHistory" class="text-center"  groupid="' + grpcallID + '" isCreatedBy="' + isCreatedBy + '" style="margin: 2px 0 0;width:100%;background-color:#323c41 !important;padding: 4px 0;cursor:pointer;">Load More</div>').show();
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


                    // for (var k = 0; k < callDetailsLength ; k++) {
                    // totalMinutes += parseInt(result.Data2[k].Duration);
                    // totalPrice += parseFloat(result.Data2[k].CallPrice);

                    // }

                    grpTalkCallsInfo += ' <div class="col-md-12 margin-bottom-5">';
                    grpTalkCallsInfo += '<ul>';
                    grpTalkCallsInfo += ' <li class="col-md-3 pull-left"><img src="images/Time.png" alt="Time" />' + result.Data.TotalCallDuration + '</li>';
                    grpTalkCallsInfo += '<li class="col-md-4"><img src="images/Duration1.png" alt="Duration" /> Total Minutes Consumed: <span style="color:#f4a440;font-weight:bold;">' + result.Data.TotalMinutes + '</span></li>';
                    grpTalkCallsInfo += '<li class="col-md-5 pull-left"><img src="images/Rupee.png" alt="Rupee" /> Total Amount Charged: <span style="color:#f4a440;font-weight:bold;">' + currencyName + '  ' + result.Data.TotalCallPrice + '</span></li>';
                    grpTalkCallsInfo += '</ul>';
                    grpTalkCallsInfo += '</div></div>';
                    participantInfo += '<div class="col-md-12 margin-bottom-5" id="reportsTb"><div class="reportsTable"> <div class="scroll"><table class="table table-bordered margin-bottom-0" style="width: 70% !important;margin: 0px auto;" id="tblBody"><thead><tr><th style="text-align:left !important; background-color: #f1f1f1; color:#606060;">Members</th><th style="background-color: #f1f1f1; color:#606060;">Duration in Minute(s)</th><th style="background-color: #f1f1f1; color:#606060;">Amount Charged ('+currencyName+')</th></tr></thead> <tbody>';
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
                    participantInfo += '<tr><td style="text-align:left !important;">' + participantName + '</td>';
                    // if (result.Items[i].DurationInHours != "") {
                    // participantInfo += '<td style="background-color: #f1f1f1;">' + formatAMPM(result.Items[i].mintime) + '-' + formatAMPM(result.Items[i].maxtime) + '</td>';
                    // }
                    // else {
                    // participantInfo += '<td style="background-color: #f1f1f1;"></td>'
                    // }


                    if (result.Items[i].DurationInHours == "") { participantInfo += '<td> 0 </td>'; }
                    else { participantInfo += '<td>' + result.Items[i].Duration + '</td>'; }

                    var p = 0;
                    for (var l = 0; l < callDetailsLength ; l++) {
                        if (result.Items[i].MobileNumber == result.Data2[l].Number) {
                            p = parseFloat(p) + parseFloat(result.Data2[l].CallPrice);

                        }
                    }

                    participantInfo += '<td>' + parseFloat(p) + '</td></tr>';
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
                else { $('#tblBody').append(participantInfo); }
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
                tableHeader = "<table class='table table-bordered tbl_bg'><thead style='background-color: #e8e8e8;color: #333;'><tr><th>Name</th><th>Mobile Number</th><th>Status</th><th style='text-align:center;'>Actions</th></tr></thead><tbody>";
            } else {
                tableHeader = "<table class='table table-bordered tbl_bg'><thead style='background-color: #e8e8e8;color: #333;'><tr><th>Name</th><th>Mobile Number</th><th>Status</th></tr></thead><tbody>";
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
                            hostData = "<table class='table host' style='margin-bottom:15px; margin-top:15px;'><tbody>";
                            hostData = hostData + "<tr><td mobilenumber=" + result.result.data[i].to_num + " style='border-top:0;'><span class='f_13 margin-right-15'>Host :</span><span class='bold-6' style='color:#333;'>" + result.result.data[i].member + "( You )</span></td>";
                            if (result.result.data[i].call_status == "default") {
                                hostData = hostData + "<td style='text-align:right;border-top:0;'><ul><li><a href='javascript:void(0);' mobilenumber=" + result.result.data[i].to_num + " grpcallid=" + grpCallId + " class='singleRedial'><img width='22' src='images/Redial.png' alt=''></a></li></ul></td></tr></tbody></table>";
                            }
                            else {
                                hostData = hostData + "<td style='text-align:right;border-top:0;'><ul><li><a href='javascript:void(0);' mobilenumber=" + result.result.data[i].to_num + " grpcallid=" + grpCallId + " class='singleHangUp'><img width='22' src='images/individualhangup.png' alt=''></a></li></ul></td></tr></tbody></table>";
                            }
                        }
                        else {
                            hostData = "<table class='table host' style='margin-bottom:0;width:100% !important'><tbody>";
                            hostData = hostData + "<tr><td mobilenumber=" + result.result.data[i].to_num + " style='border-top:0;'><span class='f_13 margin-right-15'>Host :</span><span class='bold-6' style='color:#333;'>" + result.result.data[i].member + "(Host)</span></td><td></td>";

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
                            $('.'+grpCallId+'timer').timer('remove');
                            $('.'+grpCallId+'timer').timer({
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
	var isMute;
	var muteAction=$('#mute').attr("action");
	if(muteAction.toLowerCase() == "unmute")
	{isMute= true;}
else if(muteAction.toLowerCase() == "mute")
	{isMute= false;}
    var dialObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"False","MobileNumber":"' + mobileNumber + '","IsCallFromBonus":"0","IsMute" : "'+isMute+'"}';
    if (!$(this).parents('table').hasClass('host')) {
        $(this).parent().parent().parent('td').prev('td').html('dialing');
    }
	if($('.callEnded').hasClass('active'))
	{		$(this).parents('tr').remove();
		console.log($('#callEndedCount').text());
		var value=$('#callEndedCount').text().slice(0,-1);
		value = value.slice(1,value.length);
		$('#callEndedCount').text('('+parseInt(parseInt(value)-1)+')');
		
	}
	else{
		$(this).parent().parent().html("<li><a class='singleHangUp' grpCallID=" + grpCallId + "  mobileNumber=" + mobileNumber + " href='javascript:void(0);'><img width='22' alt='' src='images/IndividualHangup.png' /></a></li>");
	}
    

    singleRedial(dialObj);

});
function singleRedial(dialObj)
{
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
}

$(document).click(function () {
    $('#searchBtn').css("border-color", "none");
});
$(document).delegate("#cancelGroupCall", "click", function () {

    var grpCallId = $('.list.active').attr('id');
    var x = confirm("Are you sure you want to cancel scheduled grptalk?");
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
                    Notifier("You have cancelled the group call successfully",1);
                    $('.list.active[id=' + grpCallId + ']').find("#date").html('<p style="font-size: 40px; line-height: 36px;">' + month[1].replace(/,(?=[^,]*$)/, '') + '</p><p style="font-size: 18px; line-height: 20px;">' + month[0] + '</p>')
					$('.bschedule').html('');
					

                }
            },
            error: function (result) {
                Notifier("Something Went Wrong",2);
            }

        });
    }
    else {
        return false;
    }
});

$(document).delegate('#addMember', 'click', function (e) {
    e.preventDefault();
	//$('.selectedContacts').hide();
    grpCallID = $(this).attr("grpID");

    $('#contactsModalDiv').modal('show');
    $('.mobileContacts').click();
    mode = 1;
    $('#addCall').attr('grpCallID', grpCallID);
    $('.contacts').removeClass('selected');
    $('.contacts').find('.fa-check').css('display', 'none');
    $('#selectedContacts').html('');
    countContacts = 0;
	$('#selectedContacts').html('');
	if (countContacts == 0) {
		 selectedMobileNos =new Array();
                nameArray = new Array();
				$('.count').text('');
                $('a[href="#selectedContacts"]').hide();
                $('a[href="#selectedContacts"]').parent('li').removeClass('active');
                $('a[href="#grpCallMobileContacts"]').parent('li').addClass('active');
                MobileTabClick();
            }
            
    $('.count').html('(' + countContacts + ')');
});

$('#addCall').on('click', function () {

    grpCallID = $(this).attr('grpCallID');
    //alert(grpCallID);
    //  {"ConferenceID":"186410","Participants":[{"praveen smsc":"8341962057"}],"WebListIds":""}
    memberName = "", memberMobile = "", resStr = "", response = "";
    participants = "";

    $('.contacts.unselected').each(function () {
        
            memberName = $(this).find('p:eq(0)').text();
            memberMobile = $(this).find('p:eq(1)').text();

            if ($(this).attr('listId') == "0") {
                listIds = "";
            }
            participants += '{"' + memberName + '":"' + memberMobile + '"}' + ",";
        
    });

    resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";


    response = '{"ConferenceID":"' + grpCallID + '",' + resStr + ',"WebListIds":""}';

    addMemeberWhileOnCall(response, mode);
});

function addMemeberWhileOnCall(paramObj, modeValue) {
	var mobilenumbers="";
	
	
	
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
					
					if(result.AddedParticipants.length > 0)
					{
						for(var i=0; i< result.AddedParticipants.length ; i++)
						{
							mobilenumbers=mobilenumbers+result.AddedParticipants[i].Mobile+','
						}
						var redialObj = '{"ConferenceID":"' + grpID + '","IsAll":"False","MobileNumber":"'+mobilenumbers+'","IsCallFromBonus":"0","IsMute" : "False"}';
                    
						singleRedial(redialObj);
					}
					else{
						Notifier("Member already in call",2)
					}
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
    var x = confirm("Are you sure to edit the grptalk");
    if (x == true) {
        $('div .margin-bottom-1').each(function () {

            if ($(this).hasClass("active")) {
                var grpCallHighlight = $(this);
                var grpId = grpCallHighlight.attr("id");
                var grpIdHistoryJson = '{"Groups": [{"GroupID":"' + grpId + '"}]}';
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
            return getGrpTalkUserDateTime() < new Date(a.StartDateTime);
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
            return new Date(a.StartDateTime) < getGrpTalkUserDateTime();

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




function lengthTrim(stringName)
{if(stringName!==undefined)
	{
	if($.trim(stringName).length>10)	
	return stringName.substring(0,8)+'..'
else
	return stringName;
	}
	else
		return '';
}