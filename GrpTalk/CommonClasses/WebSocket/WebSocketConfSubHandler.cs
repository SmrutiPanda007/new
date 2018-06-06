using System;
using System.Collections.Concurrent;
using System.Web;
using Microsoft.Web.WebSockets;
using log4net;

public class WebSocketConfSubHandler : WebSocketHandler
{
    public static ConcurrentDictionary<String, PubSubStateElement> channels = new ConcurrentDictionary<String, PubSubStateElement>();
    private String channelName;
    private static ILog logger = log4net.LogManager.GetLogger("ExceptionLogs");
	private String remoteHost;
    private String remotePort;


    public WebSocketConfSubHandler(String room,String host, String port )
    {
        channelName = room;
        remoteHost = host;
        remotePort = port;
    }

    public override void OnOpen()
    {
        PubSubStateElement roomState;
        if (!channels.TryGetValue(channelName, out roomState))
        {
            roomState = channels.GetOrAdd(channelName, new PubSubStateElement());
        }
        Boolean substate = roomState.AddSubscriber(this);
		String sendLog = "{\"Action\" : \"open\",\"host\":\" " + remoteHost + "\",\"port\": " + remotePort + ", \"subscribe\":" + (substate ? "true" : "false") + ",\"subscount\":" + roomState.GetSubscribersCount() + "}";
		logger.Info("OnOpen " + channelName + "-->" + sendLog);
         this.Send(sendLog);
    }

    public override void OnMessage(string message)
    {

    }
	 public override void OnError()
    {
		String sendLog = "{\"Action\" : \"close\",\"host\":\" " + remoteHost + "\",\"port\": " + remotePort + "}";
        logger.Info("OnError " + channelName + "-->" + sendLog);
	}
	
    public override void OnClose()
    {
        PubSubStateElement roomState;
        if (channels.TryGetValue(channelName, out roomState))
        {
            roomState.RemoveSubscriber(this);
        }
        String sendLog = "{\"Action\" : \"close\",\"host\":\" " + remoteHost + "\",\"port\": " + remotePort + ",\"subscount\":" + roomState.GetSubscribersCount() + "}";
        logger.Info("OnClose " + channelName + "-->" + sendLog);

    }
}