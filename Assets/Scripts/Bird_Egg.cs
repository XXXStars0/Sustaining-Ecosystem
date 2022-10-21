using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Egg : MonoBehaviour
{
    public GameObject baby;
    AudioSource born;
    Animator a;
    bool tagg;
    void Start()
    {
        tagg = false;
        born = GameObject.Find("SE_Born").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Organism>().aging)
        {
            if (!tagg)
            {
                born.Play();
                tagg = true;
            }

            Instantiate(baby, this.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
