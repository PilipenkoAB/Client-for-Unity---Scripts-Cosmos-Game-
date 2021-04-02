using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GarageSceneCircleItemActions : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    private GameObject ItemRighClickInfo;

    private GameObject ItemFocusInfo;
    private Text ItemFocusInfoText1;
    private Text ItemFocusInfoText;

    // rightclick next
    private Button ItemRighClickInfoButtonInfo;
    private Button ItemRighClickInfoButtonRemove;
    private GameObject ItemFullInfoPanel;
    private Button ItemFullInfoCloseButton;
    void Start()
    {
        // focus
        ItemFocusInfo = GameObject.Find("ItemFocusInfo");
        ItemFocusInfoText1 = GameObject.Find("ItemFocusInfoText1").GetComponent<Text>();
        ItemFocusInfoText = GameObject.Find("ItemFocusInfoText").GetComponent<Text>();

        //
        ItemRighClickInfo = GameObject.Find("ItemRighClickInfo");

        // right click buttons
        ItemRighClickInfoButtonInfo = GameObject.Find("ItemRighClickInfoButtonInfo").GetComponent<Button>();
        ItemRighClickInfoButtonRemove = GameObject.Find("ItemRighClickInfoButtonRemove").GetComponent<Button>();
        ItemFullInfoPanel = GameObject.Find("ItemFullInfoPanel");
        ItemFullInfoCloseButton = GameObject.Find("ItemFullInfoCloseButton").GetComponent<Button>();

        ItemRighClickInfoButtonInfo.onClick.AddListener(ShowFullInfoPanel);
        ItemRighClickInfoButtonRemove.onClick.AddListener(RemoveItem);
        ItemFullInfoCloseButton.onClick.AddListener(CloseFullInfoPanel);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string elementId = this.GetComponent<UnityEngine.UI.Image>().sprite.name;

        if (eventData.button == PointerEventData.InputButton.Right && elementId != "Empty") 
        {
            Vector2 lastPosition = this.transform.position;
            Vector2 addPosition = new Vector3(250, 350);

            ItemRighClickInfo.transform.position = lastPosition + addPosition;

            ItemRighClickInfo.SetActive(true);

            ItemFocusInfo.SetActive(false);
        } 
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        ItemFocusInfo.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPanel();
       // StartCoroutine(OnPointerEnterCoroutine());
    }

    // focus delay
    //IEnumerator OnPointerEnterCoroutine()
    //{
    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(0.25F);
    //    ShowPanel();

    //}

    
    private void ShowPanel()
    {
        Vector2 lastPosition = this.transform.position;
        Vector2 addPosition = new Vector3(350, 200);

        ItemFocusInfo.transform.position = lastPosition + addPosition;

        // text
        string elementId = this.GetComponent<UnityEngine.UI.Image>().sprite.name;

        if (elementId != "Empty")
        {
            ItemFocusInfoText.text = LocalizedText.Localize("Garage_ItemFocusInfoText");

            if (this.name == "EngineSlot")
            {
                ItemFocusInfoText1.text = LocalizedText.Localize("DB_Engine_" + elementId + "_Name");
            }
            else if (this.name == "CockpitSlot")
            {
                ItemFocusInfoText1.text = LocalizedText.Localize("DB_Cockpit_" + elementId + "_Name");
            }
            else if (this.name == "BigSlot1" || this.name == "BigSlot2" || this.name == "BigSlot3" || this.name == "BigSlot4" || this.name == "BigSlot5")
            {
                ItemFocusInfoText1.text = LocalizedText.Localize("DB_BigSlot_" + elementId + "_Name");
            }
            else if (this.name == "Weapon1" || this.name == "Weapon2" || this.name == "Weapon3" || this.name == "Weapon4" || this.name == "Weapon5")
            {
                ItemFocusInfoText1.text = LocalizedText.Localize("DB_Weapon_" + elementId + "_Name");
            }
            else
            {
                ItemFocusInfoText1.text = "?????";
            }
        }
        else
        {
            ItemFocusInfoText1.text = LocalizedText.Localize("Garage_ItemFocusInfoTextEmptySlot");
            ItemFocusInfoText.text = null;
        }

        ItemFocusInfo.SetActive(true);
    }


    // right click - > next buttons

    private void ShowFullInfoPanel() 
    {
        Vector2 lastPosition = this.transform.position;
        Vector2 addPosition = new Vector3(350, 200);

        ItemFullInfoPanel.transform.position = lastPosition + addPosition;

        ItemFullInfoPanel.SetActive(true);
    }

    private void RemoveItem() { }

    private void CloseFullInfoPanel()
    {
        ItemFullInfoPanel.SetActive(false);
    }

}
