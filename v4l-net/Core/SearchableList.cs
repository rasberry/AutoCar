#region LICENSE
/* 
 * Copyright (C) 2007 Tim Taubert (twenty-three@users.berlios.de)
 * 
 * This file is part of Video4Linux.Net.
 *
 * Video4Linux.Net is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 *
 * Video4Linux.Net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion LICENSE

using System;
using System.Reflection;

namespace Video4Linux.Core
{
	/// <summary>
	/// Extends a generic list so that it can be searched by a given property.
	/// </summary>
	public class SearchableList<T> : System.Collections.Generic.List<T>
	{
		#region Public Methods
		
		/// <summary>
		/// Return an item's index by searching it by a given property name and value.
		/// </summary>
		/// <param name="property">The property to search for.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>The index of the item.</returns>
		public int IndexOf(string property, object value)
		{
			// are there any items?
			if (Count == 0)
				return -1;
			
			PropertyInfo pi = this[0].GetType().GetProperty(property);
			// did we find the property?
			if (pi == null)
				return -1;
			
			// search the matching item
			for (int i=0; i<Count; i++)
				if (pi.GetValue(this[i], new object[0]).Equals(value))
					return i;
			
			return -1;
		}
		
		#endregion Public Methods
	}
}
