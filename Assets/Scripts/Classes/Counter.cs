using Unity.VisualScripting;
using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;
using static EnumLibrary.EquipmentType;

public class Counter : MonoBehaviour, ITakeable
{
    private bool _hasItem;
    
    private GameObject _heldGameObject;
    
    private IngredientType _ingredient;

    private Material _myMaterial;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _myMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Take(GameObject player)
    {
        PlayerStatus statusScript = player.GetComponent<PlayerStatus>();

        IngredientType playerHeldIngredient = statusScript.GetHeldIngredient();
        
        Debug.Log($"Called Counter Take with {playerHeldIngredient}");
        
        if (playerHeldIngredient == NULL)
        {
            if (!_hasItem)
            {
                return;
            }
            
            statusScript.SetHeldObject(_heldGameObject);
            statusScript.SetHeldIngredient(_ingredient);
            
            if (_ingredient == PLATE)
            {
                if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("takingPlate");
            }
            else
            {
                if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("takingIngredients");
            }
            
            _ingredient = NULL;
            SetHeldParent(player);
            _heldGameObject = null;
            
            _hasItem = false;
            return;
        }

        if (_hasItem == false)
        {
            if (playerHeldIngredient == PLATE)
            {
                _hasItem = true;
                _ingredient = PLATE;
                _heldGameObject = statusScript.GetHeldObject();
                statusScript.SetHeldParenToEquipment(this.gameObject);
                
                SoundManager.S.PlaySoundEffect("settingDish");
                
                return;
            }

            if (_ingredient == PLATE)
            {
                if (AddIngredientToPlate(playerHeldIngredient)) statusScript.RemoveHeld();
                return;
            }
            
            _heldGameObject = statusScript.GetHeldObject();
            statusScript.SetHeldParenToEquipment(this.gameObject);
            _ingredient = playerHeldIngredient;
            _hasItem = true;
        }

        if (_ingredient == PLATE)
        {
            if (AddIngredientToPlate(playerHeldIngredient)) statusScript.RemoveHeld();
        }
    }

    private bool AddIngredientToPlate(IngredientType ingredient)
    {
        return _heldGameObject.GetComponent<Plate>().AddIngredient(ingredient);
    }
    
    private void SetHeldParent(GameObject parent)
    {
        _heldGameObject.transform.parent = parent.transform;
        _heldGameObject.transform.localPosition = new Vector3(0, 1, 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        _myMaterial.EnableKeyword("_EMISSION");
        _myMaterial.SetColor("_EmissionColor", Color.gray);
    }

    private void OnTriggerExit(Collider other)
    {
        _myMaterial.SetColor("_EmissionColor", Color.black);
    }
    
    
}
