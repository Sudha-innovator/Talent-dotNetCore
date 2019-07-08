﻿using ExcelDataReader;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using RelevantCodes.ExtentReports;
using NUnit.Framework;

namespace TalentFrameWork.Global
{
    class Definition
    {
     
        //Initialise the browser
        public static IWebDriver driver { get; set; }

        // Declare ExtentReports variables
        public static ExtentTest test;
        public static ExtentReports extent;

        [SetUp]
        public void initialise()
        {
            extent = new ExtentReports(ReadJson().ReportPath, false, DisplayOrder.NewestFirst);
            extent.LoadConfig(ReadJson().ReportXmlPath);
        }

        [TearDown]
        public void tearDown()
        {
            //end reports
            extent.EndTest(test);
            extent.Flush();


        }

            public static DefineJson ReadJson()
            {
                DefineJson JsonData = new DefineJson();
                var WebClient = new WebClient();
                var json = WebClient.DownloadString(@"C:\Users\sudha\Desktop\MyProjects\VS\ProjectTalent\ProjectTalent\TalentFrameWork\Global\JsonResourceFile.json");
                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                JsonData.ExcelPath = result.Urls.ExcelPath;
                JsonData.Screenshot = result.Urls.ScreenShot;
                JsonData.ReportPath = result.Urls.ReportPath;
                JsonData.ReportXmlPath = result.Urls.ReportXmlPath;
                return JsonData;

            }


            public class ExcelOperations
            {
                static List<DataCollection> dataCol = new List<DataCollection>();
                private static DataTable ExcelToDataTable(string filename, string sheetName)
                {
                    try
                    {
                        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                        FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read);

                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        //excelReader.IsFirstRowAsColumnNames = true; //Does not work any more

                        DataSet resultSet = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                        {
                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }

                        });

                        DataTableCollection table = resultSet.Tables;
                        DataTable resultTable = table[sheetName];
                        return resultTable;
                    }
                    catch (Exception)
                    {
                        throw;
                    }


                }
                public class DataCollection
                {

                    public int rowNumber { get; set; }
                    public string colName { get; set; }
                    public string colValue { get; set; }
                }


                // static List<DataCollection> dataCol = new List<DataCollection>();

                public static void ClearData()
                {
                    dataCol.Clear();
                }

                public static void PopulateInCollection(string filename, string sheetName)
                {
                    ExcelOperations.ClearData();
                    DataTable table = ExcelToDataTable(filename, sheetName);
                    for (int row = 1; row <= table.Rows.Count; row++)
                    {
                        for (int col = 0; col < table.Columns.Count; col++)
                        {
                            DataCollection dtTable = new DataCollection()
                            {
                                rowNumber = row,
                                colName = table.Columns[col].ColumnName,
                                colValue = table.Rows[row - 1][col].ToString()

                            };
                            dataCol.Add(dtTable);
                        }
                    }
                }



                public static string ReadData(int rowNumber, string columnName)
                {
                    try
                    {

                    //Retriving Data using LINQ to reduce much of iterations

                      rowNumber = rowNumber - 1;
                      string data = (from colData in dataCol
                                   where colData.colName == columnName && colData.rowNumber == rowNumber
                                   select colData.colValue).SingleOrDefault();

                      
                      return data.ToString();
                    }
                    catch (Exception e)
                    {
                        return null;

                    }
                }

            }

            #region screenshots
            public class SaveScreenShotClass
            {
                public static string SaveScreenshot(IWebDriver driver, string ScreenShotFileName) // Definition
                {
                    var folderLocation = (ReadJson().Screenshot);

                    if (!System.IO.Directory.Exists(folderLocation))
                    {
                        System.IO.Directory.CreateDirectory(folderLocation);
                    }

                    var screenShot = ((ITakesScreenshot)driver).GetScreenshot();
                    var fileName = new StringBuilder(folderLocation);

                    fileName.Append(ScreenShotFileName);
                    fileName.Append(DateTime.Now.ToString("_dd-mm-yyyy_mss"));
                    //fileName.Append(DateTime.Now.ToString("dd-mm-yyyym_ss"));
                    fileName.Append(".png");
                    screenShot.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Png);
                    return fileName.ToString();
                }
            }
            #endregion
        }
    }


