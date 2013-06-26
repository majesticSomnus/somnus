using System;

namespace Somnus.Common.Util
{
    [Serializable]
    public class GetRequestError : Exception
    {
        public GetRequestError()
            : base()
        {
        }

        public GetRequestError(string msg)
            : base(msg)
        {
        }

        public GetRequestError(string msg, Exception inner)
            : base(msg, inner)
        {
        }
    }
}
