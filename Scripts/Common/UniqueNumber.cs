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


	////���� ������� 2010�� 1�� 1��
	//const Int32 start_year = 2010;
	//const Int32 start_month = 1;
	//const Int32 start_day = 1;

	////10�и��� day_time �� 1�� �ø��� count_up ���� �����ϸ� �� �����ϰ� �����Ҽ� �ִ�. ��� ���� �����.
	////10�е��ȿ� 4294967296 ���� ���డ��. 81715�⵿�� �����ִ�.

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
	//    //���� �ð��� 2010 �� 6�� 1�Ϻ��� �Ʒ���� ����ϴ�. 
	//    //BOOST_ASSERT( start_year >= now_date.year() );
	//    //BOOST_ASSERT( start_month >= now_date.month() );
	//    //BOOST_ASSERT( start_day >= now_date.day() );

	//    Double adv_day = (m_openTime.TotalDays - m_startDate.TimeOfDay.TotalDays) * countup_day;

	//    Int32 adv_hour = (Int32)m_openTime.Hours * countup_hour;
	//    Int32 adv_minute = (Int32)m_openTime.Minutes / countup;

	//    //���⿡ �ɸ��� ����� ��߳���. �Ǵ� ��Į �ð谡 ��
	//    // BOOST_ASSERT( adv_day + adv_hour + adv_minute < 0xFFFFFFFF );	//�ð��� ǥ���� ���� ��Ÿ���� / 2 �� �ؾ��Ѵ�. ������ ���� ī��Ʈ

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
