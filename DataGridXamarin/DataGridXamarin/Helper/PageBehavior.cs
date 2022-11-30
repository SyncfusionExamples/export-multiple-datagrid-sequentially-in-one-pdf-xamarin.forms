using System;
using System.IO;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Exporting;
using Xamarin.Forms;

namespace DataGridXamarin
{
    public class PageBehavior : Behavior<ContentPage>
    {
        #region Fields

        private SfDataGrid sfDataGrid1 = null;
        private SfDataGrid sfDataGrid2 = null;
        private Button button = null;
        #endregion

        #region Overrides

        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            button = bindable.FindByName<Button>("exportPdf");
            sfDataGrid1 = bindable.FindByName<SfDataGrid>("sfGrid");
            sfDataGrid2 = bindable.FindByName<SfDataGrid>("sfGrid1");
            button.Clicked += Button_Clicked;
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            button.Clicked -= Button_Clicked;
            button = null;
            sfDataGrid1 = null;
            sfDataGrid2 = null;
        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            //Create a new PDF document
            var pdfDoc = new PdfDocument();
            //Add a page
            PdfPage page = pdfDoc.Pages.Add();
            DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
            MemoryStream stream = new MemoryStream();
            //Export First Grid
            var exportToPdfGrid = pdfExport.ExportToPdfGrid(this.sfDataGrid1, this.sfDataGrid1.View, new DataGridPdfExportOption()
            {
                FitAllColumnsInOnePage = true,
            }, pdfDoc);

            //Add the layout format for grid pagination
            PdfGridLayoutFormat layoutFormat = new PdfGridLayoutFormat();
            layoutFormat.Layout = PdfLayoutType.Paginate;

            //Draw First grid to the page of PDF document
            PdfLayoutResult result = exportToPdfGrid.Draw(page, new PointF(10, 10), layoutFormat);

            //Export second Grid
            var exportToPdfGrid1 = pdfExport.ExportToPdfGrid(this.sfDataGrid2, this.sfDataGrid2.View, new DataGridPdfExportOption()
            {
                FitAllColumnsInOnePage = true,
            }, pdfDoc);

            //Draw grid to the resultant page of the first grid
            exportToPdfGrid1.Draw(result.Page, new PointF(10, result.Bounds.Height + 20));

            //Save the PDF document as stream
            pdfDoc.Save(stream);

            //Close the PDF document
            pdfDoc.Close(true);

            if (Device.RuntimePlatform == Device.UWP)
                Xamarin.Forms.DependencyService.Get<ISaveWindows>().Save("DataGrid.pdf", "application/pdf", stream);
            else
                Xamarin.Forms.DependencyService.Get<ISave>().Save("DataGrid.pdf", "application/pdf", stream);
        }
        #endregion
    }
}
