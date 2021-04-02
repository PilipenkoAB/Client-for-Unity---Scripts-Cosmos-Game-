//GarageSceneInitialize
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class GarageSceneInitialize : MonoBehaviour
{

    private GameObject myCarrierObject;

    public Text playerName;

    public Button buttonSlot;
    public Image buttonShipId;
    public Sprite image1;

    // carousel
    private Button buttonSlotRight;
    private Button buttonSlotLeft;

    // 
    public GameObject shipPlace;

    private int slotLeftShipNumber;
    private int slotCurrentShipNumber;
    private int slotRightShipNumber;

    private string slotLeftAccountShipId;
    private string slotCurrentAccountShipId;
    private string slotRightAccountShipId;

    // Circle Models
    public Button circleEngine;
    public Button circleCockpit;
    public Button circleBigSlot1;
    public Button circleBigSlot2;
    public Button circleBigSlot3;
    public Button circleBigSlot4;
    public Button circleBigSlot5;
    public Button circleMediumSlot1;
    public Button circleMediumSlot2;
    public Button circleMediumSlot3;
    public Button circleMediumSlot4;
    public Button circleMediumSlot5;
    public Button circleSmallSlot1;
    public Button circleSmallSlot2;
    public Button circleSmallSlot3;
    public Button circleSmallSlot4;
    public Button circleSmallSlot5;
    public Button circleWeapon1;
    public Button circleWeapon2;
    public Button circleWeapon3;
    public Button circleWeapon4;
    public Button circleWeapon5;
    public Button circleDron1;
    public Button circleDron2;
    public Button circleDron3;
    public Button circleDron4;
    public Button circleDron5;

    private GameObject ItemFocusInfo;
    private GameObject ItemRighClickInfo;
    private GameObject ItemFullInfoPanel;

    // ship information panel
    private Text PanelShipInfoShipName;
    private Text PanelShipInfoShipType;
    private Text PanelShipInfoDescriptionText;
    private Text PanelShipInfoHealthText;
    private Text PanelShipInfoEnergyText;

    // Ship modules
    private GameObject engineModel;
    private GameObject cockpitModel;
    private GameObject bigSlot1Model;
    private GameObject bigSlot2Model;
    private GameObject bigSlot3Model;
    private GameObject bigSlot4Model;
    private GameObject bigSlot5Model;
    private GameObject mediumSlot1Model;
    private GameObject mediumSlot2Model;
    private GameObject mediumSlot3Model;
    private GameObject mediumSlot4Model;
    private GameObject mediumSlot5Model;
    private GameObject weapon1Model;
    private GameObject weapon2Model;
    private GameObject weapon3Model;
    private GameObject weapon4Model;
    private GameObject weapon5Model;

    // camera rotation
    private Vector3 previousCameraPosition;
    private float distanceFromCameraToShip;

    // camera scroll
    private double minCameraDistance = 4;
    private double maxCameraDistance = 20;


    // math calculations for circle d= sqr((xp - xc)^2+(yp - yc)^2)
    double d;
    double r = 800;
    bool mousePressedInside;

    // double click 
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;




    // Start is called before the first frame update
    void Start()
    {
        //
        buttonSlotRight = GameObject.Find("ButtonSlotRight").GetComponent<Button>();
        buttonSlotLeft = GameObject.Find("ButtonSlotLeft").GetComponent<Button>();


        PanelShipInfoShipName = GameObject.Find("PanelShipInfoShipName").GetComponent<Text>();
        PanelShipInfoShipType = GameObject.Find("PanelShipInfoShipType").GetComponent<Text>();
        PanelShipInfoDescriptionText = GameObject.Find("PanelShipInfoDescriptionText").GetComponent<Text>();
        PanelShipInfoHealthText = GameObject.Find("PanelShipInfoHealthText").GetComponent<Text>();
        PanelShipInfoEnergyText = GameObject.Find("PanelShipInfoEnergyText").GetComponent<Text>();
        //---


        //----------

        // COSTIL' to hide the panel from the start
        ItemFocusInfo = GameObject.Find("ItemFocusInfo");
        ItemFocusInfo.transform.position = new Vector2(-25000,-25000);

        ItemRighClickInfo = GameObject.Find("ItemRighClickInfo");
        ItemRighClickInfo.transform.position = new Vector2(-25000, -25000);
        //------------
        ItemFullInfoPanel = GameObject.Find("ItemFullInfoPanel");
        ItemFullInfoPanel.transform.position = new Vector2(-25000, -25000);

        //------------
        distanceFromCameraToShip = Vector3.Distance(Camera.main.transform.position, shipPlace.transform.position);

        // GET INFORMATION FROM TRANSFERDATA - HOW to CALL it correctlu
        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;
        Debug.Log("Loading_garage - ready");



        // CHECK IF PLAYER IN SOME SESSIONS

        string message = "2;"+ playerId+";"+ sessionToken+";0"+";0"; //entering the garage - what 3 slots and what is first slot to put 3d

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            Debug.Log("answer - " + answer);
            // GET the information about slots and name of the player
            ProcessServerAnswer(answer);

            // Process click on the slots
            // ?????????????/
            buttonSlotRight.onClick.AddListener(delegate { ChangeSlot("right", playerId, sessionToken, slotRightAccountShipId); });
            buttonSlotLeft.onClick.AddListener(delegate { ChangeSlot("left", playerId, sessionToken, slotLeftAccountShipId); });

        }
        else {
            Debug.Log("Error in recieve message 2 with code 0 in Garage");
        }

        }

    private void Update()
    {
        if(IsPointerOverUIElement() == false)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f && distanceFromCameraToShip > minCameraDistance) // up
            {
                d = Math.Sqrt(Math.Pow((Input.mousePosition.x - 1900), 2) + Math.Pow((Input.mousePosition.y - 1000), 2));
                if (d < r)
                {
                    // move camera close to the ship
                    Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, shipPlace.transform.position, 0.25f);
                    distanceFromCameraToShip = Vector3.Distance(Camera.main.transform.position, shipPlace.transform.position);
                }

            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f && distanceFromCameraToShip < maxCameraDistance) // down
            {
                d = Math.Sqrt(Math.Pow((Input.mousePosition.x - 1900), 2) + Math.Pow((Input.mousePosition.y - 1000), 2));
                if (d < r)
                {
                    // move camera away from the ship
                    Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, shipPlace.transform.position, -0.25f);
                    distanceFromCameraToShip = Vector3.Distance(Camera.main.transform.position, shipPlace.transform.position);
                }
            }
        }

        // rotate camera around the ship in the circle where ship is placed.
        // coordinates of the centre 1900:1000. 
        // 1100:1050-left, 2700:1050-right, 1900:1800-up,1900,200 - down
        // if pressied right or left click inside the circle and move mouse - it moves the camera

        if (Input.GetButtonUp("Fire1"))
            {
                mousePressedInside = false;
            }

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && IsPointerOverUIElement() == false)
            {
                d = Math.Sqrt(Math.Pow((Input.mousePosition.x - 1900), 2) + Math.Pow((Input.mousePosition.y - 1000), 2));
                if (d < r)
                {
                    mousePressedInside = true;
                    previousCameraPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

                //https://forum.unity.com/threads/detect-double-click-on-something-what-is-the-best-way.476759/
                // double click   - return camera to start position
                    clicked++;
                     if (clicked == 1) clicktime = Time.time;

                     if (clicked > 1 && Time.time - clicktime < clickdelay)
                     {
                        clicked = 0;
                        clicktime = 0;
                        Debug.Log("Double CLick: ");
                    Camera.main.transform.position = new Vector3(0,10,-5);
                    Camera.main.transform.rotation = Quaternion.Euler(60, 0, 0);

                }
                     else if (clicked > 2 || Time.time - clicktime > 1) clicked = 0;
                }
            }
            if ((Input.GetMouseButton(0) || Input.GetMouseButton(1)) && mousePressedInside == true)
            {
                    Vector3 newPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    Vector3 direction = previousCameraPosition - newPosition; // this line is lag problem probably because 
                                                                              // previousCameraPosition = 0 when its
                                                                              //                      not started Input.GetMouseButtonDown(0)

                    float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
                    float rotationAroundXAxis = direction.y * 180; // camera moves vertically

                    Camera.main.transform.position = shipPlace.transform.position;

                    Camera.main.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
                    Camera.main.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); // <— This is what makes it work!

                    Camera.main.transform.Translate(new Vector3(0, 0, -distanceFromCameraToShip));

                    previousCameraPosition = newPosition;
            }


    }



    // check if OVERLAP UI (working perfectly)
    // https://answers.unity.com/questions/1095047/detect-mouse-events-for-ui-canvas.html

    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
    //--------------------------------------------















    // Answer from the server at the start of the scene
    private void ProcessServerAnswer(string answer) {
        string[] words = answer.Split(';');

        Debug.Log("id ships " + words[0]+"--"+ words[1] + "--" + words[2]);
        Debug.Log("slots (m - r - l) " + words[47] + "--" + words[48] + "--" + words[49]);
        slotCurrentAccountShipId = words[47];
        slotRightAccountShipId = words[48];
        slotLeftAccountShipId = words[49];

        // crew info
        string[] crewId = new string[] { words[50], words[51], words[52], words[53], words[54], words[55], words[56], words[57], words[58], words[59], words[60], words[61], words[62], words[63], words[64] };

        // Load ships' images for right and left slots
        buttonSlotRight.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GarageShips/" + words[1]);
        buttonSlotLeft.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/GarageShips/" + words[2]);

        if (words[0] == "0")
        {
            Debug.Log("There is no ship in this slot - " + words[47]);

            // remove model
            if (GameObject.Find("PlayerShipModel") != null)
            {
                Destroy(shipPlace.transform.GetChild(0).gameObject);
            }

            //remove aditional info
            PanelShipInfoShipName.text = "";
            PanelShipInfoShipType.text = "";
            PanelShipInfoDescriptionText.text = "";
            PanelShipInfoHealthText.text = "";
            PanelShipInfoEnergyText.text = "";

            // deativate circle items
            circleBigSlot1.gameObject.SetActive(false);
            circleBigSlot2.gameObject.SetActive(false);
            circleBigSlot3.gameObject.SetActive(false);
            circleBigSlot4.gameObject.SetActive(false);
            circleBigSlot5.gameObject.SetActive(false);
            circleMediumSlot1.gameObject.SetActive(false);
            circleMediumSlot2.gameObject.SetActive(false);
            circleMediumSlot3.gameObject.SetActive(false);
            circleMediumSlot4.gameObject.SetActive(false);
            circleMediumSlot5.gameObject.SetActive(false);
            circleWeapon1.gameObject.SetActive(false);
            circleWeapon2.gameObject.SetActive(false);
            circleWeapon3.gameObject.SetActive(false);
            circleWeapon4.gameObject.SetActive(false);
            circleWeapon5.gameObject.SetActive(false);
            circleEngine.gameObject.SetActive(false);
            circleCockpit.gameObject.SetActive(false);

        }
         else 
         { 

         LoadShipInformationPanel(words[0]);

         // set ship model
         //ship slots 
         slotCurrentShipNumber = Convert.ToInt32(words[0]);
         slotRightShipNumber = Convert.ToInt32(words[1]);
         slotLeftShipNumber = Convert.ToInt32(words[2]);


         // destroy old ship if exist
         if (GameObject.Find("PlayerShipModel") != null)
            {
                Destroy(shipPlace.transform.GetChild(0).gameObject);
            }
          // Load ship model for first slot
          var slot1Ship = Resources.Load("Models/Ships/Inside/" + words[0]) as GameObject;
          // (Instantiate(slot1Ship, new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f)) as GameObject).transform.parent = shipPlace.transform;
          var playerShipModel = Instantiate(slot1Ship, new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
          playerShipModel.transform.parent = shipPlace.transform;
          playerShipModel.name = "PlayerShipModel";


            // MODULES

            // WHAT IS WHAT????????????
            //   answerToClient = slotShip[0] + ";" + slotShip[1] + ";" + slotShip[2] +
            //";" + accountEngineSlot + ";" + engineSlotId + ";" + accountCockpitSlot + ";" + cockpitSlotId +
            //";" + accountBigSlot[0] + ";" + bigSlotId[0] + ";" + accountBigSlot[1] + ";" + bigSlotId[1] +
            //";" + accountBigSlot[2] + ";" + bigSlotId[2] + ";" + accountBigSlot[3] + ";" + bigSlotId[3] +
            //";" + accountBigSlot[4] + ";" + bigSlotId[4] +
            //";" + accountMediumSlot[0] + ";" + mediumSlotId[0] + ";" + accountMediumSlot[1] + ";" + mediumSlotId[1] +
            //";" + accountMediumSlot[2] + ";" + mediumSlotId[2] + ";" + accountMediumSlot[3] + ";" + mediumSlotId[3] +
            //";" + accountMediumSlot[4] + ";" + mediumSlotId[4] +
            //";" + accountSmallSlot[0] + ";" + smallSlotId[0] + ";" + accountSmallSlot[1] + ";" + smallSlotId[1] +
            //";" + accountSmallSlot[2] + ";" + smallSlotId[2] + ";" + accountSmallSlot[3] + ";" + smallSlotId[3] +
            //";" + accountSmallSlot[4] + ";" + smallSlotId[4] +
            //";" + accountWeapon[0] + ";" + weaponId[0] + ";" + accountWeapon[1] + ";" + weaponId[1] +
            //";" + accountWeapon[2] + ";" + weaponId[2] + ";" + accountWeapon[3] + ";" + weaponId[3] +
            //";" + accountWeapon[4] + ";" + weaponId[4];
              //        ";" + slotIdInfo[0] + ";" + slotIdInfo[1] + ";" + slotIdInfo[2] +
             //";" + crew[0] + ";" + crew[1] + ";" + crew[2] + ";" + crew[3] + ";" + crew[4] + ";" + crew[5] +
             //";" + crew[6] + ";" + crew[7] + ";" + crew[8] + ";" + crew[9] + ";" + crew[10] + ";" + crew[11] +
             //";" + crew[12] + ";" + crew[13] + ";" + crew[14];



            //         -------  modules

            // Engine
            CheckIfModuleExist(words[3], circleEngine, playerShipModel, words[4], "EngineSlot", "Engine", "EngineSlot");
     
        //Cockpit
        CheckIfModuleExist(words[5], circleCockpit, playerShipModel, words[6], "CockpitSlot", "Cockpit", "CockpitSlot");

        //BigSlot1
        CheckIfModuleExist(words[7], circleBigSlot1, playerShipModel, words[8], "BigSlot1", "BigSlot", "BigSlot");

        //BigSlot2
        CheckIfModuleExist(words[9], circleBigSlot2, playerShipModel, words[10], "BigSlot2", "BigSlot", "BigSlot");

        //BigSlot3
        CheckIfModuleExist(words[11], circleBigSlot3, playerShipModel, words[12], "BigSlot3", "BigSlot", "BigSlot");

        //BigSlot4
        CheckIfModuleExist(words[13], circleBigSlot4, playerShipModel, words[14], "BigSlot4", "BigSlot", "BigSlot");

        //BigSlot5
        CheckIfModuleExist(words[15], circleBigSlot5, playerShipModel, words[16], "BigSlot5", "BigSlot", "BigSlot");

        //MediumSlot1
        CheckIfModuleExist(words[17], circleMediumSlot1, playerShipModel, words[18], "MediumSlot1", "MediumSlot", "MediumSlot");

        //MediumSlot2
        CheckIfModuleExist(words[19], circleMediumSlot2, playerShipModel, words[20], "MediumSlot2", "MediumSlot", "MediumSlot");

        //MediumSlot3
        CheckIfModuleExist(words[21], circleMediumSlot3, playerShipModel, words[22], "MediumSlot3", "MediumSlot", "MediumSlot");

        //MediumSlot4
        CheckIfModuleExist(words[23], circleMediumSlot4, playerShipModel, words[24], "MediumSlot4", "MediumSlot", "MediumSlot");

        //MediumSlot5
        CheckIfModuleExist(words[25], circleMediumSlot5, playerShipModel, words[26], "MediumSlot5", "MediumSlot", "MediumSlot");


        //SmallSlot1
        if (Convert.ToInt32(words[27]) == -1)
        {
            circleSmallSlot1.gameObject.SetActive(false);
        }
        else if (Convert.ToInt32(words[27]) == 0)
        {
            circleSmallSlot1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/empty");
            circleSmallSlot1.gameObject.SetActive(true);
        }
        else if (Convert.ToInt32(words[27]) > 0)
        {
            circleSmallSlot1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + words[28]);
        }

        //SmallSlot2
        if (Convert.ToInt32(words[29]) == -1)
        {
            circleSmallSlot2.gameObject.SetActive(false);
        }
        else if (Convert.ToInt32(words[29]) == 0)
        {
            circleSmallSlot2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/empty");
            circleSmallSlot2.gameObject.SetActive(true);
        }
        else if (Convert.ToInt32(words[29]) > 0)
        {
            circleSmallSlot2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + words[30]);
        }

        //SmallSlot3
        if (Convert.ToInt32(words[31]) == -1)
        {
            circleSmallSlot3.gameObject.SetActive(false);
        }
        else if (Convert.ToInt32(words[31]) == 0)
        {
            circleSmallSlot3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/empty");
            circleSmallSlot3.gameObject.SetActive(true);
        }
        else if (Convert.ToInt32(words[31]) > 0)
        {
            circleSmallSlot3.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + words[32]);
        }

        //SmallSlot4
        if (Convert.ToInt32(words[33]) == -1)
        {
            circleSmallSlot4.gameObject.SetActive(false);
        }
        else if (Convert.ToInt32(words[33]) == 0)
        {
            circleSmallSlot4.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/empty");
            circleSmallSlot4.gameObject.SetActive(true);
        }
        else if (Convert.ToInt32(words[33]) > 0)
        {
            circleSmallSlot4.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + words[34]);
        }

        //SmallSlot5
        if (Convert.ToInt32(words[35]) == -1)
        {
            circleSmallSlot5.gameObject.SetActive(false);
        }
        else if (Convert.ToInt32(words[35]) == 0)
        {
            circleSmallSlot5.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/empty");
            circleSmallSlot5.gameObject.SetActive(true);
        }
        else if (Convert.ToInt32(words[35]) > 0)
        {
            circleSmallSlot5.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + words[36]);
        }

        //Weapon1
        CheckIfModuleExist(words[37], circleWeapon1, playerShipModel, words[38], "Weapon1", "Weapon", "WeaponSlot");

            Debug.Log("DEBUG WEAPON - " + words[37] +" - "+ words[38]);

        //Weapon2
        CheckIfModuleExist(words[39], circleWeapon2, playerShipModel, words[40], "Weapon2", "Weapon", "WeaponSlot");

        //Weapon3
        CheckIfModuleExist(words[41], circleWeapon3, playerShipModel, words[42], "Weapon3", "Weapon", "WeaponSlot");

        //Weapon4
        CheckIfModuleExist(words[43], circleWeapon4, playerShipModel, words[44], "Weapon4", "Weapon", "WeaponSlot");

        //Weapon5
        CheckIfModuleExist(words[45], circleWeapon5, playerShipModel, words[46], "Weapon5", "Weapon", "WeaponSlot");



            // add rigidbody to all modules 
            foreach (Transform child in playerShipModel.transform)
            {
                child.gameObject.AddComponent<MeshCollider>();

              //  var modulesRigidbody =  child.gameObject.AddComponent<Rigidbody>();
              //  modulesRigidbody.useGravity = false;
            }


        }
        //---------------------------------------------------------------------------------------------

        // load crew
        LoadCrew(crewId);

    }

    private void LoadShipInformationPanel(string shipId)
    {
        // ship name from server if custom (NOT FOR NOW). For now - default one!
        PanelShipInfoShipName.text = LocalizedText.Localize("DB_Ship_" + shipId + "_ShipName");

        // through ID
        PanelShipInfoShipType.text = LocalizedText.Localize("DB_Ship_" + shipId + "_ShipType");

        PanelShipInfoDescriptionText.text = LocalizedText.Localize("DB_Ship_" + shipId + "_ShipDescription");

        // through calculations of the modules\ships etc
        // for now base health
        PanelShipInfoHealthText.text = LocalizedText.Localize("DB_Ship_" + shipId + "_BaseHealth");
        // for now base energy
        PanelShipInfoEnergyText.text = LocalizedText.Localize("DB_Ship_" + shipId + "_BaseEnergy");

    }

    private void LoadCrew(string[] crewId)
    {

        //Delete old crew if they existed
        foreach (Transform child in GameObject.Find("CrewModels").transform)
        {
            Destroy(child.transform.gameObject);
        }



        // create new crew
        string nameOfTheRandomModule;
        List<string> listOfRandomModules = new List<string>();

        if (GameObject.Find("PlayerShipModel")) 
        {
            // get the array of names of available slots to place the crew
            if (GameObject.Find("PlayerShipModel").transform.Find("EngineSlot") != null)
            {
                listOfRandomModules.Add("EngineSlot");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("CockpitSlot") != null)
            {
                listOfRandomModules.Add("CockpitSlot");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("BigSlot1") != null)
            {
                listOfRandomModules.Add("BigSlot1");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("BigSlot2") != null)
            {
                listOfRandomModules.Add("BigSlot2");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("BigSlot3") != null)
            {
                listOfRandomModules.Add("BigSlot3");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("BigSlot4") != null)
            {
                listOfRandomModules.Add("BigSlot4");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("BigSlot5") != null)
            {
                listOfRandomModules.Add("BigSlot5");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("MediumSlot1") != null)
            {
                listOfRandomModules.Add("MediumSlot1");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("MediumSlot2") != null)
            {
                listOfRandomModules.Add("MediumSlot2");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("MediumSlot3") != null)
            {
                listOfRandomModules.Add("MediumSlot3");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("MediumSlot4") != null)
            {
                listOfRandomModules.Add("MediumSlot4");
            }
            if (GameObject.Find("PlayerShipModel").transform.Find("MediumSlot5") != null)
            {
                listOfRandomModules.Add("MediumSlot5");
            }
        }
       

        Debug.Log("LoadCrew");
        //create crew from server infromation about crew connected to the ship
        for (int i = 0; i < crewId.Length; i++)
        {
            if(crewId[i] != "0")
            {
                var crewModel = Resources.Load("Models/Crew/" + crewId[i]) as GameObject;
                var crew = Instantiate(crewModel, new Vector3(0, 0, 0), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                crew.transform.parent = GameObject.Find("CrewModels").transform;

                // place crew
                nameOfTheRandomModule = listOfRandomModules[UnityEngine.Random.Range(0, listOfRandomModules.Count)];
                //nameOfTheRandomModule = "BigSlot1";
                // set random module from loaded to the ship
                crew.transform.position = GameObject.Find("PlayerShipModel").transform.Find(nameOfTheRandomModule).position;


                // crew name = id of the crew (FOR NOW) - TO FIX????
                crew.name = crewId[i];

                //y position
                 crew.transform.position = new Vector3(crew.transform.position.x, crew.transform.position.y - 0.25f, crew.transform.position.z) ;
                //size
                crew.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); ;

                // capsule collider
                CapsuleCollider crewCapsuleCollider = crew.AddComponent<CapsuleCollider>();
                crewCapsuleCollider.center = new Vector3(0,2,0);
                crewCapsuleCollider.radius = 1;
                crewCapsuleCollider.height = 5;

                //rigidbody
                Rigidbody crewRigidbody = crew.AddComponent<Rigidbody>();
               // crewRigidbody.freezeRotation = true;
                crewRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
               // crewRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                crewRigidbody.useGravity = false;

                //random movement
                crew.AddComponent<GarageSceneCrewRandomMovement>();
            }
        }


        // UI for Crew
        PopulateCrewUi(crewId);
    }

    void PopulateCrewUi(string[] crewId) 
    {
        foreach (Transform child in GameObject.Find("PanelCrew_Content").transform)
        {
            Destroy(child.gameObject);
            Debug.Log("qweqweqweq");
        }

        // 
        var crewUiItem = GameObject.Find("PanelCrewDeffaultItem");

        for (int i = 0; i < crewId.Length; i++) 
        {
            if (crewId[i] != "0")
            {
                var newCrewUiItem = Instantiate(crewUiItem) as GameObject;
                newCrewUiItem.transform.parent = GameObject.Find("PanelCrew_Content").transform;

                //image
                newCrewUiItem.transform.Find("PanelCrewDeffaultImage").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/CrewIcons/" + crewId[i]);

                // name 
                newCrewUiItem.transform.Find("PanelCrewDeffaultText").GetComponent<Text>().text = LocalizedText.Localize("DB_Crew_" + crewId[i] + "_Occupation");

                //item name in the game
                newCrewUiItem.name = crewId[i];
            }
        }
            
    }

    // change slot
    private void ChangeSlot(string slot, string playerId, string sessionToken, string slotRightAccountShipId) {
        Debug.Log("Pressed - " + slot);

        // send request to the server to get information about the ship

        string message = "2;" + playerId + ";" + sessionToken + ";0" + ";" + slotRightAccountShipId; //entering the garage - what 3 slots and what is first slot to put 3d

        string answer = TcpConnection.Connect(message);

        if (answer != "" || answer != "000")
        {
            ProcessServerAnswer(answer);
            Debug.Log("Pressed update - ");
        }
        else
        {
            Debug.Log("Error in recieve message 2 with code 0 in Garage");
        }

        //if (slot == 1 && slot1ShipNumber >= 0)
        //{
        //    Destroy(shipPlace.transform.GetChild(0).gameObject);

        //    var slotShip = Resources.Load("Models/Ships/Inside/" + (slot1ShipNumber)) as GameObject;
        //    (Instantiate(slotShip, new Vector3(0, 0, 0), Quaternion.Euler(-90f, 0f, 180f)) as GameObject).transform.parent = shipPlace.transform;
        //} 
        //else if (slot == 2 && slot2ShipNumber >= 0) {

        //    Destroy(shipPlace.transform.GetChild(0).gameObject);

        //    var slotShip = Resources.Load("Models/Ships/Inside/" + (slot2ShipNumber)) as GameObject;
        //    (Instantiate(slotShip, new Vector3(0, 0, 0), Quaternion.Euler(-90f, 0f, 180f)) as GameObject).transform.parent = shipPlace.transform;
        //}
        //else if (slot == 3 && slot3ShipNumber >= 0)
        //{
        //    Destroy(shipPlace.transform.GetChild(0).gameObject);

        //    var slotShip = Resources.Load("Models/Ships/Inside/" + (slot3ShipNumber)) as GameObject;
        //    (Instantiate(slotShip, new Vector3(0, 0, 0), Quaternion.Euler(-90f, 0f, 180f)) as GameObject).transform.parent = shipPlace.transform;
        //}
    }




    private void CheckIfModuleExist(string accountSlotId, Button moduleObject, GameObject playerShipModel, 
                                    string moduleId, string moduleName, string LoadModuleModelFolder, string LoadModuleImageFolder)
    {
        if (Convert.ToInt32(accountSlotId) == -1)
        {
            moduleObject.gameObject.SetActive(false);
        }
        else if (Convert.ToInt32(accountSlotId) == 0)
        {
            moduleObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/empty");
            moduleObject.gameObject.SetActive(true);
            ChangeModuleModel(playerShipModel, "0", moduleName, LoadModuleModelFolder);
        }
        else if (Convert.ToInt32(accountSlotId) > 0)
        {
            moduleObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/" + LoadModuleImageFolder + "/" + moduleId);
            moduleObject.gameObject.SetActive(true);
            ChangeModuleModel(playerShipModel, moduleId, moduleName, LoadModuleModelFolder);
        }
    }

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
        }
        //else
        //{
        //    return null;
        //}
    }



}



