using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.IO;
using DataGridXamarin.Droid;
using Java.IO;
using System;
using AndroidX.Core.Content;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(implementorType: typeof(SaveAndroid))]

namespace DataGridXamarin.Droid
{
    class SaveAndroid : ISave
    {
        public void Save(string fileName, String contentType, MemoryStream stream)
        {
            string exception = string.Empty;
            string root = null;
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            }
            else
                root = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            if (file.Exists())
            {
                file.Delete();
            }

            try
            {
                FileOutputStream outs = new FileOutputStream(file);
                outs.Write(stream.ToArray());

                outs.Flush();
                outs.Close();
            }
            catch (Exception e)
            {
                exception = e.ToString();
            }
            if (file.Exists() && contentType != "application/html")
            {

                var fileUri = Android.Support.V4.Content.FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".provider", file);
                var intent = new Intent(Intent.ActionView);
                intent.SetData(fileUri);
                intent.AddFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                Android.App.Application.Context.StartActivity(intent);

            }

        }
    }
}
