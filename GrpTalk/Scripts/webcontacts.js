/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />
var regEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var regMobile = /^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[9|8|7][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$/;
var reg = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
var jsonWebContacts = new Array();
var webContactsData = ""; webContactsAppend = "", contactID = 0, userName = "", mobile = "";
var G_img_name = "";
var roundCropX = 0; roundCropY = 0; roundCropW = 0; roundCropH = 0;
var jcrop_api = "", G_Event_Pic = "";
var CircleSel = "", imgPath = "", path = "", listId = 0, jsonContactsByList = new Array();
var webListsAppend = "", webContactsList = "";
var newContactListId = 0;
var webPageInde = 0; var webPageCount = 0, firstClick = 0;
var countryID = 0; Iregx = "";
var webContactsListAll = ""; var pageIndexModal = 0, modalPageCount = 0, webContactsDataModal = "", Iregx = "";
var inputs = $('#addListContactsModal').find('input');
var weblistNames = "";
var xlList = "block";
var listModal = "", defaultWebListId = 0;
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
    getAllWebContacts(listId); //Loading All Webcontacts 

    $('#search-input').val('');

    $('#plusSymbol').click(function (e) {
        ListNames();
        $('#listItems').show();
        $("#name").val('');
        $("#mobileNumber").val('');
        $("#ddlWebList").val('0');
        $("#newWebList").val('');
        G_img_name = "";
        xlList = "block";
        hidePopUpItems();

        $("ul#listContactsModal li a[href='#allContacts']").hide(); $("ul#listContactsModal li a[href='#mobileContacts']").hide();
        $("ul#listContactsModal li a[href='#excelFormBody']").show();
        $('#newWebList').removeAttr("disabled");

    });

    $(document).on('click', '.addNewContact', function (e) {
        $('#listItems').hide();
        xlList = "none";
        listId = $(this).attr("listid");
        ListNames();
        hidePopUpItems();
        G_img_name = "";
        $("ul#listContactsModal li a[href='#allContacts']").show(); $("ul#listContactsModal li a[href='#mobileContacts']").show(); $('#allContacts').hide(); $("ul#listContactsModal li a[href='#excelFormBody']").show();
    });

    $('.mobileContacts').click(function (e) {

    });

    // var firstInput = inputs.first();
    // var closeBtn = $('#closeList');


    // closeBtn.on('keydown', function (e) {
    // if ((e.which === 9 && !e.shiftKey)) {
    // e.preventDefault();
    // firstInput.focus();
    // }
    // });



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

    $(document).on('focusout', '#newWebList', function (e) {
        if (e.relatedTarget.id == "closeList") {
            $('.close').click();
            return false;
        }
        else if (e.relatedTarget.id == "addCntctList") {
            $('#addCntctList').click();
            return false;
        }
        if (parseInt($('#ddlWebList').val()) == 0) {
            var name = $('#newWebList').val();
            if (name.length < 1) {

                $('#errorDescForWebList').html('You must add this contact to an existing weblist or create a new list');
            }
            else {

                $('#errorDescForWebList').html('');
            }
        }
    });


    $(document).on('click', '#addCntctList', function (e) {
        var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
        var name = $('#name').val();
        var mobileNumber = $('#mobileNumber').val();
        if (xlList == "block") {
            listId = $('#ddlWebList').val();
            newList = $('#newWebList').val();
        }
        var IsValid = true;

        //$('ul#myWebTab li').last("a").show();

        if (name.toString() == "" || name == null) {
            IsValid = false;
            $('#name').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('Please Enter Name');
        }
        else {
            $('#name').closest('.form-group').removeClass('has-error').addClass('has-success')
            $('#errorDescForName').html('');
        }
        countryID = $('#countryID').val();
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
            console.log(countryID);
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
        if (xlList == "block") {
            if (parseInt(listId) == 0 && $.trim(newList) == '') {
                IsValid = false
                $('#errorDescForWebList').html("You must add this contact to an existing weblist or create a new list");
            }
            else {
                $('#errorDescForWebList').html('');
                IsValid = true;
            }

        }


        if (!IsValid) {
            return false;
        }

        if (countryID == 108) {
            if (mobileNumber.length == 10) { mobileNumber = "91" + mobileNumber; }
        }
        if (countryID == 241) { mobileNumber = "1" + mobileNumber; }


        // if(parseInt(listId) != 0 && listId != null)
        // {
        // //manageWebContacts(1, 0, name, mobileNumber, "", listId);
        // }
        // else{
        // //newListAddContact(1, 0, newList, name, mobileNumber);
        // }
        if ($('#ddlWebList').is(':visible')) {
            listId = $('#ddlWebList').val();
        }



        $.ajax({
            url: '/HandlersWeb/Contacts.ashx',
            method: 'POST',
            dataType: 'JSON',
            data: {
                type: 12,
                mode: 1,
                listId: listId,
                Mobile: $('#mobileNumber').val()
            },
            success: function (result) {
                var str = '', SameNameId = '';
                //if(result.contactName.length>0 && $('#name').val().toLowerCase()==result.contactName[0].Name.toLowerCase())
                //alert(result.contactName[0].Name)

                if (result.Success == false) {
                    Notifier(result.Message, 2);
                    return;
                }
                else if (result.contactName.length > 0) {
                    str += '<div class="well well-sm margin-bottom-10">';
                    str += '<div class="row"><div class="col-sm-2">' + $('#mobileNumber').val() + '</div><div class="col-sm-10">';
                    str += '<label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" id="0" value="' + name + '"/>' + name + '</label>';
                    for (var i = 0; i < result.contactName.length; i++) {
                        if (name.toLowerCase() == result.contactName[i].Name.toLowerCase())
                            SameNameId = result.contactName[i].Id;
                        else
                            str += '<label class="pull-left margin-right-15"><input type="radio" class="margin-right-5" name="repeat" id="' + result.contactName[i].Id + '" value="' + result.contactName[i].Name + '"/>' + result.contactName[i].Name + '</label>';
                    }
                    str += '</div></div></div>';
                    $("#excelDup").html(str);
                    if (SameNameId != '')
                        $("input[name='repeat'][id='0']").attr('id', SameNameId);
                    $("#btnSave,#btnSaveExcel").show();
                    $("#btnSaveExcel").attr('id', 'btnSave');
                    if ($("input[name='repeat']").length == 1) {
                        $("input[name='repeat']:first").attr("checked", true);
                        $("#btnSave").click();
                    }
                    else {
                        $("#addWebListContacts").modal('hide');
                        $("#selectDblContacts").modal('show');
                        $("#btnSave").show();
                    }
                }
                else {
                    if (parseInt(listId) != 0 && listId != null) {
                        manageWebContacts(1, 0, name, mobileNumber, "", listId, 1);
                    }
                    else {
                        newListAddContact(1, 0, newList, name, mobileNumber);
                    }
                }

            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == 401) {
                    window.location.href = "/Home.aspx?message=Session expired";
                }
                else {

                    Notifier("Something went wrong", 2);
                    console.log(errorThrown);
                }
            }
        });


    });
    $("#btnSave").click(function () {
        if (this.id == 'btnSave') {
            var contactId = ($("input[name='repeat']:checked").attr('id'));
            var name = $('#name').val();
            var mobileNumber = $('#mobileNumber').val();
            if (xlList == "block") {
                listId = $('#ddlWebList').val();
                newList = $('#newWebList').val();
            }

            if (parseInt(listId) != 0 && listId != null) {
                manageWebContacts(1, contactId, name, mobileNumber, "", listId, 1);
            }
            else {
                newListAddContact(1, contactId, newList, name, mobileNumber);
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
        }
        else { $('#newWebList').attr('disabled', false); }
    });


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
    $(document).on('click', '.fa-edit', function () {
        $('#name, #mobileNumber').val('');
        $('.jcrop-circle-demo').css('display', 'none');
        $('#webContactProfile').attr('src', '');
        $('#contactsAllList,#mobileContacts').hide();
        $('#saveListContact').hide()
        $('#allListContacts').hide();
        $('#addListContacts,#allContacts,#addListContactsModal').show();
        $("ul#myWebTab li").addClass("active")
        $('#webContactProfile').attr('src', '');
        $('ul#listContactsModal li').removeClass("active");
        $('.modal-body .tab-content div').removeClass("active in");
        $('div#addListContactsModal').addClass("active in")
        $('.modal-footer button').hide();
        $('#addCntctList, #closeList').show();
        $('ul#listContactsModal li.addContacts').addClass("active");
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('#errorDescForName, #errorDescForMobile,#errorDescForWebList').html('');
        $('#listItems').hide();
        $('#updateContact,.jcrop-active.jcrop-circle-demo').show();
        $('#addCntctList').hide();
        $('#contactsAllList').hide();
        $('#saveListContact').hide()
        $('#excelUpload').hide();
        $('#allContacts').hide();
        $('#successMessage,#errorMessage').html("");
        $('#excelFormBody').hide();
        $('#saveExcelContacts').hide();
        $("ul#listContactsModal li a[href='#allContacts']").hide();
        $("ul#listContactsModal li a[href='#mobileContacts']").hide();
        $("ul#listContactsModal li a[href='#excelFormBody']").hide();
        contactID = $(this).attr('id');
        mobile = $(this).attr('mobile');
        userName = $(this).attr('userName');
        G_img_name = "";
        $('#webContactProfile').attr('src', $(this).attr('imgPath'));
        $('#name').val(userName);
        $('#mobileNumber').val(mobile);
        $('.jcrop-box.jcrop-drag,.jcrop-selection.jcrop-current').hide();



        //$('#addWebListContacts').addClass("in");
    });

    $(document).on('click', '#updateContact', function () {
        var name = $('#name').val();
        var mobileNumber = $('#mobileNumber').val();
        var listIdNo = $('.list.highlight').attr("id");
        //contacts 
        //idno
        // alert('div[idno="'+listIdNo+'"]')
        // alert($('div[idno="'+listIdNo+'"]').children().html())
        var IsValid = true;
        if (name.toString() == "" || name == null) {
            IsValid = false;
            $('#name').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('Please Enter Name');
        }
        else {
            $('#name').closest('.form-group').removeClass('has-error').addClass('has-success')
            $('#errorDescForName').html('');
        }
        countryID = $('#countryID').val();
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
            console.log(countryID);
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



        if (!IsValid) {
            return false;
        }

        if (countryID == 108) {
            if (mobileNumber.length == 10) { mobileNumber = "91" + mobileNumber; }
        }

        manageWebContacts(2, contactID, name, mobileNumber, "", listIdNo, 0);

        listClickContactsDisplay(listIdNo)
        contactID = 0;
    });

    $('#imageUpld').fileupload({
        //$('#imageUpld').change(function(){
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
            $('.jcrop-box.jcrop-drag,.jcrop-selection.jcrop-current').show();

            $("#webContactProfile").attr("src", "/Temp_crop_Images/" + G_img_name);

            load_crop_thumb();

        },
        error: function (e, data) {
            alert(data);

        }

    });
    $(document).on("click", ".deleteListName", function () {
        var deleteListIdNo = $(this).attr("id");
        var x = confirm("These contacts will be deleted from all the groups, Are you sure you want to delete this weblist?");
        if (x == true) {
            $.ajax({
                url: "/HandlersWeb/Contacts.ashx",
                dataType: "json",
                type: "post",
                data: {
                    type: 11,
                    listId: deleteListIdNo,
                    mode: 4,
                },
                success: function (result) {
                    window.location.reload();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (jqXHR.status == 401) {
                        window.location.href = "/Home.aspx?message=Session expired";
                    }
                    else {

                        Notifier("Something went wrong", 2);
                        console.log(errorThrown);
                    }
                }
            });
        }
        else {
            return false;
        }
    });

    $(document).on('click', '.delete', function (e) {
        if ($("#list-group #addItem").length != 0) {
            $("#list-group #addItem").remove();
            global_addContactList = 0;
        }
        if ($("input[identity='editLisItemInput']").length != 0) {
            $("input[identity='editLisItemInput']").remove();
            $("a[id='" + editListId + "']").html('<span class="contactList" id="' + editListId + '" lcount="' + listContactsCount + '" lname="' + editlistName + '">' + editlistName + '</span>(<span id="count' + editListId + '">' + listContactsCount + '</span>)');
            global_addContactList = 0;
        }
        contactID = $(this).attr('id');
        listId = $('.list.highlight').attr("id");

        var x = confirm("This contact will be deleted from all the groups, Are you sure you want to delete the contact?");
        if (x == true) {
            $.ajax({
                url: "/HandlersWeb/Contacts.ashx",
                type: "post",
                dataType: "json",
                data: {
                    type: 3,
                    contactID: contactID,
                    listID: listId,
                    mode: 5
                },
                success: function (result) {
                    if (listId == 0)
                    { window.location.reload(); }
                    else {
                        var cnt = $("span[id='count" + listId + "']").text();
                        cnt = cnt - 1;
                        $("span[id='count" + listId + "']").text(cnt);
                        $('div [idNo="' + contactID + '"]').remove();

                        if (cnt == 0)
                        { $('#webLists').html('No Contacts are found in this list') }
                    }
                    Notifier(result.Message, 1);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    if (jqXHR.status == 401) {
                        window.location.href = "/Home.aspx?message=Session expired";
                    }
                    else {

                        Notifier("Something went wrong", 2);
                        console.log(errorThrown);
                    }
                }
            });
        }
        else {
            return false;
        }
    });



    $(document).on('focusout', '#name', function (e) {
        if (e.relatedTarget.id == "closeList") {
            $('.close').click();
            return false;
        }
        else if (e.relatedTarget.id == "addCntctList") {
            $('#addCntctList').click();
            return false;
        }
        var name = $('#name').val();
        if (name.length < 1) {
            $('#name').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('Please Enter Name');
        }
        else {
            $('#name').closest('.form-group').removeClass('has-error').addClass('has-success')
            $('#errorDescForName').html('');
        }
    });

    //--------------------------Validation while focusing on textbox
    $(document).on('focusout', '#mobileNumber', function (e) {
        e.stopPropagation();

        var mobileNumber = $('#mobileNumber').val();
        IsValid = true;

        countryID = $('#countryID').val();
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
            console.log(countryID);
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
        if (e.relatedTarget.id == "closeList") {
            $('.close').click();
            return false;
        }
        else if (e.relatedTarget.id == "addCntctList") {
            if ($('#errorDescForMobile').html().length > 0) {
                $('#addCntctList').click();
                return false;
            }

        }

    });

    $(document).on('click', '#list-group .contactList', function () {

        listId = $(this).attr('id');
        $('#webLists').html('');
        if ($("#list-group #addItem").length != 0) {
            $("#list-group #addItem").remove();
            global_addContactList = 0;
        }
        $('#createGrpList,.list').removeClass("highlight");
        $(this).parents(".list").addClass("highlight");
        listClickContactsDisplay(listId);

    });


    $(document).on('click', '#saveListName', function () {
        $(this).attr('id');
        $(this).attr('listName');
    });


});

function getAllWebContacts(listId) {

    webContactsListAll = "";
    webPageInde++;
    $('span[id=searchCount]').html('<br>');
    if (webPageInde == 1 || webPageInde <= webPageCount) {
        $.blockUI({ message: '<h4> Loading...</h4>' })
        $.ajax({
            url: '/HandlersWeb/Contacts.ashx',
            type: 'post',
            async: false,
            dataType: 'json',
            data: {
                type: 2, listId: listId,
                pageIndex: webPageInde,
            },
            success: function (result) {
                jsonWebContacts = result;
                webContactsData = "";
                ContactsList = "";
                webContactsList = "";
                webPageCount = result.pageCount;
                var weblistNames = "";
                if (result.Data.length > 0) {
                    for (var i = 0; i < result.Data.length; i++) {



                        if (result.Data[i].ImagePath == null) {
                            imgPath = 'images/avatar-img-5.jpg';
                        }
                        else if (result.Data[i].ImagePath != "") {
                            imgPath = result.Data[i].ImagePath
                        }
                        else {
                            imgPath = 'images/avatar-img-5.jpg';
                        }
                     
                      webContactsData += '<div class="contacts margin-bottom-5 margin-right-5" idNo="' + result.Data[i].Id + '"> '; 
                        webContactsData += '<div id="profilePic">';
                        webContactsData += '<img alt="user" src="' + imgPath + '"></div>';
                        webContactsData += '<div id="profileDetails">';
                        if (result.Data[i].Name.length > 25) {
                            webContactsData += '<p title="' + result.Data[i].Name + '">' + result.Data[i].Name.substring(0, 25) + '..</p>';
                        }
                        else {
                            webContactsData += '<p>' + result.Data[i].Name + '</p>';
                        }

                        webContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Data[i].MobileNumber + '</p>';
                        webContactsData += '</div>';
                        if (result.Data[i].Source == 2) {
                            webContactsData += '<div class="contact_actions"><a href="javascript:void(0);"  data-toggle="modal" data-target="#addWebListContacts"><i class="fa fa-edit" id="' + result.Data[i].Id + '" userName="' + result.Data[i].Name + '" mobile = "' + result.Data[i].MobileNumber + '" imgPath="' + imgPath + '"></i></a><a href="javascript:void(0);" class="delete" id="' + result.Data[i].Id + '"><i class="fa fa-trash-o"></i></a></div>';
                        }
                        else { webContactsData += '<div class="contact_actions"><a href="javascript:void(0);"  data-toggle="modal" data-target="#addWebListContacts"><a href="javascript:void(0);" class="delete" id="' + result.Data[i].Id + '"><i class="fa fa-trash-o"></i></a></div>'; }
                        webContactsData += '</div>';

                    }
                    $('#webLists').html('');
                    $('#webLists').html(webContactsData).show();
                    $('.alphabet').show();

                }
                else {
                    $('.alphabet').hide();
                    $('#webLists').html('No Contacts are found in this list').show();

                }
                if (firstClick == 0) {
                    var ContactsList = "";
                    //ContactsList += '<div class="list"><div style="width:93%;float:left;"><a style="display:block;" href="javascript:;" id="getAllContacts">All Contacts(<span id="allContactsCnt">' + result.AllContactsCount + '</span>)</a></div><div style="width:7%;float:right;text-align:center;"><a style="display: block;" href="javascript:;" data-toggle="dropdown" class=" dropdown dropdown-toggle"><i class="fa fa-ellipsis-v" aria-hidden="true"></i></a><ul class="dropdown-menu dropdown-menu-right" class="addNewContact" listid="0"><li unique="modal" listId="0"><a href="#" data-toggle="modal" data-target="#addWebListContacts">Add Contacts</a></li></ul></div></div>';
                    if (result.ContactList.length > 0) {


                        //ContactsList += '<div class="list"><a href="javascript:;" id="getAllContacts">All Contacts</a><i class="fa fa-ellipsis-v dropdown dropdown-toggle" data-toggle="dropdown" aria-hidden="true"></i><ul class="dropdown-menu dropdown-menu-right"><li class="addNewContact" listid="0"><a href="#" data-toggle="modal" data-target="#addWebListContacts" listid="0">Add Contacts</a></li></ul></div>';
                        for (var j = 0; j < result.ContactList.length; j++) {
                            if (result.ContactList[j].Source == 2) {
                                if (j == 0) {
                                    ContactsList += '<div class="list highlight" id="' + result.ContactList[j].Id + '" >';
                                    weblistNames = '<option value="0">Select List</option>'
                                    listId = result.ContactList[j].Id;
                                }
                                else { ContactsList += '<div class="list" id="' + result.ContactList[j].Id + '" >'; }
                                weblistNames += '<option value="' + result.ContactList[j].Id + '">' + result.ContactList[j].ListName + '</option>'
                                ContactsList += '<div style="width:93%;float:left;"><a  id="' + result.ContactList[j].Id + '" class="contactList"><span  id="' + result.ContactList[j].Id + '" lcount="' + result.ContactList[j].listCount + '" lname="' + result.ContactList[j].ListName + '">' + result.ContactList[j].ListName + '</span>(<span id="count' + result.ContactList[j].Id + '">' + result.ContactList[j].listCount + '</span>)</a></div>';
                                ContactsList += '<div style="width:7%;float:right;text-align:center;"><a style="display: block;" href="javascript:;" data-toggle="dropdown" class=" dropdown dropdown-toggle"><i class="fa fa-ellipsis-v" aria-hidden="true"></i></a>';
                                ContactsList += '<ul class="dropdown-menu dropdown-menu-right">';
                                ContactsList += '<li class="addNewContact" listId="' + result.ContactList[j].Id + '" unique="modal" ><a href="#" data-toggle="modal" listId="' + result.ContactList[j].Id + '" data-target="#addWebListContacts">Add Contacts</a></li>';
                                ContactsList += '<li class="editListName" listName="' + result.ContactList[j].ListName + '" id="' + result.ContactList[j].Id + '" editListId="' + result.ContactList[j].Id + '"><a href="#">Edit</a></li>';
                                ContactsList += '<li class="deleteListName" id="' + result.ContactList[j].Id + '"><a href="#">Delete</a></li>';
                                ContactsList += ' </ul>';
                                ContactsList += ' </div></div>';
                                listModal += '<div class="list" id="' + result.ContactList[j].Id + '"><div style="width:93%;float:left;"><a id="' + result.ContactList[j].Id + '" href="javascript:;" class="contactList"><span id="' + result.ContactList[j].Id + '">' + result.ContactList[j].ListName + '</span>(<span id="countcount' + result.ContactList[j].Id + '">' + result.ContactList[j].listCount + '</span>)</a></div></div>';
                            }
                        }

                        $('#ddlWebList').html(weblistNames);
                    }
                        //$('#allContacts').html('<div id="list-groupmodal" style="overflow: hidden; width: 200px; height: 500px;">'+listModal+'</div>');

                    else {
                        $('#ddlWebList').html('<option value="0">No Lists Found</option>');
                        $('#webLists').html('No Lists Found');

                    }

                    $('#list-group').html(ContactsList);
                    firstClick++;
                }
                webContactsAppend = webContactsData;

                $("ul#myTab li").eq(0).addClass("active");
                setTimeout($.unblockUI, 200);
            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                }
                else if (jqXHR.status == 401) {
                    window.location.href = "/Home.aspx?message=Session expired";
                }
                else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                } else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                $('#webLists').html(msg);
                $.unblockUI();
            }
        });
    }
}

function load_crop_thumb() {
    //$('#avatar_first').load(function () {
    //    alert("file upload");
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
        setSelect: [10, 10, 100, 100],

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


function manageWebContacts(mode, contactID, name, mobileNumber, prefix, listIdNo, isAddContact) {
    var image_name = "";
    if (G_img_name.length != 0) {
        image_name = G_img_name.replace("/ContactImages/", "");
    }
    else {
        image_name = "";
    }
    $.ajax({
        url: "/HandlersWeb/Contacts.ashx",
        data: {
            type: 4,
            mode: mode,
            contactID: contactID,
            name: name,
            mobileNumber: mobileNumber,
            prefix: '',
            cropX: parseInt(roundCropX),
            cropY: parseInt(roundCropY),
            cropW: parseInt(roundCropW),
            cropH: parseInt(roundCropH),
            imgName: image_name,
            listID: listIdNo,
            listName: '',
        },
        dataType: "json",
        async: false,
        method: "POST",
        success: function (result) {
            if (result.Status == 1) {

                if (result.Message != 'Contact Already Exist in List') {
                    if ((listIdNo != 0 && contactID == 0) || isAddContact == 1) {

                        listClickContactsDisplay(listIdNo);
                        $('#createGrpList,.list').removeClass("highlight");
                        $('a[id=' + listIdNo + ']').parents('.list').addClass("highlight");
                        var cnt = $("span[id='count" + listIdNo + "']").text();
                        //$("span[id='count" + listIdNo + "']").text(++cnt);
                        cnt = $("span[id=allContactsCnt]").text();;
                        $("span[id=allContactsCnt]").text(++cnt);
                        alert('Contact added to list Successfully');
                    }

                    else if (contactID != 0) {

                        Notifier('Contact Updated Successfully', 1);
                        $('#webLists .contacts').each(function (e) {
                            if ($(this).attr("idno") == contactID) {
                                $(this).find("p:eq(1)").html(' <i class="fa fa-mobile" aria-hidden="true"> </i> ' + mobileNumber);
                                $(this).find("p:eq(0)").text(name);
                                if (G_img_name.length != 0)
                                { $(this).find("img").attr("src", "/ContactImages/" + G_img_name); }



                            }

                        });
                    }

                    clearModalValues();

                }
                else {

                    Notifier(result.Message, 2);
                    clearModalValues();
                }
            }
            else {

                alert(result.Message);
                clearModalValues();
            }
            G_img_name = "";

        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                window.location.href = "/Home.aspx?message=Session expired";
            }
            else {

                Notifier("Something went wrong", 2);
                console.log(errorThrown);
            }
        }
    });
}
function listClickContactsDisplay(listId) {
    //$('.list').removeClass("highlight");
    $('.list#' + listId).addClass("highlight");
    global_addContactList = 0;
    var listName = $(this).text();
    $('#search-input').val('');
    $('span[id=searchCount]').html('<br>');
    var searchCount = 0; webContactsData = "";
    $('#webLists').html('');
    $('#listName').text(listName);
    webListsAppend = "";
    webPageInde = 0;
    webContactsListAll = "";
    webPageInde++;
    if (webPageInde == 1 || webPageInde <= webPageCount) {
        $.ajax({
            url: '/HandlersWeb/Contacts.ashx',
            type: 'post',
            async: false,
            dataType: 'json',
            data: {
                type: 2, listId: listId,
                pageIndex: webPageInde,
            },
            success: function (result) {
                jsonWebContacts = result;
                webContactsData = "";
                ContactsList = "";
                webContactsList = "";
                webPageCount = 1;

                if (result.Data.length > 0) {
                    for (var i = 0; i < result.Data.length; i++) {



                        if (result.Data[i].ImagePath == null) {
                            imgPath = 'images/avatar-img-5.jpg';
                        }
                        else if (result.Data[i].ImagePath != "") {
                            imgPath = result.Data[i].ImagePath
                        }
                        else {
                            imgPath = 'images/avatar-img-5.jpg';
                        }
                        webContactsList += '<div class="contacts margin-bottom-5 margin-right-5" idNo="' + result.Data[i].Id + '"> '; 
                        webContactsList += '<div id="profilePic">';
                        webContactsList += '<img alt="user" src="' + imgPath + '"></div>';
                        webContactsList += '<div id="profileDetails">';
                        if (result.Data[i].Name.length > 25) {
                            webContactsList += '<p title="' + result.Data[i].Name + '">' + result.Data[i].Name.substring(0, 25) + '..</p>';
                        }
                        else {
                            webContactsList += '<p>' + result.Data[i].Name + '</p>';
                        }

                        webContactsList += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Data[i].MobileNumber + '</p>';
                        webContactsList += '</div>';
                        if (result.Data[i].Source == 2) {
                            webContactsList += '<div class="contact_actions"><a href="javascript:void(0);"  data-toggle="modal" data-target="#addWebListContacts"><i class="fa fa-edit" id="' + result.Data[i].Id + '" userName="' + result.Data[i].Name + '" mobile = "' + result.Data[i].MobileNumber + '" imgPath="' + imgPath + '"></i></a><a href="javascript:void(0);" class="delete" id="' + result.Data[i].Id + '"><i class="fa fa-trash-o"></i></a></div>'
                        }
                        else { webContactsList += '<div class="contact_actions"><a href="javascript:void(0);"  data-toggle="modal" data-target="#addWebListContacts"><a href="javascript:void(0);" class="delete" id="' + result.Data[i].Id + '"><i class="fa fa-trash-o"></i></a></div>'; }
                        webContactsList += '</div>';

                    }
                    $('.alphabet').show();
                    webListsAppend = "";
                    webListsAppend = webContactsList;
                    $("span[id='count" + listId + "']").text(result.AllContactsCount)

                }
                else {
                    webContactsList = "No Contacts are found in this list";
                    webListsAppend = "";
                    webListsAppend = webContactsList;
                    $('.alphabet').hide();
                }



            },
            error: function (jqXHR, exception) {
                var msg = '';
                if (jqXHR.status === 0) {
                    msg = 'Not connect.\n Verify Network.';
                } else if (jqXHR.status == 404) {
                    msg = 'Requested page not found. [404]';
                }

                else if (jqXHR.status == 401)
                { window.location.href = "/Home.aspx?message=Session expired"; }
                else if (jqXHR.status == 500) {
                    msg = 'Internal Server Error [500].';
                } else if (exception === 'parsererror') {
                    msg = 'Requested JSON parse failed.';
                } else if (exception === 'timeout') {
                    msg = 'Time out error.';
                } else if (exception === 'abort') {
                    msg = 'Ajax request aborted.';
                } else {
                    msg = 'Uncaught Error.\n' + jqXHR.responseText;
                }
                $('#webContacts').html(msg);
            }
        });
        if ($('#alphabetWebContacts').parent('.slimScrollDiv').length != 0) {
            $('#alphabetWebContacts').parent().hide();
        }
        $('#alphabetWebContacts').hide();
        $('#webLists').parent().show();
        $('#webLists').html(webListsAppend).show();
        webContactsData = webListsAppend;
        if ($('.list.highlight').length == 0)
            $('#webLists').html('No Lists Found').show();

    }

}

$(document).on("click", ".membersList", function () {
    if (!$(this).hasClass("selected")) {
        $(this).find('.fa-check').css("display", "block");
        $(this).addClass("selected");
    }
    else {
        $(this).removeClass("selected");
        $(this).find('.fa-check').css("display", "none");
    }
});

function clearModalValues() {
    $('#name, #mobileNumber').val('');
    $('#webContactProfile').attr('src', '');
    $('#closeList').click();
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
    $('#errorDescForName,#errorDescForMobile').html('');
}
$(document).on("click", "#saveListContact", function () {

    var contactId = '';
    $('.selected').each(function () {
        var id = $(this).attr("id");
        contactId += id + ',';

    });
    if ($.trim(contactId).length == 0) {
        Notifier('Please select contacts', 2);
        return false;
    }
    G_img_name = "";
    manageWebContactsList(4, contactId, listId);
});

function manageWebContactsList(mode, contactID, listIdNo) {
    var image_name = G_img_name.replace("/ContactImages/", "")

    $.ajax({
        url: "/HandlersWeb/Contacts.ashx",
        data: {
            type: 8,
            mode: mode,
            contactID: contactID,
            listIdParam: listIdNo,
        },
        dataType: "json",
        async: false,
        method: "POST",
        success: function (result) {
            if (result.Status == 1) {
                $('#closeList').click();
                listClickContactsDisplay(listIdNo);
                $('#createGrpListcreateGrpList,.list').removeClass("highlight");
                $('a[id=' + listIdNo + ']').parents(".list").addClass("highlight");
                $("span[id='count" + listIdNo + "']").text(result.ListContactsCount);
                clearPopUpValues();
                Notifier('Contact added to list successfully', 1);
            }
            else if (result.Status == 2) {
                alert(result.Message);
                clearPopUpValues();
                Notifier(result.Message, 1);
            }
            else if (result.Status == 0) {
                $('#closeList').click();
                clearPopUpValues();
                Notifier(result.Message, 2);

            }

        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                window.location.href = "/Home.aspx?message=Session expired";
            }
            else {

                Notifier("Something went wrong", 2);
                console.log(errorThrown);
            }
        }
    });
}

function clearPopUpValues() {
    $('#saveContact').show();
    $('#name, #mobileNumber').val('');
    $('#webContactProfile').attr('src', '');

    $('.selected').each(function () {
        $(this).removeClass("selected");

    });
}

function webContactsOffsetScroll(listId, webPageIndex, webPageCounts, webData, modal) {

    if (webPageIndex == 1 || webPageIndex <= webPageCounts) {
        webContactsData = webData;
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
                modalPageCount = result.pageCount;
                for (var i = 0; i < result.Items.length; i++) {



                    if (result.Items[i].Imagepath == null) {
                        imgPath = 'images/avatar-img-5.jpg';
                    }
                    else if ($.trim(result.Items[i].Imagepath) == "") {
                        imgPath = 'images/avatar-img-5.jpg';
                    }
                    else {
                        // path = result.Items[i].Imagepath.split('\\ContactImages\\');
                        imgPath = result.Items[i].Imagepath

                    }
                    var mod = modal;
                    if (mod == "modal")
                    {
                    
                          webContactsData += '<div class="contacts margin-bottom-5 margin-right-5 membersList" id="' + result.Items[i].Id + '" idNo="' + result.Items[i].Id + '"> '; 
                     
                    }
                    else
                    {
                      
                       webContactsData += '<div class="contacts margin-bottom-5 margin-right-5" id="' + result.Items[i].Id + '" idNo="' + result.Items[i].Id + '">'; 
                    }
                    webContactsData += '<div id="profilePic">';
                    webContactsData += '<img alt="user" src="' + imgPath + '"></div>';
                    webContactsData += '<div id="profileDetails">';
                    if (result.Items[i].Name.length > 25) {
                        webContactsData += '<p title="' + result.Items[i].Name + '">' + result.Items[i].Name.substring(0, 25) + '..</p>';
                    }
                    else {
                        webContactsData += '<p>' + result.Items[i].Name + '</p>';
                    }

                    webContactsData += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Items[i].MobileNumber + '</p>';
                    webContactsData += '</div>';
                    if (result.Items[i].Source == 2) {
                        webContactsData += '<div class="contact_actions"><a href="javascript:void(0);"  data-toggle="modal" data-target="#addWebListContacts"><i class="fa fa-edit" id="' + result.Items[i].Id + '" userName="' + result.Items[i].Name + '" mobile = "' + result.Items[i].MobileNumber + '" imgPath="' + imgPath + '"></i></a><a href="javascript:void(0);" class="delete" id="' + result.Items[i].Id + '"><i class="fa fa-trash-o"></i></a></div>';
                    }
                    else { webContactsData += '<div class="contact_actions"><a href="javascript:void(0);"  data-toggle="modal" data-target="#addWebListContacts"><a href="javascript:void(0);" class="delete" id="' + result.Data[i].Id + '"><i class="fa fa-trash-o"></i></a></div>'; }
                    webContactsData += '</div>';
                }

                webContactsDataModal = webContactsData;


            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == 401) {
                    window.location.href = "/Home.aspx?message=Session expired";
                }
                else {

                    Notifier("Something went wrong", 2);
                    console.log(errorThrown);
                }
            }
        });
    }
}


// $(document).on('click', '#saveExcelContacts', function () {
// uploadContactsThroughExcel(fileName)
// });


function uploadContactsThroughExcel(fileName) {

    $.ajax({
        url: '/ExcelUpload.ashx',
        type: 'post',
        data: { type: 2, fileName: fileName },
        dataType: 'json',
        success: function (result) {
            $('#fileUploadSuccess').hide();
            $('#fileUploadSuccess').html("");
            $('#successMessage,#errorMessage').html("");
            if (result.Status == 1) {
                setTimeout(function () {
                    $('#successMessage').html(result.Message).show();


                    window.location.reload();
                }, 4000);
            }
            else {
                setTimeout(function () {
                    $('#errorMessage').html(result.Message).show();
                    window.location.reload();
                }, 4000);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                window.location.href = "/Home.aspx?message=Session expired";
            }
            else {

                Notifier("Something went wrong", 2);
                console.log(errorThrown);
            }
        }
    });

}

$("ul#listContactsModal li").click(function () {
    $('#name, #mobileNumber').val('');
    $('.fa-check').css("display", "none");
    $('#sheettxt').text('');
    $('#sheeterr').hide();
    $('.contacts').removeClass('selected');
    var activeTab = $(this).find("a").attr("href");
    if (activeTab == "#addListContactsModal") {
        $('#excelFormBody').hide();
        $('#addListContactsModal').show();
        $('#addCntctList').show();
        $('#saveExcelContacts').hide();
        $('#saveListContact').hide();
        $('#allContacts,#mobileContacts').hide();
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('#errorDescForName,#errorDescForMobile').html('');
        $('#webContactProfile').attr('src', '');
        $('.jcrop-circle-demo').css('display', 'none');
    }
    else if (activeTab == "#allContacts") {
        $('#addCntctList,#addListContactsModal').hide();
        $('#saveExcelContacts').hide();
        $('#excelFormBody,#mobileContacts').hide();
        $('#allContacts').show();
        $('#saveListContact').show();
        $('#webContactProfile').attr('src', '');
        $('.jcrop-circle-demo').css('display', 'none');

        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('#errorDescForName,#errorDescForMobile').html('');
    }
    else if (activeTab == "#mobileContacts") {
        $('#addCntctList').hide();
        $('#saveExcelContacts,#allContacts,#addListContactsModal').hide();
        $('#excelFormBody').hide();
        $('#saveListContact,#mobileContacts').show();
        $('#webContactProfile').attr('src', '');
        $('.jcrop-circle-demo').css('display', 'none');
        isModalMobile = 1;
        $('#mobileContacts .contacts').removeClass("selected");
        $('.contacts').find()
        $('#mobileContacts').css("top", "10px");
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('#errorDescForName,#errorDescForMobile').html('');
    }
    else if (activeTab == "#excelFormBody") {

        $('#allContacts,#addListContactsModal').hide();
        $('#xlinfo_div').hide();
        $('#excelFormBody').show();
        $('#addCntctList,#mobileContacts').hide();
        $('#saveExcelContacts').show();
        $('#saveListContact').hide();
        $('#successMessage,#errorMessage').html("");
        $('#saveContact').hide();
        $('#saveExcelContacts').show();
        $('#sheeterr,#headerselect').hide();
        $('#upmsg').html('');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
        $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
        $('#errorDescForName,#errorDescForMobile').html('');
        $('#webContactProfile').attr('src', '');
        $('.jcrop-circle-demo').css('display', 'none');
    }
});

$(document).on('click', '.groupList', function () {
    $('.groupList').removeClass('active');
    $(this).addClass('active');
    getAllWebContactsForModal(this.id);
});
function getAllWebContactsForModal(listId) {

    $.ajax({
        url: '/HandlersWeb/Contacts.ashx',
        type: 'post',
        async: false,
        dataType: 'json',
        data: {
            type: 2, listId: listId,
            pageIndex: 1,
        },
        success: function (result) {

            if (result.Data.length > 0 && $('.groupList').length != 0) {
                webContactsDataModal = '';
                for (var i = 0; i < result.Data.length; i++) {



                    if (result.Data[i].ImagePath == null) {
                        imgPath = 'images/avatar-img-5.jpg';
                    }
                    else if ($.trim(result.Data[i].ImagePath) == "") {
                        imgPath = 'images/avatar-img-5.jpg';
                    }
                    else {
                        // path = result.Items[i].Imagepath.split('\\ContactImages\\');
                        imgPath = result.Data[i].ImagePath

                    }
                    webContactsDataModal += '<div class="contacts margin-bottom-5 margin-right-5 membersList" id="' + result.Data[i].Id + '" idNo="' + result.Data[i].Id + '">'; 
                    webContactsDataModal += '<div id="profilePic">';
                    webContactsDataModal += '<img alt="user" src="' + imgPath + '"></div>';
                    webContactsDataModal += '<div id="profileDetails">';
                    if (result.Data[i].Name.length > 25)
                    { webContactsDataModal += '<p title="' + result.Data[i].Name + '">' + result.Data[i].Name.substring(0, 25) + '..</p>'; }
                    else { webContactsDataModal += '<p>' + result.Data[i].Name + '</p>'; }

                    webContactsDataModal += '<p><i class="fa fa-mobile" aria-hidden="true"></i> ' + result.Data[i].MobileNumber + '</p>';
                    webContactsDataModal += '</div>';
                    webContactsDataModal += '<div class="contact_actions"></div>';
                    webContactsDataModal += '<i class="fa fa-check select_check" style="display:none"></i></div>';
                }

                $('.weblistCont').html(webContactsDataModal);

            }
            else
                $('.weblistCont').html("No contacts");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status == 401) {
                window.location.href = "/Home.aspx?message=Session expired";
            }
            else {

                Notifier("Something went wrong", 2);
                console.log(errorThrown);
            }
        }
    });

}

// $('#allContacts').scroll(function () {
// if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
// pageIndexModal++;
// getAllWebContactsForModal(0);

// }
// });

//$(document).ajaxComplete(function () {
//    if ($('#alphabetWebContacts').parent('.slimScrollDiv').length != 0 && $('#phoneContacts').parent('.slimScrollDiv').length != 0 && $('#alphabetMobileContacts').parent('.slimScrollDiv').length != 0)
//    { $('#alphabetWebContacts,#phoneContacts,#alphabetMobileContacts').parent().hide(); }

//});
function newListAddContact(mode, contactID, newList, name, mobileNumber) {
    var imageName = G_img_name.replace("/ContactImages/", "")

    if ($('#ddlWebList option').filter(function () { return $(this).html().toLowerCase() == newList.toLowerCase(); }).text().toLowerCase() == "") {
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
                    $('#successMessage').html(result.Message);
                    Notifier(result.Message, 1);
                    $('#contactsModal').modal("hide");
                    $('#addWebListContacts').modal("hide");
                    $(".list").removeClass('highlight');
                    ContactsList = '';
                    ContactsList += '<div class="list highlight" id="' + result.List[0].ListId + '">';
                    ContactsList += '<div style="width:93%;float:left;"><a  id="' + result.List[0].ListId + '"  class="contactList"><span  id="' + result.List[0].ListId + '" lcount="' + result.List[0].ListCount + '" lname="' + newList + '">' + newList + '</span>(<span id="count' + result.List[0].ListId + '">' + result.List[0].ListCount + '</span>)</a></div>';
                    ContactsList += '<div style="width:7%;float:right;text-align:center;"><a style="display: block;" href="javascript:;" data-toggle="dropdown" class=" dropdown dropdown-toggle"><i class="fa fa-ellipsis-v" aria-hidden="true"></i></a>';
                    ContactsList += '<ul class="dropdown-menu dropdown-menu-right">';
                    ContactsList += '<li class="addNewContact" listId="' + result.List[0].ListId + '" unique="modal" ><a href="#" data-toggle="modal" listId="' + result.List[0].ListId + '" data-target="#addWebListContacts">Add Contacts</a></li>';
                    ContactsList += '<li class="editListName" listName="' + newList + '" id="' + result.List[0].ListId + '" editListId="' + result.List[0].ListId + '"><a href="#">Edit</a></li>';
                    ContactsList += '<li class="deleteListName" id="' + result.List[0].ListId + '"><a href="#">Delete</a></li>';
                    ContactsList += ' </ul>';
                    ContactsList += ' </div></div>';
                    //$('#list-group').append('<div class="list highlight"><div style="width: 100%; float: left;"><a data-target="#" href="javascript:void(0);" style="display: block;" id="'+result.List[0].ListId+'" class="contactList">'+newList+'('+result.List[0].ListCount+')</a> </div></div>')
                    $('#list-group').append(ContactsList);
                    listClickContactsDisplay(result.List[0].ListId);

                }
                else {
                    $('#errorMessage').html(result.Message);
                    setTimeout(function () {
                        window.location.reload();
                    }, 1000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.status == 401) {
                    window.location.href = "/Home.aspx?message=Session expired";
                }
                else {

                    Notifier("Something went wrong", 2);
                    console.log(errorThrown);
                }
            }
        });
    }
    else
        Notifier('This Web list name is already existed', 2);
}

function hidePopUpItems() {

    $('ul#listContactsModal li').removeClass("active");
    $('.modal-body .tab-content div').removeClass("active in");
    $('div#addListContactsModal').addClass("active in")
    $('.modal-footer button').hide();
    $('#updateContact,#allContacts,#excelFormBody').hide();
    $('#addCntctList, #closeList,#addListContactsModal').show();
    $('div#allContacts').css("top", "0px !important");

    $('ul#listContactsModal li.addContacts').addClass("active");
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-success');
    $('#name, #mobileNumber').closest('.form-group').removeClass('has-error');
    $('#errorDescForName,#errorDescForMobile,#errorDescForWebList').html(''); $('#name, #mobileNumber').val('');
    $('#name').focus();
    $('#sheettxt').text('');
    $('#sheeterr').hide();
    webContactsDataModal = "";
    $('.fa-check').css("display", "none");
    $('.contacts').removeClass('selected');
    $('#webContactProfile').attr('src', '');
    $('.jcrop-circle-demo').css('display', 'none');
    $('#mobileContacts').hide();
}

function ListNames() {

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
            jsonWebContacts = result;
            webContactsData = "";
            ContactsList = "";
            webContactsList = "";
            webPageCount = result.pageCount;
            var listModal = "";

            if (result.ContactList.length > 0) {
                var isFirstTime = 0;
                var ddlString = '<option value="0">Select List</option>';
                for (var j = 0; j < result.ContactList.length; j++) {
                    // alert(listId+"!="+result.ContactList[j].Id)
                    ddlString += '<option value="' + result.ContactList[j].Id + '">' + result.ContactList[j].ListName + '</option>';
                    if (result.ContactList[j].Source == 2 && listId != result.ContactList[j].Id) {

                        var activeClass = '';
                        //listModal +=  '<div class="list" id="' + result.ContactList[j].Id + '"><div style="width:93%;float:left;"><a id="' + result.ContactList[j].Id + '" href="javascript:;" class="contactList"><span id="' + result.ContactList[j].Id + '">'+ result.ContactList[j].ListName +'</span>(<span id="countcount' + result.ContactList[j].Id + '">' + result.ContactList[j].listCount + '</span>)</a></div></div>';
                        if (isFirstTime == 0) {
                            defaultWebListId = result.ContactList[j].Id;
                            activeClass = "active";
                            isFirstTime = 1;
                        }
                        listModal += '<ul id="list-groupmodal" ><li style="cursor:pointer" class="' + activeClass + ' groupList" id="' + result.ContactList[j].Id + '"><span>' + result.ContactList[j].ListName + '</span>(' + result.ContactList[j].listCount + ')</li></ul>';
                    }
                }
                $("#ddlWebList").html(ddlString);
            }

            $('#allContacts').html('<div class="row"><div class="col-sm-4 allLists"><div style="overflow-y:auto;" class="left-cont">' + listModal + '</div></div><div class="col-sm-8 weblistCont">contacts</div></div>');
            getAllWebContactsForModal(defaultWebListId);
            setTimeout($.unblockUI, 200);

            $('.left-cont').slimScroll({
                allowPageScroll: true,
                height: 450
            });
        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            }
            else if (jqXHR.status == 401) {
                window.location.href = "/Home.aspx?message=Session expired";
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            $('#webLists').html(msg);
            $.unblockUI();
        }
    });

}