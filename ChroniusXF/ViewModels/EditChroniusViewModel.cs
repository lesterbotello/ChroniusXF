using System;
using System.Threading.Tasks;
using ChroniusXF.DataModels;
using ChroniusXF.Persistence;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ChroniusXF.ViewModels
{
    public class EditChroniusViewModel : ViewModelBase, INavigatedAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;
        private readonly IChroniusDatabase _database;

        public HomePageViewModel ParentViewModel { get; set; }

        Chronius _chronius;
        public Chronius Chronius
        {
            get => _chronius;
            set => SetProperty(ref _chronius, value);
        }

        public DelegateCommand SaveCommand { get; }

        public EditChroniusViewModel(INavigationService service, IPageDialogService dialogService, IChroniusDatabase database)
        {
            _navigationService = service;
            _dialogService = dialogService;
            _database = database;
            SaveCommand = new DelegateCommand(async () => await Save());
        }

        private async Task Save()
        {
            try
            {
                var rowsAffected = await _database.SaveChroniusAsync(Chronius);

                if (rowsAffected == 0)
                {
                    await _dialogService.
                        DisplayAlertAsync(
                            "Alert", "An error occurred while saving the Chronius", "OK"
                        );

                    return;
                }

                await ParentViewModel?.LoadAvailableData();
                await _navigationService.GoBackAsync();
            }
            catch(Exception ex)
            {
                await _dialogService.
                    DisplayAlertAsync(
                        "Alert", $"An error occurred while saving the Chronius: {ex.Message}", "OK"
                    );
            }

        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("chronius"))
            {
                Chronius = (Chronius)parameters["chronius"];
            }

            if(parameters.ContainsKey("parentViewModel"))
            {
                ParentViewModel = (HomePageViewModel)parameters["parentViewModel"];
            }
        }
    }
}
