using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.IO;

namespace LW.Common.Tool
{
    public class ReportAction
    {
        /// <summary>
        /// 导出DataTable到Excel中
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="filepath"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable datatable, string filepath, out string error)
        {
            error = "";
            try
            {
                if (datatable == null)
                {
                    error = "DataTableToExcel:datatable 为空";
                    return false;
                }
                Aspose.Cells.License license = new Aspose.Cells.License();
                string licenseFile = ConfigurationManager.AppSettings["BASEDIRECTORY"].ToString() + "\\bin\\celllic.lic";
                license.SetLicense(licenseFile);


                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                Aspose.Cells.Worksheet sheet = workbook.Worksheets[0];
                Aspose.Cells.Cells cells = sheet.Cells;

                int nRow = 0;
                for (byte i = 0; i < datatable.Columns.Count; i++)
                {
                    cells[nRow, i].PutValue(datatable.Columns[i]);
                }
                Aspose.Cells.Range w;
                w = cells.CreateRange(nRow, 0, 1, datatable.Columns.Count);

                foreach (DataRow row in datatable.Rows)
                {
                    nRow++;
                    try
                    {
                        for (int i = 0; i < datatable.Columns.Count; i++)
                        {
                            if (row[i].GetType().ToString() == "System.Drawing.Bitmap")
                            {
                                //------插入图片数据-------
                                System.Drawing.Image image = (System.Drawing.Image)row[i];
                                MemoryStream mstream = new MemoryStream();
                                image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                sheet.Pictures.Add(nRow, i, mstream);
                            }
                            else
                            {
                                cells[nRow, i].PutValue(row[i]);
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                        error = error + " DataTableToExcel: " + e.Message;
                    }
                }

                if (filepath.IndexOf(".xls") > -1)
                {
                    workbook.Save(filepath);
                }
                else
                {
                    workbook.Save(filepath, Aspose.Cells.FileFormatType.CSV);
                }
                return true;
            }
            catch (System.Exception e)
            {
                error = error + " DataTableToExcel: " + e.Message;
                return false;
            }
        }
    }
}
