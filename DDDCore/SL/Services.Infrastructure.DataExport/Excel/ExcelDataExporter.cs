using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Contracts.Services.Infrastructure.DataExport;
using Crosscutting.Infrastructure;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Validation;
using Services.Infrastructure.DataExport.Properties;

namespace Services.Infrastructure.DataExport.Excel
{
    public class ExcelDataExporter : DataExporterBase
    {
        #region Public Methods

        public override ExportSummary Export(object[] objects)
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
                sheetData.Append(CreateRows(objects));

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
                ContentType = ExportType.Excel.GetDescription()
            }; 
        }

        #endregion

        #region Private Methods

        static bool IsDocumentValid(OpenXmlPackage mydoc, out IEnumerable<ValidationErrorInfo> errors)
        {
            var validator = new OpenXmlValidator();
            errors = validator.Validate(mydoc);
            return (!errors.Any());
        }

        IEnumerable<Row> CreateRows(object[] objects)
        {
            var columns = GetHeaders(objects.First());

            for (int i = 1; i <= objects.Length; i++)
            {
                yield return CreateRow(columns, i);
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
