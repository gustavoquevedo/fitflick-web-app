
using System.ComponentModel.DataAnnotations;

namespace FitFlickWebApp.Models
{
    public enum PaymentOption
    {
        Free = 0,
        Monthly = 1,
        Annual = 2
    }

    public enum AdvertisementType
    {
        Fitness = 0,
        Health = 1,
        Wellness = 2,
        Others = 3
    }
}