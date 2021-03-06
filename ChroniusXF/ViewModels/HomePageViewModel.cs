﻿using System.Reactive.Subjects;
using Prism.Commands;
using Prism.Navigation;
using ChroniusXF.Utils;
using static System.ObservableExtensions;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ChroniusXF.DataModels;
using System.Reactive.Linq;
using System.Windows.Input;
using ChroniusXF.Persistence;

namespace ChroniusXF.ViewModels
{
    public class HomePageViewModel : ViewModelBase, INavigatedAware, IUpdateableHomeScreen
    {
        public DelegateCommand NewCommand { get; }
        INavigationService _navigationSerice;
        IChroniusDatabase _chroniusDatabase;
        private Subject<bool> _isDataAvailableSubject = new Subject<bool>();
        public IDisposable _timerSubscription;
        public ICommand EditCommand { get; }

        string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        bool _isEmptyMessageVisible;
        public bool IsEmptyMessageVisible
        {
            get => _isEmptyMessageVisible;
            set => SetProperty(ref _isEmptyMessageVisible, value);
        }

        bool _isMainContentVisible;
        public bool IsMainContentVisible
        {
            get => _isMainContentVisible;
            set => SetProperty(ref _isMainContentVisible, value);
        }

        ICollection<Chronius> _chroni;
        public ICollection<Chronius> Chroni
        {
            get => _chroni;
            set => SetProperty(ref _chroni, value);
        }

        public HomePageViewModel(INavigationService navigationService, IChroniusDatabase chroniusDatabase)
        {
            _navigationSerice = navigationService;
            _chroniusDatabase = chroniusDatabase;
            NewCommand = new DelegateCommand(New);
            EditCommand = new DelegateCommand<Chronius>(Edit);
            Title = "Chronius";

            _isDataAvailableSubject?
                .Subscribe(isDataAvailable =>
                {
                    IsMainContentVisible = isDataAvailable;
                    IsEmptyMessageVisible = !isDataAvailable;

                    // Start update timer after data has been loaded...
                    if (isDataAvailable)
                    {
                        _timerSubscription = Observable
                            .Interval(TimeSpan.FromSeconds(1))
                            .Subscribe(_ => UpdateCountdowns())
                            .DisposeWith(ViewModelSubscriptions);
                    }
                }).DisposeWith(ViewModelSubscriptions);

            ReloadData();
        }

        public async Task ReloadData()
        {
            // Unsubscribe from previous timer, if it exists...
            _timerSubscription?.Dispose();

            bool dataAvailable = false;
            await Task.Run(async () =>
            {
                Chroni = await _chroniusDatabase.GetActiveChroni();
                dataAvailable = Chroni.Count > 0;
            });

            _isDataAvailableSubject.OnNext(dataAvailable);
        }

        private void New()
        {
            var param = new NavigationParameters
            {
                { "chronius", new Chronius { TargetDate = DateTime.Now } },
                { "parentViewModel", this }
            };
            _navigationSerice?.NavigateAsync("EditChronius", param);
        }

        private void Edit(Chronius chronius)
        {
            var param = new NavigationParameters
                {
                    { "chronius", chronius },
                    { "parentViewModel", this }
                };
            _navigationSerice?.NavigateAsync("EditChronius", param);
        }

        public void OnNavigatedFrom(INavigationParameters parameters) { }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = parameters["title"] as string;
        }

        private void UpdateCountdowns()
        {
            foreach (var chronius in Chroni)
            {
                var now = DateTime.Now;
                var name = chronius.Name;
                var distance = chronius.TargetDate - now;
                var days = distance.Days;
                var hours = distance.Hours % 24;
                var minutes = distance.Minutes % 60;
                var seconds = distance.Seconds % 60;

                if(chronius.IsActive)
                    chronius.DisplayText = $"{name} is happening in {days} days, {hours} hours, {minutes} minutes and {seconds} seconds";
                else
                    chronius.DisplayText = $"{name} happened {Math.Abs(days)} days, {Math.Abs(hours)} hours, {Math.Abs(minutes)} minutes and {Math.Abs(seconds)} seconds ago";
            }
        }
    }
}
