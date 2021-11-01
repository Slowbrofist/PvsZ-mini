using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviorScript : MonoBehaviour
{

   [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    private bool isFlying = true;
    [SerializeField]
    private bool isSlowed = false;
    [SerializeField]
    private float Health = 100f;
    [SerializeField]
    private Rigidbody rigid;
    [SerializeField]
    private float Lifespan = 0f;
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
        Lifespan += Time.deltaTime;
        rigid.velocity = speedVector * Time.deltaTime * offset;
    }

    public void ReceiveDamage(float dmg) {
        Health -= dmg;
        if (Health <= 0) {
            Destroy(this.gameObject);
        }
    }

    public void ApplySlow() {
        if (isFlying) {
            isSlowed = true;
            offset = .5f;
        }
    }

    public void RemoveSlow(bool slowCheck) {
        if (slowCheck) {
            isSlowed = false;
            offset = 1;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag.Equals("Tower") && isExplosive && Lifespan >= 10f){
            Debug.Log(collision.gameObject.tag);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
