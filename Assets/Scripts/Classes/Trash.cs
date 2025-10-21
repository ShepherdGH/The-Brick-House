using UnityEngine;
using static EnumLibrary;
using static EnumLibrary.IngredientType;
using static EnumLibrary.EquipmentType;

public class Trash : MonoBehaviour, ITakeable
{
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

        if (playerHeldIngredient == NULL)
        {
            return;
        }
        
        statusScript.RemoveHeld();
        if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("throwingInTrash");
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
