using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemEntry : MonoBehaviour {

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
}
