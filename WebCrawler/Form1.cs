using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebCrawler
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			tbOutput.Text = string.Empty;
			string[] strURLs = tbInput.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			foreach (string url in strURLs)
			{
				if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
				{
					HttpWebRequest wrGet = WebRequest.Create(url) as HttpWebRequest;
					try
					{
						HttpWebResponse wrRespond = wrGet.GetResponse() as HttpWebResponse;
						tbOutput.Text += wrRespond.StatusCode.ToString() + " : " + url + Environment.NewLine;
					}
					catch (WebException ex)
					{
						HttpWebResponse errorResponse = ex.Response as HttpWebResponse;
						tbOutput.Text += errorResponse.StatusCode.ToString() + " : " + url;
					}
					catch (Exception exBad)
					{
						tbOutput.Text = "A Bad error occured:" + exBad.ToString();
					}
				}
				else
				{
					tbOutput.Text += "Not in a valid format:" + url + Environment.NewLine;
				}
			}
		}
	}
}
