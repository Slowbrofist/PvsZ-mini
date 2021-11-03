using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceBehaviorScript : MonoBehaviour
{
    public static int selected;
    public GameObject cursor;
    public bool isTaken = false;
    public bool isSelected = false;
    public static InterfaceBehaviorScript gm;
    public PrefabControlScript pfControl;

    private void Awake() {
        selected = 3;
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
        CursorBehaviorScript.SetMoveTower(selected == 3);
    }

    public void Update() {
       if(!isTaken && isSelected) SpawnTower(cursor.transform.position);
    }

    public void MoveCursor (Vector3 movePoint){
        cursor.transform.position = movePoint;
        isSelected = movePoint.x > 0;
    }

    public void SpawnTower( Vector3 spawnPoint) {
        if (selected < pfControl.towerPfArray.Length) {
            Instantiate(pfControl.towerPfArray[selected], spawnPoint, Quaternion.identity);
        }
    }
}
