
/// <summary>
/// 网格坐标.
/// </summary>
public struct SGridCoordinate
{
	public int x, z;
	public EAreaType MAreaType;

	public SGridCoordinate (int _x, int _z)
	{
		this.x = _x;
		this.z = _z;
		this.MAreaType = Definition.GetAreaType(_x,_z);
	}

	public void Clean ()
	{
		this.x = -1;
		this.z = -1;
		this.MAreaType = EAreaType.Out;
	}
		
}
