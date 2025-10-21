using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;

public class Window : MonoBehaviour, ITakeable
{
    private Dictionary<String, int> _satisfiedDishCount;

    private void Start()
    {
        _satisfiedDishCount = new Dictionary<string, int>()
        {
            { "Steak", 0 },
            { "Burger", 0 },
            { "Salad", 0 },
            { "Stew", 0 }
        };
    }

    public void Take(GameObject player)
    {
        PlayerStatus status = player.GetComponent<PlayerStatus>();

        if (status.GetHeldIngredient() == PLATE)
        {
            GameObject plate = status.GetHeldObject();
            Plate plateScript = plate.GetComponent<Plate>();
            bool dishAccepted = false;
            
            foreach (var R in OrderManager.S.activeOrderList)
            {
                Debug.Log($"Checking {R.recipeName} and got {plateScript.IsDish(R.recipeName)}");
                if (plateScript.IsDish(R.recipeName))
                {
                    _satisfiedDishCount[R.recipeName] += 1;
                    float returnPrice = 0f;
                    float percentTimeLeft = 0f;
                    
                    Debug.Log($"Order Duration: {R.orderDuration}");
                    
                    switch (R.recipeName)
                    {
                        case "Burger":
                            percentTimeLeft = R.orderDuration / 60;
                            break;
                        
                        case "Salad":
                            percentTimeLeft = R.orderDuration / 30;
                            break;
                        
                        case "Steak":
                            percentTimeLeft = R.orderDuration / 30;
                            break;
                        
                        case "Stew":
                            percentTimeLeft = R.orderDuration / 60;
                            break;
                    }
                    
                    Debug.Log($"Percent Time Left: {percentTimeLeft}");
                    
                    if (percentTimeLeft > 0.5)
                    {
                        returnPrice = R.price;
                    }
                    else if (percentTimeLeft > 0.25)
                    {
                        returnPrice = (int)(R.price * 0.75f);
                    }
                    else
                    {
                        returnPrice = (int)(R.price * 0.50f);
                    }
                    
                    Debug.Log($"Return Price: {returnPrice}");
                    
                    OrderManager.S.DeleteOrderFromActiveList(R);
                    GameManager.IncrementCurrentLevelMoney(returnPrice);
                    
                    dishAccepted = true;
                    break;
                }
            }

            if (dishAccepted)
            {
                status.RemoveHeld();
                if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("servingDish");
            }
        }
    }
}
