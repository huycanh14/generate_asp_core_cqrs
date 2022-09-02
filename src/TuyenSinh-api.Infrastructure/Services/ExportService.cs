using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClosedXML.Excel;
using TuyenSinh_api.Application.Contracts.Infrastructure;
using TuyenSinh_api.Domain.Common;

namespace TuyenSinh_api.Infrastructure.Services
{
    public class ExportService : IExportService
    {
        public byte[] ExportDataToExcel<T>(DataCsvExport<T> sheet)
            where T : CommonExportVm
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add(sheet.Title);
                worksheet.Cell(1, 1).Value = sheet.Title;

                // Tạo dòng header
                for (int i = 0; i < sheet.RowHeader.Count(); i++)
                {
                    worksheet.Cell(2, i + 1).Value = sheet.RowHeader[i].Value;
                }

                // Tạo các dòng dữ liệu
                var currentRow = 3;
                if (sheet.RowsData.Count() > 0)
                {
                    PropertyInfo[] propList = sheet.RowsData.First().GetType().GetProperties();
                    foreach (var item in sheet.RowsData)
                    {
                        for (int i = 0; i < sheet.RowHeader.Count(); i++)
                        {
                            if (propList[i].PropertyType == typeof(string))
                            {
                                worksheet.Cell(currentRow, i + 1).SetDataType(XLDataType.Text);
                                worksheet.Cell(currentRow, i + 1).SetValue(propList[i].GetValue(item));
                            }
                            else
                                worksheet.Cell(currentRow, i + 1).Value = propList[i].GetValue(item);
                        }

                        currentRow++;
                    }
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;

                }

            }
        }
    }
}
