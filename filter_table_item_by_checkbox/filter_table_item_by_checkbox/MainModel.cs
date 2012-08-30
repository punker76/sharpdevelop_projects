/*
 * Created by SharpDevelop.
 * User: punker76
 * Date: 08/30/2012
 * Time: 08:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace filter_table_item_by_checkbox
{
	/// <summary>
	/// Description of MainModel.
	/// </summary>
	public class MainModel
	{
		public MainModel(int count)
		{
			this.OneString = string.Format("The main string for {0}", count);
			this.OneInt = count;
		}
		
		public string OneString { get; set; }
		public int OneInt { get; set; }
	}
}
