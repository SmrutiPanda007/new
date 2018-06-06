using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using GT.Utilities;
using PusherServer;
using System.Diagnostics;

namespace GrpTalk
{
    public partial class UserConfirmationByMissedCall : System.Web.UI.Page
    {
        public string ConStr = ConfigurationManager.ConnectionStrings["smscconferenceconnectionstring"].ToString();
        string voiceClipUrl = ConfigurationManager.AppSettings["GrpTalkVoiceClipsUrl"].ToString();
        SqlConnection SqlCon = null;
        SqlCommand SqlCmd = null;
        string FromNumber = "";
        string ToNumber = "";
        bool IsConfirmed = false;
        JObject ParametersObj;
        int PusherAppId = 0;
        string PusherAppKey = "";
        string PusherAppSecret = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            PusherAppId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PusherAppId"].ToString());
            PusherAppKey = System.Configuration.ConfigurationManager.AppSettings["PusherAppKey"].ToString();
            PusherAppSecret = System.Configuration.ConfigurationManager.AppSettings["PusherAppSecret"].ToString();
            ParseParameters();
            UserConfirm();
        }
        public void UserConfirm()
        {
            try
            {
                if (!string.IsNullOrEmpty(FromNumber.Trim()) && !string.IsNullOrEmpty(ToNumber.Trim()))
                {
                    SqlCon = new SqlConnection(ConStr);
                    SqlCmd = new SqlCommand("ConfirmUserAccount", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.CommandTimeout = 0;
                    SqlCmd.Parameters.Add("@FromNumber", SqlDbType.VarChar, 20).Value = FromNumber;
                    SqlCmd.Parameters.Add("@ToNumber", SqlDbType.VarChar, 20).Value = ToNumber;
                    SqlCmd.Parameters.Add("@IsConfirmed", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    SqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                    SqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 300).Direction = ParameterDirection.Output;
                    SqlCon.Open();
                    SqlCmd.ExecuteNonQuery();
                    SqlCon.Close();
                    if (Convert.ToInt32(SqlCmd.Parameters["@RetVal"].Value) == 1)
                    {
                        if (Convert.ToBoolean(SqlCmd.Parameters["@IsConfirmed"].Value) == true)
                        {
                            Response.Write("<Response><play>" + voiceClipUrl + "calltoverify.mp3" + "</play><Hangup/></Response>");
                            dynamic pusher_obj = new Pusher(PusherAppId.ToString(), PusherAppKey, PusherAppSecret);
                            //dynamic PusherResponse = pusher_obj.Trigger("DndOptin", "OptinCheck", new
                            //{
                            //    Mobile = FromNumber,
                            //    IsDnd = SqlCmd.Parameters["@IsDND"].Value,
                            //    OptinSent = SqlCmd.Parameters["@OptedInstructionsSent"].Value,
                            //    IsOptedOut = 1
                            //});
                        }
                        else
                        {
                            Response.Write("<Response><Hangup/></Response>");
                        }
                    }

                }
                else
                {
                    Response.Write("Parameters Missing");
                }
            }
            catch (Exception ex)
            {
                Logger.TraceLog("exception :" + ex.ToString());
                Logger.ExceptionLog(ex.ToString());
                Response.Write(ex.ToString());
            }
            finally
            {
                if (SqlCmd != null)
                {
                    try
                    {
                        SqlCmd.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                    SqlCmd = null;
                }
                if (SqlCon != null)
                {
                    try
                    {
                        SqlCon.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.ToString());
                    }
                    SqlCon = null;
                }
            }
        }

        public void ParseParameters()
        {
            if (Request.HttpMethod.ToString().ToUpper() == "GET")
            {
                if (Request["smscresponse[from]"] != null)
                {
                    FromNumber = HttpUtility.UrlDecode(Request["smscresponse[from]"].ToString());
                }
                if (Request["smscresponse[to]"] != null)
                {
                    ToNumber =HttpUtility.UrlDecode(Request["smscresponse[to]"].ToString());
                }
            }
            else if (Request.HttpMethod.ToString().ToUpper() == "POST")
            {
                string _JsonStr = "";
                Request.InputStream.Position = 0;
                _JsonStr = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
                ParametersObj = JObject.Parse(_JsonStr);
                ParametersObj = JObject.Parse(ParametersObj.SelectToken("smscresponse").ToString());
                if (ParametersObj.SelectToken("from") != null)
                {
                    FromNumber = HttpUtility.UrlDecode(ParametersObj.SelectToken("from").ToString());
                }
                if (ParametersObj.SelectToken("to") != null)
                {
                    ToNumber = HttpUtility.UrlDecode(ParametersObj.SelectToken("to").ToString());
                }
            }
            else
            {
                Response.End();
            }
            Logger.TraceLog("ParseParameters Ended");
        }

      

    }
}