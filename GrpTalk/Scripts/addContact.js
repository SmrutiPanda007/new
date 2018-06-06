var mygrpTalks =0;var nonMemberExisted=0;
  $(document).on('click', '#saveContact', function (e) {
	  newList = $('#newWebList').val();
	  if(newList.length!=0)
	  {
		  if($('#ddlWebList option').filter(function () { return $(this).html().toLowerCase() == newList.toLowerCase(); }).text()!="")
		   {
			   Notifier('This Web list name is already existed', 2);
			   return;
		   }
	   }
        e.preventDefault();

        var name = $('#name').val();
        var mobileNumber = $('#mobileNumber').val();
		listId = $('#ddlWebList').val();
		
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
			if(parseInt(listId) == 0 && $.trim(newList) =='')
			{
					IsValid= false
					$('#errorDescForWebList').html("You must add this contact to an existing weblist or create a new list");
			}
			else{
				$('#errorDescForWebList').html('');
			}
	

        if (!IsValid) {
            return false;
        }

        if (countryID == 108) {
            if (mobileNumber.length == 10) { mobileNumber = "91" + mobileNumber; }
        }
        if (countryID == 241) { mobileNumber = "1" + mobileNumber; }
			if(mygrpTalks ==1)
			{
				$('.addtogroup').attr("name",name);
			}
		if(parseInt(listId) != 0)
		{
				if(mygrpTalks ==1)
			{
				$('.addtogroup').attr("listId",listId);
				//$('.addtophonebook').hide();
			}
			manageWebContacts(1, 0, listId, name, mobileNumber,nonMemberExisted);
		}
		else{
		
			newListAddContact(1, 0, newList, name, mobileNumber);
		}
		if(mygrpTalks ==1)
			{
			for(var i=0;i<jsonParticipants.Groups.length;i++)
		{
	     if (jsonParticipants.Groups[i].GroupID == grpID) {
          var array={"GroupId":grpID,"Mobile":mobileNumber,"Name":name};
          jsonParticipants.Groups[i].Participants.push(array);
		  $("#mem").html(jsonParticipants.Groups[i].Participants.length + " Member");
		  break;
	      }
         }
			}

    });

	
    //--------------------------------------Validation while focusing on textbox
    $('body').on('focusout', '#name', function (e) {
		if(e.relatedTarget.id=="contactClose")
		{
			$('.close').click();
			return false;
		}
        var name = $('#name').val();
        if (name == "" || name == null) {
            $('#name').closest('.form-group').addClass('has-error').removeClass('has-success')
            $('#errorDescForName').html('Please Enter Name');
        }
        else {
            $('#name').closest('.form-group').removeClass('has-error').addClass('has-success')
            $('#errorDescForName').html('');
        }
    });
	 $('body').on('focusout', '#newWebList', function (e) {
		 if(e.relatedTarget.id=="contactClose")
		{
			$('.close').click();
			return false;
		}
		 if(parseInt($('#ddlWebList').val())==0)
		 {
			var name = $('#newWebList').val();
			if (name == "" || name == null) {
				
				$('#errorDescForWebList').html('You must add this contact to an existing weblist or create a new list');
			}
			else {
				
				$('#errorDescForWebList').html('');
			}
		}
    });

    //--------------------------------------Validation while focusing on textbox
    $('body').on('focusout', '#mobileNumber', function (e) {
		if(e.relatedTarget.id=="contactClose")
		{
			$('.close').click();
			return false;
		}
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
function addToGroup(mobNumb,name,id)
{
	var confId = $('.list.active').attr("id");
	// var mobNumb= $(this).attr("number");
	// var name=$(this).attr("name");
	//var id=$(this).attr("listId");
	if($.trim(name)=="")
	{name = mobNumb; }
	   participants = '{"' + name + '":"' + mobNumb + '","ListId":"' + id + '"}'  + ",";
		resStr = '"Participants":[' + participants.replace(/,(?=[^,]*$)/, '') + " ]";
		var response='{"ConferenceID":"' + confId + '",'+resStr+',"WebListIds":""}';
	  // var x = confirm("Do you want to add this contact to '"+grpName+"'");
        // if (x == true) {
			$.ajax({
				url: '/HandlersWeb/Groups.ashx',
				method: 'POST',
				dataType: 'JSON',
				data: {
					type: 8,
					paramObj: response
				},
				success: function (result) {
					if (result.Success == false) {
						Notifier(result.Message, 2);
						return;
					}
					else {
						if(result.AddedParticipants.length>0)
					   Notifier("Contact Added to group Successfully",1);
					}
					$(this).hide();
				},
				error: function (result) {
					alert("Something Went Wrong");
				}
			});
	
	
	
}