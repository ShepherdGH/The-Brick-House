using UnityEngine;
using UnityEngine.UI;
using static EnumLibrary;

public class SingleBubble : MonoBehaviour
{
    private IngredientType myType;
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
        if (img == null) img = GetComponentInChildren<Image>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBubbleSprite(Sprite sprite)
    {
        if (img == null)
            img = GetComponentInChildren<Image>();

        if (img != null)
            img.sprite = sprite;
        else
            Debug.LogError("SingleBubble: Image component not found!");
    }

    public void SetType(IngredientType type)
    {
        myType = type;
    }
}
