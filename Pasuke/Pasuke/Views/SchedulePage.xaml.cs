using Pasuke.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Pasuke.Model;
using Prism.Services;

namespace Pasuke.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        private PasukeModel _model = new PasukeModel();


        public SchedulePage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            BindingContext = new SchedulePageViewModel();
        }
    }
}