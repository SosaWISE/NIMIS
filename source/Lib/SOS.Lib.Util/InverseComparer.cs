using System.Collections;

namespace SOS.Lib.Util
{
	public class InverseComparer : IComparer
	{
		public InverseComparer(IComparer standardComparer)
		{
			StandardComparer = standardComparer;
		}

		public IComparer StandardComparer { get; set; }

		#region IComparer Members

		public int Compare(object x, object y)
		{
			if (StandardComparer != null)
			{
				int result = StandardComparer.Compare(x, y);

				if (result < 0)
				{
					result = 1;
				}
				else if (result > 0)
				{
					result = -1;
				}

				return result;
			}
			else
			{
				return 0;
			}
		}

		#endregion
	}
}