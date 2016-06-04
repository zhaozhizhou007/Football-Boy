
public interface IInput  {

	/// <summary>
	/// 输入控制.
	/// </summary>
	/// <param name="dir">Dir.</param>
	void OnDirectionContrl(EDirection dir);

	/// <summary>
	/// 输入停止
	/// </summary>
	void OnDirectionStop();


}


public interface IInputFire
{
	void OnFire();
}