using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using GT.Utilities.Properties;
using GT.Utilities;
namespace GrpTalk
{
    /// <summary>
    /// Summary description for CustomisedXLUpload
    /// </summary>
    public class ExcelUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            JObject jObj = new JObject();
            try
            {

                if (context.Request.Files.Count > 0)
                {
                    System.Web.HttpPostedFile file = context.Request.Files[0];
                    string fileName = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
                    string extension = Path.GetExtension(file.FileName);

                    if (!ValidateFileName(fileName))
                    {
                        jObj = new JObject(new JProperty("Status", false), new JProperty("Message", "File name should not contain  i) Morethan one dot ii) Comma ."));
                        context.Response.Write(jObj);
                        return;
                    }

                    if (extension != ".xlsx" && extension != ".xls")
                    {
                        jObj = new JObject(new JProperty("Status", false),
                                         new JProperty("Message", "Invalid File.Please upload xlsx or xls files"));
                        context.Response.Write(jObj);
                        return;
                    }

                    if (!(file.ContentType == "application/vnd.ms-excel") && file.ContentType == "application/excel" && file.ContentType == "application/x-msexcel")
                    {
                        jObj = new JObject(new JProperty("Success", false),
                                         new JProperty("Message", "Invalid excel file . Please check once"));
                        context.Response.Write(jObj);
                        return;
                    }

                    string timeSpan = DateTime.Now.ToString("ddMMyyhhmmss");
                    string folderPath = "", filePath = ""; ;
                    folderPath = context.Server.MapPath("~/ExcelFilesUpload/");
                    filePath = folderPath + fileName + "_" + timeSpan + extension;
                    file.SaveAs(filePath);

                    JArray jArr = new JArray();
                    jArr = ReadExcelData(filePath);
                    if (jArr == null || jArr.Count() == 0)
                    {
                        jObj = new JObject(new JProperty("Success", false),
                                        new JProperty("Message", "First Row Should not be empty"));
                    }
                    else
                    {
                        jObj = new JObject(new JProperty("Success", true),
                                           new JProperty("FilePath", fileName + "_" + timeSpan + extension),
                                           new JProperty("ExcelSheets", jArr));
                    }
                }
                else
                {
                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", "No file.Please select atleast one file "));
                }
            }
            catch (Exception ex)
            {
                jObj = new JObject(new JProperty("Success", false),
                                   new JProperty("Message", "Server Error"));
                Logger.ExceptionLog("Exception in ExcelUpload.ashx " + ex.ToString());
                // GT.Utilities.Logger.Error("Exception in CustomisedXLUpload.ashx is ==" + ex.ToString());
            }

            context.Response.Write(jObj);
            return;

        }

        private bool ValidateFileName(string fileName)
        {
            bool isValid = true;
            try
            {
                //if (fileName.Contains("..") || fileName.Contains(',') || fileName.Contains('.'))
                if (fileName.Substring(fileName.Length-1) == ".")
                    isValid = false;
                else
                    isValid = true;

            }
            catch (Exception ex)
            {
                isValid = false;
                Logger.ExceptionLog("Exception in ExcelUpload.ashx " + ex.ToString());
            }

            return isValid;
        }


        public JArray ReadExcelData(string file)
        {
            XSSFWorkbook workBook = new XSSFWorkbook();
            Dictionary<string, XSSFSheet> sheets = new Dictionary<string, XSSFSheet>();
            string[] columns = { };
            string sheetName = "";
            ISheet _Sheet = null;
            int columnsCount = 0;
            string columnName = "";
            JArray jArr = new JArray();
            JArray headersArr = new JArray();
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                workBook = new XSSFWorkbook(fileStream);
            }
          
                sheetName = workBook[0].SheetName;
                _Sheet = workBook.GetSheet(sheetName);
                columnName = "";
                if (_Sheet.PhysicalNumberOfRows != 0)
                {
                    if (_Sheet.GetRow(0) == null)
                    {
                        columnsCount = 0;
                    }
                    else
                    {
                        columnsCount = _Sheet.GetRow(0).PhysicalNumberOfCells;
                        
                    }
                    
                    if (columnsCount != 0)
                    {
                        for (int j = 0; j <= columnsCount - 1; j++)
                        {
                            columnName = _Sheet.GetRow(0).Cells[j].ToString();
                            
                            headersArr.Add(new JObject(new JProperty("header", columnName)));
                        }
                        jArr.Add(new JObject(new JProperty("SheetName", sheetName),
                                            new JProperty("ColumnsCount", columnsCount),
                                            new JProperty("Header", headersArr)));
                    }

                }
            
            return jArr;
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