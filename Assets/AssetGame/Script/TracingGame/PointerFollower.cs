using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerFollower : MonoBehaviour
{
    [Header("Mask")]
    [SerializeField] GameObject unmasker = null;
    int headMask = 0;
    GameObject[] masks = null;


    [Header("Pointer")]
    [HideInInspector] public WordActionHandler.ActionType currentAction = WordActionHandler.ActionType.Curve;
    [SerializeField] Transform pointer = null;
    [SerializeField] Sprite curveSprite = null;
    [SerializeField] Sprite dotSprite = null;
    SpriteRenderer pointerRenderer {
        get {
            return pointer.GetComponent<SpriteRenderer>();
        }
    }
    [HideInInspector] public bool forceMovePointer = true;

    [HideInInspector]
    public int pointCount = 0;
    GtionProduction.BezierCurve _limitCurve = null;
    public GtionProduction.BezierCurve limitCurve {
        get
        {
            return _limitCurve;
        }
        set
        {
            _limitCurve = value;
            if (value != null)
                StartPointerCalc();
        }
    }

    Transform _dot = null;
    public Transform dot
    {
        get
        {
            return _dot;
        }
        set
        {
            _dot = value;
            if (value != null)
                StartPointerCalc();
        }
    }

    Vector2[] vectorCurve = null;

    [Header("Word Object")]
    public WordActionHandler word = null;

    int currentProgress = 0;


    private void Start()
    {
        //pointerRenderer = 
        PointerTracingHandler.onDown += OnDown;
        PointerTracingHandler.onDrag += OnDrag;
        HidePointer();
    }

    private void OnDestroy()
    {
        PointerTracingHandler.onDown -= OnDown;
        PointerTracingHandler.onDrag -= OnDrag;
    }

    public void HidePointer() {
        pointer.gameObject.SetActive(false);
        pointCount = 0;
    }
    public void ShowPointer()
    {
        pointer.gameObject.SetActive(true);
    }

    void StartPointerCalc()
    {

        currentProgress = 0;
        prevProgress = 0;
        deltaProgress = 0;

        if (currentAction == WordActionHandler.ActionType.Dot)
        {
            pointerRenderer.sprite = dotSprite;
            if (forceMovePointer)
            {
                transform.position = dot.position;
                pointer.position = transform.position;
                isMoving = false;
            }
            vectorCurve = new Vector2[1] { dot.position };
            RotateNormalize(Vector2.down, 10000);
        }
        else
        {
            pointerRenderer.sprite = curveSprite;
            if (forceMovePointer)
            {
                transform.position = limitCurve.GetPoint(0);
                pointer.position = transform.position;
            }

            headMask = 0;
            masks = new GameObject[pointCount];
            vectorCurve = new Vector2[pointCount];

            for (int i = 0; i < pointCount; i++)
            {
                float t = Mathf.InverseLerp(0, pointCount-1, i);
                vectorCurve[i] = limitCurve.GetPoint(t);
                masks[i] = Instantiate(unmasker, vectorCurve[i], Quaternion.identity, word.unmaskerTransform);
                masks[i].SetActive(false);
            }

            if (!forceMovePointer)
            {
                RePosition(prev);
            }
            deltaProgress = currentProgress - prevProgress;
            prevProgress = currentProgress;
            InstantiateMask();
        }
    }

    int NearestPos(Vector2 from) {
        int min = 0;
        float shortest = Vector2.Distance(vectorCurve[min], from);
        float tempDist = 0;
        for (int i = 1; i < vectorCurve.Length; i++) {
            tempDist = Vector2.Distance(vectorCurve[i], from);
            if (shortest > tempDist) {
                min = i;
                shortest = tempDist;
            }
        }
        return min;
    }


    bool isMoving = false;
    // Update is called once per frame
    void OnDown(Vector2 pos)
    {
        //transform.position = pos;
        switch (currentAction) {

            case WordActionHandler.ActionType.Dot:
                isMoving = false;
                if (Vector2.Distance(transform.position, pos) < 1f)
                {
                    Instantiate(unmasker, pointer.position, Quaternion.identity, word.unmaskerTransform);
                    word.OnFinishLine();
                }
                break;
            case WordActionHandler.ActionType.Curve:
                if (Vector2.Distance(transform.position, pos) < 1f)
                {
                    isMoving = true;
                    prev = pos;
                }
                else
                {
                    isMoving = false;
                }
                break;
        }
        
        
    }

    Vector2 prev = Vector2.zero;
    Vector2 delta = Vector2.zero;

    //Vector2 prevTouch = Vector2.zero;

    // Update is called once per frame
    void OnDrag(Vector2 pos)
    {
        if (!isMoving)
            return;

        //|| ((pos-prev).magnitude > 0.2f)
        if (Vector2.Distance(pointer.position, pos) > 1f )
        {
            isMoving = false;
            transform.position = pointer.position;
            Debug.Log("MISS");
        }
        //transform.position = pos;
        delta = pos - prev;
        prev = pos;

        RePosition(pos);

    }

    void InstantiateMask() {


        if (deltaProgress == 0)
            return;

        bool ispositive = deltaProgress > 0 ? true : false;
        int positive = deltaProgress > 0 ? 1 : -1;
        for (int i = 0; i < Mathf.Abs(deltaProgress); i++) {
            masks[headMask].SetActive(ispositive);
            headMask += positive; 
        }

        deltaProgress = 0;
    }


    int prevProgress = 0;
    int deltaProgress = 0;
    void RePosition(Vector2 pos) {



        int tempProgress = NearestPos(pos);
        //deltaProgress = ;

        if (tempProgress - prevProgress < 0) {
            transform.position = pointer.position;
            currentProgress = prevProgress;
            return;
        }else if (tempProgress - prevProgress > pointCount*0.2) {
            isMoving = false;
            transform.position = pointer.position;
            currentProgress = prevProgress;
            Debug.Log("MISS");
            return;
        }

        transform.position = vectorCurve[tempProgress];
        

        if (currentProgress == pointCount - 1) {
            //isMoving = false;
            word.OnFinishLine();
        }

    }

    Vector2 pVelo = Vector2.zero;

    private void Update()
    {
        if (pointCount == 0)
            return;

        pointer.transform.position = Vector2.SmoothDamp( pointer.position ,transform.position, ref pVelo, 0.02f);
        currentProgress = NearestPos(pointer.position);
        deltaProgress = currentProgress - prevProgress;
        prevProgress = currentProgress;
        //Debug.Log(deltaProgress + " - " + currentProgress);
        InstantiateMask();//, deltaProgress);
        RotateNormalize(50);
    }

    public void RotateNormalize(float speed = 6f)
    {
        if (vectorCurve.Length < 2)
            return;

        int progress = currentProgress;
        if (currentProgress == vectorCurve.Length - 1)
        {
            progress = currentProgress - 1;
        }

        Vector2 direction = (vectorCurve[progress + 1] - vectorCurve[progress]).normalized;

        RotateNormalize(direction, speed);
    }
    public void RotateNormalize( Vector2 direction, float speed = 6f)
    {


        Quaternion fromRot = pointer.rotation;
        Quaternion targetRot = Quaternion.FromToRotation(Vector2.down, direction);
        pointer.eulerAngles = new Vector3(0, 0, Quaternion.Slerp(fromRot, targetRot, (speed / 2f) * Time.deltaTime).eulerAngles.z);
    }
}
