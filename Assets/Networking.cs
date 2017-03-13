using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

public class Networking {
  private string serverAddress = "127.0.0.1";
  private int serverPort = 50175;
  private TcpClient client;
  //private StreamReader streamReader;
  private StreamWriter streamWriter;
  private bool serverExisits;
  public Networking()
  {

  }

  public void SendNewSolution(char gameM, uint target, string solution)
  {

    try
    {
      Socket sock = null;
      IPHostEntry hostEntry = Dns.GetHostEntry(serverAddress);
      foreach(IPAddress add in hostEntry.AddressList)
      {
        IPEndPoint ipe = new IPEndPoint(add, serverPort);
        sock = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        sock.Connect(ipe);

        if (sock.Connected)
        {
          break;
        }
      }
      if (!sock.Connected) return;


      byte[] greeting = { 0x01, 0xf2, 0x83, 0xe1, 0xa3, 0x44, 0xda, 0xb7, 0xaf, 0xf4, 0xd6, 0x69, 0xca, 0xc9, 0x01, 0x00 };

      byte[] gameMode = { Convert.ToByte(gameM) };
      
      byte[] targetNumber = BitConverter.GetBytes(target);
      Array.Reverse(targetNumber);

      byte[] solutionLength = { Convert.ToByte(solution.Length) };

      byte[] solutionBytes = Encoding.UTF8.GetBytes(solution);

      byte[] msg = new byte[greeting.Length + gameMode.Length + targetNumber.Length + solutionLength.Length + solutionBytes.Length];
      System.Buffer.BlockCopy(greeting,       0, msg, 0,                                                                                greeting.Length);
      System.Buffer.BlockCopy(gameMode,       0, msg, greeting.Length,                                                                  gameMode.Length);
      System.Buffer.BlockCopy(targetNumber,   0, msg, greeting.Length + gameMode.Length,                                                targetNumber.Length);
      System.Buffer.BlockCopy(solutionLength, 0, msg, greeting.Length + gameMode.Length + targetNumber.Length,                          solutionLength.Length);
      System.Buffer.BlockCopy(solutionBytes,  0, msg, greeting.Length + gameMode.Length + targetNumber.Length + solutionLength.Length,  solutionBytes.Length);
      sock.Send(msg);
    }
    catch (SocketException ex)
    {
      Debug.Log("Could not connect to server.\n" + ex.ToString());
      serverExisits = false;
    }
  }
}
