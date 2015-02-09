


using System;
using SubSonic;
using SOS.Data;

namespace NXS.Data.FileStore
{
	public partial class FileStoreDataContext
	{
		#region Internal Instance
		
		private static FileStoreDataContext _contextInstance;
		private static readonly object _syncRootContextInstance = new object();
		
		public static FileStoreDataContext Instance
		{
			get
			{
				if (_contextInstance == null)
				{
					lock (_syncRootContextInstance)
					{
						if (_contextInstance == null)
						{
							_contextInstance = new FileStoreDataContext();
						}
					}
				}
				return _contextInstance;
			}
		}
		
		#endregion // Internal Instance
		
		#region Controllers Properties

		FS_FileController _FS_Files;
		public FS_FileController FS_Files
		{
			get
			{
				if (_FS_Files == null) _FS_Files = new FS_FileController();
				return _FS_Files;
			}
		}

		FS_FileSourceController _FS_FileSources;
		public FS_FileSourceController FS_FileSources
		{
			get
			{
				if (_FS_FileSources == null) _FS_FileSources = new FS_FileSourceController();
				return _FS_FileSources;
			}
		}

		#endregion //Controllers Properties
		
		#region View Controllers Properties

		#endregion //View Controllers Properties
	}
	
	#region Controllers
	
	public class FS_FileController : BaseTableController<FS_File, FS_FileCollection> { }
	public class FS_FileSourceController : BaseTableController<FS_FileSource, FS_FileSourceCollection> { }

	#endregion //Controllers
	
	#region View Controllers
	

	#endregion //View Controllers
}
