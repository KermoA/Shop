﻿using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dto.WeatherDtos.AccuWeatherDtos;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models.AccuWeathers;

namespace Shop.Controllers
{
    public class AccuWeathersController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;


        public AccuWeathersController
            (
                IWeatherForecastServices weatherForecastServices
            )
        {
            _weatherForecastServices = weatherForecastServices;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = "AccuWeather";
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(AccuWeathersSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeathers", new { city = model.CityName});
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult City(string city)
        {
			AccuLocationWeatherResultDto dto = new();
            dto.CityName = city;
            
            _weatherForecastServices.AccuWeatherResult(dto);

            AccuWeatherViewModel vm = new();

            vm.CityName = dto.CityName;
            vm.EffectiveDate = dto.EffectiveDate;
            vm.EffectiveEpochDate = dto.EffectiveEpochDate;
            vm.Severity = dto.Severity;
            vm.Text = dto.Text;
            vm.Category = dto.Category;
            vm.EndDate = dto.EndDate;
            vm.EndEpochDate = dto.EndEpochDate;
            vm.TempMinValue = dto.TempMinValue;
            vm.TempMaxValue = dto.TempMaxValue;

            return View(vm);
        }
    }
}
