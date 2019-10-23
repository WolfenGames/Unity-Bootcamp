using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    #region Singleton Access
    private static InventorySystem instance;//Use of a singleton here, needs to be static in order for other scripts to access it.

    public static InventorySystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InventorySystem>();
            }

            return InventorySystem.instance;
        }
    }
    #endregion

    public int inventorySize = 20;
    public GameObject uiSlot;
    public List<CollectableItems> inventory = new List<CollectableItems>();
    private GameObject inventoryPanel;
    private CanvasGroup canvasGroup;
    private bool isOpen = false;

    void Awake()
    {
        inventoryPanel = GameObject.Find("Inventory Panel");
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void BringUpInventory()
    {
        bool newState = !canvasGroup.interactable;
        canvasGroup.interactable = newState;
        canvasGroup.blocksRaycasts = newState;
        canvasGroup.alpha = (newState == true) ? 1 : 0;
        UIController.Instance.guiIsOpen = newState;
        isOpen = !isOpen;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen == true)
        {
            BringUpInventory();
            return;
        }

        if (Input.GetKeyDown(KeyCode.I))
            if(UIController.Instance.guiIsOpen == false && isOpen == false || isOpen == true)
                BringUpInventory();
    }

    public void AddToInventory(CollectableItems item)
    {
        GameObject createdSlot = GameObject.Instantiate(uiSlot, inventoryPanel.transform);
        CollectableItems collectableItem = createdSlot.GetComponent<CollectableItems>();
        collectableItem.icon = Resources.Load<Sprite>("Weapons/" + item.itemName);
        collectableItem.CopyContents(item);
        collectableItem.prefab = Resources.Load<GameObject>("Weapons/" + item.itemName);

        //place the remaining into their own slot
        for (int i = 0; i < inventoryPanel.transform.childCount; i++)
        {
            if (inventoryPanel.transform.GetChild(i).childCount == 0)
            {
                collectableItem.transform.SetParent(inventoryPanel.transform.GetChild(i));
                collectableItem.ResetRectInSlot(collectableItem.rect);
                GameObject.Destroy(item.gameObject);
                break;
            }
        }
    }
}
