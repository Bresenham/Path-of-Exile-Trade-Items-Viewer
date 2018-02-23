using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PathOfExileAPITradeItemViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bgWorker.RunWorkerAsync();
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument == null)
                updateTreeView(null); //if we don't pass the next_change_id there is not argument
            else
                updateTreeView(e.Argument.ToString());
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine("BGWorker finished.");
            checkChangeIDButtons();
        }

        public void updateTreeView(string next_change_id = null)
        {
            treeViewStash.Invoke(new Action(() => treeViewStash.Nodes.Clear())); //we call this function from another thread so we have to send this action to the main-thread
            statusStripLabel.GetCurrentParent().Invoke(new Action(() => statusStripLabel.Text = "Fetching Root Object..."));

            Dictionary<string, List<Stash>> dList = new Dictionary<string, List<Stash>>();
            RootObject ro = DataManagement.getRootObject(next_change_id);

            statusStripLabel.GetCurrentParent().Invoke(new Action(() => statusStripLabel.Text = "Updating Tree View..."));
            currentChangeID.GetCurrentParent().Invoke(new Action(() => currentChangeID.Text = "Next Change ID: " + ro.next_change_id));
            currentChangeID.Tag = ro.next_change_id;

            for(int i = 0; i < ro.stashes.Count; i++)
            {
                if (ro.stashes[i].items.Count == 0) //only add stash tab if it actually contains items
                    continue;
                List<Stash> currentStashes = new List<Stash>();
                if(i != 0)
                {
                    if(ro.stashes[i].accountName == ro.stashes[i - 1].accountName) //check if previous stash has the same owner
                    {
                        if(dList.TryGetValue(ro.stashes[i].accountName, out currentStashes)) //check if there is already a KeyValuePair for that owner
                        {
                            currentStashes.Add(ro.stashes[i]); //add the current stash to the list of stashes
                        }
                        else //create a new KeyValuePair for this owner
                        {
                            currentStashes = new List<Stash>(); //no value returned so currentStashes is null -> create new instance
                            currentStashes.Add(ro.stashes[i]);
                            dList.Add(ro.stashes[i].accountName, currentStashes);
                        }
                    }
                }
                else //first stash so we create a new KeyValuePair
                {
                    currentStashes.Add(ro.stashes[i]);
                    dList.Add(ro.stashes[i].accountName, currentStashes);
                }
            }
            foreach(KeyValuePair<string, List<Stash>> kvp in dList)
            {
                TreeNode tn = new TreeNode(kvp.Key);
                for(int n = 0; n < kvp.Value.Count; n++)
                {
                    TreeNode tb = new TreeNode(n.ToString());
                    tb.Tag = kvp.Value[n];
                    tn.Nodes.Add(tb);
                }
                treeViewStash.Invoke(new Action(() => treeViewStash.Nodes.Add(tn)));
            }
            statusStripLabel.GetCurrentParent().Invoke(new Action(() => statusStripLabel.Text = ""));
        }

        private void checkChangeIDButtons()
        {
            btnNextChangeID.Enabled = true;
            btnLastChangeID.Enabled = true;
            if(DataManagement.nextChangeIDs.Count > 1) //if there is only one element we have to be on the first one
            {
                if(currentChangeID.Tag.ToString() == DataManagement.nextChangeIDs.First())
                {
                    btnLastChangeID.Enabled = false;
                }
            }
            else
            {
                btnLastChangeID.Enabled = false;
            }
        }

        private void spawnNextBGWorker(object argument = null)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bgWorker_DoWork; //asign the existing event handlers to the new background worker
            bgw.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
            bgw.RunWorkerAsync(argument);
        }

        private void btnLastChangeID_Click(object sender, EventArgs e)
        {
            for(int n = 0; n < DataManagement.nextChangeIDs.Count; n++)
            {
                if(DataManagement.nextChangeIDs.ElementAt(n) == currentChangeID.Tag.ToString())
                {
                    if(n == 1) //load defaul page
                    {
                        spawnNextBGWorker(null);
                    }
                    else
                    {
                        spawnNextBGWorker(DataManagement.nextChangeIDs.ElementAt(n - 2)); //subtract "2" because the API gives back the current element (which contains the change id for the next element)
                    }
                }
            }
        }

        private void btnNextChangeID_Click(object sender, EventArgs e)
        {
            spawnNextBGWorker(currentChangeID.Tag);
        }

        private void treeViewStash_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewItems.Nodes.Clear();
            treeViewProperties.Nodes.Clear();
            picBox.Image = null;
            Stash s = (Stash)e.Node.Tag;
            if(s!= null) //the superior nodes don't have a tag so check that
            {
                foreach(object obj in s.items)
                {
                    TreeNode tn = new TreeNode();
                    tn.Tag = obj;
                    Dictionary<string, object> dict = (Dictionary<string, object>)obj;
                    object itemName;
                    dict.TryGetValue("typeLine", out itemName);
                    string iName = itemName.ToString();

                    if (iName.StartsWith("<"))
                    {
                        iName = iName.Remove(0, 28); //remove strange letters in front of the true item name
                    }

                    tn.Text = iName;
                    treeViewItems.Nodes.Add(tn);
                }
            }
        }

        private void treeViewItems_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Dictionary<string, object> dict = (Dictionary<string, object>)e.Node.Tag;
            object iconUrl;
            dict.TryGetValue("icon", out iconUrl);
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            if(iconUrl != null)
            {
                picBox.Load(iconUrl.ToString());
                treeViewProperties.Nodes.Clear();
                treeViewProperties.Nodes.AddRange(DataManagement.RecurseTree(dict));
            }
        }
    }

    public class Stash
    {
        public string accountName { get; set; }
        public string lastCharacterName { get; set; }
        public string id { get; set; }
        public string stash { get; set; }
        public string stashType { get; set; }
        public List<object> items { get; set; }
        public bool @public { get; set; }
    }

    public class RootObject
    {
        public string next_change_id { get; set; }
        public List<Stash> stashes { get; set; }
    }
}
