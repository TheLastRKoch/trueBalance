using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TRueBalance.Models.MealViewModels;

namespace TRueBalance.Repositories.CSVManager
{
    public class CSVRepository : ICSVRepository
    {
        /// <summary>
        /// Convert a list of objects to CSV file
        /// </summary>
        /// <param name="path">The path where the CSV file will be save</param>
        /// <param name="objectList">The List<object> to convert to CSV</param>
        public void MealsToCSV(string path, List<MealModel> list)
        {
            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(list);
            }
           
        }

        public List<MealModel> MealsFromCSV(string filePath)
        {
            TextReader reader = new StreamReader(filePath);
            var csvReader = new CsvReader(reader);
            if (!csvReader.Configuration.HasHeaderRecord)
            {
                throw new Exception("Error trying to get the the csv data file doen't have headers");
            }
            return csvReader.GetRecords<MealModel>().ToList();
        }
    }
}
