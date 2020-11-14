using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordActionHandler : MonoBehaviour
{

    [SerializeField]
    WordCurveData[] curveData;

    public Transform unmaskerTransform = null;

    PointerFollower _pointer = null;
    public PointerFollower pointer {
        get
        {
            return _pointer;
        }
        set
        {
            _pointer = value;
            currentIdx = -1;
            OnFinishLine();
            pointer.ShowPointer();
        }
    }

    int currentIdx = 0;

    // Start is called before the first frame update
    public void OnFinishLine()
    {
        currentIdx++;
        if (currentIdx > curveData.Length - 1)
        {
            Debug.Log("Line Completed");
            pointer.HidePointer();
            GtionProduction.Vibration.Vibrate(200);
            TracingGame.Main.GameCleared();
            return;//game finish here
        }

        pointer.currentAction = curveData[currentIdx].actionType;
        pointer.forceMovePointer = curveData[currentIdx].forceRePosition;

        switch (curveData[currentIdx].actionType) {
            case ActionType.Curve:
                pointer.pointCount = curveData[currentIdx].stepCount;
                pointer.limitCurve = curveData[currentIdx].curve.GetComponent<GtionProduction.BezierCurve>();
                break;

            case ActionType.Dot:
                pointer.limitCurve = null;
                pointer.dot = curveData[currentIdx].curve;
                pointer.pointCount = 1;
                break;
        }
        


    }

    public void Hide() {
        GetComponent<Animation>().Play("WordHide");
        Destroy(gameObject, 0.4f);
    }

    public enum ActionType {
        Curve , Dot
    }

    [System.Serializable]
    public class WordCurveData {
        public ActionType actionType = ActionType.Curve;
        public bool forceRePosition = false;
        public Transform curve = null;
        public int stepCount = 50;
    }
}
