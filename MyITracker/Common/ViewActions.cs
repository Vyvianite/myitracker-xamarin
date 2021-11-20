using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyITracker.ViewModels
{
    public static class ViewActions
    {
        public static async Task DisplayAlert(string title, string message, string acknowledge)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, acknowledge);
        }   
        public static async Task<bool> DisplayAlert(string title, string message, string yes, string no)
        {
            var response = await Application.Current.MainPage.DisplayAlert(title, message, yes, no);
            return response;
        }
    }
}   
