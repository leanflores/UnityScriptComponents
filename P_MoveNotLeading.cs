using UnityEngine;
using System.Collections;
using CnControls;

public class P_MoveNotLeading : MonoBehaviour
{

    public H_Health h_health;

    float moveX, moveZ;
    public float moveSpeed = 20f;
    Rigidbody p_Rigidbody;
    Animator p_anim;
    Vector3 movement;


    void Start()
    {
        p_Rigidbody = GetComponent<Rigidbody>();
        p_anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {

        Moving();

    }
    void Moving()
    {

        moveX = CnInputManager.GetAxis("Horizontal");
        moveZ = CnInputManager.GetAxis("Vertical");
        

        if (h_health.currentHealth > 0 && (moveX != 0 || moveZ != 0) && LevelState.levelManager.levelStart)
        {
            movement.Set(moveX, 0f, moveZ);
            movement = movement.normalized * moveSpeed * Time.deltaTime;
            p_Rigidbody.MovePosition(transform.position + movement);
            p_anim.SetBool("isRunning", true);
            
            Quaternion target_rotation = Quaternion.LookRotation(new Vector3(moveX, 0f, moveZ));
            p_Rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, target_rotation, 10f));
        }
        else 
        {
            p_anim.SetBool("isRunning", false);
        }


    }

}
