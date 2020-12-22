using System;
using System.Collections.Generic;
using System.Text;

namespace AtDomain
{
    public class AtResult<T>
    {
        public AtResult()
        {

        }
        public AtResult(T payload)
        {
            IsOk = true;
            PayLoad = payload;
        }

        public AtResult(AtNotify error)
        {
            IsOk = false;
            Error = error;
        }


        public bool IsOk { get; set; }
        public AtNotify Error { get; }
        public List<AtApiError> ListError { get; set; }
        public T PayLoad { get; set; }

        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }

    public class AtApiError
    {
        public string Key { get; set; }
        public AtNotify Error { get; set; }
    }
}
