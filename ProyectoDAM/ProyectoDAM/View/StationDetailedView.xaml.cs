﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoDAM.View
{
    /// <summary>
    /// Clase que representa a la vista detalle de cada parada
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StationDetailedView : ContentPage
	{
		public StationDetailedView ()
		{
			InitializeComponent ();
		}
	}
}