using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace DatasetDownloader.BusinessLogic.Filetypes
{
    public class JsonExtractor
    {
        private IDataExtractions extractions { get; set; }
        public JsonExtractor(IDataExtractions extractions)
        {
            this.extractions = extractions;
        }

        public void GetJsonExtraction(string data, string filename)
        {
            string header = string.Empty;
            var jsondata = new Dictionary<string, List<string>>();
            this.GetFieldNamesEnumerate(data, jsondata);
            List<string> csvfile = new List<string>();
            foreach (var i in jsondata)
            {
                header += (header.Any() ? ";" : string.Empty) + i.Key;
            }

            csvfile.Add(header);
            if (jsondata.Any())
            {
                NewMethod(jsondata, csvfile);
            }

            this.extractions.ExecuteDataInformationExtraction(csvfile.ToArray(), ";", filename);
        }

        private static void NewMethod(Dictionary<string, List<string>> jsondata, List<string> csvfile)
        {
            for (int i = 0; i < jsondata.First().Value.Count; i++)
            {
                string value = string.Empty;
                foreach (var j in jsondata)
                {
                    if (j.Value.Count() > i)
                    {
                        value += (value.Any() ? ";" : string.Empty) + j.Value[i];
                    }
                }

                csvfile.Add(value);
            }
        }

        private void GetFieldNamesEnumerate(dynamic input, Dictionary<string, List<string>> jsondata)
        {
            try
            {
                input = Newtonsoft.Json.JsonConvert.DeserializeObject(input);
                input = input.Root ?? input.First ?? input;
                if (input != null)
                {
                    bool isArray = true;
                    this.ValidateInputObject(ref input, ref isArray);
                    this.EnumerateChildrenFirstStage(input, jsondata);
                }
            }
            catch
            {
            }
        }

        private void ValidateInputObject(ref dynamic input, ref bool isArray)
        {
            while (isArray)
            {
                input = input.First ?? input;
                if (input.GetType() == typeof(JObject) || input.GetType() == typeof(JValue) || input == null)
                    isArray = false;
            }
        }

        private void EnumerateChildrenFirstStage(dynamic input, Dictionary<string, List<string>> jsondata)
        {
            if (input.GetType() == typeof(JObject))
            {
                JObject inputJson = Newtonsoft.Json.Linq.JObject.FromObject(input);
                var children = inputJson.Children();
                this.EnumerateChilderenSecondStage(jsondata, children);
            }
        }

        private void EnumerateChilderenSecondStage(Dictionary<string, List<string>> jsondata, JEnumerable<JToken> children)
        {
            foreach (var i in children)
            {
                string name = ((JProperty)i).Name;
                JToken value = ((JProperty)i).Value;
                foreach (JToken tst in value.Children())
                {
                    if (tst.GetType().Name == "JObject")
                    {
                        this.GetFieldNames12(tst, jsondata);
                    }
                }
            }
        }

        public void GetFieldNames12(dynamic input, Dictionary<string, List<string>> jsondata)
        {
            try
            {
                if (input.GetType() == typeof(JObject))
                {
                    JObject inputJson = input;
                    var properties = inputJson.Properties();
                    foreach (var property in properties)
                    {
                        this.EnumerateChildrenThirdStage(jsondata, property);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void EnumerateChildrenThirdStage(Dictionary<string, List<string>> jsondata, JProperty property)
        {
            if (property.Value.GetType() == typeof(JObject) || property.Value.GetType() == typeof(JArray))
            {
                GetFieldNames12(property.Value.ToString(), jsondata);
            }
            else
            {
                if (!jsondata.ContainsKey(property.Name))
                {
                    jsondata.Add(property.Name, new List<string>());
                }

                jsondata[property.Name].Add(property.Value.ToString());
            }
        }
    }
}
