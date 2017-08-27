using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
	public static class Extensions
	{
		public static void AddAll<T>(this ObservableCollection<T> Source, IEnumerable<T> Collection)
		{
			foreach (var Item in Collection)
			{
				Source.Add(Item);
			}
		}
	}
}
