using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class H_MoveNotLeading : MonoBehaviour
{

    NavMeshAgent h_nav;
    public Transform p_transform;
    public Button boostButton;
    public GameObject boostIndicator;

    bool goto_p = true;
    H_Health h_health;
    Animator h_anim;
    
    void Start()
    {
        h_health = GetComponent<H_Health>();
        h_nav = GetComponent<NavMeshAgent>();
        h_anim = GetComponent<Animator>();
        boostButton.onClick.AddListener(Boost);
        boostIndicator.SetActive(false);
        boostButton.interactable = true;
    }

    void FixedUpdate()
    {

        if (goto_p && !h_health.isDead && LevelState.levelManager.levelStart)
        {
            h_nav.SetDestination(p_transform.position);
            h_anim.SetBool("isRunning", true);
        }
        else
        {
            h_anim.SetBool("isRunning", false);
            h_nav.SetDestination(transform.position);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Enter");

        if (other.gameObject.CompareTag("Player"))
        {
            goto_p = false;
            StopBoost();
        }

    }
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            goto_p = true;
        }

        //Debug.Log("Exit" + other.gameObject.tag + goto_p);
    }
    void OnColliderEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Goal"))
        {
            // Level end here.. use :manager:
            LevelState.levelManager.StopLevel(true);

            //Debug.Log("Level Complete!");
        }
    }

    public void Boost()
    {
        boostIndicator.SetActive(true);
        h_nav.speed = 25f;
        h_nav.acceleration = 25f;
        boostButton.interactable = false;

    }
    void StopBoost()
    {
        boostIndicator.SetActive(false);
        h_nav.speed = 15f;
        h_nav.acceleration = 15f;
    }
}
