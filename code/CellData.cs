using UnityEngine;
using System.Collections;
using System;

public class CellData : MonoBehaviour {

	void Awake() {
		game = gameScripts.GetComponent<GameEngine>();
	}

	void Start() {
	}

	void OnMouseDown() {
		Debug.Log(string.Format("Pressed cell x = {0}, y = {1}", x, y));
		game.PressCell(x, y);
	}

	private GameEngine game;
	public GameObject gameScripts;

	private int x;
	public int X {
		get {
			return x;
		}
		set {
			x = value;
		}
	}

	private int y;
	public int Y {
		get {
			return y;
		}
		set {
			y = value;
		}
	}
}
