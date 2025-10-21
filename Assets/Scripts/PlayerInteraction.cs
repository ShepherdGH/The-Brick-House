using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Collider currentHoverObject;
    
    private IEquipment CurrentEquipmentHover;
    private ITakeable _currentTakeableHover;

    private PlayerStatus _status;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _status = GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other)
    {
        GameObject collided = other.gameObject;
        
        Debug.Log("Player Trigger Entered");
        
        Debug.Log(collided.layer.ToString());
        
        if (collided.layer == 11 || collided.layer == 12)
        {
            currentHoverObject = other;
            
            IEquipment equipScript = collided.GetComponentInParent<IEquipment>();
            ITakeable takeScript = collided.GetComponentInParent<ITakeable>();

            if (equipScript != null)
            {
                CurrentEquipmentHover = equipScript;
            }

            if (takeScript != null)
            {
                _currentTakeableHover = takeScript;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject collided = other.gameObject;
        
        if (collided.layer == 11 || collided.layer == 12)
        {
            if (!currentHoverObject.Equals(other))
            {
                return;
            }
            
            _currentTakeableHover = null;
            CurrentEquipmentHover = null;
        }
    }

    public void Use()
    {
        if (CurrentEquipmentHover == null) return;
        
        CurrentEquipmentHover.Interact(this.gameObject);
    }

    public void Take()
    {
        if (_currentTakeableHover == null) return;

        _currentTakeableHover.Take(this.gameObject);
        
        Debug.Log($"Picked Up {_status.GetHeldIngredient()}");
    }
}
