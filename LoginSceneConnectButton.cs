using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginSceneConnectButton : MonoBehaviour
{
    public Button buttonConnect;

    public InputField passwordInput;

    public GameObject messageBox; // error message box
    public Text messageBoxText; // error message box text

    public InputField usernameInput; // Login input field
    public Toggle toggleRememberLogin; // toggle for remembering login

    public Text serverStatus; // Text of server status - Online or offline

    static GameObject cacheObject;

    void Start()
    {
        Button btn = buttonConnect.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // when connection is correct
    void TaskOnClick()
    {
        if (toggleRememberLogin.isOn == true)
        {
            //SaveLogin
            PlayerPrefs.SetString("rememberLogin", usernameInput.text);
            PlayerPrefs.SetInt("rememberToggle", 1);
            PlayerPrefs.Save();
        } else if (toggleRememberLogin.isOn == false) {
            PlayerPrefs.SetString("rememberLogin", "");
            PlayerPrefs.SetInt("rememberToggle", 0);
            PlayerPrefs.Save();
        }



        // animation
        GetComponent<Animation>().Play("Connect_Button");

//-----------------------------------------

        if(serverStatus.text == "offline")
        {
            messageBox.SetActive(true);

        }
        else
        {
            string username = usernameInput.text;
            string password = passwordInput.text;
            if (username == "" || password == "")
            {
                username = "NULL";
                password = "NULL";

                messageBox.SetActive(true);
                messageBoxText.text = "EMPTY USERNAME OR PASSWORD";
            }
            else
            {
                string message = "1;" + username + ";" + password;

                try
                {
                    string answer = TcpConnection.Connect( message);
                    ProceessServerAnswer(answer);
                }
                catch (Exception e)
                {
                    Debug.Log("ERROR with sending information to the server" + e.ToString());

                    messageBox.SetActive(true);

                    messageBoxText.text = "Error connecting to the server";
                }
            }
        }
    }

    void ProceessServerAnswer(string answer) {

        string[] words = answer.Split(';');

        //  Debug.Log( words[1] );

        if (words[0] == "001") {
            Debug.Log("Unsucceseful login - wrong password");
            messageBox.SetActive(true);

            messageBoxText.text = "Unsucceseful: wrong password";
        }
        else if (words[1] != "")
        {
                cacheObject = GameObject.Find("CacheData");
                cacheObject.GetComponent<CacheData>().sessionToken = words[1];
                cacheObject.GetComponent<CacheData>().playerId = words[0];

                Debug.Log("in cache - new screne - playerId - " + words[0] + " ; session token - " + words[1]);

                // Assync load new scene
                StartCoroutine(LoadSceneAsync.LoadYourAsyncScene("Garage"));
         }
         else
         {
                Debug.Log("Unsucceseful login");
                messageBox.SetActive(true);

                messageBoxText.text = "Unsucceseful: login or password is wrong";
         }

        
    }

}
