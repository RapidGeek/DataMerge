using Ganss.Text;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMerge
{
    enum PreviewSize
    {
        None,
        Hundred,
        Thousand,
        TenThousand,
        HundredThousand,
        All
    }

    public partial class frmDataMerge : Form
    {
        private static PreviewSize theirPreviewSize = PreviewSize.None;
        private static PreviewSize ourPreviewSize = PreviewSize.None;
        private static bool theirDataLoaded = false;
        private static bool ourDataLoaded = false;
        private AhoCorasick ahoCorasick;
        private BackgroundWorker bwTheirDataWorker = new BackgroundWorker();
        private BackgroundWorker bwOurDataWorker = new BackgroundWorker();
        private BackgroundWorker bwTheirDataPreviewWorker = new BackgroundWorker();
        private BackgroundWorker bwOurDataPreviewWorker = new BackgroundWorker();
        private DataReader reader = new DataReader();

        public frmDataMerge()
        {
            InitializeComponent();
            bwTheirDataWorker.DoWork += BwTheirDataWorker_DoWork;
            bwOurDataWorker.DoWork += BwOurDataWorker_DoWork;
            bwTheirDataPreviewWorker.WorkerSupportsCancellation = true;
            bwTheirDataPreviewWorker.DoWork += BwTheirDataPreviewWorker_DoWork;
            bwOurDataPreviewWorker.WorkerSupportsCancellation = true;
            bwOurDataPreviewWorker.DoWork += BwOurDataPreviewWorker_DoWork;
            btnSaveMergedData.Enabled = false;
            lbTheirDataPreviewSize.SelectedIndex = (int)PreviewSize.Hundred;
            lbOurDataPreviewSize.SelectedIndex = (int)PreviewSize.Hundred;
        }

        private void BwOurDataPreviewWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwOurDataPreviewWorker.IsBusy)
            {
                bwOurDataPreviewWorker.CancelAsync();
            }

            UpdateTheirDataPreview(OurPreviewSize);
        }

        private void BwTheirDataPreviewWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if(bwTheirDataPreviewWorker.CancellationPending)
            {
                e.Cancel = true;
            }

            UpdateTheirDataPreview(TheirPreviewSize);
        }

        private void BwOurDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OurDataLoaded = false;
            FileInfo ourFileInfo = new FileInfo(ourDataDialog.FileName);
            reader.ClearOurDataList();
            reader.PopulateOurDataList(ourFileInfo);
            lbTheirDataPreviewSize.PerformSafely(() => ourPreviewSize = (PreviewSize)lbOurDataPreviewSize.SelectedIndex);
            UpdateOurDataPreview(ourPreviewSize);
            OurDataLoaded = true;
        }

        private void BwTheirDataWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            TheirDataLoaded = false;
            FileInfo theirFileInfo = new FileInfo(theirDataDialog.FileName);
            reader.PopulateTheirDataList(theirFileInfo);
            
            lbTheirDataPreviewSize.PerformSafely(() => theirPreviewSize = (PreviewSize)lbTheirDataPreviewSize.SelectedIndex);
            UpdateTheirDataPreview(theirPreviewSize);
            TheirDataLoaded = true;
        }

        private void btnTheirData_Click(object sender, EventArgs e)
        {
            var result = theirDataDialog.ShowDialog();

            if(result == DialogResult.OK)
            {
                bwTheirDataWorker.RunWorkerAsync();
            }
        }

        private void btnOurData_Click(object sender, EventArgs e)
        {
            var result = ourDataDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                bwOurDataWorker.RunWorkerAsync();
            }
        }

        private void btnSaveMergedData_Click(object sender, EventArgs e)
        {
            // Validate that we have the data we need
            if(!(TheirDataLoaded && OurDataLoaded))
            {
                MessageBox.Show(
                    "Please wait until both data sets are finished loading.",
                    "Loading Data",
                    MessageBoxButtons.OK);
                return;
            }

            var result = saveMergedData.ShowDialog();

            if(result == DialogResult.OK)
            {
                Stopwatch sw = new Stopwatch();
                sw.Restart();
                FileInfo mergedFileInfo = new FileInfo(saveMergedData.FileName);

                using (var output = mergedFileInfo.CreateText())
                {
                    var theirDataList = reader.TheirDataList
                        .Cast<TheirClickAndImpressionData>()
                        .ToList();
                    var ourDataList = reader.OurDataList
                        .Cast<OurNameAndTrackingIdData>()
                        .Where(o => !string.IsNullOrWhiteSpace(o.TrackingID))
                        .ToList();

                    // Populate Aho-Corasick
                    ahoCorasick = new AhoCorasick(ourDataList.Select(o => o.TrackingID));
                    var acDictionary = new ConcurrentDictionary<string, OurNameAndTrackingIdData>();
                    ourDataList.ForEach(o => acDictionary.TryAdd(o.TrackingID, o));

                    StringBuilder sb = new StringBuilder();

                    Parallel.ForEach(theirDataList, theirData =>
                    {
                        var results = ahoCorasick.Search(theirData.IndexString);
                        foreach(var item in results)
                        {
                            var value = acDictionary[item.Word];
                            {
                                lock (output)
                                {
                                    sb.AppendLine($"{value.ID},{value.Name},{value.TrackingID},{theirData.IndexString},{theirData.Clicks},{theirData.Impressions},{theirData.DateStamp}");
                                }
                            }
                        }
                    });
                    output.WriteLine(sb.ToString());
                }
                sw.Stop();
                MessageBox.Show(
                    $"This algorithn took {sw.ElapsedMilliseconds} ms.",
                    "Algorithm Performance");
            }
        }

        private void UpdateTheirDataPreview(PreviewSize previewSize)
        {
            int newSize;

            switch(previewSize)
            {
                case PreviewSize.None: newSize = 0; break;
                case PreviewSize.Hundred: newSize = 100; break;
                case PreviewSize.Thousand: newSize = 1000; break;
                case PreviewSize.TenThousand: newSize = 10000; break;
                case PreviewSize.HundredThousand: newSize = 100000; break;
                case PreviewSize.All: newSize = int.MaxValue; break;
                default: newSize = 0; break;
            }

            dgvTheirData.PerformSafely(() => dgvTheirData.Columns.Clear());
            foreach (var header in reader.TheirHeaders)
            {
                dgvTheirData.PerformSafely(() =>
                    dgvTheirData.Columns.Add(header.Replace(" ", ""), header));
            }

            dgvTheirData.PerformSafely(() => dgvTheirData.Rows.Clear());
            for (int i = 0; i < newSize && i < reader.TheirDataList.Count; i++)
            {
                if (bwTheirDataPreviewWorker.CancellationPending)
                {
                    break;
                }

                // cast to the correct type
                var data = reader.TheirDataList[i] as TheirClickAndImpressionData;
                dgvTheirData.PerformSafely(() =>
                    dgvTheirData.Rows.Add(
                        data.IndexString,
                        data.Clicks,
                        data.Impressions,
                        data.DateStamp.ToShortDateString()));
            }
        }

        private void UpdateOurDataPreview(PreviewSize previewSize)
        {
            int newSize;

            switch (previewSize)
            {
                case PreviewSize.None: newSize = 0; break;
                case PreviewSize.Hundred: newSize = 100; break;
                case PreviewSize.Thousand: newSize = 1000; break;
                case PreviewSize.TenThousand: newSize = 10000; break;
                case PreviewSize.HundredThousand: newSize = 100000; break;
                case PreviewSize.All: newSize = int.MaxValue; break;
                default: newSize = 0; break;
            }

            dgvOurData.PerformSafely(() => dgvOurData.Columns.Clear());
            foreach (var header in reader.OurHeaders)
            {
                dgvOurData.PerformSafely(() =>
                    dgvOurData.Columns.Add(header.Replace(" ", ""), header));
            }

            dgvOurData.PerformSafely(() => dgvOurData.Rows.Clear());
            for (int i = 0; i < newSize && i < reader.OurDataList.Count; i++)
            {
                if (bwOurDataPreviewWorker.CancellationPending)
                {
                    break;
                }

                // cast to the correct type
                var data = reader.OurDataList[i] as OurNameAndTrackingIdData;
                dgvOurData.PerformSafely(() =>
                    dgvOurData.Rows.Add(
                        data.ID,
                        data.Name,
                        data.TrackingID));
            }
        }

        private bool TheirDataLoaded
        {
            get { return theirDataLoaded; }
            set
            {
                theirDataLoaded = value;
                btnTheirData.PerformSafely(() => btnTheirData.Enabled = value);
                btnSaveMergedData.PerformSafely(() => btnSaveMergedData.Enabled = TheirDataLoaded && OurDataLoaded);
            }
        }

        private bool OurDataLoaded
        {
            get { return ourDataLoaded; }
            set
            {
                ourDataLoaded = value;
                btnOurData.PerformSafely(() => btnOurData.Enabled = value);
                btnSaveMergedData.PerformSafely(() => btnSaveMergedData.Enabled = TheirDataLoaded && OurDataLoaded);
            }
        }

        private PreviewSize TheirPreviewSize
        {
            get { return theirPreviewSize; }
            set
            {
                theirPreviewSize = value;

                if (bwTheirDataPreviewWorker.IsBusy)
                {
                    bwTheirDataPreviewWorker.CancelAsync();
                }

                while (bwTheirDataPreviewWorker.IsBusy)
                    Application.DoEvents();

                bwTheirDataPreviewWorker.RunWorkerAsync();
            }
        }

        private PreviewSize OurPreviewSize
        {
            get { return ourPreviewSize; }
            set
            {
                ourPreviewSize = value;
                if (bwOurDataPreviewWorker.IsBusy)
                {
                    bwOurDataPreviewWorker.CancelAsync();
                }

                while (bwOurDataPreviewWorker.IsBusy)
                    Application.DoEvents();

                bwOurDataPreviewWorker.RunWorkerAsync();
            }
        }

        private void lbTheirDataPreviewSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            TheirPreviewSize = (PreviewSize)lbTheirDataPreviewSize.SelectedIndex;
        }

        private void lbOurDataPreviewSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            OurPreviewSize = (PreviewSize)lbOurDataPreviewSize.SelectedIndex;
        }
    }
}
