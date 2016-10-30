using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Contracts.Services.Infrastructure.DataExport;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Validation;
using Services.Infrastructure.DataExport.Properties;

namespace Services.Infrastructure.DataExport.Excel
{
    public class ExcelDataExporter : IDataExporter
    {
        public ExportSummary Export(IDataReader dataReader)
        {
            var tempFile = Path.GetTempFileName();
            File.WriteAllBytes(tempFile, Resources.template);

            var openSettings = new OpenSettings
            {
                MarkupCompatibilityProcessSettings = new MarkupCompatibilityProcessSettings(MarkupCompatibilityProcessMode.ProcessAllParts, FileFormatVersions.Office2007)
            };

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(tempFile, true, openSettings))
            {
                var workbookpart = spreadsheetDocument.WorkbookPart;
                var workbook = workbookpart.Workbook;

                workbook.RemoveAllChildren();

                spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets() { });

                var sheetData = AddSheet(spreadsheetDocument, "Screams");
                sheetData.Append(CreateRows(dataReader));

                IEnumerable<ValidationErrorInfo> errors;
                if (!IsDocumentValid(spreadsheetDocument, out errors))
                {
                    var sb = new StringBuilder("Document is NOT valid:" + Environment.NewLine);
                    foreach (var error in errors)
                    {
                        sb.Append(error.Description + Environment.NewLine);
                    }

                    throw new InvalidOperationException(sb.ToString());
                }
                workbook.Save();
            }

            return new ExportSummary
            {
                File = File.OpenRead(tempFile),
                ContentType = ContentType
            }; 
        }

        public string ContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        #region Private Methods

        static bool IsDocumentValid(OpenXmlPackage mydoc, out IEnumerable<ValidationErrorInfo> errors)
        {
            var validator = new OpenXmlValidator();
            errors = validator.Validate(mydoc);
            return (!errors.Any());
        }

        IEnumerable<Row> CreateRows(IDataReader dr)
        {
            var columns = new List<string>();

            for (int k = 0; k < dr.FieldCount; k++)
            {
                columns.Add(dr.GetName(k));
            }

            yield return CreateRow(columns, 1);

            var totalColumns = dr.FieldCount;

            var i = 2;
            while (dr.Read())
            {
                var values = new string[totalColumns];

                for (int j = 0; j < totalColumns; j++)
                {
                    var value = dr.GetValue(j);

                    values[j] = value == null ? String.Empty : value.ToString();
                }

                yield return CreateRow(values, i++);
            }
        }

        Row CreateRow(IEnumerable<string> values, int number)
        {
            var row = new Row { RowIndex = Convert.ToUInt32(number) };
            var dataType = new EnumValue<CellValues>(CellValues.String);

            row.Append(values.Select((x, i) =>
                new Cell
                {
                    CellReference = string.Concat(GetColumnName(i + 1), row.RowIndex),
                    CellValue = new CellValue(x),
                    DataType = dataType
                }));

            return row;
        }

        static string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = String.Empty;

            while (dividend > 0)
            {
                var modifier = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modifier) + columnName;
                dividend = (dividend - modifier) / 26;
            }

            return columnName;
        }

        static SheetData AddSheet(SpreadsheetDocument doc, string sheetName)
        {
            var worksheetPart = doc.WorkbookPart.AddNewPart<WorksheetPart>();
            var sheetData = new SheetData();
            var relationshipId = doc.WorkbookPart.GetIdOfPart(worksheetPart);

            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheets = doc.WorkbookPart.Workbook.GetFirstChild<Sheets>();

            var sheetId = 1U;
            if (sheets.Elements<Sheet>().Any())
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            sheets.Append(new Sheet
            {
                Id = relationshipId,
                SheetId = sheetId,
                Name = sheetName
            });

            return sheetData;
        }

        #endregion
    }
}
