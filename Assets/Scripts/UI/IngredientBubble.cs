using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static EnumLibrary;

public class IngredientBubble : MonoBehaviour
{
    [Header("Bubble Panel")]
    public GameObject allBubblesPanel;
    public GameObject singleBubblePrefab;

    [Header("Ingredient Sprites")]
    public Sprite breadBubbleSprite;
    public Sprite mushroomBubbleSprite;
    public Sprite lettuceBubbleSprite;
    public Sprite tomatoBubbleSprite;
    public Sprite meatBubbleSprite;

    // reference to other things
    private Plate plateScript;
    private List<IngredientType> ingredientsList;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOneBubble(IngredientType type)
    {
        GameObject newBubbleObject = Instantiate(singleBubblePrefab, allBubblesPanel.transform);
        SingleBubble bubbleScript = newBubbleObject.GetComponent<SingleBubble>();
        bubbleScript.SetType(type);

        Debug.Log("adding bubble for type: " + type);

        switch (type)
        {
            case (IngredientType.Bread):
                bubbleScript.SetBubbleSprite(breadBubbleSprite);
                break;
            case (IngredientType.FriedPotato):
                bubbleScript.SetBubbleSprite(mushroomBubbleSprite);
                break;
            case (IngredientType.ChoppedLettuce):
                bubbleScript.SetBubbleSprite(lettuceBubbleSprite);
                break;
            case (IngredientType.ChoppedTomato):
                bubbleScript.SetBubbleSprite(tomatoBubbleSprite);
                break;
            case (IngredientType.FriedMeat):
                bubbleScript.SetBubbleSprite(meatBubbleSprite);
                break;
            default:
                Debug.Log("Failed to set bubbble type for ingredient type: " + type);
                break;
        }
    }
}
