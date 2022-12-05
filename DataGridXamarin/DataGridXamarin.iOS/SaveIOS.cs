using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Foundation;
using DataGridXamarin.iOS;
using QuickLook;
using Xamarin.Forms;
using System.IO;

[assembly: Dependency(typeof(SaveIOS))]
namespace DataGridXamarin.iOS
{
    public class SaveIOS : ISave
    {
        public void Save(string filename, string contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string filePath = Path.Combine(path, filename);
            try
            {
                FileStream fileStream = File.Open(filePath, FileMode.Create);
                stream.Position = 0;
                stream.CopyTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }

            if (contentType == "application/html" || exception != string.Empty)
                return;

            UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            while (currentController.PresentedViewController != null)
                currentController = currentController.PresentedViewController;
            UIView currentView = currentController.View;

            QLPreviewController previewController = new QLPreviewController();
            QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
            previewController.DataSource = new PreviewControllerDS(item);

            currentController.PresentViewController((UIViewController)previewController, true, (Action)null);
        }
    }

    internal class PreviewControllerDS : QLPreviewControllerDataSource
    {
        private QLPreviewItem item;

        public PreviewControllerDS(QLPreviewItem item)
        {
            this.item = item;
        }

        public override IQLPreviewItem GetPreviewItem(QLPreviewController controller, nint index)
        {
            return item;
        }

        public override nint PreviewItemCount(QLPreviewController controller)
        {
            return (nint)1;
        }
    }

    internal class QLPreviewItemBundle : QLPreviewItem
    {
        string _fileName, _filePath;
        public QLPreviewItemBundle(string fileName, string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
        }

        public override string ItemTitle
        {
            get
            {
                return _fileName;
            }
        }

        public override NSUrl ItemUrl
        {
            get
            {
                var documents = NSBundle.MainBundle.BundlePath;
                var lib = Path.Combine(documents, _filePath);
                var url = NSUrl.FromFilename(lib);
                return url;
            }
        }
    }
}