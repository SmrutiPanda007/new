$(document).ready(function () {
    var global_CountryId = $("#hdnCountryId").val();
    var global_MaxLength = $("#hdnMaxLength").val();
    var global_MinLength = $("#hdnMinLength").val();
	$("#txtNumber").attr('maxlength', global_MaxLength);
    //$('.carousel').carousel();
    $("#txtNumber").val('');
    $("#txtNumber2").val('');
    $('#txtEmail').val('');
    $('#txtEmail2').val('');
    $('#subscribe').val('');
    $(document).on('click', '#subscribe-button,#btnSubscribe,#btnGetDemo,#btnGetDemo2', function (evt) {
     
            if ($("#modalAlert").is(":visible"))
            {
                $("#modalAlert").modal("hide");
                return false;
            }
      

            var mobile = "", email = "";
            if (this.id == "subscribe-button") {
                mobile = $("#txtNumber").val();
                if (mobile == '') {
                    alert('Please Enter Your Mobile Number');
                    return false;
                }


            }
            else if (this.id == "btnSubscribe") {
                mobile = $("#txtNumber2").val();
                if (mobile == '') {
                    alert('Please Enter Your Mobile Number');
                    return false;
                }
            }
            else if (this.id == "btnGetDemo") {
                email = $('#txtEmail').val();
                if (email == '') {
                    alert('Please Enter Your Email');
                    return false;
                }

            }
            else if (this.id == "btnGetDemo2") {
                email = $('#txtEmail2').val();
                if (email == '') {
                    alert('Please Enter Your Email');
                    return false;
                }

            }



            if ((email != '' && validateForm(email)) || (mobile != '' && MobileValidator(mobile) == 1))
                $.ajax({
                    url: "/Handlers/Index.ashx",
                    data: {
                        type: 1,
                        MobileNumber: mobile,
                        Email: email,
                        CountryId: global_CountryId,
                        LeadType: 1
                    },
                    async: false,
                    method: "POST",
                    dataType: "JSON",
                    success: function (res) {
                        if (res.Success == 1) {

                            $("#txtNumber").val('');
                            $("#txtNumber2").val('');
                            $('#txtEmail').val('');
                            $('#txtEmail2').val('');
                            $('#subscribe').val('');
                            $('#subscribe-button,#btnSubscribe,#btnGetDemo,#btnGetDemo2').focusout();
                            if (email != '')
                                $('#alertMsg').text('Thank you for requesting a  grpTalk demo. Our team will revert to you within 2 business days');
                            else {
                                $('#alertMsg').text('Thank you for choosing grpTalk. You will soon receive the app download link on your phone. In case of any issues, please email us on hello@grpTalk.com');
                            }
                            $('#modalAlert').modal("show");

                        }
                    }
                });
        
    });



    function validateForm(email) {
        var atpos = email.indexOf("@");
        var dotpos = email.lastIndexOf(".");
        if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= email.length) {
            alert("Not a valid e-mail address");
            return false;
        }
        else
            return true;
    }



    function MobileValidator(mobile) {
        if (global_CountryId == 108) {
            var ret = 1;
            var filter = /^[0-9]*$/;
            if (!(filter.test(mobile))) {
                ret = 0;
            }
            if (mobile.length > global_MinLength) {

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

                } else if (mobile.length == global_MaxLength) {
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
            else if (mobile.length < global_MinLength) {
                ret = 0;
            } else {
                if (mobile.length == global_MinLength) {
                    if (mobile.charAt(0) == 7 || mobile.charAt(0) == 8 || mobile.charAt(0) == 9) {
                        ret = 1;
                    } else {
                        ret = 0;
                    }
                }

            }
            if (ret == 0)
                alert("Please Enter Valid Mobile Number");
            return ret;
        }
    }


});

$(document).keypress(function (e) {
    var key = e.which;
    if (key == 13) {
        if ($("#modalAlert").is(":visible")) {
            $("#modalAlert").modal("hide");
            return false;
        }
    }
});

function isNumber(evt) {

    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode > 31 && (charCode < 48 || charCode > 57)) || charCode == 13)
    { return false; }
    return true;
}