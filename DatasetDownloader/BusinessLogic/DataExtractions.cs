﻿using DatasetDownloader.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DatasetDownloader.DataContracts.DataFieldAnalysis;

namespace DatasetDownloader.BusinessLogic
{
    public class DataExtractions : IDataExtractions
    {
        private const string CleanSetLocation = @"C:\Temp\DataDownload\Cleandata\";

        public DataFieldMain ExecuteDataInformationExtraction(string[] items, string delimiter, string originalFilename)
        {
            var analysisList = new DataFieldMain() { DataFieldAnalysis = new List<DataFieldAnalysis>() };
            List<string> cleanedSet = new List<string>();
            var potentialHeader = items.First();

            cleanedSet.Add(this.CleanString(potentialHeader.Replace(delimiter, "|")).Replace("|", ";").ToLower());
            items.ToList().CopyTo(1, items, 0, items.Length - 1);
            var index = 0;

            foreach (var column in potentialHeader.Split(delimiter))
            {
                string lastvalue = string.Empty;

                try
                {
                    DataFieldAnalysis dfa = new DataFieldAnalysis();
                    this.InstanceNewDataFieldAnalysis(dfa);
                    this.GetSizeInfo(items, delimiter, index, column, dfa);
                    lastvalue = this.LoopForEachItem(items, delimiter, cleanedSet, index, dfa, lastvalue);
                    this.CleanItemProperties(items, dfa);

                    analysisList.DataFieldAnalysis.Add(dfa);
                    index++;
                }
                catch (Exception ex)
                {
                    // handle exception!
                    Console.Write(ex.Message);
                    break;
                }
            }

            analysisList.CleansetFilename = this.WriteCleanset(cleanedSet);
            analysisList.OriginalsetFilename = this.WriteCleanset(cleanedSet);
            return analysisList;
        }

        private void CleanItemProperties(string[] items, DataFieldAnalysis dfa)
        {
            dfa.DamerauValue = dfa.DamerauValue / items.Count();
            if (dfa.FieldConsistenceList.Where(f => f.Trim().Any()).ToArray().Distinct().Count() == 1 ||
                (
                    dfa.FieldType.Distinct().Count() == 1 && (
                        dfa.FieldType.First() == FieldTypes.Dec ||
                        dfa.FieldType.First() == FieldTypes.Int ||
                        dfa.FieldType.First() == FieldTypes.Long ||
                        dfa.FieldType.First() == FieldTypes.Short)))
            {
                dfa.FieldIsConsistent = true;
                dfa.ConsistentDataType = dfa.FieldConsistenceList.First();
                dfa.FieldConsistenceList = new List<string> { dfa.FieldConsistenceList.First() };
                dfa.FieldType = new List<FieldTypes> { dfa.FieldType.First() };
            }
            else
            {
                dfa.FieldConsistenceList = dfa.FieldConsistenceList.Where(f => f.Trim().Any()).Select(f => f.Replace(";", string.Empty)).ToArray().Distinct().ToList();
                dfa.FieldType = dfa.FieldType.Distinct().ToList();
            }
        }

        private string LoopForEachItem(string[] items, string delimiter, List<string> cleanedSet, int index, DataFieldAnalysis dfa, string lastvalue)
        {
            foreach (var i in items)
            {
                cleanedSet.Add(this.CleanString(i.Replace(delimiter, "|")).Replace("|", ";").ToLower());
                if (i.Trim().Any())
                {
                    var linedata = i.Split(delimiter)[index];
                    dfa.FieldConsistenceList.Add(this.GetConsistency(this.CleanString(linedata).Select(f => this.GetDataConsistence(f)).ToArray()));
                    dfa.FieldType.Add(this.DetermineFieldType(dfa.FieldConsistenceList.Last(), linedata.Trim()));

                    if (lastvalue.Any())
                    {
                        dfa.DamerauValue += this.DamerauLevenshteinDistance(linedata, lastvalue);
                    }

                    lastvalue = linedata;
                }
            }

            return lastvalue;
        }

        private void InstanceNewDataFieldAnalysis(DataFieldAnalysis dfa)
        {
            if (dfa.FieldConsistenceList == null)
            {
                dfa.FieldConsistenceList = new List<string>();
            }

            if (dfa.FieldType == null)
            {
                dfa.FieldType = new List<FieldTypes>();
            }
        }

        private void GetSizeInfo(string[] items, string delimiter, int index, string column, DataFieldAnalysis dfa)
        {
            dfa.MinimumFieldLength = items.Where(f => f.Trim().Length > 0).Select(f => f.Split(delimiter)[index].Length).Min();
            dfa.MaximumFieldLength = items.Where(f => f.Trim().Length > 0).Select(f => f.Split(delimiter)[index].Length).Max();
            dfa.AverageFieldLength = (int)items.Where(f => f.Trim().Length > 0).Select(f => f.Split(delimiter)[index].Length).Average();
            dfa.FieldName = this.CleanString(column).ToLower();
        }

        private string WriteCleanset(List<string> cleanedSet)
        {
            if (!cleanedSet.Any())
                return string.Empty;

            var cleanedset = string.Join("\r\n", cleanedSet);
            if (!Directory.Exists(CleanSetLocation))
            {
                Directory.CreateDirectory(CleanSetLocation);
            }

            var file = Path.Combine(CleanSetLocation, Guid.NewGuid().ToString() + ".csv");
            using (var streamwriter = new StreamWriter(file))
            {
                streamwriter.Write(cleanedset);
                streamwriter.Close();
            }

            return file;
        }

        private string GetConsistency(string[] data)
        {
            string consists = string.Empty;
            string currentType = string.Empty;
            int currentcount = 0;
            foreach(var dat in data)
            {
                if(dat != currentType)
                {
                    if(currentType.Any())
                    {
                        consists = string.Format(consists, currentcount);
                    }

                    consists += (consists.Any() ? ";" : string.Empty) + dat + "{0}";
                    currentcount = 0;
                }

                currentcount++;
                currentType = dat;
            }

            consists = string.Format(consists, currentcount);
            return consists;
        }

        private string GetDataConsistence(char data)
        {
            int output = 0;
            return int.TryParse(data.ToString(), out output) ? "N" : "A";
        }

        private string CleanString(string data)
        {
            var removeChars = new[] { ".", ",", "?", "!", "*", "$", "<", ">", "\'", "\\", ";", ":", "\"", " ", "_", "-", "`", "+", "=", "\r", "\n", "\n" };
            foreach (var remchar in removeChars)
                data = data.Replace(remchar.ToString(), string.Empty);

            return data.Trim();
        }

        private FieldTypes DetermineFieldType(string data, string fieldvalue)
        {
            var result = data.Split(';');
            if(result.Length > 1)
            {
                // alfanumerical;
                return FieldTypes.Alfanumeric;
            }
            else
            {
                bool boolValue = false;
                int intValue = 0;
                long longValue = 0;
                decimal decimalvalue = 0;

                bool parsed = false;

                parsed = bool.TryParse(fieldvalue, out boolValue);
                if (parsed)
                    return FieldTypes.Bool;

                parsed = decimal.TryParse(fieldvalue, out decimalvalue);
                if (parsed && decimalvalue != (int)decimalvalue)
                    return FieldTypes.Dec;

                parsed = long.TryParse(fieldvalue, out longValue);
                if (parsed)
                    return FieldTypes.Long;

                parsed = int.TryParse(fieldvalue, out intValue);
                if (parsed)
                    return FieldTypes.Int;

                return FieldTypes.Alfanumeric;
            }

        }

        private float DamerauLevenshteinDistance(string value1, string value2)
        {
            if (value1 == null || value2 == null)
            {
                return 0;
            }

            if (value2.Length == 0)
            {
                return value1.Length;
            }

            var costs = new int[value2.Length];
            for (var i = 0; i < costs.Length;)
            {
                costs[i] = ++i;
            }

            for (var i = 0; i < value1.Length; i++)
            {
                var cost = i;
                var additionCost = i;
                var value1Char = value1[i];

                for (int j = 0; j < value2.Length; j++)
                {
                    CalculateCosts(value1, value2, costs, i, ref cost, ref additionCost, value1Char, j);

                    costs[j] = cost;
                }
            }

            return costs[costs.Length - 1];
        }

        private static void CalculateCosts(string value1, string value2, int[] costs, int i, ref int cost, ref int additionCost, char value1Char, int j)
        {
            int insertionCost = cost;
            cost = additionCost;
            additionCost = costs[j];
            if (value1Char != value2[j])
            {
                if (insertionCost < cost)
                {
                    cost = insertionCost;
                }

                if (additionCost < cost)
                {
                    cost = additionCost;
                }

                if (i > 0 && j > 0 && value1[i] == value2[j - 1] && value1[i - 1] == value2[j])
                {
                    --cost;
                }

                ++cost;
            }
        }
    }
}