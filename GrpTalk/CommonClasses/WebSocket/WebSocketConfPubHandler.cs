using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;
using Newtonsoft.Json.Linq;
using System.Threading;

public class WebSocketConfPubHandler : WebSocketHandler
{
    private String ipaddr;

    public WebSocketConfPubHandler(String _ipaddr)
    {
        ipaddr = _ipaddr;
    }

    public override void OnOpen()
    {

    }

    public override void OnMessage(string message)
    {
        try
        {
            JObject jobj = JObject.Parse(message);
            JToken room;
            if (jobj.TryGetValue("room", out room))
            {
                PubSubStateElement roomState;
                if (!WebSocketConfSubHandler.channels.TryGetValue(room.ToString(), out roomState))
                {
                    roomState = WebSocketConfSubHandler.channels.GetOrAdd(room.ToString(), new PubSubStateElement());
                }
                String action = jobj.Property("action") != null ? jobj.Property("action").ToObject<String>() : null;
                if (action != null)
                {
                    if (action == "enter") //from ycomoutbound
                    {
                        roomState.IncrementInProgressCount();
                        if (!jobj.Property("mute").ToObject<Boolean>())
                        {
                            roomState.IncrementUnMuteCount();
                        }
                    }
                    else if (action == "exit") //from YcomRest
                    {
                        roomState.DecrementInProgressCount();

                    }
                    else if (action == "egress") //from ycomoutbound
                    {
                        if (!jobj.Property("mute").ToObject<Boolean>())
                        {
                            roomState.DecrementUnMuteCount();
                        }
                        JArray digits = (JArray)jobj.Property("digits").Value;
                        foreach (JToken digit in digits)
                        {
                            if (digit.ToObject<String>() == "0")
                            {
                                roomState.DecrementHandRiseCount();
                                break;
                            }
                        }
                    }
                    else if (action == "digits-match" && jobj.Property("digit").ToObject<String>() == "0")//from ycomoutbound
                    {
                        roomState.IncrementHandRiseCount();
                    }
                    else if (action == "unmute")
                    {
                        roomState.IncrementUnMuteCount();
                        if (jobj.Property("handrise").ToObject<Boolean>())
                        {
                            roomState.DecrementHandRiseCount();
                        }
                    }
                    else if (action == "mute")
                    {
                        roomState.DecrementUnMuteCount();
                    }

                    jobj.Add(new JProperty("count", roomState.GetInProgressCount()));
                    jobj.Add(new JProperty("handrise-count", roomState.GetHandRiseCount()));
                    jobj.Add(new JProperty("unmute-count", roomState.GetUnMuteCount()));
                    message = jobj.ToString();
                }
                roomState.BroadcastToSubscribers(message);
                this.Send(jobj.GetValue("requestId") + " subscribers count" + roomState.GetSubscribersCount() + " " + room.ToString());

                //}
                //else
                //{
                //   this.Send(jobj.GetValue("requestId") + " subscribers count 0 " + room.ToString());
                //}
            }
        }
        catch (Exception e)
        {
            this.Send("Exception :" + e.ToString());
        }
    }

    public override void OnClose()
    {

    }
}