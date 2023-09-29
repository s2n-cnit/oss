
using LanguageExt.Common;
using MediatR;

using Oss.Model.Nfvcl;
using Oss.Model.Response;

namespace Oss.Requests
{
    public class CreateNamespaceRequest: IRequest<Result<VasInfo>>
    {
        public string Id { get; set; }
        public Response Response { get; set; }
    }
}
