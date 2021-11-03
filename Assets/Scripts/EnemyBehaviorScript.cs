using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{

   [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    private bool isSlowed = false;
    [SerializeField]
    private float Health = 100f;
    [SerializeField]
    private Rigidbody rigid;
    public bool isExplosive = false;
    private Vector3 speedVector;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        speedVector = new Vector3(Speed, 0, 0);
        offset = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = speedVector * Time.deltaTime * offset;
        if (transform.position.x > 12) {
            Debug.Log("Inimigo passou!");
            Destroy(this.gameObject);
        }
    }

    public void ReceiveDamage(float dmg) {
        Health -= dmg;
        if (Health <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void ApplySlow() {
            isSlowed = true;
            offset = .5f;
    }

    public void RemoveSlow(bool slowCheck) {
        if (slowCheck) {
            isSlowed = false;
            offset = 1;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Tower") && isExplosive){
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.Equals("FanHitbox") && gameObject.tag.Equals("FlyingEnemy") ) {
            ApplySlow();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag.Equals("FanHitbox") && gameObject.tag.Equals("FlyingEnemy")) {
            RemoveSlow(isSlowed);
        }
    }
}
