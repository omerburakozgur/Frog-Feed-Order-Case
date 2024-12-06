using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeAnimator : MonoBehaviour {

    Animator animator;
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        StartTrigger();

    }

    //private void OnEnable() {
    //    StartTrigger();
    //}
    private void StartTrigger() {
        animator.SetTrigger(AnimationStrings.START_TRIGGER);

    }

    public void CorrectTrigger() {
        animator.SetTrigger(AnimationStrings.CORRECT_TRIGGER);

    }

    public void WrongTrigger() {
        animator.SetTrigger(AnimationStrings.WRONG_TRIGGER);

    }

    public void ReturnTrigger() {
        animator.SetTrigger(AnimationStrings.WRONG_TRIGGER);

    }

    public void ResetTrigger() {
        animator.SetTrigger(AnimationStrings.RESET_TRIGGER);

    }
}
