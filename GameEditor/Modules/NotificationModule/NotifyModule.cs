using UnityEngine;

public class NotifyModule : CustomObjectSettings
{
    public override void SetSettings(string[] paramEters, int syncedTo)
    {
        GetComponent<CallNotify>().type = paramEters[0];
        GetComponent<CallNotify>().msg = paramEters[1];

        GetComponent<TriggerBase>().syncToId = syncedTo;

        SettingsSave(paramEters);
    }
}
