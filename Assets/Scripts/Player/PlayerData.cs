using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "Player/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public int currentDay = 1;
    public bool isNight = false;
    public string plotSelected = "";

    // You can add more player data here in the future (e.g., inventory, health, etc.)
}