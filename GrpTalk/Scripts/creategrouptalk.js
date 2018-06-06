//// <reference path="../assets/global/plugins/jquery-1.11.0.min.js" />
//// <reference path="../assets/global/plugins/jquery-1.11.0.min.js" />
//// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
var regEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var regMobile = /^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[9|8|7][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$/;
var reg = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
var listId = 0, imgPath = "", webContactsData = "", mobileContactsData = "", defaultLines = 0;
var webContactsAppend = "", mobileContactsAppend = "", mobileDropDownlist = ""; mobileNumberVal = "", nameSplit = [];
var subContactsResponse = new Array();
var webPageIndex = 0, searchValue = "", alphabetSort = "", alphabetPageIndex = 0, pageIndex = 0;
var imgName = "", roundCropX = 0; roundCropY = 0; roundCropW = 0; roundCropH = 0, G_img_name = "", alphabetPageIndex = 0;
var pageCount = 0, contactId = 0, countContacts = 0, grpTalkName = "", webListsAppend = "";
var webPageCount = 0, global_alphabetMobileContactsData = "", selectedContacts = "", currentTime = "", currentDate = "", response = "", webListContacts = "";
var jsonContacts = new Array(), participants = new Array(), contactIdArray = new Array(), global_listId = "", listClick = 0, mobileAlphabetClick = 0;
var global_alphabetMobileContactsData = "", global_searchndex = 0, plugoutput = '', txtDate = '', memberName = "", memberMobile = "", resStr = "", response = "";
var listIds = new Array();
var particpantsMobileArray = new Array();
var partcipants = "", mobileNosInSelectedTab = "", response = "", weblistNames = "";
var participantsArray = [];
var date = new Date();
var mdate = (date.getMonth() + 1) + '-' + date.getDate() + '-' + date.getFullYear() + ' ' + date.getHours() + ':' + date.getMinutes() + ':00';
var countryID = 0; Iregx = "";
var firstClick = 0, selectedTabClick = 0, faCheckPic = "", alphabetClick = 0, selectBtnText = "", selectedBtnList = "", selectedBtnTextAll = "";
var selectAllListId = 0, selectedClass = "", mobileScroll = 0, webScroll = 0; gName = "", selectedTabSearch = 0;
var SelectedContactsTabArray = new Array();
var selectedMobileNos = new Array();
var nameArray = new Array();
var participantLength = 0;
var isOnlyDialIn = 0;
var isMuteDialAll = false;
var sliceNumber = 0;
var listIdArray = new Array();
var newList = "";
var creategropTalk = 1;
var xlList = "block";
var isLiveCall = 0;
var isManualGroupName = 0;
var managerMobile = '';

$(document).ready(function () {
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
    sliceNumberFn();
    $('#phoneContactsDisplay,.phnCntcts').hide();
    $('.searchByMember,#grpTalkName').val('');

    $('#successMessage,#errorMessage').html("");

    getAllWebContacts(0);        //-----Fetching all Web Lists contacts
    getMobileContacts(1);       //-----Fetching All Mobile Contacts
    if ($('#grpCallWebContacts div.contacts').length > 0)
        $('#selectAll').show();


    $('#grpCallWebContacts').slimScroll({
        allowPageScroll: false,
        height: '250',
    });

    $("#advancedSettings").click(function () {
        if ($(this).hasClass('active')) {
            $(this).html('Advanced Settings<span class="glyphicon glyphicon-chevron-up">');
            $('#advancedFeatures').show()
            $(this).removeClass('active')
        } else {
            $(this).html('Advanced Settings<span class="glyphicon glyphicon-chevron-down">');
            $('#advancedFeatures').hide()
            $(this).addClass('active')
        }
    });



    //--------------------------------------Scroll event on Web contacts
    $('#grpCallWebContacts').scroll(function () {
        if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight - 1) {
            if (global_alphabetpageIndex == 0) {
                webContactsOffsetScroll(listId);
                var height = $(this).scrollTop();
                // $(this).css("top", height);
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


    //--------------------------------------Tabs click
    $("ul#myTabList li").click(function () {
        $("ul#myTabList li").removeClass("active");

        var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
        $(this).addClass("active");
        $('#selectedContacts').hide();

        if (activeTab == "#grpCallWebContacts") {
            WebTabClick();
            tabClickFetchtContacts();
            if ($('#grpCallWebContacts .contacts').length > 0) {
                $('#selectAll').show();
            }
            else { $('#selectAll').hide(); }
            if ($('#grpCallMobileContacts').parent('.slimScrollDiv').length != 0)
            { $('#grpCallMobileContacts').parent().css("display", "none"); }

        }
        else if (activeTab == "#grpCallMobileContacts") {

            tabClickFetchtContacts();
            $('#grpCallMobileContacts,.alphabet').show();
            $('#grpCallWebContacts,#list-group1').parent().hide();
            $('#grpCallWebContacts').hide();
            $('#grpCallMobileContacts').parent().show();
            $('#listName').text("All Web Contacts");
            if ($('#selectedContacts').parent('.slimScrollDiv').length != 0)
            { $('#selectedContacts').parent().hide(); }
            $('.addNewContact').hide();
            $('.list1').hide();
            $('#phoneContactsDisplay,.phnCntcts').show();
            $('grpCallMobileContacts.slimScrollDiv').show();

            $('#grpCallWebContacts,#selectedContacts').removeClass("active in");
            firstClick++;
            $('#selectAll').hide();
            $('#grpCallMobileContacts').addClass("active in");
            $('#grpCallMobileContacts').css("display", "block !important");
            //$('#mobileContacts').html(mobileContactsAppend);
        }
        else if (activeTab == "#selectedContacts") {
            $('#selectAll').hide();
            $('#alphabetWebContacts,#alphabetMobileContacts').hide();
            $('#grpCallWebContacts').parent().hide();
            $('#grpCallMobileContacts,.alphabet').hide();
            if ($('#grpCallMobileContacts').parent('.slimScrollDiv').length != 0)
            { $('#grpCallMobileContacts').parent().hide(); }
            $('#grpCallWebContacts').hide();
            $('#selectedContacts').show();
            $('#selectedContacts').parent().show();
            $('#phoneContactsList').hide();
            $('#phoneContactsDisplay,.phnCntcts').hide();
            $('.addNewContact').hide();
            $('.list1').hide();
            $('#selectedContacts').addClass("active in");
            if (selectedTabClick == 0) {
                $('#selectedContacts').slimScroll({
                    allowPageScroll: false,
                    height: '250',
                });
            }

            selectedTabClick++;
            $('#grpCallWebContacts,#grpCallMobileContacts').removeClass("active in");

        }

    });

    //--------------------------------------Click event on Weblists to display contacts
    $(document).on('click', '.contactList1', function (e) {
        $('#search-input').val('');
        e.preventDefault();
        console.log(listId);
        $('.list1').removeClass("highlight");
        $(this).parents('.list1').addClass("highlight");
        var listName = "";
        listName = $(this).text();
        listId = $(this).attr('id');
        listClick++;
        $('#webList').html($(this).html());
        $('#listName').text(listName);
        $('a[href="#grpCallWebContacts"]').attr('listID', listId);
        $('#grpCallWebContacts').html('');
        webPageIndex = 0;
        getAllWebContacts(listId);
        $('.addNewContact').show();
    });


    //--------------------------------------Filtering contacts by alphabets

    $(document).on('click', '.alphabet li', function (e) {
        e.preventDefault();
        $('#search-input').val('');
        $('#search').css("border-color", "none");
        var clickCounter = $(this).data('clickCounter') || 0;
        if (clickCounter == 0) {
            clickCounter += 1;
            global_searchndex = 0;
            $(this).data('clickCounter', 0);
            global_alphabetMobileContactsData = "";
        }

        global_alphabet = alphabet;
        global_alphabetMobileContactsData = "";
        var alphabet = $(this).text().toLowerCase();
        alphabetOrSerachClick(global_searchndex, alphabet);
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

    $('#search-input').click(function (e) {
        e.stopPropagation();
        $('#search').css("border-color", "#66afe9");
    })

    //--------------------------------------filtering contacts through search filter
    $(document).on('click', '#search', function (e) {
        e.preventDefault()
        global_searchndex = 0;
        var alphabet = $('#search-input').val().toLowerCase();
        var activeTab = $('ul#myTabList li.active').find("a").attr("href");
        if ($.trim(alphabet).length == 0) {
            if (activeTab == "#grpCallWebContacts" || activeTab == "#webContacts") {
                $('#grpCallWebContacts').html('');
                $('#selectAll').show();
                $('#grpCallWebContacts').html('');
                webPageIndex = 0;
                getAllWebContacts($('.list1.highlight .contactList1').attr("id"));
                return;
                $('#search-input').val('');
            }
            else if (activeTab == "#grpCallMobileContacts") {
                subContactsResponse = new Array();
                $('#grpCallMobileContacts').slimScroll({ destroy: true });
                $('#grpCallMobileContacts').slimScroll({
                    allowPageScroll: false,
                    height: '250',
                });
                $('#grpCallMobileContacts').html('');
                pageIndex = 0;
                getMobileContacts(1);
                return;
            }
            else { selectedContactsTabClick(''); }
            return;
        }

        global_alphabet = alphabet;
        global_alphabetMobileContactsData = "";
        alphabetOrSerachClick(global_searchndex, alphabet);
        e.stopImmediatePropagation();
    });

    //advancedSettings$
    $('#dialinswitch').change(function (e) {
        e.stopPropagation();

        if ($('#dialinswitch').is(':checked')) {
            $('#openlinecondition').show();
        }
        else {
            $('#openlinecondition').hide();
            $('#openlineswitch').attr("checked", false);
        }
    })


    $('#openswitch').click(function (e) {
        if ($('#datefield').val().length == 0) {
            Notifier('Please select date and time', 2);
            return false;
        }
        if ($('#datefield').val().length != 0) {
            openLineSwitchTimeCheck();
        }
    });

    $('#datefield').focusout(function (e) {
        if ($('#datefield').val.length != 0) {
            if ($('#openlineswitch').is(':checked')) {
                var repeats = $('.weekDay.active').length;
                if (repeats == 0)
                    openLineSwitchTimeCheck();
            }
        }
    });

    //end of advanced settings


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

    //$(document).on("click", "div ul.dropdown-menu li a", function (e) {
    //    var anchorValue = $(this).text();
    //    $('#phoneNumber' + mobileNumberVal).text(anchorValue);
    //    if ($('.dropdown').hasClass('open')) {
    //        $('.dropdown').removeClass('open')
    //    }
    //    e.stopPropagation();
    //});

    $(document).on('click', '.addNewContact', function (e) {
        e.preventDefault();
        listId = $(this).attr('listId');
        $('#name, #mobileNumber,#newWebList').val('');
        $('#ddlWebList').val(0);
        $('#webContactProfile').attr('src', '');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('#errorDescForName, #errorDescForMobile,#errorDescForWebList').html('');
        $('.jcrop-circle-demo').css('display', 'none');
        $('#successMessage,#errorMessage').html("");
        $('#contactsModal').modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });
        $('#sheeterr,#headerselect').hide();
        $('#upmsg').html('');
        ListNames();
        $('#newWebList').removeAttr("disabled");
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

    $('#excelUpload').on('click', function () {
        $('#successMessage,#errorMessage,#errorDescForWebList').html("");
        $('#contactFormBody').hide();
        $('#excelFormBody').show();
        $('#saveContact').hide();
        $('#saveExcelContacts').show();
        $('#sheeterr,#headerselect').hide();
        $('#name,#mobileNumber,#newWebList').val('');
        $('#ddlWebList').val(0);
        $('#upmsg').html('');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('.jcrop-circle-demo').css('display', 'none');
    });

    $(document).on('change', '#grpTalkName', function () {
        if ($("#grpTalkName").val().length != 0) {
            if ($("#grpTalkName").val().length < 3 && $('.selected.contacts').length != 0)
                Notifier('Please enter minimum 3 characters for group name', 2)

            isManualGroupName = 1;
        }
        else
            isManualGroupName = 0;
    });

    //--------------------------------------Click  event on participants selection
    var isSelected = 0;
    $(document).on('click', '.contacts', function () {

        if (isManualGroupName == 1 && $("#grpTalkName").val().length < 3) {
            Notifier('Please enter minimum 3 characters for group name', 2)
            return false;
        }
        var activeTab = $('ul#myTabList li.active').find("a").attr("href");
        if (activeTab == "#grpCallWebContacts")
            $('#selectAll').show();
        memberName = ""; memberMobile = ""; nameSplit = [];
        var mobileNo = $(this).find("p:eq(1)").text();
        memberName = $(this).find('p:eq(0)').text();
        var contactId = $(this).attr("id");
        if ($(this).hasClass('selected') && !$(this).hasClass('unselected')) {
            nameSplit = [];

            var mobileNo = $(this).find("p:eq(1)").text();
            var conId = $(this).attr("id");

            if ($(this).hasClass('selected')) {
                var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
                selectedMobileNos.splice(index, 1);
                nameArray.splice(index, 1);
                contactIdArray.splice(index, 1);
                listIdArray.splice(index, 1);

                countContacts = countContacts - 1;
                $('.count').html('(' + countContacts + ')');

                var divName = $(this).attr("id");
                //$('.selected').each(function () {
                //    var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                //    if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                $(this).removeClass("selected");
                $(this).find('.fa-check').css('display', 'none');
                //    }

                //});
                $('div.unselected').each(function () {
                    var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();

                    if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                        $(this).remove();
                    }

                });


                $('.contacts.unselected').each(function () {
                    memberName = $(this).find('p:eq(0)').text();
                    memberMobile = $(this).find('p:eq(1)').text();
                    memberName = $(this).find('p:eq(0)').text().substring(0, 2).toUpperCase();
                    nameSplit += memberName.replace();

                    if ($('.contacts.unselected').length == 1)
                        nameSplit = $(this).find('p:eq(0)').text().substring(0, 4).toUpperCase();
                    if (isManualGroupName == 0)
                        $('#grpTalkName').val(nameSplit);

                });

                var length = $('.contacts.selected').length;
                if (length == 0) {
                    nameSplit += '';
                    if (isManualGroupName == 0)
                        $('#grpTalkName').val(nameSplit);

                }
                var tabContacts = $('#grpCallWebContacts .contacts').length;
                var tabSelectedContacts = $('#grpCallWebContacts .selected').length;
                if (tabContacts == tabSelectedContacts)
                    $('#selectAll').text('Unselect All');
                else
                    $('#selectAll').text('Select All');
            }

            if (countContacts == 0) {
                $('a[href="#selectedContacts"]').hide();
                if (activeTab != "#grpCallMobileContacts") {
                    $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
                    $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
                    WebTabClick();
                }
            }
            else { $('a[href="#selectedContacts"]').show(); }
        }
        else if (!$(this).hasClass('unselected')) {

            //secondary contacts           
            if ($('#selectedContacts div.unselected#' + this.id).hasClass('unselected')) {
                var mobileNo = $('#selectedContacts div.unselected#' + this.id).find("p:eq(1)").text();
                var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
                selectedMobileNos.splice(index, 1);
                nameArray.splice(index, 1);
                contactIdArray.splice(index, 1);
                listIdArray.splice(index, 1);
                countContacts--;
            }
            //secondary contacts  end
            //select
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
                $('#selectedContacts').html('');

            var conId = $(this).attr("id");
            listId = $(this).attr('listId');
            var mobileNo = $(this).find("p:eq(1)").text();
            var name = $(this).find("p:eq(0)").attr('name');

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
                selectedMobileNos.push($.trim(mobileNo.replace(/\D/g, "").slice(sliceNumber)));
                contactIdArray.push(contactId);
                nameArray.push(name);

                listIdArray.push(listId);;
                $(this).addClass('selected')
                $(this).find('.fa-check').css('display', 'block');
                countContacts = countContacts + 1;
                $('.count').html('(' + countContacts + ')');
                selectedContacts = '<div class="contacts margin-right-5 margin-bottom-5 unselected" id=' + contactId + ' listId=' + listId + '><div id="profilePic"><img alt="default user" src="images/avatar-img-5.jpg"></div><div id="profileDetails">';
                if (name.length > 25) {
                    selectedContacts += '<p name="' + name + '" title="'+name+'" >' + name.substring(0,25) + '..</p>';
                } else { selectedContacts += '<p name="' + name + '" >' + name + '</p>'; }
                selectedContacts += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNo + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>';
                $('#selectedContacts').append(selectedContacts);
                //$('.contacts').each(function () {
                //    var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                //    if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                //        $(this).addClass('selected')
                //        $(this).find('.fa-check').css('display', 'block');
                //    }
                //});
                var tabContacts = $('#grpCallWebContacts .contacts').length;
                var tabSelectedContacts = $('#grpCallWebContacts .selected').length;
                if (tabContacts == tabSelectedContacts)
                    $('#selectAll').text('Unselect All');
                else
                    $('#selectAll').text('Select All');

            }

            if (countContacts == 0) {
                $('a[href="#selectedContacts"]').hide();
                if (activeTab != "#grpCallMobileContacts") {
                    $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
                    $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
                    WebTabClick();
                }
            }
            else { $('a[href="#selectedContacts"]').show(); }

            gName = "";
            gName = $('#grpTalkName').val();

            $('.contacts').each(function () {
                if ($(this).hasClass('unselected')) {
                    memberName = $(this).find('p:eq(0)').text();
                    memberMobile = $(this).find('p:eq(1)').text();

                    if (nameSplit.length < 25) {
                        if ($('.contacts.unselected').length == 1)
                            nameSplit = memberName.substring(0, 4).toUpperCase();
                        else
                            nameSplit += memberName.substring(0, 2).toUpperCase();
                    }
                    //$('#grpCallErrorMessage').html("").hide();
                    // gName = $('#grpTalkName').val();
                }
            });
            if (gName == "") { $('#grpTalkName').val(nameSplit); }
            else if (gName != "" && (isManualGroupName == 0)) {
                if ($('.unselected').length == 1) {
                    if (gName.length > 2) {
                        $('#grpTalkName').val(gName);
                        isSelected = 1;
                    }
                    else if (gName.length == 2) {
                        $('#grpTalkName').val(nameSplit);
                    }
                }
                else {
                    if (isSelected == 1) { $('#grpTalkName').val(gName); }
                    else { $('#grpTalkName').val(nameSplit); }
                }
            }

            // if (nameSplit.length > 6) {
            // nameSplit += nameSplit.substring(0, 6).toUpperCase();

            // }

        }

    });

    $('#selectAll').click(function () {

        if (isManualGroupName == 1 && $("#grpTalkName").val().length < 3) {
            Notifier('Please enter minimum 3 characters for group name', 2)
            return false;
        }
        var selectAllText = $.trim($('#selectAll').text()); var booleanAdd = true;
        var selectedCount = 0; var number = 0;
        if (selectAllText == "Select All") {
            var duplicateArray = new Array();
            var str = '';
            $('#grpCallWebContacts .contacts').each(function () {
                if (!$(this).hasClass('selected')) {
                    if ($.trim(participantLength) != 0) {
                        if (parseInt($('#selectedContacts div.unselected').length) + parseInt(participantLength) >= defaultLines - 1) {
                            var minimumCount = "You can't select more than " + parseInt(parseInt(defaultLines) - 1) + " members";
                            Notifier(minimumCount, 2);
                            selectBtnText = "Unselect All";
                            return false;
                        }
                    }
                    else {
                        if ($('#selectedContacts div.unselected').length >= defaultLines - 1) {
                            var minimumCount = "You can't select more than " + parseInt(parseInt(defaultLines) - 1) + " members";
                            Notifier(minimumCount, 2);
                            selectBtnText = "Unselect All";
                            return false;
                        }
                    }
                    booleanAdd = true;
                    listId = $(this).attr('listId');
                    contactId = $(this).attr('id');
                    mobileNumberVal = $(this).find("p:eq(1)").text();
                    var name = $(this).find("p:eq(0)").attr('name');

                    $('#selectedContacts div.unselected').each(function () {
                        var selectedTabMobileNos = $(this).find("p:eq(1)").text();
                        if (mobileNumberVal.slice(sliceNumber) == selectedTabMobileNos.slice(sliceNumber)) {
                            duplicateArray.push($(this).find("p:eq(0)").text());
                            duplicateArray.push(selectedTabMobileNos);
                            booleanAdd = false;
                            number--;
                        }
                    });

                    if (booleanAdd) {
                        // if($('.contacts.unselected').length==0)	
                        // nameSplit='';
                        // if (($('#grpTalkName').val().length == 0 || $('#grpTalkName').val().length < 25) && (isManualGroupName==0)) {
                        // nameSplit += name.substring(0, 2).toUpperCase();
                        // $('#grpTalkName').val(nameSplit);
                        // }
                        if (nameArray.length == 0)
                        { $('#selectedContacts').html(''); }
                        $(this).removeClass('selected');
                        selectedMobileNos.push(mobileNumberVal.replace(/\D/g, "").slice(sliceNumber));
                        nameArray.push(name);
                        contactIdArray.push(contactId);
                        listIdArray.push(listId);
                        $(this).addClass('selected');
                        $(this).find('.fa-check').css('display', 'block');
                        countContacts++;
                        selectedCount++;
                        $('.count').html('(' + countContacts + ')');
                        selectedContacts = '<div class="contacts margin-right-5 margin-bottom-5 unselected" id=' + contactId + ' listId=' + listId + '><div id="profilePic"><img alt="default user" src="images/avatar-img-5.jpg"></div><div id="profileDetails">';
                        if (name.length > 25) {
                            selectedContacts += '<p name="' + name + '" title="' + name + '">' + name.substring(0, 25) + '..</p>';
                        }
                        else {
                            selectedContacts += '<p name="' + name + '">' + name + '</p>';
                        }
                        selectedContacts += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNumberVal + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>';
                        $('#selectedContacts').append(selectedContacts);
                    }
                }
            });
            if ($('#grpCallWebContacts .selected').length == 0) {
                selectBtnText = "Select All";
            }
            else if ($('#grpCallWebContacts .selected').length == ($('#grpCallWebContacts .contacts').length + number)) {
                selectBtnText = "Unselect All";
            }

            if (countContacts == 0) {
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
            global_listId = "";
            $('#grpCallWebContacts .contacts').each(function () {
                var mobileNo = $(this).find("p:eq(1)").text();
                var name = $(this).find("p:eq(0)").text().substring(0, 2).toUpperCase();

                if ($(this).hasClass('selected')) {
                    var index = selectedMobileNos.indexOf(mobileNo.slice(sliceNumber));
                    selectedMobileNos.splice(index, 1);
                    nameArray.splice(index, 1);
                    contactIdArray.splice(index, 1);
                    listIdArray.splice(index, 1);
                    listId = $(this).attr('listId');
                    contactId = $(this).attr('id');
                    $(".contacts[id='" + contactId + "']").removeClass("selected");
                    $(this).find('.fa-check').css('display', 'none');
                    countContacts = countContacts - 1;
                    $('.count').html('(' + countContacts + ')');
                    $('#selectedContacts div.unselected').each(function () {
                        var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                        if ($.trim(mobileNo.slice(sliceNumber)) == $.trim(mobileNosInSelectedTab.slice(sliceNumber))) {
                            $(this).remove();
                        }

                    });
                    $('.contacts').each(function () {
                        var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                        if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                            $(this).removeClass('selected')
                            $(this).find('.fa-check').css('display', 'none');
                        }
                    });
                }
                // if ($('#grpTalkName').val().length != 0 && (isManualGroupName==0)) {
                // var grpName = $('#grpTalkName').val();
                // grpName = grpName.replace(name, '');
                // $('#grpTalkName').val(grpName);
                // }
            });
            if (countContacts == 0) {
                if (isManualGroupName == 0)
                    $('#grpTalkName').val('');
                $('a[href="#selectedContacts"]').hide();
                $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
                $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
                WebTabClick();
            }
            else { $('a[href="#selectedContacts"]').show(); }

            selectBtnText = "Select All";

        }
        $('#selectAll').text(selectBtnText);
        //selectedContacts = "";
        nameSplit = '';
        $('.contacts.unselected').each(function () {
            if (isManualGroupName == 0) {
                memberName = $(this).find('p:eq(0)').text();
                memberMobile = $(this).find('p:eq(1)').text();
                memberName = $(this).find('p:eq(0)').text().substring(0, 2).toUpperCase();
                nameSplit += memberName.replace();

                if ($('.contacts.unselected').length == 1)
                    nameSplit = $(this).find('p:eq(0)').text().substring(0, 4).toUpperCase();
                //if(isManualGroupName==0)
                $('#grpTalkName').val(nameSplit.substring(0, 25));
            }
        });

    });

    //--------------------------------------Starting Group call
    $(document).on('click', '#startNow', function () {
        var bool = true;
        var weblistIds = '';
        if (isManualGroupName == 1 && $("#grpTalkName").val().length < 3) {
            Notifier('Please enter minimum 3 characters for group name', 2)
            return false;
        }
        if ($('#selectedContacts .contacts').length == 0) {
            //$('#grpCallErrorMessage').html("Please Choose  Participants To Make A Call").show();
            Notifier("Please Choose  Participants To Make A Call", 2);
            return false;
        }
        grpTalkName = $('#grpTalkName').val();
        if (grpTalkName == "") {
            Notifier("Please Enter Group Call Name", 2);
            return false;
        }

        $('#selectedContacts .contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                mobileNosInSelectedTab = $(this).find('p:eq(1)').text();

                if ($('#hostNumber').val().slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                    Notifier("Please Remove Your Number From Selected Contacts", 2);
                    bool = false;
                    return false;
                }
            }
        });
        if (bool) {
            $('#dialMutedial').modal("show");
        }
    });

    $('#dial').click(function () {
        $(this).prop('disabled', true);
        var dialResponse = startNowResponse(0);
        createGroupCall(dialResponse, 1, false)
    });

    $('#muteDial').click(function () {
        $(this).prop('disabled', true);
        var muteDialResponse = startNowResponse(1);
        createGroupCall(muteDialResponse, 1, true)
    });

    $('#cancel').click(function () {
        $('#dialMutedial').modal("hide");
    })

    // createGroupCall(response, 1);})
    $('#chkOnlyDialIn').click(function () {
        if ($(this).is(":checked"))
            isOnlyDialIn = 1;
        else if ($(this).is(":not(:checked)"))
            isOnlyDialIn = 0;
    });
    $('#chkMuteDialAll').click(function () {

        if ($(this).is(":checked"))
            isMuteDialAll = true;
        else if ($(this).is(":not(:checked)"))
            isMuteDialAll = false;

    });

    $('#saveGroupCall').click(function (e) {
        //auto complete for manager  
        $("#txtAssignManger").val('');
        $("#selectManger").hide();
        var autoArray = selectedMobileNos.concat(nameArray)
        $("#txtAssignManger").autocomplete({
            source: autoArray
        });
        //auto complete for manager end

        if (isManualGroupName == 1 && $("#grpTalkName").val().length < 3) {
            Notifier('Please enter minimum 3 characters for group name', 2)
            return false;
        }
        grpTalkName = $('#grpTalkName').val();
        if (grpTalkName == "") {
            Notifier("Please Enter Group Call Name", 2);
            return false;
        }

        $('#savedialinswitch,#saveallownonswitch,#savemutedialswitch,#saveopenlineswitch,#assignManager,#assignManagerSchedule').attr("checked", false);
        $('#saveModal').modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });


    });

    //assign manager

    $(document).on('click', "#assignManager,#assignManagerSchedule", function () {
        if ($('#assignManager').is(':checked'))
            $("#selectManger").show();
        else
            $("#selectManger").hide();

        if ($('#assignManagerSchedule').is(':checked'))
            $("#selectMangerSchedule").show();
        else
            $("#selectMangerSchedule").hide();
    });
    //assign manager end

    $('#saveDate').click(function (e) {

        $('#saveModal .modal-content').block({ message: '<h4> Saving grpTalk... </h4>' });

        var isOnlyDialIn = 0, isAllowNonMembers = 0, openLineBefore = 0, muteDial = 0, weblistIds = '';
        memberName = "", memberMobile = "", resStr = "", response = "", participants = "";
        nameSplit = []; var bool = true;
        $('#selectedContacts div.contacts').each(function () {
            if ($(this).hasClass('unselected')) {
                memberName = $(this).find('p:eq(0)').attr('name');
                memberMobile = $(this).find('p:eq(1)').text();
                if ($(this).attr('listId') == "m1") {
                    listIds = "0";
                }
                else {
                    listIds = $(this).attr('listId');
                    weblistIds = 0;
                }

                participants += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false","ListId":"' + listIds + '"}' + ",";
                participantsArray.push(memberName);
            }

        });

        resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '').replace('/', '//').replace('\\', '\\\\') + " ]";
        var participantsLenght = $('#selectedContacts').find("div.unselected").length;
        displayCurrentTime();
        displayCurrentDate();

        $('#selectedContacts .contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                mobileNosInSelectedTab = $(this).find('p:eq(1)').text();

                if ($('#hostNumber').val().slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                    Notifier("Please Remove Your Number From Selected Contacts", 2);
                    $('#saveModal .modal-content').unblock();
                    bool = false;
                }
            }

        });

        //auto complete for manager  

        if ($('#assignManager').is(':checked')) {
            var manager = $("#txtAssignManger").val();
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
            if (managerMobile == '') {
                alert("Please select mobile number for manager");
                $('#saveModal .modal-content').unblock();
                bool = false;
            }
            if (nameCount > 1) {
                alert("Please select mobile number, same name is there more than one time");
                $("#txtAssignManger").val('');
                $('#saveModal .modal-content').unblock();
                bool = false;
            }
        }
        //auto complete for manager end

        if (bool) {
            if ($('#savedialinswitch').is(':checked'))
                isOnlyDialIn = 1;

            if ($('#saveallownonswitch').is(':checked'))
                isAllowNonMembers = 1;

            if ($('#savemutedialswitch').is(':checked'))
                muteDial = 1;

            if (!(isOnlyDialIn == 1 && isAllowNonMembers == 1)) {
                if ($('#selectedContacts .contacts').length == 0) {
                    Notifier("Please choose participants to make a group call", 2);
                    $('#saveModal .modal-content').unblock();
                    return false;
                }
            }

            response = '{"GroupCallName":"' + grpTalkName + '","SchType":"100","SchduledTime":"' + currentTime + '","IsOnlyDialIn":"' + isOnlyDialIn + '","IsAllowNonMembers":"' + isAllowNonMembers + '","SchduledDate":"' + currentDate + '","IsMuteDial":"' + muteDial + '","WeekDays":"","Reminder":"30","WebListIds":"' + weblistIds + '","managerMobile":"' + managerMobile + '",' + resStr + '}';
            console.log(response);
            createGroupCall(response, 3, "");

        }
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

        //auto complete for manager  
        $("#selectMangerSchedule").hide();
        $("#assignManagerSchedule").attr("checked", false);
        $("#txtAssignMangerSchedule").val('');
        var autoArray = selectedMobileNos.concat(nameArray)
        $("#txtAssignMangerSchedule").autocomplete({
            source: autoArray
        });
        //auto complete for manager end

        if (isManualGroupName == 1 && $("#grpTalkName").val().length < 3) {
            Notifier('Please enter minimum 3 characters for group name', 2)
            return false;
        }

        grpTalkName = $('#grpTalkName').val();
        currentDate = getGrpTalkUserDateTime();
        if (grpTalkName == "") {
            Notifier("Please Enter Group Call Name", 2);
            return false;
        }

        $('.weekDay').removeClass("active");

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

        $('#dialinswitch,#allownonswitch,#mutedialswitch,#openlineswitch').attr('checked', false);
        $('#openlinecondition').hide();

    });

    $('#scheduleDate').on('click', function (e) {

        $('#datepickerModal .modal-content').block({ message: '<h4> Saving grpTalk... </h4>' });
        var bool = true;
        var isOnlyDialIn = 0, isAllowNonMembers = 0, muteDial = 0, openLineBeore = 0, weblistIds = '';
        e.preventDefault();
        var repeats = "";
        $('.weekDay.active').each(function (e) {
            repeats += $(this).val() + ",";
        });
        var weekDays = "", schType = 0, editType = 0;
        participants = "";
        response = "";
        $('#selectedContacts .contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
                if ($('#hostNumber').val().slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
                    Notifier("Please Remove Your Number From Selected Contacts", 2);
                    bool = false;
                    $('#datepickerModal .modal-content').unblock();
                    return false;
                }
            }
        });

        grpTalkName = $('#grpTalkName').val();
        if ($('#dialinswitch').is(':checked'))
            isOnlyDialIn = 1;

        if ($('#allownonswitch').is(':checked'))
            isAllowNonMembers = 1;

        if ($('#mutedialswitch').is(':checked'))
            muteDial = 1;

        if ($('#openlineswitch').is(':checked'))
            openLineBeore = 1;


        $('#selectedContacts .contacts').each(function () {
            contactId = $(this).attr('id');
            if ($(this).hasClass('unselected')) {
                memberName = $(this).find('p:eq(0)').attr('name');                
                memberMobile = $(this).find('p:eq(1)').text();
                if ($(this).attr('listId') == "m1") {
                    listIds = "0";
                }
                else {
                    listIds = $(this).attr('listId');
                    weblistIds = 0;
                }

                participants += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false","ListId":"' + listIds + '"}' + ",";
                participantsArray.push(memberName);
            }
        });

        if ($('#assignManagerSchedule').is(':checked')) {
            var manager = $("#txtAssignMangerSchedule").val();
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
            if (managerMobile == '') {
                alert("Please select mobile number for manager");
                $('#datepickerModal .modal-content').unblock();
                bool = false;
            }
            if (nameCount > 1) {
                alert("Please select mobile number, same name is there more than one time");
                $("#txtAssignManger").val('');
                $('#datepickerModal .modal-content').unblock();
                bool = false;
            }

        }
        if (bool) {
            if (!(isOnlyDialIn == 1 && isAllowNonMembers == 1)) {
                if (participantsArray.length == 0) {
                    //$('#grpCallErrorMessage').html("Please Choose  Participants To Make A Call").show();
                    Notifier("Please Choose  Participants To Make A Call", 2);
                    $('#datepickerModal .modal-content').unblock();
                    return false;
                }
            }

            if ($.trim(repeats) == "") {
                weekDays = "";
                schType = 0;
            }
            else {
                weekDays = repeats.slice(0, -1);
                schType = 1;
            }

            txtDate = $('.date-picker').val().toString();
            var scheduleDateTime = "";
            scheduleDateTime = txtDate.split("at");
            currentDate = scheduleDateTime[0];
            currentTime = scheduleDateTime[1];
            if (currentDate == "") {
                Notifier('Please Select Schedule Datetime', 2);
                $('#datepickerModal .modal-content').unblock();
                return false;
            }
            var minDate = getGrpTalkUserDateTime();
            minDate.setMinutes(minDate.getMinutes() + 9);
            var selectedDate = "";
            selectedDate = new Date(scheduleDateTime[0] + " " + scheduleDateTime[1]);
            if (schType == 0) {
                if (Date.parse(selectedDate) < Date.parse(minDate) && schType != 1) {
                    Notifier("Please select 10 mins from now", 2);
                    $('#datepickerModal .modal-content').unblock();
                    return false;
                }

                if ($('#openlineswitch').is(":checked")) {
                    minDate.setMinutes(minDate.getMinutes() + 20);
                    if (Date.parse(selectedDate) < Date.parse(minDate)) {
                        Notifier("Please select 30 mins from now", 2);
                        $('#datepickerModal .modal-content').unblock();
                        return false;
                    }
                }
            }

            resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '').replace('/', '//').replace('\\', '\\\\') + " ]";

            response = '{"GroupCallName":"' + grpTalkName + '","SchType":"' + schType + '","SchduledTime":"' + currentTime + '","IsOnlyDialIn":"' + isOnlyDialIn + '","IsAllowNonMembers":"' + isAllowNonMembers + '","OpenLineBefore":"' + openLineBeore + '","SchduledDate":"' + currentDate + '","IsMuteDial":"' + muteDial + '","WeekDays":"' + weekDays + '","Reminder":"30","WebListIds":"' + weblistIds + '","managerMobile":"' + managerMobile + '",' + resStr + '}';

            createGroupCall(response, 2, "");
        }
    });

    $('#clearRepeat').click(function () {
        $('.weekDay').each(function () {
            if ($(this).hasClass("active")) {
                $(this).removeClass("active");
            }
        });
        $('#repeat').val('');
    });

    $('#repeat').click(function (e) {
        e.preventDefault();
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
    contactIdArray.splice(index, 1);
    listIdArray.splice(index, 1);
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
                if (isManualGroupName == 0)
                    $('#grpTalkName').val(nameSplit);
                e.stopPropagation();
            }
        });

        var length = $('.unselected').length;
        if (length == 0 && isManualGroupName == 0) {
            nameSplit += '';
            $('#grpTalkName').val(nameSplit);
        }
    }


    $('.selected').each(function () {

        var mobileNosInSelectedTab = $(this).find('p:eq(1)').text();
        if (mobileNo.slice(sliceNumber) == mobileNosInSelectedTab.slice(sliceNumber)) {
            $(this).removeClass("selected");
            $(this).find('.fa-check').css('display', 'none');
        }
        e.stopPropagation();
    })
    var tabContacts = $('#grpCallWebContacts .contacts').length;
    var tabSelectedContacts = $('#grpCallWebContacts .selected').length;
    if (tabContacts == tabSelectedContacts)
        $('#selectAll').text('Unselect All');
    else $('#selectAll').text('Select All');
    if (countContacts == 0) {
        $('a[href="#selectedContacts"]').hide();
        $('a[href="#selectedContacts"], a[href="#grpCallMobileContacts"]').parent('li').removeClass('active');
        $('a[href="#grpCallWebContacts"]').parent('li').addClass('active');
        WebTabClick();
    }
    else { $('a[href="#selectedContacts"]').show(); }
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

$(document).click(function () {
    $('#search').css("border-color", "none");
});

$('#imageUpld').fileupload({

    url: 'SaveProfileImageHandler.ashx?pic=small',
    add: function (e, data) {
        var uploadErrors = [];
        var acceptFileTypes = /^image\/(gif|jpe?g|png)$/i;
        if (data.originalFiles[0]['type'].length && !acceptFileTypes.test(data.originalFiles[0]['type'])) {
            uploadErrors.push('Not an accepted file type');
        }

        if (uploadErrors.length > 0) {
            alert(uploadErrors.join("\n"));
        } else {
            data.submit();
        }
    },
    done: function (e, data) {

        G_img_name = data.result;
        $('input[type="file"]').css('color', 'transparent');
        $('.jcrop-circle-demo').css('display', 'block');
        $('.jcrop-selection.jcrop-current').show();
        $("#webContactProfile").attr("src", "/Temp_crop_Images/" + G_img_name);

        load_crop_thumb();
    },
    error: function (e, data) {
        alert(data);
    }

});

// $('#grpTalkName').click(function(){
// $('#grpCallErrorMessage').hide();
// });
//--------------------------------------------Fetching All Web Lists Contacts
function getAllWebContacts(listId) {

    selectAllListId = listId;
    // if (listId == 0) {
    // $('#selectAll').hide();
    // }

    webPageIndex++;
    webScroll = 0;

    if (webPageIndex == 1 || webPageIndex <= webPageCount) {
        //$.blockUI({ message: '<h4> Loading...</h4>' })
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
                        webContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '" style="width:45px;"></div>';
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
                    else { $('#selectAll').text("Select All"); }

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
                                    ContactsList += '<div class="list1 highlight">';
                                    listId = result.ContactList[j].Id;
                                    $('#webList').html(result.ContactList[j].ListName + '(' + result.ContactList[j].listCount + ')');

                                }
                                else {
                                    ContactsList += '<div class="list1">';
                                }

                                ContactsList += '<div style="width: 100%; float: left;"><a data-target="#" href="javascript:void(0);" style="display: block;" id="' + result.ContactList[j].Id + '"  class="contactList1" lname="' + result.ContactList[j].ListName + '" lcount="' + result.ContactList[j].listCount + '">' + result.ContactList[j].ListName + '(' + result.ContactList[j].listCount + ')</a>';
                                ContactsList += ' </div></div>';
                            }
                        }

                        $('#list-group1').html(ContactsList);
                        if (isLiveCall == 1)
                            $('#webLists').html(ContactsList);

                    }

                    else if (result.ContactList.length == 0) {
                        $("#selectAll").hide()
                        $('#grpCallWebContacts').html('No Lists Found').show();
                    }
                }
                setTimeout($.unblockUI, 200);
            },
            error: function (result) {
                alert("Something Went Wrong");
                //$.unblockUI();
            }
        });
    }
}

//---------------------------------------------Fetching All Mobile Contacts
function getMobileContacts(src) {

    pageIndex++;
    global_alphabetpageIndex = 0;
    mobileScroll = 0;
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
            //  async: false,
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
                            selectedClass = ""; faCheckPic = "";
                            if ($.trim(result.contactDetails[j].source) == "1") {
                                // nameSplit = (result.contactDetails[j].name).substring(0, 2).toUpperCase();
                                for (var i = 0; i < contactIdArray.length; i++) {
                                    var responseContactId = $.trim(contactIdArray[i]);
                                    var contactsId = $.trim(result.contactDetails[j].id);

                                    if (responseContactId == contactsId) {
                                        selectedClass += "selected"
                                        faCheckPic += "block";

                                    }
                                    else {
                                        selectedClass += "";
                                        faCheckPic += "";
                                    }
                                }

                                mobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + result.contactDetails[j].id + '" listId="m1" >';
                                imgPath = "";
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
                                if (result.contactDetails[j].name.length > 25) {
                                    mobileContactsData += '<p title="' + result.contactDetails[j].name + '" name="' + result.contactDetails[j].name + '">' + result.contactDetails[j].name.substring(0, 25) + '..</p>';
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
                        //setTimeout($.unblockUI, 200);
                        $('#grpCallMobileContacts').append(mobileContactsData);

                    }
                }
                if (result.Success == false)
                { }
            },
            error: function (result) {
                alert("Something Went Wrong");
                //$.unblockUI();
            }


        });
    }
}


//--------------------------------------------Offset Scrolling for Web Contacts
function webContactsOffsetScroll(listId) {
    webPageIndex++;
    if (webPageIndex == 1 || webPageIndex <= webPageCount) {

        $.ajax({
            url: '/HandlersWeb/Contacts.ashx',
            type: 'post',
            async: false,
            dataType: 'json',
            data: {
                type: 2, listId: listId,
                pageIndex: webPageIndex,
            },
            success: function (result) {
                for (var i = 0; i < result.Items.length; i++) {

                    nameSplit = (result.Items[i].Name).substring(0, 2).toUpperCase();
                    selectedClass = ""; faCheckPic = "";
                    for (var j = 0; j < contactIdArray.length; j++) {
                        var responseContactId = $.trim(contactsId[j]);
                        var contactsId = $.trim(result.Items[i].ContactId);

                        if (contactsId == responseContactId) {
                            selectedClass += "selected"
                            faCheckPic += "block";
                        }
                        else {
                            selectedClass += "";
                            faCheckPic += "";
                        }
                    }
                    nameSplit = (result.Items[i].Name).substring(0, 2).toUpperCase();
                    imgPath = "";
                    if ($.trim(result.Items[i].ImagePath) == null) {
                        imgPath += 'images/avatar-img-5.jpg';
                    }
                    else if ($.trim(result.Items[i].ImagePath) != '')
                    { imgPath += result.Items[i].ImagePath; }
                    else {
                        imgPath += 'images/avatar-img-5.jpg'
                    }

                    webContactsData += '<div class="contacts margin-bottom-5 margin-right-5 ' + $.trim(selectedClass) + '" id="' + result.Items[i].ContactId + '" listID="0">';
                    webContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    webContactsData += '<div id="profileDetails">';
                    if (result.Items[i].Name.length > 25) {
                        webContactsData += '<p title="' + result.Items[i].Name + '" name="' + result.Items[i].Name + '">' + result.Items[i].Name.substring(0, 25) + '..</p>';
                    }
                    else { webContactsData += '<p name="' + result.Items[i].Name + '">' + result.Items[i].Name + '</p>'; }
                    webContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Items[i].MobileNumber + '</p>';
                    if ($.trim(faCheckPic).length == 0)
                    { faCheckPic = "none"; }
                    webContactsData += '</div><i class="fa fa-check select_check" style="display:' + faCheckPic + '"></i>';
                    webContactsData += '</div>';

                }

                $('#grpCallWebContacts').html(webContactsData);

            },
            error: function (result) {
                $.unblockUI();
                alert("Something Went Wrong");
            }
        });
    }
}



//--------------------------------------------Alphabetical sorting on web contacts and mobile contacts
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
                alphabetResponse(contactResult, sourceValue, listId);

            },
            error: function (contactResult) {
                Notifier("Something Went Wrong", 2);
            }

        });
    }
}


//-------------------------------------------Special Character Sorting Filter
function specialCharSearch(sourceValue, listId) {
    global_alphabetMobileContactsData = "";
    sortSubContactsResponse = new Array();
    $.ajax({
        url: "/HandlersWeb/Contacts.ashx",
        data: {
            type: 12,
            source: sourceValue,
            alphabetPageIndex: 1,
            modeSp: 3,
            listID: listId,
        },
        async: false,
        method: "POST",
        dataType: "JSON",
        success: function (contactResult) {
            alphabetResponse(contactResult, sourceValue, listId);
        },
        error: function (contactResult) {
            alert("Soemthing Went Wrong")
        }
    });
}

function alphabetOrSerachClick(global_searchndex, alphabet) {
    var activeTab = $('ul#myTabList li.active').find("a").attr("href");
    var sourceValue = 0;
    var spMode = 2;
    if (alphabet == '#') {
        spMode = 3;
    }
    if (activeTab == "#grpCallWebContacts" || activeTab == "#webContacts") {
        sourceValue = 2;
        alphabetClick++;
        if ($('.list1.highlight .contactList1').length == 0)
            $('#grpCallWebContacts').html('No Contacts Found');
        else
            alphabeticalContactSorting(sourceValue, global_searchndex, alphabet, $('.list1.highlight .contactList1').attr("id"), spMode);

    }
    else if (activeTab == "#grpCallMobileContacts") {
        sourceValue = 1;
        mobileAlphabetClick++;
        alphabeticalContactSorting(sourceValue, global_searchndex, alphabet, 0, spMode);
    }
    else {
        selectedContactsTabClick(alphabet);
    }
}

function alphabetResponse(contactResult, sourceValue, listId) {

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

                listIds = global_listId.split(",");
                for (var j = 0; j < contactIdArray.length; j++) {
                    var responseContactId = $.trim(contactIdArray[j]);
                    var contactsId = "";

                    if ($.trim(contactResult.contactDetails[contactRows].source) == "1") {
                        contactsId = $.trim(contactResult.contactDetails[contactRows].id);
                    }
                    else {
                        contactsId = $.trim(contactResult.contactDetails[contactRows].ContactId);
                    }
                    if (contactsId == responseContactId) {
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
                    global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + contactResult.contactDetails[contactRows].id + '" listId="' + listId + '" >';
                    global_alphabetMobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    global_alphabetMobileContactsData += '<div id="profileDetails">';
                    if (contactResult.contactDetails[contactRows].name.length > 25) {
                        global_alphabetMobileContactsData += '<p title="' + contactResult.contactDetails[contactRows].name + '" name="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '..</p>';
                    }
                    else {
                        global_alphabetMobileContactsData += '<p name="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name + '</p>';
                    }

                    if ($.trim(contactResult.contactDetails[contactRows].subContactDeatils).length != 0) {
                        subContactsResponse.push(contactResult.contactDetails[contactRows].subContactDeatils);
                        // global_alphabetMobileContactsData += '<p id="phoneNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" style="float:left;color:#0a93d7 !important"><i class="fa fa-mobile" aria-hidden="true"></i> ' + contactResult.contactDetails[contactRows].mobileNumber + '<div class="dropdown" style="float:right;margin: -3px 0 0;"> <a id="btnDropdown"  contactid=' + contactResult.contactDetails[contactRows].id + ' mobile=' + contactResult.contactDetails[contactRows].mobileNumber + ' class="dropdown-toggle"  data-toggle="dropdown"><span class="caret"></span></a> <ul class="dropdown-menu" id="mobileNumber' + contactResult.contactDetails[contactRows].mobileNumber + '" ></ul></div></p>';
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
                    //}
                    global_alphabetMobileContactsData += '</div>';
                    mobileScroll = 1;

                }
                else {
                    webScroll = 1
                    global_alphabetMobileContactsData += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + contactResult.contactDetails[contactRows].ContactId + '" listId="' + listId + '" >';
                    global_alphabetMobileContactsData += '<div id="profilePic"><img alt="default user" src="' + imgPath + '"></div>';
                    global_alphabetMobileContactsData += '<div  listId="' + listId + '" id="profileDetails">';
                    if (contactResult.contactDetails[contactRows].name.length > 25) {
                        global_alphabetMobileContactsData += '<p title="' + contactResult.contactDetails[contactRows].name + '" name="' + contactResult.contactDetails[contactRows].name + '">' + contactResult.contactDetails[contactRows].name.substring(0, 25) + '..</p>';
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
            global_alphabetMobileContactsData += "No Contacts Found";
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

        global_alphabetMobileContactsData += "No Contacts Found";
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
//-------------------------------------------function calling on adding new contacts
function manageWebContacts(mode, contactID, listId, name, mobileNumber) {
    var imageName = G_img_name.replace("/ContactImages/", "")
    console.log(mobileNumber);

    $.ajax({
        url: "/HandlersWeb/Contacts.ashx",
        data: {
            type: 4,
            mode: mode,
            contactID: contactID,
            name: name,
            listID: listId,
            listName: '',
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
                if (result.Message != 'Contact Already Exist in List') {

                    Notifier('Contact created successfully ', 1);
                    if (parseInt(listId) == parseInt($('.list1.highlight .contactList1').attr("id"))) {
                        var contactsAdd = "";
                        contactsAdd += '<div class="contacts margin-right-5 margin-bottom-5" id="' + result.ContactId + '" listID="' + listId + '">';
                        if ($.trim(imageName).length == 0) {
                            imageName = "images/avatar-img-5.jpg";
                        }
                        else { imageName = "ContactImages/" + imgName; }
                        contactsAdd += '<div id="profilePic"><img alt="default user" src="' + imageName + '"></div>';
                        contactsAdd += '<div listId="0" id="profileDetails">';
                        if (name.length > 25) {
                            contactsAdd += '<p title="' + name + '" name="' + name + '">' + name.substring(0, 25) + '..</p>';
                        }
                        else {
                            contactsAdd += '<p name="' + name + '">' + name + '</p>';
                        }
                        contactsAdd += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNumber + '</p>';


                        contactsAdd += '</div><i class="fa fa-check select_check" style="display:none"></i>';
                        contactsAdd += '</div></div>';
                        $('#grpCallWebContacts').append(contactsAdd);

                    }
                    $('.contactList1').each(function (e) {
                        var contactLis = $('.contactList1')[e];
                        if ($(contactLis).attr("id") == listId) {
                            var listName = $(contactLis).attr("lname");
                            var lcount = parseInt($(contactLis).attr("lcount")) + 1;
                            $(contactLis).html(listName + '(' + lcount + ')');
                        }
                    });
                    $('#contactsModal').modal("hide");

                }
                else {
                    if (mygrpTalks == 1) {
                        if (nonMemberExisted == 0)
                            Notifier(result.Message, 2);
                    }
                    else
                        Notifier(result.Message, 2);

                    $('#contactsModal').modal("hide");

                }
                if (mygrpTalks == 1) {
                    $('.addtogroup').each(function (e) {
                        if ($(this).attr('number').slice(-10) == mobileNumber.slice(-10)) {
                            //$('.addtogroup').click();
                            addToGroup(mobileNumber, name, listId);
                            $("img[batchid='" + addtophonebookBatchId + "'][number='" + mobileNumber + "'].addtophonebook").parents('td').text(name);


                        }
                    });
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
}


//-------------------------------------------function calling for uploading excel contatcs
function uploadContactsThroughExcel(fileName) {
    $.ajax({
        url: '/ExcelUpload.ashx',
        type: 'post',
        data: { type: 2, fileName: fileName },
        dataType: 'json',
        success: function (result) {
            $('#successMessage,#errorMessage').html("");
            if (result.Status == 1) {
                $('#successMessage').html(result.Message);
                setTimeout(function () {
                    window.location.reload();
                }, 1000);
            }
            else {
                $('#errorMessage').html(result.Message);
                setTimeout(function () {
                    window.location.reload();
                }, 1000);
            }
        },
        error: function (result) {
            alert("Something Went Wrong");
        }
    });

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
    if (month < 10)
        month = '0' + month;

    currentDate = month + "-" + day + "-" + year;
    return currentDate;
}

//-------------------------------------------Create Group call
function createGroupCall(response, mode, dialMute) {
    $.blockUI({ message: '<h4> Creating Group </h4>' })
    $.ajax({
        url: 'HandlersWeb/Groups.ashx',
        async: false,
        method: "POST",
        dataType: "JSON",
        data: { type: 4, paramObj: response },
        success: function (result) {
            if (result.Success == true) {
                if (mode == 1) {
                    var grpCallId = 0;
                    grpCallId = result.GroupCallDetails.GroupID;
                    groupCallDial(grpCallId, dialMute);

                    setTimeout(function () {
                        window.location.href = "/MyGrpTalks.aspx";
                    }, 2000);

                }
                if (mode == 2) {
                    Notifier('Schedule Call created successfully', 1);
                    window.location.href = "/MyGrpTalks.aspx";
                }
                if (mode == 3) {
                    Notifier('Group call saved successfully', 1);
                    setTimeout(function () {
                        window.location.href = "/MyGrpTalks.aspx";
                    }, 2000);
                }

            }
            else {
                Notifier(result.Message, 2);
                $('#dial,#muteDial').prop('disabled', false);
            }
            setTimeout($.unblockUI(), 200);

        },
        error: function (result) {
            $.unblockUI();
            $('#dial,#muteDial').prop('disabled', false);
            Notifier("Something Went Wrong", 2);
        }
    });
}

function groupCallDial(grpCallId, dialMute) {
    $.blockUI({ message: '<h4> Initialing grpTalk call </h4>' })
    var dialObj = '{"ConferenceID":"' + grpCallId + '","IsAll":"True","MobileNumber":"","IsCallFromBonus":"0","IsMute" : "' + dialMute + '"}';
    $.ajax({
        url: '/HandlersWeb/GroupCalls.ashx',
        type: 'post',
        data: { type: 1, dialObj: dialObj },
        dataType: 'json',
        success: function (result) {
            $.unblockUI();
        },
        error: function (result) {
            $.unblockUI();
            Notifier(result.Message, 2);
        }
    });
}

//-------------------------------------------Mobile number validation
function MobileValidator(mobile) {

    var ret = 1;
    var filter = /^[0-9]*$/;
    if (!(filter.test(mobile)))
        ret = 0;

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



function AddContact(result, name, mobileNo) {
    var addcontact = "";
    $('#contactsModal').modal("hide");
    addcontact += '<div class="contacts margin-right-5 margin-bottom-5 ' + $.trim(selectedClass) + '" id="' + result.contactID + '" listId="' + listId + '">';
    //webContactsData += '<div id="profilePic"><span style=" background: #add9ec none repeat scroll 0 0;border-radius: 50%;color: #428bca;font-weight: bold;height: 34px;padding: 9px;width: 32px;">' + nameSplit + '</span></div>';
    addcontact += '<div id="profilePic"><img alt="default user" src="images/avatar-img-5.jpg"></div>';

    addcontact += '<div listId="0" id="profileDetails">';
    if (name.length > 25) {
        addcontact += '<p name="' + name + '" title="' + name + '">' + name.substring(0, 25) + '..</p>';
    }
    else {
        addcontact += '<p name="' + name + '">' + name + '</p>';
    }
    addcontact += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + mobileNo + '</p>';

    addcontact += '</div><i class="fa fa-check select_check" style="display:none"></i>';
    addcontact += '</div>';


    $('#grpCallWebContacts').append(addcontact).show();

}

function WebTabClick() {

    if ($('#grpCallMobileContacts').parent('.slimScrollDiv').length != 0) {
        $('#grpCallMobileContacts').parent('.slimScrollDiv').attr("display", "none");
    }
    $('#grpCallMobileContacts').hide();
    $('#grpCallWebContacts,.alphabet').show();
    $('#grpCallWebContacts,#list-group1').parent().show();
    $('#contactList').show();
    $('.addNewContact').show();
    $('.list1').show();
    $('#phoneContactsDisplay,.phnCntcts').hide();
    $('#selectedContacts').hide();
    if ($('#selectedContacts').parent('.slimScrollDiv').length != 0) {
        $('#selectedContacts').parent().hide();
    }

    $('#alphabetMobileContacts,#alphabetWebContacts').hide();
    $('#grpCallWebContacts').addClass("active in");
    $('#grpCallMobileContacts,#selectedContacts').removeClass("active in");
    if (selectAllListId != 0) {
        $('#selectAll').show();
    }
}

$(document).click(function () {
    $('#search').css("border-color", "none");
});


function sliceNumberFn() {
    var countryId = $('#countryID').val();
    if (countryId == 108 || countryId == 241) {
        sliceNumber = -10;
    }
    else if (countryId == 19 || countryId == 124 || countryId == 173) {
        sliceNumber = -8;
    }
    else if (countryId == 199 || countryId == 239)
    { sliceNumber = -9; }
    else
    { sliceNumber = -7; }
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
            divSelected += '<div class="contacts margin-right-5 margin-bottom-5 unselected" id=' + contactIdArray[i] + ' listId=' + listIdArray[i] + '><div id="profilePic"><img alt="default user" src="images/avatar-img-5.jpg"></div><div id="profileDetails">';
            if (nameArray[i].length > 25) {
                divSelected += '<p name="' + nameArray[i] + '" title="' + nameArray[i] + '">' + nameArray[i].substring(0, 25) + '..</p><p><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedMobileNos[i] + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>';
            }
            else { divSelected += '<p name="' + nameArray[i] + '">' + nameArray[i] + '</p><p><i class="fa fa-mobile" aria-hidden="true"></i> ' + selectedMobileNos[i] + '</p></div><i class="fa fa-check select_check" style="display:block"></i></div>'; }
        }
    }
    if ($.trim(divSelected) == "")
    { divSelected = "No Contacts Found"; }
    $('#selectedContacts').html(divSelected);

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
    if ($('.weekDay.active').length == 0) {
        selectedDate = new Date(scheduleDateTime[0] + " " + scheduleDateTime[1]);
        if (Date.parse(selectedDate) < Date.parse(minDate)) {
            $('#openlineswitch').attr("disabled", true);
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

function startNowResponse(mute) {
    memberName = "", memberMobile = "", resStr = "", response = "", participants = "";
    nameSplit = [];
    var webListIds = '';

    $('#selectedContacts .contacts').each(function () {
        contactId = $(this).attr('id');
        if ($(this).hasClass('unselected')) {
            memberName = $(this).find('p:eq(0)').attr('name');
            memberMobile = $(this).find('p:eq(1)').text();
            if ($(this).attr('listId') == "m1") {
                listIds = 0;
            }
            else {
                listIds = $(this).attr('listId');
                webListIds = 0;
            }

            participants += '{"' + memberName + '":"' + memberMobile + '","IsDndFlag":"false","ListId":"' + listIds + '"}' + ",";
            participantsArray.push(memberName);
        }

    });

    resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '').replace('/', '//').replace('\\', '\\\\') + " ]";

    displayCurrentTime();
    displayCurrentDate();

    grpTalkName = $('#grpTalkName').val();
    response = '{"GroupCallName":"' + grpTalkName + '","SchType":"100","SchduledTime":"' + currentTime + '","SchduledDate":"' + currentDate + '","IsMuteDial":"' + mute + '","IsOnlyDialIn":"' + 0 + '","WeekDays":"","Reminder":"30","WebListIds":"' + webListIds + '","managerMobile":"' + managerMobile + '",' + resStr + '}';
    return response;
}


function load_crop_thumb() {
    //$('#avatar_first').load(function () {

    $('#webContactProfile').Jcrop({
        bgOpacity: 0.5,
        bgColor: 'white',
        allowResize: true,
        boxWidth: 280,
        boxHeight: 200,
        // Change default Selection component for new selections
        selectionComponent: CircleSel,

        // Use a default filter chain that omits shader
        applyFilters: ['constrain', 'extent', 'backoff', 'ratio', 'round'],

        // Start with circles only
        aspectRatio: 1,

        // Set an initial selection
        setSelect: [10, 10, 80, 80],

        // Only n/s/e/w handles
        handles: ['n', 's', 'e', 'w'],

        // No dragbars or borders
        dragbars: [],
        borders: [],
        onChange: storeCoords,
        onSelect: storeCoords

    }, function () {
        this.container.addClass('jcrop-circle-demo');
    });
    //}).on('error', function (event, id, name, errorReason, xhrOrXdr) {
    //});
}


function storeCoords(c) {
    roundCropX = c.x;
    roundCropY = c.y;
    roundCropW = c.w;
    roundCropH = c.h;
}

function newListAddContact(mode, contactID, newList, name, mobileNumber) {
    var imageName = G_img_name.replace("/ContactImages/", "")

    if (($("a[lname='" + newList + "']").hasClass("contactList1")) == false)
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
                    if ($.trim($('#list-group1').html()) == 'No Lists Found') {
                        $('#list-group1').html('');
                        highlightClass = 'highlight';
                    }
                    $('#list-group1').append('<div class="list1 ' + highlightClass + '"><div style="width: 100%; float: left;"><a data-target="#" href="javascript:void(0);" style="display: block;" id="' + result.List[0].ListId + '" class="contactList1"  lname="' + newList + '" lcount="1">' + newList + '(' + result.List[0].ListCount + ')</a> </div></div>')
                    if (mygrpTalks == 1) {
                        $('.addtogroup').attr("listId", result.List[0].ListId);
                        $('.addtogroup').each(function (e) {
                            if ($(this).attr('number').slice(-10) == mobileNumber.slice(-10)) {
                                //$('.addtogroup').click();						
                                addToGroup(mobileNumber, name, result.List[0].ListId);

                                $("img[batchid='" + addtophonebookBatchId + "'][number='" + mobileNumber + "'].addtophonebook").parents('td').text(name);
                            }
                        });
                    }

                    if (parseInt($('#list-group1 .list1').length) == 1) {
                        $('.contactList1').click();
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
        error: function (jqXHR, exception) {

        }
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

function tabClickFetchtContacts() {
    if (alphabetClick > 0) {
        $('#grpCallWebContacts').html('');
        webPageIndex = 0;
        $('#grpCallWebContacts').slimScroll({ destroy: true });
        $('#grpCallWebContacts').slimScroll({
            allowPageScroll: false,
            height: '250',

        });
        var lisId = $('.list1.highlight .contactList1').attr("id")
        webPageIndex = 0;
        getAllWebContacts(lisId);
        alphabetClick = 0;

    }
    if (mobileAlphabetClick > 0) {

        $('#grpCallMobileContacts').html('');
        pageIndex = 0;
        mobileContactsData = "";
        $('#grpCallMobileContacts').slimScroll({ destroy: true });

    }
    if (firstClick == 0 || mobileAlphabetClick > 0) {
        $('#grpCallMobileContacts').slimScroll({
            allowPageScroll: false,
            height: '250',
        });
        mobileAlphabetClick = 0;
        getMobileContacts(1);
    }
    if (nameArray.length == 0)
    { $('#selectedContacts').html(''); }
    $('#search-input').val('');
    selectedContactsTabClick('');
}