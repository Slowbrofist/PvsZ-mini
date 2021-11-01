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
    private GameObject particleGenerator;
    private float timer = 0f;
    private BoxCollider aimRange;

    // Start is called before the first frame update
    void Start()
    {
        aimRange = this.gameObject.GetComponent<BoxCollider>();
        particleGenerator.SetActive(false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(currentTarget != null) {          
            Vector3 targetDirection = currentTarget.transform.position- transform.position;
            transform.rotation = Quaternion.LookRotation(targetDirection);
            if (timer >= interval) {
                DealDamage(currentTarget);
                timer -= interval;
            }
        }
        if (timer > interval) timer = interval;
        if(currentTarget == null) particleGenerator.SetActive(false);                       
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
            particleGenerator.SetActive(true);
        }
    }

    public float GetAngleFromVector(Vector3 vector) {
        vector = vector.normalized;
        float n = Mathf.Atan2(vector.z, vector.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }
}
