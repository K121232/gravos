using UnityEngine;
using System.Collections.Generic;

public class SeqKeyLauncher : MissileLauncher {
    public List<PoolSpooler>    varieties;
    public  int                 selection;
    public  KeyCode             spawnKey;

    public override void Update() {
        if ( Input.GetKeyDown ( spawnKey ) ) {
            autoLoader = varieties[selection];
            spawnChance = 1.1f;
            deltaTime = 0;
        } else {
            spawnChance = -1;
        }
        if ( Input.GetKeyDown ( KeyCode.Comma ) ) {
            selection = ( selection + 1 ) % varieties.Count;
        }
        if ( Input.GetKeyDown( KeyCode.Period ) ) {
            selection = ( selection + varieties.Count - 1 ) % varieties.Count;
        }
        base.Update();
    }
}
