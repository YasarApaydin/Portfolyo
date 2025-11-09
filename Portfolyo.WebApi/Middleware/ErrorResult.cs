using Newtonsoft.Json;

namespace Portfolyo.WebApi.Middleware
{
    public class ErrorResult
    {

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
