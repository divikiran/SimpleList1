using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.Practices.Unity;

namespace SimpleList
{
	public partial class SimpleListPage : ContentPage
	{
		public SimpleListViewModel ViewModel
		{
			get;
			set;
		}

		readonly ICountryRepository CountryRepository;

		public SimpleListPage(ICountryRepository countryRepository)
		{
			CountryRepository = countryRepository;
			InitializeComponent();

			ToolbarItem secondPage = new ToolbarItem() { Text = "GO" };
			secondPage.Clicked += async (object sender, System.EventArgs e) =>
			{
				await GoToSecondPage();
			};

			ToolbarItems.Add(secondPage);

			BindingContext = ViewModel = App.Container.Resolve<SimpleListViewModel>();
		}

		async Task GoToSecondPage()
		{
			await Navigation.PushAsync(new SecondPage());
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			ViewModel.IsBusy = true;
			//CountryRepository repo = new CountryRepository();
			await CountryRepository.GetData().ContinueWith((arg1) =>
			{
				MessagingCenter.Send<SimpleListPage>(this, "LoadCountries");
			});
		}
	}
}
