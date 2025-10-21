using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using static EnumLibrary;
using static EnumLibrary.IngredientType;
using static EnumLibrary.EquipmentType;

public class EquipmentBase : MonoBehaviour, IEquipment, ITakeable
{
    [SerializeField] private float _progressMax = 10f;
    [SerializeField] private EquipmentType _myType;
    [SerializeField] private bool _operatable;
    
    [SerializedDictionary("Ingredient", "ProcessedIngredient")] 
    public SerializedDictionary<IngredientType, IngredientType> _ingredientToProcessed;
    
    [SerializeField] private float _naturalProgressGrowth = 0.1f;

    [SerializeField] private GameObject
        UnskinnedMeatGO,
        RawMeatGO,
        ChoppedMeatGO,
        FriedMeatGO;

    [SerializeField] private GameObject
        PotatoGO;

    [SerializeField] private GameObject
        ChoppedPotatoGO;

    [SerializeField] private GameObject
        FriedPotatoGO;

    [SerializeField] private GameObject
        LettuceGO,
        ChoppedLettuceGO,
        TomatoGO,
        ChoppedTomatoGO;


    private Dictionary<IngredientType, GameObject> _ingredientToGameObject;
        
    
    private IngredientType _currIngredient;
    
    private float _progress;
    
    private bool _processing;


    // UI
    private Canvas bubbleCanvas;  // canvas
    private FoodProgressSlider sliderScript;

    private Material _myMaterial;
    
    protected virtual void Start()
    {
        _ingredientToGameObject = new Dictionary<IngredientType, GameObject>()
        {
            { UnskinnedMeat, UnskinnedMeatGO },
            { RawMeat, RawMeatGO },
            { ChoppedMeat, ChoppedMeatGO },
            { ChoppedPotato, ChoppedPotatoGO },
            { FriedMeat, FriedMeatGO },
            { FriedPotato, FriedPotatoGO},
            { ChoppedLettuce, ChoppedLettuceGO },
            { ChoppedTomato, ChoppedTomatoGO },
            { Potato, PotatoGO},
            { Lettuce, LettuceGO },
            { Tomato, TomatoGO }
        };

        bubbleCanvas = GetComponentInChildren<Canvas>();
        bubbleCanvas.enabled = false;
        sliderScript = GetComponentInChildren<FoodProgressSlider>();
        sliderScript.StartNewSlider();
        
        _myMaterial = GetComponentInChildren<MeshRenderer>().material;
    }

    protected void Update()
    {
        if (_processing)
        {
            _progress += _naturalProgressGrowth * Time.deltaTime;
            sliderScript.UpdateSlider(GetProgress() / GetMaxProgress());
        }
        
        CheckProgress();
        
        AnimateState();
    }

    public void incrementProgress(float increment)
    {
        if (_operatable)
        {
            _progress += increment;
            Debug.Log($"Added Progress by {increment}, progress is now {_progress}");
        }
    }

    protected void CheckProgress()
    {
        if (_progress >= _progressMax)
        {
            _progress = 0f;
            _processing = false;
            _currIngredient = _ingredientToProcessed[_currIngredient];
        }
    }

    private bool AddIngredient(IngredientType ingredient)
    {
        if (_currIngredient == NULL && CorrectOperationCheck(_myType, ingredient))
        {
            _currIngredient = ingredient;

            _processing = true;
            bubbleCanvas.enabled = true;  // enable canvas
            sliderScript.StartNewSlider();
            
            Debug.Log(_currIngredient);
            
            return true;
        }

        Debug.Log($"Failed To Add Ingredient, Check Returned {CorrectOperationCheck(_myType, ingredient)}, Current Ingredient Is {_currIngredient}");
        
        return false;
    }

    private IngredientType TakeIngredient()
    {
        IngredientType ret = _currIngredient;
        _currIngredient = NULL;
        return ret;
    }

    // Does different things on interact depending on the current state of the equipment
    public void Interact(GameObject player)
    {
        PlayerStatus statusScript = player.GetComponent<PlayerStatus>();
        IngredientType playerHeldIngredient = statusScript.GetHeldIngredient();
       
        if (_processing)
        {
            incrementProgress(statusScript.GetIncrementAmount());
            sliderScript.UpdateSlider(GetProgress() / GetMaxProgress());  // update slider UI
                
            if (_myType == CuttingBoard)
            {
                if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("cutOnce");
            }
            
            Debug.Log($"Incremented Progress Of Equipment {_myType}");
        }
    }

    public void Take(GameObject player)
    {
        PlayerStatus statusScript = player.GetComponent<PlayerStatus>();
        IngredientType playerHeldIngredient = statusScript.GetHeldIngredient();

        if (statusScript.GetHeldObject() == null && !_processing)
        {
            IngredientType removedIngredient = TakeIngredient(); 
            statusScript.SetHeldIngredient(removedIngredient);
            GameObject spawned = Instantiate(_ingredientToGameObject[removedIngredient], 
                statusScript.gameObject.transform, false);
            spawned.transform.localPosition += new Vector3(0, 1, 0);
            statusScript.SetHeldObject(spawned);

            bubbleCanvas.enabled = false;  // disable canvas
            
            if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("takingIngredients");

            Debug.Log($"Took Ingredient From Equipment {_myType}");
        }
        else
        {
            if (playerHeldIngredient != NULL)
            {
                bool added = AddIngredient(playerHeldIngredient);
            
                if (added)
                {
                    statusScript.RemoveHeld();
                
                    Debug.Log($"Added Ingredient To Equipment {_myType}");

                    if (_myType == FryingPan)
                    {
                        if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("frying");
                    }
                    
                    return;
                }
            }
        }
    }

    private void AnimateState()
    {
        if (_processing)
        {
            // Place In Processing Sprite
        }
        else
        {
            // Place In Empty Sprite
        }
    }

    public float GetProgress()
    {
        return _progress;
    }

    public float GetMaxProgress()
    {
        return _progressMax;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlayerStatus status = other.gameObject.transform.parent.gameObject.GetComponent<PlayerStatus>();
        IngredientType ingredient = status.GetHeldIngredient();
        
        
        _myMaterial.EnableKeyword("_EMISSION");
        if (CorrectOperationCheck(_myType, ingredient))
        {
            _myMaterial.SetColor("_EmissionColor", Color.gray);
            return;
        }
        _myMaterial.SetColor("_EmissionColor", Color.red);
    }

    private void OnTriggerExit(Collider other)
    {
        _myMaterial.SetColor("_EmissionColor", Color.black);
    }
}
