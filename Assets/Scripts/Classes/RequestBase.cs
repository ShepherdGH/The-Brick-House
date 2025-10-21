using UnityEngine;
using static EnumLibrary;


[CreateAssetMenu(fileName = "NewRecipe", menuName = "Game/Recipe")]
public class RequestBase : ScriptableObject 
{
    public string recipeName;
    public IngredientType[] ingredients;
    public float orderDuration = 60f;
    public float price = 0f;

    public Sprite recipeImg;
}
