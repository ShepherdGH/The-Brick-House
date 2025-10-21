using System;
using System.Collections.Generic;
using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;

public class Plate : MonoBehaviour
{
    private DishBase _dish;
    private Canvas _canvas;
    private IngredientBubble _bubbleCanvas;

    [SerializeField] private GameObject Steak, Stew, Salad, Burger;
    
    [SerializeField] private Mesh SteakMesh, StewMesh, SaladMesh, BurgerMesh;    
    [SerializeField] private Material SteakMat, StewMat, SaladMat, BurgerMat;

    private GameObject currentChild;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dish = new DishBase();
        _canvas = GetComponentInChildren<Canvas>();
        _bubbleCanvas = _canvas.GetComponent<IngredientBubble>();

        _canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddIngredient(IngredientType ingredient)
    {
        // add the ingredient
        bool added = _dish.AddIngredient(ingredient);

        if (added) 
        {
            // create new bubble in canvas
            IngredientType newIngredient = _dish.GetRecentAdd();
            if (newIngredient != IngredientType.NULL)
            {
                _canvas.enabled = true;
                _bubbleCanvas.AddOneBubble(newIngredient);
            }
            else
            {
                Debug.Log("recent add ingredient failed: IngredientType.NULL");
            }
        }

        if (_dish.SameDish("Steak"))
        {
            ChangeSpriteChildren(Steak);
            return added;
        }
        if (_dish.SameDish("Salad"))
        {
            ChangeSpriteChildren(Salad);
            return added;
        }
        
        if (currentChild != null)
        {
            Destroy(currentChild);
            currentChild = null;
        }
        
        if (_dish.SameDish("Burger"))
        {
            ChangeSprite(Burger);
            return added;
        }
        if (_dish.SameDish("Stew"))
        {
            ChangeSprite(Stew);
            return added;
            
        }
        
        return added;
    }

    public bool IsDish(string dishName)
    {
        return _dish.SameDish(dishName);
    }

    public List<IngredientType> GetIngredients()
    {
        return _dish.GetIngredients();
    }

    private void ChangeSprite(GameObject sprite)
    {
        gameObject.GetComponent<MeshFilter>().mesh = sprite.GetComponent<MeshFilter>().sharedMesh;
        gameObject.GetComponent<MeshRenderer>().material = sprite.GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void ChangeSpriteChildren(GameObject sprite)
    {
        currentChild = Instantiate(sprite, this.transform);
    }
}
