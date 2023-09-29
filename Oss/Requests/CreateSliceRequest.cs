

using LanguageExt.Common;
using MediatR;

using Oss.Model.Request;

namespace Oss.Requests
{
    public class CreateSliceRequest: IRequest<Result<string>>
    {
        public SliceIntent Intent { get; set; }
    }
}
