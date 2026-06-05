using UnityEngine;

public class SpawnCustom : CustomObjectSettings
{
    public override void SetSettings(string[] paramEters, int syncedTo)
    {
        GetComponent<SpawnManager>().spawnName = paramEters[0]; //value 0 is spawn

        SettingsSave(paramEters);
    }
}
