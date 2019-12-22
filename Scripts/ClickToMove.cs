using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent navMeshAgent = null;

    [SerializeField]
    float approacheLimit = 0.3f;

    [SerializeField]
    float reamingDistanceRange = 0.3f;

    public class WalkingStopEventArgs : System.EventArgs
    {
        public Vector3 nowPos;
        public Vector3 targetPos;

        public WalkingStopEventArgs(Vector3 _nowPos, Vector3 _targetPos)
        {
            nowPos = _nowPos;
            targetPos = _targetPos;
        }
    }

    public System.EventHandler<WalkingStopEventArgs> WalkingStop;

    public bool Walking { get { return walking; }}
    bool walking = false;

    Vector3 nowTarget;
    public Vector3 NowTarget { get { return nowTarget; }}

    public void SetNavigateTo(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        SetNavigateTo(pointerData.pointerPressRaycast.worldPosition);
    }

    public void SetNavigateTo(Vector3 point)
    {
        nowTarget = point;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(point);
        StartCoroutine(WalkingTruing());
    }

    IEnumerator WalkingTruing()
    {
        yield return new WaitForEndOfFrame();
        walking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(walking && (Mathf.Abs(navMeshAgent.remainingDistance) <= reamingDistanceRange || Vector3.Distance(nowTarget, navMeshAgent.transform.position) <= approacheLimit))
        {
            Stop();
        }
    }

    public void Stop()
    {
        Debug.Log("Stopped");

        navMeshAgent.isStopped = true;
        walking = false;
        WalkingStop?.Invoke(this, new WalkingStopEventArgs(transform.position, nowTarget));
    }

    public void SetNavMeshAgent(NavMeshAgent _navMeshAgent)
    {
        navMeshAgent = _navMeshAgent;
    }
}
