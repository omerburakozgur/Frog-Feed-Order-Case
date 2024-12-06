using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICellObject {

    public void EnableObject();
    public void GetObject();
    public void RemoveObject();
    public void DestroyCell();
    public GameObject GetParentGameobject();
    public void SetCellGeneratorReference(CellGenerator cellGenerator);


}
