/* C# Utility to monitor a process and kill the processes that work longer than the threshold specified*/

using System.Diagnostics;
    
    namespace Applications
    {
        public class MyApplication
        {
            public static void Main(String[] args)
            {
                string processName = args[0];
                int maxLifetimeMinutes = int.Parse(args[1]);
                int monitoringFrequencyMinutes = int.Parse(args[2]);
                MyApplication myApplication = new MyApplication();
                Console.WriteLine("Starting the MonitorProcess app ");
                Console.WriteLine("Press Q or q to stop the monitoring process");
                while (true)
                {
                    //The control exits from loop when the key Q or q is pressed.
                    if (Console.KeyAvailable && Console.ReadKey().Key==ConsoleKey.Q)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Exiting......");
                        break;
                    }
                    //Calling the method to check if the process has exceeded the threshold.
                    myApplication.MonitoringProcess(processName, maxLifetimeMinutes, monitoringFrequencyMinutes);
                    Console.WriteLine($"Waiting for {monitoringFrequencyMinutes} minute(s)");
                    Thread.Sleep(monitoringFrequencyMinutes * 60 * 1000);
                }
            }

            public void MonitoringProcess(string processName , int maxLifeTimeMinutes, int monitoringFrequencyMinutes)
            {
                Process[] processes;
                //Open the log file to which the terminated process has to be recorded
                FileStream file = new FileStream("log.txt", FileMode.OpenOrCreate);
                StreamWriter writer = new StreamWriter(file);
                processes = Process.GetProcessesByName(processName);
                // Checks if the process with processName exists.If no process exists then waiting continues.
                if (processes.Length == 0)
                {
                    Console.WriteLine($"Currently process {processName} does not exist , waiting for {monitoringFrequencyMinutes} minute(s).");
                }
                else
                {
                //Each process with process name processName is verified if the allowed threshold is reached. If threshold is reached then the process is killed.
                    foreach (Process process in processes)
                    {
                        if ((DateTime.Now - process.StartTime).TotalMinutes > maxLifeTimeMinutes)
                        {
                            Console.WriteLine($"The process {processName} has exceeded the allowed duration ,therefore terminating the process");
                            // Method call to kill the process
                            KillProcess(process);
                            //Method call to write the terminated process to log file
                            Log($"The process {processName} has been terminated", writer);
                        }
                    }
                }
                writer.Close();
                file.Close();

            }

            public void KillProcess(Process process)
            {
                process.Kill();
            }

            public  void Log(string logMessage, TextWriter w)
            { 
                w.Write($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                w.Write("     ");
                w.WriteLine($"{logMessage}");
            }
        }
    }
