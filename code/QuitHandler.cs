using UnityEngine;
using System.Collections;

public class QuitHandler : MonoBehaviour {
	void OnMouseDown() {
		Debug.Log("Quit");
		Application.Quit();
	}
}
