using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpreadsheetLight;

namespace DataMerge
{
    /// <summary>
    /// Data Format Handler implemented as a Chain of Responsibility
    /// </summary>
    public abstract class DataReaderFormatHandler
    {
        protected DataReaderFormatHandler _successor;

        public void SetSuccessor(DataReaderFormatHandler successor)
        {
            _successor = successor;
        }

        public abstract void HandleFormatRequest(List<string> headers);

        public abstract TheirData ReadLine(List<string> line);
    }

    /// <summary>
    /// Click and Impression Concrete handler for the Chain of Responsibility
    /// </summary>
    public class ClickAndImpressionFormatHandler : DataReaderFormatHandler
    {
        public override void HandleFormatRequest(List<string> headers)
        {
            // If we handle this type of request then do so here
            if (headers.Count >= 4 &&
                string.Compare(headers[0], "Creative", true) == 0 &&
                string.Compare(headers[1], "Clicks", true) == 0 &&
                string.Compare(headers[2], "Impressions", true) == 0 &&
                string.Compare(headers[3], "DateStamp", true) == 0)
            {
                // we meet the criteria so remain at this handler
            }
            else if (_successor != null)
            {
                _successor.HandleFormatRequest(headers);
            }
        }

        /// <summary>
        /// This will return a TheirData concrete implementation with Clicks and Impressions
        /// </summary>
        /// <param name="lineElements">The parts of the line in a list</param>
        /// <returns>TheirClickAndImpressionData</returns>
        public override TheirData ReadLine(List<string> lineElements)
        {
            var theirData = new TheirClickAndImpressionData();

            // Convert this string to Upper so that searching can be completed
            // with a known case and to remove some of the inconsistency in the
            // data we take in
            theirData.IndexString = lineElements[0].ToUpper();
            theirData.Clicks = Int32.Parse(lineElements[1]);
            theirData.Impressions = Int32.Parse(lineElements[2]);
            theirData.DateStamp = DateTime.FromOADate(Int32.Parse(lineElements[3]));

            return theirData;
        }
    }

    /// <summary>
    /// This is the end of the Chain of Responsibility.
    /// This means that either we don't know how to process this file and it will need to be documented or
    /// that one of our previous Chain elements has failed to handle something it should have
    /// </summary>
    public class NullFormatHandler : DataReaderFormatHandler
    {
        public override void HandleFormatRequest(List<string> headers)
        {
            throw new NotImplementedException(
                "Failed to parse the data reader format.",
                new Exception(
                    String.Format(
                        "Headers which failed to parse: {0}",
                        String.Join(",", headers))));
        }

        public override TheirData ReadLine(List<string> line)
        {
            throw new NotImplementedException(
                String.Format(
                    "After failing to read the headers, a readline was called with the following line: {0}",
                    String.Join(",", line)));
        }
    }
}
