using UnityEngine;
using UnityEngine.UI;

public class OrderTimerScript : MonoBehaviour
{
    [Header("Order Detail")]
    [SerializeField] private float duration = 60f;
    public RequestBase requestData;  // reference to the request this timer is relating
    private float timeLeft;

    [Header("UI Component")]
    [SerializeField] private Image fill;
    public Image BackgroundImg;
    private Slider slider;
    private Color green;
    private Color yellow;
    private Color red;

    


    public void SetDuration(float d) { duration = d; }
    public void SetRequest(RequestBase request) { requestData = request; }
    
    public void SetImage(Sprite bgImg) { BackgroundImg.sprite = bgImg; }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // color values
        green = new Color32(117, 193, 30, 255);
        yellow = new Color32(234, 137, 0, 255);
        red = new Color32(211, 74, 0, 255);

        // get slider component
        slider = GetComponentInChildren<Slider>();

        fill.color = green;  // initialize color
        timeLeft = duration;  // initialize timer
        slider.value = timeLeft / duration;  // initialize slider value
    }

    // Update is called once per frame
    void Update()
    {
        // change slider value
        // TODO: change profit player gets
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            slider.value = timeLeft / duration;

            // change slider fill color
            float sliderVal = slider.value;
            if (sliderVal <= 0.5f && sliderVal > 0.25)
            {
                fill.color = yellow;
            }
            else if (sliderVal <= 0.25)
            {
                fill.color = red;
            }
        }

        else
        {
            // run out of time
            // TODO: money

            Debug.Log("Time's up for this order!");
            if (SoundManager.S != null) SoundManager.S.PlaySoundEffect("orderDisappearing");

            if (OrderManager.S != null && requestData != null)
            {
                OrderManager.S.activeOrderList.Remove(requestData);
            }

            Destroy(this.gameObject);
        }

        
    }
}
