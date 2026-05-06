using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemScrollbar : MonoBehaviour
{
    private Scrollbar scrollbar;
    public int barValue;
    private InventoryManager inventoryManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scrollbar = GetComponent<Scrollbar>();
        scrollbar.onValueChanged.AddListener(OnScroll);
        inventoryManager = FindAnyObjectByType<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnScroll(float value)
    {
        barValue = (int)(Math.Round(value, 1) * 200);
        inventoryManager.GenerateButtons(barValue);
    }
}
