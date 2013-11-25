using System;
using System.Collections;

public class UniqueNumber
{
	////template< auint _val, unsigned int _count >
	////struct POW
	////{
	////	enum : auint64 { value = _val * POW<_val,_count-1>::value };
	////};
	////
	////	template< unsigned int _val >
	////struct POW<_val, 0>
	////{
	////	enum : auint64 { value = 1 };
	////};

	////template< unsigned int _size >
	////struct MAX_NUM
	////{
	////	enum : auint64 { value = POW< 256, _size >::value };
	////};

	//struct stock
	//{
	//    public UInt32 day_time;
	//    public UInt32 chunk;
	//};


	////시작 년월일을 2010년 1월 1일
	//const Int32 start_year = 2010;
	//const Int32 start_month = 1;
	//const Int32 start_day = 1;

	////10분마다 day_time 을 1씩 올린다 count_up 값만 수정하면 더 촘촘하게 발행할수 있다. 대신 년이 쭐어든다.
	////10분동안에 4294967296 개를 발행가능. 81715년동안 쓸수있다.

	//const UInt64 countup = 10;
	//const UInt64 countup_adv = countup * 60;

	//const UInt64 countup_hour = 60 / countup;
	//const UInt64 countup_day = 24 * countup_hour;

	//stock m_offsetIndex;
	//private DateTime m_startDate;
	//private TimeSpan m_openTime;
	//private TimeSpan m_prevTime;

	//public UniqueNumber()
	//{
	//    m_startDate = new DateTime(start_year, start_month, start_day);

	//    m_openTime = DateTime.Now.TimeOfDay;
	//    //현재 시간이 2010 년 6월 1일보다 아래라면 곤란하다. 
	//    //BOOST_ASSERT( start_year >= now_date.year() );
	//    //BOOST_ASSERT( start_month >= now_date.month() );
	//    //BOOST_ASSERT( start_day >= now_date.day() );

	//    Double adv_day = (m_openTime.TotalDays - m_startDate.TimeOfDay.TotalDays) * countup_day;

	//    Int32 adv_hour = (Int32)m_openTime.Hours * countup_hour;
	//    Int32 adv_minute = (Int32)m_openTime.Minutes / countup;

	//    //여기에 걸리면 계산이 어긋난거. 또는 로칼 시계가 ㅄ
	//    // BOOST_ASSERT( adv_day + adv_hour + adv_minute < 0xFFFFFFFF );	//시간은 표현의 반을 나타내니 / 2 를 해야한다. 나머지 반은 카운트

	//    m_offsetIndex.day_time = (Int32)adv_day + adv_hour + adv_minute;
	//   // m_prevTime									= boost::posix_time::time_duration(adv_hour, adv_minute * countup, 0);
	//}

	//UInt64 GetUID()
	//{
	//    DateTime current = DateTime.Now;

	//    Double total_currseconds = current.TimeOfDay.TotalSeconds;
	//    Double total_prevseconds = m_prevTime.TotalSeconds;

	//    Double advance_seconds = total_currseconds - total_prevseconds;
	//    Double advance_countup = advance_seconds / countup_adv;

	//    if (advance_countup > 0)
	//    {
	//        m_offsetIndex.chunk = 0;
	//        m_offsetIndex.day_time = m_offsetIndex.day_time + (Int32)advance_countup;
	//        m_prevTime = current.TimeOfDay;
	//    }

	//    UInt64 uniqueIndex = m_offsetIndex.chunk | (UInt64)m_offsetIndex.day_time << sizeof(UInt32) * 8 ;

	//    ++m_offsetIndex.chunk;
	//    return uniqueIndex;
	//}
}
