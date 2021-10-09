using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace StockPortfolioDB
{
    [Serializable]
    public class ApiHttpException : Exception
    {
        public ApiHttpException(HttpStatusCode statusCode, string errorCode, string message, Exception ex = null)
            : this(statusCode, new ErrorDto { ErrorCode = errorCode, Message = message, IsRetryable = false }, ex)
        {
        }

        public ApiHttpException(HttpStatusCode statusCode, ErrorDto error, Exception ex = null) : base(error.ToString(), ex)
        {
            Error = error;
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; internal set; }

        public ErrorDto Error { get; }

        #region Serializable

        protected ApiHttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);
        }

        #endregion
    }
}
