var UIConfirmations=function(){var n=function(){$("#bs_confirmation_demo_1").on("confirmed.bs.confirmation",function(){alert("You confirmed action #1")});$("#bs_confirmation_demo_1").on("canceled.bs.confirmation",function(){alert("You canceled action #1")});$("#bs_confirmation_demo_2").on("confirmed.bs.confirmation",function(){alert("You confirmed action #2")});$("#bs_confirmation_demo_2").on("canceled.bs.confirmation",function(){alert("You canceled action #2")})};return{init:function(){n()}}}()