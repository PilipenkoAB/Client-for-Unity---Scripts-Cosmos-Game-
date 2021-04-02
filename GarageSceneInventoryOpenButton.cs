using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class GarageSceneInventoryOpenButton : MonoBehaviour
{
	private GameObject myCarrierObject; //cache
										
	
	//  public GameObject inventory;

	//public GameObject canvas;



	//public GameObject itemImage; // This is our prefab object that will be exposed in the inspector

	public Text uiCurrentAmount;

	public Text uiMaxAmount;

	//------------------

	private GameObject inventoryPanel;

	private GameObject inventoryContent;

	// Start is called before the first frame update
	void Start()
	{
		inventoryPanel = GameObject.Find("InventoryPanel");
		inventoryContent = GameObject.Find("InventoryContent");

		Button buttonOpenInventory = this.GetComponent<Button>();
        buttonOpenInventory.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
	{
		myCarrierObject = GameObject.Find("CacheData");
		string sessionToken = myCarrierObject.GetComponent<CacheData>().sessionToken;
		string playerId = myCarrierObject.GetComponent<CacheData>().playerId;

		// ask server for information

		string message = "2;" + playerId + ";" + sessionToken + ";" + ";2";

		string answer = TcpConnection.Connect(message);

		if (answer != "" || answer != "000")
		{
			Debug.Log("2 - 2  informtion was sended and successeful");
		}
		else
		{
			// show message that can't load inventory!
			Debug.Log("2 - 2 Fail ");
		}

		// populate inventory with information
		Populate(answer);

		inventoryPanel.transform.position = new Vector2((Screen.width) / 2, (Screen.height) / 2);
		//inventory.SetActive(true);
    }


	void Populate(string answer)
	{
		foreach (Transform child in inventoryContent.transform)
		{
			Destroy(child.gameObject);
		}

		// process answer - to array
		string[] answerObjects = answer.Split(';');

		int amount = answerObjects.Length;
		uiCurrentAmount.text = Convert.ToString(amount / 3);
		uiMaxAmount.text = "100";

		int[] playerItemID = new int[100];
		int[] typeItem = new int[amount / 3];
		int[] idItem = new int[amount / 3];



		Debug.Log("amount " + amount);
		// devide answerObjects to 3 arrays with ID[0], TypeItem[1] and IdItem[2]
		int i0 = 0;
		for (int i = 0; i < amount; i+=3)
		{

			playerItemID[i0] = Convert.ToInt32(answerObjects[i]);
			//Debug.Log(" -answerObjects[i]- " + answerObjects[i]);
			Debug.Log("i = " + i + " -playerItemId- " + playerItemID[i0]);
			i0++;
		}
		int i1 = 0;
		for (int i = 1; i < amount; i += 3)
		{
			typeItem[i1] = Convert.ToInt32(answerObjects[i]);
			//Debug.Log("i = " + i + " -typeId- " + typeItem[i1]);
			i1++;
		}
		int i2 = 0;
		for (int i = 2; i < amount; i += 3)
		{
			idItem[i2] = Convert.ToInt32(answerObjects[i]);
			//Debug.Log("i = " + i + " -itemId- " + idItem[i2]);
			i2++;
		}









		for (int i = 0; i < amount / 3; i++)
		{
			GameObject item = (Instantiate(GameObject.Find("InventoryDefaultItem")) as GameObject);
			item.transform.parent = inventoryContent.transform;

			// choose the image depends on the slot type and ID
			if (typeItem[i] == 0)
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/CockpitSlot/" + idItem[i]);
			}
			else if (typeItem[i] == 1) 
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/EngineSlot/" + idItem[i]);
			}
			else if (typeItem[i] == 2)
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/WeaponSlot/" + idItem[i]);
			}
			else if (typeItem[i] == 3)
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/BigSlot/" + idItem[i]);
			}
			else if (typeItem[i] == 4)
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/MediumSlot/" + idItem[i]);
			}
			else if (typeItem[i] == 5)
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/SmallSlot/" + idItem[i]);
			}
			else 
			{
				item.transform.Find("InventoryDefaultItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/ItemsIcons/Error/");
			}

			item.name = "InventoryItem" + Convert.ToString(playerItemID[i]);
			item.transform.Find("InventoryDefaultItemButton").name = Convert.ToString(playerItemID[i]);

		}

	}

}
