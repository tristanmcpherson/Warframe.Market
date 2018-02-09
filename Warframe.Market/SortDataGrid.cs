using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Warframe.Market
{
    public class SortDataGrid : System.Windows.Controls.DataGrid
    {
        public static readonly DependencyProperty SortDescriptionsProperty = DependencyProperty.Register("SortDescriptions", typeof(List<SortDescription>), typeof(SortDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// Sort descriptions for when grouped LCV is being used. Due to bu*g in WCF this must be set otherwise sort is ignored.
        /// </summary>
        /// <remarks>
        /// IN YOUR XAML, THE ORDER OF BINDINGS IS IMPORTANT! MAKE SURE SortDescriptions IS SET BEFORE ITEMSSOURCE!!! 
        /// </remarks>
        public List<SortDescription> SortDescriptions
        {
            get => (List<SortDescription>)GetValue(SortDescriptionsProperty);
            set => SetValue(SortDescriptionsProperty, value);
        }

        protected override void OnItemsSourceChanged(System.Collections.IEnumerable oldValue, System.Collections.IEnumerable newValue)
        {
            //Only do this if the newValue is a listcollectionview - in which case we need to have it re-populated with sort descriptions due to DG bug
            if (SortDescriptions != null && ((newValue as ListCollectionView) != null))
            {
                var listCollectionView = (ListCollectionView)newValue;
                foreach (var val in SortDescriptions)
                {
                    listCollectionView.SortDescriptions.Add(val);
                }
            }

            base.OnItemsSourceChanged(oldValue, newValue);
        }
    }
}
