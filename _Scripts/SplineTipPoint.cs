using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using static FrogObject;

public class SplineTipPoint : MonoBehaviour {

    [SerializeField] private SplineTest splineTest;
    [SerializeField] private FrogObject frogObject;
    private bool gotLastItem = false;

    private void OnTriggerEnter(Collider other) {
        // get color type of the frog with a method
        if (other.tag.Equals(TagStrings.GRAPE)) {
            CollectibleGrape grape = other.GetComponent<CollectibleGrape>();
            GrapeAnimator grapeAnimator = other.GetComponent<GrapeAnimator>();


            if (splineTest.GetReverseEnabled() && grape.GetSplineCollisionEnabled()) {

                if ((int)frogObject.GetColorType() == ((int)grape.GetColorType())) {
                    if (!gotLastItem) {
                        CollectibleGrape lastGrape = splineTest.grapeList.Last();
                        lastGrape.transform.SetParent(transform, true);
                        lastGrape.transform.localPosition = Vector3.zero;
                        gotLastItem = true;
                    }


                    other.transform.SetParent(transform, true);
                    other.transform.localPosition = Vector3.zero;
                    // trigger destroy cell animation, it should destroy itself automatically then spawn the next cell with its own animation
                    grape.GetGrapeCellManager().GetCellGeneratorReference().RemoveTopMostCell();
                    // set the grape collection successful to true
                    splineTest.grapeCollectionSuccessful = true;
                }
            }
            else {
                if ((int)frogObject.GetColorType() == ((int)grape.GetColorType())) {
                    // the color of the grape matches the color of the frog but this is the first collision
                    if (!splineTest.GetReverseEnabled()) {
                        grapeAnimator.CorrectTrigger();


                    }
                    else {
                        // get the animators scripts of added grapes and trigger the return animation
                    }
                    print("Play grape animation");

                }
                else {
                    grapeAnimator.WrongTrigger();

                    // the tongue collided with a different type of grape type
                    // return tongue spline back to its original position
                    // create a method that reset the spline logic or regenerate a new frog if required
                }
            }
        }
    }
}
