using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;
using System.IO;
using System.ComponentModel;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DataMerge
{
    /// <summary>
    /// Read in the user provided data
    /// </summary>
    public class DataReader
    {
        public DataReader()
        {
            TheirHeaders = new List<string>();
            OurHeaders = new List<string>();
            TheirDataList = new List<TheirData>();
            OurDataList = new List<OurData>();
        }

        public void PopulateTheirDataList(FileInfo theirFileInfo)
        {
            // Validate input exists
            if(!theirFileInfo.Exists)
            {
                throw new ArgumentException(
                    "The file name given to populate their data list doesn't exist.",
                    "theirFileInfo",
                    new Exception(
                        string.Format("Given File for Their Data: {0}", theirFileInfo)));
            }

            using (var sl = new SLDocument(theirFileInfo.FullName))
            {
#if True
                var theirDataCells = sl.GetCells();
                var sharedStrings = sl.GetSharedStrings();

                // Gather the headers
                TheirHeaders.Clear();
                var row = theirDataCells[1];
                for (int i = 1; i <= row.Count; i++)
                {
                    var cell = row[i];
                    TheirHeaders.Add(
                        ConvertCellToString(
                            cell,
                            sharedStrings)
                                .Trim()
                                .ToUpper());
                }

                TheirDataList.Clear();
                // Data starts at row 2
                for(int i = 2; i < theirDataCells.Count; i++)
                {
                    TheirDataList.Add(ConvertCellsToTheirData(i, theirDataCells, sharedStrings));
                }
#else
                {
                    var headerIndex = 1;
                    var row = 2;
                    var col = 1;
                    var value = sl.GetCellValueAsString(1, headerIndex);
                    var header = new List<string>();
                    var lineElements = new List<string>();

                    // Gather all the headers
                    while (!string.IsNullOrWhiteSpace(value))
                    {
                        header.Add(value.Trim().ToUpper());
                        value = sl.GetCellValueAsString(1, ++headerIndex);
                    }
                    TheirHeaders = new List<string>(header);

                    // Get the format handler setup
                    var dataReaderFormatHandler = new ClickAndImpressionFormatHandler();
                    // Add the Null Format Handler to tell us we can't process this file
                    dataReaderFormatHandler.SetSuccessor(new NullFormatHandler());

                    while (!string.IsNullOrWhiteSpace(sl.GetCellValueAsString(row, 1)))
                    {
                        while (col < headerIndex)
                        {
                            lineElements.Add(sl.GetCellValueAsString(row, col).Trim());
                            col++;
                        }

                        TheirDataList.Add(dataReaderFormatHandler.ReadLine(lineElements));

                        // Setup for next loop
                        row++;
                        col = 1;
                        lineElements.Clear();
                    }
                }
#endif
            }
        }

        public void PopulateOurDataList(FileInfo ourFileInfo)
        {
            // Validate input exists
            if (!ourFileInfo.Exists)
            {
                throw new ArgumentException(
                    "The file name given to populate our data list doesn't exist.",
                    "ourFileInfo",
                    new Exception(
                        string.Format("Given File for Our Data: {0}", ourFileInfo)));
            }

            using (var sl = new SLDocument(ourFileInfo.FullName))
            {
#if true
                var OurDataCells = sl.GetCells();
                var sharedStrings = sl.GetSharedStrings();

                // Gather the headers
                OurHeaders.Clear();
                var row = OurDataCells[1];
                for (int i = 1; i <= row.Count; i++)
                {
                    var cell = row[i];
                    OurHeaders.Add(
                        ConvertCellToString(
                            cell,
                            sharedStrings)
                                .Trim()
                                .ToUpper());
                }

                OurDataList.Clear();
                for (int i = 2; i < OurDataCells.Count; i++)
                {
                    OurDataList.Add(ConvertCellsToOurData(i, OurDataCells, sharedStrings));
                }
#else
                var headerIndex = 1;
                var row = 2;
                var value = sl.GetCellValueAsString(1, headerIndex);
                var header = new List<string>();

                // Gather all the headers
                while (!string.IsNullOrWhiteSpace(value))
                {
                    header.Add(value.Trim().ToUpper());
                    value = sl.GetCellValueAsString(1, ++headerIndex);
                }
                OurHeaders = new List<string>(header);

                // Validate headers match expected values
                if (header.Count < 3 ||
                    string.Compare(header[0], "id", true) != 0 ||
                    string.Compare(header[1], "name", true) != 0 ||
                    string.Compare(header[2], "TrackingID", true) != 0)
                {
                    throw new Exception(
                        string.Format(
                            "Failed to read Our Data file because the format didn't match. Header Format in file: {0}",
                            string.Join(",", header)));
                }

                while (!string.IsNullOrWhiteSpace(sl.GetCellValueAsString(row, 1)))
                {
                    var element = new OurNameAndTrackingIdData();
                    element.ID = sl.GetCellValueAsInt32(row, 1);
                    element.Name = sl.GetCellValueAsString(row, 2).ToUpper().Trim();
                    // For our purposes, a NULL string is effectively am empty string
                    // we don't want to search for "NULL" in their data over and over again
                    element.TrackingID = sl.GetCellValueAsString(row, 3).Replace("NULL", string.Empty).ToUpper().Trim();

                    OurDataList.Add(element);

                    // Setup for next loop
                    row++;
                }
#endif
            }
        }

        public void ClearTheirDataList()
        {
            TheirDataList.Clear();
        }

        public void ClearOurDataList()
        {
            OurDataList.Clear();
        }

        public List<TheirData> TheirDataList { get; private set; }

        public List<OurData> OurDataList { get; private set; }

        public List<string> TheirHeaders { get; private set; }

        public List<string> OurHeaders { get; private set; }

        private int ConvertCellToInt(SLCell cell)
        {
            // Check if it is a number
            if (cell != null &&
                cell.DataType == CellValues.Number &&
                cell.CellText == null)
            {
                return (int)cell.NumericValue;
            }

            throw new ArgumentException(
                "The given cell is not able to be converted to an Integer.",
                "SLCell cell",
                new Exception(
                    $"Given Cell: {cell.ToString()}"));
        }

        private double ConvertCellToDouble(SLCell cell)
        {
            // Check if it is a number
            if (cell != null &&
                cell.DataType == CellValues.Number &&
                cell.CellText == null)
            {
                return cell.NumericValue;
            }

            throw new ArgumentException(
                "The given cell is not able to be converted to a Double.",
                "SLCell cell",
                new Exception(
                    $"Given Cell: {cell.ToString()}"));
        }

        private string ConvertCellToString(SLCell cell, List<SLRstType> sharedStrings)
        {
            // Check if it is a number
            if (cell != null &&
                cell.DataType == CellValues.String &&
                cell.CellText != null)
            {
                return cell.CellText.ToUpper();
            }
            else if (cell != null &&
                cell.DataType == CellValues.SharedString)
            {
                return sharedStrings[(int)cell.NumericValue].GetText().ToUpper();
            }
            else if (cell != null &&
                cell.DataType == CellValues.Number)
            {
                return cell.NumericValue.ToString().ToUpper();
            }

            throw new ArgumentException(
                "The given cell is not able to be converted to a String.",
                "SLCell cell",
                new Exception(
                    $"Given Cell: {cell.ToString()}"));
        }

        private DateTime ConvertCellToDateTime(SLCell cell)
        {
            // Check if it is a number
            if (cell != null &&
                cell.DataType == CellValues.Date &&
                cell.CellText == null)
            {
                return DateTime.FromOADate(cell.NumericValue);
            }
            else if (cell != null &&
                cell.DataType == CellValues.Number)
            {
                return DateTime.FromOADate(cell.NumericValue);
            }

            throw new ArgumentException(
                "The given cell is not able to be converted to a DateTime.",
                "SLCell cell",
                new Exception(
                    $"Given Cell: {cell.ToString()}"));
        }

        private TheirClickAndImpressionData ConvertCellsToTheirData(int row, Dictionary<int, Dictionary<int, SLCell>> cells, List<SLRstType> sharedStrings)
        {
            var result = new TheirClickAndImpressionData();

            result.IndexString = ConvertCellToString(cells[row][1], sharedStrings);
            result.Clicks = ConvertCellToInt(cells[row][2]);
            result.Impressions = ConvertCellToInt(cells[row][3]);
            result.DateStamp = ConvertCellToDateTime(cells[row][4]);

            return result;
        }

        private OurNameAndTrackingIdData ConvertCellsToOurData(int row, Dictionary<int, Dictionary<int, SLCell>> cells, List<SLRstType> sharedStrings)
        {
            var result = new OurNameAndTrackingIdData();
            var cellRow = cells[row];

            if(cellRow.Keys.Contains(1))
            {
                result.ID = ConvertCellToInt(cellRow[1]);
            }
            if (cellRow.Keys.Contains(2))
            {
                result.Name = ConvertCellToString(cellRow[2], sharedStrings);
            }
            else
            {
                result.Name = string.Empty;
            }
            if (cellRow.Keys.Contains(3))
            {
                result.TrackingID = ConvertCellToString(cellRow[3], sharedStrings).Replace("NULL", "").Trim().ToUpper();
            }
            else
            {
                result.TrackingID = string.Empty;
            }

            return result;
        }
    }
}
