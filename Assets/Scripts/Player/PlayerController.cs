using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    NavMeshAgent NavAgent;

    [Header("Movement")]
    [SerializeField] LayerMask WalkableLayer;
    [SerializeField] private float TurnSpeed = 10.0f;
    [SerializeField] private float WalkableDistance = 30.0f;

    [Header("Visual")]
    [SerializeField] GameObject PointerMeshClass;
    private GameObject PointerMesh;

    private float Difference;
    private NavMeshPath Path;

    void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        Path = new NavMeshPath();
    }
    void Update()
    {
        TurnToTarget();
        
        CheckIfCompleted();
    }

    public void ExecuteMovement()
    {
       
        RaycastHit TargetLocation;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out TargetLocation, 100.0f, WalkableLayer))
        {
            NavMesh.CalculatePath(transform.position, TargetLocation.point, NavMesh.AllAreas, Path);
        

            float CurrentTraveledLen = 0;
            Vector3 FinalLoc = new Vector3();

            for (int i = 0; i < Path.corners.Length - 1; i++)
            {
                float DistanceBetweenPoints = Vector3.Distance( Path.corners[i], Path.corners[i + 1]);

                CurrentTraveledLen += DistanceBetweenPoints;
                
                if(CurrentTraveledLen > WalkableDistance)
                {
                    //calculate the thingy
                    float ratio =  Difference / DistanceBetweenPoints ; 
                    
                    FinalLoc = new Vector3(
                        (1-ratio) * Path.corners[i].x  + ratio* Path.corners[i+1].x, 
                        (1-ratio) * Path.corners[i].y  + ratio* Path.corners[i+1].y,
                        (1-ratio) * Path.corners[i].z  + ratio* Path.corners[i+1].z
                    );

                    break;
                }
                else
                {
                    FinalLoc = Path.corners[i+1];
                }

                Difference = WalkableDistance - CurrentTraveledLen;

                Debug.DrawLine(Path.corners[i], Path.corners[i + 1], Color.red, 10.0f);
            }

            NavAgent.destination = FinalLoc;
           
            SpawnCircleCursor(FinalLoc);

        }
    }

    void TurnToTarget()
    {
        if (NavAgent.hasPath)
        {
            Vector3 targetRot = (NavAgent.destination - transform.position).normalized;
            Quaternion Rotation = Quaternion.LookRotation(new Vector3(targetRot.x, 0, targetRot.z));

            transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, Time.deltaTime * TurnSpeed);
        }
       
    }

    void SpawnCircleCursor(Vector3 SpawnLocation)
    {
        if (PointerMesh)
        {
            Destroy(PointerMesh);
        }
        PointerMesh = Instantiate(PointerMeshClass, SpawnLocation, PointerMeshClass.transform.rotation);

    }


    void CheckIfCompleted()
    {
        float dist = NavAgent.remainingDistance;
        if(dist != Mathf.Infinity && NavAgent.pathStatus == NavMeshPathStatus.PathComplete && dist == 0)
        {
            Destroy(PointerMesh);
        }
    }
}
