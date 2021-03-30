﻿using System;
using System.Collections.Generic;
using System.Text;
using MWFModelsLibrary.Enums;
using MWFModelsLibrary.Models;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net.Sockets;
using System.Net;

namespace ClusterHandlerLibrary
{
    public class Cluster
    {
        public List<GameInstanceModel> GameInstances { get; set; } // should this be a list? what type of operations will we be doing with this
    

        public GameInstanceModel SpinUp(Game game, string args)
        {
            string localIp;
            try
            {
                localIp = GetMachineIP();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            GameInstanceModel newGameInstance = new GameInstanceModel
            {
                Id = -1,                // -1 for now until the database creates an entry for this game instance and returns its Id
                Game = game,
                Port = "-1",            // -1 for now until we assign it
                Args = args,
                HostId = 0              // Come up with a way for this host to know its id
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))                                          // LINUX
            {
                Console.WriteLine("Hello Linux!");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))                                   // WINDOWS
            {
                switch (newGameInstance.Game)
                {
                    case Game.Game0:
                        /*Process.Start(new ProcessStartInfo() { Arguments = newGameInstance.Args });*/
                        //gameInstanceProcess.Exited += new EventHandler(gameInstance_Exited);
                        //gameInstanceProcess = Process.Start(_gameFilePathsOptions.ALSReplicated, gameInstance.Args);
                        //Process.Start(_gameFilePathsOptions.ALSReplicated, newGameInstance.Args);
                        break;
                    case Game.Game1:
                        break;
                }
            }

            return newGameInstance;
        }

        private static string GetMachineIP()
        {
            string localIp;
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIp = endPoint.Address.ToString();
            }

            return localIp;
        }
    }
}