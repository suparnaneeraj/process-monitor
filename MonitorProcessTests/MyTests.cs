using System.Diagnostics;
using Applications;

namespace MonitorProcessTests
{
    [TestFixture]
    public class MyTests
    {
        MyApplication myApplication;
        public MyTests()
        {
            myApplication = new MyApplication();
        }

        //The process exceeds the allowed duration and therefore its been terminated.Verifies if the termimnatd process has been recorded in log file.
        [Test]
        public void MonitorProcesss_ProcessExceedsLifeTime()
        {
            Process process;
            String filedetails;
            File.WriteAllText("log.txt", string.Empty);
            process =Process.Start("/System/Applications/Notes.app/Contents/MacOS/Notes");
            Thread.Sleep(70000);
            myApplication.MonitoringProcess(process.ProcessName, 1, 1);
            filedetails = File.ReadAllText("log.txt");
            Assert.That(filedetails, Does.Contain(process.ProcessName));
        }

        //A process is started after starting the monitoring process and verifies if the process is terminated on rewaching the threshold
        [Test]
        public void MonitorProcesss_ProcessStartedInBetween()
        {
            Process process;
            String filedetails;
            File.WriteAllText("log.txt",string.Empty);
            myApplication.MonitoringProcess("Notes", 1, 1);
            process = Process.Start("/System/Applications/Notes.app/Contents/MacOS/Notes");
            Thread.Sleep(70000);
            myApplication.MonitoringProcess(process.ProcessName, 1, 1);
            filedetails = File.ReadAllText("log.txt");
            Assert.That(filedetails, Does.Contain(process.ProcessName));
        }

        //Verifies that no log is written for process that does not exist.
        [Test]
        public void MonitorProcess_ProcessDoesNotExist()
        {
            String filedetails;
            File.WriteAllText("log.txt", string.Empty);
            myApplication.MonitoringProcess("Notes", 1, 1);
            filedetails = File.ReadAllText("log.txt");
            Assert.That(filedetails, Is.Empty);
        }

        //Multiple instance of same process exist .Verifies if all the process gets terminated on reaching the threshold.
        [Test]
        public void MonitorProcess_MultipleProcessExist()
        {
            int noOfProcessAfterKilling;
            Process.Start("/System/Applications/Notes.app/Contents/MacOS/Notes");
            Process.Start("/System/Applications/Notes.app/Contents/MacOS/Notes");
            Thread.Sleep(120000);
            myApplication.MonitoringProcess("Notes", 2, 2);
            noOfProcessAfterKilling= Process.GetProcessesByName("Notes").Length;
            Assert.That(noOfProcessAfterKilling, Is.EqualTo(0));
        }
    }
}
