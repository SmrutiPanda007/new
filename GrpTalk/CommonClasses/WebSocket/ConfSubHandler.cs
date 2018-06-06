using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Web.WebSockets;

public class ConfSubHandler : IHttpHandler
{
	public ConfSubHandler()
	{
		
	}
    public void ProcessRequest(HttpContext context)
    {
        if (context.IsWebSocketRequest)
        {
            context.AcceptWebSocketRequest(new WebSocketConfSubHandler(context.Request.QueryString["room"], context.Request.ServerVariables["REMOTE_ADDR"], context.Request.ServerVariables["REMOTE_PORT"]));
        }
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}