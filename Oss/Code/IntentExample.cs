
using System.Text.Json;

using Swashbuckle.AspNetCore.Filters;

using Oss.Model.Request;

namespace Oss.Code
{
    public class IntentExample : IExamplesProvider<SliceIntent>
    {
        private readonly string filePath_ = Path.Combine(AppContext.BaseDirectory, "Intent.json");

        public SliceIntent GetExamples()
        {
            return JsonSerializer.Deserialize<SliceIntent>(File.ReadAllText(filePath_));
        }
    }
}
