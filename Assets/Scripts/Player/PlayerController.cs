using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    NavMeshAgent NavAgent;

    [Header("Movement")]
    [SerializeField] LayerMask WalkableLayer;
    [SerializeField] private float TurnSpeed = 10.0f;
    [SerializeField] private float WalkableDistance = 30.0f;


    void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();

    }

    public void ExecuteMovement()
    {
        // Debug.Log("HOLY SHIT");
        RaycastHit TargetLocation;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out TargetLocation, 100.0f, WalkableLayer))
        {
            NavAgent.destination = TargetLocation.point;
            
            //play effects or whatever

        }
    }
}
