var UIBlockUI=function(){var n=function(){$("#blockui_sample_1_1").click(function(){Metronic.blockUI({target:"#blockui_sample_1_portlet_body"});window.setTimeout(function(){Metronic.unblockUI("#blockui_sample_1_portlet_body")},2e3)});$("#blockui_sample_1_2").click(function(){Metronic.blockUI({target:"#blockui_sample_1_portlet_body",boxed:!0});window.setTimeout(function(){Metronic.unblockUI("#blockui_sample_1_portlet_body")},2e3)})},t=function(){$("#blockui_sample_2_1").click(function(){Metronic.blockUI();window.setTimeout(function(){Metronic.unblockUI()},2e3)});$("#blockui_sample_2_2").click(function(){Metronic.blockUI({boxed:!0});window.setTimeout(function(){Metronic.unblockUI()},2e3)});$("#blockui_sample_2_3").click(function(){Metronic.startPageLoading("Please wait...");window.setTimeout(function(){Metronic.stopPageLoading()},2e3)})},i=function(){$("#blockui_sample_3_1_0").click(function(){Metronic.blockUI({target:"#basic",overlayColor:"none",cenrerY:!0,boxed:!0});window.setTimeout(function(){Metronic.unblockUI("#basic")},2e3)});$("#blockui_sample_3_1").click(function(){Metronic.blockUI({target:"#blockui_sample_3_1_element",overlayColor:"none",boxed:!0})});$("#blockui_sample_3_1_1").click(function(){Metronic.unblockUI("#blockui_sample_3_1_element")});$("#blockui_sample_3_2").click(function(){Metronic.blockUI({target:"#blockui_sample_3_2_element",boxed:!0})});$("#blockui_sample_3_2_1").click(function(){Metronic.unblockUI("#blockui_sample_3_2_element")})},r=function(){$("#blockui_sample_4_1").click(function(){Metronic.blockUI({target:"#blockui_sample_4_portlet_body",boxed:!0,message:"Processing..."});window.setTimeout(function(){Metronic.unblockUI("#blockui_sample_4_portlet_body")},2e3)});$("#blockui_sample_4_2").click(function(){Metronic.blockUI({target:"#blockui_sample_4_portlet_body",iconOnly:!0});window.setTimeout(function(){Metronic.unblockUI("#blockui_sample_4_portlet_body")},2e3)});$("#blockui_sample_4_3").click(function(){Metronic.blockUI({target:"#blockui_sample_4_portlet_body",boxed:!0,textOnly:!0});window.setTimeout(function(){Metronic.unblockUI("#blockui_sample_4_portlet_body")},2e3)})};return{init:function(){n();t();i();r()}}}()