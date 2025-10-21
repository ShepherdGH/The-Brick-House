using System.Collections.Generic;
using UnityEngine;

public static class EnumLibrary
{
    public enum IngredientType
    {
        NULL,
        PLATE,
        UnskinnedMeat,
        RawMeat,
        ChoppedMeat,
        FriedMeat,
        Potato,
        ChoppedPotato,
        FriedPotato,
        Lettuce,
        ChoppedLettuce,
        Tomato,
        ChoppedTomato,
        Bread
    }
    
    public enum EquipmentType
    {
        FryingPan,
        CuttingBoard,
    }
    
    private static Dictionary<EquipmentType, HashSet<IngredientType>> _equipmentToIngredent =
        new Dictionary<EquipmentType, HashSet<IngredientType>>()
        {
            { EquipmentType.FryingPan, new HashSet<IngredientType>
                {
                    IngredientType.RawMeat,
                    IngredientType.ChoppedPotato,
                }
            },
            { EquipmentType.CuttingBoard, new HashSet<IngredientType>
                {
                    IngredientType.UnskinnedMeat,
                    IngredientType.Potato,
                    IngredientType.Lettuce,
                    IngredientType.Tomato
                }
            },
        };
    
    public static bool CorrectOperationCheck(EquipmentType equipment, IngredientType ingredient)
    {
        return _equipmentToIngredent[equipment].Contains(ingredient);
    }
}
