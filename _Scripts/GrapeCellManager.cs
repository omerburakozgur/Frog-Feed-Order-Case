using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeCellManager : MonoBehaviour, ICellObject {

    [SerializeField] private CollectibleGrape grapeReference;
    [SerializeField] private CellGenerator cellGeneratorReference;
    public event EventHandler OnCellDestroyed;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void EnableObject() {
        if (grapeReference != null) {
            grapeReference.gameObject.SetActive(true);

        }
    }

    public void GetObject() {
        throw new System.NotImplementedException();
    }

    public void DestroyCell() {
        OnCellDestroyed?.Invoke(this, EventArgs.Empty);
        // create cell animation controllers for spawn and destroy
        // play destroy animation
    }
    public void DestroyCellGameobject() {
        Destroy(gameObject);
    }
    public void RemoveObject() {
        Destroy(grapeReference);
    }

    public GameObject GetParentGameobject() {
        return gameObject;
    }

    public void SetCellGeneratorReference(CellGenerator cellGenerator) {
        cellGeneratorReference = cellGenerator;
    }

    public CellGenerator GetCellGeneratorReference() {
        return cellGeneratorReference;
    }
}
