using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/*namespace RefRecipe
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

                        foreach (Row row in worksheet.Descendants<Row>())
                        {
                            List<string> rowData = new List<string>();

                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                string cellValue = GetCellValue(cell, workbookPart);
                                rowData.Add(cellValue);
                            }

                            excelData.Add(rowData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return excelData;
        }

        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }
            else if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                int sharedStringIndex = int.Parse(cell.InnerText);
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                return sharedStringPart.SharedStringTable.ElementAt(sharedStringIndex).InnerText;
            }
            else
            {
                return cell.InnerText;
            }
        }
    }
} */

/*namespace RefRecipe
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

                        foreach (Row row in worksheet.Descendants<Row>())
                        {
                            List<string> rowData = new List<string>();
                            int columnCount = 0; // Seurataan luettujen sarakkeiden määrää

                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                string cellValue = GetCellValue(cell, workbookPart);
                                rowData.Add(cellValue);
                                columnCount++;

                                // Jos on luettu 11 saraketta, lopeta solujen lukeminen
                                if (columnCount == 11)
                                {
                                    break;
                                }
                            }

                            excelData.Add(rowData);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return excelData;
        }

        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }
            else if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                int sharedStringIndex = int.Parse(cell.InnerText);
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                return sharedStringPart.SharedStringTable.ElementAt(sharedStringIndex).InnerText;
            }
            else
            {
                return cell.InnerText;
            }
        }
    }
} */

namespace RefRecipe
{
    public class ExcelReader
    {
        public List<List<string>> ReadExcel(string filePath, int maxRows)
        {
            List<List<string>> excelData = new List<List<string>>();
            int rowCount = 0;

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

                        foreach (Row row in worksheet.Descendants<Row>())
                        {
                            if (rowCount >= maxRows)
                            {
                                break; 
                            }

                            List<string> rowData = new List<string>();
                            int columnCount = 0; 

                            foreach (Cell cell in row.Descendants<Cell>())
                            {
                                string cellValue = GetCellValue(cell, workbookPart);
                                rowData.Add(cellValue);
                                columnCount++;

                              
                                if (columnCount == 19)
                                {
                                    break;
                                }
                            }

                            excelData.Add(rowData);
                            rowCount++;

                            if (rowCount >= maxRows)
                            {
                                break; 
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return excelData;
        }

        private string GetCellValue(Cell cell, WorkbookPart workbookPart)
        {
            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }
            else if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                int sharedStringIndex = int.Parse(cell.InnerText);
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                return sharedStringPart.SharedStringTable.ElementAt(sharedStringIndex).InnerText;
            }
            else
            {
                return cell.InnerText;
            }
        }
    }
} 
