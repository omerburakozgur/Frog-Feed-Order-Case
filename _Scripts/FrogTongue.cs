using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTongue : MonoBehaviour {
    [SerializeField] private FrogObject frogObjectReference;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public FrogObject GetFrogReference() {
        return frogObjectReference;
    }
}
