using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawnedEventArgs : EventArgs
{
    public string Life { get; set; }
}

public class BaseTurretSpawner
{
    public EventHandler<TurretSpawnedEventArgs> TurretSpawned;

    public void Spawn(BaseTurret turret)
    {
        OnTurretSpawned(turret);
    }

    protected virtual void OnTurretSpawned(BaseTurret turret)
    {
        if (TurretSpawned != null)
        {
            TurretSpawned(this, new TurretSpawnedEventArgs() { Life = (turret.Life).ToString() });
        }  
    }
}
