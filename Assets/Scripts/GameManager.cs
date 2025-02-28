using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;  // Singleton instance

    public PlayerData playerData;  // Reference to the PlayerData ScriptableObject

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

        if (playerData == null)
        {
            Debug.LogError("PlayerData is not assigned in the GameManager!");
        }
    }

    // Called when player selects a dig site from PC
    public void TravelToDigSite()
    {
        playerData.isNight = false;  // Ensure it's day at dig site

        if (!playerData.isNight && playerData.plotSelected == "field")
        {
            SceneManager.LoadScene("Field");
        }
        else if (!playerData.isNight && playerData.plotSelected == "")
        {
            Debug.Log("No location chosen");
        }
        else if(playerData.isNight)
        {
            Debug.Log("It's night time!");
        }
        else
        {
            Debug.Log("I forgot a condition");
        }
    }

    // Called when player returns to home after digging
    public void ReturnHome()
    {
        playerData.isNight = true;  // Set time to night
        SceneManager.LoadScene("Home");
    }

    // Called when player sleeps in bed
    public void SleepAndStartNewDay()
    {
        if (!playerData.isNight)
        {
            Debug.Log("It's day time!");
        }
        else
        {
            playerData.currentDay++;  // Increase day count
            playerData.isNight = false;  // Reset to morning
            SceneManager.LoadScene("Home");
            playerData.plotSelected = "";  // Reset the plot selection
        }
    }
}