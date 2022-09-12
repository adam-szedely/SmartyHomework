﻿using System;
namespace SmartyHomework.Services
{
	public interface IExchangeRateRepository
	{
        public File CreateTheFile(string dateName);
        //1. Receive data
        //2. REMOVE non-european countries (have list of EU countries, deserialize or get it directly from ČNB?)
        //3. Create the file with appropriate name yyyy.MM.dd.json
    }
}

//- IExchangeRateRepository (jeho role je uložení dat směnných kurzů)
//  1





