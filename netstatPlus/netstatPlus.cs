using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace netstatPlus
{
     public class Connection
        {

        public string remoteaddy;
        public string remoteport;

        public string localaddy;
        public string localport;

        public string pid;
        public string name;
        public string state;
        public string protocol;
       }

    public class netclient
    {
        public static List<Connection> GetGetByID(string id)
        {
            var output = GetConnections(ID: id);
            return output;

        }


        public static List<Connection> GetGetByName(string name)
        {
            var output = GetConnections(NAME: name);
            return output;

        }

        public string remoteaddy;
        public string remoteport;

        public string localaddy;
        public string localport;

        public string pid;
        public string name;
        public string state;
        public string protocol;

        static readonly Regex trimmer = new Regex(@"  +");


        public static string[] states =
        {
            "CLOSE_WAIT",
            "CLOSED",
            "ESTABLISHED",
            "FIN_WAIT_1",
            "FIN_WAIT_2",
            "LAST_ACK",
            "LISTEN",
            "SYN_RECEIVED",
            "SYN_SEND",
            "TIMED_WAIT",

        };

        public static List<Connection> GetConnections(string[] STATES = null, string PROTOCOL = "none", string NAME = "none", string ID = "none")
        {
            if(STATES == null)
            {
                STATES = states;

            }

            Process p = new Process();
            StreamReader sr;

            ProcessStartInfo startInfo = new ProcessStartInfo("netstat");
            startInfo.Arguments = "-n -a -o";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            p.StartInfo = startInfo;
            p.StartInfo.Verb = "runas";
            p.Start();

            sr = p.StandardOutput;

            List<Connection> connections = new List<Connection>();

            using (sr)
            {
                string line;
                
                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {

                    
                    line = trimmer.Replace(line, " ");
                    

                    if (line != "" && !line.Contains("*") && !line.Contains("Proto") && !line.Contains("Connections") && !line.Contains("["))
                    {






                        line = line.Remove(0, 1);





                        string[] args = line.Split(' ');

                        Connection connection = new Connection();

                        string[] local = args[1].ToString().Split(':');


                        string[] remote = args[2].ToString().Split(':');

                        connection.localaddy = local[0];
                        connection.localport = local[1];

                        connection.remoteaddy = remote[0];
                        connection.remoteport = remote[1];

                        connection.state = args[3];

                        connection.pid = args[4];

                        connection.protocol = args[0];

                        Process process = Process.GetProcessById(Int32.Parse(args[4]));

                        connection.name = process.ProcessName;

                        bool passed = false;




                        foreach (var statee in STATES)
                        {

                            if (statee.ToUpper() == args[3].ToUpper())
                            {
                                passed = true;
                            }
                        }

                        if (PROTOCOL != "none")
                        {
                            switch (PROTOCOL.ToUpper())
                            {
                                case "TCP":
                                    if (args[0] != "TCP")
                                    {
                                        passed = false;
                                    }
                                    break;


                                case "UDP":
                                    if (args[0] != "UDP")
                                    {
                                        passed = false;
                                    }
                                    break;

                            }
                        }

                        if (NAME != "none")
                        {
                            if (NAME != connection.name)
                            {
                                passed = false;
                            }

                        }


                        if (ID != "none")
                        {
                            if (ID != connection.pid)
                            {
                                passed = false;
                            }

                        }


                        if (passed)
                        {
                            connections.Add(connection);
                        }







                    }
                    }
                    catch
                    {
                        throw new InvalidOperationException("Got error\n" + line);

                    }
                }
            }

            return connections;

        }


    }

}
