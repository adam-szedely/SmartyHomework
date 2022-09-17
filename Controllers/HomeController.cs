﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http;
using SmartyHomework.Services;


namespace SmartyHomework.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IExchangeRateConnector _exchangeRateConnector;
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public HomeController(IExchangeRateRepository exchangeRateRepository, IExchangeRateConnector exchangeRateConnector)
        {
            _exchangeRateConnector = exchangeRateConnector;
            _exchangeRateRepository = exchangeRateRepository;
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download()
        {
            var outputPath = @"/Users/adamszedely/Projects/SmartyHomework/SmartyHomework/Data/";

            DateTime begindate = Convert.ToDateTime("01/07/2022");
            DateTime enddate = Convert.ToDateTime("4/07/2022");

            var countOfFiles = 1;
            var d = begindate.Day;

            while (begindate < enddate)
            {
                if (begindate.DayOfWeek == DayOfWeek.Saturday)
                {
                    begindate = begindate.AddDays(2);
                }
                _exchangeRateConnector.DownloadTxtWithFlurl(_exchangeRateConnector.GenerateUri(begindate), outputPath, countOfFiles);

                begindate = begindate.AddDays(1);
                countOfFiles++;
            }

            return Ok();

            //THEY'RE NOT SAVED THERE YET UNTIL DOWNLOAD FINISHES - need to create redirect or new action or sth
        }

        [HttpGet("edit")]
        public async Task<IActionResult> Edit()
        {
            var outputPath = @"/Users/adamszedely/Projects/SmartyHomework/SmartyHomework/Data/";
            var fileCount = (from file in Directory.EnumerateFiles(outputPath, "*.txt", SearchOption.TopDirectoryOnly)
                             select file).Count();

            for (int i = 1; i <= fileCount; i++)
            {
                _exchangeRateRepository.SaveEuRates(outputPath + "CurrencyRate" + i + ".txt");
            }

            return Ok();
        }
    }
}

