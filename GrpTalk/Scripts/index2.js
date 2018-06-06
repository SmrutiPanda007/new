$(document).ready(function () {
    var locationpath = window.location.href;
    var host = window.location.host;
    if (window.location.href.indexOf("web.grptalk.com") > -1) {
        window.location.replace("https://web.grptalk.com/home.aspx");
    }
    // if(host.toLowerCase()=="www.grptalk.com"){
    // window.location.replace("https://grptalk.com");
    // }


    //Key Press Events for phone textbox,otp textbox,otpviasms textbox
    $("#txtPhone").keypress(function (e) {
        $('.ErrorMsg').html('<br/>');
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            if (e.which == 13) {
                e.preventDefault();
                $('#btnSentOtp').click();
            }
            else {
                $('.ErrorMsg').text('Please Enter Numerical Values Only');
                $('.ErrorMsg').show();
                return false;
            }
        }
    });
    $("#txtOtp ,#txtOtpviasms").keypress(function (e) {
        $('.ErrorMsg').html('<br/>');
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            if (e.which == 13) {
                e.preventDefault();
                //$('#btnSentOtp').click();
            }
            else {
                $('.ErrorMsg').text('Please Enter Numerical Values Only');
                $('.ErrorMsg').show();
                return false;
            }
        }
    });
    //country dropdown change event
    $("#ddlCountry").change(function () {

        var cntryCode = $("#ddlCountry").val();
        $("#ddlCountry").removeClass();
        $("#ddlCountry").addClass(cntryCode);
        var countryPrefix = $('#ddlCountry').val();
        if (countryPrefix == 'AE') {
            $('#txtPhone').attr('maxlength', 9);
        }
        else if (countryPrefix == 'BH') {
            $('#txtPhone').attr('maxlength', 8);
        }
        else {
            $('#txtPhone').attr('maxlength', 10);
        }
    });
    $('.terms').click(function (e) {
        e.preventDefault();
        $('.ver-inline-menu a[href="#tab_3"]').tabs('show');
    });
    $('.contact').click(function (e) {
        e.preventDefault();
        $('.ver-inline-menu a[href="#tab_4"]').tabs('show');
    });
    // sent otp via notification button click event
    $('#btnSentOtp').click(function () {
        $('#ErrorMsg').html('');
        var mobileNumer = $('#txtPhone').val().trim();
        var countryCode = $('#ddlCountry').val();
        var prefix = '';
        var retVal = 1;
        if (countryCode == 'UK') {
            prefix = '44';
            retVal = MobileLengthValidator(mobileNumer, 10);
        }
        else if (countryCode == 'AE') {
            prefix = '971';
            retVal = MobileLengthValidator(mobileNumer, 9);
        }
        else if (countryCode == 'BH') {
            prefix = '973';
            retVal = MobileLengthValidator(mobileNumer, 8);
        }
        else if (countryCode == 'US') {
            prefix = '1';
            retVal = MobileLengthValidator(mobileNumer, 10);
        }
        else {
            prefix = '91';
            if (mobileNumer.charAt(0) == 9 || mobileNumer.charAt(0) == 8 || mobileNumer.charAt(0) == 7 || mobileNumer.charAt(0) == 6) {

            }
            else {
                $('.ErrorMsg').text("Invalid MobileNumber");
                $('.ErrorMsg').show();
                return false;
            }
            retVal = MobileLengthValidator(mobileNumer, 10);
        }
        if (retVal == 0) {
            $('.ErrorMsg').text("Invalid MobileNumber");
            $('.ErrorMsg').show();
            return false;
        }

        var mobile = prefix + mobileNumer;

        UserLoginCheck(mobile, "", 0);

        var counter = 30;
        var id;
        id = setInterval(function () {
            counter--;
            if (counter < 0) {
                clearInterval(id);
                $('.WaitMsg').html('');
                $('.WaitMsg').html('Have not received OTP notification yet?');
                $('#btnSentOtpViaSms').show();
            } else {
                $('#timer').html(counter.toString())
            }
        }, 1000);
    });
    // Sent otp via sms button click event
    $('#btnSentOtpViaSms').on('click', function () {
        $('.ErrorMsg').html('');
        var resendmobile = $(this).attr("mobile");
        txnID = $(this).attr("txnID");
        UserLoginCheck(resendmobile, txnID, 1);
        $('#btnSentOtpViaSms').hide();
        var counter = 60;
        var id;
        id = setInterval(function () {
            counter--;
            if (counter < 0) {
                clearInterval(id);
                $('.WaitMsg').html('Tired of waiting?');
                $('.CallUsMsg').show();

                $('#callUs').show();
            } else {
                $('#timer3').html(counter.toString())
            }
        }, 1000);
    });
    // automatic read otp notification textbox event
    $("#txtOtp").keyup(function (event) {
        var otpLength = $('#txtOtp').val().length
        if (otpLength == 6) {
            $('#txtOtp').addClass('loadinggif');
            $("#txtOtp").attr("disabled", "disabled");
            var otp = $("#txtOtp").val();
            mob = $(this).attr("mobile");
            VerifyOtp(otp, mob);
        }
    });
    $("#txtOtpViaSms").keyup(function (event) {
        var otpLength = $('#txtOtp').val().length
        if (otpLength == 6) {
            $('#txtOtpViaSms').addClass('loadinggif');
            $("#txtOtpViaSms").attr("disabled", "disabled");
            var otp = $("#txtOtpViaSms").val();
            mob = $(this).attr("mobile");
            VerifyOtp(otp, mob);
        }
    });



    $('#foo4').carouFredSel({
        responsive: true,
        width: '100%',
        cookie: false,
        scroll: 1,

        items: {
            width: 300,
            visible: {
                min: 1,
                max: 3
            }
        }
    });


    function UserLoginCheck(mobileNumer, txnID, isResend) {

        // $('#ddlMenu').block({
        // message: 'Processing',
        // css: { border: '3px ', width: '100%' }
        // });


        $.ajax({
            url: "/HandlersWeb/Login.ashx",
            data: {
                mobileNumber: mobileNumer,
                isResend: isResend,
                txnID: txnID,
                type: 1
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (res) {
                if (res.Success == true) {
                    if (isResend == 1) {
                        // $('#ddlMenu').unblock();
                        $('.WaitMsg').html('');
                        $('.WaitMsg').html('Your OTP has been sent via SMS to your registered mobile number.<br><span class="WaitMsg2"> Please wait for <span style="font-weight:bold;font-size:15px;text-align:center;color: rgb(231, 157, 54);" id="timer3">60</span> sec</span>');
                        //$('#formOTP').hide();
                        //$('#formOTPviaSMS').fadeIn();
                    }
                    else {
                        //  $('#ddlMenu').unblock();
                        $('#formLogin').hide();
                        $('#formOTP').fadeIn();
                    }


                    $('#txtOtp').attr('mobile', mobileNumer);
                    $('#btnSentOtpViaSms').attr('txnID', res.txnID);
                    $('#btnSentOtpViaSms').attr('mobile', mobileNumer);



                } else {
                    //  $('#ddlMenu').unblock();
                    $('.ErrorMsg').text(res.Message);
                    $('.ErrorMsg').show();
                    return false;
                }
            },
            error: function (res) {

            }
        });

    }

    function VerifyOtp(otp, mobileNumber) {

        if (otp == '') {
            $('#err-msg').text('Please enter Mobile Number');
            $('#err-div').show();
            return false;
        }
        $.ajax({
            url: "/HandlersWeb/Login.ashx",
            data: {
                Otp: otp,
                type: 2,
                mobileNumber: mobileNumber
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (res) {

                if (res.Success == true) {
                    window.location.href = res.RedirectURL;
                } else {
                    $('#txtOtp').removeClass('loadinggif');
                    $("#txtOtp").removeAttr("disabled");
                    $('.ErrorMsg').text(res.Message);
                    $('.ErrorMsg').show();
                }
            },
            error: function (res) {
                $("#txtOtp").removeAttr("disabled");
                // $('#ddlMenu').unblock();
            }
        });
    }

    function MobileLengthValidator(mobileNumber, length) {
        if (mobileNumber.length == length) {
            return 1;
        }
        else {
            return 0;
        }
    }


});