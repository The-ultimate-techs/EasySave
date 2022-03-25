using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace EasySave.MVVM.Model
{
    public sealed class SocketHandler
    {

    
        public Socket client { get; set; }
        public string Data2Send { get; set; }
        public bool Sending;
        Socket newsock;


        private static SocketHandler instance = null;
        private static readonly object padlock = new object();

        private SocketHandler()
        {

            Sending = false;



            //Création du point de communication avec adresse IP locale et un numéro de port

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1050);

            // Création du socket d'écoute

            newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // relier le socket au point de communication
            newsock.Bind(ipep);

            //créer un nouveau thread qui écoute le réseau et accepte les demandes du client
            Thread t = new Thread(new ThreadStart(Listen));
            t.Start();




            Thread SendingThread= new Thread(new ThreadStart(SendingFunction));
            SendingThread.Start();

        }


        private void SendingFunction()
        {
           

            while (true)
            {

                Thread.Sleep(500);


                if (Data2Send != null && Sending == true && client != null)
                {

                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(Data2Send);

                    try
                    {
                        
                        client.Send(byData);
                        
                    }
                    catch (SocketException exp)
                    {
                        Console.WriteLine(exp.Message);

                    }
                }


            }


        }






        private void Listen()
        {
            newsock.Listen(1);
            client = newsock.Accept();


        }


        public void synchronization()
        {
            if (client !=null)
            {

                Thread ListenThread = new Thread(new ThreadStart(Listen));
                ListenThread.Start();

            }
        }


     





        public void Close()
        {


            
            if (client != null)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }

            
           
        }

  


        public static SocketHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SocketHandler();
                        }
                    }
                }
                return instance;
            }
        }
    }

}
