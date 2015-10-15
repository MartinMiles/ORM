using ORM.Results;

namespace ORM.Results
{
    public class Result
    {
        public string ErrorMessage { get; private set;  }

        public Status Status { get; set; }

        internal void SetError(string errorText)
        {
            ErrorMessage = errorText;
            Status = Status.Error;
        }
    }

}
