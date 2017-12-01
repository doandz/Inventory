using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEntry : MonoBehaviour, ISelectHandler, IDeselectHandler {

	[SerializeField]
	Text _name;
	
	[SerializeField]
	Text _category;

	[SerializeField]
	Text _brand;

	[SerializeField]
	Text _unitScale;

	[SerializeField]
	Text _quantity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setProperties(string name, string category, string brand, string unitScale, float scale, float quantity){
		_name.text = name;
		_category.text = category;
		_brand.text = brand;
		_unitScale.text = unitScale + " : " + scale.ToString ();
		_quantity.text = quantity.ToString ();
	}

	public void OnSelect(BaseEventData data)
	{
		GetComponent<Image>().color = new Color32(128,128,225,100);
		print ("OnSelect");
	}
	public void OnDeselect(BaseEventData data)
	{

		GetComponent<Image>().color = new Color32(255,255,225,100);
		print ("OnDeselect");
	}
}
