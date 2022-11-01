using UnityEngine;
using System.Collections;

public class Warp : MonoBehaviour {
    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.playerStage += 1;

            //씬을 다시 로드하는 식.
            //Application.LoadLevel("stage1");

            //씬을 그대로 사용하는 식.
            WarpManager.Instance.StageWarp(other);
            Destroy(gameObject);
        }
    }
}