namespace Application.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class BaseResult<TResult> where TResult : class
    {
        public BaseResult(TResult result = default, IDictionary<string, string> errors = default)
        {
            this.Result = result;
            this.Errors = errors ?? new Dictionary<string, string>();
        }

        public TResult Result { get; set; }
        public bool Success => !this.Errors.Any() && this.Result != null;
        public IDictionary<string, string> Errors { get; set; }
    }
}