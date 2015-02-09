using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.ScheduleEngine;
using System.Collections.Generic;

namespace SOS.FunctionalServices.Models.ScheduleEngine
{
	public class FnsSeScheduleBlock : IFnsSeScheduleBlock
	{
		#region .ctor
        public FnsSeScheduleBlock(SE_ScheduleBlock scheduleBlock)
		{
                BlockID = scheduleBlock.BlockID;
                Block = scheduleBlock.Block;
                ZipCode = scheduleBlock.ZipCode;
                MaxRadius = scheduleBlock.MaxRadius;
                Distance = scheduleBlock.Distance;
                AvailableSlots = scheduleBlock.AvailableSlots;
                StartTime = scheduleBlock.StartTime;
                EndTime =  scheduleBlock.EndTime;
                TechnicianId = scheduleBlock.TechnicianId;
                CurrentTicketId = scheduleBlock.CurrentTicketId;
                Color = scheduleBlock.Color;
                IsBlocked = scheduleBlock.IsBlocked;
                IsRed = scheduleBlock.IsRed;
                IsTechConfirmed = scheduleBlock.IsTechConfirmed;
                DateTechConfirmed = scheduleBlock.DateTechConfirmed;
		}

        public FnsSeScheduleBlock(SE_ScheduleBlocksView scheduleBlock)
        {
            BlockID = scheduleBlock.BlockID;
            Block = scheduleBlock.Block;
            ZipCode = scheduleBlock.ZipCode;
            MaxRadius = scheduleBlock.MaxRadius;
            Distance = scheduleBlock.Distance;
            AvailableSlots = scheduleBlock.AvailableSlots;
            StartTime = scheduleBlock.StartTime;
            EndTime = scheduleBlock.EndTime;
            TechnicianId = scheduleBlock.TechnicianId;
            TechnicianName = scheduleBlock.TechnicianName;
            CurrentTicketId = scheduleBlock.CurrentTicketId;
            BlockLatitude = scheduleBlock.BlockLatitude;
            BlockLongitude = scheduleBlock.BlockLongitude;
            TicketLatitude = scheduleBlock.TicketLatitude;
            TicketLongitude = scheduleBlock.TicketLongitude;
            IsRed = scheduleBlock.IsRed;
            IsTechConfirmed = scheduleBlock.IsTechConfirmed;
            DateTechConfirmed = scheduleBlock.DateTechConfirmed;
            Color = scheduleBlock.Color;
            IsBlocked = scheduleBlock.IsBlocked;
            NoOfTickets = scheduleBlock.NoOfTickets;
            
        }


        public FnsSeScheduleBlock(SE_TechnicianScheduleBlocksView scheduleBlock)
        {
            BlockID = scheduleBlock.BlockID;
            Block = scheduleBlock.Block;
            ZipCode = scheduleBlock.ZipCode;
            MaxRadius = scheduleBlock.MaxRadius;
            Distance = scheduleBlock.Distance;
            AvailableSlots = scheduleBlock.AvailableSlots;
            StartTime = scheduleBlock.StartTime;
            EndTime = scheduleBlock.EndTime;
            TechnicianId = scheduleBlock.TechnicianId;
            TechnicianName = scheduleBlock.TechnicianName;
            CurrentTicketId = scheduleBlock.CurrentTicketId;
            IsRed = scheduleBlock.IsRed;
            IsTechConfirmed = scheduleBlock.IsTechConfirmed;
            DateTechConfirmed = scheduleBlock.DateTechConfirmed;
            Color = scheduleBlock.Color;
            IsBlocked = scheduleBlock.IsBlocked;
            NoOfTickets = scheduleBlock.NoOfTickets;

        }


        public FnsSeScheduleBlock(SE_ScheduleBlocksView scheduleBlock, List<IFnsSeTicket> ticketList)
        {
            BlockID = scheduleBlock.BlockID;
            Block = scheduleBlock.Block;
            ZipCode = scheduleBlock.ZipCode;
            MaxRadius = scheduleBlock.MaxRadius;
            Distance = scheduleBlock.Distance;
            AvailableSlots = scheduleBlock.AvailableSlots;
            StartTime = scheduleBlock.StartTime;
            EndTime = scheduleBlock.EndTime;
            TechnicianId = scheduleBlock.TechnicianId;
            TechnicianName = scheduleBlock.TechnicianName;
            CurrentTicketId = scheduleBlock.CurrentTicketId;
            BlockLatitude = scheduleBlock.BlockLatitude;
            BlockLongitude = scheduleBlock.BlockLongitude;
            TicketLatitude = scheduleBlock.TicketLatitude;
            TicketLongitude = scheduleBlock.TicketLongitude;
            IsRed = scheduleBlock.IsRed;
            IsTechConfirmed = scheduleBlock.IsTechConfirmed;
            DateTechConfirmed = scheduleBlock.DateTechConfirmed;
            Color = scheduleBlock.Color;
            IsBlocked = scheduleBlock.IsBlocked;
            NoOfTickets = scheduleBlock.NoOfTickets;
            TicketList = ticketList;
        }






        public FnsSeScheduleBlock(long blockId,
                                  string block  ,
                                  string zipCode,
                                  double? maxRadius,
                                  double? distance,
                                  DateTime? startTime,
                                  DateTime? endTime,
                                  int? availableSlots,
                                  string technicianId,
                                  bool? isTechConfirmed,
                                  long? currentTicketId,
                                  DateTime? dateTechConfirmed,
                                  bool? isRed,
                                  string color,
                                  bool isBlocked
            )
        {
            BlockID = blockId;
            Block = block;
            ZipCode = zipCode;
            MaxRadius = maxRadius;
            Distance = distance;
            AvailableSlots = availableSlots;
            StartTime = startTime;
            EndTime = endTime;
            TechnicianId = technicianId;
            CurrentTicketId = currentTicketId;
            IsTechConfirmed = isTechConfirmed;
            DateTechConfirmed = dateTechConfirmed;
            IsRed = isRed;
            Color = color;
            IsBlocked = isBlocked;
        }


		#endregion .ctor

		#region Properties

        public long BlockID { get; set; }

        public string Block { get; set; }


        public string ZipCode { get; set; }


        public double? MaxRadius { get; set; }


        public double? Distance { get; set; }

        public int? AvailableSlots { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string TechnicianId { get; set; }

        public string TechnicianName { get; set; }


        public bool? IsTechConfirmed { get; set; }

        public DateTime? DateTechConfirmed { get; set; }

        public double? BlockLatitude { get; set; }
        public double? BlockLongitude { get; set; }
        public double? TicketLongitude { get; set; }
        public double? TicketLatitude { get; set; }

        public long? CurrentTicketId { get; set; }

        public bool? IsRed { get; set; }

        public string Color { get; set; }

        public bool IsBlocked { get; set; }

        public int? NoOfTickets { get; set; }

        public List<IFnsSeTicket> TicketList { get;  set; }

		#endregion Properties
	}
}
