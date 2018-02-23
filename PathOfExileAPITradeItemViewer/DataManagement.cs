using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace PathOfExileAPITradeItemViewer
{
    public static class DataManagement
    {
        public static HashSet<string> nextChangeIDs = new HashSet<string>();
        public static TreeNode[] RecurseTree(Dictionary<string,object> dic) //this function maps the JSON data to the property tree view
        {
            List<TreeNode> branch = new List<TreeNode>();
            foreach(KeyValuePair<string,object> kvp in dic)
            {
                TreeNode twig = new TreeNode(kvp.Key);
                if(kvp.Value is string || kvp.Value is bool || kvp.Value is int)
                {
                    twig.Nodes.Add(new TreeNode(kvp.Value.ToString()));
                }
                else if(kvp.Value is Dictionary<string, object>)
                {
                    twig.Nodes.AddRange(RecurseTree((Dictionary<string, object>)kvp.Value));
                }
                else //yes, this is quite confusing
                {
                    IEnumerable enumerable = (kvp.Value as IEnumerable); //if kvp.value is an array
                    if(enumerable != null)
                    {
                        foreach(var item in enumerable) //loop through each element in array
                        {
                            if(item is string || item is bool || item is int)
                            {
                                twig.Nodes.Add(new TreeNode(item.ToString()));
                            }
                            else if(item is Dictionary<string, object>)
                            {
                                twig.Nodes.AddRange(RecurseTree((Dictionary<string, object>)item)); //yes, an element in the array can also be nested
                            }
                            else
                            {
                                IEnumerable en = (item as IEnumerable); //item of an array can be another array
                                foreach (var i in en) //but it is the last option possible
                                    twig.Nodes.Add(new TreeNode(i.ToString()));
                            }
                        }
                    }
                }
                branch.Add(twig);
            }
            return branch.ToArray();
        }
        private static string getJSON(string next_change_id = null)
        {
            string JSONRequest = "http://www.pathofexile.com/api/public-stash-tabs";
            if(next_change_id != null) //the default item page doesn't require any id
            {
                JSONRequest += "?id=" + next_change_id;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(JSONRequest);
            WebResponse response = request.GetResponse();

            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        public static RootObject getRootObject(string next_change_id = null)
        {
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            json_serializer.MaxJsonLength = Int32.MaxValue;
            string result = getJSON(next_change_id);
            RootObject ro = json_serializer.Deserialize<RootObject>(result);
            if (!nextChangeIDs.Contains(ro.next_change_id))
            {
                nextChangeIDs.Add(ro.next_change_id);
            }
            return ro;
        }
    }
}
