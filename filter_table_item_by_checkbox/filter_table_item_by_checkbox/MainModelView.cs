/*
 * Created by SharpDevelop.
 * User: punker76
 * Date: 08/30/2012
 * Time: 08:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Data;

namespace filter_table_item_by_checkbox
{
	/// <summary>
	/// Description of MainModelView.
	/// </summary>
	public class MainModelView : INotifyPropertyChanged
	{
		public MainModelView()
		{
			var coll = new ObservableCollection<MainModel>();
			for (int i = 0; i < 1000; i++) {
				coll.Add(new MainModel(i));
			}
			yourCollView = CollectionViewSource.GetDefaultView(coll);
			yourCollView.Filter += new Predicate<object>(yourCollView_Filter);
		}

		bool yourCollView_Filter(object obj)
		{
			return FilterItems
				? (((MainModel)obj).OneInt % 2) == 0 // now filter your item
				: true;
		}
		
		private ICollectionView yourCollView;
		public ICollectionView YourCollView
		{
			get { return yourCollView; }
			set
			{
				if (value == yourCollView) {
					return;
				}
				yourCollView = value;
				this.NotifyPropertyChanged("YourCollView");
			}
		}

		private bool _filterItems;
		public bool FilterItems
		{
			get { return _filterItems; }
			set
			{
				if (value == _filterItems) {
					return;
				}
				_filterItems = value;
				// filer your collection here
				YourCollView.Refresh();
				this.NotifyPropertyChanged("FilterItems");
			}
		}
		
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(String propertyName)
		{
			var eh = PropertyChanged;
			if (eh != null) {
				eh(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
