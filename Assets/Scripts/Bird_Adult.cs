using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Adult : MonoBehaviour
{
    public bool isSleep = false;
    public float speedRange;
    public bool isHerb = false;
    float X_speed;
    float Y_speed;
    Animator a;
    AudioSource eat;
    AudioSource die;
    TimeSys timer;
    int changeCount;
    public GameObject egg;
    bool layegg = false;
    // Start is called before the first frame update
    void Start()
    {
        changeCount = 0;
        X_speed = Random.Range(-speedRange, speedRange);
        Y_speed = Random.Range(-speedRange, speedRange);
        isSleep = false;
        eat = GameObject.Find("SE_Eat").GetComponent<AudioSource>();
        die = GameObject.Find("SE_Die").GetComponent<AudioSource>();
        timer = GameObject.Find("EventSystem").GetComponent<TimeSys>();
        a = this.GetComponent<Animator>();
        layegg = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSleep)
        {
            changeCount++;
            if (changeCount > 30)
            {
                    X_speed = Random.Range(-speedRange, speedRange);
                    Y_speed = Random.Range(-speedRange, speedRange);
                changeCount = 0;
            }
            if (this.transform.position.x > 14 || this.transform.position.x < -14)
            {
                X_speed = -X_speed;
            }

            if (this.transform.position.y > 10 || this.transform.position.y < -10)
            {
                Y_speed = -Y_speed;
            }
            this.transform.position += new Vector3(X_speed, Y_speed);
        }

        if (isHerb)
        {
            if (timer.type.Equals("night"))
            {
                isSleep = true;
            }
            else
            {
                isSleep = false;
            }
        }
        else
        {
            if (timer.type.Equals("day"))
            {
                isSleep = true;
            }
            else
            {
                isSleep = false;
            }
        }

        a.SetBool("isSleep", isSleep);
        //evolution
        if (this.GetComponent<Organism>().aging)
        {
            die.Play();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (!isSleep)
        {
            if (isHerb)
            {
                if (c.gameObject.CompareTag("BugEgg") || c.gameObject.CompareTag("Bug"))
                {
                    X_speed = -X_speed;
                    Y_speed = -Y_speed;
                }
                    if (c.gameObject.CompareTag("Fruit"))
                {
                    eat.Play();
                    int n = c.gameObject.GetComponent<Organism>().nutrition;
                    int max = c.gameObject.GetComponent<Organism>().maxNutrition;
                    if (this.GetComponent<Organism>().currSize < 100)
                    {
                        this.GetComponent<Organism>().currSize += n;
                    }
                    if (this.GetComponent<Organism>().health + n <= this.GetComponent<Organism>().maxHealth)
                    {
                        this.GetComponent<Organism>().health += n;
                    }
                    else
                    {
                        this.GetComponent<Organism>().health = this.GetComponent<Organism>().maxHealth;

                    }
                    Destroy(c.gameObject);

                }
            }
            else
            {
                if (c.gameObject.CompareTag("Fruit"))
                {
                    X_speed = -X_speed;
                    Y_speed = -Y_speed;
                }
                    //Eats Bug Eggs
                    if (c.gameObject.CompareTag("BugEgg")|| c.gameObject.CompareTag("Bug"))
                {
                    eat.Play();
                    int n = c.gameObject.GetComponent<Organism>().nutrition;
                    int max = c.gameObject.GetComponent<Organism>().maxNutrition;
                    if (this.GetComponent<Organism>().currSize < 100)
                    {
                        this.GetComponent<Organism>().currSize += n;
                    }
                    if (this.GetComponent<Organism>().health + n <= this.GetComponent<Organism>().maxHealth)
                    {
                        this.GetComponent<Organism>().health += n;
                    }
                    else
                    {
                        this.GetComponent<Organism>().health = this.GetComponent<Organism>().maxHealth;

                    }
                    Destroy(c.gameObject);

                }
            }
          
            //Bounce Back
            if (c.gameObject.CompareTag("Bird"))
            {
                X_speed = -X_speed;
                Y_speed = -Y_speed;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject[] b = GameObject.FindGameObjectsWithTag("Bird");
        if (b.Length<6 && layegg)
        {
            layegg = false;
            Instantiate(egg, this.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        layegg = true;
    }
}
