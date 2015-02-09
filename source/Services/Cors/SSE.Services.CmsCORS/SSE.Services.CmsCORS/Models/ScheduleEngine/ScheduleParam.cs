using System;
namespace SSE.Services.CmsCORS.Models.ScheduleEngine
{
	public class ScheduleParam:JsonParamBase
	{
        public DateTime AppointmentDateStart { get; set; }
        public DateTime AppointmentDateEnd { get; set; }


        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }


	}
}