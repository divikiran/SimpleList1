using Microsoft.Practices.Unity;
using Xamarin.Forms;


namespace SimpleList
{
	public partial class App : Application
	{

		public static UnityContainer Container { get; set; }

		public App()
		{
			InitializeComponent();

			Container = new UnityContainer();
			App.Container.RegisterType<ICountryRepository, CountryRepository>();
			//App.Container.RegisterType<SimpleListPage>();

			var simpleListPage = App.Container.Resolve<SimpleListPage>();
			MainPage = new NavigationPage(simpleListPage);
			//MainPage = new NavigationPage(new SimpleListPage());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
