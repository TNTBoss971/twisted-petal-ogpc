using UnityEngine;

// ANY SCRIPT THAT HAS DATA THAT IS SAVED MUST INCLDE IDataPersistance AFTER MONOBEHAVIOR
// ANY SUCH SCRIPT ALSO NEEDS THE LoadData() AND SaveData() METHODS

public interface IDataPersistance
{
    void LoadData(GameData data);
    void SaveData(ref GameData data);
}
