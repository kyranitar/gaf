using UnityEngine;
using System.Collections;

public class MaterialHandle : MonoBehaviour {
	
	public Material[] myMaterials;
	private int nextMaterial = 0;
	
	void Start () {
	
	}

	void Update () {
		
	}
	
	public void NextMaterialUsed() {
	
		renderer.sharedMaterial = myMaterials[nextMaterial];
	}
	
	public void IncMaterialIndex(bool right) {
		
		if(right) {
			if (nextMaterial < myMaterials.Length - 1) {
				nextMaterial += 1;
			} else {
				nextMaterial = 0;
			}
		} else {
			if(nextMaterial > 0) {
				nextMaterial -= 1;
			} else {
				nextMaterial = myMaterials.Length - 1;
			}
		}
		
		NextMaterialUsed();
	}
}
