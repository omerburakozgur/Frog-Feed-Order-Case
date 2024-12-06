using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowCellManager : MonoBehaviour, ICellObject {

    [SerializeField] private ArrowObject arrowReference;
    [SerializeField] private CellGenerator cellGeneratorReference;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void EnableObject() {
        arrowReference.gameObject.SetActive(true);

    }
    public void GetObject() {
        throw new System.NotImplementedException();
    }

    public void DestroyCell() {
        Destroy(gameObject);
    }

    public void RemoveObject() {
        Destroy(arrowReference);
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
