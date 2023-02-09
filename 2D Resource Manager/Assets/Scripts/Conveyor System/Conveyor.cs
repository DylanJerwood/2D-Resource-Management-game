using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private static int conveyorID = 0;

    public Conveyor conveyorInSequence;
    public ConveyorItem conveyorItem;
    public bool isSpaceTaken;
    public GameObject raycastOrigin;
    public GameObject itemPostion;

    private ConveyorManager conveyorManager;

    private void Start() {
        conveyorManager = FindObjectOfType<ConveyorManager>();
        conveyorInSequence = null;
        conveyorInSequence = FindNextConveyor();
        gameObject.name = $"Conveyor: {conveyorID++}";
    }

    private void Update() {

        // if(conveyorInSequence == null) {
        //     conveyorInSequence = FindNextConveyor();
        // }
        
        // if (Input.GetKeyDown(KeyCode.F)) {
        //     conveyorInSequence = FindNextConveyor();
        // } 

        conveyorInSequence = FindNextConveyor();

        if(conveyorItem != null && conveyorItem.item != null) {
            StartCoroutine(StartConveyorMove());
        }
    }

    //Gets position of where item needs to go on the next belt
    public Vector3 GetItemPosition() {
        var position = itemPostion.transform.position;
        return new Vector3(position.x, position.y, position.z);
    }

    //visually moves the item
    private IEnumerator StartConveyorMove() {
        isSpaceTaken = true;

        if(conveyorItem.item != null && conveyorInSequence != null && conveyorInSequence.isSpaceTaken == false) {
            Vector3 toPosition = conveyorInSequence.GetItemPosition();

            conveyorInSequence.isSpaceTaken = true;

            var step = conveyorManager.speed * Time.deltaTime;

            while(conveyorItem.item.transform.position != toPosition) {
                conveyorItem.transform.position = Vector3.MoveTowards(conveyorItem.transform.position, toPosition, step);

                yield return null;
            }

            isSpaceTaken = false;
            conveyorInSequence.conveyorItem = conveyorItem;
            conveyorItem.transform.parent = conveyorInSequence.transform;
            conveyorItem = null;
        }
    }

    //finds the next conveyor using raycast
    private Conveyor FindNextConveyor()
    {
        Vector2 origin = new Vector2(raycastOrigin.transform.position.x, raycastOrigin.transform.position.y);

        Debug.DrawRay(origin, transform.TransformDirection(Vector2.down) * 1f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.down), 1f); 
        
        if (hit) {
            Conveyor conveyor = hit.collider.GetComponent<Conveyor>();

            if(conveyor != null) {
                return conveyor;
            }
            else {
                return null;
            }
        }
        else{
            return null;
        }
    }
}
