using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;
using static EnumLibrary.EquipmentType;

public class Crate : MonoBehaviour, ITakeable
{
    [SerializeField] private IngredientType _holding;
    [SerializeField] private GameObject IngredientGameObject;
    
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
        PlayerStatus status = player.GetComponent<PlayerStatus>();
        Debug.Log("Interacted with crate");
        if (status.GetHeldIngredient() == NULL)
        {
            status.SetHeldIngredient(_holding);
            GameObject spawned = Instantiate(IngredientGameObject, status.gameObject.transform, false);
            spawned.transform.localPosition += new Vector3(0, 1, 0);
            status.SetHeldObject(spawned);
            Debug.Log($"Gave Player {_holding}");

            if (_holding == PLATE)
            {
                if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("takingPlate");
            }
            else
            {
                if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("takingIngredients");
            }
        }
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
