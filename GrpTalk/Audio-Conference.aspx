<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Audio-Conference.aspx.cs" Inherits="GrpTalk.Audio_Conference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="css_body" runat="server"></asp:Content>    

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <div class="row">
    
        </div>
    <div class="col-sm-6 wow fadeInLeft text-left">
            <!--<img class="img-responsive margin-top-40" src="images/Inp1.png" alt="" width="270">-->
            <div class="media service-box wow fadeInRight">
                <div class="pull-left">
                    <i class="fa" id="call"></i>
                </div>
                <div class="media-body" style="height: 87px;">
                    <h4 class="media-heading">Crystal Clear Call Quality</h4>
                    <ul>
                        <li>Traditional landline calls, so no call drops</li>
                    </ul>
                </div>
            </div>

            <div class="media service-box wow fadeInRight">
                <div class="pull-left">
                    <i class="fa fa-calendar"></i>
                </div>
                <div class="media-body" style="height: 87px;">
                    <h4 class="media-heading">Plan your group calls</h4>
                    <ul>
                        <li>setup an instant call or schedule for later</li>
                    </ul>
                </div>
            </div>

            <div class="media service-box wow fadeInRight">
                <div class="pull-left">
                    <i class="glyphicon glyphicon-record"></i>
                </div>
                <div class="media-body" style="height: 87px;">
                    <h4 class="media-heading">Call Recording</h4>
                    <ul>
                        <li>recording enabled and disabled on the fly</li>
                    </ul>
                </div>
            </div>

        </div>
    <div class="col-sm-6">
            <div class="media service-box wow fadeInRight">
                <div class="pull-left">
                    <i class="fa large material-icons">dialpad</i>
                </div>
                <div class="media-body">
                    <h4 class="media-heading">No PINs required</h4>
                    <ul>
                        <li>all grp members receive a call</li>
                        <li>receivers need not install the app</li>
                    </ul>

                </div>
            </div>

            <div class="media service-box wow fadeInRight">
                <div class="pull-left">
                    <i class="fa fa-user"></i>
                </div>
                <div class="media-body">
                    <h4 class="media-heading">Who you're talking to</h4>
                    <ul>
                        <li>know who's on the call</li>
                        <li>see pics and social profiles</li>
                    </ul>

                </div>
            </div>

            <div class="media service-box wow fadeInRight">
                <div class="pull-left">
                    <i class="fa fa-microphone-slash"></i>
                </div>
                <div class="media-body">
                    <h4 class="media-heading">Control your calls</h4>
                    <ul>
                        <li>mute members on the fly</li>
                        <li>add new members in call</li>
                    </ul>

                </div>
            </div>
        </div>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="reviewSection" runat="server">
     <section id="reviews">
        <div class="container">
            <div class="section-header">
                <h2 class="section-title text-center wow fadeInDown">Reviews</h2>
            </div>

            <div class="row">
                <div class="list_carousel responsive">
                    <ul id="foo4">
                    </ul>
                    <div class="clearfix"></div>
                </div>
            </div>
            </div>
    </section>
</asp:Content>--%>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        $(document).ready(function (e) {

            $('.section-header').show();

            //review();
            //	Responsive layout, resizing the items
            $('#foo4').carouFredSel({
                responsive: true,
                width: '100%',
                cookie: false,
                scroll: 1,
                items: {
                    width: 400,
                    //	height: '30%',	//	optionally resize item-height
                    visible: {
                        min: 1,
                        max: 3
                    }
                }
            });
        });


        //function review() {
        //    $.ajax({
        //        url: "https://itunes.apple.com/in/rss/customerreviews/id=1074172134/sortBy=mostRecent/json",
        //        type: "GET",
        //        dataType: "json",
        //        async: false,
        //        success: function (res) {
        //            console.log(res.feed);
        //            var len = res.feed.entry.length;
        //            var res_div = "";
        //            for (var i = 1; i < len; i++) {
        //                if (res.feed.entry[i]["im:rating"].label >= 4) {
        //                    res_div += '<li><div class="media service-box wow fadeInRight"><div class="pull-left"><i class="fa fa-user"></i> </div>';
        //                    res_div += '<div class="media-body"><h4 class="media-heading">' + res.feed.entry[i].title.label + '</h4><p>' + res.feed.entry[i].author.name.label + '</p><span class="stars">' + res.feed.entry[i]["im:rating"].label + '</span><p> ' + res.feed.entry[i].content.label + '</p></div></div></li>';
        //                }

        //            }

        //            $('#foo4').html(res_div);

        //        },
        //        error: function (err) {
        //            alert("Error");
        //        }
        //    });
        //}

    </script>
    <%--<script type="text/javascript">
        $.fn.stars = function () {
            return $(this).each(function () {
                // Get the value
                var val = parseFloat($(this).html());
                // Make sure that the value is in 0 - 5 range, multiply to get width
                var size = Math.max(0, (Math.min(5, val))) * 16;
                // Create stars holder
                var $span = $('<span />').width(size);
                // Replace the numerical value with stars
                $(this).html($span);
            });
        }
        $(function () {
            $('span.stars').stars();
        });

    </script>--%>
</asp:Content>
