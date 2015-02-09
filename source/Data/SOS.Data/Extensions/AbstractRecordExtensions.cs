using SubSonic;

namespace SOS.Data.Extensions
{
	public static class AbstractRecordExtensions
	{
		public static void MarkDirty<T>(this AbstractRecord<T> ar)
			where T : AbstractRecord<T>, new()
		{
		}
	}
}
