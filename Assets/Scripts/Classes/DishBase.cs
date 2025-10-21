using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;

public class DishBase
{
    [SerializeField] private Stack<IngredientType> _dish;
    private IngredientType _recentAdd;

    private bool _FMeat, _FPot, _CLet, _CTom, _Bread;
    
    public Stack<IngredientType> GetDish()
    {
        return _dish;
    }

    private bool And(bool b1, bool b2 = true, bool b3 = true, bool b4 = true, bool b5 = true)
    {
        return b1 && b2 && b3 && b4 && b5;
    }
    
    public bool SameDish(String dishName)
    {
        switch (dishName)
        {
            case "Burger":
                return And(_FMeat, !_FPot, _CLet, _CTom, _Bread);
            case "Salad":
                return And(!_FMeat, !_FPot, _CLet, _CTom, !_Bread);
            case "Steak":
                return And(_FMeat, !_FPot, !_CLet, !_CTom, !_Bread);
            case "Stew":
                return And(_FMeat, _FPot, !_CLet, _CTom, !_Bread);
        }

        return false;
    }

    public bool AddIngredient(IngredientType ingredient)
    {
        switch (ingredient)
        {
            case FriedMeat:
                if (And(!_FMeat))
                {
                    _FMeat = true;
                    _recentAdd = FriedMeat;
                    return true;
                }

                return false;
            case FriedPotato:
                if (And(!_Bread, !_FPot, !_CLet))
                {
                    _FPot = true;
                    _recentAdd = FriedPotato;
                    return true;
                }

                return false;
            case ChoppedLettuce:
                if (And(!_CLet, !_FPot))
                {
                    _CLet = true;
                    _recentAdd = ChoppedLettuce;
                    return true;
                }

                return false;
            case ChoppedTomato:
                if (And(!_CTom))
                {
                    _CTom = true;
                    _recentAdd = ChoppedTomato;
                    return true;
                }

                return false;
            case Bread:
                if (And(!_Bread, !_FPot))
                {
                    _Bread = true;
                    _recentAdd = Bread;
                    return true;
                }

                return false;
        }

        return false;
    }
    
    public List<IngredientType> GetIngredients()
    {
        List<IngredientType> L = new List<IngredientType>();
        
        if (_FMeat) L.Add(FriedMeat);
        if (_CLet) L.Add(ChoppedLettuce);
        if (_Bread) L.Add(Bread);
        if (_FPot) L.Add(FriedPotato);
        if (_CTom) L.Add(ChoppedTomato);

        return L;
    }

    public IngredientType GetRecentAdd()
    {
        return _recentAdd;
    }
}
