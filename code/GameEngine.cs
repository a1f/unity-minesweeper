using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

// TODO: create tests
// TODO: connect all together
// TODO: start game on button and start timer on button

public class GameEngine : MonoBehaviour {

	void Awake() {
		gameTimer = GetComponent<GameTimer>();

		winSprite = winSpriteObject.GetComponent<SpriteRenderer>();
		loseSprite = loseSpriteObject.GetComponent<SpriteRenderer>();
	}

	void Start() {
		InitTheField();
	}

	void Update() {
		if (gameTimer.TimeOut) {
			GameOver();
		}
	}

	public void GameOver() {
		gameStatus = 2;

		winSprite.enabled = false;
		loseSprite.enabled = true;
		gameTimer.StopTimer();
	}

	public void GameWon() {
		gameStatus = 2;
		gameTimer.StopTimer();

		winSprite.enabled = true;
		loseSprite.enabled = false;
	}

	public void RestartGame() {
		PrepareField();

		winSprite.enabled = false;
		loseSprite.enabled = false;
		gameStatus = 0;
		gameTimer.RefreshTimer();
	}

	public void PressCell(int x, int y) {
		if (gameStatus == 2)
			return;
		if (gameStatus == 0) {
			GenerateMines(x, y);
			gameStatus = 1;
			gameTimer.StartTimer();
		}

		if (field [x, y].IsMine) {
			tileSprites [x, y].sprite = bomb;
			GameOver();
			return;
		}

		DFS(x, y);
		OpenCells();
		if (CountOpenCells() + countMines == width * height) {
			GameWon();
			return;
		}
	}

	private void DFS(int x, int y) {
		if (x < 0 || y < 0 || x >= height || y >= width) {
			return;
		}
		if (field[x, y].IsOpened || field[x, y].IsMine) {
			return;
		}
		field[x, y].IsOpened = true;
		if (field[x, y].CountNeighbors != 0) {
			return;
		}
		DFS(x - 1, y);
		DFS(x + 1, y);
		DFS(x, y - 1);
		DFS(x, y + 1);
	}

	private void GenerateMines(int setX, int setY) {
		int countSet = countMines;

		while (countSet > 0) {
			int x = (int) UnityEngine.Random.Range(0, height);
			int y = (int) UnityEngine.Random.Range(0, width);
			if (x == height) {
				--x;
			}
			if (y == width) {
				--y;
			}
			if (field[x, y].IsMine) {
				continue;
			}
			if (x == setX && y == setY) {
				continue;
			}
			field[x, y].IsMine = true;
			--countSet;
		}

		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < width; ++j) {
				if (field[i, j].IsMine) {
					continue;
				}
				int count = 0;
				for (int dx = -1; dx <= 1; ++dx) {
					for (int dy = -1; dy <= 1; ++dy) {
						int nx = i + dx, ny = j + dy;
						if (nx >= 0 && nx < height && ny >= 0 && ny < width) {
							if (field[nx, ny].IsMine) {
								++count;
							}
						}
					}
				}
				field[i, j].CountNeighbors = count;
			}
		}
	}

	private void InitTheField() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		Array.Sort<GameObject>(tiles, (x,y) => {
			if (Math.Abs(x.transform.position.y - y.transform.position.y) < 1e-6) {
				if (Math.Abs(x.transform.position.x - y.transform.position.x) < 1e-6) {
					return 0;
				}
				if (x.transform.position.x < y.transform.position.x) {
					return -1;
				} else {
					return 1;
				}
			}
			if (x.transform.position.y < y.transform.position.y) {
				return -1;
			} else {
				return 1;
			}
		});
		int count = 0;
		fieldRender = new GameObject[height, width];
		field = new Cell[height, width];
		tileSprites = new SpriteRenderer[height, width];
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < width; ++j) {
				fieldRender[i, j] = tiles[count++];
				CellData cd = fieldRender[i, j].GetComponent<CellData>();
				tileSprites[i, j] = fieldRender[i, j].GetComponent<SpriteRenderer>();
				cd.X = i;
				cd.Y = j;
			}
		}
		PrepareField();
	}

	private void OpenCells() {
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < width; ++j) {
				if (field[i, j].IsOpened) {
					if (tileSprites[i, j].sprite == closed) {
						tileSprites[i, j].sprite = numbers[field[i, j].CountNeighbors];
					}
				}
			}
		}
	}

	private void PrepareField() {
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < width; ++j) {
				field[i, j] = new Cell();
				tileSprites[i, j].sprite = closed;
			}
		}
	}

	private int CountOpenCells() {
		int count = 0;
		for (int i = 0; i < height; ++i) {
			for (int j = 0; j < width; ++j) {
				if (field[i, j].IsOpened) {
					++count;
				}
			}
		}
		return count;
	}

	public Sprite[] numbers;
	public Sprite bomb;
	public Sprite closed;
	public GameObject winSpriteObject;
	public GameObject loseSpriteObject;
	public int height;
	public int width;
	public int countMines;

	private GameTimer gameTimer;
	private SpriteRenderer[,] tileSprites;
	private SpriteRenderer winSprite;
	private SpriteRenderer loseSprite;

	private Cell[,] field;
	private GameObject[,] fieldRender;
	// 0 - init, 1 - running, 2 - over
	private int gameStatus;
}
