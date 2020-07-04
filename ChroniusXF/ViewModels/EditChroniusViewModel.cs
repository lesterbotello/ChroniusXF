using System;
using System.Linq;
using System.Threading.Tasks;
using ChroniusXF.DataModels;
using ChroniusXF.Persistence;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace ChroniusXF.ViewModels
{
    public class EditChroniusViewModel : ViewModelBase, IInitialize
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;
        private readonly IChroniusDatabase _database;
        private bool _isReady;

        private IUpdateableHomeScreen ParentViewModel { get; set; }

        Chronius _chronius;
        public Chronius Chronius
        {
            get => _chronius;
            set => SetProperty(ref _chronius, value);
        }

        public DelegateCommand SaveCommand { get; }
        
        public DelegateCommand NavigateToAddLocation => new DelegateCommand(async () =>
        {
            var eventType = Chronius.EventType;

            if (eventType == EventType.Meeting || eventType == EventType.Party ||
                eventType == EventType.Seminar)
            {
                await _navigationService.NavigateAsync("AddLocation");
            }
            else
            {
                await _dialogService.DisplayAlertAsync("Chronius", "This event type doesn't need a location.", "Ok");
            }
        });

        public DelegateCommand PickEventTypeCommand => new DelegateCommand(async () =>
        {
            var eventTypes = Enum.GetValues(typeof(EventType)).Cast<EventType>();
            var actionButtons = eventTypes.Select(et => 
                ActionSheetButton.CreateButton(
                    et.ToName(),
                    () =>
                    {
                        if (Chronius != null)
                        {
                            Chronius.EventTypeId = (int) et;
                            RaisePropertyChanged(nameof(Chronius));
                        }
                    })).ToArray();
            await _dialogService.DisplayActionSheetAsync("Select Event Type", actionButtons);
        });

        public EditChroniusViewModel(INavigationService service, IPageDialogService dialogService, IChroniusDatabase database)
        {
            _navigationService = service;
            _dialogService = dialogService;
            _database = database;
            SaveCommand = new DelegateCommand(async () => await Save(), () => _isReady);
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

                await ParentViewModel?.ReloadData();
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

        public void Initialize(INavigationParameters parameters)
        {
            if(parameters.ContainsKey("chronius"))
            {
                Chronius = (Chronius)parameters["chronius"];
            }

            if(parameters.ContainsKey("parentViewModel"))
            {
                ParentViewModel = (IUpdateableHomeScreen)parameters["parentViewModel"];
            }

            _isReady = true;
            SaveCommand.RaiseCanExecuteChanged();
        }
    }
}
