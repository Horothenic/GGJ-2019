
/// /////////////////////////////// ///
/// ///////  UTILITY ENUMS  /////// ///
/// /////////////////////////////// ///

public enum Direction
{
	None	= 0x00,
	Right 	= 0x01,
	Left	= 0x02,
	Up		= 0x04,
	Down	= 0x08
}

static class DirectionMethods
{

	public static Direction Opposite(this Direction dir)
	{
		Direction newDir = Direction.None;
		if ((dir & Direction.Right) == Direction.Right)
			newDir = newDir | Direction.Left;
		if ((dir & Direction.Left) == Direction.Left)
			newDir = newDir | Direction.Right;
		if ((dir & Direction.Up) == Direction.Up)
			newDir = newDir | Direction.Down;
		if ((dir & Direction.Down) == Direction.Down)
			newDir = newDir | Direction.Up;

		return newDir;
	}
}