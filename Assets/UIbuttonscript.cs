using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbuttonscript : MonoBehaviour
{
    private bool isSelected;
    public MenuLogic menu;
    public GameObject image;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
	image.SetActive(false);
    }
    void OnEnable()
    {
        isSelected = false;
	image.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeSelected() {
	
	isSelected = !isSelected;
	if(isSelected) {
		image.SetActive(true);
		menu.gunList.Add(name);
	}
	else {
		image.SetActive(false);
		menu.gunList.Remove(name);
	}
	Debug.Log(menu.gunList.Count);
    }
}
