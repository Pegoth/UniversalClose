using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UniversalClose.BuildTasks.Tests
{
    [TestClass]
    public class ProjectLauncherUpdaterTaskTests
    {
        [TestMethod]
        public void RunTest()
        {
            File.Copy(Path.Combine("..", "..", "..", "..", "UniversalClose", "Properties", "launchSettings.json"), "launchSettings.json", true);
            new ProjectLauncherUpdaterTask {LaunchSettingsPath = "launchSettings.json"}.Execute();
        }
    }
}