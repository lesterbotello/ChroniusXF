using System;
using System.Threading.Tasks;
using ChroniusXF.DataModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace ChroniusXF.ViewModels
{
    public class EditChroniusViewModel : ViewModelBase, INavigatedAware
    {
        private INavigationService _navigationService;
        private IPageDialogService _dialogService;

        public HomePageViewModel ParentViewModel { get; set; }

        int _chroniusId;
        public int ChroniusId
        {
            get => _chroniusId;
            set => SetProperty(ref _chroniusId, value);
        }

        string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private DateTime _targetDate;
        public DateTime TargetDate
        {
            get => _targetDate;
            set => SetProperty(ref _targetDate, value);
        }

        public DelegateCommand SaveCommand { get; }

        public EditChroniusViewModel(INavigationService service, IPageDialogService dialogService)
        {
            _navigationService = service;
            _dialogService = dialogService;
            SaveCommand = new DelegateCommand(async () => await Save());
        }

        private async Task Save()
        {
            var chronius = new Chronius
            {
                Id = _chroniusId,
                Name = Name,
                Description = Description,
                TargetDate = TargetDate,
                StartingDate = DateTime.Now // TODO: Figure out how to keep old starting date for updates
            };

            try
            {
                var rowsAffected = await App.Database.SaveChroniusAsync(chronius);

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
            Chronius chronius = null;

            if(parameters.ContainsKey("chronius"))
            {
                chronius = (Chronius)parameters["chronius"];
            }

            if(parameters.ContainsKey("parentViewModel"))
            {
                ParentViewModel = (HomePageViewModel)parameters["parentViewModel"];
            }

            if (parameters.ContainsKey("chroniusId") && parameters["chroniusId"] != null)
                _chroniusId = (int)parameters["chroniusId"];
            else
                _chroniusId = 0;

            if (_chroniusId == 0)
            {
                Title = "New Chronius";
                TargetDate = DateTime.Now;
            }
            else
            {
                if(chronius != null)
                {
                    Name = chronius.Name;
                    Description = chronius.Description;
                    TargetDate = chronius.TargetDate;

                    Title = $"Edit '{Name}'";
                }
            }
        }
    }
}
