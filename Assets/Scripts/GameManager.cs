using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Singleton instance

    public int currentDay = 1;
    public bool isNight = false;  // Tracks if it's nighttime
    public string plotselected = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keeps GameManager active across scenes
        }
        else
        {
            Destroy(gameObject);  // Prevent duplicates
        }
    }

    // Called when player selects a dig site from PC
    public void TravelToDigSite()
    {
        isNight = false;  // Ensure it's day at dig site

        //add the locations
        if (!isNight && plotselected == "field")
        {
            {
                SceneManager.LoadScene("Field");
            }
        }
        else if(!isNight && plotselected == "")
        {
            Debug.Log("No location chosen");
        }
        else
        {
            Debug.Log("Its night time lmao");
        }
        
    }

    // Called when player returns to home after digging
    public void ReturnHome()
    {
        isNight = true;  // Set time to night
        SceneManager.LoadScene("Home");
    }

    // Called when player sleeps in bed
    public void SleepAndStartNewDay()
    {
        if(!isNight)
        {
            Debug.Log("its day lmao");
        }
        else
        {
            currentDay++;  // Increase day count
            isNight = false;  // Reset to morning
            SceneManager.LoadScene("Home");
            plotselected = "";
        }
        
    }
}
