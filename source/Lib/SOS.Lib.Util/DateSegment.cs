namespace SOS.Lib.Util
{
	//public class DateSegmentList : List<DateSegment>
	//{
	//    //public static DateSegmentList GetDateSegmentList(ITimePeriod timePeriod, DateSpan dateSpan)
	//    //{
	//    //    DateSegmentList result = new DateSegmentList();

	//    //    DateTime date = timePeriod.GetStartDate(dateSpan.StartDate);

	//    //    //check if at end
	//    //    while (date.CompareTo(dateSpan.EndDate) < 0) {

	//    //        DateSpan span = new DateSpan(date, timePeriod.GetEndDate(date));
	//    //        result.Add(timePeriod.GetDateSegment(span));

	//    //        //increment
	//    //        date = timePeriod.GetNextDate(date);
	//    //    }

	//    //    return result;
	//    //}

	//    //public void Sort(SortOrder order)
	//    //{
	//    //    if (order != SortOrder.Unspecified) {
	//    //        this.Sort(new DateSegmentComparer(order));
	//    //    }
	//    //}
	//}
	//public class DateSegment
	//{
	//    public DateSpan Span { get; set; }
	//    public string DateFormat { get; set; }

	//    public string GetDateText()
	//    {
	//        return
	//            Span.StartDate.ToString(DateFormat)
	//            + " - "
	//            + Span.EndDate.ToString(DateFormat);
	//    }
	//}

	//public class DateSegmentComparer : IComparer<DateSegment>
	//{
	//    public SortOrder Order { get; set; }

	//    public DateSegmentComparer(SortOrder order)
	//    {
	//        this.Order = order;
	//    }

	//    public int Compare(DateSegment x, DateSegment y)
	//    {
	//        switch (this.Order) {
	//            case SortOrder.Unspecified:
	//                return 0;
	//            case SortOrder.Ascending:
	//                return x.Span.StartDate.CompareTo(y.Span.StartDate);
	//            case SortOrder.Descending:
	//                return y.Span.StartDate.CompareTo(x.Span.StartDate);
	//            default:
	//                throw new NotSupportedException();
	//        }
	//    }
	//}
}