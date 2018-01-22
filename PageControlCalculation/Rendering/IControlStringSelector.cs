
namespace PageControlCalculation
{
	public interface IControlStringSelector
	{
		string RenderPreviousControl(int? index);
		string RenderGapMarker();
		string RenderOrdinalControl(int index, bool isSelected);
		string RenderNextControl(int? index);
	}
}
