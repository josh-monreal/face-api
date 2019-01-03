using FA.Business;
using FA.Business.Core;
using FA.Business.Utilities;
using FA.External;
using FA.External.APIs;
using FA.External.Core;
using Unity;

namespace FA.CompositionRoot
{
    public class DependencyInjection<T>
    {
        private readonly UnityContainer _container;

        public DependencyInjection()
        {
            _container = RegisterDependencies();
        }

        public UnityContainer RegisterDependencies()
        {
            var container = new UnityContainer();

            #region Business Layer
            container.RegisterType<IPersonGroupLogic, PersonGroupLogic>();
            container.RegisterType<IPersonLogic, PersonLogic>();
            container.RegisterType<IFaceLogic, FaceLogic>();
            container.RegisterType<IResponseHelper, ResponseHelper>();
            #endregion

            #region External Layer
            container.RegisterType<IHttpHelper, HttpHelper>();
            container.RegisterType<IPersonGroupAPI, PersonGroupAPIs>();
            container.RegisterType<IPersonAPI, PersonAPIs>();
            container.RegisterType<IFaceAPI, FaceAPIs>();
            #endregion

            return container;
        }

        public T Resolve()
        {
            return _container.Resolve<T>();
        }
    }
}
