using WabiSabiMonitor.Utils.Helpers;
using WabiSabiMonitor.Utils.Hwi.Exceptions;
using WabiSabiMonitor.Utils.Models;

namespace WabiSabiMonitor.Utils.Extensions;

public static class ExceptionExtensions
{
	public static string ToTypeMessageString(this Exception ex)
	{
		var trimmed = Guard.Correct(ex.Message);

		if (trimmed.Length == 0)
		{
			if (ex is HwiException hwiEx)
			{
				return $"{hwiEx.GetType().Name}: {hwiEx.ErrorCode}";
			}
			return ex.GetType().Name;
		}
		else
		{
			return $"{ex.GetType().Name}: {ex.Message}";
		}
	}

	public static SerializableException ToSerializableException(this Exception ex)
	{
		return new SerializableException(ex);
	}
}
