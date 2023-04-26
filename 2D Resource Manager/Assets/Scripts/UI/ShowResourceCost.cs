using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowResourceCost : MonoBehaviour {

    public PlacedObjectTypeSO placedObjectTypeSO;
    public GameObject resourceWindow;
    public RectTransform windowBacking;
    public Image buildingImage;
    public TMP_Text nameText;
    public TMP_Text ironText;
    public TMP_Text copperText;

    private bool setPositions;
    private int resourceNum;

    public void Update() {
        if(setPositions == true) {
            List<TMP_Text> listOfText = ResourceNumCheck();
            ResourceRearange(listOfText);
            resourceWindow.GetComponent<RectTransform>().anchoredPosition = GetPositionToMove(resourceNum);
            windowBacking.localScale = GetBackingScale();
            setPositions = false;
        }

        if(resourceWindow.activeSelf == false) {
            ResetPositions();
        }

    }

    public void ShowCostOfBuilding() {
        resourceWindow.SetActive(true);
        buildingImage.sprite = placedObjectTypeSO.buildingImage;
        nameText.text = placedObjectTypeSO.nameString;
        setPositions = true;
        ironText.text = placedObjectTypeSO.ironCost.ToString();
        copperText.text = placedObjectTypeSO.copperCost.ToString();
    }

    private List<TMP_Text> ResourceNumCheck() {
        resourceNum = 0;
        List<TMP_Text> listOfMaterialCosts = new List<TMP_Text>();
        if(placedObjectTypeSO.ironCost > 0) {
            resourceNum = resourceNum + 1;
        }
        if(placedObjectTypeSO.copperCost > 0) {
            resourceNum = resourceNum + 1;
        }

        listOfMaterialCosts.Add(ironText);
        listOfMaterialCosts.Add(copperText);
        return listOfMaterialCosts;
    }

    private Vector2 GetPositionToMove(int numOfResources) {
        int movementPerResource = 40;
        int numOfResource = numOfResources - 1;
        return new Vector2(0, movementPerResource * numOfResource);
    }

    private void ResourceRearange(List<TMP_Text> listOfMaterialCosts) {
        int numOfHiddenResources = 0;
        for(int i = 0; i < listOfMaterialCosts.Count; i++) {
            GameObject gameHolder = listOfMaterialCosts[i].gameObject;
            gameHolder.transform.parent.gameObject.SetActive(true);
            if(listOfMaterialCosts[i].text == "0") {
                numOfHiddenResources = numOfHiddenResources + 1;
                gameHolder.transform.parent.gameObject.SetActive(false);
            }
            if(numOfHiddenResources > 0) {
                gameHolder.transform.parent.GetComponent<RectTransform>().anchoredPosition = listOfMaterialCosts[i].GetComponent<RectTransform>().anchoredPosition + new Vector2(-32.4f, 10 * numOfHiddenResources);
            }
        }
    }

    private void ResetPositions() {
        ironText.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        copperText.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    private Vector3 GetBackingScale() {
        if(resourceNum == 1) {
            return new Vector3(windowBacking.localScale.x ,1 ,windowBacking.localScale.z);
        }
        if(resourceNum == 2) {
            return new Vector3(windowBacking.localScale.z ,1.4f ,windowBacking.localScale.z);
        }
        return new Vector3(windowBacking.localScale.x ,1 ,windowBacking.localScale.z);
    }
}
