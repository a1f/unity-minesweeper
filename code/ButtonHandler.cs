using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

	public GameObject gameScripts;

	void Start() {
		game = gameScripts.GetComponent<GameEngine>();
	}

	void OnMouseDown() {
		game.RestartGame();
	}

	private GameEngine game;
}
