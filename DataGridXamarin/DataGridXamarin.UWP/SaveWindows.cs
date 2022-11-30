using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataGridXamarin;
using DataGridXamarin.UWP;
using Windows.Storage;
using Windows.Storage.Pickers;
using Xamarin.Forms;

[assembly: Dependency(typeof(SaveWindows))]
namespace DataGridXamarin.UWP
{
    public class SaveWindows : ISaveWindows
    {
        public async Task Save(string filename, string contentType, MemoryStream stream)
        {
            if (Device.Idiom != TargetIdiom.Desktop)
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile outFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (Stream outStream = await outFile.OpenStreamForWriteAsync())
                {
                    outStream.Write(stream.ToArray(), 0, (int)stream.Length);
                }
                if (contentType != "application/html")
                    await Windows.System.Launcher.LaunchFileAsync(outFile);
            }
            else
            {
                StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFile outFile = await local.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                using (Stream outStream = await outFile.OpenStreamForWriteAsync())
                {
                    outStream.Write(stream.ToArray(), 0, (int)stream.Length);
                }

                //Launch the saved file for viewing in default viewer.
                await Windows.System.Launcher.LaunchFileAsync(outFile);
            }
        }
    }
}
