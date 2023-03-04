using UnityEngine;
using System.Collections.Generic;

// Infected class scripts

public class SeqKeyLauncher : MissileLauncher {
    public List<PoolSpooler>    varieties;
    public  int                 selection;

    public override void Update() {
        if ( Input.GetKeyDown ( KeyCode.I ) ) {
            autoLoader = varieties[selection];
            spawnChance = 1.1f;
            deltaTime = 0;
        } else {
            spawnChance = -1;
        }
        if ( Input.GetKeyDown ( KeyCode.L ) ) {
            selection = ( selection + 1 ) % varieties.Count;
        }
        if ( Input.GetKeyDown( KeyCode.J ) ) {
            selection = ( selection + varieties.Count - 1 ) % varieties.Count;
        }
        base.Update();
    }
}
