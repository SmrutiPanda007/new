using System;
using System.Web;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using log4net;

public class WsConfSubscriberHandler : IHttpHandler
{
    private static ILog logger = log4net.LogManager.GetLogger("WSConferenceLogger");
    public static ConcurrentDictionary<String, WsConfPubSubState> channels = new ConcurrentDictionary<String, WsConfPubSubState>();
    private String channelName;
    private String remoteHost;
    private String remotePort;

    public void ProcessRequest(HttpContext context)
    {
        if (context.IsWebSocketRequest)
        {
            channelName = context.Request.QueryString["room"];
            remoteHost = context.Request.ServerVariables["REMOTE_ADDR"];
            remotePort = context.Request.ServerVariables["REMOTE_PORT"];
            context.AcceptWebSocketRequest(HandleWebSocket);
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }

    private async Task HandleWebSocket(WebSocketContext wsContext)
    {
        const int maxMessageSize = 1024;
        byte[] receiveBuffer = new byte[maxMessageSize];
        WebSocket subSocket = wsContext.WebSocket;
        WsConfSubscriber subscriber = new WsConfSubscriber(subSocket);
        await OnOpen(subscriber);

        while (subSocket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult receiveResult = null;
            try
            {
                receiveResult = await subSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);
            }
            catch (WebSocketException wse)
            {
                OnClose(subscriber, WebSocketCloseStatus.InvalidMessageType);
                logger.Error("Error at ReceiveAsync", wse);
				break;
            }

            if (receiveResult.MessageType == WebSocketMessageType.Close)
            {
                OnClose(subscriber, receiveResult.CloseStatus.Value);
                await subSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            }
            else if (receiveResult.MessageType == WebSocketMessageType.Binary)
            {
                OnClose(subscriber, WebSocketCloseStatus.InvalidMessageType);
                await subSocket.CloseAsync(WebSocketCloseStatus.InvalidMessageType, "Binary frame not allowed", CancellationToken.None);
            }
            else
            {
                int count = receiveResult.Count;
                while (receiveResult.EndOfMessage == false)
                {
                    if (count >= maxMessageSize)
                    {
                        string closeMessage = string.Format("Max message size: {0} bytes.", maxMessageSize);
                        OnClose(subscriber, WebSocketCloseStatus.MessageTooBig);
                        await subSocket.CloseAsync(WebSocketCloseStatus.MessageTooBig, closeMessage, CancellationToken.None);
                        return;
                    }
                    receiveResult = await subSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer, count, maxMessageSize - count), CancellationToken.None);
                    if (receiveResult.Count == 0)
                    {
                        logger.Info("onReceiveAsync " + channelName + "--> BytesCount:0");
                    }
                    count += receiveResult.Count;
                }
                //var receivedString = Encoding.UTF8.GetString(receiveBuffer, 0, count);
            }
        }
        String sendLog = "{\"Action\" : \"close\",\"host\":\" " + remoteHost + "\",\"port\": " + remotePort + "}";
        logger.Info("outOfWhile " + channelName + "-->" + sendLog);
    }

    public async Task OnOpen(WsConfSubscriber subscriber)
    {
        WsConfPubSubState confState;
        Boolean substate = false;
        if (!channels.TryGetValue(channelName, out confState))
        {
            confState = channels.GetOrAdd(channelName, new WsConfPubSubState());
        }
        substate = confState.AddSubscriber(subscriber);
        String sendLog = "{\"Action\" : \"open\",\"host\":\" "
            + remoteHost + "\",\"port\": "
            + remotePort + ", \"subscribe\":"
            + (substate ? "true" : "false")
            + ",\"subscount\":" + confState.GetSubscribersCount() + "}";
        ArraySegment<byte> welcomeBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(sendLog));
        Boolean gotSignal = false;
        try
        {
            gotSignal = subscriber.autoREvent.WaitOne();
            await subscriber.subSocket.SendAsync(welcomeBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }
        finally
        {
            if (gotSignal) subscriber.autoREvent.Set();
        }
        logger.Info("OnOpen " + channelName + "-->" + sendLog);
    }

    public void OnClose(WsConfSubscriber subsciber, WebSocketCloseStatus status)
    {
        WsConfPubSubState confState;
        if (channels.TryGetValue(channelName, out confState))
        {
            confState.RemoveSubscriber(subsciber);
        }
        String sendLog = "{\"Action\" : \"close\",\"host\":\" " + remoteHost + "\",\"port\": " + remotePort + ",\"subscount\":" + confState.GetSubscribersCount() + ",\"CloseStatus\": " + status + "}";
        logger.Info("OnClose " + channelName + "-->" + sendLog);

    }
    public bool IsReusable
    {
        get { return false; }
    }

}