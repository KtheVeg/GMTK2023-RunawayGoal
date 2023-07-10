using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscPlayerHandler : MonoBehaviour
{
    MiscControls controls;

    [Header("Controls")]
    [SerializeField]
    Vector2 test;

    [Header("Inventory")]
    [SerializeField]
    int itemSelected = 0;

    [Header("Object References")]
    [SerializeField]
    GameObject[] inventoryItems;
    [SerializeField]
    UnityEngine.UI.Image inventoryItemSelector;
    [SerializeField]
    TMPro.TextMeshProUGUI inventoryItemDescription;
    [SerializeField]
    TMPro.TextMeshProUGUI inventoryItemCount;
    [SerializeField]
    GameObject PauseMenu;
    [SerializeField]
    GameFlow gameFlow;
    [SerializeField]
    GameObject capsuleDisplay;
    [SerializeField]
    GameObject flagpoleDisplay;
    [SerializeField]
    ParticleSystem flagpoleParticles;
    
    [Header("Constants")]
    readonly string[] inventoryItemDescriptions = {"Blocks the enemy.", "When triggered, expands to push away the enemy", "When triggered, will slow the enemy down for a short time"};
    void Awake()
    {
        controls = new MiscControls();
        controls.Inventory.Enable();
        controls.Global.Enable();
        controls.Inventory.DeployItem.performed += ctx => DeployItem();
        controls.Inventory.CycleItem.performed += ctx =>
        {
            itemSelected++;
            if (itemSelected >= 3)
                itemSelected = 0;
            inventoryItemDescription.text = inventoryItemDescriptions[itemSelected];
            inventoryItemSelector.rectTransform.anchoredPosition = new Vector2(inventoryItemSelector.rectTransform.anchoredPosition.x, -itemSelected * 50);
        };
        controls.Global.Pause.performed += ctx =>
        {
            
            PauseMenu.SetActive(!PauseMenu.activeSelf);
            Time.timeScale = PauseMenu.activeSelf ? 0 : 1;
            Cursor.lockState = PauseMenu.activeSelf ? CursorLockMode.None : CursorLockMode.Locked;
        };
    }
    void Update()
    {
        inventoryItemCount.text = "<u>Items:</u>" + gameFlow.remainingInventoryItems.ToString();
    }
    void DeployItem()
    {
        if (gameFlow.remainingInventoryItems <= 0)
            return;
        gameFlow.remainingInventoryItems--;
        GameObject.Instantiate(inventoryItems[itemSelected], transform.position, transform.rotation);
    }
    public void EnterFlagpoleMode()
    {
        capsuleDisplay.SetActive(false);
        flagpoleDisplay.SetActive(true);
        flagpoleParticles.Play();
    }
    public void EnterCapsuleMode()
    {
        capsuleDisplay.SetActive(true);
        flagpoleDisplay.SetActive(false);
        flagpoleParticles.Play();
    }
}
