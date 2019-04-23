using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonApp.ViewModels
{
	public class DetallePokemonPageViewModel : ViewModelBase
	{
        public DetallePokemonPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
             : base(navigationService)
        {

        }
	}
}
