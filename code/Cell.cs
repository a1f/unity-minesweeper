public class Cell {
	// TODO: probably merge with @code{CellData}

	public Cell(bool mine) {
		isOpened = false;
		isMine = mine;
		countNeighbors = 0;
	}

	public Cell() {
		isOpened = false;
		isMine = false;
		countNeighbors = 0;
	}

	private bool isOpened; 
	public bool IsOpened {
		get {
			return isOpened;
		}

		set {
			isOpened = value;	
		}
	}

	private bool isMine;
	public bool IsMine { 
		get {
			return isMine;
		}

		set {
			isMine = value;	
		}
	}

	private int countNeighbors;
	public int CountNeighbors { 
		get {
			return countNeighbors;
		}

		set {
			countNeighbors = value;	
		}
	}
}

