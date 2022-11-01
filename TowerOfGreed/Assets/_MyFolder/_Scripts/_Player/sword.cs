using UnityEngine;
using System.Collections;
using Xft; //xft클래스의 XWeaponTrail을 사용하기위해서 적었음. 네임스페이스생략을 위함.

public class sword : MonoBehaviour {

    public float damge;

    public bool isAttack;

    bool test;

    float current_Rot;

    float end_Rot;

    BoxCollider checkBox;

    [SerializeField]
    XWeaponTrail trail;

    [SerializeField]
    Quaternion end_Quat;

    [SerializeField]
    float timeSave;

	// Use this for initialization
	void Start ()
    {
        test = false;
        current_Rot = -60;
        checkBox = GetComponent<BoxCollider>();
        //trail = GetComponent<XWeaponTrail>();
        checkBox.enabled = false;
        trail.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
        if(isAttack)
        {
            timeSave += Time.deltaTime;
            if (test == false)
            {
                transform.localRotation = Quaternion.Euler(transform.rotation.x, 0 - current_Rot, transform.rotation.z);
                end_Rot = -(current_Rot);
                end_Quat = Quaternion.Euler(transform.rotation.x, 0 - end_Rot, transform.rotation.z);
                test = true;
            }
            checkBox.enabled = true;
            trail.enabled = true;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, end_Quat, Time.deltaTime);
            if(timeSave >= 0.5f)
            {
                isAttack = false;
                timeSave = 0;
            }
        }
        else
        {
            test = false;
            checkBox.enabled = false;
            trail.enabled = false;
            current_Rot = -(current_Rot);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<P_Health>()._TakeDamage(damge);
    }

}
 ///////////////모델이 칼과 일체형인듯?.... 코드 갈아엎어야함.//////////////////////////////////////////