using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Reflection;
using WeddingStoreData;

namespace WeddingStoreDesktop.Functions
{
    public class AddImageToResource
    {
        public static void AddImage(string name, Image myImage)
        {
            //using (ResXResourceReader rsRed = new ResXResourceReader(WeddingStoreData.WeddingStoreResource.ResourceManager.BaseName))
            //{
            //    IDictionaryEnumerator e = rsRed.GetEnumerator();
            //    while (e.MoveNext())
            //    {
            //        string ahihi = e.Value.ToString();
            //    }
            //}

            //ResXResourceWriter myResWritter = new ResXResourceWriter(@".\WeddingStoreResource.resx");
            //using (ResXResourceWriter myResWritter = new ResXResourceWriter(Properties.Resources.ResourceManager.BaseName))
            //{
            //    myResWritter.AddResource(name, myUrl);
            //    myResWritter.Generate();
            //}

            //ResXResourceWriter RwX = new ResXResourceWriter("WeddingStoreData.WeddingStoreResource.resx");
            //RwX.AddResource("ahihi", "Texssssss");
            //RwX.Generate();
            //RwX.Close();

            //ResXResourceWriter res = new ResXResourceWriter(Assembly.GetExecutingAssembly().GetManifestResourceStream("WeddingStoreData.WeddingStoreResource.resx"));
            using (ResXResourceWriter res = new ResXResourceWriter("WeddingStoreData.WeddingStoreData.WeddingStoreResource.resx"))
            {
                res.AddResource(name, myImage);
                res.Generate();
            }
           
        }
    }
}
