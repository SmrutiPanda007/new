<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Faq.aspx.cs" Inherits="GrpTalk.Faq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="css_body" runat="server">

    <link rel="stylesheet" href="https://code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <link href="js/Jquery-ui2.css" rel="stylesheet" />


    <style type="text/css">
        h3 {
            font-size: 14px !important;
        }


        .ui-icon,
.ui-widget-content .ui-icon {
	background-image: url("images/ui-icons_222222_256x240.png");
}
.ui-widget-header .ui-icon {
	background-image: url("images/ui-icons_222222_256x240.png");
}
.ui-state-default .ui-icon {
	background-image: url("images/ui-icons_888888_256x240.png");
}
.ui-state-hover .ui-icon,
.ui-state-focus .ui-icon {
	background-image: url("images/ui-icons_454545_256x240.png");
}
.ui-state-active .ui-icon {
	background-image: url("images/ui-icons_454545_256x240.png");
}
.ui-state-highlight .ui-icon {
	background-image: url("images/ui-icons_2e83ff_256x240.png");
}
.ui-state-error .ui-icon,
.ui-state-error-text .ui-icon {
	background-image: url("images/ui-icons_cd0a0a_256x240.png");
}

 positioning 
.ui-icon-blank { background-position: 16px 16px; }
.ui-icon-carat-1-n { background-position: 0 0; }
.ui-icon-carat-1-ne { background-position: -16px 0; }
.ui-icon-carat-1-e { background-position: -32px 0; }
.ui-icon-carat-1-se { background-position: -48px 0; }
.ui-icon-carat-1-s { background-position: -64px 0; }
.ui-icon-carat-1-sw { background-position: -80px 0; }
.ui-icon-carat-1-w { background-position: -96px 0; }
.ui-icon-carat-1-nw { background-position: -112px 0; }
.ui-icon-carat-2-n-s { background-position: -128px 0; }
.ui-icon-carat-2-e-w { background-position: -144px 0; }
.ui-icon-triangle-1-n { background-position: 0 -16px; }
.ui-icon-triangle-1-ne { background-position: -16px -16px; }
.ui-icon-triangle-1-e { background-position: -32px -16px; }
.ui-icon-triangle-1-se { background-position: -48px -16px; }
.ui-icon-triangle-1-s { background-position: -64px -16px; }
.ui-icon-triangle-1-sw { background-position: -80px -16px; }
.ui-icon-triangle-1-w { background-position: -96px -16px; }
.ui-icon-triangle-1-nw { background-position: -112px -16px; }
.ui-icon-triangle-2-n-s { background-position: -128px -16px; }
.ui-icon-triangle-2-e-w { background-position: -144px -16px; }
.ui-icon-arrow-1-n { background-position: 0 -32px; }
.ui-icon-arrow-1-ne { background-position: -16px -32px; }
.ui-icon-arrow-1-e { background-position: -32px -32px; }
.ui-icon-arrow-1-se { background-position: -48px -32px; }
.ui-icon-arrow-1-s { background-position: -64px -32px; }
.ui-icon-arrow-1-sw { background-position: -80px -32px; }
.ui-icon-arrow-1-w { background-position: -96px -32px; }
.ui-icon-arrow-1-nw { background-position: -112px -32px; }
.ui-icon-arrow-2-n-s { background-position: -128px -32px; }
.ui-icon-arrow-2-ne-sw { background-position: -144px -32px; }
.ui-icon-arrow-2-e-w { background-position: -160px -32px; }
.ui-icon-arrow-2-se-nw { background-position: -176px -32px; }
.ui-icon-arrowstop-1-n { background-position: -192px -32px; }
.ui-icon-arrowstop-1-e { background-position: -208px -32px; }
.ui-icon-arrowstop-1-s { background-position: -224px -32px; }
.ui-icon-arrowstop-1-w { background-position: -240px -32px; }
.ui-icon-arrowthick-1-n { background-position: 0 -48px; }
.ui-icon-arrowthick-1-ne { background-position: -16px -48px; }
.ui-icon-arrowthick-1-e { background-position: -32px -48px; }
.ui-icon-arrowthick-1-se { background-position: -48px -48px; }
.ui-icon-arrowthick-1-s { background-position: -64px -48px; }
.ui-icon-arrowthick-1-sw { background-position: -80px -48px; }
.ui-icon-arrowthick-1-w { background-position: -96px -48px; }
.ui-icon-arrowthick-1-nw { background-position: -112px -48px; }
.ui-icon-arrowthick-2-n-s { background-position: -128px -48px; }
.ui-icon-arrowthick-2-ne-sw { background-position: -144px -48px; }
.ui-icon-arrowthick-2-e-w { background-position: -160px -48px; }
.ui-icon-arrowthick-2-se-nw { background-position: -176px -48px; }
.ui-icon-arrowthickstop-1-n { background-position: -192px -48px; }
.ui-icon-arrowthickstop-1-e { background-position: -208px -48px; }
.ui-icon-arrowthickstop-1-s { background-position: -224px -48px; }
.ui-icon-arrowthickstop-1-w { background-position: -240px -48px; }
.ui-icon-arrowreturnthick-1-w { background-position: 0 -64px; }
.ui-icon-arrowreturnthick-1-n { background-position: -16px -64px; }
.ui-icon-arrowreturnthick-1-e { background-position: -32px -64px; }
.ui-icon-arrowreturnthick-1-s { background-position: -48px -64px; }
.ui-icon-arrowreturn-1-w { background-position: -64px -64px; }
.ui-icon-arrowreturn-1-n { background-position: -80px -64px; }
.ui-icon-arrowreturn-1-e { background-position: -96px -64px; }
.ui-icon-arrowreturn-1-s { background-position: -112px -64px; }
.ui-icon-arrowrefresh-1-w { background-position: -128px -64px; }
.ui-icon-arrowrefresh-1-n { background-position: -144px -64px; }
.ui-icon-arrowrefresh-1-e { background-position: -160px -64px; }
.ui-icon-arrowrefresh-1-s { background-position: -176px -64px; }
.ui-icon-arrow-4 { background-position: 0 -80px; }
.ui-icon-arrow-4-diag { background-position: -16px -80px; }
.ui-icon-extlink { background-position: -32px -80px; }
.ui-icon-newwin { background-position: -48px -80px; }
.ui-icon-refresh { background-position: -64px -80px; }
.ui-icon-shuffle { background-position: -80px -80px; }
.ui-icon-transfer-e-w { background-position: -96px -80px; }
.ui-icon-transferthick-e-w { background-position: -112px -80px; }
.ui-icon-folder-collapsed { background-position: 0 -96px; }
.ui-icon-folder-open { background-position: -16px -96px; }
.ui-icon-document { background-position: -32px -96px; }
.ui-icon-document-b { background-position: -48px -96px; }
.ui-icon-note { background-position: -64px -96px; }
.ui-icon-mail-closed { background-position: -80px -96px; }
.ui-icon-mail-open { background-position: -96px -96px; }
.ui-icon-suitcase { background-position: -112px -96px; }
.ui-icon-comment { background-position: -128px -96px; }
.ui-icon-person { background-position: -144px -96px; }
.ui-icon-print { background-position: -160px -96px; }
.ui-icon-trash { background-position: -176px -96px; }
.ui-icon-locked { background-position: -192px -96px; }
.ui-icon-unlocked { background-position: -208px -96px; }
.ui-icon-bookmark { background-position: -224px -96px; }
.ui-icon-tag { background-position: -240px -96px; }
.ui-icon-home { background-position: 0 -112px; }
.ui-icon-flag { background-position: -16px -112px; }
.ui-icon-calendar { background-position: -32px -112px; }
.ui-icon-cart { background-position: -48px -112px; }
.ui-icon-pencil { background-position: -64px -112px; }
.ui-icon-clock { background-position: -80px -112px; }
.ui-icon-disk { background-position: -96px -112px; }
.ui-icon-calculator { background-position: -112px -112px; }
.ui-icon-zoomin { background-position: -128px -112px; }
.ui-icon-zoomout { background-position: -144px -112px; }
.ui-icon-search { background-position: -160px -112px; }
.ui-icon-wrench { background-position: -176px -112px; }
.ui-icon-gear { background-position: -192px -112px; }
.ui-icon-heart { background-position: -208px -112px; }
.ui-icon-star { background-position: -224px -112px; }
.ui-icon-link { background-position: -240px -112px; }
.ui-icon-cancel { background-position: 0 -128px; }
.ui-icon-plus { background-position: -16px -128px; }
.ui-icon-plusthick { background-position: -32px -128px; }
.ui-icon-minus { background-position: -48px -128px; }
.ui-icon-minusthick { background-position: -64px -128px; }
.ui-icon-close { background-position: -80px -128px; }
.ui-icon-closethick { background-position: -96px -128px; }
.ui-icon-key { background-position: -112px -128px; }
.ui-icon-lightbulb { background-position: -128px -128px; }
.ui-icon-scissors { background-position: -144px -128px; }
.ui-icon-clipboard { background-position: -160px -128px; }
.ui-icon-copy { background-position: -176px -128px; }
.ui-icon-contact { background-position: -192px -128px; }
.ui-icon-image { background-position: -208px -128px; }
.ui-icon-video { background-position: -224px -128px; }
.ui-icon-script { background-position: -240px -128px; }
.ui-icon-alert { background-position: 0 -144px; }
.ui-icon-info { background-position: -16px -144px; }
.ui-icon-notice { background-position: -32px -144px; }
.ui-icon-help { background-position: -48px -144px; }
.ui-icon-check { background-position: -64px -144px; }
.ui-icon-bullet { background-position: -80px -144px; }
.ui-icon-radio-on { background-position: -96px -144px; }
.ui-icon-radio-off { background-position: -112px -144px; }
.ui-icon-pin-w { background-position: -128px -144px; }
.ui-icon-pin-s { background-position: -144px -144px; }
.ui-icon-play { background-position: 0 -160px; }
.ui-icon-pause { background-position: -16px -160px; }
.ui-icon-seek-next { background-position: -32px -160px; }
.ui-icon-seek-prev { background-position: -48px -160px; }
.ui-icon-seek-end { background-position: -64px -160px; }
.ui-icon-seek-start { background-position: -80px -160px; }
 ui-icon-seek-first is deprecated, use ui-icon-seek-start instead 
.ui-icon-seek-first { background-position: -80px -160px; }
.ui-icon-stop { background-position: -96px -160px; }
.ui-icon-eject { background-position: -112px -160px; }
.ui-icon-volume-off { background-position: -128px -160px; }
.ui-icon-volume-on { background-position: -144px -160px; }
.ui-icon-power { background-position: 0 -176px; }
.ui-icon-signal-diag { background-position: -16px -176px; }
.ui-icon-signal { background-position: -32px -176px; }
.ui-icon-battery-0 { background-position: -48px -176px; }
.ui-icon-battery-1 { background-position: -64px -176px; }
.ui-icon-battery-2 { background-position: -80px -176px; }
.ui-icon-battery-3 { background-position: -96px -176px; }
.ui-icon-circle-plus { background-position: 0 -192px; }
.ui-icon-circle-minus { background-position: -16px -192px; }
.ui-icon-circle-close { background-position: -32px -192px; }
.ui-icon-circle-triangle-e { background-position: -48px -192px; }
.ui-icon-circle-triangle-s { background-position: -64px -192px; }
.ui-icon-circle-triangle-w { background-position: -80px -192px; }
.ui-icon-circle-triangle-n { background-position: -96px -192px; }
.ui-icon-circle-arrow-e { background-position: -112px -192px; }
.ui-icon-circle-arrow-s { background-position: -128px -192px; }
.ui-icon-circle-arrow-w { background-position: -144px -192px; }
.ui-icon-circle-arrow-n { background-position: -160px -192px; }
.ui-icon-circle-zoomin { background-position: -176px -192px; }
.ui-icon-circle-zoomout { background-position: -192px -192px; }
.ui-icon-circle-check { background-position: -208px -192px; }
.ui-icon-circlesmall-plus { background-position: 0 -208px; }
.ui-icon-circlesmall-minus { background-position: -16px -208px; }
.ui-icon-circlesmall-close { background-position: -32px -208px; }
.ui-icon-squaresmall-plus { background-position: -48px -208px; }
.ui-icon-squaresmall-minus { background-position: -64px -208px; }
.ui-icon-squaresmall-close { background-position: -80px -208px; }
.ui-icon-grip-dotted-vertical { background-position: 0 -224px; }
.ui-icon-grip-dotted-horizontal { background-position: -16px -224px; }
.ui-icon-grip-solid-vertical { background-position: -32px -224px; }
.ui-icon-grip-solid-horizontal { background-position: -48px -224px; }
.ui-icon-gripsmall-diagonal-se { background-position: -64px -224px; }
.ui-icon-grip-diagonal-se { background-position: -80px -224px; }


    </style>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">


    <h3>Daily Bonus</h3>
    <div id="accordion1">
        <h3>What is Daily Bonus?</h3>
        <div>
            <p>Daily 10 minutes of grp minutes will be added to your account with a validity of 24hrs. After validity these minutes will expire.</p>
        </div>

        <h3>With how many members I can talk using these group minutes?</h3>
        <div>
            <p>You can talk with up to 20 members using this bonus group minutes</p>
        </div>

        <h3>Can I split this bonus for multiple calls?</h3>
        <div>
            <p>Yes, you can split these 10 minutes of bonus group minutes to multiple calls.</p>
        </div>

        <h3>Can I choose the balance type before making a grpTalk?</h3>
        <div>
            <p>Yes, after you tapped start now or quick call button, one pop up will be displayed to select the balance type (main account balance or Daily Bonus).</p>
        </div>

        <h3>Can I use these group minutes to schedule a call?</h3>
        <div>
            <p>No, you cannot use this bonus talk to schedule a call.</p>
        </div>

        <h3>What is the difference between Standard Minutes and Group minutes?</h3>
        <div>
            <p>
                <b>Standard minutes</b> are regular grpTalk minutes, which will be consumed according to the duration of the call and the number of participants.
                                    For instance if you host a grpTalk with 4 members for 5 minutes
                                    4 members x 5 minutes =  20 Total minutes will be consumed.<br />
                <br />
                <b>Group Minutes</b> are consumed according to the duration of the call only. For instance if you host a grpTalk with20 members for 5 minutes only 5 minutes will be consumed from your group minutes.
            </p>
        </div>
    </div>

    <h3>How grpTalk works</h3>
    <div id="accordion2">
        <h3>Can I receive a call from grpTalk if I don’t have my cellular data turned on or an active internet connection?</h3>
        <div>
            <p>3G or internet is not required to receive a call. However cellular data (3G or faster) or internet is required to schedule a call and to control the call using the app.</p>
        </div>

        <h3>Do my friends and family need grpTalk app to receive my grpTalk calls?</h3>
        <div>
            <p>No app is required to receive a call from grpTalk. However, to host your own grpTalk you need to download the app.</p>
        </div>

        <h3>Can I add a contact to grpTalk which is not in my phone contact book?</h3>
        <div>
            <p>No, you have to first add that contact in your phone book and then add to grpTalk.</p>
        </div>

        <h3>What should I do if I missed a grpTalk call?</h3>
        <div>
            <p>You can always join a grpTalk by calling that number you got a missed call from as long as the talk is ongoing.</p>
        </div>

        <h3>Why is there no PIN to join a grpTalk?</h3>
        <div>
            <p>You can join a grpTalk only if you are invited. Therefore no PIN is necessary.</p>
        </div>

        <h3>Does my network charge me for receiving a call from grpTalk?</h3>
        <div>
            <p>Since incoming calls are free in India, you will not be charged.</p>
        </div>

        <h3>Will other members be able to participate in the grpTalk without the host joining?</h3>
        <div>
            <p>Yes, other members can start the grpTalk without the host.</p>
        </div>

        <h3>If my call is disconnected due to some reason, while my grpTalk is in progress, how can I connect to my live grpTalk?</h3>
        <div>
            <p>To reconnect you can always call the grpTalk number you received a call from.</p>
        </div>

        <h3>Can I use my account on multiple phones? Will I lose my grpTalk history?</h3>
        <div>
            <p>No, you can only use it on one phone at a time. If you activate your account on another phone your account will be deactivated on the old phone and grpTalk history will be transferred to the new phone.</p>
        </div>

        <h3>What if some of my contacts are on DND, will grpTalk still be able to call them?</h3>
        <div>
            <p>If some of the members in your group are on DND, you (host) will be prompted to send them an SMS from your mobile asking them to opt-in. Only those that opt-in will receive a call from grpTalk at scheduled time.</p>
        </div>

        <h3>Can I call other countries using grpTalk?</h3>
        <div>
            <p>Currently you can only call members in India. International calling is coming soon.</p>
        </div>
    </div>

    <h3>Features</h3>
    <div id="accordion3">
        <h3>How many people can I talk to using grpTalk?</h3>
        <div>
            <p>You can talk to maximum of 20 members at a time. You need a minimum of 3 members to host a grpTalk.</p>
        </div>

        <h3>How do I selectively mute participants on the call?</h3>
        <div>
            <p>Yes, using the mute button against every member’s name in the live grpTalk panel. You can also mute all members at once using the ‘mute all’ button above the host name.</p>
        </div>

        <h3>Can I add or remove members as a host, while grpTalk is in progress?</h3>
        <div>
            <p>Yes, a host can add or remove members while grpTalk is in progress.</p>
        </div>

        <h3>How do I record my grpTalk? Can I download the recordings?</h3>
        <div>
            <p>Yes you can turn on or off grpTalk recording whenever you want. You can download individual recordings in that grpTalk’s history tab.</p>
        </div>

        <h3>Where can I see detailed reports of my past grpTalks?</h3>
        <div>
            <p>You can view them in the ‘History’ tab of a particular grpTalk. Access details of members that participated, recordings, call charges and more.</p>
        </div>

        <h3>Can I schedule a call for next month?</h3>
        <div>
            <p>Yes, using the schedule feature.</p>
        </div>

        <h3>I scheduled a call for tomorrow and instead started the call today. Does it affect my scheduled call?</h3>
        <div>
            <p>No. Your scheduled grpTalk for tomorrow will happen as planned unless you cancel it.</p>
        </div>
    </div>

    <h3>Call Charges & Payment</h3>
    <div id="accordion4">
        <h3>Why am I being charged for grpTalk?</h3>
        <div>
            <p>grpTalk calls members on their mobile phones. Therefore regular carrier charges along with a minimum service cost will apply.</p>
        </div>

        <h3>If I host a grpTalk with 4 members for 10 minutes, how many minutes will I be charged?</h3>
        <div>
            <p>
                4 members x 10 m = 40 m<br />
                1 host x 10 m = 10 m<br />
                Total 50 minutes (provided everyone is called by grpTalk at the same time)
            </p>
        </div>

        <h3>How do I make a payment? How can I recharge my account?</h3>
        <div>
            <p>Right now you need to call our support number to make a payment or recharge your account.</p>
        </div>

        <h3>How do I check my balance?</h3>
        <div>
            <p>You can see your account balance in the Account Tab in Main Menu.</p>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="reviewSection" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">

    <script src="https://code.jquery.com/ui/1.12.0-rc.2/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(function () {
                $("#accordion,#accordion1,#accordion2,#accordion3,#accordion4").accordion({
                    collapsible: true,
                    active: false
                });
            }
            );

        });
    </script>
</asp:Content>
