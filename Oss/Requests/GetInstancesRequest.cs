
using LanguageExt.Common;
using MediatR;

using Oss.Model.Response;

namespace Oss.Requests
{
    public class GetInstancesRequest: IRequest<Result<IEnumerable<VasInfo>>>
    {
        public string[] Fields { get; set; }
    }
}
