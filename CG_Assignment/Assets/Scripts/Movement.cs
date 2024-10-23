using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    void Update()
    {
        anim.SetBool("isWalking", Input.GetKey(KeyCode.W));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }

        anim.SetBool("Land", Input.GetKey(KeyCode.S));
    }
}
