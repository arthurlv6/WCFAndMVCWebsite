using Project.Website.ServiceAccess;
using StructureMap;
using StructureMap.Graph;

namespace Project.Website {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });
                            x.For<IVehicleContainer>().Use<VehicleContainer>();
                        });
            return ObjectFactory.Container;
        }
    }
}