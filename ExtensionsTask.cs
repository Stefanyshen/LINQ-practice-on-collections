using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
	public static class ExtensionsTask
	{
        /// <summary>
        /// Медіана списку з непарної кількості елементів — це центральний елемент списку після сортування.
        /// Медіана списку з непарної кількості елементів — це середнє арифметичне 
        /// двох центральних елементів списку після сортування.
        /// </summary>
        /// <exception cref="InvalidOperationException">Якщо послідовність не вміщує в собі жодних елементів.</exception>
        public static double Median(this IEnumerable<double> items)
		{
			var list = items.OrderBy(x => x).ToList();
            var count = list.Count();
			if (count == 0) throw new InvalidOperationException();
			return count % 2 == 0 ?
				(list[count / 2 - 1] + list[count / 2]) / 2 : list[count / 2];
        }

		/// <returns>
		/// повертає послідовність, що створена з пар сосідніх елементів.
		/// Наприклад, по послідовності {1,2,3} метод повинен повернути дві пари: (1,2) и (2,3).
		/// </returns>
		public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
		{
			var en = items.GetEnumerator();
			en.MoveNext();
			var element = en.Current;
			while(en.MoveNext())
				yield return Tuple.Create(element, element =  en.Current);
		}
	}
}