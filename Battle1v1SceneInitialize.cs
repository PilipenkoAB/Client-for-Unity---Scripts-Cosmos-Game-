using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Battle1v1SceneInitialize : MonoBehaviour
{
    private GameObject myCarrierObject; //cache


    private string[] bigModulesType = new string[] {"", "", "", "", ""};
    // UI
    private Text playerHealthCurrent;
    private Text playerHealthMax;

    private Text uiEnemyHealthMax;
    private Text uiEnemyHealthCurrent;

    private Text playerEnergyCurrent;
    private Text playerEnergyMax;

    private Text enemyDamageRecieved;
    private Text playerDamageRecieved;
    private GameObject enemyDamageRecievedObject;
    private GameObject playerDamageRecievedObject;

    private GameObject bulletDefault;
    //---
    private Text playerShieldsCurrent;
    private Text playerShieldsMax;

    private Text enemyShieldsCurrent;


    private GameObject testPlayerShieldEffect;
    private GameObject testEnemyShieldEffect;
    //-----
    public int test;

   // public Text uiPlayerWeapon1TextDamage;
   // public Text uiPlayerWeapon1TextName;
   // public Text uiPlayerWeapon1TextReloadTime;
   // public Text uiPlayerWeapon1TextReloadCurrent;

    public Text uiCurrentTime;

    // Models of ships

    public GameObject playerShipPlace;  // PlaceForShipPlayer
    public GameObject enemyShipPlace;     // PlaceForShipEnemy

    private bool needUpdateFromServer = true;
    // Start is called before the first frame update
    void Start()
    {
        // load objects
        // UI
        playerHealthCurrent = GameObject.Find("PlayerHealthCurrent").GetComponent<Text>();
        playerHealthMax = GameObject.Find("PlayerHealthMax").GetComponent<Text>();


        uiEnemyHealthMax = GameObject.Find("EnemyHealthMax").GetComponent<Text>();
        uiEnemyHealthCurrent = GameObject.Find("EnemyHealthCurrent").GetComponent<Text>();

        playerEnergyCurrent = GameObject.Find("PlayerEnergyCurrent").GetComponent<Text>();
        playerEnergyMax = GameObject.Find("PlayerEnergyMax").GetComponent<Text>();

        enemyDamageRecieved = GameObject.Find("EnemyDamageRecieved").GetComponent<Text>();
        playerDamageRecieved = GameObject.Find("PlayerDamageRecieved").GetComponent<Text>();

        enemyDamageRecievedObject = GameObject.Find("EnemyDamageRecieved");
        playerDamageRecievedObject = GameObject.Find("PlayerDamageRecieved");
        enemyDamageRecievedObject.SetActive(false);
        playerDamageRecievedObject.SetActive(false);

        bulletDefault = GameObject.Find("BulletDefault");

        playerShieldsCurrent = GameObject.Find("PlayerShieldsCurrent").GetComponent<Text>();
        playerShieldsMax = GameObject.Find("PlayerShieldsMax").GetComponent<Text>();

        enemyShieldsCurrent = GameObject.Find("EnemyShieldsCurrent").GetComponent<Text>();


        testPlayerShieldEffect = GameObject.Find("TestPlayerShieldEffect");
        testEnemyShieldEffect = GameObject.Find("TestEnemyShieldEffect");

        //Load cache 
        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;
        int battleSessionId = myCarrierObject.GetComponent<CacheData>().battleSessionId;

        Debug.Log("sessionToken - " + sessionToken);
        Debug.Log("playerId - " + playerId);
        Debug.Log("battleSessionId - " + battleSessionId);

        //Send request for information to place in UI
        string message = "3;" + playerId + ";" + sessionToken + ";" + battleSessionId + ";0";

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            // Process answer to put information in UI
            Debug.Log("UI Getting from server answer - " + answer);
            ProcessServerAnswerPrepareUi(answer);


            //Send information to the server that client is ready
            string message_2 = "3;" + playerId + ";" + sessionToken + ";" + battleSessionId + ";1";

            string answer_2 = TcpConnection.Connect(message_2);

            if (answer_2 != "" || answer_2 != "000")
            {
                Debug.Log("Connfirmation answer - " + answer_2);
            }
            else
            {
                Debug.Log("Getting information from server for confirmation about is battle is ready failed ");
            }

        }
        else {
            Debug.Log("Getting information from server for setting UI information Failed ");
        }



        // Repeat request to the server of getting information about battle every starting from 0ms every 50ms
        if (needUpdateFromServer) { InvokeRepeating("RequestToUpdateUi", 0, 0.05f);  }
        
    }

    // Update is called once per frame
    private void Update()  {    }

    private void ProcessServerAnswerPrepareUi(string answer)
    {
        string[] answerArray = answer.Split(';');

        //DEBUG
        for (int i = 0; i < answerArray.Length; i++)
        {
            Debug.Log("amount - "+ answerArray.Length + " i = " + i + " - value - " + answerArray[i]);
        }

        // player
        string playerShipId = answerArray[0];
        string playerShipMaxHealth = answerArray[1];
        string playerShipMaxEnergy = answerArray[2];

        // separete modules 
        string[,] playerModuleSlot = new string[17,6];
       // string[] moduleArray = new string[5];
        for (int i = 0; i < 17; i++)
        {
          //  moduleArray = answerArray[i + 3].Split(',');
          //  Debug.Log("iii - "+ i +"DEBUG - " + answerArray[i + 3] + " - 1 -" + moduleArray[0] + moduleArray[1] + moduleArray[2] + moduleArray[3] + moduleArray[4]);
            for (int ii = 0; ii < 6; ii++)
            {
                  playerModuleSlot[i, ii] = answerArray[i + 3].Split(',')[ii];
               // playerModuleSlot[i, ii] = moduleArray[0];
            }
        }

        // ????????? separate weapons
        string[,] playerWeaponSlot = new string[5,6];
      //  string[] weaponArray = new string[6];
        for (int i = 0; i < 5; i++)
        {
            for (int ii = 0; ii < 6; ii++)
            {
                playerWeaponSlot[i, ii] = answerArray[i + 20].Split(',')[ii];
            }
        }

        // enemy
        string enemyShipId = answerArray[25];
        string enemyShipMaxHealth = answerArray[26];
        string enemyShipMaxEnergy = answerArray[27];

        string[] enemyModuleSlot = new string[17];
        for (int i = 0; i < enemyModuleSlot.Length; i++)
        {
            enemyModuleSlot[i] = answerArray[i + 28];
        }

        string[] enemyWeaponSlot = new string[5];
        for (int i = 0; i < enemyWeaponSlot.Length; i++)
        {
            enemyWeaponSlot[i] = answerArray[i + 45];
        }





        //string playerWeapon1Name = words[4];
        //string playerWeapon1Damage = words[5];
        //string playerWeapon1ReloadTime = words[6];





        // Setting information to UI
        playerHealthMax.text = playerShipMaxHealth;
        playerHealthCurrent.text = playerShipMaxHealth;

        uiEnemyHealthMax.text = enemyShipMaxHealth;
        uiEnemyHealthCurrent.text = enemyShipMaxHealth;

        playerEnergyCurrent.text = playerShipMaxEnergy;
        playerEnergyMax.text = playerShipMaxEnergy;

        //uiPlayerWeapon1TextDamage.text = playerWeapon1Damage;
        //uiPlayerWeapon1TextName.text = playerWeapon1Name;
        //uiPlayerWeapon1TextReloadTime.text = playerWeapon1ReloadTime;
        //uiPlayerWeapon1TextReloadCurrent.text = playerWeapon1ReloadTime;


        // Set Ships models by ShipID's

        // Load ship model 
        var playerShip = Resources.Load("Models/Ships/Inside/" + (playerShipId)) as GameObject;
        var playerShipModel = Instantiate(playerShip, new Vector3(-3, 0, 0), Quaternion.Euler(0f, 10f, 0f)) as GameObject;
        playerShipModel.transform.parent = playerShipPlace.transform;
        playerShipModel.name = "PlayerShipModel";



        SetModulesPlayer(playerModuleSlot, playerShipModel);

        SetWeaponsPlayer(playerWeaponSlot, playerShipModel);



        var enemyShip = Resources.Load("Models/Ships/Inside/" + (enemyShipId)) as GameObject;
        var enemyShipModel = Instantiate(enemyShip, new Vector3(100, 0, 0), Quaternion.Euler(0f, -190f, 0f)) as GameObject;
        enemyShipModel.transform.parent = enemyShipPlace.transform;
        enemyShipModel.name = "EnemyShipModel";

        SetModulesEnemy(enemyModuleSlot, enemyShipModel);

        SetWeaponsEnemy(enemyWeaponSlot, enemyShipModel);


        // Set Timer
        uiCurrentTime.text = "0";
    }



    // set modules to the ship

    private void ChangeModuleModel(GameObject playerShipModel, string moduleId, string moduleName, string LoadModuleFolder)
    {
        var childObject = playerShipModel.transform.Find(moduleName);

        if (childObject != null)
        {
            Destroy(childObject.gameObject);
            // set new module
            var newChild = Resources.Load("Models/Modules/" + LoadModuleFolder + "/" + moduleId) as GameObject;
            var childShipModel = Instantiate(newChild, new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            childShipModel.transform.parent = playerShipModel.transform;
            childShipModel.transform.position = childObject.transform.position;
            childShipModel.transform.rotation = childObject.transform.rotation;
            childShipModel.name = moduleName;

            childShipModel.gameObject.AddComponent<MeshCollider>();

        }
    }

    // set modules to the panel

    private void SetModulesPlayer(string[,] modulesId, GameObject playerShipModel) 
    {

        var moduleUiItem = GameObject.Find("PlayerModuleDeffault");

        // //[ engine , cockpit, biglot1 .. 5, mediumslot 1 .. 5, smallslot 1 .. 5]
        for (int i = 0; i < 17; i++)
        {
            if (modulesId[i,0] != "0" && modulesId[i,0] != "-1")
            {
                var newModuleUiItem = Instantiate(moduleUiItem) as GameObject;
                newModuleUiItem.transform.parent = GameObject.Find("PlayerModulesPanel_Content").transform;

                
                
                // customize - what module is it

                //type and name of the module

                if (i == 0)  // engine
                {
                    newModuleUiItem.name = "engineSlot";
                    newModuleUiItem.transform.Find("PlayerModuleDeffault_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/BattleModulesIcons/" + "EngineSlot");

                    ChangeModuleModel(playerShipModel,  modulesId[i,0], "EngineSlot", "Engine");
                } 
                else if (i == 1) //cockpit
                {
                    newModuleUiItem.name = "cockpitSlot";
                    newModuleUiItem.transform.Find("PlayerModuleDeffault_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/BattleModulesIcons/" + "CockpitSlot");

                    ChangeModuleModel(playerShipModel, modulesId[i,0], "CockpitSlot", "Cockpit");
                }
                else if (i >= 2 && i <= 6) // bigslot
                {
                    newModuleUiItem.name = "bigSlot";
                    newModuleUiItem.transform.Find("PlayerModuleDeffault_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/BattleModulesIcons/" + "BigSlot");

                    ChangeModuleModel(playerShipModel, modulesId[i,0], "BigSlot"+(i-1), "BigSlot");

                    //if (modulesId[i, 4] == "shield") 
                    //{
                    //    bigModulesType[i-2] = "shield";
                    //    playerShieldsCurrent.text = "0";
                    //    playerShieldsMax.text = modulesId[i, 5];
                    //}
                }
                else if (i >= 7 && i <= 11) // mediumslot
                {
                    newModuleUiItem.name = "mediumSlot";
                    newModuleUiItem.transform.Find("PlayerModuleDeffault_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CrewIcons/" + 3);
                }

                //item ID in the game
                newModuleUiItem.transform.Find("PlayerModuleDeffault_Button").name = "PlayerModuleId"+Convert.ToString(i);
                newModuleUiItem.transform.Find("PlayerModuleDeffault_CurrentEnergy").name = "PlayerModuleCurrentEnergy" + Convert.ToString(i);
                newModuleUiItem.transform.Find("PlayerModuleDeffault_EnergyUp").name = Convert.ToString(i);
                newModuleUiItem.transform.Find("PlayerModuleDeffault_EnergyDown").name = Convert.ToString(i);

                // set module on or off
                if (modulesId[i, 2] == "0")
                {
                    newModuleUiItem.transform.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(142, 142, 142, 255);
                    newModuleUiItem.transform.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "0";
                }
                else if (modulesId[i, 2] == "1") 
                {

                    newModuleUiItem.transform.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(125, 231, 88, 255);
                    newModuleUiItem.transform.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "1";
                }



                // set energy amount on the module

            }

            if (modulesId[i,0] == "0")
            {
                if (i == 0)  // engine
                {
                    ChangeModuleModel(playerShipModel, "0", "EngineSlot", "Engine");
                }
                else if (i == 1) //cockpit
                {
                    ChangeModuleModel(playerShipModel, "0", "CockpitSlot", "Cockpit");
                }
                else if (i >= 2 && i <= 6) // bigslot
                {
                    ChangeModuleModel(playerShipModel, "0", "BigSlot" + (i - 1), "BigSlot");
                }
                else if (i >= 7 && i <= 11) // mediumslot
                {
                    ChangeModuleModel(playerShipModel, "0", "MediumSlot" + (i - 6), "MediumSlot");
                }
            }
        
        }
    }

    private void SetWeaponsPlayer(string[,] weaponsId, GameObject playerShipModel) 
    {
        var weaponUiItem = GameObject.Find("PlayerWeaponDeffault");

        for (int i = 0; i < 5; i++)
        {
            if (weaponsId[i,0] != "0" && weaponsId[i,0] != "-1")
            {
                var newWeaponUiItem = Instantiate(weaponUiItem) as GameObject;
                newWeaponUiItem.transform.parent = GameObject.Find("PlayerWeaponsPanel_Content").transform;

                newWeaponUiItem.name = "PlayerWeapon" + (i+1);
                newWeaponUiItem.transform.Find("PlayerWeaponDeffault_Button").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/BattleModulesIcons/" + "EngineSlot");
                newWeaponUiItem.transform.Find("PlayerWeaponDeffault_Button").name = "PlayerWeaponButton"+Convert.ToString(i + 1);

                newWeaponUiItem.transform.Find("PlayerWeaponDeffault_TextReloadCurrent").name = "PlayerWeapon"+ Convert.ToString(i+1) + "TextReloadCurrent";
                newWeaponUiItem.transform.Find("PlayerWeaponDeffault_CurrentEnergy").name = "PlayerWeaponCurrentEnergy" + Convert.ToString(i);
                newWeaponUiItem.transform.Find("PlayerWeaponDeffault_EnergyUp").name = Convert.ToString(i);
                newWeaponUiItem.transform.Find("PlayerWeaponDeffault_EnergyDown").name = Convert.ToString(i);

                ChangeModuleModel(playerShipModel, weaponsId[i,0], "Weapon"+(i+1), "Weapon");
            }
        }
    }


    // enemy modules and weapon
    private void SetModulesEnemy(string[] modulesId, GameObject enemyShipModel)
    {
        // //[ engine , cockpit, biglot1 .. 5, mediumslot 1 .. 5, smallslot 1 .. 5]
        for (int i = 0; i < modulesId.Length; i++)
        {
            if (modulesId[i] != "0" && modulesId[i] != "-1")
            {
                // customize - what module is it

                //type and name of the module

                if (i == 0)  // engine
                {
                    ChangeModuleModel(enemyShipModel, modulesId[i], "EngineSlot", "Engine");
                }
                else if (i == 1) //cockpit
                {
                    ChangeModuleModel(enemyShipModel, modulesId[i], "CockpitSlot", "Cockpit");
                }
                else if (i >= 2 && i <= 6) // bigslot
                {
                    ChangeModuleModel(enemyShipModel, modulesId[i], "BigSlot" + (i - 1), "BigSlot");
                }
                else if (i >= 7 && i <= 11) // mediumslot
                {
                }


            }

            if (modulesId[i] == "0")
            {
                if (i == 0)  // engine
                {
                    ChangeModuleModel(enemyShipModel, "0", "EngineSlot", "Engine");
                }
                else if (i == 1) //cockpit
                {
                    ChangeModuleModel(enemyShipModel, "0", "CockpitSlot", "Cockpit");
                }
                else if (i >= 2 && i <= 6) // bigslot
                {
                    ChangeModuleModel(enemyShipModel, "0", "BigSlot" + (i - 1), "BigSlot");
                }
                else if (i >= 7 && i <= 11) // mediumslot
                {
                    ChangeModuleModel(enemyShipModel, "0", "MediumSlot" + (i - 6), "MediumSlot");
                }
            }
        }
    }
    private void SetWeaponsEnemy(string[] weaponsId, GameObject enemyShipModel)
    {
        for (int i = 0; i < weaponsId.Length; i++)
        {
            if (weaponsId[i] != "0" && weaponsId[i] != "-1")
            {
                 ChangeModuleModel(enemyShipModel, weaponsId[i], "Weapon" + (i + 1), "Weapon");
              //  Debug.Log("DEBUG weaponenemy i - "+ weaponsId[i] + " " + i);
            }
        }
    }


    //Request to server to update UI and processing it 
    private void RequestToUpdateUi() {
        //Load cache 
        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;
        int battleSessionId = myCarrierObject.GetComponent<CacheData>().battleSessionId;

        // set request to the server every 50 ms to get infromation about the battle
        string message = "3;" + playerId + ";" + sessionToken + ";" + battleSessionId + ";2";

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            // battle is finished
            if (answer == "111")
            { 
                needUpdateFromServer = false;
                // show final message and then when in message pressed OK - move to garage
                //??????
                StartCoroutine(LoadSceneAsync.LoadYourAsyncScene("Garage"));
            }
            // battle is active
            else 
            {
                ProcessServerAnswerUpdateUi(answer);
            }

        }
        else
        {
            Debug.Log("Getting information from server for update infromation about the battle is failed ");
        }
    }
    private void ProcessServerAnswerUpdateUi(string answer) {

        string[] serverResponse = answer.Split(';');

        // player  modules 
        for (int i = 0; i < 17; i++)
        {
            string[] moduleArray = serverResponse[i + 3].Split(',');
            // 0 -slot powered , 1 -slot health

            //// update shield information
            //if (i == 2 || i == 3 || i == 4 || i == 5 || i == 6) 
            //{
            //    if (bigModulesType[i-2] == "shield") 
            //    {
            //        playerShieldsCurrent.text = moduleArray[2];
            //    }
            //}


            if (GameObject.Find("PlayerModuleId" + Convert.ToString(i)) != null)
            {
                // module status 
                if (moduleArray[0] == "0" && Convert.ToInt32(moduleArray[1]) == 10) // unpowered and not damaged
                {
                    GameObject.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(191, 191, 191, 255);
                    GameObject.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "0";
                }
                else if (moduleArray[0] == "0" && Convert.ToInt32(moduleArray[1]) > 0 && Convert.ToInt32(moduleArray[1]) < 10) // unpowered and damaged
                {
                    GameObject.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(222, 201, 62, 255);
                    GameObject.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "0";
                }
                else if ((moduleArray[0] == "0" || moduleArray[0] == "1") && Convert.ToInt32(moduleArray[1]) <= 0) // destroyed
                {
                    GameObject.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(224, 0, 0, 255);
                    GameObject.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "0";
                }
                else if (moduleArray[0] == "1" && Convert.ToInt32(moduleArray[1]) == 10) // powered and not damaged
                {
                    GameObject.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(50, 233, 53, 255);
                    GameObject.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "1";
                }
                else if (moduleArray[0] == "1" && Convert.ToInt32(moduleArray[1]) > 0 && Convert.ToInt32(moduleArray[1]) < 10) // powered and damaged
                {
                    GameObject.Find("PlayerModuleId" + Convert.ToString(i)).GetComponent<Image>().color = new Color32(248, 248, 97, 255);
                    GameObject.Find("PlayerModuleCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = "1";
                }
            }
        }


        //player weapons

        // set reload to ALL weapons
        for (int i = 0; i < 5; i++)
        {
            string[] weaponModuleArray = serverResponse[i + 20].Split(',');

            // 0 - powered, 1- reload time , 2 - projectile time
            if (GameObject.Find("PlayerWeapon"+(i+1)) != null) 
            {


                GameObject.Find("PlayerWeapon"+(i+1)+"TextReloadCurrent").GetComponent<Text>().text = weaponModuleArray[1];

                GameObject.Find("PlayerWeaponCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text = weaponModuleArray[0];

                if (weaponModuleArray[0] == "0") //unpowered
                {
                    GameObject.Find("PlayerWeaponButton" + Convert.ToString(i + 1)).GetComponent<Image>().color = new Color32(191, 191, 191, 255);

                } 
                else if (weaponModuleArray[0] == "1" && weaponModuleArray[1] != "0")  // powered and reloading 
                {
                    GameObject.Find("PlayerWeaponButton" + Convert.ToString(i + 1)).GetComponent<Image>().color = new Color32(110, 110, 188, 255);
                }
                else if (weaponModuleArray[0] == "1" && weaponModuleArray[1] == "0")  // powered and ready to shoot
                {
                    GameObject.Find("PlayerWeaponButton" + Convert.ToString(i + 1)).GetComponent<Image>().color = new Color32(50, 233, 53, 255);
                }


                if (weaponModuleArray[2] != "0" && weaponModuleArray[2] != "-1") 
                {
                    // 100,0,0 - coordinates of the ship

                    if (GameObject.Find("Weapon" + i + "Projectile" + 1) != null)
                    {
                        GameObject.Find("Weapon" + i + "Projectile" + 1).transform.position = new Vector3((100 - (float.Parse(weaponModuleArray[2]) / 1500) * 100), 0, 0);
                    }
                    else
                    {
                        GameObject bullet = Instantiate(bulletDefault, transform.position, Quaternion.identity) as GameObject;
                        bullet.name = "Weapon" + i + "Projectile" + 1;
                    }
                }
                else if (GameObject.Find("Weapon" + i + "Projectile" + 1) != null)
                {
                    Destroy(GameObject.Find("Weapon" + i + "Projectile" + 1));

                    if (Convert.ToInt32(uiEnemyHealthCurrent.text) > Convert.ToInt32(serverResponse[25]))
                    {
                        enemyDamageRecievedObject.SetActive(true);
                        enemyDamageRecieved.text = Convert.ToString(Convert.ToInt32(uiEnemyHealthCurrent.text) - Convert.ToInt32(serverResponse[25]));
                        StartCoroutine(SleepCoroutine(2.5f, enemyDamageRecievedObject));
                    }
                    else
                    {
                        enemyDamageRecievedObject.SetActive(true);
                        enemyDamageRecieved.text = "miss";
                        StartCoroutine(SleepCoroutine(2.5f, enemyDamageRecievedObject));
                    }
                }


            }
        }


        //ai projectile test ( working)
        if (serverResponse[26] != "0" && serverResponse[26] != "-1")
        {
            if (GameObject.Find("AIWeapon1Projectile1") != null)
            {
                GameObject.Find("AIWeapon1Projectile1").transform.position = new Vector3(((float.Parse(serverResponse[26]) / 1500) * 100)-1, 0, 0);
            }
            else
            {
                GameObject bullet = Instantiate(bulletDefault, transform.position, Quaternion.identity) as GameObject;
                bullet.name = "AIWeapon1Projectile1";
            }
        }
        else
        {
            if (GameObject.Find("AIWeapon1Projectile1") != null)
            {
                Destroy(GameObject.Find("AIWeapon1Projectile1"));

                if (Convert.ToInt32(playerHealthCurrent.text) > Convert.ToInt32(serverResponse[1]))
                {
                    playerDamageRecievedObject.SetActive(true);
                    playerDamageRecieved.text = Convert.ToString(Convert.ToInt32(playerHealthCurrent.text) - Convert.ToInt32(serverResponse[1]));
                    StartCoroutine(SleepCoroutine(2.5f, playerDamageRecievedObject));
                }
                else 
                {
                    playerDamageRecievedObject.SetActive(true);
                    playerDamageRecieved.text = "miss";
                    StartCoroutine(SleepCoroutine(2.5f, playerDamageRecievedObject));
                }
            }
        }






        // Setting information to UI
        uiCurrentTime.text = serverResponse[0];
        playerHealthCurrent.text = serverResponse[1];
        playerEnergyCurrent.text = serverResponse[2];

        uiEnemyHealthCurrent.text = serverResponse[25];


        playerShieldsCurrent.text = serverResponse[27];
        enemyShieldsCurrent.text = serverResponse[28];

        // show shield effect on player
        if (serverResponse[27] != "0")
        {
            testPlayerShieldEffect.SetActive(true);
        }
        else 
        {
            testPlayerShieldEffect.SetActive(false);
        }

        // show shield effect on enemy
        if (serverResponse[28] != "0")
        {
            testEnemyShieldEffect.SetActive(true);
        }
        else
        {
            testEnemyShieldEffect.SetActive(false);
        }

    }



    IEnumerator SleepCoroutine(Single time, GameObject damageInformation)
    {
        //Print the time of when the function is first called.
       // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        damageInformation.SetActive(false);

        //After we have waited 5 seconds print the time again.
       // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
