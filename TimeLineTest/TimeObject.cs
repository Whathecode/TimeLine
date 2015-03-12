using System;


namespace TimeLineTest
{
	public class TimeObject
	{
		public DateTime Occurance { get; set; }


		public TimeObject()
		{
			Occurance = DateTime.Now;
		}
	}
}
