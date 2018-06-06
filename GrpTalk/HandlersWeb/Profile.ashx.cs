using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;
using GrpTalk.CommonClasses;
using GT.BusinessLogicLayer.V_1_5;
using System.IO;
using System.Web.SessionState;
using System.Configuration;

namespace GrpTalk.HandlersWeb
{
    /// <summary>
    /// Summary description for Profile
    /// </summary>
    public class Profile : IHttpHandler, IRequiresSessionState
    {
        JObject jobj = new JObject();
        public void ProcessRequest(HttpContext context)
        {
            int caseType = Convert.ToInt32(HttpContext.Current.Request["type"]);
            int mode = 0;
            switch (caseType)
            {
                case 1:
                    mode = Convert.ToInt32(HttpContext.Current.Request["modeSp"]);
                    jobj = GetProfileDetails(context, mode);
                    context.Response.Write(jobj);
                    return;
                case 2:
                    mode = Convert.ToInt32(HttpContext.Current.Request["modeSp"]);
                    int x = Convert.ToInt16(context.Request["cropX"]);
                    int y = Convert.ToInt16(context.Request["cropY"]);
                    int w = Convert.ToInt16(context.Request["cropW"]);
                    int h = Convert.ToInt16(context.Request["cropH"]);
                    int fixWidth = 965;
                    string imgName = Convert.ToString(context.Request["imgName"]);
                    jobj = CropImage(imgName, w, h, x, y, fixWidth, context, mode, Convert.ToInt32(context.Session["UserID"]));
                    context.Response.Write(jobj);
                    return;
                case 3:
                    jobj = RechargeAountPieChart(context);
                    context.Response.Write(jobj);
                    return;
                case 4:
                    mode = Convert.ToInt32(HttpContext.Current.Request["modeSp"]);
                    jobj = SetOnlyProfileNameEmail(context, mode);
                    context.Response.Write(jobj);
                    return;
                case 5:
                    jobj = SetUserUtcOffset(context);
                    context.Response.Write(jobj);
                    return;

            }
        }
        private JObject GetProfileDetails(HttpContext context, int mode)
        {

            GT.BusinessLogicLayer.V_1_5.Profile profileObj = new GT.BusinessLogicLayer.V_1_5.Profile();
            jobj = profileObj.GetProfileDetailsDisplay(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), mode);
            return jobj;

        }

        private JObject RechargeAountPieChart(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Profile profileObj = new GT.BusinessLogicLayer.V_1_5.Profile();
            jobj = profileObj.RcAmountPieDiagram(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]));
            return jobj;

        }

        public JObject CropImage(string imgName, int w, int h, int x, int y, int fixWidth, HttpContext context, int mode, int userID)
        {
            string readPath = HttpContext.Current.Server.MapPath("/TempImages/") + imgName;//ConfigurationManager.AppSettings["CropImageSavePath"].ToString() + "\\" + imgName.Substring(imgName.LastIndexOf("/") + 1);
            string savePath = HttpContext.Current.Server.MapPath("/Temp_crop_images/") + imgName; // ConfigurationManager.AppSettings["TempUploadPath"].ToString() + "\\" + imgName.Substring(imgName.LastIndexOf("/") + 1);
            GT.BusinessLogicLayer.V_1_5.Profile profileObj = new GT.BusinessLogicLayer.V_1_5.Profile();
            jobj = profileObj.CropWLeader(MyConf.MyConnectionString, savePath, readPath, imgName, w, h, x, y, fixWidth, context, mode, userID);
            return jobj;

        }
        private JObject SetOnlyProfileNameEmail(HttpContext context, int mode)
        {

            GT.BusinessLogicLayer.V_1_5.Profile profileObj = new GT.BusinessLogicLayer.V_1_5.Profile();
            jobj = profileObj.ProfileUpdateNameEmail(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), mode, context);
            return jobj;

        }
        private JObject SetUserUtcOffset(HttpContext context)
        {

            GT.BusinessLogicLayer.V_1_5.Profile profileObj = new GT.BusinessLogicLayer.V_1_5.Profile();
            jobj = profileObj.SetUserUtcOffSet(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(HttpContext.Current.Request["modeSp"]), HttpContext.Current.Request["offSetValue"].ToString());
            return jobj;

        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}