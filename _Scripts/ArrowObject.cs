using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObject : MonoBehaviour {
    public enum ArrowDirection {
        Forward,
        Backward,
        Left,
        Right
    }
    public enum ColorType {
        Blue,
        Green,
        Purple,
        Red,
        Yellow
    }

    [SerializeField] ArrowCellManager arrowCellManager;
    [SerializeField] ColorType colorType;
    [SerializeField] ArrowDirection currentArrowDirection;

    public ColorType GetColorType() {
        return colorType;
    }
    public ArrowCellManager GetArrowCellManager() {
        return arrowCellManager;
    }

    public ArrowCellManager GetCellGeneratorManager() {
        return arrowCellManager;
    }

    public ArrowDirection GetArrowDirection() {
        return currentArrowDirection;
    }
}
