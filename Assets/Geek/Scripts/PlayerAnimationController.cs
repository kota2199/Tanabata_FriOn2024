using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator playerAnimator;

    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetFloat("WalkHorizontal", Input.GetAxis("Horizontal"));
        playerAnimator.SetFloat("WalkVertical", Input.GetAxis("Vertical"));
    }
}
