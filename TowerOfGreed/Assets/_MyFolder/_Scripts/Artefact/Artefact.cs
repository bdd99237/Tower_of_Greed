using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Artefact : MonoBehaviour {

    public int artefact_index;
    public string type;
    public string a_Name;
    public string caption;
    public Sprite image;
    public int count;
    Text viewCount;

    public virtual void Start()
    {
        viewCount = GetComponentInChildren<Text>();
    }

    public virtual void Update()
    {
        viewCount.text = count.ToString();
    }

    public virtual void Action()
    {

    }

    public virtual void InvokeCheck()
    {

    }
}
