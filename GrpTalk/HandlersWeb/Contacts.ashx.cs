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
using System.Configuration;
using System.Web.SessionState;
using System.Data.OleDb;
using System.Data;
using GT.Utilities;
namespace GrpTalk.HandlersWeb
{
    /// <summary>
    /// Summary description for Contacts
    /// </summary>
    public class Contacts : IHttpHandler, IRequiresSessionState
    {
        JObject jObj = new JObject();

        public void ProcessRequest(HttpContext context)
        {
            int caseType = Convert.ToInt32(context.Request["type"]);


            switch (caseType)
            {
                case 1:
                    jObj = GetMobileContacts(context);
                    context.Response.Write(jObj);
                    return;
                case 2:
                    jObj = GetWebContacts(context);
                    context.Response.Write(jObj);
                    return;
                case 3:
                    jObj = DeleteWebContact(context);
                    context.Response.Write(jObj);
                    return;
                case 4:
                    jObj = CropImage(context);
                    context.Response.Write(jObj);
                    return;
                case 5:
                    jObj = GetWebContactsNew(context);
                    context.Response.Write(jObj);
                    return;
                case 6:
                    jObj = AddContactList(context);
                    context.Response.Write(jObj);
                    return;
                case 7:
                    jObj = EditContactList(context);
                    context.Response.Write(jObj);
                    return;
                case 8:
                    jObj = AddAllContactsToParticularList(context);
                    context.Response.Write(jObj);
                    return;
                case 9:
                    jObj = GetMobileContactsSort(context);
                    context.Response.Write(jObj);
                    return;
                case 10:
                    jObj = UploadExcelContact(context);
                    context.Response.Write(jObj);
                    return;
                case 11:
                    jObj = DeleteListIdNListIdContacts(context);
                    context.Response.Write(jObj);
                    return;
                case 12:
                    jObj = AddNonMember(context);
                    context.Response.Write(jObj);
                    return;

            }
        }

        private JObject AddNonMember(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.AddNonMember(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["listId"]), context.Request["Mobile"].ToString(), Convert.ToInt32(context.Request["mode"]));
            return jObj;
        }
        private JObject DeleteListIdNListIdContacts(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.DeleteListIdNListIdContacts(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["listId"]), Convert.ToInt32(context.Request["mode"]));
            return jObj;
        }

        private JObject UploadExcelContact(HttpContext context)
        {
          
            JObject paramObj = new JObject();
            JObject objectj = new JObject();
            string mobileNumber = "", contactName = "";
            Int64 contactId = 0;
            DataTable Tab = new DataTable();
            DataTable excelDuplicates = new DataTable();
            //Tab.Columns.Add("cid", typeof(string));
            Tab.Columns.Add("name", typeof(string));
            Tab.Columns.Add("mobile", typeof(string));
          
            excelDuplicates.Columns.Add("contactId", typeof(Int64));
            excelDuplicates.Columns.Add("Name", typeof(string));
            excelDuplicates.Columns.Add("mobile", typeof(string));     
                  

            if (context.Request["mode"] == "1")
            {
                
                paramObj =JObject.Parse(context.Request["excelContacts"]);
                
                foreach (JObject _Member in (JArray)paramObj.SelectToken("excelDuplicates"))
                {
                    mobileNumber = "";
                    contactName = ""; contactId = 0;
                    foreach (JProperty _Token in _Member.Properties())
                    {
                        if (_Token.Name == "name")
                        {
                            contactName = Convert.ToString(_Token.Value);
                        }
                        else if (_Token.Name == "mobilenumber")
                        {
                            mobileNumber =Convert.ToString(_Token.Value);
                        }
                        else if (_Token.Name == "contactid")
                        {
                            contactId =Convert.ToInt32(_Token.Value);
                        }
                        Logger.TraceLog("contactId" + contactId + " contactName" + contactName + " mobileNumber" + mobileNumber);

                    }

                    excelDuplicates.Rows.Add(contactId,contactName.Trim(), mobileNumber.Trim());
                    Logger.TraceLog("Excel Duplicates length" + excelDuplicates.Rows.Count.ToString());

                }

            }
            else
            {
                string header = "";
                if (Convert.ToInt16(context.Request["header"]) == 1)
                {
                    header = "Yes";
                }
                else
                {
                    header = "No";
                }
                string fileName = context.Request["Path"].ToString();
                string xlSheetData = context.Request["semiData"].ToString();
                JObject jObj = JObject.Parse(xlSheetData);
                string excelUploadPath = HttpContext.Current.Server.MapPath("~/ExcelFilesUpload/");
                string extension = System.IO.Path.GetExtension(fileName);
                JArray sheetArray = new JArray();
                sheetArray = jObj.SelectToken("data") as JArray;
                DataTable table = new DataTable();
                
                table.Columns.Add("sheetname", typeof(string));
                table.Columns.Add("columns", typeof(int));
                table.Columns.Add("name", typeof(string));
                table.Columns.Add("mobile", typeof(string));
           
                foreach (JObject _sheet in sheetArray)
                {
                    object[] val = new object[4];
                    val[0] = _sheet.SelectToken("sheetname").ToString();
                    val[1] = _sheet.SelectToken("columnscount").ToString();
                    val[2] = _sheet.SelectToken("name").ToString();
                    val[3] = _sheet.SelectToken("mobile").ToString();
                    table.Rows.Add(val);
                }
                string excelOleDbConString = "";
                OleDbConnection oleDbCon = default(OleDbConnection);
                oleDbCon = null;
                if (extension == ".xlsx")
                {
                    excelOleDbConString = excelOleDbConString + "provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelUploadPath + fileName + ";Persist Security Info=True; Extended Properties=\"Excel 12.0;HDR=" + header + ";IMEX=1;\"";
                }
                else if (extension == ".xls")
                {
                    excelOleDbConString = excelOleDbConString + "provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelUploadPath + fileName + ";Persist Security Info=True; Extended Properties=\"Excel 8.0;HDR=" + header + ";IMEX=1;\"";
                }

                oleDbCon = new OleDbConnection(excelOleDbConString);
                //oleDbCon = New OleDbConnection(excelOleDbConString)
                OleDbCommand OleCmdSelect = null;
                OleDbDataAdapter OleAdapter = null;
                DataSet dSet = null;
                int NaP = 0;
                int MobP = 0;
                
                int cid = 1;
                int ColumnsCount = 0;
                for (int k = 0; k <= table.Rows.Count - 1; k++)
                {
                    OleAdapter = null;
                    dSet = null;
                    OleCmdSelect = new OleDbCommand("SELECT   *  FROM [" + table.Rows[k]["sheetname"] + "$]", oleDbCon);
                    OleAdapter = new OleDbDataAdapter(OleCmdSelect);
                    dSet = new DataSet();
                    OleAdapter.Fill(dSet);
                    NaP = 0;
                    MobP = 0;
                    mobileNumber = "";
                    contactName = "";
                    if (dSet.Tables[0].Columns.Count >= 2)
                    {
                        var _with1 = dSet.Tables[0];
                        ColumnsCount = dSet.Tables[0].Columns.Count;
                        MobP = Convert.ToInt16(table.Rows[k]["mobile"]);
                        NaP = Convert.ToInt16(table.Rows[k]["name"]);
                        foreach (DataRow _Row in dSet.Tables[0].Rows)
                        {


                            if (MobP != 0)
                            {
                                mobileNumber = _Row[MobP - 1].ToString();
                                bool isIntString = mobileNumber.All(char.IsDigit);
                                if (!isIntString)
                                {
                                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", "Invalid Mobile Column"));
                                    return jObj;
                                }
                            }

                            else
                            {
                                mobileNumber = "";
                            }
                            if (NaP != 0)
                            {
                                contactName = _Row[NaP - 1].ToString();
                            }


                            if (mobileNumber.Trim() != string.Empty)
                            {
                                Tab.Rows.Add(contactName.Trim(), mobileNumber.Trim());
                            }
                            // cid = cid + 1;

                        }


                    }
                    if (Tab.Rows.Count == 0)
                    {
                        jObj = new JObject(new JProperty("Success", false),
                                  new JProperty("Message", "Invalid Mobile Column"));
                        return jObj;

                    }
                    // JObject jObj = new JObject();
                }
            }
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            objectj = contactsObj.UploadExcelData(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Tab, excelDuplicates, Convert.ToInt32(context.Request["listID"]), Convert.ToInt32(context.Request["mode"]), Convert.ToString(context.Request["listName"]));
            string json = JsonConvert.SerializeObject(Tab, Formatting.Indented);


            return objectj;
        }
        private JObject GetWebContactsNew(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.WebContactsList(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["mode"]), Convert.ToInt32(context.Request["listId"]), Convert.ToInt32(context.Request["source"]), Convert.ToInt32(context.Request["pageIndex"]), context.Request["alphabetSort"].ToString(), context.Request["searchValue"].ToString());
            return jObj;
        }
        private JObject GetMobileContacts(HttpContext context)
        {
            int soucreType = Convert.ToInt32(context.Request["source"]);
            int pageIndex = Convert.ToInt32(context.Request["pageIndex"]);
            int mode = Convert.ToInt32(context.Request["modeSp"]);
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.GetMobilContactsDetails(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), soucreType, mode, pageIndex, context);
            return jObj;

        }

        private JObject GetMobileContactsSort(HttpContext context)
        {
            int mode = Convert.ToInt32(context.Request["modeSp"]);
            int soucreType = Convert.ToInt32(context.Request["source"]);
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.GetMobilContactsDetailsSort(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), soucreType, context.Request["alphabetSort"], mode, Convert.ToInt32(context.Request["alphabetPageIndex"]), Convert.ToInt32(context.Request["listID"]), context);
            return jObj;

        }


        public JObject CropImage(HttpContext context)
        {
            int xCordinate = Convert.ToInt16(context.Request["cropX"]);
            int yCordinate = Convert.ToInt16(context.Request["cropY"]);
            int width = Convert.ToInt16(context.Request["cropW"]);
            int height = Convert.ToInt16(context.Request["cropH"]);
            int fixWidth = 965;
            string imgName = Convert.ToString(context.Request["imgName"]);
            string readPath = "";
            string savePath = "";
            if (imgName != "")
            {
                readPath = HttpContext.Current.Server.MapPath("/ContactImages/") + "\\" + imgName.Substring(imgName.LastIndexOf("/") + 1);
                savePath = HttpContext.Current.Server.MapPath("/Temp_crop_images/") + "\\" + imgName.Substring(imgName.LastIndexOf("/") + 1);
            }

            //string readPath = ConfigurationManager.AppSettings["CropImageSavePath"].ToString() + "\\" + imgName.Substring(imgName.LastIndexOf("/") + 1);
            //string savePath = ConfigurationManager.AppSettings["TempUploadPath"].ToString() + "\\" + imgName.Substring(imgName.LastIndexOf("/") + 1);
            //Profile profileObj = new Profile();
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.CropImage(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), context.Request["name"].ToString(), context.Request["mobileNumber"].ToString(), context.Request["prefix"].ToString(), Convert.ToInt32(context.Request["contactID"]), Convert.ToInt32(context.Request["mode"]), Convert.ToInt32(context.Request["listID"]), Convert.ToString(context.Request["listName"]), savePath, readPath, imgName, width, height, xCordinate, yCordinate, fixWidth);
            return jObj;

        }
        private JObject DeleteWebContact(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.DeleteWebContact(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["contactID"]), Convert.ToInt32(context.Request["listID"]), Convert.ToInt32(context.Request["mode"]));
            return jObj;
        }

        private JObject GetWebContacts(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.WebContactsList(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["listId"]), Convert.ToInt32(context.Request["pageIndex"]));
            Logger.TraceLog("Session Of UserId for mobileContacts" + Convert.ToInt32(context.Session["UserID"]));
            return jObj;
        }

        private JObject AddContactList(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.AddWebContactList(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), context.Request["listName"], Convert.ToInt32(context.Request["modeSp"]));
            return jObj;
        }

        private JObject EditContactList(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.EditWebContactList(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["listId"]), context.Request["listName"], Convert.ToInt32(context.Request["modeSp"]));
            return jObj;
        }

        private JObject AddAllContactsToParticularList(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Contacts contactsObj = new GT.BusinessLogicLayer.V_1_5.Contacts();
            jObj = contactsObj.AddAllContactsToParticularList(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), context.Request["contactID"], Convert.ToInt32(context.Request["mode"]), Convert.ToInt32(context.Request["listIdParam"]));
            return jObj;
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