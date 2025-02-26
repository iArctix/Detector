using UnityEngine;

public class ToolSelectionUI : MonoBehaviour
{
    public UpgradeBench upgradeBench;
    public GameObject shovelwall;
    public GameObject shovelbench;
    public GameObject detectorwall;
    public GameObject detectorbench;

    public void SelectShovel()
    {
        shovelbench.SetActive(true);
        shovelwall.SetActive(false);
        upgradeBench.SelectTool(shovelbench);

    }

    public void SelectDetector()
    {
        detectorwall.SetActive(false);
        detectorbench.SetActive(true);
        upgradeBench.SelectTool(detectorbench);
    }
}