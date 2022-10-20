using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Organism : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float currAge;
    public float maxAge;
    public int startSize;
    public int currSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currAge += Time.deltaTime;
        this.gameObject.transform.localScale=new Vector3(currSize / 100f, currSize / 100f, 1);
    }
}
