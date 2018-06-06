using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using GT.Utilities;
using SD = System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.IO;

namespace GT.BusinessLogicLayer.V_1_4
{
    public class Contacts_V140
    {

        public static string curPath;
        public static string savePath;
        public static string imageName;

        public JObject PhoneCallHistorySync(string sConnString, int userID, string phoneNumbers)
        {
            JObject listsResponse = new JObject();
            JObject listsJobj = new JObject();
            string retMessage = "";
            string timeStamp = "";
            int retVal = 0, errorCode = 0;
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                retVal = contactsObj.PhoneCallHistorySync(userID, phoneNumbers, out retMessage, out errorCode);
                if (retVal == 1)
                {
                    listsResponse = new JObject(new JProperty("Success", true),
                                new JProperty("Message", "Success"), new JProperty("ErrorCode", errorCode));


                }
                else
                {
                    listsResponse = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                }

            }
            catch (Exception ex)
            {
                listsResponse = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.TraceLog("Exception In PhoneCallHistorySync BLL : " + ex.ToString());

            }

            return listsResponse;
        }
        public JObject GetAllContactsLists(string sConnString, int userID)
        {
            JObject listsResponse = new JObject();
            DataSet listsDataSet = new DataSet();
            JObject listsJobj = new JObject();
            JArray listsJarr = new JArray();
            JArray deletedListsJarr = new JArray();
            string retMessage = "";
            string timeStamp = "";
            int retVal = 0;
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                listsDataSet = contactsObj.GetAllContactsList(userID, out retVal, out retMessage, out timeStamp);
                if (retVal == 1)
                {
                    if (listsDataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow _row in listsDataSet.Tables[0].Rows)
                        {
                            listsJobj = new JObject();
                            foreach (DataColumn _column in listsDataSet.Tables[0].Columns)
                            {
                                listsJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                            }
                            listsJarr.Add(listsJobj);
                        }

                    }
                    if (listsDataSet.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow _row in listsDataSet.Tables[1].Rows)
                        {
                            foreach (DataColumn _column in listsDataSet.Tables[1].Columns)
                            {
                                deletedListsJarr.Add(_row["Id"]);
                            }

                        }
                    }
                    listsResponse = new JObject(new JProperty("Success", true),
                            new JProperty("Message", "Success"), new JProperty("ErrorCode", "117"),
                            new JProperty("TimeStamp", timeStamp),
                            new JProperty("Lists", listsJarr),
                            new JProperty("DeletedLists", deletedListsJarr));
                }
                else
                {
                    listsResponse = new JObject(new JProperty("Success", false), new JProperty("ErrorCode", "112"),
                            new JProperty("Message", retMessage));


                }

            }
            catch (Exception ex)
            {
                listsResponse = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.TraceLog("Exception In GetAllContactsLists BLL : " + ex.ToString());

            }

            return listsResponse;
        }


        public JObject WebContactsList(string sConnString, int userID, int listId, int pageIndex)
        {
            JObject responseJObj = new JObject();
            DataSet ds = new DataSet();
            JObject contactJobj = new JObject();
            JObject listJobj = new JObject();
            JObject contactListJobj = new JObject();
            JArray Jarr = new JArray();
            JArray JarrList = new JArray();
            JArray JarrContactList = new JArray();
            int returnValue = 0;
            string returnMessage = "";
            int pageCount = 0, allContactsCount = 0;
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                ds = contactsObj.WebContactsList(userID, listId, pageIndex, out returnValue, out returnMessage, out pageCount, out allContactsCount);
                if (returnValue == 1)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow _row in ds.Tables[0].Rows)
                        {
                            contactJobj = new JObject();
                            foreach (DataColumn _column in ds.Tables[0].Columns)
                            {
                                contactJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                            }
                            Jarr.Add(contactJobj);
                        }

                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        foreach (DataRow _row in ds.Tables[1].Rows)
                        {
                            listJobj = new JObject();
                            foreach (DataColumn _column in ds.Tables[1].Columns)
                            {
                                listJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                            }
                            JarrList.Add(listJobj);
                        }

                    }
                    if (ds.Tables[3].Rows.Count > 0)
                    {

                        foreach (DataRow _row in ds.Tables[3].Rows)
                        {
                            contactListJobj = new JObject();
                            foreach (DataColumn _column in ds.Tables[3].Columns)
                            {
                                contactListJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                            }
                            JarrContactList.Add(contactListJobj);
                        }

                    }
                    responseJObj = new JObject(new JProperty("Success", true),
                            new JProperty("Message", returnMessage), new JProperty("ErrorCode", "117"),
                             new JProperty("Status", returnValue),
                              new JProperty("AllContactsCount", allContactsCount),
                              new JProperty("pageCount", pageCount),
                            new JProperty("Items", Jarr),
                            new JProperty("ContactList", JarrList),
                            new JProperty("Data", JarrContactList));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in WebContactsList Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "No contatcs found"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }
        public JObject GetMobilContactsDetails(string sConnString, int userId, int source, int mode, int pageIndex, HttpContext context)
        {
            JObject jResponse = new JObject();
            JArray jArray = new JArray();
            JArray jarSubcontcts = new JArray();
            try
            {
                int retValue = 0, defaultLines = 0;
                string retMessage = "";
                int pageCount = 1;
                DataSet allContacts = new DataSet();
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                allContacts = contactsObj.GetMobileContactDetails(userId, source, mode, pageIndex, out retValue, out retMessage, out pageCount, out defaultLines);
                if (retValue == 1)
                {
                    if (allContacts != null)
                    {
                        if (allContacts.Tables.Count != 0)
                        {
                            if (allContacts.Tables[0].Rows.Count != 0)
                            {
                                for (int rowCount = 0; rowCount < allContacts.Tables[0].Rows.Count; rowCount++)
                                {
                                    DataRow[] result = null;
                                    if (allContacts.Tables.Count > 1)
                                    {
                                        result = null;
                                        if (allContacts.Tables[2].Rows.Count != 0)
                                        {
                                            result = allContacts.Tables[2].Select("ContactID =" + allContacts.Tables[0].Rows[rowCount]["ID"] + " ");
                                            int COUNT = result.Count();
                                            jarSubcontcts = new JArray();
                                            foreach (DataRow drSubContacts in result)
                                            {

                                                jarSubcontcts.Add(new JObject(new JProperty("subContactId", drSubContacts["contactId"]),
                                                new JProperty("contactType", drSubContacts["contactType"]),
                                                new JProperty("contactNumber", drSubContacts["Number"]),
                                                new JProperty("userId", drSubContacts["UserId"])));
                                                //jArray.Add(new JProperty("subContactDeatils", jarSubcontcts));

                                            }
                                        }
                                    }


                                    jArray.Add(new JObject(new JProperty("id", allContacts.Tables[0].Rows[rowCount]["Id"]),
                                        new JProperty("name", allContacts.Tables[0].Rows[rowCount]["Name"]),
                                        new JProperty("mobileNumber", allContacts.Tables[0].Rows[rowCount]["MobileNumber"]),
                                        new JProperty("imagePath", allContacts.Tables[0].Rows[rowCount]["ImagePath"]),
                                        new JProperty("source", allContacts.Tables[0].Rows[rowCount]["Source"]),
                                         new JProperty("Prefix", allContacts.Tables[0].Rows[rowCount]["Prefix"]),
                                        new JProperty("subContactDeatils", jarSubcontcts)));




                                }



                                jResponse = new JObject(new JProperty("Success", true),
                                   new JProperty("Message", retMessage),
                                   new JProperty("ErrorCode", "117"),
                                   new JProperty("Status", retValue),
                                   new JProperty("DefaultLines", defaultLines),
                                    new JProperty("pageCount", pageCount),
                                   new JProperty("contactDetails", jArray));


                                return (jResponse);
                            }
                            else
                            {
                                jResponse = new JObject(new JProperty("Success", false),
                                   new JProperty("Message", "No Contacts Found"));
                            }
                        }
                    }
                }
                else
                {
                    jResponse = new JObject(new JProperty("Success", false),
                         new JProperty("Message", retMessage), new JProperty("ErrorCode", "101"));
                    return (jResponse);
                }
            }
            catch (Exception ex)
            {
                jResponse = new JObject(new JProperty("Success", false),
                           new JProperty("Message", ex.ToString()), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.StackTrace));
                Logger.ExceptionLog("Exception in GetMobilContactsDetails BLL" + ex.StackTrace);

            }

            return jResponse;

        }

        public JObject GetMobilContactsDetailsSort(string sConnString, int userId, int source, string alphabet, int mode, int pageIndex, int listId, HttpContext context)
        {
            JObject jResponse = new JObject();
            JArray jArray = new JArray();
            JArray jarSubcontcts = new JArray();
            try
            {
                int retValue = 0, contactsCount = 0;
                string retMessage = "";
                int pageCount = 1;
                DataSet allContacts = new DataSet();
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                allContacts = contactsObj.GetMobileContactDetailsSort(userId, source, mode, alphabet, pageIndex, listId, out retValue, out retMessage, out pageCount, out contactsCount);
                if (retValue == 1)
                {
                    if (allContacts != null)
                    {
                        if (allContacts.Tables.Count != 0)
                        {
                            if (allContacts.Tables[0].Rows.Count != 0)
                            {
                                for (int rowCount = 0; rowCount < allContacts.Tables[0].Rows.Count; rowCount++)
                                {
                                    if (source == 1)
                                    {
                                        DataRow[] result = null;

                                        if (allContacts.Tables.Count > 1)
                                        {
                                            result = null;
                                            if (allContacts.Tables[1].Rows.Count != 0)
                                            {
                                                result = allContacts.Tables[1].Select("ContactID =" + allContacts.Tables[0].Rows[rowCount]["ID"] + " ");
                                                int COUNT = result.Count();
                                                jarSubcontcts = new JArray();
                                                foreach (DataRow drSubContacts in result)
                                                {

                                                    jarSubcontcts.Add(new JObject(new JProperty("subContactId", drSubContacts["contactId"]),
                                                       new JProperty("contactType", drSubContacts["contactType"]),
                                                       new JProperty("contactNumber", drSubContacts["Number"]),
                                                       new JProperty("userId", drSubContacts["UserId"])));


                                                }
                                            }
                                        }

                                    }

                                    jArray.Add(new JObject(new JProperty("id", allContacts.Tables[0].Rows[rowCount]["Id"]),
                                        new JProperty("name", allContacts.Tables[0].Rows[rowCount]["Name"]),
                                        new JProperty("mobileNumber", allContacts.Tables[0].Rows[rowCount]["MobileNumber"]),
                                        new JProperty("imagePath", allContacts.Tables[0].Rows[rowCount]["ImagePath"]),
                                        new JProperty("source", allContacts.Tables[0].Rows[rowCount]["Source"]),
                                          new JProperty("Prefix", allContacts.Tables[0].Rows[rowCount]["Prefix"]),
                                           new JProperty("ContactId", allContacts.Tables[0].Rows[rowCount]["ContactId"]),
                                        new JProperty("subContactDeatils", jarSubcontcts)));




                                }



                                jResponse = new JObject(new JProperty("Success", true),
                                   new JProperty("Message", retMessage),
                                   new JProperty("Status", retValue), new JProperty("ErrorCode", "117"),
                                   new JProperty("ContactsCount", contactsCount),
                                    new JProperty("pageCount", pageCount),
                                   new JProperty("contactDetails", jArray));


                                return (jResponse);
                            }
                            else
                            {
                                jResponse = new JObject(new JProperty("Success", false),
                                new JProperty("Message", "No Data Found"),
                                new JProperty("ErrorCode", "139"),
                                new JProperty("ContactsCount", contactsCount),
                                new JProperty("Status", 0));


                                return (jResponse);
                            }
                        }
                    }
                }
                else
                {
                    jResponse = new JObject(new JProperty("Success", false),
                         new JProperty("Message", retMessage), new JProperty("ErrorCode", "101"));
                    return (jResponse);
                }
            }
            catch (Exception ex)
            {
                jResponse = new JObject(new JProperty("Success", false),
                           new JProperty("Message", ex.ToString()), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.StackTrace));
                Logger.ExceptionLog("Exception in GetMobilContactsDetailsSort BLL" + ex.StackTrace);

            }

            return jResponse;

        }


        public JObject DeleteWebContact(string sConnString, int userID, int contactID, int listId, int mode)
        {
            JObject responseJObj = new JObject();
            DataSet ds = new DataSet();
            JObject tempJobj = new JObject();
            JArray Jarr = new JArray();
            int returnValue = 0;
            string returnMessage = "";
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                ds = contactsObj.DeleteWebContact(userID, listId, contactID, mode, out returnValue, out returnMessage);
                if (returnValue == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                         new JProperty("Status", returnValue), new JProperty("ErrorCode", "117"),
                          new JProperty("Message", returnMessage));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                           new JProperty("Status", returnValue), new JProperty("ErrorCode", "101"),
                           new JProperty("Message", "Contact deleted"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in DeleteWebContact Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }


        public JObject CropImage(string sConnString, int userID, string name, string mobileNumber, string prefix, int contactID, int mode, int listID,string listName, string saveaPath, string readPath, string imgName, int w, int h, int x, int y, int fixWidth)
        {
            JObject jres = new JObject();

            string saveTo = "";
            string respStr = string.Empty;

            try
            {
                if (readPath != "")
                {

                    savePath = HttpContext.Current.Server.MapPath("/ContactImages/");
                    imageName = imgName;
                    byte[] CropImage = Crop(saveaPath, w, h, x, y);
                    using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
                    {
                        ms.Write(CropImage, 0, CropImage.Length);
                        using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                        {
                            //Dim SaveTo As String = cur_path & img_name
                            saveTo = savePath + imgName;

                            CroppedImage.Save(saveTo, CroppedImage.RawFormat);
                            if (w > fixWidth)
                            {
                                int nw = 0;
                                int nh = 0;
                                nw = fixWidth;
                                nh = nw / (w / h);
                                ResizeImage(nh, nw, saveTo);
                                string storePath = saveaPath + imgName;

                            }

                            respStr = imgName;
                            Logger.ExceptionLog("DLL IMAGE PATH " + imgName);
                            jres = ManageWebContacts(sConnString, userID, name, mobileNumber, prefix, contactID, mode, listID,listName, imgName);


                        }
                    }
                }
                else if (listName == string.Empty)
                {
                    jres = ManageWebContacts(sConnString, userID, name, mobileNumber, prefix, contactID, mode, listID, "","");
                }
                else {

                    jres = CreateWebListContact(sConnString, userID, name, mobileNumber, prefix, contactID, mode, listID,listName, imgName);
                }

                return jres;
            }
            catch (Exception ex)
            {
                jres = new JObject(new JProperty("Success", false),
                       new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.StackTrace));
                Logger.ExceptionLog("Exception in CropImage Bll" + ex.StackTrace);
            }
            return jres;
        }


        private static void ResizeImage(int h, int w, string imgName)
        {
            System.Drawing.Bitmap bmpOut = null;
            string lcFileName = imgName;
            try
            {
                Bitmap loBMP = new Bitmap(lcFileName);
                ImageFormat loFormat = loBMP.RawFormat;

                int newWidth = w;
                int newHeight = h;

                bmpOut = new Bitmap(newWidth, newHeight);


                using (Graphics g = System.Drawing.Graphics.FromImage(bmpOut))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                    g.FillRectangle(Brushes.White, 0, 0, newWidth, newHeight);
                    g.DrawImage(loBMP, 0, 0, newWidth, newHeight);
                    loBMP.Dispose();
                }
                bmpOut.Save(lcFileName);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        private static byte[] Crop(string img, int width, int height, int X, int Y)
        {
            try
            {
                using (SD.Image originalImage = SD.Image.FromFile(img))
                {
                    using (SD.Bitmap bmp = new SD.Bitmap(width, height))
                    {
                        bmp.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);
                        using (SD.Graphics graphic = SD.Graphics.FromImage(bmp))
                        {
                            graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphic.DrawImage(originalImage, new SD.Rectangle(0, 0, width, height), X, Y, width, height, SD.GraphicsUnit.Pixel);
                            MemoryStream ms = new MemoryStream();
                            bmp.Save(ms, originalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }

        }

        public JObject CreateWebListContact(string sConnString, int userId, string name, string mobileNumber, string prefix, int contactID, int mode, int listID,string listName, string tempCropImagePath)
        {
            JObject responseJObj = new JObject();
            JArray jList = new JArray();
            MemoryStream mStream = new MemoryStream();
            int returnValue = 0;
            string returnMessage = "";
            string imagePath = "";
            int contactId = 0;
            try
            {

                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                imagePath = HttpContext.Current.Server.MapPath("/ContactImages/");
                Logger.TraceLog("ListName:" + listName);
                DataSet ds = contactsObj.ManageWebContacts(userId, name, mobileNumber, prefix, contactID, mode, listID,listName, out returnValue, out returnMessage, tempCropImagePath, out contactId);
                if (returnValue == 1)
                {
                    
                         if(ds.Tables.Count > 0)
                         {
                            if(ds.Tables[0].Rows.Count > 0)
                            {
                                jList.Add(new JObject(new JProperty("ListId",ds.Tables[0].Rows[0]["ListId"]),
                                    new JProperty("ListCount", ds.Tables[0].Rows[0]["listCount"])));
                            
                            }
                         }
                         responseJObj = new JObject(new JProperty("Success", true),
                             new JProperty("Status", returnValue),
                              new JProperty("ErrorCode", "117"),
                             new JProperty("Message", returnMessage),
                             new JProperty("List", jList));

                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                           new JProperty("Status", returnValue), new JProperty("ErrorCode", "142"),
                           new JProperty("Message", "Contact Not created"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ManageWebContacts Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }

        public JObject ManageWebContacts(string sConnString, int userId, string name, string mobileNumber, string prefix, int contactID, int mode, int listID,string listName, string tempCropImagePath)
        {
            JObject responseJObj = new JObject();
            JArray jList = new JArray();
            MemoryStream mStream = new MemoryStream();
            int returnValue = 0;
            string returnMessage = "";
            string imagePath = "";
            int contactId = 0;
            try
            {

                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                imagePath = HttpContext.Current.Server.MapPath("/ContactImages/");
                //byte[] imgCropArray = ReadFile(imagePath + tempCropImagePath);
                
                DataSet ds = contactsObj.ManageWebContacts(userId, name, mobileNumber, prefix, contactID, mode, listID,listName, out returnValue, out returnMessage, tempCropImagePath, out contactId);
                if (returnValue == 1)
                {
                    if(listName.Trim()!=string.Empty)
                    { 
                        if (ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                jList.Add(new JObject(new JProperty("ListId", ds.Tables[0].Rows[0]["ListId"]),
                                    new JProperty("ListCount", ds.Tables[0].Rows[0]["listCount"])));

                            }
                        }
                    }
                    responseJObj = new JObject(new JProperty("Success", true),
                        new JProperty("ContactId", contactId),
                         new JProperty("Status", returnValue),
                         new JProperty("ErrorCode", "117"),
                          new JProperty("Message", returnMessage),
                          new JProperty("List", jList));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                           new JProperty("Status", returnValue), new JProperty("ErrorCode", "142"),
                           new JProperty("Message", "Contact Not created"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ManageWebContacts Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }

        public JObject UploadExcelData(string sConnString, int userID, DataTable dt,DataTable excelDuplicates, int listId, int mode, string listName)
        {
            JObject responseJObj = new JObject();
            JArray jar = new JArray();
            int returnValue = 0;
            DataSet ds = new DataSet();
            string returnMessage = "";
            JArray jarDup= new JArray();
            JObject jobjdup = new JObject();
            try
            {
              
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                ds = contactsObj.UploadExcelData(userID, dt,excelDuplicates, out returnValue, out returnMessage, listId,listName,mode);
                if (returnValue == 1)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            jar.Add(new JObject(new JProperty("ListId", ds.Tables[0].Rows[0]["ListId"]),
                                new JProperty("ListCount", ds.Tables[0].Rows[0]["ListCount"]),
                                new JProperty("ListName", ds.Tables[0].Rows[0]["ListName"])));
                         }
                        if (ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[1].Rows)
                                {
                                    jobjdup = new JObject();
                                    foreach (DataColumn dc in ds.Tables[1].Columns)
                                    {
                                        jobjdup.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));
                                    }
                                    jarDup.Add(jobjdup);
                                }
                            }
                        }

                    }

                    responseJObj = new JObject(new JProperty("Success", true),
                         new JProperty("Status", returnValue), new JProperty("ErrorCode", "117"),
                          new JProperty("Message", returnMessage), new JProperty("Details", jar), new JProperty("Duplicates", jarDup));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                           new JProperty("Status", returnValue), new JProperty("ErrorCode", "101"),
                           new JProperty("Message", returnMessage));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in UploadExcelData Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }

        public JObject AddWebContactList(string sConnString, int userID, string listName, int mode)
        {
            JObject responseJObj = new JObject();
            int returnValue = 0;
            string returnMessage = "";
            int listID = 0;
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                returnValue = contactsObj.AddWebContactList(userID, listName, mode, out returnMessage, out listID);
                if (returnValue == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                            new JProperty("Message", returnMessage), new JProperty("ErrorCode", "117"),
                             new JProperty("Status", returnValue.ToString()),
                             new JProperty("listIdOut", listID.ToString()));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AddWebContactList Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("AddWebListContacts", "WebListContact failed to Add"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }

        public JObject EditWebContactList(string sConnString, int userID, int listId, string listName, int mode)
        {
            JObject responseJObj = new JObject();
            int returnValue = 0;
            string returnMessage = "";
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                returnValue = contactsObj.EditWebContactList(userID, listId, listName, mode, out returnMessage);
                if (returnValue == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                            new JProperty("Message", returnMessage), new JProperty("ErrorCode", "117"),
                             new JProperty("Status", returnValue),
                             new JProperty("listId", listId));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in EditWebContactList BLL " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }



        public JObject AddAllContactsToParticularList(string sConnString, int userId, string contactId, int mode, int listId)
        {
            JObject responseJObj = new JObject();
            int returnValue = 0, listContactsCount = 0;
            string returnMessage = "";
            try
            {

                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                returnValue = contactsObj.AddAllContactsToParticularList(userId, listId, contactId, mode, out returnMessage, out listContactsCount);
                if (returnValue == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                            new JProperty("Message", returnMessage), new JProperty("returnValue", returnValue), new JProperty("ErrorCode", "117"),
                             new JProperty("ListContactsCount", listContactsCount),
                             new JProperty("Status", returnValue),
                             new JProperty("listId", listId));
                }
                else {

                    responseJObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", returnMessage), new JProperty("Status", returnValue), new JProperty("ErrorCode", "117"));
                              
                    
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AddAllContactsToParticularList Bll" + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("ErrorCode", "101"));
            }
            return responseJObj;


        }
        public JObject WebContactsList(string sConnString, int userID, int mode, int listId, int source, int pageIndex, string alphabetSort, string searchValue)
        {
            JObject responseJObj = new JObject();
            DataSet ds = new DataSet();
            JObject contactJobj = new JObject();
            JObject listJobj = new JObject();
            JObject contactListJobj = new JObject();
            JArray Jarr = new JArray();
            JArray JarrList = new JArray();
            JArray JarrContactList = new JArray();
            int returnValue = 0;
            string returnMessage = "";
            int pageCount = 0;
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                ds = contactsObj.WebContactsList(userID, mode, listId, source, pageIndex, alphabetSort, searchValue, out returnValue, out returnMessage, out pageCount);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow _row in ds.Tables[0].Rows)
                    {
                        contactJobj = new JObject();
                        foreach (DataColumn _column in ds.Tables[0].Columns)
                        {
                            contactJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                        }
                        Jarr.Add(contactJobj);
                    }

                }
                if (mode == 1)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        foreach (DataRow _row in ds.Tables[1].Rows)
                        {
                            listJobj = new JObject();
                            foreach (DataColumn _column in ds.Tables[1].Columns)
                            {
                                listJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                            }
                            JarrList.Add(listJobj);
                        }

                    }
                    //if (Ds.Tables[3].Rows.Count > 0)
                    //{

                    //    foreach (DataRow _row in Ds.Tables[3].Rows)
                    //    {
                    //        contactListJobj = new JObject();
                    //        foreach (DataColumn _column in Ds.Tables[3].Columns)
                    //        {
                    //            contactListJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                    //        }
                    //        JarrContactList.Add(contactListJobj);
                    //    }

                    //}
                }
                responseJObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", returnMessage),
                         new JProperty("Status", returnValue),
                        new JProperty("contactsData", Jarr),
                        new JProperty("contactListDetails", JarrList),
                        new JProperty("contactListData", JarrContactList));
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in WebContactsList Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("success", false),
                        new JProperty("message", "No contatcs found"));
            }
            return responseJObj;
        }
        public JObject DeleteListIdNListIdContacts(string sConnString, int userID, int listId, int mode)
        {
            JObject responseJObj = new JObject();
            int returnValue = 0;
            string returnMessage = "";
            try
            {

                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                returnValue = contactsObj.DeleteListIdNListIdContacts(userID, listId, mode, out returnMessage);
                if (returnValue == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                            new JProperty("Message", returnMessage), new JProperty("ErrorCode", "117"),
                             new JProperty("Status", returnValue),
                             new JProperty("listId", listId));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in DeleteListIdNListIdContacts Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false));
            }
            return responseJObj;
        }

        public JObject SaveContactToGroup(string sConnString, int userId, long groupID, string mobileNumber)
        {
            JObject responseJObj = new JObject();
            int retVal = 0;
            string retMsg = "";
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                retVal =contactsObj.SaveContactToGroup(userId,groupID,mobileNumber,out retMsg);
                if (retVal == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message","Contact Added"),
                          new JProperty("ErrorCode", "117"));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Contact Not Added"),
                          new JProperty("ErrorCode", "117"));
                }


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in SaveContactToGroup Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("ErrorCode", "101"));
            }

            return responseJObj;

        }

        public JObject AddNonMember(string sConnString, int userId, int listId, string moblie,int mode)
        {
            JObject responseJObj = new JObject();
            JArray nameArray = new JArray();
            JObject nameObj=new JObject();
            int retVal = 0;
            string retMsg = "";
            DataSet ds=new DataSet();
            try
            {
                DataAccessLayer.V_1_4.Contacts_V140 contactsObj = new DataAccessLayer.V_1_4.Contacts_V140(sConnString);
                ds = contactsObj.AddNonMember(userId, listId, moblie,mode,out retVal,out retMsg);
                if (retVal == 1)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                nameObj = new JObject();
                                foreach (DataColumn dc in ds.Tables[0].Columns)
                                { 
                                    nameObj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));
                                }  
                                nameArray.Add(nameObj);
                            }
                        }
                        
                    }
                    responseJObj = new JObject(new JProperty("Success", true),
                          new JProperty("ErrorCode", "117"), new JProperty("contactName", nameArray));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                         new JProperty("ErrorCode", "117"));
                }


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in addNonMember Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("ErrorCode", "101"));
            }

            return responseJObj;
        }
    }
}
