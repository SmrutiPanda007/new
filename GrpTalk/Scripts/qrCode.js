var checked = 0;
var channelname = "";
var QrCodeCount = 1;
var UnSubscribeChannel = "";


$(document).ready(function () {
    generateBarCode();
    channelname = $.trim($('#qrChannel').text());



    $('.terms').click(function (e) {
        e.preventDefault();
        $('.ver-inline-menu a[href="#tab_3"]').tab('show');
    });
    $('.contact').click(function (e) {
        e.preventDefault();
        $('.ver-inline-menu a[href="#tab_4"]').tab('show');
    });
});

$('#regenerateQrCode').click(function () {

    QrCodeCount = 0;
    generateBarCode();


});

function generateBarCode() {

    if (QrCodeCount < 5) {
        $.ajax({
            url: "/HandlersWeb/Login.ashx",
            data: {
                type: 5
            },
            async: false,
            method: "POST",
            dataType: "JSON",
            success: function (result) {
                var pusher;
                if (result.Success == true) {
                    pusher = new Pusher('ed522d982044e2680be6');
                    channelname = result.ConfCode;

                    UnSubscribeChannel = $.trim($('#qrChannel').text());
                    if (UnSubscribeChannel != "") {

                        pusher.unsubscribe(UnSubscribeChannel);

                        console.log("unsubscribetyrtururt :" + UnSubscribeChannel);
                    }
                    $('#qrChannel').text(channelname);
                    $('#imgQrCode').attr('src', 'data:image/png;base64,' + result.BaseString)
                }
                else if (result.Success == false) {
                }
                QrCodeCount = QrCodeCount + 1;
                channelname = $.trim($('#qrChannel').text());

                pusher = new Pusher('ed522d982044e2680be6');
                var channel = pusher.subscribe(channelname);
                console.log(channelname);
                channel.bind('Login', function (res) {

                    console.log(res);
                    if (res.IsloggedIn == 0) {
                        $('#home').html('<div align="center"><img src="/images/load.gif" style="position:relative;top:200px;"></img></div>');
                        setTimeout(function () { ValidateQrCode(res); }, 3000);

                    }



                });

            },
            error: function (res)
            { },
            dataType: "JSON",
            complete: setTimeout(function () { generateBarCode() }, 30000),
            timeout: 2000
        });
    }
    //else {
    //    $('#imgQrCode').attr('src', '/images/QrCodeReload.png');
    //    $('#imgQrCode').attr('style', 'cursor:pointer;width:40%;height:40%;margin-top:50px');
    //    $('#imgQrCode').addClass('reload');
    //}

}
function ValidateQrCode(response) {
    //alert(response.QrCode);
    //Getting Browser Name
    var browserJson = bowser;
    var browserName = String(browserJson.name);
    if (browserName == "Internet Explorer")
    { browserName = 'IE'; }
    else if (browserName == "Chrome")
    { browserName = 'Chrome'; }
    else if (browserName == "Firefox") {
        browserName = 'Firefox';
    }
    else if (browserName == "Safari") {
        browserName = 'Safari';
    }

    else if (browserName == "Opera") {
        browserName = "Opera";
    }
    else {
        browserName = browserName;
    }

    //Getting OS Name
    var unknown = '-';
    var nVer = navigator.appVersion;
    var nAgt = navigator.userAgent;
    var version = '' + parseFloat(navigator.appVersion);
    var majorVersion = parseInt(navigator.appVersion, 10);
    if ((ix = version.indexOf(';')) != -1) version = version.substring(0, ix);
    if ((ix = version.indexOf(' ')) != -1) version = version.substring(0, ix);
    if ((ix = version.indexOf(')')) != -1) version = version.substring(0, ix);

    majorVersion = parseInt('' + version, 10);
    if (isNaN(majorVersion)) {
        version = '' + parseFloat(navigator.appVersion);
        majorVersion = parseInt(navigator.appVersion, 10);
    }

    var os = unknown;
    var clientStrings = [
        { s: 'Windows 10', r: /(Windows 10.0|Windows NT 10.0)/ },
        { s: 'Windows 8.1', r: /(Windows 8.1|Windows NT 6.3)/ },
        { s: 'Windows 8', r: /(Windows 8|Windows NT 6.2)/ },
        { s: 'Windows 7', r: /(Windows 7|Windows NT 6.1)/ },
        { s: 'Windows Vista', r: /Windows NT 6.0/ },
        { s: 'Windows Server 2003', r: /Windows NT 5.2/ },
        { s: 'Windows XP', r: /(Windows NT 5.1|Windows XP)/ },
        { s: 'Windows 2000', r: /(Windows NT 5.0|Windows 2000)/ },
        { s: 'Windows ME', r: /(Win 9x 4.90|Windows ME)/ },
        { s: 'Windows 98', r: /(Windows 98|Win98)/ },
        { s: 'Windows 95', r: /(Windows 95|Win95|Windows_95)/ },
        { s: 'Windows NT 4.0', r: /(Windows NT 4.0|WinNT4.0|WinNT|Windows NT)/ },
        { s: 'Windows CE', r: /Windows CE/ },
        { s: 'Windows 3.11', r: /Win16/ },
        { s: 'Android', r: /Android/ },
        { s: 'Open BSD', r: /OpenBSD/ },
        { s: 'Sun OS', r: /SunOS/ },
        { s: 'Linux', r: /(Linux|X11)/ },
        { s: 'iOS', r: /(iPhone|iPad|iPod)/ },
        { s: 'Mac OS X', r: /Mac OS X/ },
        { s: 'Mac OS', r: /(MacPPC|MacIntel|Mac_PowerPC|Macintosh)/ },
        { s: 'QNX', r: /QNX/ },
        { s: 'UNIX', r: /UNIX/ },
        { s: 'BeOS', r: /BeOS/ },
        { s: 'OS/2', r: /OS\/2/ },
        { s: 'Search Bot', r: /(nuhk|Googlebot|Yammybot|Openbot|Slurp|MSNBot|Ask Jeeves\/Teoma|ia_archiver)/ }
    ];
    for (var id in clientStrings) {
        var cs = clientStrings[id];
        if (cs.r.test(nAgt)) {
            os = cs.s;
            break;
        }
    }

    var osVersion = unknown;

    if (/Windows/.test(os)) {
        osVersion = /Windows (.*)/.exec(os)[1];
        os = 'Windows';
    }
    switch (os) {
        case 'Mac OS X':
            osVersion = /Mac OS X (10[\.\_\d]+)/.exec(nAgt)[1];
            break;

        case 'Android':
            osVersion = /Android ([\.\_\d]+)/.exec(nAgt)[1];
            break;

        case 'iOS':
            osVersion = /OS (\d+)_(\d+)_?(\d+)?/.exec(nVer);
            osVersion = osVersion[1] + '.' + osVersion[2] + '.' + (osVersion[3] | 0);
            break;
    }

    $.ajax({
        url: "/HandlersWeb/Login.ashx",
        data: {
            type: 4,
            qrCodeAccessToken: response.QrCodeAccessToken,
            QrCode: response.QrCode,
            DeviceUniqueId: response.DeviceUniqueId,
            DeviceToken: response.DeviceToken,
            OsId: response.OsId,
            browserName: browserName,
            osName: os + ' ' + osVersion,
            keepMeLoggedIn: checked

        },
        async: false,
        method: "POST",
        dataType: "JSON",
        success: function (res) {

            if (res.Success == true) {
                window.location.href = res.RedirectURL;
                localStorage.clear();
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