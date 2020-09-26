using App3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RelatorioAnalisesView : ContentPage
    {
        public RelatorioAnalisesView()
        {
            InitializeComponent();
            this.BindingContext = new RelatorioAnalisesViewModel();
        }
    }
}