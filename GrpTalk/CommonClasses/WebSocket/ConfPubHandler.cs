using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

public class ConfPubHandler : IHttpHandler

{
   
	public ConfPubHandler()
	{
		
	}
		
    public void ProcessRequest(HttpContext context)
    {
        if (context.IsWebSocketRequest)
        {
            context.AcceptWebSocketRequest(new WebSocketConfPubHandler(context.Request.ServerVariables["REMOTE_ADDR"]));
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