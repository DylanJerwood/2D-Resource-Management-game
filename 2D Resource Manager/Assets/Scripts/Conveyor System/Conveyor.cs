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

    public float speed; 

    private void Start() {
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
            Deposit();
        }
    }

    //Gets position of the next conveyor/where the item needs to go next
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

            var step = speed * Time.deltaTime;

            while(conveyorItem.item.transform.position != toPosition) {
                conveyorItem.transform.position = Vector3.MoveTowards(conveyorItem.transform.position, toPosition, step);

                yield return null;
            }

            isSpaceTaken = false;
            conveyorItem.transform.parent = conveyorInSequence.transform;
            conveyorInSequence.conveyorItem = conveyorItem;
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
        }
        return null;
    }
    //Deposit
    private void Deposit() {
        Vector2 origin = new Vector2(raycastOrigin.transform.position.x, raycastOrigin.transform.position.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, transform.TransformDirection(Vector2.down), 1f);

        if(hit) {
            Turret turret = hit.collider.GetComponent<Turret>();

            if(turret != null && turret.ammoCount < turret.maxAmmo) {
                turret.ammoCount = turret.ammoCount + 1;
                ClearSpace();
            }

            if(hit.collider.GetComponent<CoreScript>()) {
                CoreScript core = hit.collider.GetComponent<CoreScript>();
                core.IncreaseMatCount(conveyorItem.name);
                ClearSpace();
            }
            
        }
        
    }

    private void ClearSpace(){
        isSpaceTaken = false;
        Destroy(conveyorItem.gameObject);
        conveyorItem = null;
    }
}
