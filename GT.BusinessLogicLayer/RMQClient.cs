using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using RabbitMQ.Client;
using Newtonsoft.Json.Linq;
using log4net;
using System.Configuration;
using System.Web;
using GT.Utilities;

namespace GT.BusinessLogicLayer
{
    public class RMQClient 
    {
        static ConnectionFactory factory = new ConnectionFactory
        {
            HostName = ConfigurationManager.AppSettings["RMQHost"],
            Port =Convert.ToInt32(ConfigurationManager.AppSettings["RMQPort"]),
            UserName = ConfigurationManager.AppSettings["RMQUserName"],
            Password = ConfigurationManager.AppSettings["RMQPassword"]
        };

        public static bool enQueueApiStat(string body = "")
	    {
		    IConnection connection = null;
		    IModel channel = null;
		    JObject statObj = null;
		    try {
                statObj = new JObject();
			    statObj.Add(new JProperty("StartTime", HttpContext.Current.Items["StartTime"].ToString()));
			    statObj.Add(new JProperty("AuthCompleteTime", HttpContext.Current.Items["AuthCompleteTime"].ToString()));
			    statObj.Add(new JProperty("ProcessStartTime", HttpContext.Current.Items["ProcessStartTime"].ToString()));
			    statObj.Add(new JProperty("DbStartTime", HttpContext.Current.Items["DbStartTime"].ToString()));
			    statObj.Add(new JProperty("DbEndTime", HttpContext.Current.Items["DbEndTime"].ToString()));
			    statObj.Add(new JProperty("EndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds));
			    statObj.Add(new JProperty("InBytes", HttpContext.Current.Request.TotalBytes));
			    statObj.Add(new JProperty("OutBytes", HttpContext.Current.Items["OutBytes"].ToString()));
			    statObj.Add(new JProperty("ActionName", HttpContext.Current.Items["ActionName"].ToString()));
                statObj.Add(new JProperty("AppVersion", HttpContext.Current.Items["AppVersion"].ToString()));
			    statObj.Add(new JProperty("RequestID", HttpContext.Current.Items["RequestID"].ToString()));
			    connection = factory.CreateConnection();
			    channel = connection.CreateModel();
                channel.QueueDeclare(ConfigurationManager.AppSettings["ActionStats"].ToString(), true, false, false, null);
			    dynamic properties = channel.CreateBasicProperties();
			    properties.DeliveryMode = 2;
			    channel.BasicPublish("", "ApiStats", properties, Encoding.UTF8.GetBytes(statObj.ToString()));
		    } catch (Exception ex) {
                Logger.TraceLog("Error while publishing " + ex.ToString());
			    return false;
		    } finally {
			    try {
				    if (channel != null)
					    channel.Close();
				    if (connection != null)
					    connection.Close();

			    } catch (Exception ex) {
			    }
		    }
		    return true;
	    }
    }
}
