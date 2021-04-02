using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GarageSceneInventoryItem : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    // Start is called before the first frame update
    void Start()
    {
        // item.transform.Find("InventoryDefaultItemId").GetComponent<Text>().text = Convert.ToString(playerItemID[i]);
       // item.name = "InventoryItem" + Convert.ToString(playerItemID[i]);
       // item.transform.Find("InventoryDefaultItemButton").name = "InventoryItemButton" + Convert.ToString(playerItemID[i]);
       // item.transform.Find("InventoryDefaultItemId").name = "InventoryDefaultItemId" + playerItemID[i];

    }

    public void OnPointerClick(PointerEventData eventData)
    {

       string elementId = this.name;

        if (eventData.button == PointerEventData.InputButton.Right && elementId != "")
        {
            Debug.Log("DEBUG INVENTORY ITEM CLICK" + elementId);

            //    Vector2 lastPosition = this.transform.position;
            //    Vector2 addPosition = new Vector3(250, 350);

            //    ItemRighClickInfo.transform.position = lastPosition + addPosition;

            //    ItemRighClickInfo.SetActive(true);

            //    ItemFocusInfo.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ItemFocusInfo.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPanel();
        // StartCoroutine(OnPointerEnterCoroutine());
    }


    private void ShowPanel()
    {
    }
}
