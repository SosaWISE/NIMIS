using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SOS.Data.GpsTracking;
using SOS.Data.GpsTracking.ControllerExtensions;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.GpsTracking;
using SOS.Lib.LaipacAPI.Commands;
using SOS.Lib.Util.Extensions;
using FnsGsAccountGeoFenceCircles = SOS.FunctionalServices.Models.GpsTracking.FnsGsAccountGeoFenceCircles;
using FnsGsAccountGeoFencePolygons = SOS.FunctionalServices.Models.GpsTracking.FnsGsAccountGeoFencePolygons;

namespace SOS.FunctionalServices
{
	public class GpsTrackingSerivces : IGpsTrackingSerivces
	{
		public IFnsResult<List<IFnsGsEventsView>> EventDeviceEventsGet(long lAccountID, DateTime? dStartDate, DateTime? dEndDate, int nPageSize, int nPageNumber = 1)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsGsEventsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing EventDeviceEventsGet"
			};

			/** Validate. */
			if (lAccountID == 0)
			{
				return new FnsResult<List<IFnsGsEventsView>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "EventDeviceEventsGet is missing lAccountID argument."
				};
			}
			if (dStartDate == null || dEndDate == null)
			{
				return new FnsResult<List<IFnsGsEventsView>>
					{
						Code = (int)ErrorCodes.GeneralMessage
						, Message = "EventDeviceEventsGet is passing invalide dates.  Please make sure that the dates passed are not null."
					};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				/** Initialize. */
				GS_EventsViewCollection oResulList = GpsTrackingDataContext.Instance.GS_EventsViews.GetDeviceEvents(lAccountID, dStartDate.Value.ToUniversalTime(), dEndDate.Value.ToUniversalTime(), nPageSize, nPageNumber);
				List<FnsGsEventsView> oResultValue = oResulList.Select(oItem => new FnsGsEventsView(oItem)).ToList();

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsGsEventsView>(oResultValue);
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsGsEventsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at EventDeviceEventsGet: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsGsEventsView>> EventDeviceEventsMasterGet(long lCMFID, long? lCustomerId, DateTime? dStartDate, DateTime? dEndDate, int nPageSize, int nPageNumber = 1)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsGsEventsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing EventDeviceEventsMasterGet"
			};

			/** Validate. */
			if (lCMFID == 0)
			{
				return new FnsResult<List<IFnsGsEventsView>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "EventDeviceEventsMasterGet is missing lCMFID argument."
				};
			}
			if (dStartDate == null || dEndDate == null)
			{
				return new FnsResult<List<IFnsGsEventsView>>
					{
						Code = (int)ErrorCodes.GeneralMessage
						, Message = "EventDeviceEventsMasterGet is passing invalide dates.  Please make sure that the dates passed are not null."
					};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				/** Initialize. */
				GS_EventsViewCollection oResulList = GpsTrackingDataContext.Instance.GS_EventsViews.GetDeviceEventsMaster(lCMFID, lCustomerId, dStartDate.Value.ToUniversalTime(), dEndDate.Value.ToUniversalTime(), nPageSize, nPageNumber);
				List<FnsGsEventsView> oResultValue = oResulList.Select(oItem => new FnsGsEventsView(oItem)).ToList();

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsGsEventsView>(oResultValue);
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsGsEventsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at EventDeviceEventsMasterGet: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsGsAccountGeoFencePoints> GeoPointCreateUpdate(long lGeoFenceID, long lAccountId, string sPlaceName, string sPlaceDescription, double sLattitude, double sLongitude, string sUserId)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsGsAccountGeoFencePoints>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoPointCreateUpdate"
			};

			/** Validate. */
			if (lAccountId == 0)
			{
				return new FnsResult<IFnsGsAccountGeoFencePoints>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "EventDeviceEventsGet is missing lAccountId argument."
				};
			}

			#endregion INITIALIZATION


			#region TRY

			try
			{
				/** Initialize. */
				GS_AccountGeoFencePointsView oResulItem = GpsTrackingDataContext.Instance.GS_AccountGeoFencePointsViews.GeoPointCreateUpdate(lGeoFenceID, lAccountId, sPlaceName, sPlaceDescription, sLattitude, sLongitude, sUserId);
				var oValue = new FnsGsAccountGeoFencePoints(oResulItem);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oValue;
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsGsAccountGeoFencePoints>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at EventDeviceEventsGet: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<bool> GeoPointDelete(long lGeoFenceID, long lAccountId, string sUserId)
		{
			/** Initialize. */
			#region INITIALIZATION

			const string METHOD_NAME = "GeoPointDelete";
			var oResult = new FnsResult<bool>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			/** Validate. */
			if (lGeoFenceID == 0)
			{
				return new FnsResult<bool>
				{
					Code = (int)ErrorCodes.GeneralMessage
					,
					Message = string.Format("{0} is missing lGeoFenceID argument.", METHOD_NAME)
				};
			}
			if (lAccountId == 0)
			{
				return new FnsResult<bool>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = string.Format("{0} is missing lAccountId argument.", METHOD_NAME)
				};
			}

			#endregion INITIALIZATION


			#region TRY

			try
			{
				/** Initialize. */
				GpsTrackingDataContext.Instance.GS_AccountGeoFencePointsViews.GeoPointDelete(lGeoFenceID, lAccountId, sUserId);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = true;
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<bool>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, oEx.Message)
					, Value = false
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsGsAccountGeoFencePoints> GeoPointRead(long lGeoFenceID, long lAccountId)
		{
			/** Initialize. */
			#region INITIALIZATION

			const string METHOD_NAME = "GeoPointDelete";
			var oResult = new FnsResult<IFnsGsAccountGeoFencePoints>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = string.Format("Initializing {0}", METHOD_NAME)
			};

			/** Validate. */
			if (lGeoFenceID == 0)
			{
				return new FnsResult<IFnsGsAccountGeoFencePoints>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = string.Format("{0} is missing lGeoFenceID argument.", METHOD_NAME)
				};
			}
			if (lAccountId == 0)
			{
				return new FnsResult<IFnsGsAccountGeoFencePoints>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = string.Format("{0} is missing lAccountId argument.", METHOD_NAME)
				};
			}

			#endregion INITIALIZATION


			#region TRY

			try
			{
				/** Initialize. */
				GS_AccountGeoFencePointsView oResulItem = GpsTrackingDataContext.Instance.GS_AccountGeoFencePointsViews.GeoPointRead(lGeoFenceID, lAccountId);

				/** Checl to see if something was found. */
				if (oResulItem == null)
				{
					oResult.Code = (int) ErrorCodes.GeneralMessage;
					oResult.Message = string.Format("No point found with id of '{0}'.", lGeoFenceID);
					oResult.Value = null;
				}
				else
				{
					var oValue = new FnsGsAccountGeoFencePoints(oResulItem);

					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = oValue;
				}
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsGsAccountGeoFencePoints>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<List<IFnsGsAccountGeoFencePolygons>> GeoPolygonUpdate(long lGeoFenceId, long lAccountId, List<IFnsGsAccountGeoFencePolygons> listOfCoords, string sUserId)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsGsAccountGeoFencePolygons>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoPolygonUpdate"
			};

			/** Validate. */
			long? lID = lGeoFenceId == 0 ? (long?)null : lGeoFenceId;
			if (lAccountId == 0)
			{
				return new FnsResult<List<IFnsGsAccountGeoFencePolygons>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoPolygonUpdate is missing lAccountId argument."
				};
			}

			/** Build the stream of long/latt pairs list. */
			var sbListOfCoords = new StringBuilder();
			foreach (var oItem in listOfCoords)
			{
				if (sbListOfCoords.Length != 0) sbListOfCoords.Append(",");
				sbListOfCoords.Append(oItem.Longitude);
				sbListOfCoords.Append(" ");
				sbListOfCoords.Append(oItem.Lattitude);
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				oResult.Code = (int)ErrorCodes.ExecutionInProg;
				oResult.Message = "Saving polygon";
				/** Save the poly. */
				var oGeoFence = GpsTrackingDataContext.Instance.GS_AccountGeoFences.SavePolygoneFence(lID, lAccountId, sbListOfCoords.ToString(), sUserId);
				if (oGeoFence != null)
				{
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					var oPointsList =
						GpsTrackingDataContext.Instance.GS_AccountGeoFencePolygons.GetByGeoFenceId(oGeoFence.GeoFenceID);
					
					/** Build list. */
					listOfCoords = oPointsList.Select(oPoint => new FnsGsAccountGeoFencePolygons
					            {
					                GeoFencePolygonID = oPoint.GeoFencePolygonID
									, GeoFenceId = oGeoFence.GeoFenceID
									, Sequence = oPoint.Sequence
									, Lattitude = oPoint.Lattitude
									, Longitude = oPoint.Longitude
									, CreatedOn = oPoint.CreatedOn
									, CreatedBy = oPoint.CreatedBy
					            }).Cast<IFnsGsAccountGeoFencePolygons>().ToList();
					oResult.Value = listOfCoords;
				}
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsGsAccountGeoFencePolygons>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at GeoPolygonUpdate: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}
		
		public IFnsResult<List<IFnsGsAccountGeoFencePolygons>> GeoPolygonRead(long lGeoFenceId)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsGsAccountGeoFencePolygons>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoPolygonRead"
			};

			/** Validate. */
			if (lGeoFenceId == 0)
			{
				return new FnsResult<List<IFnsGsAccountGeoFencePolygons>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoPolygonUpdate is missing lGeoFenceId argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				oResult.Code = (int)ErrorCodes.ExecutionInProg;
				oResult.Message = "Reading polygon";

				GS_AccountGeoFencePolygonCollection oPointsList =
					GpsTrackingDataContext.Instance.GS_AccountGeoFencePolygons.GetByGeoFenceId(lGeoFenceId);
					
				/** Build list. */
				List<IFnsGsAccountGeoFencePolygons> listOfCoords = oPointsList.Select(oPoint => new FnsGsAccountGeoFencePolygons
					        {
					            GeoFencePolygonID = oPoint.GeoFencePolygonID
								, GeoFenceId = lGeoFenceId
								, Sequence = oPoint.Sequence
								, Lattitude = oPoint.Lattitude
								, Longitude = oPoint.Longitude
								, CreatedOn = oPoint.CreatedOn
								, CreatedBy = oPoint.CreatedBy
					        }).Cast<IFnsGsAccountGeoFencePolygons>().ToList();

				/** Read the points of the polygon. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = listOfCoords;
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsGsAccountGeoFencePolygons>>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at GeoPolygonUpdate: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}

		public IFnsResult<List<IFnsGeoFencesView>> GeoFencesRead(long lAccountId)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsGeoFencesView>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoFencesRead"
			};

			/** Validate. */
			if (lAccountId == 0)
			{
				return new FnsResult<List<IFnsGeoFencesView>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFencesRead is missing lAccountId argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Initialize. */
				GS_AccountGeoFencesViewCollection oCol =
					GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.GetByAccountID(lAccountId);

				var oResultList = new List<FnsGeoFencesView>();// = oCol.Select(oFenceItem => new FnsGeoFencesView(oFenceItem)).ToList();
				foreach (var oFence in oCol)
				{
					/** Initialize. */
					List<FnsGsAccountGeoFencePolygons> oPolyPointsList = null;
					// ** Check that this is a polygon. 
					if (oFence.GeoFenceTypeId.Equals(GS_AccountGeoFenceType.MetaData.PolygonID))
					{
						/** Add Polygon Points list to the fence item. */
						IFnsResult<List<IFnsGsAccountGeoFencePolygons>> oPointsListResult = GeoPolygonRead(oFence.GeoFenceID);
						if (oPointsListResult.Code != (int) ErrorCodes.Success) break;

						/** Bind points to list. */
						var oResultListValue = (List<IFnsGsAccountGeoFencePolygons>)oPointsListResult.GetValue();
						oPolyPointsList = new List<FnsGsAccountGeoFencePolygons>();

						foreach (IFnsGsAccountGeoFencePolygons oItem in oResultListValue)
						{
							oPolyPointsList.Add(new FnsGsAccountGeoFencePolygons
							    {
									GeoFenceId = oItem.GeoFenceId
									, GeoFencePolygonID = oItem.GeoFencePolygonID
									, Sequence = oItem.Sequence
									, Lattitude = oItem.Lattitude
									, Longitude = oItem.Longitude
									, CreatedOn = oItem.CreatedOn
									, CreatedBy = oItem.CreatedBy
							    });
						}
					}

					/** Create Fence Item. */
					FnsGeoFencesView oFenceItem = (oPolyPointsList == null) 
						? new FnsGeoFencesView(oFence, null) 
						: new FnsGeoFencesView(oFence, new List<IFnsGsAccountGeoFencePolygons>(oPolyPointsList));

					oResultList.Add(oFenceItem);
				}

				/** Get result value and return. */
				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsGeoFencesView>(oResultList);
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				return new FnsResult<List<IFnsGeoFencesView>>
				{
				    Code = (int)ErrorCodes.GeneralError
					, Message = string.Format("The following exception was thrown on GeoFencesRead: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT

		}

		public IFnsResult<List<IFnsGeoFencesView>> GeoFencesByCMFID(long lCMFID, long? lCustomerId)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsGeoFencesView>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoFencesByCMFID"
			};

			/** Validate. */
			if (lCMFID == 0)
			{
				return new FnsResult<List<IFnsGeoFencesView>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFencesByCMFID is missing lCMFID argument."
				};
			}
			if (lCustomerId == 0)
			{
				return new FnsResult<List<IFnsGeoFencesView>>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFencesByCMFID is missing lCustomerId argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Initialize. */
				GS_AccountGeoFencesViewCollection oCol =
					GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.GetByCMFID(lCMFID, lCustomerId);

				var oResultList = new List<FnsGeoFencesView>();// = oCol.Select(oFenceItem => new FnsGeoFencesView(oFenceItem)).ToList();
				foreach (var oFence in oCol)
				{
					/** Initialize. */
					List<FnsGsAccountGeoFencePolygons> oPolyPointsList = null;
					// ** Check that this is a polygon. 
					if (oFence.GeoFenceTypeId.Equals(GS_AccountGeoFenceType.MetaData.PolygonID))
					{
						/** Add Polygon Points list to the fence item. */
						IFnsResult<List<IFnsGsAccountGeoFencePolygons>> oPointsListResult = GeoPolygonRead(oFence.GeoFenceID);
						if (oPointsListResult.Code != (int)ErrorCodes.Success) break;

						/** Bind points to list. */
						var oResultListValue = (List<IFnsGsAccountGeoFencePolygons>)oPointsListResult.GetValue();
						oPolyPointsList = new List<FnsGsAccountGeoFencePolygons>();

						foreach (IFnsGsAccountGeoFencePolygons oItem in oResultListValue)
						{
							oPolyPointsList.Add(new FnsGsAccountGeoFencePolygons
							{
								GeoFenceId = oItem.GeoFenceId
								, GeoFencePolygonID = oItem.GeoFencePolygonID
								, Sequence = oItem.Sequence
								, Lattitude = oItem.Lattitude
								, Longitude = oItem.Longitude
								, CreatedOn = oItem.CreatedOn
								, CreatedBy = oItem.CreatedBy
							});
						}
					}

					/** Create Fence Item. */
					FnsGeoFencesView oFenceItem = (oPolyPointsList == null)
						? new FnsGeoFencesView(oFence, null)
						: new FnsGeoFencesView(oFence, new List<IFnsGsAccountGeoFencePolygons>(oPolyPointsList));

					oResultList.Add(oFenceItem);
				}

				/** Get result value and return. */
				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = new List<IFnsGeoFencesView>(oResultList);
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				return new FnsResult<List<IFnsGeoFencesView>>
				{
					Code = (int)ErrorCodes.GeneralError
					,
					Message = string.Format("The following exception was thrown on GeoFencesByCMFID: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT

		}


		#region CRUD Circle Fences

		public IFnsResult<IFnsGsAccountGeoFenceCircles> GeoFenceCircleUpdate(long lGeoFenceId, double dRadius, double dCenterLattitude, double dCenterLongitude, string sModifiedBy)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsGsAccountGeoFenceCircles>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoFencesRead"
			};

			/** Validate. */
			if (lGeoFenceId == 0)
			{
				return new FnsResult<IFnsGsAccountGeoFenceCircles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceCircleUpdate is missing lGeoFenceId argument."
				};
			}
			if (Math.Abs(dRadius - 0) < 1E-10)
			{
				return new FnsResult<IFnsGsAccountGeoFenceCircles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceCircleUpdate is missing dRadius argument."
				};
			}
			if (Math.Abs(dCenterLattitude - 0) < 1E-10)
			{
				return new FnsResult<IFnsGsAccountGeoFenceCircles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceCircleUpdate is missing dCenterLattitude argument."
				};
			}
			if (Math.Abs(dCenterLongitude - 0) < 1E-10)
			{
				return new FnsResult<IFnsGsAccountGeoFenceCircles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceCircleUpdate is missing dCenterLongitude argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				GS_AccountGeoFence oFence = GpsTrackingDataContext.Instance.GS_AccountGeoFences.LoadByPrimaryKey(lGeoFenceId);
				GS_AccountGeoFenceCircle oGsCircle = GpsTrackingDataContext.Instance.GS_AccountGeoFenceCircles.Save(lGeoFenceId,
																													oFence.AccountId,
				                                                                                                    dRadius,
				                                                                                                    dCenterLattitude,
				                                                                                                    dCenterLongitude,
				                                                                                                    sModifiedBy);
				var oFnsGsGeoFenceCircle = new FnsGsAccountGeoFenceCircles(oGsCircle);

				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Successful";
				oResult.Value = oFnsGsGeoFenceCircle;
			}
			
			#endregion TRY

			#region CATCH
			
			catch (Exception oEx)
			{
				return new FnsResult<IFnsGsAccountGeoFenceCircles>
				{
				    Code = (int)ErrorCodes.GeneralError
					, Message = string.Format("The following exception was thrown on GeoFenceCircleUpdate: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}

		#endregion CRUD Circle Fences

		#region CRUD Rectangle Fences

		public IFnsResult<IFnsGsAccountGeoFenceRectangles> GeoFenceRectangleCreate(long lAccountId, string sItemId, string sReportMode, string geoFenceName, string geoFenceDescription, double dMaxLattitude, double dMaxLongitude, double dMinLattitude, double dMinLongitude, short? zZoomLevel, string sModifiedBy)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsGsAccountGeoFenceRectangles>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoFencesRead"
			};

			/** Validate. */
			if (Math.Abs(dMaxLattitude - 0) < 1E-10 || Math.Abs(dMinLattitude - 0) < 1E-10 || Math.Abs(dMinLongitude - 0) < 1E-10 || Math.Abs(dMaxLongitude - 0) < 1E-10)
			{
				return new FnsResult<IFnsGsAccountGeoFenceRectangles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceRectangleCreate is missing a coordinate or one coordinate has a value of zero (0) argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Initialize. */
				GS_AccountGeoFencesView oRectangleView;
				/** Send command to device. */
				switch (sItemId)
				{
					case "S911BRC-CE":
					case "S911BRC-HC":
						oRectangleView = Lib.LaipacAPI.ClientApi.Instance.CreateFence(lAccountId, sReportMode, geoFenceName, geoFenceDescription, dMaxLattitude, dMaxLongitude
							, dMinLattitude, dMinLongitude, zZoomLevel, sModifiedBy);
						break;
					default:
						/** Save information. */
						var oRectangle = GpsTrackingDataContext.Instance.GS_AccountGeoFenceRectangles.Create(lAccountId, sReportMode, geoFenceName, geoFenceDescription, dMaxLattitude,
																							  dMinLongitude, dMinLattitude, dMaxLongitude, zZoomLevel,
																							  sModifiedBy);
						oRectangleView = GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.LoadByPrimaryKey(oRectangle.GeoFenceID);
						break;
				}


				var oFnsGsGeoFenceRectangle = new FnsGsAccountGeoFenceRectangles(oRectangleView);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Successful";
				oResult.Value = oFnsGsGeoFenceRectangle;
			}

			#endregion TRY

			#region CATCH

			catch (Exception oEx)
			{
				return new FnsResult<IFnsGsAccountGeoFenceRectangles>
				{
					Code = (int)ErrorCodes.GeneralError
					, Message = string.Format("The following exception was thrown on GeoFenceRectangleCreate: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}

		public IFnsResult<IFnsGsAccountGeoFenceRectangles> GeoFenceRectangleUpdate(long lGeoFenceID, string sItemId, string sReportMode, string geoFenceName, string geoFenceDescription, double dMaxLattitude, double dMaxLongitude, double dMinLattitude, double dMinLongitude, short? zZoomLevel, string sModifiedBy)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsGsAccountGeoFenceRectangles>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoFencesRead"
			};

			/** Validate. */
			if (lGeoFenceID == 0)
			{
				return new FnsResult<IFnsGsAccountGeoFenceRectangles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceRectangleUpdate is missing lGeoFenceID argument."
				};
			}
			if (dMaxLattitude.IsZero() || dMinLattitude.IsZero() || dMinLongitude.IsZero() || dMaxLongitude.IsZero())
			{
				return new FnsResult<IFnsGsAccountGeoFenceRectangles>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeoFenceRectangleUpdate is missing a coordinate or one coordinate has a value of zero (0) argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Initialize. */
				GS_AccountGeoFence oFence = GpsTrackingDataContext.Instance.GS_AccountGeoFences.LoadByPrimaryKey(lGeoFenceID);
				GS_AccountGeoFencesView oRectangleView;

				/** Send command to device. */
				switch(sItemId)
				{
					case "S911BRC-CE":
					case "S911BRC-HC":
						oRectangleView = Lib.LaipacAPI.ClientApi.Instance.UpdateFence(oFence.GeoFenceID, oFence.AccountId, sReportMode, geoFenceName, geoFenceDescription, dMaxLattitude, dMaxLongitude
							, dMinLattitude, dMinLongitude, zZoomLevel, sModifiedBy);
						break;
					default:
						/** Save information. */
						GpsTrackingDataContext.Instance.GS_AccountGeoFenceRectangles.Update(lGeoFenceID, oFence.AccountId, sReportMode, geoFenceName, geoFenceDescription, dMaxLattitude,
																							  dMinLongitude, dMinLattitude, dMaxLongitude, zZoomLevel,
																							  sModifiedBy);
						oRectangleView = GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.LoadByPrimaryKey(lGeoFenceID);
						break;
				}

				var oFnsGsGeoFenceRectangle = new FnsGsAccountGeoFenceRectangles(oRectangleView);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Successful";
				oResult.Value = oFnsGsGeoFenceRectangle;
			}

			#endregion TRY

			#region CATCH

			catch (Exception oEx)
			{
				return new FnsResult<IFnsGsAccountGeoFenceRectangles>
				{
					Code = (int)ErrorCodes.GeneralError
					, Message = string.Format("The following exception was thrown on GeoFenceRectangleUpdate: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}

        public IFnsResult<bool> GeoFenceRectangleDelete(long lGeoFenceID, string sModifiedBy)
		{
			#region INITIALIZE
            var oResult = new FnsResult<bool>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeoFencesRead"
			};

            /** Validate. */
            if(lGeoFenceID == 0)
            {
                return new FnsResult<bool>
                {
                    Code = (int)ErrorCodes.GeneralMessage
                    , Message = "GeoFenceRectangleDelete is missing the geofence ID argument."
                };
            }

			#endregion INITIALIZE

            #region TRY
			try
			{
				/** Initialize. */
			    GS_AccountGeoFenceRectangle oFence = GpsTrackingDataContext.Instance.GS_AccountGeoFenceRectangles.Delete(lGeoFenceID,
			        sModifiedBy);

				/** Successfull delete. */
			    oResult.Code = (int) ErrorCodes.Success;
			    oResult.Message = "Success";
			    oResult.Value = true;
			}

			#endregion TRY
            
            #region CATCH

            catch (Exception oEx)
            {
                return new FnsResult<bool>
                {
                    Code = (int)ErrorCodes.GeneralError
                    , Message = string.Format("The following exception was thrown on GeoFenceRectangleDelete: {0}", oEx.Message)
                };
            }

            #endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}

		#endregion CRUD Rectangle Fences

		#region Request GeoFences From Device

		const int _NUM_OF_SQLPINGS = 6;
		public IFnsResult<List<IFnsGsAccountGeoFenceRectangles>> GetLaipacS911GeoFences(long lAccountID, string sUsername)
		{
			return (IFnsResult<List<IFnsGsAccountGeoFenceRectangles>>) GenericServiceWrapper("GetLaipacS911GeoFences", () =>
			{
				/** Initialize. */
				const int NUM_OF_FENCES = 5;
				var result = new FnsResult<List<IFnsGsAccountGeoFenceRectangles>>();
				//MS_Account msAccount = SosCrmDataContext.Instance.MS_Accounts.GetByLaipacUnitID(lUnitID.ToString(CultureInfo.InvariantCulture));
				GS_Account msAccount = SosCrmDataContext.Instance.GS_Accounts.LoadByPrimaryKey(lAccountID);
				
				/** Call all fences. */
				for (short i = 0; i < NUM_OF_FENCES; i++)
				{
					/** Push to device request for geofences. */
					var commandRequest = new EAVGOFRequest(msAccount.GpsWatchPassword, Convert.ToInt64(msAccount.GpsWatchUnitID), i);
					commandRequest.QueueRequest(msAccount.AccountID);
				}

				/** Loop through the requests to find the response. */
				int tries = 0;
				var resultListValue = new List<IFnsGsAccountGeoFenceRectangles> ();
				var deviceList = new List<LP_CommandMessageEAVRSP4>();
				do
				{
					tries++;
					/** Wait for 5 seconds. */
					Thread.Sleep(5000); // Wait 5 seconds before checking for the response of the device.

					/** Check if the device has responded. */
					LP_CommandMessageEAVRSP4Collection responsCol =
						GpsTrackingDataContext.Instance.LP_CommandMessageEAVRSP4s.ProcessByUnitID(Convert.ToInt64(msAccount.GpsWatchUnitID));

					/** Build your list. */
					deviceList.AddRange(responsCol);

					/** Check to see if there are all the fences present. */
					if (deviceList.Count == NUM_OF_FENCES) break;

					/** Check to make sure we don't exceed the number of tries. */
					if (tries >= _NUM_OF_SQLPINGS) break;
				} while (resultListValue.Count < NUM_OF_FENCES);

				/** build the response. */
				if (deviceList.Count > 0)
				{
					resultListValue.AddRange(from eavrsp4 in deviceList where !eavrsp4.LattitudeI1.IsZero() || !eavrsp4.LongitudeI1.IsZero() select GpsTrackingDataContext.Instance.GS_AccountGeoFencesViews.BindLaipacFence(eavrsp4.CommandMessageID, msAccount.AccountID) into gsGeoFenceRectItem select new FnsGsAccountGeoFenceRectangles(gsGeoFenceRectItem));
					result.Code = (int) ErrorCodes.Success;
					result.Message = "Success";
					result.Value = resultListValue;
				}
				else
				{
					result.Code = (int)ErrorCodes.GPSDeviceNoResponse;
					result.Message = "No Geo Fences were returned by the device.";
				}

				/** Return result. */
					return result;
			});
		}

		#endregion Request GeoFences From Device

		#region Request Device Location

		public IFnsResult<IFnsGsAccountGeoFencePoints> GetLaipacS911CurrentLocation(long lAccountID, string sUsername)
		{
			/** Initializing. */
			const string METHOD_NAME = "GetLaipacS911CurrentLocation";
			return (IFnsResult<IFnsGsAccountGeoFencePoints>)GenericServiceWrapper(METHOD_NAME, () =>
			{ /** Initialize. */
				var result = new FnsResult<IFnsGsAccountGeoFencePoints>
				             	{
				             		Code = (int)ErrorCodes.Initializing,
									Message = string.Format("Initializing {0}", METHOD_NAME)
				             	};
				GS_Account msAccount = SosCrmDataContext.Instance.GS_Accounts.LoadByPrimaryKey(lAccountID);

				/** Push request to Device. */
				var commandRequest = new AVREQRequest(msAccount.GpsWatchPassword, Convert.ToInt64(msAccount.GpsWatchUnitID), AVREQCommandIDs.RequestCurrentPosition);
				LP_Request queueRequestItem = commandRequest.QueueRequest(sUsername, lAccountID);

				Console.WriteLine("LP_Request: ReqID: {0} | CommandMessageID: {1}", queueRequestItem.RequestID, queueRequestItem.CommandMessageId);
				
				/** Loop through responses. */
				LP_CommandMessageAVRMCsView response;
				int tries = 0;
				do
				{
					tries++;
					/** Wait for 5 seconds. */
					Thread.Sleep(5000); // Wait 5 seconds before checking for the response of the device.

					/** Try to process. */
					response = GpsTrackingDataContext.Instance.LP_CommandMessageAVRMCsViews.ProcessByUnitIDAndReqCommandID(Convert.ToInt64(msAccount.GpsWatchUnitID)
									, commandRequest.CommandMessage.CommandMessageID
									, LP_EventCode.MetaData.ResponseToRequestCurrentPositionID);

					/** Check the result.  */
					if (response != null)
					{
						var responseValue = new FnsGsAccountGeoFencePoints(response);

						/** Check that there is a location for this request. */
						if (responseValue.Lattitude.IsZero() && responseValue.Longitude.IsZero())
						{
							result = new FnsResult<IFnsGsAccountGeoFencePoints> { Code = (int)ErrorCodes.GPSDeviceNoLocation, Message = "Device has not been able to determine its location.", Value = responseValue };
							break;
						}

						result = new FnsResult<IFnsGsAccountGeoFencePoints> { Code = (int) ErrorCodes.Success, Message = "Successfull", Value = responseValue };
						break;
					}

					/** Check to make sure we don't exceed the number of tries. */
					if (tries >= _NUM_OF_SQLPINGS) break;
				} while (true);

				/** Check result. */
				if (response == null)
					result = new FnsResult<IFnsGsAccountGeoFencePoints>
					         	{
					         		Code = (int)ErrorCodes.GeneralError,
									Message = string.Format("Device did not responde.")
					         	};

				/** Return result. */
				return result;
			});
		}

		#endregion Request Device Location

		#region CRUD EventType

		public IFnsResult<List<IFnsGsEventTypeView>> EventTypeReadAll(string eventTypeID, string eventType)
		{
			#region INITIALIZE
			var oResult = new FnsResult<List<IFnsGsEventTypeView>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = "Initializing EventTypeReadAll"
			};

			/** Validate. */
			if (string.IsNullOrEmpty(eventTypeID)) eventTypeID = null;
			if (string.IsNullOrEmpty(eventType)) eventType = null;

			#endregion INITIALIZE

			#region TRY

			try
			{
				GS_EventTypesViewCollection oCol = GpsTrackingDataContext.Instance.GS_EventTypesViews.ReadAll(eventTypeID, eventType);

				var fnsListValue = oCol.Select(eventTypeItem => new FnsGsEventTypeView(eventTypeItem)).Cast<IFnsGsEventTypeView>().ToList();

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = fnsListValue;
			}

			#endregion TRY


			#region CATCH

			catch (Exception oEx)
			{
				return new FnsResult<List<IFnsGsEventTypeView>>
				{
					Code = (int)ErrorCodes.GeneralError
					, Message = string.Format("The following exception was thrown on EventTypeReadAll: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			#region RETURN RESULT

			return oResult;

			#endregion RETURN RESULT
		}

		#endregion CRUD EventType

		#region Reports

		public IFnsResult<List<IFnsGsDeviceEvents>> ReportEvents(long customerMasterFileId, long? deviceId, string eventTypeId, long? locationID, DateTime startDate, DateTime endDate, string username, int pageSize, int pageNumber)
		{
			#region Initialize
			var oResult = new FnsResult<List<IFnsGsDeviceEvents>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing ReportEvents"
				
			};

			/** Init Arguments. */
			if (string.IsNullOrEmpty(eventTypeId)) eventTypeId = null;
			#endregion Initialize

			#region Try

			try
			{
				GS_EventsViewCollection oCol = GpsTrackingDataContext.Instance.GS_EventsViews.EventsReport(customerMasterFileId,
					deviceId, eventTypeId, locationID, startDate, endDate, pageSize, pageNumber);

				var eventsList = new List<IFnsGsDeviceEvents>();

				foreach (var eventItem in oCol)
				{
					eventsList.Add(new FnsGsDeviceEvents(eventItem));
				}

				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = eventsList;
			}
			#endregion Try

			#region Catch
			catch (Exception oEx)
			{
				return new FnsResult<List<IFnsGsDeviceEvents>>
				{
					Code = (int)ErrorCodes.GeneralError
					, Message = string.Format("The following exception was thrown on ReportEvents: {0}", oEx.Message)
				};
			}
			#endregion Catch

			#region Return Result

			return oResult;

			#endregion Return Result

		}

		#endregion Reports
		
		#region GenericServiceWrapper
		private object GenericServiceWrapper<T>(string methodName, Func<IFnsResult<T>> action)
		{
			/** Initialize. */
			#region INITIALIZATION

			// ReSharper disable RedundantAssignment
			var oResult = new FnsResult<T>
			{
				Code = (int)ErrorCodes.GeneralMessage
				,
				Message = string.Format("Initializing {0}", methodName)
			};
			// ReSharper restore RedundantAssignment

			#endregion INITIALIZATION

			#region TRY
			try
			{
				return action();
			}
			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				var sMsg = string.Format("Exception thrown at ExecuteSentence: {0}", oEx.Message);
				oResult = new FnsResult<T>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = sMsg
				};
			}
			#endregion CATCH

			/** Call action and return result. */
			return oResult;
		}
		#endregion GenericServiceWrapper
	}


}