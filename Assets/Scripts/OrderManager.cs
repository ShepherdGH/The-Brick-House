using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using static EnumLibrary;
using System.Threading;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    public static OrderManager S { get; private set; }


    [Header("Order UI")]
    public GameObject singleOrderPanelPrefab;
    public Transform ordersPanel;
    public GameObject levelTimerPanel;
    private TextMeshProUGUI levelTimerText;

    [Header("Order Info")]
    public List<RequestBase> activeOrderList = new List<RequestBase>();  // stores orders that are active (non-expired) 

    [Header("Timing")]
    private float totalTime;
    private float timeLeft;
    private float orderSpawnInterval = 3f;
    private float startGenerateTime = 2f;
    private bool levelStarted = false;
    private bool levelEnded = false;

    [Header("End Receipt UI")]
    public Canvas endLevelCanvas;
    public TextMeshProUGUI subTotal;
    public TextMeshProUGUI rentDue;
    public TextMeshProUGUI grandTotal;
    public Button menuButton;
    public Button nextButton;


    private void Awake()
    {
        if (S != null && S != this)
        {
            Destroy(gameObject);
            return;
        }
        S = this;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // reads information from level manager
        orderSpawnInterval = LevelManager.S.levelOrderInterval;
        totalTime = LevelManager.S.levelTime;
        timeLeft = totalTime;
        // UI
        levelTimerText = levelTimerPanel.GetComponentInChildren<TextMeshProUGUI>();  // get text component
        levelTimerText.text = TimeToText(timeLeft);  // update text

        // disable end screen canvas
        endLevelCanvas.enabled = false;
        
    }


    public void StartLevel()
    {
        // starting in _ seconds, generate an order every _ seconds
        levelStarted = true;
        InvokeRepeating("GenerateOrder", startGenerateTime, orderSpawnInterval);
    }


    void GenerateOrder()
    {

        var level = LevelManager.S;

        // check if level manager is not set properly
        if (level == null || level.GetRequestList().Length == 0)
        {
            Debug.Log("No level manager or recipe list found.");
            return;
        }

        

        // check if active list is full
        if (activeOrderList.Count >= 6)
        {
            return;
        }
        // if active list not full, generate a new random order
        RequestBase request = level.GetRandomRequest();
        activeOrderList.Add(request);  // add to active list
        if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("orderAppearing");
        Debug.Log("New Order: " + request.name);

        // UI
        GameObject orderPanel = Instantiate(singleOrderPanelPrefab, ordersPanel);  // TODO: write a function that creates UI for the request!
        // setting timer
        OrderTimerScript timer = orderPanel.GetComponent<OrderTimerScript>();
        if (timer != null)
        {
            timer.SetDuration(request.orderDuration);
            timer.SetRequest(request);
            timer.SetImage(request.recipeImg);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!levelStarted || levelEnded)
        {
            return;
        }

        if (timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            levelTimerText.text = TimeToText(timeLeft);
        }
        else
        {
            // times up for level
            levelEnded = true;
            EndLevel();

            
        }
    }


    private void EndLevel()
    {
        // play end level receipt sound
        SoundManager.S.PlaySoundEffect("receipt");

        timeLeft = 0f;
        levelTimerText.text = TimeToText(timeLeft);
        CancelInvoke("GenerateOrder");  // stop generating order
        foreach (Transform child in ordersPanel)
        {
            Destroy(child.gameObject);  // remove all active orders UI
        }

        GameManager.UpdateTotalMoney();

        // update UI for END receipt
        endLevelCanvas.enabled = true;
        float subTotalNum = GameManager.GetCurrentLevelMoney();
        float rentDueNum = LevelManager.S.levelRent;
        float grandTotalNum = subTotalNum - rentDueNum;
        subTotal.text = "$" + subTotalNum.ToString();
        rentDue.text = "$" + rentDueNum.ToString();
        grandTotal.text = "$" + grandTotalNum.ToString();
        Debug.Log(subTotalNum.ToString() + " | " + rentDueNum.ToString() + " | " + grandTotalNum.ToString());

        // button text
        TextMeshProUGUI nextButtonText = nextButton.GetComponentInChildren<TextMeshProUGUI>();
        nextButton.onClick.RemoveAllListeners();  // clear all previous listeners

        // enable menu button
        menuButton.enabled = true;

        // check if could afford rent
        if (subTotalNum < 0)
        {
            // fail to pay rent, wire button to restart current level
            nextButtonText.text = "RESTART";
            nextButton.onClick.AddListener(() => {
                GameManager.ResetToLevelStart();  // reset game manager to "cancel" this level's money
                int sceneIndex = SceneManager.GetActiveScene().buildIndex;
                GameManager.ResetCurrentLevelMoney();
                SceneManager.LoadScene(sceneIndex);
            });
        }
        else
        {
            // could afford rent, go to next level
            GameManager.SubtractRent(rentDueNum);  // subtract rent from total money
            nextButtonText.text = "NEXT";
            nextButton.onClick.AddListener(() => {
                string nextScene = LevelManager.S.nextLevelName;
                if (nextScene == "END")
                {
                    // end of level 3
                    menuButton.enabled = false;
                    // nextButtonText.text = "MENU";
                    
                    // TODO: show Outtro scene
                    if (GameManager.GetTotalMoney() > 4000)
                    {
                        SceneManager.LoadScene("VictoryScene");
                    }
                    else
                    {
                        SceneManager.LoadScene("DefeatScene");
                    }
                    
                    
                }
                else
                {
                    GameManager.ResetCurrentLevelMoney();
                    SceneManager.LoadScene(nextScene);
                }
            });
        }
    }


    string TimeToText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        string output = string.Format("{0:00}:{1:00}", minutes, seconds);
        return output;
    }


    public void DeleteOrderFromActiveList(RequestBase target)
    {

        // delete request from active list
        if (activeOrderList.Contains(target))
        {
            activeOrderList.Remove(target);
            Debug.Log("Removed request: " + target.recipeName);
        }
        else
        {
            Debug.Log("Tried to remove request that does not exist in active list.");
            return;
        }

        // search for order UI and delete UI
        foreach (Transform child in ordersPanel)
        {
            OrderTimerScript timer = child.GetComponent<OrderTimerScript>();
            if (timer != null && timer.requestData == target)
            {
                Destroy(child.gameObject);
                break;
            }
        }
    }
}
