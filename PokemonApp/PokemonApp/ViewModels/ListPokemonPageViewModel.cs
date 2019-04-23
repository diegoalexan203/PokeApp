using Newtonsoft.Json;
using PokemonApp.Models;
using PokemonApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;

namespace PokemonApp.ViewModels
{
	public class ListPokemonPageViewModel : ViewModelBase
	{
        private INavigationService _navigationServices;
        private IPageDialogService _dialogeServices;

        public DelegateCommand NavigateCommand { get; }

        private ObservableCollection<PokemonDetalleDto> _pokemonDetalleDtos;
        private PokemonDetalleDto _pokemonSelect;
        private bool _isEnable;

        public ListPokemonPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
             : base(navigationService)
        {
            _navigationServices = navigationService;
            _dialogeServices = dialogService;
            NavigateCommand = new DelegateCommand(NavegationCommand);
            GetPokemons();
        }

        private void NavegationCommand()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("Pokemon", PokemonSelect);
            _navigationServices.NavigateAsync("DetallePokemonPage", navigationParams, true, true);
        }

        private void GetPokemons()
        {
            var pokemonServices = Refit.RestService.For<IPokemonServices>(AppSettings.AppSettings.UrlBackend);

            var response = pokemonServices.GetListPokemon().Result;

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var jsonString = response.Content.ReadAsStringAsync().Result;
                var lis = JsonConvert.DeserializeObject<PokemonDto>(jsonString);
                PokemonDetalleDtos = new ObservableCollection<PokemonDetalleDto>(lis.PokemonDetalleDto);
            }
        }



        public ObservableCollection<PokemonDetalleDto> PokemonDetalleDtos
        {
            get { return _pokemonDetalleDtos; }
            set
            {
                SetProperty(ref _pokemonDetalleDtos, value);
            }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                SetProperty(ref _isEnable, value);

            }

        }

        public PokemonDetalleDto PokemonSelect
        {
            get { return _pokemonSelect; }
            set
            {
                SetProperty(ref _pokemonSelect, value);
                IsEnable = true;
                NavegationCommand();
            }
        }
    }
}
