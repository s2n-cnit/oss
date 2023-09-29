
using Oss.Model;

namespace Oss.Extensions
{
    public static class ExceptionExtensions
    {
        public static ErrorMsg MapToResponse(this Exception ex)
        {
            return new()
            {
                Message = ex.Message
            };
        }
    }
}
