using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollTable : MonoBehaviour
{
    int isSpinning = 1;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Spin() {
        isSpinning = 1;
        animator.SetInteger("spinning", isSpinning);
    }

    public void StopSpinning() {
        isSpinning = 0;
        animator.SetInteger("spinning", isSpinning);
    }
}
