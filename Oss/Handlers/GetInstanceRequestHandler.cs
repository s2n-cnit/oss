
using LanguageExt.Common;
using MediatR;

using Oss.Requests;
using Oss.Model.Response;
using Oss.Services;

namespace Oss.Handlers
{
    public class GetInstanceRequestHandler: IRequestHandler<GetInstanceRequest, Result<VasInfo>>
    {
        private readonly DatabaseService db_;

        public GetInstanceRequestHandler(DatabaseService db)
        {
            db_ = db;
        }

        public Task<Result<VasInfo>> Handle(GetInstanceRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Result<VasInfo>(db_.GetVasInfo(request.InstanceId)));
        }
    }
}
