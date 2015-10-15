using System.Collections.Generic;

namespace ORM.Results
{
    public class SelectResult<T> : Result
    {
        public IEnumerable<T> Values { get; set; }
    }
}
