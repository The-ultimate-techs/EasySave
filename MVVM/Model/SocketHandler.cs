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
        private bool close;
        public bool Receive;
        Socket newsock;


        private static SocketHandler instance = null;
        private static readonly object padlock = new object();

        private SocketHandler()
        {

            Sending = false;
            close = false; 



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


                if (Data2Send != null && Sending == true && client != null && close == false)
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


                if (Sending == false && client != null && close == false)
                {

                    string Flag = @"/!\STOPSENDING/!\";
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(Flag);

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

        public string ReceiveData()
        {

            //Déclaration d'un buffer de type byte pour enregistrer les données reçues
            byte[] data = new byte[10240];

            //appel de la méthode receive qui reçoit les données envoyées par le serveur et les stocker 
            //dans le tableau data, elle renvoie le nombre d'octet reçus

            if (Sending == true && client != null && client.Connected == true && close == false && Receive == true)
            {
                try
                {
                    int recv = client.Receive(data);
                }
                catch (SocketException exception)
                {
                    return null;
                }

                //transcodage de data en string
                return Encoding.UTF8.GetString(data);

            }else
            {
                return null;
            }


        }




        private void Listen()
        {

            while (true)
            {


                if (client == null)
                {
                    newsock.Listen(1);
                    client = newsock.Accept();

                }
                else
                {
                    if (client.Connected == false)
                    {
                        newsock.Listen(1);
                        client = newsock.Accept();
                    }
                }



            }


        }


    





        public void Close()
        {

            string closeMessage = @"/!\SERVEROFFLINE/!\";

            Sending = false;
            close = true;
            Thread.Sleep(200);
                   
            





            if (client != null)
            {



                byte[] byData = System.Text.Encoding.ASCII.GetBytes(closeMessage);

                try
                {
                    for (int i = 0; i < 150; i++)
                    {
                        client.Send(byData);
                        Thread.Sleep(25);
                    }

                }
                catch (SocketException exp)
                {
                    Console.WriteLine(exp.Message);

                }


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




    class SocketReceiver
    {
     
        public SocketReceiver()
        {



        }


    }
}
