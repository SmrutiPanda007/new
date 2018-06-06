// <reference path="../assets/global/plugins/jquery-1.11.0.min.js" />
/// <reference path="../assets/plugins/jquery-1.10.1.min.js" />

//var regEmail = /[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}/igm;
var regEmail = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
var regMobile = /^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}[9|8|7][0-9](\s){0,1}(\-){0,1}(\s){0,1}[0-9]{1}[0-9]{7}$/;
var global_mode = 0;
var global_rcAmount = 0;
var global_crntBalance = 0;
var global_appBalance = 0;
var global_webBalance = 0;
var G_img_name = "";
var roundCropX = 0; roundCropY = 0; roundCropW = 0; roundCropH = 0;
var jcrop_api = "", G_Event_Pic = "", imagePath = "";
var currencyName = CurrencyName();
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


    global_mode = 1;

    getProfileDetails();

    rechargeAmountPie();
    $("#btnCallSubscription").click(function () {
        $.ajax({
            url: "/HandlersWeb/Profile.ashx",
            data: {
                modeSp: 4,
                nickname: '',
                email_id: '', type: 4,
            },
            dataType: "json",
            async: false,
            method: "POST",
            success: function (modifiedResult) {
                if (modifiedResult.Success == true) {
                    $("#btnCallSubscription").hide();
                }
                else if (modifiedResult.Success == false) {
                    alert("Something Went Wrong");

                }

            },
            error: function (img) { alert("error"); }
        });
        //$("#btnCallSubscription").hide();   
        Notifier('your Account is Subscribed to Voice Record Successfully', 1);
    });

    $("#userPic").blurIt($("#prof_img"), 10);
    $('#userPic').css({
        "background-image": "url('/" + imagePath + "')",
        "background-size": "389%",
        "background-repeat": " no-repeat",
        "opacity": "0.8",
        "z-index": "1000"
    });

    $('.tab-content').slimScroll({
        allowPageScroll: false,
        height: '180'
    });
    //$('.AllTimeZones').change(function (e) {

    //    $.blockUI({ message: "<h4>Changing Time Zone....</h4>" });
    //    $.ajax({
    //        url: "/HandlersWeb/Profile.ashx",
    //        data: {
    //            type: 5,
    //            modeSp: 5,
    //            offSetValue: $('.AllTimeZones').val()

    //        },
    //        async: false,
    //        method: "POST",
    //        dataType: "JSON",
    //        success: function (result) {

    //            $.unblockUI();
    //            rechargeAmountPie();
    //            Notifier('Time Zone has changed and selected time zone will reflect in all time formats', 1)
    //        },
    //        error: function (result) {
    //            Notifier('Something Went wrong', 2);

    //        }
    //    });
    //});
    $('#saveData').on('click', function (e) {
        var nickName = $('#userName').val();
        var emailId = $('#emailID').val();
        var validateDiv = "";
        var bool = true;
        var IsValid = true;
        if (nickName.toString() == "" || nickName == null) {
            IsValid = false;
            $('#userName').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('Please Enter Name');
        }
        else if (nickName.length > 20) {
            IsValid = false;
            $('#userName').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('Please Enter Name');
        }
        else if (nickName.length < 3) {
            IsValid = false;
            $('#userName').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('User Name must be minimum of 3 characters');
        }
        else {
            $('#userName').closest('.form-group').removeClass('has-error').addClass('has-success')
            $('#errorDescForName').html('');
        }

        if (emailId.toString() == "" || emailId == null) {
            IsValid = false;
            $('#emailID').closest(".form-group").addClass('has-error').removeClass('has-success')
            $("#errorDescForEmail").html("Please Enter Email Id");
        }
        else if (!emailId.match(regEmail)) {
            IsValid = false;
            $('#emailID').closest(".form-group").addClass('has-error').removeClass('has-success');
            $("#errorDescForEmail").html("Please Enter Valid Email Id");
        }
        else {
            $('#emailID').closest(".form-group").removeClass('has-error').addClass('has-success')
            $("#errorDescForEmail").html("");
        }
        if (!IsValid) {
            return false;
        }

        if ($.trim(G_img_name).length != 0) {
            profileImageCrop();
        }
        else {

            updateProfileDetails(nickName, emailId);
        }
    });

    $('body').on('keyup', '#emailID', function (e) {

        var emailId = $('#emailID').val();
        if (emailId.toString() == "" || emailId == null) {
            $('#emailID').closest(".form-group").addClass('has-error').removeClass('has-success')
            $("#errorDescForEmail").html("Please Enter Email Id");
        }
        else if (!emailId.match(regEmail)) {
            $('#emailID').closest(".form-group").addClass('has-error').removeClass('has-success');
            $("#errorDescForEmail").html("Please Enter Valid Email Id");
        }
        else {
            $('#emailID').closest(".form-group").removeClass('has-error').addClass('has-success')
            $("#errorDescForEmail").html("");
        }
    });

    $('body').on('keyup', '#userName', function (e) {
        var nickName = $('#userName').val();
        if (nickName.length >= 3) {
            $("#errorDescForName").hide();
            $('#userName').closest('.form-group').addClass('has-success').removeClass('has-error');
        }
        else {
            $('#errorDescForName').html('User Name must be minimum of 3 characters').show();
            $('#userName').closest('.form-group').addClass('has-error').removeClass('has-success')

        }

    });

    $('#profileUpload').fileupload({

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
                $('#saveData').attr('disabled', 'true');
                $('#editprofile .modal-content').block({ message: '<h4> Image Uploading... </h4>' });
                data.submit();
            }
        },
        done: function (e, data) {

            $('input[type="file"]').css('color', 'transparent');
            $('.jcrop-circle-demo').css('display', 'block');
            G_img_name = data.result;

            $("#avatar_first").attr("src", "/Temp_crop_Images/" + G_img_name);

            load_crop_thumb();
            setTimeout(function () { $('#editprofile .modal-content').unblock() }, 8000);


        },
        error: function (e, data) {
            Notifier(data, 2);
            setTimeout(function () { $('#editprofile .modal-content').unblock() }, 8000);
            $('#saveData').removeAttr('disabled');

        }


    });

    $(document).delegate('#changeProfileDetails', 'click', function (e) {
        e.preventDefault();
        $('#errorDescForName, #errorDescForEmail').html('');
        $('#userName, #emailID').closest('.form-group').removeClass('has-success');
        $('#userName, #emailID').closest('.form-group').removeClass('has-error');
        $('#userName').val($(this).attr("nickname"));
        $('#emailID').val($(this).attr("email"));
        $('#avatar_first').attr('src', $(this).attr("pic"));
        $('#editprofile').modal('show');
        console.log($('#userName').val() + "cmnvsdnfjs" + $('#emailID').val());
        $('.jcrop-circle-demo').css('display', 'none');
        $('#imageCrop').html('<img id="avatar_first" width="190" height="200" alt="Smiley face" src="' + $(this).attr("pic") + '" style="width: 184.615px; height: 200px;">');
    });

    $('#close').click(function (e) {
        $('.close').click();
        e.stopImmediatePropagation();
        roundCropX = 0; roundCropY = 0; roundCropW = 0; roundCropH = 0;
        G_img_name = "";
        $('#errorDescForName, #errorDescForEmail').html('');
        $('#userName, #emailID').closest('.form-group').removeClass('has-success');
        $('#userName, #emailID').closest('.form-group').removeClass('has-error');

        $('#editprofile').modal("hide");


    });

});

//------------------------------------------Fetching User Profile details
function getProfileDetails() {
    $.blockUI({ message: "<h4>Loading....</h4>" });
    $.ajax({
        url: "/HandlersWeb/Profile.ashx",
        data: {
            type: 1,
            modeSp: 1,

        },
        async: false,
        method: "POST",
        dataType: "JSON",
        success: function (displayResult) {
            var len = displayResult.Profile.length;
            var impPath = new Array();
            $('#callerID').html(displayResult.Profile[0].OriginationCallerId);
            var imgFullPath = "";
            if ($.trim(displayResult.Profile[0].DisplayPic).length == 0) {
                imgFullPath = "/images/avatar-img-1.png";
            }
            else {
                imgFullPath = displayResult.Profile[0].DisplayPic;
            }
            imagePath = imgFullPath;
            var profileDetailsAppend = "";
            profileDetailsAppend += '<div id="userPic">';
            profileDetailsAppend += ' <img id="prof_img" src= "' + imgFullPath + '" class="img-responsive" width="150" /> ';

            profileDetailsAppend += '</div>';
            profileDetailsAppend += '<div id="userDetails">';
            profileDetailsAppend += ' <p id="nickname"><i class="fa fa-user"></i> ' + displayResult.Profile[0].NickName + '</p>';
            profileDetailsAppend += '<p id="mobile"><i class="fa fa-phone" style="cursor:default!important"></i>  ' + displayResult.Profile[0].Mobile + '</p>';
            var emailTrim = displayResult.Profile[0].Email;
            if (displayResult.Profile[0].Email.length > 20)
                emailTrim = emailTrim.substring(0, 20) + '...';
            profileDetailsAppend += '<p id="email" title="' + displayResult.Profile[0].Email + '"><i class="fa fa-envelope"></i> ' + emailTrim + '</p>';
            if (displayResult.Profile[0].AccountType == 2) {
                profileDetailsAppend += '<p style="color:#ef9843;" class="cursive-font"> Premium Account </p>';
                if (displayResult.Profile[0].IsCallSubscription == 0)
                    profileDetailsAppend += '<button class="btn btn-default" id="btnCallSubscription">Subscribe to Voice Record</button>';
            }
            profileDetailsAppend += '</div>';
            profileDetailsAppend += '<div id="changeProfile">';
            profileDetailsAppend += '<button id="changeProfileDetails" class="btn btn-primary" data-toggle="modal" data-target="#editprofile" email="' + displayResult.Profile[0].Email + '" nickname="' + displayResult.Profile[0].NickName + '"  pic="' + imgFullPath + '" >Change Profile</button>'
            // profileDetailsAppend += '  <button type="button" class="btn btn-primary"   pic="' + imgFullPath +'" nickname="' + displayResult.Profile[0].NickName + '" email="' + displayResult.Profile[0].Email + '" id="changeProfileDetails">Change your Profile</button>';
            profileDetailsAppend += '</div>';
            //for (var offset = 0; offset < displayResult.UtcOffSets.length; offset++) {
            //    $('.AllTimeZones').append("<option value=" + displayResult.UtcOffSets[offset].Slno + ">" + displayResult.UtcOffSets[offset].OffSetName + "</option>")
            //}
            $('.AllTimeZones').html(displayResult.UtcOffSets[0].OffSetName );
            impPath = displayResult.Profile[0].DisplayPic;

            $('#userProfile').html(profileDetailsAppend);

            //$('#userPic').css("background-image", "url('" + imgFullPath + "') no-repeat");
            $.unblockUI();


        },
        error: function (displayResult) {
            alert('Something Went wrong');
            $.unblockUI();
        }

    });
    $.unblockUI();

}

//------------------------------------------REcharge Amount request
function rechargeAmountPie() {

    $.ajax(
        {
            url: "/HandlersWeb/Profile.ashx",
            data:
                {
                    type: 3,

                },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (rcResult) {
                global_rcAmount = rcResult.RechargeDetails[0].RcAmount;
                global_crntBalance = rcResult.RechargeDetails[0].CurrentBal;
                global_appBalance = rcResult.RechargeDetails[0].AppAmount;
                global_webBalance = rcResult.RechargeDetails[0].WebUsedAmount;
                var webBal = (global_webBalance / global_rcAmount) * 100;
                var appBal = (global_appBalance / global_rcAmount) * 100;
                var currentBal = (global_crntBalance / global_rcAmount) * 100;
                $('#web_inr').text(currencyName + " " + global_webBalance.toFixed(2));
                $('#app_inr').text(currencyName + " " + global_appBalance.toFixed(2));
                // var balance = foo([webBal, appBal, currentBal], 100);
                // var ar=compareandSort(balance,[webBal, appBal, currentBal]);
                drawPie(webBal, appBal, currentBal, global_webBalance.toFixed(2), global_crntBalance, global_appBalance);
                membersPresented(rcResult.RechargeDetails[0].DefaultLines, rcResult.RechargeDetails[0].MaxLinesUsed);
                notificationsDisplay(rcResult.Notifications);
                $('#tot_channels').text(rcResult.RechargeDetails[0].DefaultLines);
                $('#maxhits').text(rcResult.RechargeDetails[0].MaxLinesUsed);
                $('.canvasjs-chart-credit').hide();
                $('#totalmem').css('cursor', 'default');
            },
            error: function (rcResult) {

            }

        });
}

//------------------------------------------Doughnut chart usign canvasJS 

function drawPie(wBal, apBal, crnBal, webBalInRupees, crntBalInRupees, appBalInRupees) {

    CanvasJS.addColorSet("greenShades",
                [//colorSet Array
                "#37acd7",
                "#63c5ae",
                "#2e5a70"

                ]);
    var chart = new CanvasJS.Chart("chartContainer",
              {
                  theme: "theme2",
                  colorSet: "greenShades",
                  title: {
                      text: currencyName + " " + crntBalInRupees + " Current Balance",
                      verticalAlign: "center",
                      fontSize: "16",
                      wrap: true,
                      dockInsidePlotArea: true,
                      maxWidth: "120",
                      fontColor: "#37acd7"
                  },
                  legend: {
                      verticalAlign: "bottom",
                      horizontalAlign: "left",
                      fontSize: "13"

                  },
                  animationEnabled: false,
                  data: [
                  {
                      type: "doughnut",
                      showInLegend: false,
                      radius: "70%",

                      toolTipContent: "{y} %",
                      innerRadius: "80%", //vary the angle here.
                      dataPoints: [
                      { y: crnBal, label: "", cursor: "pointer" },
                        { y: apBal, label: "", cursor: "pointer" },
                        { y: wBal, label: "", cursor: "pointer" }

                      ]
                  }
                  ]


              });

    chart.render();

}

function membersPresented(total, prsnt) {
    if (total != 0) {
        function getSetIntersections() {
            areas = [{ "sets": ["0", "1"], "size": "" + prsnt + "" }];
            return areas;
        }

        function getSets() {
            var sets = [{ "label": "" + total + "", "size": "" + total + "" }, { "label": "" + prsnt + "", "size": "" + prsnt + "" }], areas = [];
            return sets;
        }
        $('.dynamic').html('');
        var w = Math.min(250, document.documentElement.clientWidth - 30), h = 2 * w / 3;

        // draw the initial set
        var sets = venn.venn(getSets(), getSetIntersections()),
          diagram = venn.drawD3Diagram(d3.select(".dynamic"), sets, w, h);

        // redraw the sets on any change in input
        d3.selectAll("input").on("change", function () {
            var sets = venn.venn(getSets(), getSetIntersections());
            venn.updateD3Diagram(diagram, sets);
        });
    }
    if (parseInt(prsnt) == 0)
    { $('#prntdmem tspan').attr('x', '193'); }
}

function notificationsDisplay(notifications) {
    var notificationsArray = new Array();
    notificationsArray = notifications;
    var allNotifications = "<table class='table' border='0'>";
    var rechargeTabNotofications = "<table class='table' border='0'>";           // Only for recharge notifications 

    for (var i = 0; i < notificationsArray.length; i++) {

        if ($.trim(notificationsArray[i].Notfytype) == "1") {

            allNotifications += "<tr>  <td style='text-align: left;'><i class='fa fa-file-image-o'></i> " + notificationsArray[i].NotificationMsg + "</td><td style='text-align: right; padding-right: 20px;'>" + notificationsArray[i].InsertedTime + "</td><tr>";
        }
        else if ($.trim(notificationsArray[i].Notfytype) == "2") {
            allNotifications += "<tr>  <td style='text-align: left;'><i class='fa fa-user'></i> " + notificationsArray[i].NotificationMsg + "</td><td style='text-align: right; padding-right: 20px;'>" + notificationsArray[i].InsertedTime + "</td><tr>";
        }

        else if ($.trim(notificationsArray[i].Notfytype) == "3") {
            allNotifications += "<tr>  <td style=' text-align: left;'><i class='fa fa-envelope-o'></i> " + notificationsArray[i].NotificationMsg + "</td><td style='text-align: right; padding-right: 20px;'>" + notificationsArray[i].InsertedTime + "</td><tr>";
        }
        else if ($.trim(notificationsArray[i].Notfytype) == "4") {
            $('#recharge').show();
            allNotifications += "<tr><td style='text-align: left;'><i class='fa fa-money'></i> " + notificationsArray[i].NotificationMsg + "</td><td style='text-align: right; padding-right: 20px;'>" + notificationsArray[i].InsertedTime + "</td><tr>";
            rechargeTabNotofications += "<tr><td style='text-align: left;'><i class='fa fa-money'></i> " + notificationsArray[i].NotificationMsg + "</td><td style='text-align: right; padding-right: 20px;'>" + notificationsArray[i].InsertedTime + "</td><tr>";
        }

    }
    allNotifications += "</table>";
    rechargeTabNotofications += "</table>";
    $('#sectionA').html(allNotifications);
    $('#sectionB').html(rechargeTabNotofications);

}

//------------------------------------------Updating Profile Details
function updateProfileDetails(nickName, emailId) {


    $.ajax({
        url: "/HandlersWeb/Profile.ashx",
        data: {
            modeSp: 2,
            nickname: nickName,
            email_id: emailId, type: 4,
        },
        dataType: "json",
        async: false,
        method: "POST",
        success: function (modifiedResult) {
            if (modifiedResult.Success == true) {
                var impPath = "";
                impPath = modifiedResult.Profile[0].Display;
                $('#uname').text(modifiedResult.Profile[0].Nname);
                $('#udesig').text(modifiedResult.Profile[0].Mobile);
                $('#div_email').text(modifiedResult.Profile[0].Mail);
                $('#prof_img').attr('src', impPath),
                 $('#changeProfileDetails').attr("pic", impPath);
                $('#editprofile').modal('hide');
                location.reload();
            }
            else if (modifiedResult.Success == false) {
                alert("Something Went Wrong");
                $('#editprofile').modal('hide');
            }

        },
        error: function (img) { alert("error"); }
    });
}

//------------------------------------------Profile Image cropping
function profileImageCrop() {
    var image_name = G_img_name.replace("/TempImages/", "")
    $.ajax({
        url: "/HandlersWeb/Profile.ashx",
        data: {
            modeSp: 2,
            nickname: $('#userName').val(),
            email_id: $('#emailID').val(), type: 2, cropX: parseInt(roundCropX), cropY: parseInt(roundCropY), cropW: parseInt(roundCropW), cropH: parseInt(roundCropH), imgName: image_name
        },
        dataType: "json",
        async: false,
        method: "POST",
        success: function (modifiedResult) {
            if (modifiedResult.Success == true) {
                var impPath = "";
                impPath = modifiedResult.Profile[0].Display;
                var G_Event_Pic = G_img_name;
                $('#uname').text(modifiedResult.Profile[0].Nname);
                $('#udesig').text(modifiedResult.Profile[0].Mobile);
                $('#div_email').text(modifiedResult.Profile[0].Mail);
                $('#prof_img').attr('src', impPath),
                $('#changeProfileDetails').attr("src", impPath);
                $('#userPic').css({
                    'background-color': 'url("' + impPath + '")',
                    'background-size': 'cover'
                });
                $('#editprofile').modal('hide');
                location.reload();
            }
            else if (modifiedResult.Success == false) {
                alert("Something Went Wrong");
                $('#editprofile').modal('hide');
            }

        },
        error: function (img) { alert("error"); }
    });
}


function load_crop_thumb() {
    //$('#avatar_first').load(function () {
    //    alert("file upload");

    $('#avatar_first').Jcrop({
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
        setSelect: [80, 80, 100, 100],

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
    $('#saveData').removeAttr('disabled');
}


function storeCoords(c) {
    roundCropX = c.x;
    roundCropY = c.y;
    roundCropW = c.w;
    roundCropH = c.h;
}

function foo(l, target) {
    var off = target - _.reduce(l, function (acc, x) { return acc + Math.round(x) }, 0);
    return _.chain(l).
            sortBy(function (x) { return Math.round(x) - x }).
            map(function (x, i) { return Math.round(x) + (off > i) - (i >= (l.length + off)) }).
            value();
}

function compareandSort(arr, realArr) {
    var resultArray = new Array();
    for (var i = 0; i < realArr.length; i++) {
        for (var j = 0; j < arr.length; j++) {
            if (parseInt(realArr[i].toFixed(0) - 1) == arr[j])
                resultArray.push(arr[j])
            else if (parseInt(realArr[i].toFixed(0) + 1) == arr[j])
                resultArray.push(arr[j])
            else if (realArr[i].toFixed(0) == 0)
                resultArray.push(arr[j])
        }
    }
    return resultArray;
}








