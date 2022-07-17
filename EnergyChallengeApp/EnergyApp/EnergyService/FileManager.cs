﻿using EnergyApp.DataModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Configuration;

namespace EnergyApp.EnergyService
{
    public class FileManager
    {
        public ReferenceData LoadReferenceDataFile()
        {
            ReferenceData referenceData = new ReferenceData();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ReferenceData));

            var referenceDataConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var referenceFilePath = referenceDataConfig.GetValue<string>("AppSettings:REFERENCE_FILE_PATH");
            var referenceFileName = referenceDataConfig.GetValue<string>("AppSettings:REFERENCE_FILE_NAME");

            using (Stream reader = new FileStream(referenceFilePath, FileMode.Open))
            {
                referenceData = (ReferenceData)xmlSerializer.Deserialize(reader);
            }

            return referenceData;
        }
    }
}
