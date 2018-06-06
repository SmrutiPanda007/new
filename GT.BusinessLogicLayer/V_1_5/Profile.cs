using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using GT.Utilities;
using GT.DataAccessLayer;
using Newtonsoft.Json.Linq;
using System.Web;
using SD = System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;

namespace GT.BusinessLogicLayer.V_1_5
{
    public class Profile
    {
        public static string cur_path;
        public static string save_path;
        public static string image_name;
        public JObject GetProfileDetailsDisplay(string sConnString, int userId, int mode)
        {
            JObject jres = new JObject();
            JArray jar = new JArray();
            JArray jOffSet = new JArray();
            JObject tempJobj = new JObject();
            JObject offSetObj = new JObject();
            try
            {
                int retVal = 0;
                string retMessage = "";
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                DataSet ds = profileObj.GetDahsboardProfile(userId, mode, out retVal, out retMessage);
                if (retVal == 1)
                {
                    if (ds != null)
                    {
                        if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                        {
                            if (ds.Tables[0].Rows.Count != 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    tempJobj = new JObject();
                                    foreach (DataColumn dc in ds.Tables[0].Columns)
                                    {
                                        tempJobj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));

                                    }
                                    jar.Add(tempJobj);
                                }

                            }
                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count != 0)
                            {
                                if (ds.Tables[1].Rows.Count != 0)
                                {
                                    foreach (DataRow dr in ds.Tables[1].Rows)
                                    {
                                        offSetObj = new JObject();
                                        foreach (DataColumn dc in ds.Tables[1].Columns)
                                        {
                                            offSetObj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));

                                        }
                                        jOffSet.Add(offSetObj);
                                    }

                                }
                            }


                            jres = new JObject(new JProperty("Success", true),
                                new JProperty("Message", "OK"),
                                new JProperty("ErrorCode", "117"),
                                new JProperty("Profile", jar),
                                new JProperty("UtcOffSets", jOffSet));
                            return (jres);
                        }
                    }

                }
                else
                {
                    jres = new JObject(new JProperty("Success", false),
                         new JProperty("Message", retMessage), new JProperty("ErrorCode", "112"));
                    return (jres);
                }
            }
            catch (Exception ex)
            {

                jres = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("Exception in GetProfileDetailsDisplay BLL" + ex.StackTrace);
            }
            return jres;
        }

        public JObject RcAmountPieDiagram(string sConnString, int userId)
        {
            JObject jres = new JObject();
            JArray jar = new JArray();
            JArray jarNotifications = new JArray();
            try
            {
                int retVal = 0;
                string retMessage = "";
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                DataSet ds = profileObj.GetRechargeDetails(userId, out retVal, out retMessage);
                if (retVal == 1)
                {
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            jar.Add(new JObject(new JProperty("RcAmount", ds.Tables[0].Rows[0]["rcamount"]),
                           new JProperty("CurrentBal", ds.Tables[0].Rows[0]["currentbalance"]),
                           new JProperty("AppAmount", ds.Tables[0].Rows[0]["Appusedamnt"]),
                           new JProperty("WebUsedAmount", ds.Tables[0].Rows[0]["WebUsedAmount"]),
                           new JProperty("DefaultLines", ds.Tables[0].Rows[0]["Defaultlines"]),
                           new JProperty("MaxLinesUsed", ds.Tables[0].Rows[0]["linesperuser"])));

                        }
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                jarNotifications.Add(new JObject(new JProperty("Notfytype", ds.Tables[1].Rows[i]["NotificationType"]),
                               new JProperty("NotificationMsg", ds.Tables[1].Rows[i]["Notificationmessage"]),
                               new JProperty("InsertedTime", ds.Tables[1].Rows[i]["Time"])));

                            }
                        }


                        jres = new JObject(new JProperty("Success", true),
                            new JProperty("Message", "OK"), new JProperty("ErrorCode", "117"),
                            new JProperty("RechargeDetails", jar),
                            new JProperty("Notifications", jarNotifications));
                        return (jres);
                    }

                }
                else
                {
                    jres = new JObject(new JProperty("Success", false),
                         new JProperty("Message", retMessage), new JProperty("ErrorCode", "101"));
                    return (jres);
                }
            }
            catch (Exception ex)
            {

                jres = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.StackTrace));
                Logger.ExceptionLog("Exception in RcAmountPieDiagram BLL" + ex.StackTrace);
            }
            return jres;
        }
        public JObject SetUserUtcOffSet(string sConnString, int userId, int mode, string offSetValue)
        {
            JObject jres = new JObject();

            try
            {
                int retVal = 0;
                string retMessage = "";
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                retVal = profileObj.SetUserUtcOffSetTime(userId, mode, offSetValue, out retMessage);
                if (retVal == 1)
                {
                    jres = new JObject(new JProperty("Success", true),
                          new JProperty("Message", retMessage), new JProperty("ErrorCode", "117"));
                    return (jres);

                }
                else
                {
                    jres = new JObject(new JProperty("Success", false),
                         new JProperty("Message", retMessage), new JProperty("ErrorCode", "101"));
                    return (jres);
                }
            }
            catch (Exception ex)
            {

                jres = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.StackTrace));
                Logger.ExceptionLog("Exception in RcAmountPieDiagram BLL" + ex.StackTrace);
            }
            return jres;
        }





        public JObject CropWLeader(string p, string saveaPath, string readPath, string imgName, int w, int h, int x, int y, int fixWidth, HttpContext con, int mode, int userID)
        {
            JObject jres = new JObject();
            save_path = HttpContext.Current.Server.MapPath("/TempImages/");
            image_name = imgName;
            string saveTo = "";
            string resp_str = string.Empty;
            try
            {
                byte[] CropImage = Crop(saveaPath, w, h, x, y);
                using (MemoryStream ms = new MemoryStream(CropImage, 0, CropImage.Length))
                {
                    ms.Write(CropImage, 0, CropImage.Length);
                    using (SD.Image CroppedImage = SD.Image.FromStream(ms, true))
                    {
                        //Dim SaveTo As String = cur_path & img_name
                        saveTo = save_path + imgName;

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

                        resp_str = imgName;
                        jres = ProfileUpdateCropSave(p, userID, mode, imgName, con);


                    }
                }
                return jres;
            }
            catch (Exception ex)
            {
                jres = new JObject(new JProperty("Success", false),
                       new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.StackTrace));
                Logger.ExceptionLog("Exception in CropWLeader BLL" + ex.StackTrace);
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

        public JObject ProfileUpdateCropSave(string sConnString, int userId, int mode, string tempCropImagePath, HttpContext contxt)
        {
            JObject jres = new JObject();
            JArray jar = new JArray();
            MemoryStream mStream = new MemoryStream();

            try
            {

                int retVal = 0;
                string resMgs = "";
                if (tempCropImagePath.Trim() != string.Empty)
                {
                    tempCropImagePath = "TempImages/" + tempCropImagePath;
                }
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                //byte[] imgCropArray = ReadFile(tempCropImagePath);;
                byte[] imgCropArray = null;
                DataSet ds = profileObj.SetProfileDetailsUpdate(userId, mode, out retVal, out resMgs, contxt, imgCropArray, tempCropImagePath);
                if (retVal == 1)
                {
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            jar.Add(new JObject(new JProperty("Mobile", ds.Tables[0].Rows[0]["MobileNumber"]),
                           new JProperty("Nname", ds.Tables[0].Rows[0]["NickName"]),
                           new JProperty("Mail", ds.Tables[0].Rows[0]["EmailId"]),
                           new JProperty("Display", ds.Tables[0].Rows[0]["Displaypicpath"])));

                        }


                        jres = new JObject(new JProperty("Success", true),
                            new JProperty("Message", resMgs), new JProperty("ErrorCode", "117"),
                            new JProperty("Profile", jar));
                        return (jres);


                    }

                    return jres;

                }


                else
                {
                    jres = new JObject(new JProperty("Success", false),
                         new JProperty("Message", "Updation Failed"), new JProperty("ErrorCode", "101"));
                    return (jres);
                }
            }
            catch (Exception ex)
            {

                jres = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("Exception in ProfileUpdateCropSave BLL" + ex.StackTrace);
            }
            return jres;
        }
        public JObject ProfileUpdateNameEmail(string sConnString, int userId, int mode, HttpContext contxt)
        {
            JObject jres = new JObject();
            JArray jar = new JArray();
            MemoryStream mStream = new MemoryStream();

            try
            {

                int retVal = 0;
                string resMgs = "";

                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                DataSet ds = profileObj.SetProfileNameEmailUpdate(userId, mode, out retVal, out resMgs, contxt);
                if (retVal == 1)
                {
                    if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {

                            jar.Add(new JObject(new JProperty("Mobile", ds.Tables[0].Rows[0]["MobileNumber"]),
                           new JProperty("Nname", ds.Tables[0].Rows[0]["NickName"]),
                           new JProperty("Mail", ds.Tables[0].Rows[0]["EmailId"]),
                           new JProperty("Display", ds.Tables[0].Rows[0]["Displaypicpath"])));

                        }

                    }
                    jres = new JObject(new JProperty("Success", true),
                            new JProperty("Message", resMgs), new JProperty("ErrorCode", "117"),
                            new JProperty("Profile", jar));
                    return (jres);


                }


                else
                {
                    jres = new JObject(new JProperty("Success", false),
                         new JProperty("Message", "Updation Failed"), new JProperty("ErrorCode", "101"));
                    return (jres);
                }
            }
            catch (Exception ex)
            {

                jres = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("Exception in ProfileUpdateNameEmail BLL" + ex.StackTrace);
            }
            return jres;
        }
        public JObject PhoneContactsSync(string sConnString, JObject paramObj, int UserID, string deviceUniqueID, string deviceName)
        {
            JObject respnseJObj = new JObject();
            DataTable phoneContacts = new DataTable();
            DataTable editContacts = new DataTable();
            DataTable secondaryContacts = new DataTable();
            DataTable editSecondaryContacts = new DataTable();
            string name = "";
            string deviceContactID = "";
            string mobileNumbers = "";
            string mobileTypes = "";
            Image _image = null;
            MemoryStream mStream = new MemoryStream();
            byte[] byteArr;
            string contactImageStoragePath = "";
            string contactImageName = "";
            string contactImagePath = "";
            string deleteContacts = "";
            JObject responseJobj = new JObject();
            JObject responseJobj2 = new JObject();
            JArray responseJarr = new JArray();
            int retVal = 0;
            string retMsg = "";
            List<string> liMobileNumbers = new List<string>();
            List<string> liMobileTypes = new List<string>();
            List<string> liEditMobileNumbers = new List<string>();
            List<string> liEditMobileTypes = new List<string>();

            int secondaryRow = 1;
            int editSecondaryRow = 1;
            contactImageStoragePath = "ContactImages/";

            phoneContacts.Columns.Add("Name", typeof(string));
            phoneContacts.Columns.Add("MobileNumbers", typeof(string));
            phoneContacts.Columns.Add("MobileNumberTypes", typeof(string));
            phoneContacts.Columns.Add("DeviceContactID", typeof(string));
            phoneContacts.Columns.Add("ContactImagePath", typeof(string));

            secondaryContacts.Columns.Add("Id", typeof(int));
            secondaryContacts.Columns.Add("MobileNumbers", typeof(string));
            secondaryContacts.Columns.Add("MobileNumberTypes", typeof(string));
            secondaryContacts.Columns.Add("DeviceContactID", typeof(string));

            editContacts.Columns.Add("Name", typeof(string));
            editContacts.Columns.Add("MobileNumbers", typeof(string));
            editContacts.Columns.Add("MobileNumberTypes", typeof(string));
            editContacts.Columns.Add("DeviceContactID", typeof(string));
            editContacts.Columns.Add("ContactImagePath", typeof(string));

            editSecondaryContacts.Columns.Add("Id", typeof(int));
            editSecondaryContacts.Columns.Add("MobileNumbers", typeof(string));
            editSecondaryContacts.Columns.Add("MobileNumberTypes", typeof(string));
            editSecondaryContacts.Columns.Add("DeviceContactID", typeof(string));
            Logger.TraceLog("Phone COntacts Synch " + paramObj.ToString());
            try
            {
                foreach (JObject _Member in (JArray)paramObj.SelectToken("Contacts"))
                {
                    foreach (JProperty _Token in _Member.Properties())
                    {
                        if (_Token.Name == "contactName")
                        {
                            name = _Token.Value.ToString();
                        }
                        if (_Token.Name == "mobileNumbers")
                        {
                            mobileNumbers = _Token.Value.ToString();



                        }
                        if (_Token.Name == "mobileTypes")
                        {
                            mobileTypes = _Token.Value.ToString();

                        }
                        if (_Token.Name == "deviceContactId")
                        {
                            deviceContactID = _Token.Value.ToString();
                        }
                        if (_Token.Name == "contactImage")
                        {
                            if (_Token.Value.ToString() != "")
                            {
                                try
                                {
                                    contactImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + deviceContactID + ".jpg";
                                    contactImagePath = contactImageStoragePath + contactImageName;
                                    byteArr = Convert.FromBase64String(_Token.Value.ToString().Replace(" ", "+"));
                                    mStream = new MemoryStream(byteArr);
                                    _image = Image.FromStream(mStream);
                                    _image.Save(HttpContext.Current.Server.MapPath("/ContactImages/") + contactImageName);
                                }
                                catch (Exception ex)
                                {
                                    Logger.TraceLog("exception Sync :" + ex.ToString());
                                }
                            }
                            else
                            {
                                contactImagePath = "";
                            }

                        }
                    }
                    var tuple = RemoveDuplicatesFromArray(mobileNumbers, mobileTypes, mobileNumbers.Split(',')[0]);
                    liMobileNumbers = tuple.Item1;
                    liMobileTypes = tuple.Item2;
                    phoneContacts.Rows.Add(name, mobileNumbers.Split(',')[0], mobileTypes.Split(',')[0], deviceContactID, contactImagePath);
                    for (int contacts = 0; contacts < liMobileNumbers.Count(); contacts++)
                    {

                        secondaryContacts.Rows.Add(secondaryRow++, liMobileNumbers[contacts], liMobileTypes[contacts], deviceContactID);

                    }
                }
                foreach (JObject _Member in (JArray)paramObj.SelectToken("EditContacts"))
                {
                    foreach (JProperty _Token in _Member.Properties())
                    {
                        if (_Token.Name == "contactName")
                        {
                            name = _Token.Value.ToString();
                        }
                        if (_Token.Name == "mobileNumbers")
                        {
                            mobileNumbers = _Token.Value.ToString();
                        }
                        if (_Token.Name == "mobileTypes")
                        {
                            mobileTypes = _Token.Value.ToString();
                        }
                        if (_Token.Name == "deviceContactId")
                        {
                            deviceContactID = _Token.Value.ToString();
                        }
                        if (_Token.Name == "contactImage")
                        {
                            if (_Token.Value.ToString() != "")
                            {
                                try
                                {
                                    contactImageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + deviceContactID + ".jpg";
                                    contactImagePath = contactImageStoragePath + contactImageName;
                                    byteArr = Convert.FromBase64String(_Token.Value.ToString().Replace(" ", "+"));
                                    mStream = new MemoryStream(byteArr);
                                    _image = Image.FromStream(mStream);
                                    _image.Save(HttpContext.Current.Server.MapPath("/ContactImages/") + contactImageName);
                                }
                                catch (Exception ex)
                                {
                                    Logger.TraceLog("exception Sync :" + ex.ToString());
                                }
                            }
                            else
                            {
                                contactImagePath = "";
                            }

                        }
                    }
                    editContacts.Rows.Add(name, mobileNumbers, mobileTypes, deviceContactID, contactImagePath);
                    var tuple = RemoveDuplicatesFromArray(mobileNumbers, mobileTypes, mobileNumbers.Split(',')[0]);
                    liMobileNumbers = tuple.Item1;
                    liMobileTypes = tuple.Item2;
                    for (int contacts = 0; contacts < liMobileNumbers.Count(); contacts++)
                    {

                        editSecondaryContacts.Rows.Add(editSecondaryRow++, liMobileNumbers[contacts], liMobileTypes[contacts], deviceContactID);

                    }
                }
                deleteContacts = paramObj.SelectToken("DeleteContacts").ToString();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In parsing contacts PhoneContactsSync BLL :" + ex.ToString());
                responseJobj = new JObject(new JProperty("Status", true), new JProperty("Success", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }

            try
            {
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                retVal = profileObj.PhoneContactsSync(phoneContacts, secondaryContacts, editContacts, editSecondaryContacts, deleteContacts, deviceUniqueID, deviceName, UserID, Convert.ToInt32(paramObj.SelectToken("IsFirstTime")), out retMsg);
                if (retVal == 1)
                {
                    responseJobj = new JObject(new JProperty("Status", true), new JProperty("Success", "Success"), new JProperty("ErrorCode", "117"));
                }
                else
                {
                    responseJobj = new JObject(new JProperty("Status", false), new JProperty("Success", retMsg), new JProperty("ErrorCode", "112"));
                }
            }
            catch (Exception ex)
            {
                responseJobj = new JObject(new JProperty("Status", false), new JProperty("Success", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("Exception In PhoneContactsSync BLL :" + ex.ToString());
            }

            return responseJobj;
        }
        public JObject UserBalance(string sConnString, int userId)
        {
            JObject jObj = new JObject();
            string retMsg = "";
            int retVal = 0;
            double balance;
            DataSet ds = new DataSet();
            try
            {
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                ds = profileObj.UserBalance(userId, out retMsg, out retVal, out balance);
                if (retVal == 1)
                {
                    jObj = new JObject(new JProperty("Success", true),
                                     new JProperty("Message", retMsg), new JProperty("ErrorCode", "117"),
                                     new JProperty("UserBalance", balance));


                }
                else
                {

                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", retMsg), new JProperty("ErrorCode", "112"));
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ProfileBusiness.UserBalance is ==>" + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.ToString()));

            }

            return jObj;
        }


        public JObject ProfileImage(string profileImg, string tempStoragePath, string tempFileName, string sConnString, int userId)
        {
            JObject jObj = new JObject();
            Image _image = null;
            MemoryStream mStream = new MemoryStream();
            byte[] byteArr, data;
            DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
            int result;
            string imagePath = "";
            try
            {
                if (profileImg != "")
                {
                    byteArr = Convert.FromBase64String(profileImg.Replace(" ", "+"));
                    mStream = new MemoryStream(byteArr);
                    _image = Image.FromStream(mStream);
                    _image.Save(tempStoragePath + tempFileName);

                    data = ReadFile(tempStoragePath + tempFileName);
                    imagePath = ConfigurationManager.AppSettings["WebUrl"].ToString() + "TempImages/" + tempFileName.ToString() + "";
                }
                else
                {
                    data = new byte[] { };
                }
                result = profileObj.ProfileImage(userId, tempFileName, data);
                if (result > 0)
                {

                    jObj = new JObject(new JProperty("Status", true),
                                     new JProperty("Message", "Success"), new JProperty("ErrorCode", "114"),
                                     new JProperty("imagepath", imagePath));
                }
                else
                {

                    jObj = new JObject(new JProperty("Status", false),
                                     new JProperty("Message", "Failed"), new JProperty("ErrorCode", "115"));
                }
            }
            catch (Exception ex)
            {
                jObj = new JObject(new JProperty("Status", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.ToString()));
                Logger.ExceptionLog("Exception in ProfileBusiness.ProfileImage is ==>" + ex.ToString());
            }

            return jObj;

        }
        public JObject UpdateProfile(string sConnString, int userID, string nickName, string emailID, string offSet, string company)
        {
            JObject resultJobj = new JObject();
            JObject tempObj = new JObject();
            int retVal = 0, errorCode = 0;
            string retMsg = "";
            DataSet ds = new DataSet();
            try
            {
                DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
                ds = profileObj.UpdateProfile(userID, nickName, emailID, offSet, company, out retVal, out retMsg, out errorCode);
                if (retVal == 1)
                {

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        resultJobj = new JObject(new JProperty("Success", true),
                                           new JProperty("Message", "Success"), new JProperty("ErrorCode", errorCode),
                                           new JProperty("userid", ds.Tables[0].Rows[0]["UserID"]),
                                            new JProperty("Nickname", ds.Tables[0].Rows[0]["Nickname"]),
                                            new JProperty("EmailID", ds.Tables[0].Rows[0]["EmailID"]),
                                            new JProperty("Company", ds.Tables[0].Rows[0]["Company"]),
                                            new JProperty("imagepath", ConfigurationManager.AppSettings["WebUrl"].ToString() + ds.Tables[0].Rows[0]["DisplayPicPath"]),
                                            new JProperty("Currency", ds.Tables[0].Rows[0]["CurrencyName"]),
                                            new JProperty("RequestedAmount", ds.Tables[0].Rows[0]["PaidAmount"]),
                                            new JProperty("RequestedMinutes", ds.Tables[0].Rows[0]["AddedAmount"]),
                                            new JProperty("InAppPurchase", true),
                                            new JProperty("SupportEmailID", "hello@grptalk.com"));

                    }


                }
                else
                {
                    resultJobj = new JObject(new JProperty("Success", false),
                                             new JProperty("Message", "User Not Available"), new JProperty("ErrorCode", "112"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in UpdateProfile BLL :" + ex.ToString());
                resultJobj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"),
                    //new JProperty("Message", ex.ToString()),
                                         new JProperty("ErrorCode", "101"));
            }

            return resultJobj;
        }
        private byte[] ReadFile(string filePath)
        {
            byte[] data = null;

            FileInfo fileInformation = new FileInfo(filePath);
            long numOfBytes = fileInformation.Length;
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            BinaryReader _binaryReader = new BinaryReader(fStream);
            data = _binaryReader.ReadBytes(int.Parse(numOfBytes.ToString()));
            return data;
        }

        public JObject GetCountries(string sConnString)
        {
            JObject countriesJObj = new JObject();
            JObject TempJobj = new JObject();
            JArray CountriesJarr = new JArray();
            JObject OffSetJobj = new JObject();
            JArray offSetJarr = new JArray();
            DataSet Ds = new DataSet();
            DataAccessLayer.V_1_5.Profile profileObj = new DataAccessLayer.V_1_5.Profile(sConnString);
            String retMsg = "";
            Ds = profileObj.GetCountries(out retMsg);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow _row in Ds.Tables[0].Rows)
                {
                    TempJobj = new JObject();
                    foreach (DataColumn _column in Ds.Tables[0].Columns)
                    {
                        TempJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                    }
                    CountriesJarr.Add(TempJobj);
                }
                if (Ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow _row in Ds.Tables[1].Rows)
                    {
                        OffSetJobj = new JObject();
                        foreach (DataColumn _column in Ds.Tables[1].Columns)
                        {
                            OffSetJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                        }
                        offSetJarr.Add(OffSetJobj);
                    }
                }
                countriesJObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", retMsg), new JProperty("ErrorCode", "117"),
                        new JProperty("Countries", CountriesJarr), new JProperty("OffSetCountries", offSetJarr));
            }
            return countriesJObj;
        }


        private Tuple<List<string>, List<string>> RemoveDuplicatesFromArray(string mobileNos, string mobileTypes, string firstMobileNo)
        {
            var tz = TimeZoneInfo.GetSystemTimeZones();
            List<string> liMobileNos = new List<string>();
            List<string> liMobileTypes = new List<string>();
            string[] mobilenosArray = mobileNos.Split(',');
            string[] mobiletpesArray = mobileTypes.Split(',');
            BusinessHelper _helper = new BusinessHelper();
            _helper.GetOnlyNumeric(ref firstMobileNo);
            for (int i = 0; i < mobilenosArray.Length; i++)
            {
                _helper.GetOnlyNumeric(ref mobilenosArray[i]);
                if (!liMobileNos.Contains(mobilenosArray[i]) && firstMobileNo.Trim('+') != mobilenosArray[i].Trim('+') && mobilenosArray[i].Trim() != string.Empty)
                {

                    liMobileNos.Add(mobilenosArray[i]);
                    liMobileTypes.Add(mobiletpesArray[i]);
                }
            }


            return Tuple.Create(liMobileNos, liMobileTypes);
        }
    }


}
