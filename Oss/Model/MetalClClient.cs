
//using MetalCl.Base;

//using Oss.Services;

//namespace Oss.Model
//{
//    internal class MetalClClient: RestClient
//    {
//        private const string jsonContentType_ = "application/json";

//        private const string vimsPath_ = "v1/openstack";
//        private const string vimPath_ = "v1/openstack/{0}";

//        private const string zonesPath_ = "v1/zones";
//        private const string zonePath_ = "v1/zones/{0}";

//        private const string projectsPath_ = "v1/projects";
//        private const string projectPath_ = "v1/projects/{0}";

//        public MetalClClient(IServiceScope scope) : base(scope.ServiceProvider.GetService<SettingsService>().MetalclHost, 
//                                                   scope.ServiceProvider.GetService<ILogger<MetalClClient>>()) { }

//        public async Task<IEnumerable<OpenStackInstance>> GetVims(string zoneId)
//        {
//            ArgumentNullException.ThrowIfNull(zoneId);

//            try
//            {
//                return await Get<IEnumerable<OpenStackInstance>>(GetUriWithQuery(vimsPath_, "zoneId", zoneId));
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetVims");

//                throw;
//            }
//        }

//        public async Task<OpenStackInstance> GetVim(string id)
//        {
//            ArgumentNullException.ThrowIfNull(id);

//            try
//            {
//                return await GetObjectById<OpenStackInstance>(vimPath_, id);
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetVim");

//                throw;
//            }
//        }

//        public async Task<IEnumerable<Zone>> GetZones()
//        {
//            try
//            {
//                return await Get<IEnumerable<Zone>>(GetUri(zonesPath_));
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetZones");

//                throw;
//            }
//        }

//        public async Task<Zone> GetZone(string id)
//        {
//            ArgumentNullException.ThrowIfNull(id);

//            try
//            {
//                return await GetObjectById<Zone>(zonePath_, id);
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetZone");

//                throw;
//            }
//        }

//        public async Task<IEnumerable<Project>> GetProjects()
//        {
//            try
//            {
//                return await Get<IEnumerable<Project>>(GetUri(projectsPath_));
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetProject");

//                throw;
//            }
//        }

//        public async Task<Project> GetProject(string id)
//        {
//            ArgumentNullException.ThrowIfNull(id);

//            try
//            {
//                return await GetObjectById<Project>(projectPath_, id);
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetProject");

//                throw;
//            }
//        }

//        private async Task<TObject> GetObjectById<TObject>(string basePath, string id)
//        {
//            try
//            {
//                return await Get<TObject>(GetUriIdInPath(basePath, id));
//            }
//            catch (Exception ex)
//            {
//                logger_.LogError(ex, "GetObjectById");

//                throw;
//            }
//        }
//    }
//}
