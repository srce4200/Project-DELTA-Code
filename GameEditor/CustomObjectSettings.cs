using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CustomObjectSettings : MonoBehaviour
{
    public string[] objectParameters;
    public string[] paramsNames;

    [SerializeField] GameObject syncToLinePrefab;

    public virtual void SetSettings(string[] paramEters, int syncTo)
    {
        objectParameters[0] = paramEters[0];
        objectParameters[1] = paramEters[1];
        objectParameters[2] = paramEters[2];
    }
    public virtual void SettingsSave(string[] entry)
    {
        for (int i = 0; i < objectParameters.Length; i++)
        {
            objectParameters[i] = entry[i];
        }
    }
    public virtual string[] RetrieveSettings()
    {
        return objectParameters;
    }
    public virtual string[] RetrieveSettingsNames()
    {
        return paramsNames;
    }
    public virtual void DisplaySyncTo(Transform pos1, Transform pos2)
    {
        Instantiate(syncToLinePrefab).GetComponent<SyncLineScript>().CheckLine(pos1, pos2);
    }
}
