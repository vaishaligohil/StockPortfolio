using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StockPortfolioDB
{
    public class ErrorDto
    {
        /// <summary>
        /// Application name of the application where the error occured.
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// The resource (i.e. endpoint) where the error occured.
        /// </summary>
        public string Resource { get; set; }

        public string ErrorCode { get; set; }

        public bool IsRetryable { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        public List<InternalErrorDto> ObjectErrors { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine().AppendFormat(CultureInfo.InvariantCulture, "Error code: {0}", ErrorCode).AppendLine();
            stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "Error message: {0}", Message).AppendLine();

            if (ObjectErrors != null)
            {
                stringBuilder.AppendLine("Error data:");
                foreach (InternalErrorDto obj in ObjectErrors)
                {
                    if (obj != null)
                    {
                        stringBuilder.AppendLine(obj.ErrorCode + ". " + obj.Message);
                    }
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
    }

    public class InternalErrorDto
    {
        public InternalErrorDto()
        {
            Parameters = new Dictionary<string, string>();
        }

        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public Dictionary<string, string> Parameters { get; set; }
    }
}
