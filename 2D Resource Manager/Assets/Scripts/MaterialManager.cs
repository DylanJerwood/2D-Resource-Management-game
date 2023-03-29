using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MaterialManager : MonoBehaviour {

    public int ironCount;
    public int copperCount;

    public TMP_Text ironText;
    public TMP_Text copperText;
    
    private void Start() {

    }

    private void Update() {
        ironText.text = ironCount.ToString();
        copperText.text = copperCount.ToString();
    }
}
