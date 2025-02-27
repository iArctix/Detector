using UnityEngine;

public class ToolSelectionUI : MonoBehaviour
{
    public UpgradeBench upgradeBench;
    public GameObject shovelwall;
    public GameObject shovelbench;
    public GameObject detectorwall;
    public GameObject detectorbench;

    public GameObject shovelupgrades;
    public GameObject detectorupgrades;

    public void SelectShovel()
    {
        shovelbench.SetActive(true);
        shovelwall.SetActive(false);
        upgradeBench.SelectTool(shovelbench);
        //ui
        shovelupgrades.SetActive(true);
        detectorupgrades.SetActive(false);

    }

    public void SelectDetector()
    {
        detectorwall.SetActive(false);
        detectorbench.SetActive(true);
        upgradeBench.SelectTool(detectorbench);
        //ui;
        shovelupgrades.SetActive(false);
        detectorupgrades.SetActive(true);
    }

    public void deselectboth()
    {
        detectorwall.SetActive(true);
        detectorbench.SetActive(false);
        shovelbench.SetActive(false);
        shovelwall.SetActive(true);
    }
}