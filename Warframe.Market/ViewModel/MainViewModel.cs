using GalaSoft.MvvmLight;
using Nito.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Warframe.Market.Model;
using Warframe.Market.Service;

namespace Warframe.Market.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public WarframeMarketApi warframeMarketApi;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            
            warframeMarketApi = new WarframeMarketApi();
        }

        private bool _itemsRetrieved;

        private CustomAsyncCommand _getItemsCommand;

        public CustomAsyncCommand GetItemsCommand
        {
            get
            {
                if (_getItemsCommand == null)
                {
                    _getItemsCommand = new CustomAsyncCommand(
                        async (a) =>
                        {
                            await GetItems();
                            _itemsRetrieved = true;
                        }, (a) => !_itemsRetrieved);
                }
                return _getItemsCommand;
            }
        }
        

        public async Task GetItems()
        {
            Items = new ObservableCollection<Item>(await warframeMarketApi.GetItems());
            _itemsRetrieved = true;
        }

        private CustomAsyncCommand _getListingForSelectedItemCommand;

        public CustomAsyncCommand GetListingForSelectedItemCommand
        {
            get
            {
                if (_getListingForSelectedItemCommand == null)
                {
                    _getListingForSelectedItemCommand = new CustomAsyncCommand(
                        async () =>
                        {
                            await GetListingForSelectedItem();
                        }, () => SelectedItem != null);
                }
                return _getListingForSelectedItemCommand;
            }
        }
        

        public async Task GetListingForSelectedItem()
        {
            var listing = await warframeMarketApi.GetListingForItem(SelectedItem);
            SellListings = new ObservableCollection<Sell>(listing.sell.Where(s => s.online_ingame).OrderBy(s => s.price));
        }

        private Visibility _listingVisibility = Visibility.Collapsed;

        /// <summary>
        /// Sets and gets the ListingVisibility property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Visibility ListingVisibility
        {
            get
            {
                return _listingVisibility;
            }

            set
            {
                if (_listingVisibility == value)
                {
                    return;
                }
                _listingVisibility = value;
                RaisePropertyChanged(nameof(ListingVisibility));
            }
        }

        private Item _selectedItem = null;

        /// <summary>
        /// Sets and gets the SelectedItem property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Item SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                if (_selectedItem == value)
                {
                    return;
                }

                if (value == null)
                {
                    ListingVisibility = Visibility.Hidden;
                }
                else
                {
                     Task.Run(async () => await GetListingForSelectedItemCommand.ExecuteAsync(null));
                    ListingVisibility = Visibility.Visible;
                }

                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }
        
        private ObservableCollection<Item> _items = new ObservableCollection<Item>();

        /// <summary>
        /// Sets and gets the Items property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Item> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                {
                    return;
                }

                _items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
    
        private ObservableCollection<Sell> _sellListings = new ObservableCollection<Sell>();

        /// <summary>
        /// Sets and gets the  property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Sell> SellListings
        {
            get
            {
                return _sellListings;
            }

            set
            {
                if (_sellListings == value)
                {
                    return;
                }

                _sellListings = value;
                RaisePropertyChanged(nameof(SellListings));
            }
        }

    }
}