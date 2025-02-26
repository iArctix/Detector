using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    public void UpgradeDamage()
    {
        Debug.Log("Damage Upgraded!");
        // Apply damage upgrade to selected tool
    }

    public void UpgradeDurability()
    {
        Debug.Log("Durability Upgraded!");
        // Apply durability upgrade to selected tool
    }

    public void ExitUpgrade()
    {
        FindObjectOfType<UpgradeBench>().ExitUpgradeMode();
    }
}