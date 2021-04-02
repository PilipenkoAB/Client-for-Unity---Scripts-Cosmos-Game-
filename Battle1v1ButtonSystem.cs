using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Battle1v1ButtonSystem : MonoBehaviour
{
    private GameObject myCarrierObject; //cache

    //public Button uiButtonWeapon1;

    private bool weaponSelected = false;
    private int idWeaponSelected;

    private bool bulletReady = false;
    private int[] weaponAIM = new int[] { -1,-1,-1,-1,-1 }; // i - weaponid, number - module to attack

    Ray ray;
    RaycastHit hit;


    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        //look if buttons exist and put listener to them
        if (GameObject.Find("PlayerWeaponButton1") != null)
        { 
            GameObject.Find("PlayerWeaponButton1").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { TaskOnClick(0); });
        }
        else if (GameObject.Find("PlayerWeaponButton2") != null)
        {
            GameObject.Find("PlayerWeaponButton2").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { TaskOnClick(1); });
        }
        else if (GameObject.Find("PlayerWeaponButton3") != null)
        {
            GameObject.Find("PlayerWeaponButton3").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { TaskOnClick(2); });
        }
        else if (GameObject.Find("PlayerWeaponButton4") != null)
        {
            GameObject.Find("PlayerWeaponButton4").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { TaskOnClick(3); });
        }
        else if (GameObject.Find("PlayerWeaponButton5") != null)
        {
            GameObject.Find("PlayerWeaponButton5").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => { TaskOnClick(4); });
        }
    }

    private void Update()
    {
        if (weaponSelected == true) 
        {
            ray = GameObject.Find("SecondCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var mainMaterial = hit.collider.GetComponent<Renderer>().material;
                hit.collider.GetComponent<Renderer>().material = Resources.Load("Materials/RegularOutlineMaterial") as Material;
                
                if (Input.GetMouseButtonDown(0)) 
                {
                    // print(hit.collider.name);

                    string moduleName = ""; // 0 - 11 (engine,cockpit,big,medium)
                    string weaponId = ""; // 0 - 4

                    //for test
                    weaponId = Convert.ToString(idWeaponSelected);
                    //---

                    if (hit.collider.name == "EngineSlot")
                    {
                        moduleName = "0";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected+1) + "TextReloadCurrent").GetComponent<Text>().text) > 0 )
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "CockpitSlot")
                    {
                        moduleName = "1";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "BigSlot1")
                    {
                        moduleName = "2";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "BigSlot2")
                    {
                        moduleName = "3";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "BigSlot3")
                    {
                        moduleName = "4";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "BigSlot4")
                    {
                        moduleName = "5";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "BigSlot5")
                    {
                        moduleName = "6";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "MediumSlot1")
                    {
                        moduleName = "7";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "MediumSlot2")
                    {
                        moduleName = "8";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected+1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "MediumSlot3")
                    {
                        moduleName = "9";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "MediumSlot4")
                    {
                        moduleName = "10";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }
                    else if (hit.collider.name == "MediumSlot5")
                    {
                        moduleName = "11";

                        hit.collider.GetComponent<Renderer>().material = mainMaterial;
                        UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                        weaponSelected = false;

                        if (Convert.ToInt32(GameObject.Find("PlayerWeapon" + (idWeaponSelected + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0)
                        {
                            weaponAIM[idWeaponSelected] = Convert.ToInt32(moduleName);
                            // set image on the slot
                            GameObject.Find("PlayerAimSpotWeapon" + (idWeaponSelected + 1)).transform.position = Input.mousePosition;
                        }
                        else
                        {
                            SendInfoToTheServer(moduleName, weaponId);
                        }
                    }


                }

                hit.collider.GetComponent<Renderer>().material = mainMaterial;
            }

            if (Input.GetMouseButtonDown(1)) 
            {
                UnityEngine.Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                weaponSelected = false;
            }

            
        }

        for (int i = 0; i < weaponAIM.Length; i++)
        {
            if (weaponAIM[i] != -1 && Convert.ToInt32(GameObject.Find("PlayerWeapon" + (i + 1) + "TextReloadCurrent").GetComponent<Text>().text) == 0)
            {
                SendInfoToTheServer(Convert.ToString(weaponAIM[i]), Convert.ToString(i));
                weaponAIM[i] = -1;
                // remove image 
                // set image on the slot
                GameObject.Find("PlayerAimSpotWeapon" + (i + 1)).transform.position = new Vector2(-149, -1850); // ????
            } else if (weaponAIM[i] != -1 && Convert.ToInt32(GameObject.Find("PlayerWeapon" + (i + 1) + "TextReloadCurrent").GetComponent<Text>().text) > 0 && GameObject.Find("PlayerWeaponCurrentEnergy" + Convert.ToString(i)).GetComponent<Text>().text == "0") 
            {
                weaponAIM[i] = -1;
                GameObject.Find("PlayerAimSpotWeapon" + (i + 1)).transform.position = new Vector2(-149, -1850); // ????
            }
        }

    }

    void TaskOnClick(int idWeapon)
    {
        if (GameObject.Find("PlayerWeaponCurrentEnergy" + Convert.ToString(idWeapon)) != null && GameObject.Find("PlayerWeaponCurrentEnergy" + Convert.ToString(idWeapon)).GetComponent<Text>().text != "0") 
        {
            idWeaponSelected = idWeapon;
            Debug.Log("WEAPON ID - " + idWeapon);
            weaponSelected = true;
            UnityEngine.Cursor.SetCursor(Resources.Load<Texture2D>("Images/BattleModulesIcons/" + "EngineSlot"), Vector2.zero, CursorMode.Auto);

        }
    }

    void SendInfoToTheServer(string moduleId, string weaponId) 
    {
        //Load cache 
        myCarrierObject = GameObject.Find("CacheData");
        string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
        string playerId = myCarrierObject.GetComponent<CacheData>().playerId;
        int battleSessionId = myCarrierObject.GetComponent<CacheData>().battleSessionId;

        string message = "3;" + playerId + ";" + sessionToken + ";" + battleSessionId + ";3" + ";3"+";"+ weaponId + ";"+ moduleId;

        string answer = TcpConnection.Connect(message);
        if (answer != "" || answer != "000")
        {
            Debug.Log("3 - 3 - 3 informtion was sended and successeful");

            if (answer == "1")
            {
              //  WeaponProjectile(moduleId, Convert.ToInt32(weaponId));
            }
        }
        else
        {
            Debug.Log("3 - 3 - 3  Fail ");
        }


    }

    //void WeaponProjectile(string moduleId, int weaponId) 
    //{
    //    bulletReady = true;
    //    GameObject bullet = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), transform.position, Quaternion.identity) as GameObject;
    //    bullet.name = "Weapon"+ weaponId+"Projectile"+1;

    //}


}
