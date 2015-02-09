using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
    public class FnsConnextCombinedMonthlySalesDetails : IFnsConnextCombinedMonthlySalesDetails
    {
        #region .ctor

        public FnsConnextCombinedMonthlySalesDetails()
        {
            OfficeStats = new List<IFnsConnextMonthlySalesDetails>();
            RepStats = new List<IFnsConnextMonthlySalesDetails>();
        }

        //public FnsConnextCombinedMonthlySalesDetailsByUserID(RU_UsersGetDetailedStatisticsConnextView officeStats, RU_UsersGetDetailedStatisticsConnextView repStats)
        //{
        //    OfficeStats = new List<IFnsConnextMonthlySalesDetailsByUserID>();
        //    foreach (RU_UsersGetDetailedStatisticsConnextView offItem in officeStats)
        //    {
        //        OfficeStats.Add(new FnsConnextMonthlySalesDetailsByUserID(offItem));
        //    }

        //    RepStats = new List<IFnsConnextMonthlySalesDetailsByUserID>();
        //    foreach (RU_UsersGetDetailedStatisticsConnextView repItem in repStats)
        //    {
        //        RepStats.Add(new FnsConnextMonthlySalesDetailsByUserID(repItem));
        //    }
        //}

        #endregion .ctor

        #region Properties

        public List<IFnsConnextMonthlySalesDetails> OfficeStats { get; private set; }

        public List<IFnsConnextMonthlySalesDetails> RepStats { get; private set; }
        #endregion Properties
    }
}
