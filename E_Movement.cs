using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class E_Movement : MonoBehaviour {

    GameObject hero;
    Transform h_Transform;
    H_Health h_health;
    NavMeshAgent e_nav;
    bool startChasing = false;
    Animator e_anim;
    GameObject e_image;

    void Start()
    {
        e_nav = GetComponent<NavMeshAgent>();
        
        hero = GameObject.FindGameObjectWithTag("Hero");
        h_Transform = hero.transform;
        h_health = hero.GetComponent<H_Health>();
        e_anim = GetComponent<Animator>();
        e_image = GetComponentInChildren<Image>().gameObject;
        e_image.SetActive(false);
    }
     
    void FixedUpdate()
    {
        if (!LevelState.levelManager.levelStart)
        {
            e_nav.SetDestination(transform.position);
            e_anim.SetBool("isRunning", false);
            return;
        }

        if (startChasing && h_health.currentHealth > 0)
        {
            e_nav.SetDestination(h_Transform.position);
            e_anim.SetBool("isRunning", true);
        }
        else
        {
            e_anim.SetBool("isRunning", false);
            e_nav.SetDestination(transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == hero)
        {
            startChasing = true;
            e_image.SetActive(true);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (startChasing && other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Entered");
            StartCoroutine(SlowDown());

        }
    }
    IEnumerator SlowDown()
    {
        e_nav.speed = 10f;
        e_anim.speed = 0.5f;
        yield return new WaitForSeconds(3f);
        e_nav.speed = 18f;
        e_anim.speed = 1f;
    }
}
