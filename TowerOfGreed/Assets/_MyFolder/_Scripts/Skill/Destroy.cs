using UnityEngine;
using System.Collections;

public class Destroy : MonoBehaviour {

    public GameObject target;
    [SerializeField]
    float saveTime;
    public float destroyTime;

    void Start() {
        saveTime = 0;
    }

    void Update()
    {
        saveTime += Time.deltaTime;

        if (target != null)
        {
            gameObject.transform.position = target.transform.position;
        }

        if(saveTime>destroyTime)
        {
            Debug.Log("정상삭제");
            Destroy(gameObject);
        }
    }
}
