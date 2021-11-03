using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducedTowerScript : MonoBehaviour
{
    [SerializeField]
    private float health = 60;

    public void OnCollisionStay(Collision other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            health -= 1f;
            if (health <= 0) {
                Destroy(this.gameObject);
            }
        }
    }
}
