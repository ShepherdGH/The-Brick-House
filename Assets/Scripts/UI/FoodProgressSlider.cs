using System;
using UnityEngine;
using UnityEngine.UI;

public class FoodProgressSlider : MonoBehaviour
{
    public Slider slider;
    public EquipmentBase equipment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();

        if (slider == null)
        {
            Debug.Log("SHIT");
        }
        slider.value = 0;
        equipment = GetComponentInParent<EquipmentBase>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float sliderValue = equipment.GetProgress() / equipment.GetMaxProgress();
        //UpdateSlider(sliderValue);
    }

    public void UpdateSlider(float sliderVal)
    {
        slider.value = sliderVal;
    }

    public void StartNewSlider()
    {
        if (slider == null)
        {
            Debug.Log("Fuck");
        }
        slider.value = 0;
    }
}
