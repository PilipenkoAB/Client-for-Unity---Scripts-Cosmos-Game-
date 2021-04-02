using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoginSceneInitialize : MonoBehaviour
{

    private GameObject cacheData;


    public Text serverStatusText; // Text of server status - Online or offline

    public GameObject messageBox; //Text box if server status if offline
    public Text messageBoxText;
    public GameObject messageBoxButton;

    public InputField usernameInput; // Login input field
    public Toggle toggleRememberLogin; // toggle for remembering login

    // Start is called before the first frame update
    void Start()
    {



        // show login if toggle is true
        int rememberToggle = PlayerPrefs.GetInt("rememberToggle");
        if (rememberToggle == 1)
        {
            toggleRememberLogin.isOn = true;
            string rememberLogin = PlayerPrefs.GetString("rememberLogin");
            usernameInput.text = rememberLogin;
        }
        else
        {
            toggleRememberLogin.isOn = false;
        }

        // check if server is online 
        StartCoroutine(ServerCheck());
    }


    // Update is called once per frame (change resolution keys)
    void Update()
    {

        // Change resolution 
        if (Input.GetKeyDown("0"))
        {
            print("800, 600, false");
            Screen.SetResolution(800, 600, false);
        }
        if (Input.GetKeyDown("9"))
        {
            print("800, 600, true");
            Screen.SetResolution(800, 600, true);
        }
        if (Input.GetKeyDown("8"))
        {
            print("1024, 768, false");
            Screen.SetResolution(1024, 768, false);
        }
        if (Input.GetKeyDown("7"))
        {
            print("1024, 768, true");
            Screen.SetResolution(1024, 768, true);
        }
        if (Input.GetKeyDown("6"))
        {
            print("3840, 2160, true");
            Screen.SetResolution(3840, 2160, true);
        }
    }


    public IEnumerator ServerCheck()
    {
        if (TcpConnection.Connect("0") == "error")
        {
            serverStatusText.color = Color.red;
            serverStatusText.text = LocalizedText.Localize("Login_ServerStatusOffline");

            messageBox.SetActive(true);
          //  messageBoxText.text = "Server is offline!";
        }
        else
        {
            serverStatusText.color = Color.green;
            serverStatusText.text = LocalizedText.Localize("Login_ServerStatusOnline");
        }
            yield return null;
    }

}
