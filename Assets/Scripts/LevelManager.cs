using NUnit.Framework;
using UnityEngine;
using static EnumLibrary;
using static RequestBase;

public class LevelManager : MonoBehaviour
{
    public static LevelManager S { get; private set; }

    // available requests for a level
    [SerializeField] private RequestBase[] levelRequestTypeList;

    // timing info for Order Manager to read
    public float levelOrderInterval = 10f;
    public float levelTime = 60f;
    public float levelRent = 0f;
    
    public string nextLevelName;

    private void Awake()
    {
        if (S != null && S != this)
        {
            Destroy(gameObject);
            return;
        }
        S = this;
    }


    // returns the available requests for the level
    public RequestBase[] GetRequestList() { return levelRequestTypeList; }


    // returns a random request from the available request list
    public RequestBase GetRandomRequest()
    {
        if (levelRequestTypeList == null || levelRequestTypeList.Length == 0)
        {
            Debug.Log("Request Type list for this level is not set.");
            return null;
        }
        return levelRequestTypeList[Random.Range(0, levelRequestTypeList.Length)];
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
