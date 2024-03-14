using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RefRecipe
{
    public class ExcelReader
    {
        public List<List<string>> ReadExcel(string filePath)
        {
            List<List<string>> excelData = new List<List<string>>();

            try
            {
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    IEnumerable<Sheet> sheets = workbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                    foreach (Sheet sheet in sheets)
                    {
                        WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                        Worksheet worksheet = worksheetPart.Worksheet;
                        SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;

                        foreach (Row row in worksheet.Descendants<Row>())
                        {
                            List<string> rowData = new List<string>();

                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                string cellValue = GetCellValue(cell, sharedStringPart);
                                rowData.Add(cellValue);
                            }

                            excelData.Add(rowData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Virhe: {ex.Message}");
                // Voit lisätä tähän tarkemman virheenkäsittelyn tarpeen mukaan
            }

            return excelData;
        }

        private string GetCellValue(Cell cell, SharedStringTablePart sharedStringPart)
        {
            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }
            else if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                int sharedStringIndex = int.Parse(cell.InnerText);
                return sharedStringPart.SharedStringTable.ElementAt(sharedStringIndex).InnerText;
            }
            else
            {
                return cell.InnerText;
            }
        }
    }
}

