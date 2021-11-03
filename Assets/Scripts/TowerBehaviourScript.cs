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
    [SerializeField]
    private GameObject attackObject;
    [SerializeField]
    private bool attacksFlying = false;
    [SerializeField]
    private bool towerRotate = true;
    private float timer = 0f;
    private BoxCollider aimRange;

    // Start is called before the first frame update
    void Start()
    {
        aimRange = this.gameObject.GetComponent<BoxCollider>();
        attackObject.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(currentTarget != null) {
            if (towerRotate) {
                Vector3 targetDirection = currentTarget.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(targetDirection);
            }
            if (timer >= interval) {
                DealDamage(currentTarget);
                timer -= interval;
            }
        }
        if (timer > interval) timer = interval;
        if(currentTarget == null) attackObject.SetActive(false);                       
    }

    public void DealDamage(EnemyBehaviorScript enemy) {
        enemy.ReceiveDamage(this.damage);
    }

    public void OnCollisionStay(Collision other) {
        if (other.gameObject.tag.Equals("Enemy")) {
            health -= .5f;
            if (health <= 0) {
                Destroy(this.gameObject);
            }
        }
    }



    public void OnTriggerStay(Collider other) {
        if (other.tag.Equals("Enemy") || (other.tag.Equals("FlyingEnemy") && attacksFlying)) {
            //default to first target in range
            if (currentTarget == null) {
                currentTarget = other.gameObject.GetComponent<EnemyBehaviorScript>();
                return;
            }
            //chooses target fursthest ahead
            if (other.transform.position.x > currentTarget.transform.position.x) {
                currentTarget = other.gameObject.GetComponent<EnemyBehaviorScript>();
            }
            attackObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (currentTarget != null && currentTarget.gameObject == other.gameObject) currentTarget = null;
    }
}
