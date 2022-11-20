using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class StatisticsTask
	{
		/// <summary>
		/// Метод що визначає скільки часу в середньому користувачі проводять на слайді заданого типу.
		/// Враховуються лише перегляди, що тривають більше одної хвилини та менше двох годин
		/// </summary>
		/// <param name="visits"> список відвідувань. </param>
		/// <param name="slideType"> тип слайду, який потрібно дослідити. </param>
		/// <returns></returns>
		public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
		{
			try
			{
				return visits
					.OrderBy(x => x.DateTime)
					.GroupBy(x => x.UserId)
					.Select(x => x
						.Bigrams()
						.Where(y => y.Item1.SlideType == slideType && y.Item1.SlideId != y.Item2.SlideId))
					.SelectMany(y => y
						.Select(x => (double)(x.Item2.DateTime - x.Item1.DateTime).TotalMinutes)
						.Where(x => x >= 1 && x <= 120))
					.Median();
			}
			catch
			{
				return 0;
			}
		}
	}
}