
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.Net;
using System.Configuration;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using log4net;
using System.Security.Cryptography;
using System.Text;

using System.Drawing;

partial class GoogleCallback : System.Web.UI.Page
{
    string sToken = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["access_token"]) == false)
        {
            sToken = Request.QueryString["access_token"].ToString();
        }
       
        PurchaseCredits();
    }
    private string PurchaseCredits()
    {
        string retMessage = "";
        string httpUrl = "https://accounts.google.com/o/oauth2/token";
        StreamReader Sreader = null;
        bool status = false;
        string success = "";
        string _postdata = "";
        string ResponseString = "";
        JObject ResObj = new JObject();
        HttpWebRequest _Req = null;
        HttpWebResponse _Resp = null;
        StreamWriter Swriter = null;
        try
        {
            _Req = (HttpWebRequest)HttpWebRequest.Create(httpUrl.ToString());
            _Req.Method = "POST";
            _postdata = "grant_type=authorization_code&refresh_token=1/mQjQgyUCZuMaLrrq12jKRHF5dy6m_MuA95m2Q9KRn7A&client_id=826384144356-3jovq0pd51blm16i40e9lik263cv2kj1.apps.googleusercontent.com&client_secret=PJxMmjy14CfxYkDv_Aj_sfIt";
            _Req.ContentType = "application/x-www-form-urlencoded";
            Swriter = new StreamWriter(_Req.GetRequestStream());
            Swriter.Write(_postdata.Trim());
            Swriter.Flush();
            Swriter.Close();
            _Resp = (HttpWebResponse)_Req.GetResponse();
            Sreader = new StreamReader(_Resp.GetResponseStream());
            ResponseString = Sreader.ReadToEnd();

            ResObj = JObject.Parse(ResponseString);
            Response.Write(ResObj);
          //  logclass.LogRequest("Reponse------" + ResObj.ToString());
           // status = ResObj.SelectToken("Success");

            if (status == true)
            {
                success = "success";
            }
            return retMessage;
        }
        catch (Exception ex)
        {
            //Return ex.ToString()
          //  logclass.LogRequest("Exp at PurchaseCredits : " + ex.ToString());
            return "{'status':'2000'}";
        }

    }
    public GoogleCallback()
    {
        Load += Page_Load;
    }
}
