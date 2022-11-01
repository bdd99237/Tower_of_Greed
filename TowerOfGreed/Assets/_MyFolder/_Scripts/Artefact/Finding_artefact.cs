using UnityEngine;
using System.Collections;

//아티펙트 습득처리 스크립트
public class Finding_artefact : MonoBehaviour {

    bool testCheck;
    int num;

    GameObject artefactParent;

    void Start()
    {
        artefactParent = GameObject.Find("ArtefactGrid");
        testCheck = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "artefact")
        {
            for (int i = 0; i < GameManager.Instance.artefact.Count; i++)
            {
                if (other.GetComponent<DropArtefact>().artefact_index == GameManager.Instance.artefact[i].artefact_index)
                {
                    testCheck = true;
                    num = i;
                }
            }

            if (testCheck)
            {
                GameManager.Instance.artefact[num].count += 1;
                testCheck = false;
            }
            else
            {
                GameObject artefact = Instantiate(Resources.Load<GameObject>("Artefact"), Vector3.zero, Quaternion.identity) as GameObject;
                artefact.transform.parent = artefactParent.transform;
                artefact.transform.localRotation = Quaternion.Euler(0, 0, 0);
                artefact.transform.localScale = new Vector3(1, 1, 1);
                artefact.transform.localPosition = Vector3.zero;

                PlayerManager.Instance.AddArtefectScript(artefact, other.GetComponent<DropArtefact>().artefact_index);

                GameManager.Instance.ArtefactListAdd(artefact);
                
            }
            Destroy(other.gameObject);
        }
    }
}
