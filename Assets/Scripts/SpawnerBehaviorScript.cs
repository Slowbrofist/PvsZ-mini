using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviorScript : MonoBehaviour
{
    public PrefabControlScript prefablist;
    public float countdown;
    private int enemyI;
    private int randZ;
    public float delay = 3;
    public float offset;

    // Start is called before the first frame update
    void Start() {
        offset = Random.Range(0f, 3f);
        countdown = offset + delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0) {
            offset = Random.Range(0f, 3f);
            randZ = Random.Range(1, 7);
            enemyI = Random.Range(0, prefablist.enemyPfArray.Length);
            Vector3 tempVector = new Vector3(transform.position.x, transform.position.y, randZ);
            Instantiate(prefablist.enemyPfArray[enemyI], tempVector, Quaternion.identity);
            countdown += delay + offset;
        }
    }
}
