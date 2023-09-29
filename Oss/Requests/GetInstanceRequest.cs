
using LanguageExt.Common;
using MediatR;

using Oss.Model.Response;

namespace Oss.Requests
{
    public class GetInstanceRequest: IRequest<Result<VasInfo>>
    {
        public string InstanceId { get; set; }
        public string[] Fields { get; set; }
    }
}
