using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Newtonsoft.Json;
using rtome.Scripts.Controllers.Body;
using rtome.Scripts.Controllers.Header;
using rtome.Scripts.Controllers.Footer;
using rtome.Scripts.ScriptedObjects;
using rtome.Scripts.Services;
using Sirenix.OdinInspector;
using UnityEngine;


public class MainServiceController : MonoBehaviour
{
    [Title("Component Controllers: ")] 
    public HeaderController headerController;
    public FooterController footerController;
    [Title("Body Component Controllers")] 
    public MainViewController mainViewController;
    public PantryInventoryViewController pantryInventoryViewController;
    public GroceryListViewController groceryListViewController;
    [Title("Inventories:")]
    [ShowInInspector, EnableGUI] public List<GroceryItem> groceryListInventory;
    [ShowInInspector, EnableGUI] public GroceryList outOfStockInventory;
    [ShowInInspector, EnableGUI] public PantryInventory pantryInventory;
    
    private void Awake()
    {
        if (pantryInventory == null)
            pantryInventory = Resources.Load("PantryInventory") as PantryInventory;

        if (groceryListInventory == null)
            groceryListInventory = new List<GroceryItem>();
        if (outOfStockInventory == null)
            outOfStockInventory = new GroceryList();
        
        //LoadPantryItemsFromWeb();
        //PostTestGroceryItems();
        LoadGroceryListFromWeb();
    }

    private void Start()
    {
        footerController.ReturnHomeRequest += HandleOnHomeButtonPressed;
    }

    private void OnDestroy()
    {
        footerController.ReturnHomeRequest -= HandleOnHomeButtonPressed;
    }

    public void SetPantryViewActive()
    {
        mainViewController.gameObject.SetActive(false);
        pantryInventoryViewController.gameObject.SetActive(true);
        groceryListViewController.gameObject.SetActive(false);
    }
    
    public void SetGroceryViewActive()
    {
        mainViewController.gameObject.SetActive(false);
        pantryInventoryViewController.gameObject.SetActive(false);
        groceryListViewController.gameObject.SetActive(true);
    }

    public void SetMainViewActive()
    {
        mainViewController.gameObject.SetActive(true);
        pantryInventoryViewController.gameObject.SetActive(false);
        groceryListViewController.gameObject.SetActive(false);
    }
    
    public void SetHeaderText(string leftText, string rightText = "")
    {
        headerController.SetHeaderText(leftText, rightText);
    }

    public void LoadPantryItemsFromWeb()
    {
        try
        {
            var res = WebDataAccessor.GetPantryInventory();
            var objects = JsonUtility.FromJson<PantryItem[]>(res);
            if (objects == null
                || objects.Length == 0)
            {
                Debug.Log("Null objects");
                pantryInventory.data = new Dictionary<string, PantryItem>();
                return;
            }

            pantryInventory.data = new Dictionary<string, PantryItem>();
            foreach (var obj in objects)
            {
                obj.id = Guid.NewGuid().ToString();
                pantryInventory.Add(obj);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void PostTestGroceryItems()
    {
        try
        {
            var groceryItem = new GroceryItem(Guid.NewGuid().ToString()
                , "Apple"
                , "6"
                , "0.79");
            WebDataAccessor.PostGroceryItem(groceryItem);
            var groceryItem2 = new GroceryItem(Guid.NewGuid().ToString()
                , "Orange"
                , "4"
                , "1.29");
            WebDataAccessor.PostGroceryItem(groceryItem2);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void LoadGroceryListFromWeb()
    {
        var objs = WebDataAccessor.GetGroceryItems();
        if (objs.Length <= 0)
            return;
        groceryListInventory = objs.ToList();
    }

    private void HandleOnHomeButtonPressed(object sender, EventArgs args)
    {
        SetMainViewActive();
    }
}
