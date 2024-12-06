using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAnimator : MonoBehaviour {
    private GrapeCellManager grapeCellManager;
    Animator animator;
    // Start is called before the first frame update
    void Start() {
        grapeCellManager = GetComponent<GrapeCellManager>();
        animator = GetComponent<Animator>();
        grapeCellManager.OnCellDestroyed += GrapeCellManager_OnCellDestroyed;
    }

    private void GrapeCellManager_OnCellDestroyed(object sender, System.EventArgs e) {
        PlayCellDestroyAnimation();
    }

    public void PlayCellDestroyAnimation() {
        animator.SetTrigger(AnimationStrings.DESTROY_TRIGGER);
    }
}
