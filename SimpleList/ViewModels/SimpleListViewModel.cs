using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleList
{
	public class SimpleListViewModel : INotifyPropertyChanged
	{
		bool isBusy = false;

		public bool IsBusy
		{
			get
			{
				return isBusy;
			}

			set
			{
				isBusy = value;
				OnPropertyChanged();
			}
		}

		ObservableCollection<Country> countryList;

		public ObservableCollection<Country> CountryList
		{
			get
			{
				return countryList;
			}

			set
			{
				countryList = value;
				OnPropertyChanged();
			}
		}

		public ICountryRepository CountryRepo
		{
			get;
			set;
		}

		public SimpleListViewModel(ICountryRepository countryRepository)
		{
			CountryRepo = countryRepository;
			CountryList = new ObservableCollection<Country>();
			MessagingCenter.Subscribe<SimpleListPage>(this, "LoadCountries", async (sender) =>
			{
				await LoadCountriesToListView();
			});
		}

		async Task LoadCountriesToListView()
		{
			List<Country> countries = Persistence.Instance.CountryList;
			if (countries == null || countries?.Count == 0)
			{
				await Task.Delay(4000);
				var allCountries = CountryRepo.GetAll();
				countries = CountryRepo.ConvertToModels(allCountries);
				CountryList = new ObservableCollection<Country>(countries);
				Persistence.Instance.CountryList = countries;
			}

			//Perfect example for profiler
			//foreach (var item in countries)
			//{
			//	CountryList.Add(item);
			//}

			IsBusy = false;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}