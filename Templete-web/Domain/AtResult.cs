using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
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

        public AtResult(Notify error)
        {
            IsOk = false;
            Error = error;
        }


        public bool IsOk { get; set; }
        public Notify Error { get; }
        public List<AtApiError> ListError { get; set; }
        public T PayLoad { get; set; }
    }

    public class AtApiError
    {
        public string Key { get; set; }
        public Notify Error { get; set; }
    }
}
