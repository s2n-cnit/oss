
using LanguageExt.Common;
using MediatR;

using Oss.Requests;
using Oss.Model.Response;
using Oss.Services;

namespace Oss.Handlers
{
    public class GetInstancesRequestHandler: IRequestHandler<GetInstancesRequest, Result<IEnumerable<VasInfo>>>
    {
        private readonly DatabaseService db_;

        public GetInstancesRequestHandler(DatabaseService db)
        {
            db_ = db;
        }

        public Task<Result<IEnumerable<VasInfo>>> Handle(GetInstancesRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new Result<IEnumerable<VasInfo>>(db_.GetVasInfos()));
        }
    }
}
