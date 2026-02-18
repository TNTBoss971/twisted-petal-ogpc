using UnityEngine;

public class LevelSummaryCreator : MonoBehaviour
{
    private string summary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string CreateSummary(DataManagement saveData, float playerHealth, GameObject itemLooted)
    {
        summary = "Day " + (saveData.levelsBeaten) + ": ";
        if (playerHealth >= 100)
        {
            summary += "I made it through without even a scratch!";
        }
        if ((playerHealth >= 80) && (playerHealth < 100))
        {
            summary += "I made it through without too much damage.";
        }
        if ((playerHealth >= 60) && (playerHealth < 80))
        {
            summary += "I made it through, but took a bit of damage";
        }
        if ((playerHealth >= 20) && (playerHealth < 50))
        {
            summary += "I made it through, but took some damage";
        }
        if ((playerHealth > 1) && (playerHealth < 20))
        {
            summary += "I made it through, but sustained significant damage";
        }
        if (playerHealth == 1)
        {
            summary += "I barely made it out alive!";
        }
        if (itemLooted != null)
        {
            summary += " On the way there, I found a " + itemLooted.GetComponent<GunController>().weaponName + ".";
        }
        return summary;
    }
}
