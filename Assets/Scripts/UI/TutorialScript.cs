using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] Sprite[] tutorialSprites;
    [SerializeField] Image tutorialImage;
    [SerializeField] Button nextButton;
    private int imgIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // enable this canvas
        this.gameObject.SetActive(true);

        // if no sprites, skip to level
        if (tutorialSprites == null || tutorialSprites.Length == 0)
        {
            nextButton.onClick.AddListener(() => { OrderManager.S.StartLevel(); gameObject.SetActive(true); });
            return;
        }

        // show first img
        tutorialImage.sprite = tutorialSprites[0];

        // add listener to button & update button
        nextButton.onClick.AddListener(OnNextButtonClicked);
        UpdateButtonText();


        //if (tutorialSprites.Length > 0)
        //{
        //    imgIndex++;

        //    // if more img to load
        //    if (tutorialSprites.Length >= imgIndex)
        //    {
        //        nextButton.onClick.AddListener(() => ShowImgAtIndex(imgIndex));

        //    }
        //    else
        //    {
        //        // make the next button to go to level directly
        //        nextButton.onClick.AddListener(() => { OrderManager.S.StartLevel(); this.gameObject.SetActive(false); });
        //    }
        //}
    }

    public void OnNextButtonClicked()
    {
        // if more image to show, advance index
        if (imgIndex < tutorialSprites.Length - 1)
        {
            imgIndex++;
            tutorialImage.sprite = tutorialSprites[imgIndex];
            UpdateButtonText();
        }
        else
        {
            // if last image -> start level
            OrderManager.S.StartLevel();
            gameObject.SetActive(false);
        }
    }


    public void UpdateButtonText()
    {
        var buttonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
        if (tutorialSprites.Length == 1 || imgIndex == tutorialSprites.Length - 1)
        {
            buttonText.text = "START";
        }
        else
        {
            buttonText.text = "NEXT";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
