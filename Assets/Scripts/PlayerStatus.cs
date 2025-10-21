using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;
using static EnumLibrary.EquipmentType;

public class PlayerStatus : MonoBehaviour
{
    private IngredientType _heldIngredient;
    private GameObject _heldGameObject;
    
    [SerializeField] private float _incrementAmount = 2f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _heldIngredient = NULL;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IngredientType GetHeldIngredient()
    {
        return _heldIngredient;
    }

    public void SetHeldIngredient(IngredientType ingredient)
    {
        _heldIngredient = ingredient;
    }

    public GameObject GetHeldObject()
    {
        return _heldGameObject;
    }
    
    public void SetHeldObject(GameObject input)
    {
        _heldGameObject = input;
    }
    
    public void DestroyHeldObject()
    {
        Destroy(_heldGameObject);
        _heldGameObject = null;
    }
    
    public float GetIncrementAmount()
    {
        return _incrementAmount;
    }

    public void RemoveHeld()
    {
        _heldIngredient = NULL;
        DestroyHeldObject();
    }

    public void SetHeldParenToEquipment(GameObject parent)
    {
        _heldGameObject.transform.parent = parent.transform;
        _heldGameObject.transform.localPosition = new Vector3(0, 1, 0);
        _heldIngredient = NULL;
        _heldGameObject = null;
    }
}
