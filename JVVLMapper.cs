using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace JVVLMAPPER
{
    public class JVVLMapper : IJVVLMapper
    {


        public T? mapper_JVVL<T>(object objeto, object objeto2, bool all_Or_Emty, bool same)
        {

            var json = JsonConvert.SerializeObject(objeto);

            if (same == true && all_Or_Emty == false)
            {

                var json2 = JsonConvert.DeserializeObject<T>(json);

                return json2;

            }

            var json3 = JsonConvert.SerializeObject(objeto2);

            var jsonItem = JObject.Parse(json);

            var jsonItem2 = JObject.Parse(json3);


            List<string> jsonString = new List<string>();

            StringBuilder jsonStringBuild = new StringBuilder();

            var cont = 0;
            var cont2 = 0;
            var cont3 = 0;
            var cont4 = 0;
            foreach (var item in jsonItem)
            {
                cont4++;
                jsonString.Add(item.Value.ToString());
                if (cont4 == jsonItem2.Count)
                {
                    break;
                }

            }

            foreach (var item2 in jsonItem2)
            {
                cont++;


                if (cont == 1)
                {
                    jsonStringBuild.Append("{");
                }
                if (all_Or_Emty == true)
                {
                    if (cont <= jsonItem.Count)
                    {
                        cont2 = 1;
                        jsonStringBuild.Append("'" + item2.Key.ToString() + "':" + "'" + jsonString[cont - 1] + "'");
                    }
                    if (cont > jsonItem.Count)
                    {
                        cont2 = 1;
                        jsonStringBuild.Append("'" + item2.Key.ToString() + "':" + "'" + item2.Value + "'");
                    }
                }
                if (all_Or_Emty == false)
                {
                    if (jsonString[cont - 1].Equals("False") || jsonString[cont - 1].Equals("True"))
                    {
                        cont3 = 1;
                    }
                    if (!item2.Value.Equals(null) && item2.Value.ToString() != "0" && item2.Value.ToString() != "" && cont3 == 0)
                    {
                        cont2 = 1;
                        jsonStringBuild.Append("'" + item2.Key.ToString() + "':" + "'" + item2.Value + "'");
                    }

                    cont3 = 0;

                    if (item2.Value == null || item2.Value.ToString() == "" || item2.Value.ToString() == "0" || jsonString[cont - 1].Equals("True") || jsonString[cont - 1].Equals("False"))
                    {
                        cont2 = 1;
                        jsonStringBuild.Append("'" + item2.Key.ToString() + "':" + "'" + jsonString[cont - 1] + "'");
                    }

                }

                if (cont < jsonItem2.Count && cont2 == 1)
                {
                    jsonStringBuild.Append(",");
                }
                if (cont == jsonItem2.Count)
                {
                    jsonStringBuild.Append("}");
                }
            }

            jsonStringBuild = jsonStringBuild.Replace(@"'[", "[");
            jsonStringBuild = jsonStringBuild.Replace(@"]'", "]");


            var result = JsonConvert.DeserializeObject<T>(jsonStringBuild.ToString());


            return result;
        }

        //----------------------------------------------------------------------------------------------------------------
    }
}