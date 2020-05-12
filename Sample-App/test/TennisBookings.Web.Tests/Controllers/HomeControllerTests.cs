using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Controllers;
using TennisBookings.Web.Services;
using TennisBookings.Web.ViewModels;
using Xunit;

namespace TennisBookings.Web.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsSun()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(m => m.GetCurrentWeather()).Returns(new WeatherResult()
            {
                WeatherCondition = WeatherCondition.Sun
            });

            var mockFeature = new Mock<IOptions<FeaturesConfiguration>>();

            var sut = new HomeController(mockWeatherForecaster.Object, mockFeature.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("It's sunny right now.", model.WeatherDescription);
        }

        [Fact]
        public void ReturnsExpectedViewModel_WhenWeatherIsRain()
        {
            var mockWeatherForecaster = new Mock<IWeatherForecaster>();
            mockWeatherForecaster.Setup(m => m.GetCurrentWeather()).Returns(new WeatherResult()
            {
                WeatherCondition = WeatherCondition.Rain
            });

            var mockFeature = new Mock<IOptions<FeaturesConfiguration>>();

            var sut = new HomeController(mockWeatherForecaster.Object, mockFeature.Object);

            var result = sut.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<HomeViewModel>(viewResult.ViewData.Model);
            Assert.Contains("We're sorry but it's raining here.", model.WeatherDescription);
        }
    }
}