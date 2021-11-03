using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviorScript : MonoBehaviour
{
    public Transform selectedTower;
    public static bool moveTower = false;

    private void Update() {
        if (moveTower && selectedTower != null && !InterfaceBehaviorScript.gm.isTaken) {
            selectedTower.position = transform.position;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Tower") && transform.position == other.transform.position) {
            InterfaceBehaviorScript.gm.isTaken = true;
//           if (moveTower && selectedTower != null) {
//               return;
//            }
            selectedTower = other.transform;
        }       
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Tower")) {
            InterfaceBehaviorScript.gm.isTaken = false;            
        }
        if (!moveTower) {
            selectedTower = null;
        }
    }

    public static void SetMoveTower(bool move) {
        moveTower = move;
    }
}
