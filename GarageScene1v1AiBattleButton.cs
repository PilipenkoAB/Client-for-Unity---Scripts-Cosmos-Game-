using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GarageScene1v1AiBattleButton : MonoBehaviour
{
    private GameObject myCarrierObject; //cache

    public Button buttonConnect;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = buttonConnect.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;






        string message = "2;" + playerId + ";" + sessionToken + ";1"; //entering the garage - what 3 slots and what is first slot to put 3d

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            Debug.Log("answer - " + answer);


            // TEST BATTLE
            //  string message1 = "3;" + playerId + ";" + sessionToken + ";"+ answer; //

            // string answer1 = TcpConnection.Connect(message1);

            Debug.Log("answer from sessionID - " + answer);

            myCarrierObject.GetComponent<CacheData>().battleSessionId = Convert.ToInt32(answer);

            StartCoroutine(LoadSceneAsync.LoadYourAsyncScene("Battle1v1"));

            // answer with ID of the battle session. Load battle scene with parameters in cache
            // BattleSessionID
        }
        else {
            Debug.Log("ERROR WITH ANSWER - ");
        }

    }

}

