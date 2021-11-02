using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceBehaviorScript : MonoBehaviour
{
    public static int selected;
    public static InterfaceBehaviorScript gm;
    public PrefabControlScript pfControl;

    private void Awake() {
        selected = 99;
        if (gm == null) {
            gm = this;
            DontDestroyOnLoad(this);
        }
        else if (gm != this) {
            Destroy(gameObject);
        }
    }

    public void SelectIndex(int index) {
        selected = index;
    }

    public void SpawnTower( Vector3 spawnPoint) {
        if (selected != 99) {
            Instantiate(pfControl.towerPfArray[selected], spawnPoint, Quaternion.identity);
            selected = 99;
        }
    }
}
