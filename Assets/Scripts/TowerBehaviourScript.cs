using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private EnemyBehaviorScript currentTarget = null;
    [SerializeField]
    private float damage = 5f;
    [SerializeField]
    private float interval = .05f;
    [SerializeField]
    private float health = 100;
    private float timer = 0f;
    private BoxCollider aimRange;

    // Start is called before the first frame update
    void Start()
    {
        aimRange = this.gameObject.GetComponent<BoxCollider>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(currentTarget != null && timer >= interval) {
            DealDamage(currentTarget);
            timer -= interval;
        }
        if (timer > interval) timer = interval;
    }

    public void DealDamage(EnemyBehaviorScript enemy) {
        enemy.ReceiveDamage(this.damage);
    }


    public void OnTriggerStay(Collider other) {
        if (other.tag.Equals("Enemy")) {
            //default to first target in range
            if (currentTarget == null) {
                currentTarget = other.gameObject.GetComponent<EnemyBehaviorScript>();
                return;
            }
            //chooses target fursthest ahead
            if (other.transform.position.x > currentTarget.transform.position.x) {
                currentTarget = other.gameObject.GetComponent<EnemyBehaviorScript>();
            }
        }
    }
}
