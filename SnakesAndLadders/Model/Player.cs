namespace SnakesAndLadders.Model
{
	public sealed class Player
	{
		#region Fields

		private int currentCellPosition;

		private string name;

		#endregion

		#region Constructor

		public Player(string name)
		{
			this.SetCurrentCellPosition(0);
			this.SetName(name);
		}

		#endregion

		#region Methods

		public int GetCurrentCellPosition() => this.currentCellPosition;

		public string GetName() => this.name;

		public void IncrementCurrentCellPosition(int currentDiceValue) => this.currentCellPosition = this.currentCellPosition + currentDiceValue;

		public void SetCurrentCellPosition(int currentCellPosition) => this.currentCellPosition = currentCellPosition;

		private void SetName(string name) => this.name = name;
		
		#endregion
	}
}