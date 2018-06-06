<%@ Page Title="" Language="C#" MasterPageFile="~/LandingMaster.Master" AutoEventWireup="true" CodeBehind="Faqs.aspx.cs" Inherits="GrpTalk.Faqs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>grpTalk | Frequently Asked Questions</title>
    <meta name="description" content="Have a question about grpTalk? Visit our FAQs page to learn exactly how grpTalk can improve business communication with clients and customers." />
    <style>
    section#about-us {
    float: left;
    width: 100%;
    height: auto;
    margin: 0px auto;
    background: #f4f3f3;
}
    </style>
    <!-- Intro Header -->

        <script>
            $(function () {
                // Since there's no list-group/tab integration in Bootstrap
                $('.list-group-item').on('click', function (e) {
                    var previous = $(this).closest(".list-group").children(".active");
                    previous.removeClass('active'); // previous list-item
                    $(e.target).addClass('active'); // activated list-item
                });
            });
</script>
<body id="page-top" data-spy="scroll" data-target=".navbar-fixed-top">

<div class="cta">
        <i class="fa fa-phone"></i><span>For More Information<br>
            Give Missed Call To 
				
					<span class="font-conf-blue bold" id="missedCallNo">91-40-4747-5550</span>
				
					
        </span>
    </div>



<!-- Intro Header -->


<header class="terms-intro">
<img src="img/faq.jpg" class="img-responsive" style="max-height:450px; width:100%;">
    <div class=" container-fluid">    
        <div class="container" style="padding:50px;">
  <div class="col-md-4">
    <ul class="list-group help-group">
      <div class="faq-list list-group nav nav-tabs">
        <a href="#tab1" class="list-group-item active" role="tab" data-toggle="tab">How does grpTalk work? </a>
        <a href="#tab2" class="list-group-item" role="tab" data-toggle="tab"><i class="mdi mdi-account"></i> Features</a>
        <a href="#tab3" class="list-group-item" role="tab" data-toggle="tab"><i class="mdi mdi-account-settings"></i> Call Charges & Payments</a>
        <a href="#tab4" class="list-group-item" role="tab" data-toggle="tab"><i class="mdi mdi-account-settings"></i> Daily Bonus</a>
          <a href="#tab5" class="list-group-item" role="tab" data-toggle="tab"><i class="mdi mdi-account-settings"></i> Advanced Settings</a>
      </div>
    </ul>
  </div>
  <div class="col-md-8">
    <div class="tab-content panels-faq">
      <div class="tab-pane active" id="tab1">
        <div class="panel-group" id="help-accordion-1">
          <div class="panel panel-default panel-help">
            <a href="#opret-produkt" data-toggle="collapse" data-parent="#help-accordion-1">
              <div class="panel-heading">
                <h2>Will I be able to receive a call from grpTalk without being connected to the internet?</h2>
              </div>
            </a>
            <div id="opret-produkt" class="collapse in">
              <div class="panel-body">
                <p>Yes, you will be able to receive a call even if you aren’t connected to the internet as grpTalk works on PSTN (public switched telephone network). </p>
              </div>
            </div>
          </div>
          <div class="panel panel-default panel-help">
            <a href="#rediger-produkt" data-toggle="collapse" data-parent="#help-accordion-1">
              <div class="panel-heading">
                <h2>Is it necessary for the participants of grpTalk call to  have the app installed?</h2>
              </div>
            </a>
            <div id="rediger-produkt" class="collapse">
              <div class="panel-body">
                <p>No, the app doesn’t have to be installed by the participants. However, the host, who initiates the call, needs to install grpTalk on their device. </p>
              </div>
            </div>
          </div>
          <div class="panel panel-default panel-help">
            <a href="#ret-pris" data-toggle="collapse" data-parent="#help-accordion-1">
              <div class="panel-heading">
                <h2>Can I call someone who isn’t in my phone’s contact list?</h2>
              </div>
            </a>
            <div id="ret-pris" class="collapse">
              <div class="panel-body">
                <p>No, you can’t call someone who isn’t in your contact list. So, you have to save the number to your contact list in order to place the call.</p>
              </div>
            </div>
          </div>
          <div class="panel panel-default panel-help">
            <a href="#slet-produkt" data-toggle="collapse" data-parent="#help-accordion-1">
              <div class="panel-heading">
                <h2>What can I do if I miss a grpTalk call?</h2>
              </div>
            </a>
            <div id="slet-produkt" class="collapse">
              <div class="panel-body">
                <p>If you miss a grpTalk call, you can call back to the number you got a missed call fromand join the conference, provided the call is still active.</p>
              </div>
            </div>
          </div>
          <div class="panel panel-default panel-help">
            <a href="#opret-kampagne" data-toggle="collapse" data-parent="#help-accordion-1">
              <div class="panel-heading">
                <h2>Will I be able to use my grpTalk account on multiple phones? </h2>
              </div>
            </a>
            <div id="opret-kampagne" class="collapse">
              <div class="panel-body">
                <p>No, you won’t be able to use your grpTalk account on multiple phones. If you activate your account on the second phone, your account will be deactivated on the first phone and the grpTalk history will be transferred to the new phone.</p>
              </div>
            </div>
          </div>
          
          <div class="panel panel-default panel-help">
            <a href="#opret-kampagnee" data-toggle="collapse" data-parent="#help-accordion-1">
              <div class="panel-heading">
                <h2>What if some of my contacts are on DND, will grpTalk still be able to call them? </h2>
              </div>
            </a>
            <div id="opret-kampagnee" class="collapse">
              <div class="panel-body">
                <p>If any of the members in your group are on DND, the host will be prompted to send them an SMS asking them to opt-in. Only those who opt-in will receive a call from grpTalk.</p>
              </div>
            </div>
          </div>
          
        </div>
      </div>
      <div class="tab-pane" id="tab2">
        <div class="panel-group" id="help-accordion-2">      
          <div class="panel panel-default panel-help">
            <a href="#help-three" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>How many people can I talk to using grpTalk?</h2>
              </div>
            </a>
            <div id="help-three" class="collapse in">
              <div class="panel-body">
                <p>You can talk to as many people as you wish using grpTalk. </p>
              </div>
            </div>
          </div>
          
          <div class="panel panel-default panel-help">
             <a href="#help-threee" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>How do I mute a participant on the call?</h2>
              </div>
            </a>
            <div id="help-threee" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can mute a participant by clicking on the ‘Mute’option against the particular member’s name in the live grpTalk panel. You can also mute all members at once using the ‘Mute All’ button, which is above the host’s name.</p>
              </div>
            </div>
          </div>
          
          
       <div class="panel panel-default panel-help">
             <a href="#help-threeee" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I add or remove members as a host while grpTalk is in progress?</h2>
              </div>
            </a>
            <div id="help-threeee" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes, the host can add or remove members while the conference call is still active.  </p>
              </div>
            </div>
          </div>   
          
           <div class="panel panel-default panel-help">
             <a href="#help-threeeee" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I record grpTalk conversations? </h2>
              </div>
            </a>
            <div id="help-threeeee" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes, you can record grpTalk conversations and also download individual recordings in grpTalk’s history tab. </p>
              </div>
            </div>
          </div>  
          
          
          <div class="panel panel-default panel-help">
             <a href="#help-threeeeee" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Where can I access the history of grpTalks?</h2>
              </div>
            </a>
            <div id="help-threeeeee" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can view it in the ‘History’ tab of a particular grpTalk. You can also access details of the members who participated, their call recordings and call charge details. </p>
              </div>
            </div>
          </div> 
          
          <div class="panel panel-default panel-help">
             <a href="#help-threeeeeee" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>I scheduled a call for tomorrow and instead placed the call today. Does it affect my scheduled call?</h2>
              </div>
            </a>
            <div id="help-threeeeeee" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>No. Your scheduled grpTalk for tomorrow will happen as planned unless you cancel it.</p>
              </div>
            </div>
          </div> 
          
        </div>
        
        
        
        
      </div>
      
      <div class="tab-pane" id="tab3">
        <div class="panel-group" id="help-accordion-3">      
          <div class="panel panel-default panel-help">
            <a href="#help-four-1" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Why am I being charged for grpTalk?</h2>
              </div>
            </a>
            <div id="help-four-1" class="collapse in">
              <div class="panel-body">
                <p>grpTalk calls members on their mobile phones. Therefore regular carrier charges along with a minimum service cost will apply. </p>
              </div>
            </div>
          </div>
          
          <div class="panel panel-default panel-help">
             <a href="#help-four-2" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>How do I check my balance?</h2>
              </div>
            </a>
            <div id="help-four-2" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can see your account balance in the ‘Account’ tab in Main Menu.</p>
              </div>
            </div>
          </div>
          
          
       <div class="panel panel-default panel-help">
             <a href="#help-four-3" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I add or remove members as a host while grpTalk is in progress?</h2>
              </div>
            </a>
            <div id="help-four-3" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes, the host can add or remove members while the conference call is still active.  </p>
              </div>
            </div>
          </div>   
          
           <div class="panel panel-default panel-help">
             <a href="#help-four-4" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I record grpTalk conversations? </h2>
              </div>
            </a>
            <div id="help-four-4" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes, you can record grpTalk conversations and also download individual recordings in grpTalk’s history tab. </p>
              </div>
            </div>
          </div>  
          
          
          <div class="panel panel-default panel-help">
             <a href="#help-four-5" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Where can I access the history of grpTalks?</h2>
              </div>
            </a>
            <div id="help-four-5" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can view it in the ‘History’ tab of a particular grpTalk. You can also access details of the members who participated, their call recordings and call charge details. </p>
              </div>
            </div>
          </div> 
          
       
          
        </div>
        
        
        
        
      </div>
      
      <div class="tab-pane" id="tab4">
        <div class="panel-group" id="help-accordion-4">      	 <div class="panel panel-default panel-help">
             <a href="#help-five-3" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>What is Daily Bonus?</h2>
              </div>
            </a>
            <div id="help-five-3" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Every day, 10 minutes will be added to your account with a validity of 24hrs.  </p>
              </div>
            </div>
          </div>   
          
           <div class="panel panel-default panel-help">
             <a href="#help-five-4" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>With how many members I can talk using the group minutes? </h2>
              </div>
            </a>
            <div id="help-five-4" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can talk with up to 20 members using the bonus group minutes.</p>
              </div>
            </div>
          </div>  
          
          
          <div class="panel panel-default panel-help">
             <a href="#help-five-5" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I split this bonus for multiple calls?</h2>
              </div>
            </a>
            <div id="help-five-5" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes, you can split these 10 minutes of bonus group minutes for multiple calls. </p>
              </div>
            </div>
          </div> 
          
        <div class="panel panel-default panel-help">
             <a href="#help-five-6" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I choose the balance type before making a grpTalk?</h2>
              </div>
            </a>
            <div id="help-five-6" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes, when you make a call, a pop up window will display the balance types. Choose your preference and place the call.  </p>
              </div>
            </div>
          </div>
           
           <div class="panel panel-default panel-help">
             <a href="#help-five-7" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>Can I use these group minutes to schedule a call?</h2>
              </div>
            </a>
            <div id="help-five-7" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>No, you cannot use daily bonus to schedule a call. </p>
              </div>
            </div>
          </div> 
          
          
           <div class="panel panel-default panel-help">
             <a href="#help-five-8" data-toggle="collapse" data-parent="#help-accordion-2">
              <div class="panel-heading">
                <h2>What is the difference between Standard Minutes and Group minutes?</h2>
              </div>
            </a>
            <div id="help-five-8" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p><strong>Standard minutes</strong> are regular grpTalk minutes, which will be consumed according to the duration of the call and the number of participants. For instance, if you host a grpTalk with 4 members for 5 minutes 4 members x 5 minutes = 20 total minutes will be consumed.
                
                
                <br>
                <strong>Group Minutes</strong> are consumed according to the duration of the call only. For instance if you host a grpTalk with20 members for 5 minutes only 5 minutes will be consumed from your group minutes.
                </p>
              </div>
            </div>
          </div> 
          
          
        </div>
        
        
        
        
      </div>
      <div class="tab-pane" id="tab5">
        <div class="panel-group" id="help-accordion-6">      	<div class="panel panel-default panel-help">
             <a href="#help-six-3" data-toggle="collapse" data-parent="#help-accordion-6">
              <div class="panel-heading">
                <h2>What is “Dial In Only”?</h2>
              </div>
            </a>
            <div id="help-six-3" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>By enabling this feature all dial out (grpTalk server calling all the members) will be stopped, and at the time of the conference all members need to dial in/call to the grpTalk number to join the conference. No PIN is required to join the conference as all the members will be joined to the conference automatically once they call the conference number. </p>
              </div>
            </div>
          </div>   
          
           <div class="panel panel-default panel-help">
             <a href="#help-six-4" data-toggle="collapse" data-parent="#help-accordion-6">
              <div class="panel-heading">
                <h2>What is “Allow Non-Members”? </h2>
              </div>
            </a>
            <div id="help-six-4" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can give access to Non-Members (who were not added to the conference group) to join the conference by dialing to the grpTalk number and entering the PIN. PIN will be included in the “conference alert SMS”-sent to you when you schedule a conference and in “conference reminder SMS”-sent to you 30 minutes before the conference scheduled time.</p>
              </div>
            </div>
          </div>  
          
          
          <div class="panel panel-default panel-help">
             <a href="#help-six-5" data-toggle="collapse" data-parent="#help-accordion-6">
              <div class="panel-heading">
                <h2>Can I select “Dial In Only” and “Allow Non-Members” features together?</h2>
              </div>
            </a>
            <div id="help-six-5" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>Yes you can! When you do so- <br> <br>
                •	All the members (who were added to the conference group) can join the conference by dialing to the grpTalk number without any PIN.<br> <br>
•	All the Non-Members (who were not added to the conference group) can join the conference by dialing to the grpTalk number and entering provided PIN for authentication.

                </p>
              </div>
            </div>
          </div> 
          
        <div class="panel panel-default panel-help">
             <a href="#help-six-6" data-toggle="collapse" data-parent="#help-accordion-6">
              <div class="panel-heading">
                <h2>Why is a PIN required for a Non-Member to join the conference? Can’t he/she join the conference without a PIN?</h2>
              </div>
            </a>
            <div id="help-six-6" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>No, he/she can’t! We use PIN to verify the Non-Member’s authentication to join the conference, and we also use it to recognize the conference they need to join.  </p>
              </div>
            </div>
          </div>
           
           <div class="panel panel-default panel-help">
             <a href="#help-six-7" data-toggle="collapse" data-parent="#help-accordion-6">
              <div class="panel-heading">
                <h2>What is “Mute Dial”?</h2>
              </div>
            </a>
            <div id="help-six-7" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>When you select this feature, all the participants (Dial In& Dial out) will be connected to the conference in MUTE except the “Host”; and whenever needed, the host can UNMUTE the whole conference or a specific participant. This feature is helpful for the conferences with more than 10 participants. <br> <br>
You can select this feature along with any advanced settings.
</p>
              </div>
            </div>
          </div> 
          
          
           <div class="panel panel-default panel-help">
             <a href="#help-six-8" data-toggle="collapse" data-parent="#help-accordion-6">
              <div class="panel-heading">
                <h2>What is “Open line before 30 minutes”?</h2>
              </div>
            </a>
            <div id="help-six-8" class="collapse" aria-expanded="false" style="height: 0px;">
              <div class="panel-body">
                <p>You can use this feature when you schedule a conference. The conference lines will be opened 30 minutes before the conference scheduled time. If you want a private chat with any selected members, you can ask them to dial in to the conference before it actually starts.
                </p>
              </div>
            </div>
          </div> 
          
          
        </div>
        
        
        
        
      </div>
	</div>    
  </div>
</div>
</div>
</header>



<!-- jQuery -->
<script src="http://code.jquery.com/jquery-2.1.4.min.js"></script>

<!-- Latest compiled and minified JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

<!-- Plugin JavaScript -->
<script src="js/jquery.easing.min.js"></script>
<script src="js/smoothscroll.js"></script>



<!-- scroll JavaScript -->
<script src="js/jquery.nicescroll.min.js"></script>

<script>
    $(document).ready(function () {
        $("#missedCallNo").text($("#hdnMissedCallNumber").val());
    });
</script>

</body>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

</asp:Content>
