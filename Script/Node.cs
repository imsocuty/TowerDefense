using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    // Start is called before the first frame update

    public Color StartColor;
    public Color SelectColor;
    public Renderer rend;

    public bool Color;
    void Start()
    {
        rend = GetComponent<Renderer>();
        StartColor = rend.material.color;
        
    }

    public void NodeD(){
            rend.material.color = StartColor;
    }                            
    private void OnMouseUp()
    {
        if(rend.material.color == StartColor){

            Color = false;
            rend.material.color = SelectColor;
            BuildManager.instance.SelectNode = gameObject;
            
        }else{
            Color = true;
            rend.material.color = StartColor;      
        }
    }
}
