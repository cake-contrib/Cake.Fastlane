using Cake.Core;
using Cake.Core.Configuration;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using NSubstitute;
using Xunit;

namespace Cake.Fastlane.Tests
{
    public sealed class CakeContextFixture
    {
        public ICakeArguments Arguments { get; set; }
        public ICakeDataService DataService { get; set; }
        public ICakeEnvironment Environment { get; set; }
        public IFileSystem FileSystem { get; set; }
        public IGlobber Globber { get; set; }
        public ICakeLog Log { get; set; }
        public IProcessRunner ProcessRunner { get; set; }
        public IRegistry Registry { get; set; }
        public IToolLocator Tools { get; set; }
        public ICakeConfiguration Configuration { get; set; }

        public CakeContextFixture()
        {
            FileSystem = Substitute.For<IFileSystem>();
            Environment = Substitute.For<ICakeEnvironment>();
            Globber = Substitute.For<IGlobber>();
            Log = Substitute.For<ICakeLog>();
            Arguments = Substitute.For<ICakeArguments>();
            ProcessRunner = Substitute.For<IProcessRunner>();
            Registry = Substitute.For<IRegistry>();
            Tools = Substitute.For<IToolLocator>();
            DataService = Substitute.For<ICakeDataService>();
            Configuration = Substitute.For<ICakeConfiguration>();
        }

        public CakeContext CreateContext(PlatformFamily platform)
        {
            Environment.Platform.Family.Returns(platform);

            return new CakeContext(FileSystem, Environment, Globber,
                Log, Arguments, ProcessRunner, Registry, Tools, DataService, Configuration);
        }
    }
}