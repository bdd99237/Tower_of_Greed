using UnityEngine;
using System.Collections;

public class M_HPBar : MonoBehaviour {

    public GameObject _MHpBar;
    public GameObject _Monster;
    public GameObject _MainCamera;
    public GameObject _Target;

    void Update()
    {
        _Target.transform.rotation = _MainCamera.transform.rotation;

        _MHpBar.transform.localScale = new Vector3(_Monster.GetComponent<M_HP>()._MHpPresent / _Monster.GetComponent<M_HP>()._MHp, 1, 1);

        _Target.transform.position = new Vector3(_Monster.transform.position.x, _Monster.transform.position.y + 3, _Monster.transform.position.z);
    }
}
