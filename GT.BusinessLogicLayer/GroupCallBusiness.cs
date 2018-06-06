using System;
using System.Collections;
using System.Web;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT.DataAccessLayer;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using GT.Utilities;
using GT.Utilities.Properties;
using Microsoft.VisualBasic.Devices;
using PusherServer;
using PushSharp;

namespace GT.BusinessLogicLayer
{
    static class Extensions
    {
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
        public static string Left(this string value, int length)
        {
            return value.Substring(0, Math.Min(length, value.Length));
         
        }
    }

    public class GroupCallBusiness
    {
        String ClipDownloadedPath = "";
        JObject ConferenceValidateObject = new JObject();
        String _BulkDialDelimiter = "@";
        String _ConferenceCallBackUrl = System.Configuration.ConfigurationManager.AppSettings["ConferenceCallBackUrl"].ToString();
        String pushtonum="";
        String _ExtraDialString  = "";
        public JObject ConferenceRecording(string sConnString, int ConferenceId, int Action)
        {
            DataSet ds = new DataSet();
            short retval = 0;
            string retmsg = "";

            JObject ResObj = new JObject();
            string HttpUrl = "";
            string FilePath = "";
            string FileName = "";
            string ConferenceName = "";
            string FileFormat = "";
            string ConferenceAction = "";
            string RecordedFile = "";
            bool status;
            string success = "";
            string _postdata = "";
            string File = "";
            string ResourceUrl = "";
            JObject RecordResponse = null;
            int UserId = 0; 
            GroupCallEntity obj = new GroupCallEntity(sConnString);
            ds = obj.ConferenceRecording(ConferenceId, Action, out retval, out retmsg, out UserId, out RecordedFile);
            if (retval == 1)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows.Count) > 0)
                {

                    ConferenceName = ds.Tables[0].Rows[0]["ConferenceName"].ToString();
                    HttpUrl = ds.Tables[0].Rows[0]["HttpUrl"].ToString();
                    FileFormat = ds.Tables[0].Rows[0]["FileFormat"].ToString();
                    FilePath = ds.Tables[0].Rows[0]["FilePath"].ToString();
                    FileName = ds.Tables[0].Rows[0]["FileName"].ToString();
                    ResourceUrl = ds.Tables[0].Rows[0]["ResourceUrl"].ToString();
                    if (Action == 1)
                    {
                        ConferenceAction = "ConferenceRecordStart/";
                        _postdata = "ConferenceName=" + ConferenceName.ToString() + "&FileFormat=" + FileFormat.ToString() + "&FilePath=" + FilePath.ToString() + "&FileName=" + FileName.ToString();
                    }
                    else
                    {
                        ConferenceAction = "ConferenceRecordStop/";
                        if (string.IsNullOrEmpty(RecordedFile))
                        {

                            logclass.LogRequest("ed conference1");
                        }
                        else
                        {
                            RecordedFile = "";
                            logclass.LogRequest("ed conference222");
                            StopConferenceRecord(sConnString, ConferenceId);
                            RecordResponse.Add(new JProperty("Success", false));
                            RecordResponse.Add(new JProperty("Message", "Conference already stoped"));
                            return RecordResponse;
                        }
                        _postdata = "ConferenceName=" + ConferenceName.ToString() + "&RecordFile=" + RecordedFile.ToString();
                        logclass.LogRequest("COnference------" + _postdata);
                    }

                    WebRequest _Req = null;
                    HttpWebResponse _Resp = null;
                    _Req = HttpWebRequest.Create(HttpUrl.ToString() + ConferenceAction);

                    string ResponseString = null;
                    _Req.Method = "POST";
                    StreamWriter Swriter = null;
                    StreamReader Sreader = null;


                    _Req.ContentType = "application/x-www-form-urlencoded";
                    Swriter = new StreamWriter(_Req.GetRequestStream());
                    Swriter.Write(_postdata.Trim());
                    Swriter.Flush();
                    Swriter.Close();
                    _Resp = (HttpWebResponse)_Req.GetResponse();
                    Sreader = new StreamReader(_Resp.GetResponseStream());
                    ResponseString = Sreader.ReadToEnd();

                    ResObj = JObject.Parse(ResponseString);
                    logclass.LogRequest("Reponse------" + Convert.ToString(ResObj));
                    status = Convert.ToBoolean(ResObj.SelectToken("Success"));

                    if (status == true)
                    {
                        success = "success";
                    }
                    if (Action == 2)
                    {
                        ResourceUrl = ds.Tables[0].Rows[0]["ResourceUrl"].ToString();
                        DownloadRecordedClip(ConferenceId, RecordedFile, UserId, ResourceUrl);
                    }

                }
                RecordResponse.Add(new JProperty("Success", true));
                RecordResponse.Add(new JProperty("file", ResObj.SelectToken("RecordFile")));

            }
            else
            {
                RecordResponse.Add(new JProperty("Success", false));
                RecordResponse.Add(new JProperty("Message", retmsg));
            }
            return RecordResponse;
        }
        public void DownloadRecordedClip(int ConferenceId, string _RecordedFile, int user_id, string _ResourceUrl)
        {
            string FileName = "";
            string FilePath = "";
            string HttpURL = "";
            try
            {
                FileName = _RecordedFile.Split('/').Last();
                logclass.LogRequest("Download File--" + FileName + user_id.ToString());
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("ConferenceRecordings/" + user_id.ToString())))
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/ConferenceRecordings/" + user_id.ToString()));
                }
                ClipDownloadedPath = System.Configuration.ConfigurationManager.AppSettings["ClipDownloadedPath"].ToString();
                Computer myComputer = new Computer();
                myComputer.Network.DownloadFile(_ResourceUrl + FileName, ClipDownloadedPath + user_id.ToString() + "\\" + FileName, "", "");

            }
            catch (Exception ex)
            {
                logclass.LogRequest("excepion At Download File--" + ex.ToString());
            }
        }
        public void StopConferenceRecord(string sConnString, int ConferenceId)
        {
            string _RecordedFile = "";
            string FilePath = "";
            string DownloadUrl = "";
            int Conf_UserId = 0;
            int isend;
            short retval;
            string retmsg = "";
            string rs = "";
            try
            {
                GroupCallEntity obj = new GroupCallEntity(sConnString);
                rs = obj.StopConferenceRecord(ConferenceId, out isend, out Conf_UserId, out FilePath, out DownloadUrl, out retval, out retmsg);
                if (retval == 1)
                {
                    if (isend == 1)
                    {
                        //Conference.ConferenceRecording(ConfId, 2)
                        logclass.LogRequest("RecordConference- Ending --" + ConferenceId.ToString());
                        _RecordedFile = FilePath.Split('/').Last();
                        ClipDownloadedPath = System.Configuration.ConfigurationManager.AppSettings["ClipDownloadedPath"].ToString();

                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("ConferenceRecordings/" + Conf_UserId.ToString())))
                        {
                            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/ConferenceRecordings/" + Conf_UserId.ToString()));
                        }
                        Computer myComputer = new Computer();
                        myComputer.Network.DownloadFile(DownloadUrl + _RecordedFile, ClipDownloadedPath + Conf_UserId.ToString() + "\\" + _RecordedFile, "", "");
                        logclass.LogRequest("RecordConference- dwn--" + ConferenceId.ToString());
                    }
                }
                else
                {
                    logclass.LogRequest("End Recording Error---" + retmsg);
                }
            }
            catch (Exception ex)
            {
                logclass.LogRequest("End Recording Exception---" + ex.ToString());
            }
        }
        public JObject Validate(string sConnString, grpcall _call)
        {
            JObject ValidateResponse = null;
            DataSet ValidateDs;
            short retval;
            string retmsg = "";
            string CallUUID = "";
            try
            {
                GroupCallEntity obj = new GroupCallEntity(sConnString);
               // ValidateDs = obj.Validate(_call, out retval, out retmsg, out CallUUID);

                if (retval == 1)
                {
                    ValidateResponse.Add(new JProperty("Success", true));

                    if (ValidateDs.Tables.Count > 0 && ValidateDs.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataColumn _Column in ValidateDs.Tables[0].Columns)
                        {
                            ValidateResponse.Add(new JProperty(_Column.ColumnName, ValidateDs.Tables[0].Rows[0][_Column.ColumnName]));
                        }
                    }
                    else
                    {
                        ValidateResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "No Data Returned From Database"));
                    }
                }
                else
                {
                    ValidateResponse = new JObject(new JProperty("Success", false), new JProperty("Message", retmsg));
                }
            }
            catch (Exception ex)
            {
                logclass.LogRequest(ex.ToString());
                ValidateResponse = new JObject(new JProperty("Success", false), new JProperty("Message", ex.Message));
            }
            finally
            {

            }
            ConferenceValidateObject = ValidateResponse;
            logclass.LogRequest("Response From Validate Method " + ConferenceValidateObject.ToString());
            return ValidateResponse;
        }
        public JObject InsertInbloundConferenceReport(string sConnString, JObject DataObj, short _Mode)
        {
            JObject InsertResponse = new JObject();
            string rs = "";
            short retval;
            string retmsg = "";
            try
            {
                GroupCallEntity obj = new GroupCallEntity(sConnString);
                rs = obj.InsertInbloundConferenceReport(DataObj, _Mode, out retval, out retmsg);

                if (retval == 1)
                {
                    InsertResponse.Add(new JProperty("Success", true));
                    InsertResponse.Add(new JProperty("Message", "OK"));
                }
                else
                {
                    InsertResponse.Add(new JProperty("Success", false));
                    InsertResponse.Add(new JProperty("Message", retmsg));
                }
            }
            catch (Exception ex)
            {
                InsertResponse.Add(new JProperty("Success", false));
                InsertResponse.Add(new JProperty("Message", ex.ToString()));
                HttpContext.Current.Response.Write(ex.ToString());
            }
            return InsertResponse;
        }
        public JObject Dial(string sConnString, grpcall grpcall)
       {
            JObject DialResp = new JObject();
            DataSet DsDial = null;
            string _Xml = "";
            JObject DialApiResponse = new JObject();
            string MobileNumbers = "";
            DataTable ReportIdMobileNumberTable = new DataTable();
            DataTable GateWayDetailsTable = new DataTable();
            DataTable ReportIdRequestUUIDTable = new DataTable();
            JArray RequestUUIdsArr = new JArray();
            string ReportIds = "";
            string PlivoApiID = "";
            char[] BulkDialDelimiter = (grpcall.BulkDialDelimiter).ToCharArray();
            ReportIdRequestUUIDTable.Columns.Add("ReportId", typeof(int));
            ReportIdRequestUUIDTable.Columns.Add("RequestUUID", typeof(string));
            JObject SuccessStatusGateways = new JObject();
            short retval;
            string retmsg = "";
            string IsinterConnect = "";
           try
           {
               GroupCallEntity obj = new GroupCallEntity(sConnString);
               DsDial = obj.Dial(grpcall, out retval, out retmsg,out IsinterConnect);
            if (retval == 1) {

            if (DsDial.Tables.Count > 0) {
                ReportIdMobileNumberTable = DsDial.Tables[0];
                GateWayDetailsTable = DsDial.Tables[1];
                for (int i = 0; i <= ReportIdMobileNumberTable.Rows.Count - 1; i++) {
                    MobileNumbers = ReportIdMobileNumberTable.Rows[i][1].ToString();
                    MobileNumbers = "@" + MobileNumbers;
                    if (Convert.ToBoolean(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["GatewayId"])[0]["isCountryPrefixAllowed"]) == false) {
                        MobileNumbers = MobileNumbers.Replace("@" + GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["GatewayId"])[0]["CountryPrefix"], "@" + GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["GatewayId"])[0]["DialPrefix"]);
                    }
                    if (Convert.ToBoolean(ReportIdMobileNumberTable.Rows[i]["IsModerator"]) == true) {
                        _Xml = GetConferenceXml(true, false,grpcall);
                    } else {
                        _Xml = GetConferenceXml(false, grpcall.IsMute,grpcall);
                    }
                    MobileNumbers = MobileNumbers.Right(MobileNumbers.Length - 1);
                    ReportIds = ReportIdMobileNumberTable.Rows[i][2].ToString();
                    grpcall.GatewayID =Convert.ToInt16(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["GatewayId"]);
                    grpcall.HttpConferenceApiUrl =Convert.ToString(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["HttpUrl"]);
                    grpcall.OriginationUrl = Convert.ToString(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["OriginationUrl"]);
                    grpcall.CallerIdNumber = Convert.ToString(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["OriginationCallerID"]);
                    grpcall.ExtraDialString = Convert.ToString(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["ExtraDialString"]);
                  
                        PlivoClientBusiness _PlivoClient = new PlivoClientBusiness();
                    if (grpcall.GatewayID != 4) {
                        DialApiResponse = DialAPI(MobileNumbers, _Xml, grpcall, sConnString,ReportIds);
                    } else {
                        if (MobileNumbers.ToString().Split(BulkDialDelimiter).Length > 1) {
                            DialApiResponse =  _PlivoClient.MakeBulkCall(grpcall.CallerIdNumber, MobileNumbers.ToString(), grpcall.BulkDialDelimiter);
                        } else {
                            DialApiResponse = _PlivoClient.MakeCall(grpcall.CallerIdNumber, MobileNumbers.ToString());
                        }
                    }
                    if (Convert.ToBoolean(DialApiResponse.SelectToken("Success").ToString()) == false) {
                        DialResp.Add(new JProperty("Success", false));
                        DialResp.Add(new JProperty("Message", "server error"));
                        return DialResp;

                    } else {
                        RequestUUIdsArr =new JArray( DialApiResponse.SelectToken("RequestUUIDs"));
                        string[] FReportIds = ReportIdMobileNumberTable.Rows[i]["ReportId"].ToString().Split('@');
                        for (int r = 0; r <= FReportIds.Length - 1; r++) {
                            ReportIdRequestUUIDTable.Rows.Add(FReportIds[r], RequestUUIdsArr[r]);
                        }
                        string apiid=DialApiResponse.SelectToken("ApiID").ToString();
                        GroupCallEntity autodialobj = new GroupCallEntity(sConnString);
                        
                           autodialobj.UpdateAutodialInfo(grpcall);
                           GroupCallEntity dialresponseobj = new GroupCallEntity(sConnString);
                           dialresponseobj.UpdateDialResponse(grpcall,apiid,ReportIdRequestUUIDTable);
                     
                       ReportIdRequestUUIDTable.Rows.Clear();
                    }
                }
                DialResp.Add(new JProperty("Success", true));
                DialResp.Add(new JProperty("Message", "OK"));
            } else {
                DialResp.Add(new JProperty("Success", false));
                DialResp.Add(new JProperty("Message", "Member Not Found"));
                return DialResp;
            }
        } else {
            DialResp.Add(new JProperty("Success", false));
            DialResp.Add(new JProperty("Message", "" + retmsg));
        }
    } catch (Exception ex) {
        logclass.LogRequest(ex.ToString());
        DialResp = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error" + ex.ToString()));
    } finally {
       
    }
    return DialResp;   
       }
        public JObject DialAPI(string MobileNumbers, string _Xml,grpcall grpcall, string sConnString,string ReportIds = null)
        {
	        WebRequest _Req = null;
	        HttpWebResponse _Resp = null;
	        StreamReader SReader = null;
	        StreamWriter SWriter = null;
	        string PostingData = "";
	        string HttpAPIResponseString = "";
	        JObject ResultObj = new JObject();
	        JObject ResponseObj = new JObject();
	        JArray RequestUUIDs = new JArray();
	        int TotalCount = 0;
	        StringBuilder OriginationUrlNew = new StringBuilder();
	        short RetryAttempt = 0;
            char[] BulkDialDelimiter = (grpcall.BulkDialDelimiter).ToCharArray();
              GroupCallEntity obj = new GroupCallEntity(sConnString);
	        try {
		        //ConnectRetry:
                if (MobileNumbers.ToString().Split(BulkDialDelimiter, StringSplitOptions.RemoveEmptyEntries).Length > 1)
                {
			        _Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "BulkCall/");
                    TotalCount = MobileNumbers.ToString().Split(BulkDialDelimiter, StringSplitOptions.RemoveEmptyEntries).Count();
		        } else {
			        _Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "Call/");
			        TotalCount = 1;
		        }
		        if (TotalCount > 1) {
			        OriginationUrlNew.Clear();

			        for (int i = 1; i <= TotalCount; i++) {
				        OriginationUrlNew.Append(grpcall.OriginationUrl + grpcall.BulkDialDelimiter);
			        }
                    OriginationUrlNew = new StringBuilder(OriginationUrlNew.ToString().Left(OriginationUrlNew.Length - 1));
		        } else {
			        OriginationUrlNew = new StringBuilder(grpcall.OriginationUrl);
		        }
		        _Req.Method = "POST";
		        _Req.ContentType = "application/x-www-form-urlencoded";
	        // _Req.KeepAlive = false;

      
		        PostingData = "";
		        if (TotalCount > 1) {
			        PostingData = "Delimiter=" + _BulkDialDelimiter + "&";
		        }
		        if (grpcall.HttpConferenceApiUrl.Contains(":8099")) {
			        _Xml = _Xml.Replace("#quote#", "\"");
		        }
		        if (grpcall.CallerIdNumber.StartsWith("971440571")) {
                    grpcall.CallerIdNumber = grpcall.CallerIdNumber.Right(4);
		        }

                PostingData = PostingData + "Priority=H&From=" + grpcall.CallerIdNumber + "&To=" + MobileNumbers.ToString() + "&AnswerUrl=" + _ConferenceCallBackUrl + "&SequenceNumbers=" + ReportIds.ToString() + "&Gateways=" + OriginationUrlNew.ToString() + "&HangupUrl=" + _ConferenceCallBackUrl + "&ExtraDialString=" + grpcall.ExtraDialString + ",answer_xml='" + _Xml + "',drb_calluuid=" + grpcall.CallUUID;
		        SWriter = new StreamWriter(_Req.GetRequestStream());
		        SWriter.Write(PostingData);
		        SWriter.Flush();
		        SWriter.Close();
		        SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
		        HttpAPIResponseString = SReader.ReadToEnd();
		        ResultObj = JObject.Parse(HttpAPIResponseString);
		        bool IsApiCallSuccess = false;
		        IsApiCallSuccess =Convert.ToBoolean(ResultObj.SelectToken("Success"));
		        ResponseObj = new JObject();
		        if (IsApiCallSuccess == true) {
			        ResponseObj.Add(new JProperty("Success", true));
			        foreach (string UUID in Convert.ToString(ResultObj.SelectToken("RequestUUID")).Replace("[", "").Replace("]", "").Replace("\"", "").Split(',')) {
				        RequestUUIDs.Add(UUID);
			        }
			        ResponseObj.Add("RequestUUIDs", RequestUUIDs);
		        } else {
			        //logclass.SimpleLog(ResultObj)
			        ResponseObj.Add(new JProperty("Success", false));
			        ResponseObj.Add(new JProperty("Message", MobileNumbers));
		        }
	        } catch (System.Net.WebException ax) {
		        logclass.LogRequest("Retring the DialApi Method");
		        RetryAttempt += 1;
		        if (RetryAttempt < 3) {
			        System.Threading.Thread.Sleep(1000);
			        //goto ConnectRetry;
		        } else {
			        logclass.LogRequest("Max Attempts Reached,ConferenceId " + grpcall.ConferenceId + ",ReportIds : " + ReportIds.ToString());
			        DataTable TableReportIds = new DataTable();
			        TableReportIds.Columns.Add("Id", typeof(int));
			        foreach (String _id in ReportIds.ToString().Split(BulkDialDelimiter)) {
				        TableReportIds.Rows.Add(_id);
			        }

            
                      obj.UpdateSystemDownReports(TableReportIds, "NODE_DOWN");
			
			        ResponseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "System Down"));
		        }
		        //DialAPI(MobileNumbers, _Xml)
	        } catch (Exception ex) {
		        logclass.LogRequest(ex.ToString());
		        logclass.LogRequest("uPDATING sYSTEM dOWN,ConferenceId " + grpcall.ConferenceId + ",ReportIds : " + ReportIds.ToString());
		        DataTable TableReportIds = new DataTable();
		        TableReportIds.Columns.Add("Id", typeof(int));
		        foreach (String _id in ReportIds.ToString().Split(BulkDialDelimiter)) {
			        TableReportIds.Rows.Add(_id);
		        }
                obj.UpdateSystemDownReports(TableReportIds, "SERVER_ERROR");
		        ResponseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error"));
	        }
	        return ResponseObj;
}
        public string GetConferenceXml(bool IsModerator, bool IsMute, grpcall grpcall)
        {
            string _Xml = "";
            if (!string.IsNullOrEmpty(grpcall.WelcomeClip))
            {
                if (grpcall.WelcomeClip == "http://yconference.com/DefaultClips/welcome.mp3")
                {
                    grpcall.WelcomeClip = "/usr/local/freeswitch/recordings/yconf_welcome_clip.mp3";
                }
            }
            if (!string.IsNullOrEmpty(grpcall.WelcomeClip))
            {
                if (grpcall.EndConferenceOnExit.ToUpper() == "TRUE")
                {
                    _Xml = "<Response><Play>" + grpcall.WelcomeClip + "</Play><Conference ";
                    if (IsMute == true)
                    {
                        _Xml = _Xml + "muted=#quote#true#quote# ";
                    }
                    _Xml = _Xml + "method=#quote#GET#quote# maxMembers=#quote#3000#quote#  callbackUrl=#quote#" + _ConferenceCallBackUrl + "#quote# endConferenceOnExit=#quote#true#quote# waitSound =#quote#" + grpcall.WaitClip + "#quote# digitsMatch=#quote#0#quote# >" + grpcall.ConferenceRoom + "</Conference></Response>";
                }
                else
                {
                    _Xml = "<Response><Play>" + grpcall.WelcomeClip + "</Play><Conference ";
                    if (IsMute == true)
                    {
                        _Xml = _Xml + "muted=#quote#true#quote# ";
                    }
                    _Xml = _Xml + "method=#quote#GET#quote# maxMembers=#quote#3000#quote#  callbackUrl=#quote#" + _ConferenceCallBackUrl + "#quote# waitSound =#quote#" + grpcall.WaitClip + "#quote# digitsMatch=#quote#0#quote# >" + grpcall.ConferenceRoom + "</Conference></Response>";
                }
            }
            else
            {
                if (grpcall.EndConferenceOnExit.ToUpper() == "TRUE")
                {
                    _Xml = "<Response><Conference ";
                    if (grpcall.IsMute == true)
                    {
                        _Xml = _Xml + "muted=#quote#true#quote# ";
                    }
                    _Xml = _Xml + "method=#quote#GET#quote# maxMembers=#quote#3000#quote#  callbackUrl=#quote#" + _ConferenceCallBackUrl + "#quote# endConferenceOnExit=#quote#true#quote# waitSound =#quote#" + grpcall.WaitClip + "#quote# digitsMatch=#quote#0#quote# >" + grpcall.ConferenceRoom + "</Conference></Response>";
                }
                else
                {
                    _Xml = "<Response><Conference ";
                    if (grpcall.IsMute == true)
                    {
                        _Xml = _Xml + "muted=#quote#true#quote# ";
                    }
                    _Xml = _Xml + "method=#quote#GET#quote# maxMembers=#quote#3000#quote#  callbackUrl=#quote#" + _ConferenceCallBackUrl + "#quote# waitSound =#quote#" + grpcall.WaitClip + "#quote# digitsMatch=#quote#0#quote# >" + grpcall.ConferenceRoom + "</Conference></Response>";
                }
            }
            return _Xml;
        }
        public JObject Hangup(string sConnString, grpcall grpcall)
{
	JObject HangupResponse = null;
    DataSet SqlDsHangup = null;
    short retval;
           string retmsg = "";
		try {
		 GroupCallEntity obj = new GroupCallEntity(sConnString);
               SqlDsHangup = obj.Hangup(grpcall, out retval, out retmsg);
		if (retval != 1) {
			HangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", retmsg));
			return HangupResponse;
		}
		if (SqlDsHangup.Tables.Count > 0) {
			if (SqlDsHangup.Tables[0].Rows.Count == 0) {
				HangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "No Members Found"));
			} else {
					for (int i = 0; i <= SqlDsHangup.Tables[0].Rows.Count - 1; i++) {
					grpcall.GatewayID =Convert.ToInt16(SqlDsHangup.Tables[1].Rows[i]["GatewayId"]);
					grpcall.HttpConferenceApiUrl =Convert.ToString(SqlDsHangup.Tables[1].Rows[i]["HttpURL"]);
					if (grpcall.GatewayID != 4) {
						HangupResponse = HangupApi(SqlDsHangup.Tables[0].Rows[i]["RequestUUID"].ToString(), SqlDsHangup.Tables[0].Rows[i]["MemeberId"].ToString(),grpcall,sConnString);
					} else {
						string _PInput = "";
                          PlivoClientBusiness _PlivoClient = new PlivoClientBusiness();
                   	if (grpcall.IsAll == true) {
							HangupResponse = _PlivoClient.KillConference(grpcall.ConferenceRoom);
						} else {
							if (!string.IsNullOrEmpty(SqlDsHangup.Tables[0].Rows[i]["MemeberId"].ToString())) {
								_PInput = GetPlivoCallUUIDs(SqlDsHangup.Tables[0].Rows[i]["MemeberId"].ToString(), 1,grpcall,sConnString);
								HangupResponse = _PlivoClient.HangupCalls(_PInput);
							}
							if (!string.IsNullOrEmpty(SqlDsHangup.Tables[0].Rows[i]["RequestUUID"].ToString())) {
								_PInput = GetPlivoCallUUIDs(SqlDsHangup.Tables[0].Rows[i]["RequestUUID"].ToString(), 0,grpcall,sConnString);
								HangupResponse = _PlivoClient.HangupCalls(_PInput);
							}
						}
					}
				}
			}
		} else {
			HangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "No Table Returned"));
		}
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		HangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error"));
	} finally {
		
	}
	return HangupResponse;
}
        public JObject HangupApi(string RequestUUIDs, string MemberIds,grpcall grpcall,String sConnString )
{
	logclass.SimpleLog("Hangup , RequestUUIDS : " + RequestUUIDs + ", MemberIds :  " + MemberIds);
	JObject HangupResponse = null;
	WebRequest _Req = null;
	HttpWebResponse _Resp = null;
	StreamReader SReader = null;
	StreamWriter SWriter = null;
	string PostingData = "";
	string HttpAPIResponseString = "";
	JObject ResponseObj = new JObject();
	try {
		if (!string.IsNullOrEmpty(MemberIds.Trim())) {
			_Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "ConferenceHangup/");
			_Req.Method = "POST";
			_Req.ContentType = "application/x-www-form-urlencoded";
			PostingData = "ConferenceName=" + grpcall.ConferenceRoom + "&MemberID=" + MemberIds;
			SWriter = new StreamWriter(_Req.GetRequestStream());
			SWriter.Write(PostingData);
			SWriter.Flush();
			SWriter.Close();
			SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
			HttpAPIResponseString = SReader.ReadToEnd();
			SReader.Close();
			ResponseObj = JObject.Parse(HttpAPIResponseString);
			logclass.LogRequest("jobj--" + ResponseObj.ToString() + "---" + grpcall.IsAll);
			if (grpcall.IsAll == true) {
				logclass.LogRequest("jobj--" + ResponseObj.ToString());
				if (Convert.ToBoolean(ResponseObj.SelectToken("Success").ToString()) == true) {
					StopConferenceRecord(sConnString,grpcall.ConferenceId);
				}
			}
		}
		if (!string.IsNullOrEmpty(RequestUUIDs.Trim())) {
			foreach (string Uid in RequestUUIDs.Split(',')) {
				try {
					_Req = WebRequest.Create(grpcall.HttpConferenceApiUrl + "HangupCall/");
					_Req.Method = "POST";
					_Req.Timeout = 10000;
					//_Req.KeepAlive = false;
					_Req.ContentType = "application/x-www-form-urlencoded";
					PostingData = "RequestUUID=" + Uid;
					SWriter = new StreamWriter(_Req.GetRequestStream());
					SWriter.Write(PostingData);
					SWriter.Flush();
					SWriter.Close();
					SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
					HttpAPIResponseString = SReader.ReadToEnd();
					SReader.Close();
				} catch (Exception ex) {
					logclass.SimpleLog(ex.ToString());
					continue;
				}
			}
		}
		HangupResponse = new JObject(new JProperty("Success", true), new JProperty("Message", "Hangup Success"));
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		HangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "Cannot hangup at this moment"));
	}
	return HangupResponse;
}
        public string GetPlivoCallUUIDs(string InPut, short _Type,grpcall grpcall,string sConnString)
{
	string _Response = "";
	 DataSet DsP=null;
	try {
		GroupCallEntity obj = new GroupCallEntity(sConnString);
               DsP = obj.GetPlivoCallUUIDs(InPut,_Type,grpcall);
		if (DsP.Tables.Count > 0 && DsP.Tables[0].Rows.Count > 0) {
			for (int i = 0; i <= DsP.Tables[0].Rows.Count - 1; i++) {
				_Response = _Response + DsP.Tables[0].Rows[i]["PlivoCallUUID"].ToString() + ",";
			}
			_Response =_Response.Left(_Response.Length - 1);
		} else {
			logclass.SimpleLog("No Data Returned From Database");
			_Response = "";
		}
	} catch (Exception ex) {
		logclass.SimpleLog("Error Getting Plivo CallUUIDs, Reason : " + ex.ToString());
		_Response = "";
	}
	return _Response;
}
        public JObject MuteUnmute(grpcall grpcall,string sConnString)
   {
	JObject Resp = new JObject();
	DataSet SqlDsMuteUnmutem = default(DataSet);
	string MemberIds = "";
     int retval;
            string retmsg="";
	try {
		GroupCallEntity obj = new GroupCallEntity(sConnString);
               SqlDsMuteUnmutem = obj.MuteUnmute(grpcall, out retval, out retmsg);
		pushtonum = grpcall.MobileNumber;
		DataTable MuteUnmuteMembersTable = new DataTable();
		DataTable MuteUnmuteGatewaysTable = new DataTable();
		if (retval != 1) {
			Resp.Add(new JProperty("Success", false));
			Resp.Add(new JProperty("Message", retmsg));
			return Resp;
		}
		MuteUnmuteMembersTable = SqlDsMuteUnmutem.Tables[0];
		MuteUnmuteGatewaysTable = SqlDsMuteUnmutem.Tables[1];
		if (MuteUnmuteMembersTable.Rows.Count > 0) {
			for (int i = 0; i <= MuteUnmuteMembersTable.Rows.Count - 1; i++) {
				MemberIds = MuteUnmuteMembersTable.Rows[i]["MemeberId"].ToString();
				grpcall.HttpConferenceApiUrl = MuteUnmuteGatewaysTable.Rows[i]["HttpURL"].ToString();
				grpcall.GatewayID =Convert.ToInt16(MuteUnmuteGatewaysTable.Rows[i]["GatewayID"]);
				if (string.IsNullOrEmpty(MemberIds.Trim())) {
					Resp.Add(new JProperty("Success", false));
					Resp.Add(new JProperty("Message", "Unable to perform action now"));
				} else {
					if (grpcall.GatewayID != 4) {
						Resp = MuteUnmuteApi(MemberIds,grpcall);
					} else {
                         PlivoClientBusiness _PlivoClient = new PlivoClientBusiness();
						if (grpcall.IsMute == true) {
							Resp = _PlivoClient.ConferenceMute(grpcall.ConferenceRoom, MemberIds);
						} else {
							Resp = _PlivoClient.ConferenceUnMute(grpcall.ConferenceRoom, MemberIds);
						}
					}
				}
			}
		}
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		Resp = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error" + ex.ToString()));
	}
	return Resp;
}
        public JObject MuteUnmuteApi(string MemberIds,grpcall grpcall)
{
	logclass.SimpleLog("MuteUnmute, MemberIds : " + MemberIds);
	logclass.LogRequest("MuteUnmute, MemberIds : " + MemberIds);
	JObject Resp = new JObject();
	WebRequest _Req = null;
	HttpWebResponse _Resp = null;
	StreamReader SReader = null;
	StreamWriter SWriter = null;
	string HttpApiResponseString = "";
	string PostingData = "";
	try {
		if (grpcall.IsMute == true) {
			_Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "ConferenceMute/");
		} else {
			_Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "ConferenceUnmute/");
		}
		logclass.LogRequest("URL " + grpcall.HttpConferenceApiUrl.ToString());
		PostingData = "ConferenceName=" + grpcall.ConferenceRoom + "&MemberID=" + MemberIds;
		logclass.LogRequest("POstingData " + PostingData.ToString());
		_Req.Method = "POST";
		//_Req.KeepAlive = false;
		_Req.ContentType = "application/x-www-form-urlencoded";
		SWriter = new StreamWriter(_Req.GetRequestStream());
		SWriter.Write(PostingData);
		SWriter.Flush();
		SWriter.Close();
		SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
		HttpApiResponseString = SReader.ReadToEnd();
		Pusher_Conference_MuteUnMuteAndDeafUnDeaf(grpcall.MemberName, HttpApiResponseString, grpcall.ConferenceRoom, grpcall.IsMute, "Mute", grpcall.IsAll);
		Resp = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		Resp = new JObject(new JProperty("Success", false), new JProperty("Message", "Cannot mute at this moment : " + ex.Message));
	}
	return Resp;
}
        public void Pusher_Conference_MuteUnMuteAndDeafUnDeaf(string _Mem, string Respons, string CRoom, bool Isaction, string caction, bool is_All)
{
	string PusherAppId = "0";
	string PusherAppKey = "";
	string PusherAppSecret = "";
	string _CAction = "";
	string _ConfAction = "";
	int _isActionAll = 0;
	_CAction = caction;
	if (is_All == true) {
		_isActionAll = 1;
	}
	if (_isActionAll == 1) {
		if (_CAction == "Mute") {
			if (Isaction == false) {
				_ConfAction = "unmute_all";
			} else {
				_ConfAction = "mute_all";
			}
		} else if (_CAction == "Deaf") {
			if (Isaction == false) {
				_ConfAction = "undeaf_all";
			} else {
				_ConfAction = "deaf_all";
			}
		}
	} else {
		if (_CAction == "Mute") {
			if (Isaction == false) {
				_ConfAction = "unmute_member";
			} else {
				_ConfAction = "mute_member";
			}
		} else if (_CAction == "Deaf") {
			if (Isaction == false) {
				_ConfAction = "undeaf_member";
			} else {
				_ConfAction = "deaf_member";
			}
		}
	}

	PusherAppId = System.Configuration.ConfigurationManager.AppSettings["PusherAppId"].ToString();
	PusherAppKey = System.Configuration.ConfigurationManager.AppSettings["PusherAppKey"].ToString();
	PusherAppSecret = System.Configuration.ConfigurationManager.AppSettings["PusherAppSecret"].ToString();
	dynamic pusher_obj = new Pusher(PusherAppId, PusherAppKey, PusherAppSecret);
	dynamic PusherResponse = pusher_obj.Trigger(CRoom, "CallActions", new {
		conf_action = _ConfAction,
		member_name = _Mem,
		is_all = _isActionAll,
		to_number = pushtonum,
		direction = "outbound"
	});
}
        public JObject DeafUndeaf(grpcall grpcall,string sConnString)
{
	JObject Resp = new JObject();
	string MemberIds = "";
    string num="";
	try {

		 GroupCallEntity obj = new GroupCallEntity(sConnString);
        obj.DeafUndeaf(grpcall, out MemberIds, out num);
        pushtonum=num;
		if (string.IsNullOrEmpty(MemberIds.Trim())) {
			Resp = new JObject(new JProperty("Success", false), new JProperty("Message", "No Members Found"));
		} else {
			Resp = DeafUndeafApi(MemberIds,grpcall);
		}
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		Resp = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error"));
	}
	return Resp;
}
        public JObject DeafUndeafApi(string MemberIds,grpcall grpcall)
{
	logclass.SimpleLog("DeafUndeaf, MemberIds : " + MemberIds);
	JObject Resp = new JObject();
	WebRequest _Req = null;
	HttpWebResponse _Resp = null;
	StreamReader SReader = null;
	StreamWriter SWriter = null;
	string HttpApiResponseString = "";
	string PostingData = "";
	try {
		if (grpcall.IsDeaf == true) {
			_Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "ConferenceDeaf/");
		} else {
			_Req = HttpWebRequest.Create(grpcall.HttpConferenceApiUrl + "ConferenceUndeaf/");
		}
		PostingData = "ConferenceName=" + grpcall.ConferenceRoom + "&MemberID=" + MemberIds;
		_Req.Method = "POST";
		//_Req.KeepAlive = false;
		_Req.ContentType = "application/x-www-form-urlencoded";
		SWriter = new StreamWriter(_Req.GetRequestStream());
		SWriter.Write(PostingData);
		SWriter.Flush();
		SWriter.Close();
		SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
		HttpApiResponseString = SReader.ReadToEnd();
		Pusher_Conference_MuteUnMuteAndDeafUnDeaf(grpcall.MemberName, HttpApiResponseString, grpcall.ConferenceRoom, grpcall.IsDeaf, "Deaf", grpcall.IsAll);
		Resp = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		Resp = new JObject(new JProperty("Success", false), new JProperty("Message", "Cannot mute at this moment : " + ex.Message));
	}
	return Resp;
}
        public JObject PlayToAConferenceCall(string ConferenceRoom, int MemberId, string HttpUrl, string FilePath)
{
	JObject PlayResponse = null;
	WebRequest _Req = null;
	HttpWebResponse _Resp = null;
	StreamReader SReader = null;
	StreamWriter SWriter = null;
	string PostingData = "";
	string HttpAPIResponseString = "";
	try {
		_Req = HttpWebRequest.Create(HttpUrl + "ConferencePlay/");
		_Req.Method = "POST";
		_Req.ContentType = "application/x-www-form-urlencoded";
		PostingData = "ConferenceName=" + ConferenceRoom + "&MemberID=" + MemberId.ToString() + "&FilePath=" + FilePath;
		SWriter = new StreamWriter(_Req.GetRequestStream());
		SWriter.Write(PostingData);
		SWriter.Flush();
		SWriter.Close();
		SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
		HttpAPIResponseString = SReader.ReadToEnd();
		SReader.Close();
		PlayResponse = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
	} catch (Exception ex) {
		logclass.LogRequest(ex.ToString());
		PlayResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error"));
	}
	return PlayResponse;
}
        public grpcall SetConferenceVariables(JObject validobj)
{
    ConferenceValidateObject = validobj;
    grpcall grpcall = new grpcall();

	_ConferenceCallBackUrl = System.Configuration.ConfigurationManager.AppSettings["ConferenceCallBackUrl"].ToString();
	try {
		if (ConferenceValidateObject.SelectToken("WelcomeClip") != null && string.IsNullOrEmpty(ConferenceValidateObject.SelectToken("WelcomeClip").ToString()) == false) {
		grpcall.WelcomeClip = ConferenceValidateObject.SelectToken("WelcomeClip").ToString();
		}
        if (ConferenceValidateObject.SelectToken("WaitClip") != null && string.IsNullOrEmpty(ConferenceValidateObject.SelectToken("WaitClip").ToString()) == false)
        {
			grpcall.WaitClip = ConferenceValidateObject.SelectToken("WaitClip").ToString();
		}
		grpcall.ConferenceRoom = ConferenceValidateObject.SelectToken("ConferenceRoom").ToString();
		grpcall.MemberMute = ConferenceValidateObject.SelectToken("Mute").ToString();
		grpcall.StartConferenceOnEnter = ConferenceValidateObject.SelectToken("StartConferenceOnEnter").ToString();
		grpcall.EndConferenceOnExit = ConferenceValidateObject.SelectToken("EndConferenceOnExit").ToString();
		grpcall.Moderator = ConferenceValidateObject.SelectToken("Moderator").ToString();
		grpcall.ConferenceAccessKey = ConferenceValidateObject.SelectToken("ConferenceAccessKey").ToString();
		grpcall.UserId = Convert.ToInt32(ConferenceValidateObject.SelectToken("UserId").ToString());
		grpcall.ConferenceId = Convert.ToInt32(ConferenceValidateObject.SelectToken("ConferenceId").ToString());
		if (ConferenceValidateObject.SelectToken("Calluid") != null) {
			grpcall.CallUUID = ConferenceValidateObject.SelectToken("Calluid").ToString();
		} else {
			grpcall.CallUUID = ConferenceValidateObject.SelectToken("CallUUID").ToString();
		}
		grpcall.HttpConferenceApiUrl = ConferenceValidateObject.SelectToken("HttpUrl").ToString();
		grpcall.OriginationUrl = ConferenceValidateObject.SelectToken("OriginationUrl").ToString();
		grpcall.ExtraDialString = ConferenceValidateObject.SelectToken("ExtraDialString").ToString();
		grpcall.CallerIdNumber = ConferenceValidateObject.SelectToken("CallerIdNumber").ToString();
		if (!string.IsNullOrEmpty(grpcall.ExtraDialString.Trim())) {
			if (!grpcall.ExtraDialString.EndsWith(",")) {
				grpcall.ExtraDialString = _ExtraDialString + ",";
			}
		}
		grpcall.GatewayID =Convert.ToInt16(ConferenceValidateObject.SelectToken("GatewayID"));
	} catch (Exception ex) {
        logclass.LogRequest(ex.ToString());
        grpcall.WelcomeClip = ""; grpcall.WaitClip = ""; grpcall.ConferenceRoom = ""; grpcall.MemberMute = ""; grpcall.StartConferenceOnEnter = "";
        grpcall.EndConferenceOnExit = ""; grpcall.Moderator = ""; grpcall.ConferenceAccessKey = ""; grpcall.UserId = 0;
        grpcall.ConferenceId = 0; grpcall.CallUUID = ""; grpcall.HttpConferenceApiUrl = ""; grpcall.OriginationUrl = "";
        grpcall.ExtraDialString = ""; grpcall.CallerIdNumber = ""; grpcall.ExtraDialString = ""; grpcall.GatewayID = 0;
    }
    return grpcall;
}

    }
}
