using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public class ParsingTask
	{
		/// <param name="lines">Всі рядки файла, які потрібно розпарсити. Перший рядок - заголовок.</param>
		/// <returns>Словник: ключ — ідентифікатор слайда, значення — інформація про слайд</returns>
		/// <remarks>Метод повинен пропускати некоректні рядки, ігноруючи їх</remarks>
		public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
		{
			var tempArr = new string[] { "theory", "quiz", "exercise" };

            return lines
				.Skip(1)
				.Select(x => x.Split(';'))
				.Where(x =>
				{ 
					return x.Length == 3 && int.TryParse(x[0], out int _) && tempArr.Contains(x[1]); 
				})
				.ToDictionary(
					x => int.Parse(x[0]), 
					x => new SlideRecord(int.Parse(x[0]), x[1] == "theory" ? SlideType.Theory : x[1] == "quiz" ? SlideType.Quiz : SlideType.Exercise, x[2]));
		}

		/// <param name="lines">всі рядки файла, які потрібно розпарсити. Перший рядок - заголовок.</param>
		/// <param name="slides">Словник інформації про слайди по ідентифікатору слайда. 
		/// Такий словник можна отримати методом ParseSlideRecords</param>
		/// <returns>Список інформації про відвідування</returns>
		/// <exception cref="FormatException">Если якщо серед рядків є неправильні</exception>
		public static IEnumerable<VisitRecord> ParseVisitRecords(
			IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
		{
			return lines
				.Skip(1)
				.Select(x => x.Split(';'))
				.Select(x => 
				{
					try
					{
						return new VisitRecord(int.Parse(x[0]), int.Parse(x[1]), DateTime.Parse(x[2] + " " + x[3]), slides[int.Parse(x[1])].SlideType);
					}
					catch
					{
						throw new FormatException($"Wrong line [{string.Join(";", x)}]");
					}
				});
		}
	}
}