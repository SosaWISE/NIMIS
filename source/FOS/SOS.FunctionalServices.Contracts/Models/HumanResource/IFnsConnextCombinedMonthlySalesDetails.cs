using System.Collections.Generic;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{

    public interface IFnsConnextCombinedMonthlySalesDetails
    {
        List<IFnsConnextMonthlySalesDetails> OfficeStats { get; }

        List<IFnsConnextMonthlySalesDetails> RepStats { get;  }
    }
}