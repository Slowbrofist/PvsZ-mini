using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceBehaviorScript : MonoBehaviour
{
    public static int selected;
    public GameObject cursor;
    public bool isTaken;
    public bool isSelected;
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

    public void Update() {
       if(!isTaken && isSelected) SpawnTower(cursor.transform.position);
    }

    public void MoveCursor (Vector3 movePoint){
        cursor.transform.position = movePoint;
        isSelected = movePoint.x < 1;
    }

    public void SpawnTower( Vector3 spawnPoint) {
        if (selected != 99) {
            Instantiate(pfControl.towerPfArray[selected], spawnPoint, Quaternion.identity);
        }
    }
}
