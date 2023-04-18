using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour {
    
    public GameObject resourceWindow;

    private bool rearangeWindow = true;
    
    private void Update() {
        MouseHoveringShowResourceCost();
    }
    //Function to check if mouse is over the UI with ignores
    public bool IsMouseOverUI() {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for(int i = 0; i < raycastResultList.Count; i++) {
            if(raycastResultList[i].gameObject.GetComponent<MouseUIClickThrough>() != null) {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }

    private void MouseHoveringShowResourceCost() {
        bool hideWindow = true;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for(int i = 0; i < raycastResultList.Count; i++) {
            if(raycastResultList[i].gameObject.GetComponent<ShowResourceCost>() != null && rearangeWindow == true) {
                ShowResourceCost showResourceCostScript =raycastResultList[i].gameObject.GetComponent<ShowResourceCost>();
                showResourceCostScript.ShowCostOfBuilding();
                rearangeWindow = false;
            }
            if(raycastResultList[i].gameObject.GetComponent<ShowResourceCost>() != null) {
                hideWindow = false;
            }
        }
        if(hideWindow == true) {
            resourceWindow.SetActive(false);
            rearangeWindow = true;
        }
    }
}
