using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class TcpConnection : MonoBehaviour
{

    public static string Connect(string message)
    {
        int port = 13000;
        string ip = "127.0.0.1";

        string answer = "";
        try
        {
            TcpClient client = new TcpClient(ip, port);

            // Translate the passed message into ASCII and store it as a Byte array.
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);


            // Receive the TcpServer.response.

            // Buffer to store the response bytes.
            data = new Byte[612];

            // String to store the response ASCII representation.
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);

            answer = responseData;


            // Close everything.
            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Sending information error - " + e.ToString());
            answer = "error";
        }
        return answer;
    }


}
