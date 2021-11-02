using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBehaviorScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.tag.Equals("Tower") && transform.position == other.transform.position) {
            InterfaceBehaviorScript.gm.isTaken = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("Tower")) {
            InterfaceBehaviorScript.gm.isTaken = false;
        }
    }
}
