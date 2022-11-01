using UnityEngine;
using System.Collections;

public class P_Move : MonoBehaviour {

    public bool setmove;

    void Start()
    {
        setmove = false;
    }

    public void Update()
    {
        if (setmove)
        {
            transform.localPosition += transform.forward * Time.deltaTime * GameManager.Instance.result_Speed;
        }
    }

    public void Stop()
    {
        setmove = false;
    }

    public void Go()
    {
        setmove = true;
    }
}
