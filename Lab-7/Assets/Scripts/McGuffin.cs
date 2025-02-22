using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McGuffin : MonoBehaviour, IInteractable
{
    public GameObject Item;
    private bool hasItem = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        Debug.Log("Found the McGuffin!");
        hasItem = true;
        Item.SetActive(false);
    }
}
