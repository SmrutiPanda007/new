using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System;
using PushSharp;
using PushSharp.Apple;
using PushSharp.Core;
using System.Web;
using Newtonsoft.Json.Linq;
using GT.Utilities;
using System.Web.Script.Serialization;

namespace GT.BusinessLogicLayer
{
    public class NotificationPush
    {
        public string sendAndroidPush(string Message, string DeviceToken)
        {
            string str = "";
            try
            {
                str = AndroidPush(DeviceToken, Message);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
            }
            return str;
        }
        public string AndroidPush(string DeviceToken, string Message)
        {

            try
            {
                string regId = DeviceToken;
                string applicationID = "AIzaSyBfX67bZ_F0NWSqDYOelVfXoz8pYiXhIcY";
                string SENDER_ID = "678288961581";
                WebRequest tRequest = default(WebRequest);
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                Message = System.Web.HttpUtility.UrlEncode(Message);
                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
                    + Message + "&registration_id=" + DeviceToken;

                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                string sResponseFromServer = tReader.ReadToEnd();
                tReader.Close();
                dataStream.Close();
                tResponse.Close();

                return sResponseFromServer;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.StackTrace);
                return "Exception";
            }

        }

        public void IOSpush(string deviceToken, string message)
        {
            Logger.TraceLog("deviceToken : " + deviceToken);
            byte[] appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/Certificate/Certificates.p12")));
            ApnsConfiguration config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, appleCert, "", true);
            var broker = new ApnsServiceBroker(config);
            broker.OnNotificationFailed += (notification, aggregateEx) =>
            {
                aggregateEx.Handle(ex =>
                {
                    if (ex is ApnsNotificationException)
                    {
                        var notificationException = ex as ApnsNotificationException;
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;
                        Logger.TraceLog("Tracing");
                        Logger.TraceLog(ex.ToString());
                        Logger.TraceLog("Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                        Logger.TraceLog(ex.ToString());
                    }
                    else
                    {
                        Logger.TraceLog("Notification Failed for some (Unknown Reason) : {ex.InnerException}");
                    }
                    return true;
                });
            };

            broker.OnNotificationSucceeded += (notification) =>
            {
                Logger.TraceLog("Notification Sent!");
            };
            broker.Start();
            broker.QueueNotification(new ApnsNotification
            {
                DeviceToken = deviceToken,
                Payload = JObject.Parse("{\"aps\":{\"badge\":1, \"alert\":\"" + message + "\"}}")
            });
            broker.Stop();

        }

        public void FCMPushNotifications(string message, string deviceTokens, int osID)
        {
            string serverKey = "AIzaSyDBUeAvks05iy7K_fH8jTOLxP9QPz_FVCw";
            string senderId = "657012326161";
            var data = new Object();
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                if (osID == 2)
                {
                    data = new
                    {

                        to = deviceTokens,
                        data = new
                        {
                            message = message,
                            key = "2", //1 For grpTalk Messages and 2 For Promotional Messages

                        },
                        notification = new
                        {
                            body = message,
                            title = "grpTalk"
                        }

                    };
                }
                else if (osID == 1)
                {
                    data = new
                    {

                        to = deviceTokens,
                        notification = new
                        {

                            key = "2", //1 For grpTalk Messages and 2 For Promotional Messages
                            // title = "Test push notification",
                            body = message

                        }
                    };
                }
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
                Logger.ExceptionLog("Exception in FCMPushNotifications " + ex.ToString());
            }



        }
    }
}
