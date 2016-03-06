using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	void Start () {
		rt = greenBar.transform;
	}

	public void RefreshTimer() {
		timePassed = 0;
		timeOut = false;
		timerRun = false;
		widthInPercents = 100;
		coef = 0.0001f;

		rt.localScale = new Vector3(0.6238888f, 0.3478899f, 1f);
		rt.position = new Vector3(-0.129f, 1.379f, -1f);
	}

	public void StartTimer() {
		timerRun = true;
	}

	public void StopTimer() {
		timerRun = false;
	}

	void Update () {
		if (timerRun) {
			timePassed += Time.deltaTime;
			widthInPercents = 100f - 100f * timePassed / totalTime;
			float curWidth = 0.006238888f * widthInPercents;
			float origWidth = 0.6238888f;
			float delta = origWidth - curWidth;
			rt.localScale = new Vector3(0.006238888f * widthInPercents, 0.3478899f, 1f);
			rt.position = new Vector3(-0.129f - delta - coef, 1.379f, -1f);
			coef *= 1.01f;
			if (widthInPercents >= 30) {
				coef = Mathf.Min(coef, 0.025f);
			} else if (widthInPercents >= 10) {
				coef = Mathf.Min(coef, 0.036f);
			} else {
				coef = Mathf.Min(coef, 0.041f);
			}
			if (timePassed >= totalTime) {
				timerRun = false;
				timeOut = true;
			}
		}
	}

	private float coef = 0.0001f;
	private float widthInPercents;
	private float timePassed;
	private GameObject progress;
	private Transform rt;

	private bool timerRun;
	public bool TimerRun {
		get {
			return timerRun;
		}
	}

	private bool timeOut;
	public bool TimeOut {
		get {
			return timeOut;
		}
	}

	public float totalTime;
	public GameObject greenBar;
}
