
/// <summary>
/// 网格坐标.
/// </summary>
public struct SGridCoordinate
{
	public int x, z;
	//--越界坐标.
	public bool Overstep;

	public SGridCoordinate (int _x, int _z)
	{
		this.x = _x;
		this.z = _z;
		this.Overstep = false;
	}

	public void Clean ()
	{
		this.x = -1;
		this.z = -1;
		this.Overstep = false;
	}

	public bool IsNone ()
	{
		return this.x == -1 && this.z == -1;
	}

	public bool IsEqual (SGridCoordinate coordinate)
	{
		return this.x == coordinate.x && this.z == coordinate.z ;
	}
}
