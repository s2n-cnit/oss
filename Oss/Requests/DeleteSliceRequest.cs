
using LanguageExt.Common;
using MediatR;

namespace Oss.Requests
{
    public class DeleteSliceRequest: IRequest<Result<string>>
    {
        public string SliceId { get; set; }
    }
}
