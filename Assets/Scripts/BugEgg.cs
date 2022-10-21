using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEgg : MonoBehaviour
{
    public GameObject bug;
    AudioSource born;
    // Start is called before the first frame update
    void Start()
    {
        born= GameObject.Find("SE_Born").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Organism>().aging)
        {
            born.Play();
            Instantiate(bug, this.transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
