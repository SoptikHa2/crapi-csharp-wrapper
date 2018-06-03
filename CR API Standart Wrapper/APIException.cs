using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CRAPI
{
    [Serializable]
    public class APIException : Exception
    {
        /// <summary>
        /// HTTP code of exception
        /// </summary>
        public int Status { get { return (int)Data["status"]; } set { } }
        /// <summary>
        /// Exception description
        /// </summary>
        public override string Message { get { return Data["message"].ToString(); } }

        public APIException()
        {
        }

        public APIException(string message) : base(message)
        {
        }

        public APIException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public APIException(int status, string message, bool isError)
        {
            Data["status"] = status;
            Data["message"] = message;
            Data["isError"] = isError;
        }

        protected APIException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override string ToString()
        {
            return Data["message"].ToString();
        }
    }
}
