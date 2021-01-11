using ChurrascoApp.DatabaseModels;
using ChurrascoApp.Interfaces;
using ChurrascoApp.Services;
using ChurrascoApp.ViewModels;
using ChurrascoApp.Views;
using Prism;
using Prism.Ioc;
using SQLite;
using System;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ChurrascoApp
{
    public partial class App : IDisposable
    {
        #region Variáveis

        private SQLiteConnection _sqliteConnection;

        #endregion

        #region Contrutores

        public App() : this(null) { }

        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        #endregion

        protected override async void OnInitialized()
        {
            InitializeComponent();
            CreateSQLiteTables();
            CreateSQLiteData();

            await NavigationService.NavigateAsync("app:///PrismMasterDetailPage/NavigationMenuPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<NavigationMenuPage, NavigationMenuPageViewModel>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PrismMasterDetailPage, PrismMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<ChurrascosPage, ChurrascosPageViewModel>();
            containerRegistry.RegisterForNavigation<ParticipantesPage, ParticipantesPageViewModel>();
            containerRegistry.RegisterForNavigation<TotaisPage, TotaisPageViewModel>();
            containerRegistry.RegisterForNavigation<ChurrascoPage, ChurrascoPageViewModel>();
            containerRegistry.RegisterForNavigation<ParticipantePage, ParticipantePageViewModel>();
            containerRegistry.RegisterForNavigation<TotaisListaPage, TotaisListaPageViewModel>();
            containerRegistry.RegisterForNavigation<TotaisInclusaoGastoPage, TotaisInclusaoGastoPageViewModel>();
        }

        /// <summary>
        /// Método que drop e recria as tabelas do app
        /// </summary>
        private void CreateSQLiteTables()
        {
            _sqliteConnection = DependencyService.Get<ISQLiteConnection>().GetConnection();

            _sqliteConnection.DropTable<Churrasco>();
            _sqliteConnection.DropTable<ChurrascoDetails>();
            _sqliteConnection.DropTable<Funcionario>();
            _sqliteConnection.DropTable<Convidado>();
            _sqliteConnection.DropTable<Participante>();
            _sqliteConnection.DropTable<Gasto>();

            _sqliteConnection.CreateTable<Churrasco>(CreateFlags.AutoIncPK);
            _sqliteConnection.CreateTable<ChurrascoDetails>(CreateFlags.AutoIncPK);
            _sqliteConnection.CreateTable<Funcionario>(CreateFlags.AutoIncPK);
            _sqliteConnection.CreateTable<Convidado>(CreateFlags.AutoIncPK);
            _sqliteConnection.CreateTable<Participante>(CreateFlags.AutoIncPK);
            _sqliteConnection.CreateTable<Gasto>(CreateFlags.AutoIncPK);
        }

        private void CreateSQLiteData()
        {
            ChurrascoServices.CreateChurrascos();
            ChurrascoServices.CreateParticipantes();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
