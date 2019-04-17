using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odds_Grabber___12play
{
    public partial class Main_Form : Form
    {
        private ChromiumWebBrowser chromeBrowser;
        private string __app = "Odds Grabber";
        private string __app_type = "{edit this}";
        private string __brand_code = "{edit this}";
        private string __brand_color = "#171717";
        private string __url = "www.12play.com";
        private string __website_name = "12play";
        private string __app__website_name = "";
        private string __api_key = "youdieidie";
        private string __running_01 = "igk";
        private string __running_02 = "cmd";
        private string __running_11 = "IGK";
        private string __running_22 = "CMD";
        private int __send = 0;
        private int __r = 23;
        private int __g = 23;
        private int __b = 23;
        private bool __is_close;
        private bool __is_login = false;
        private bool __is_send = false;
        private bool __m_aeroEnabled;
        Form __mainFormHandler;

        // Drag Header to Move
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        // ----- Drag Header to Move

        // Form Shadow
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                __m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!__m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (__m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        // ----- Form Shadow

        public Main_Form()
        {
            InitializeComponent();

            timer_landing.Start();
        }

        // Drag to Move
        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_loader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_brand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----- Drag to Move

        // Click Close
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                __is_close = true;
                Environment.Exit(0);
            }
        }

        // Click Minimize
        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        void ___CloseMessageBox()
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "JavaScript Alert - http://12play.com");

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void timer_close_message_box_Tick(object sender, EventArgs e)
        {
            ___CloseMessageBox();
        }

        private void timer_size_Tick(object sender, EventArgs e)
        {
            __mainFormHandler = Application.OpenForms[0];
            __mainFormHandler.Size = new Size(466, 168);
        }

        // Form Closing
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!__is_close)
            {
                DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            __app__website_name = __app + " - " + __website_name;
            panel1.BackColor = Color.FromArgb(__r, __g, __b);
            panel2.BackColor = Color.FromArgb(__r, __g, __b);
            label_brand.BackColor = Color.FromArgb(__r, __g, __b);
            Text = __app__website_name;
            label_title.Text = __app__website_name;

            InitializeChromium();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (__is_send)
            {
                __is_send = false;
                MessageBox.Show("Telegram Notification is Disabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                __is_send = true;
                MessageBox.Show("Telegram Notification is Enabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timer_landing_Tick(object sender, EventArgs e)
        {
            panel_landing.Visible = false;
            panel_cefsharp.Visible = false;
            pictureBox_loader.Visible = true;
            panel3.Visible = true;
            panel4.Visible = true;
            timer_size.Start();
            timer_landing.Stop();
        }

        public static void ___FlushMemory()
        {
            Process prs = Process.GetCurrentProcess();
            try
            {
                prs.MinWorkingSet = (IntPtr)(300000);
            }
            catch (Exception err)
            {
                // leave blank
            }
        }

        private void timer_flush_memory_Tick(object sender, EventArgs e)
        {
            ___FlushMemory();
        }

        private void SendMyBot(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "772918363:AAHn2ufmP3ocLEilQ1V-IHcqYMcSuFJHx5g";
                string chatId = "@allandrake";
                string text = "-----" + __app__website_name + "-----%0A%0AIP:%20ABC PC%0ALocation:%20Pacific%20Star%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        __Flag();
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        SendMyBot(message);
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void SendABCTeam(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "651945130:AAGMFj-C4wX0yElG2dBU1SRbfrNZi75jPHg";
                string chatId = "@odds_bot_abc_team";
                string text = "Bot:%20-----" + __website_name.ToUpper() + "-----%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20<b>" + message + "</>&parse_mode=html";
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        __Flag();
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        SendABCTeam(message);
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void timer_detect_running_Tick(object sender, EventArgs e)
        {
            //___DetectRunningAsync();
        }

        private async void ___DetectRunningAsync()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://192.168.10.252:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunning2Async();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___DetectRunning2Async()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssitex.com:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunningAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private bool __detect_cmd = false;

        // CefSharp Initialize
        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser("http://www.12play1.com/");
            panel_cefsharp.Controls.Add(chromeBrowser);
            chromeBrowser.AddressChanged += ChromiumBrowserAddressChanged;
        }

        private int __first = 0;
        private int __second = 0;

        // CefSharp Address Changed
        private void ChromiumBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            __url = e.Address.ToString();
            Invoke(new Action(() =>
            {
                panel3.Visible = true;
                panel4.Visible = true;
            }));


            if (e.Address.ToString().Equals("https://www.12play1.com/"))
            {
                __is_login = false;
                Invoke(new Action(() =>
                {
                    chromeBrowser.FrameLoadEnd += (sender_, args) =>
                    {
                        if (args.Frame.IsMain)
                        {
                            Invoke(new Action(() =>
                            {
                                __first++;
                                if (__first == 1)
                                {
                                    chromeBrowser.Load("https://www.12play1.com/sg/launchinggame.html?ap=1&sub=0::sports::G::0");
                                }
                            }));
                        }
                    };
                }));
            }

            if (e.Address.ToString().Contains("http://sport.power5555.com/Main.aspx?"))
            {
                Invoke(new Action(() =>
                {
                    chromeBrowser.FrameLoadEnd += (sender_, args) =>
                    {
                        if (args.Frame.IsMain)
                        {
                            Invoke(new Action(async () =>
                            {
                                __second++;
                                if (__second == 1)
                                {
                                    Invoke(new Action(() =>
                                    {
                                        __is_login = true;
                                        __detect_cmd = true;
                                        chromeBrowser.Load("https://www.12play1.com/sg/launchinggame.html?ap=1&sub=0::sports::M::0");
                                        SendABCTeam("Firing up!");

                                        Task task_01 = new Task(delegate { ___FIRST_RUNNINGAsync(); });
                                        task_01.Start();
                                    }));
                                }
                            }));
                        }
                    };
                }));
            }
            
            if (__detect_cmd)
            {
                if (!e.Address.ToString().Contains("https://www.12play1.com/sg/launchinggame.html?ap=1&sub=0::sports::M::0"))
                {
                    if (e.Address.ToString().Contains("https://www.12play1.com/sg/u_under.html"))
                    {
                        Properties.Settings.Default.______odds_iswaiting_02 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_02)
                        {
                            Properties.Settings.Default.______odds_issend_02 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_22 + " Under Maintenance.");
                        }

                        timer_cmd.Start();
                    }
                    else if (e.Address.ToString().Contains("https://p12.fts368.com/DomainNames/P12/home.aspx"))
                    {
                        if (Properties.Settings.Default.______odds_issend_02)
                        {
                            Properties.Settings.Default.______odds_issend_02 = false;
                            Properties.Settings.Default.Save();

                            SendABCTeam(__running_22 + " Back to Normal.");
                        }
                        
                        Task task_02 = new Task(delegate { ___SECOND_RUNNINGAsync(); });
                        task_02.Start();

                        Properties.Settings.Default.______odds_iswaiting_02 = false;
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }

        private void timer_cmd_Tick(object sender, EventArgs e)
        {
            timer_cmd.Stop();
            chromeBrowser.Load("https://www.12play1.com/sg/launchinggame.html?ap=1&sub=0::sports::M::0");
        }

        // ----- Functions
        // WFT -----
        private async void ___FIRST_RUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel3.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                string start_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00");
                string end_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

                start_time = start_time.Replace("-", "%2F");
                start_time = start_time.Replace(" ", "+");
                start_time = start_time.Replace(":", "%3A");

                end_time = end_time.Replace("-", "%2F");
                end_time = end_time.Replace(" ", "+");
                end_time = end_time.Replace(":", "%3A");

                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies("http://sport.power5555.com/Main.aspx?", true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers["X-Requested-With"] = "XMLHttpRequest";
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                byte[] result = await wc.DownloadDataTaskAsync("http://sport.power5555.com/_View/RMOdds1Gen.ashx?ot=r&sort=0&at=MY&r=" + 1452756003 + "&LID=&_=" + _epoch);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);

                JArray _jo = JArray.Parse(deserializeObject.ToString());
                JToken _count_competition = _jo.SelectToken("[2]");

                string password = __website_name + __running_01 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";

                if (_count_competition.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;
                    
                    // League
                    for (int i = 0; i < _count_competition.Count(); i++)
                    {
                        // Match
                        JToken _count_match = _jo.SelectToken("[2][" + i + "]");
                        for (int ii = 0; ii < _count_match.Count(); ii++)
                        {
                            if (ii != 0)
                            {
                                // Odds
                                JToken _count_odds = _jo.SelectToken("[2][" + i + "][" + ii + "]");
                                for (int iii = 0; iii < _count_odds.Count(); iii++)
                                {
                                    JToken LeagueName = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][2]");
                                    JToken MatchID = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][15]");
                                    JToken HomeScore__AwayScore = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][5]");
                                    string[] HomeScore__AwayScore_Replace = HomeScore__AwayScore.ToString().Split('-');
                                    string HomeScore = HomeScore__AwayScore_Replace[0].Trim();
                                    string AwayScore = HomeScore__AwayScore_Replace[1].Trim();
                                    JToken KickOffDateTime = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][62]");
                                    DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                                    string StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");
                                    JToken MatchTimeHalf = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][11]") + "H";
                                    JToken MatchTimeMinute = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][12]");
                                    String MatchStatus = "";
                                    if (MatchTimeHalf.ToString() == "5H")
                                    {
                                        MatchTimeHalf = "HT";
                                    }
                                    if (MatchTimeHalf.ToString() == "0H")
                                    {
                                        MatchTimeHalf = "1H";
                                    }
                                    if (__is_numeric(MatchTimeMinute.ToString()))
                                    {
                                        if (MatchTimeHalf.ToString() == "2H" && Convert.ToInt32(MatchTimeMinute.ToString()) > 30)
                                        {
                                            MatchTimeHalf = "FT";
                                            MatchStatus = "C";
                                        }
                                        else
                                        {
                                            MatchStatus = "R";
                                        }
                                    }
                                    else
                                    {
                                        MatchStatus = "R";
                                    }
                                    JToken HomeTeamName = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][21]");
                                    JToken AwayTeamName = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][23]");

                                    // -FTHDP-
                                    JToken FTHDP = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][19]");
                                    JToken FTH = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][32]").ToString().Trim().Replace(".", "");
                                    string FTH_Replace = FTH.ToString().Replace("-", "");
                                    if (FTH_Replace.Length == 1 && FTH_Replace != "0")
                                    {
                                        FTH = FTH + "0";
                                    }
                                    FTH = Convert.ToDecimal(FTH) / 100;
                                    JToken FTA = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][33]").ToString().Trim().Replace(".", "");
                                    string FTA_Replace = FTA.ToString().Replace("-", "");
                                    if (FTA_Replace.Length == 1 && FTA_Replace != "0")
                                    {
                                        FTA = FTA + "0";
                                    }
                                    FTA = Convert.ToDecimal(FTA) / 100;

                                    // FTOU
                                    JToken FTOU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][38]");
                                    JToken FTO = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][39]").ToString().Trim().Replace(".", "");
                                    string FTO_Replace = FTO.ToString().Replace("-", "");
                                    if (FTO_Replace.Length == 1 && FTO_Replace != "0")
                                    {
                                        FTO = FTO + "0";
                                    }
                                    FTO = Convert.ToDecimal(FTO) / 100;
                                    JToken FTU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][40]").ToString().Trim().Replace(".", "");
                                    string FTU_Replace = FTU.ToString().Replace("-", "");
                                    if (FTU_Replace.Length == 1 && FTU_Replace != "0")
                                    {
                                        FTU = FTU + "0";
                                    }
                                    FTU = Convert.ToDecimal(FTU) / 100;

                                    // 1x2
                                    JToken FT1 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][28]")), 2);
                                    JToken FT2 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][29]")), 2);
                                    JToken FTX = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][30]")), 2);

                                    // O/E
                                    JToken FTOdd = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][71]").ToString().Trim().Replace(".", "");
                                    string FTOdd_Replace = FTOdd.ToString().Replace("-", "");
                                    if (FTOdd_Replace.Length == 1 && FTOdd_Replace != "0")
                                    {
                                        FTOdd = FTOdd + "0";
                                    }
                                    if (FTOdd.ToString() == "10")
                                    {
                                        FTOdd = "0.99";
                                    }
                                    else
                                    {
                                        FTOdd = Convert.ToDecimal(FTOdd) / 100;
                                    }
                                    JToken FTEven = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][72]").ToString().Trim().Replace(".", "");
                                    string FTEven_Replace = FTEven.ToString().Replace("-", "");
                                    if (FTEven_Replace.Length == 1 && FTEven_Replace != "0")
                                    {
                                        FTEven = FTEven + "0";
                                    }
                                    if (FTEven.ToString() == "10")
                                    {
                                        FTEven = "0.99";
                                    }
                                    else
                                    {
                                        FTEven = Convert.ToDecimal(FTEven) / 100;
                                    }

                                    // -FHHDP-
                                    JToken FHHDP = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][49]");
                                    JToken FHH = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][53]").ToString().Trim().Replace(".", "");
                                    string FHH_Replace = FHH.ToString().Replace("-", "");
                                    if (FHH_Replace.Length == 1 && FHH_Replace != "0")
                                    {
                                        FHH = FHH + "0";
                                    }
                                    FHH = Convert.ToDecimal(FHH) / 100;
                                    JToken FHA = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][54]").ToString().Trim().Replace(".", "");
                                    string FHA_Replace = FHA.ToString().Replace("-", "");
                                    if (FHA_Replace.Length == 1 && FHA_Replace != "0")
                                    {
                                        FHA = FHA + "0";
                                    }
                                    FHA = Convert.ToDecimal(FHA) / 100;

                                    // FHOU
                                    JToken FHOU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][57]");
                                    JToken FHO = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][58]").ToString().Trim().Replace(".", "");
                                    string FHO_Replace = FHO.ToString().Replace("-", "");
                                    if (FHO_Replace.Length == 1 && FHO_Replace != "0")
                                    {
                                        FHO = FHO + "0";
                                    }
                                    FHO = Convert.ToDecimal(FHO) / 100;
                                    JToken FHU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][59]").ToString().Trim().Replace(".", "");
                                    string FHU_Replace = FHU.ToString().Replace("-", "");
                                    if (FHU_Replace.Length == 1 && FHU_Replace != "0")
                                    {
                                        FHU = FHU + "0";
                                    }
                                    FHU = Convert.ToDecimal(FHU) / 100;

                                    // 1x2
                                    JToken FH1 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][44]")), 2);
                                    JToken FH2 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][45]")), 2);
                                    JToken FHX = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][46]")), 2);

                                    ref_match_id = MatchID.ToString();
                                    if (ref_match_id == _last_ref_id)
                                    {
                                        _row_no++;
                                    }
                                    else
                                    {
                                        _row_no = 1;
                                    }

                                    if (_row_no != 1)
                                    {
                                        FTOdd = "0";
                                        FTEven = "0";
                                    }

                                    _last_ref_id = ref_match_id;


                                    //MessageBox.Show("LeagueName: " + LeagueName + "\n" +
                                    //                "MatchID: " + MatchID + "\n" +
                                    //                "_row_no: " + _row_no + "\n" +
                                    //                "HomeTeamName: " + HomeTeamName + "\n" +
                                    //                "HomeTeamName: " + AwayTeamName + "\n" +
                                    //                "HomeScore: " + HomeScore + "\n" +
                                    //                "AwayScore: " + AwayScore + "\n" +
                                    //                "MatchTimeHalf: " + MatchTimeHalf + "\n" +
                                    //                "MatchTimeMinute: " + MatchTimeMinute + "\n" +
                                    //                "KickOffDateTime: " + KickOffDateTime + "\n" +
                                    //                "StatementDate: " + StatementDate + "\n" +
                                    //                "\n-FTHDP-\n" +
                                    //                "FTHDP: " + FTHDP + "\n" +
                                    //                "FTH: " + FTH + "\n" +
                                    //                "FTA: " + FTA + "\n" +
                                    //                "\nFTOU\n" +
                                    //                "FTOU: " + FTOU + "\n" +
                                    //                "FTO: " + FTO + "\n" +
                                    //                "FTU: " + FTU + "\n" +
                                    //                "\n1x2\n" +
                                    //                "FT1: " + FT1 + "\n" +
                                    //                "FT2: " + FT2 + "\n" +
                                    //                "FTX: " + FTX + "\n" +
                                    //                "\nOdd\n" +
                                    //                "FTOdd: " + FTOdd + "\n" +
                                    //                "FTEven: " + FTEven + "\n" +
                                    //                "\n-FHHDP-\n" +
                                    //                "FHHDP: " + FHHDP + "\n" +
                                    //                "FHH: " + FHH + "\n" +
                                    //                "FHA: " + FHA + "\n" +
                                    //                "\nFHOU\n" +
                                    //                "FHOU: " + FHOU + "\n" +
                                    //                "FHO: " + FHO + "\n" +
                                    //                "FHU: " + FHU + "\n" +
                                    //                "\n1x2\n" +
                                    //                "FH1: " + FH1 + "\n" +
                                    //                "FH2: " + FH2 + "\n" +
                                    //                "FHX: " + FHX + "\n");
                                    
                                    var reqparm_ = new NameValueCollection
                                    {
                                        {"source_id", "6"},
                                        {"sport_name", ""},
                                        {"league_name", LeagueName.ToString().Trim()},
                                        {"home_team", HomeTeamName.ToString().Trim()},
                                        {"away_team", AwayTeamName.ToString().Trim()},
                                        {"home_team_score", HomeScore.ToString()},
                                        {"away_team_score", AwayScore.ToString()},
                                        {"ref_match_id", ref_match_id},
                                        {"odds_row_no", _row_no.ToString()},
                                        {"fthdp", (FTHDP.ToString() != "") ? FTHDP.ToString() : "0"},
                                        {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                                        {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                                        {"betidftou", "0"},
                                        {"ftou", (FTOU.ToString() != "") ? FTOU.ToString() : "0"},
                                        {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                                        {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                                        {"betidftoe", "0"},
                                        {"ftodd", (FTOdd.ToString() != "") ? FTOdd.ToString() : "0"},
                                        {"fteven", (FTEven.ToString() != "") ? FTEven.ToString() : "0"},
                                        {"betidft1x2", "0"},
                                        {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                                        {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                                        {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                                        {"specialgame", "0"},
                                        {"fhhdp", (FHHDP.ToString() != "") ? FHHDP.ToString() : "0"},
                                        {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                                        {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                                        {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                                        {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                                        {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                                        {"fhodd", "0"},
                                        {"fheven", "0"},
                                        {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                                        {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                                        {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                                        {"statement_date", StatementDate.ToString()},
                                        {"kickoff_date", KickOffDateTime.ToString()},
                                        {"match_time", MatchTimeHalf.ToString()},
                                        {"match_status", MatchStatus.ToString()},
                                        {"match_minute", MatchTimeMinute.ToString()},
                                        {"api_status", "R"},
                                        {"token_api", token},
                                    };

                                    try
                                    {
                                        WebClient wc_ = new WebClient();
                                        byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                        string responsebody_ = Encoding.UTF8.GetString(result_);
                                        __send = 0;
                                    }
                                    catch (Exception err)
                                    {
                                        __send++;

                                        if (___CheckForInternetConnection())
                                        {
                                            if (__send == 5)
                                            {
                                                SendMyBot(err.ToString());
                                                __is_close = false;
                                                Environment.Exit(0);
                                            }
                                            else
                                            {
                                                await ___TaskWait_Handler(10);
                                                WebClient wc_ = new WebClient();
                                                byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                                string responsebody_ = Encoding.UTF8.GetString(result_);
                                            }
                                        }
                                        else
                                        {
                                            __is_close = false;
                                            Environment.Exit(0);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                __send = 0;
                ___FIRST_NOTRUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_01)
                        {
                            Properties.Settings.Default.______odds_issend_01 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_11 + " Under Maintenance.");
                        }

                        ___FIRST_NOTRUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___FIRST_RUNNINGAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___FIRST_NOTRUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel3.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                string start_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00");
                string end_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

                start_time = start_time.Replace("-", "%2F");
                start_time = start_time.Replace(" ", "+");
                start_time = start_time.Replace(":", "%3A");

                end_time = end_time.Replace("-", "%2F");
                end_time = end_time.Replace(" ", "+");
                end_time = end_time.Replace(":", "%3A");

                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies("http://www.12play1.com", true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                byte[] result = await wc.DownloadDataTaskAsync("http://sport.power5555.com/_View/RMOdds1Gen.ashx?ot=t&wd=&ia=0&sort=0&at=MY&r=688232980&LID=&_=" + _epoch);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);

                JArray _jo = JArray.Parse(deserializeObject.ToString());
                JToken _count_competition = _jo.SelectToken("[2]");

                string password = __website_name + __running_01 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";

                if (_count_competition.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;

                    // League
                    for (int i = 0; i < _count_competition.Count(); i++)
                    {
                        // Match
                        JToken _count_match = _jo.SelectToken("[2][" + i + "]");
                        for (int ii = 0; ii < _count_match.Count(); ii++)
                        {
                            if (ii != 0)
                            {
                                // Odds
                                JToken _count_odds = _jo.SelectToken("[2][" + i + "][" + ii + "]");
                                for (int iii = 0; iii < _count_odds.Count(); iii++)
                                {
                                    JToken LeagueName = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][2]");
                                    JToken MatchID = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][15]");
                                    JToken HomeScore__AwayScore = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][5]");
                                    string[] HomeScore__AwayScore_Replace = HomeScore__AwayScore.ToString().Split('-');
                                    string HomeScore = "";
                                    string AwayScore = "";
                                    JToken KickOffDateTime = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][62]");
                                    DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                                    KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                                    string StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");
                                    JToken MatchTimeHalf = "";
                                    JToken MatchTimeMinute = "";
                                    JToken HomeTeamName = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][21]");
                                    JToken AwayTeamName = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][23]");

                                    // -FTHDP-
                                    JToken FTHDP = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][19]");
                                    JToken FTH = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][32]").ToString().Trim().Replace(".", "");
                                    string FTH_Replace = FTH.ToString().Replace("-", "");
                                    if (FTH_Replace.Length == 1 && FTH_Replace != "0")
                                    {
                                        FTH = FTH + "0";
                                    }
                                    FTH = Convert.ToDecimal(FTH) / 100;
                                    JToken FTA = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][33]").ToString().Trim().Replace(".", "");
                                    string FTA_Replace = FTA.ToString().Replace("-", "");
                                    if (FTA_Replace.Length == 1 && FTA_Replace != "0")
                                    {
                                        FTA = FTA + "0";
                                    }
                                    FTA = Convert.ToDecimal(FTA) / 100;

                                    // FTOU
                                    JToken FTOU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][38]");
                                    JToken FTO = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][39]").ToString().Trim().Replace(".", "");
                                    string FTO_Replace = FTO.ToString().Replace("-", "");
                                    if (FTO_Replace.Length == 1 && FTO_Replace != "0")
                                    {
                                        FTO = FTO + "0";
                                    }
                                    FTO = Convert.ToDecimal(FTO) / 100;
                                    JToken FTU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][40]").ToString().Trim().Replace(".", "");
                                    string FTU_Replace = FTU.ToString().Replace("-", "");
                                    if (FTU_Replace.Length == 1 && FTU_Replace != "0")
                                    {
                                        FTU = FTU + "0";
                                    }
                                    FTU = Convert.ToDecimal(FTU) / 100;

                                    // 1x2
                                    JToken FT1 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][28]")), 2);
                                    JToken FT2 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][29]")), 2);
                                    JToken FTX = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][30]")), 2);

                                    // O/E
                                    JToken FTOdd = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][71]").ToString().Trim().Replace(".", "");
                                    string FTOdd_Replace = FTOdd.ToString().Replace("-", "");
                                    if (FTOdd_Replace.Length == 1 && FTOdd_Replace != "0")
                                    {
                                        FTOdd = FTOdd + "0";
                                    }
                                    if (FTOdd.ToString() == "10")
                                    {
                                        FTOdd = "0.99";
                                    }
                                    else
                                    {
                                        FTOdd = Convert.ToDecimal(FTOdd) / 100;
                                    }
                                    JToken FTEven = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][72]").ToString().Trim().Replace(".", "");
                                    string FTEven_Replace = FTEven.ToString().Replace("-", "");
                                    if (FTEven_Replace.Length == 1 && FTEven_Replace != "0")
                                    {
                                        FTEven = FTEven + "0";
                                    }
                                    if (FTEven.ToString() == "10")
                                    {
                                        FTEven = "0.99";
                                    }
                                    else
                                    {
                                        FTEven = Convert.ToDecimal(FTEven) / 100;
                                    }

                                    // -FHHDP-
                                    JToken FHHDP = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][49]");
                                    JToken FHH = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][53]").ToString().Trim().Replace(".", "");
                                    string FHH_Replace = FHH.ToString().Replace("-", "");
                                    if (FHH_Replace.Length == 1 && FHH_Replace != "0")
                                    {
                                        FHH = FHH + "0";
                                    }
                                    FHH = Convert.ToDecimal(FHH) / 100;
                                    JToken FHA = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][54]").ToString().Trim().Replace(".", "");
                                    string FHA_Replace = FHA.ToString().Replace("-", "");
                                    if (FHA_Replace.Length == 1 && FHA_Replace != "0")
                                    {
                                        FHA = FHA + "0";
                                    }
                                    FHA = Convert.ToDecimal(FHA) / 100;

                                    // FHOU
                                    JToken FHOU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][57]");
                                    JToken FHO = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][58]").ToString().Trim().Replace(".", "");
                                    string FHO_Replace = FHO.ToString().Replace("-", "");
                                    if (FHO_Replace.Length == 1 && FHO_Replace != "0")
                                    {
                                        FHO = FHO + "0";
                                    }
                                    FHO = Convert.ToDecimal(FHO) / 100;
                                    JToken FHU = _jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][59]").ToString().Trim().Replace(".", "");
                                    string FHU_Replace = FHU.ToString().Replace("-", "");
                                    if (FHU_Replace.Length == 1 && FHU_Replace != "0")
                                    {
                                        FHU = FHU + "0";
                                    }
                                    FHU = Convert.ToDecimal(FHU) / 100;

                                    // 1x2
                                    JToken FH1 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][44]")), 2);
                                    JToken FH2 = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][45]")), 2);
                                    JToken FHX = Math.Round(Convert.ToDecimal(_jo.SelectToken("[2][" + i + "][" + ii + "][" + iii + "][46]")), 2);

                                    ref_match_id = MatchID.ToString();
                                    if (ref_match_id == _last_ref_id)
                                    {
                                        _row_no++;
                                    }
                                    else
                                    {
                                        _row_no = 1;
                                    }

                                    if (_row_no != 1)
                                    {
                                        FTOdd = "0";
                                        FTEven = "0";
                                    }

                                    _last_ref_id = ref_match_id;

                                    //MessageBox.Show("LeagueName: " + LeagueName + "\n" +
                                    //                "MatchID: " + MatchID + "\n" +
                                    //                "_row_no: " + _row_no + "\n" +
                                    //                "HomeTeamName: " + HomeTeamName + "\n" +
                                    //                "HomeTeamName: " + AwayTeamName + "\n" +
                                    //                "HomeScore: " + HomeScore + "\n" +
                                    //                "AwayScore: " + AwayScore + "\n" +
                                    //                "MatchTimeHalf: " + MatchTimeHalf + "\n" +
                                    //                "MatchTimeMinute: " + MatchTimeMinute + "\n" +
                                    //                "KickOffDateTime: " + KickOffDateTime + "\n" +
                                    //                "StatementDate: " + StatementDate + "\n" +
                                    //                "\n-FTHDP-\n" +
                                    //                "FTHDP: " + FTHDP + "\n" +
                                    //                "FTH: " + FTH + "\n" +
                                    //                "FTA: " + FTA + "\n" +
                                    //                "\nFTOU\n" +
                                    //                "FTOU: " + FTOU + "\n" +
                                    //                "FTO: " + FTO + "\n" +
                                    //                "FTU: " + FTU + "\n" +
                                    //                "\n1x2\n" +
                                    //                "FT1: " + FT1 + "\n" +
                                    //                "FT2: " + FT2 + "\n" +
                                    //                "FTX: " + FTX + "\n" +
                                    //                "\nOdd\n" +
                                    //                "FTOdd: " + FTOdd + "\n" +
                                    //                "FTEven: " + FTEven + "\n" +
                                    //                "\n-FHHDP-\n" +
                                    //                "FHHDP: " + FHHDP + "\n" +
                                    //                "FHH: " + FHH + "\n" +
                                    //                "FHA: " + FHA + "\n" +
                                    //                "\nFHOU\n" +
                                    //                "FHOU: " + FHOU + "\n" +
                                    //                "FHO: " + FHO + "\n" +
                                    //                "FHU: " + FHU + "\n" +
                                    //                "\n1x2\n" +
                                    //                "FH1: " + FH1 + "\n" +
                                    //                "FH2: " + FH2 + "\n" +
                                    //                "FHX: " + FHX + "\n");

                                    var reqparm_ = new NameValueCollection
                                    {
                                        {"source_id", "6"},
                                        {"sport_name", ""},
                                        {"league_name", LeagueName.ToString().Trim()},
                                        {"home_team", HomeTeamName.ToString().Trim()},
                                        {"away_team", AwayTeamName.ToString().Trim()},
                                        {"home_team_score", HomeScore.ToString()},
                                        {"away_team_score", AwayScore.ToString()},
                                        {"ref_match_id", ref_match_id},
                                        {"odds_row_no", _row_no.ToString()},
                                        {"fthdp", (FTHDP.ToString() != "") ? FTHDP.ToString() : "0"},
                                        {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                                        {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                                        {"betidftou", "0"},
                                        {"ftou", (FTOU.ToString() != "") ? FTOU.ToString() : "0"},
                                        {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                                        {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                                        {"betidftoe", "0"},
                                        {"ftodd", (FTOdd.ToString() != "") ? FTOdd.ToString() : "0"},
                                        {"fteven", (FTEven.ToString() != "") ? FTEven.ToString() : "0"},
                                        {"betidft1x2", "0"},
                                        {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                                        {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                                        {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                                        {"specialgame", "0"},
                                        {"fhhdp", (FHHDP.ToString() != "") ? FHHDP.ToString() : "0"},
                                        {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                                        {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                                        {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                                        {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                                        {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                                        {"fhodd", "0"},
                                        {"fheven", "0"},
                                        {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                                        {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                                        {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                                        {"statement_date", StatementDate.ToString()},
                                        {"kickoff_date", KickOffDateTime.ToString()},
                                        {"match_time", "Upcoming"},
                                        {"match_status", "N"},
                                        {"match_minute", "0"},
                                        {"api_status", "R"},
                                        {"token_api", token},
                                    };

                                    try
                                    {
                                        WebClient wc_ = new WebClient();
                                        byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                        string responsebody_ = Encoding.UTF8.GetString(result_);
                                        __send = 0;
                                    }
                                    catch (Exception err)
                                    {
                                        __send++;

                                        if (___CheckForInternetConnection())
                                        {
                                            if (__send == 5)
                                            {
                                                SendMyBot(err.ToString());
                                                __is_close = false;
                                                Environment.Exit(0);
                                            }
                                            else
                                            {
                                                await ___TaskWait_Handler(10);
                                                WebClient wc_ = new WebClient();
                                                byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                                string responsebody_ = Encoding.UTF8.GetString(result_);
                                            }
                                        }
                                        else
                                        {
                                            __is_close = false;
                                            Environment.Exit(0);
                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                // send igk
                if (!Properties.Settings.Default.______odds_iswaiting_01 && Properties.Settings.Default.______odds_issend_01)
                {
                    Properties.Settings.Default.______odds_issend_01 = false;
                    Properties.Settings.Default.Save();

                    SendABCTeam(__running_11 + " Back to Normal.");
                }

                Properties.Settings.Default.______odds_iswaiting_01 = false;
                Properties.Settings.Default.Save();

                Invoke(new Action(() =>
                {
                    panel3.BackColor = Color.FromArgb(16, 90, 101);
                }));

                __send = 0;
                await ___TaskWait();
                ___FIRST_RUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_01)
                        {
                            Properties.Settings.Default.______odds_issend_01 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_11 + " Under Maintenance.");
                        }

                        ___FIRST_RUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___FIRST_NOTRUNNINGAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        // TBS -----
        private async void ___SECOND_RUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel4.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies(__url, true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var reqparm = new NameValueCollection
                {
                    {"fc", "1"},
                    {"m_accType", "MY"},
                    {"SystemLanguage", "en-US"},
                    {"TimeFilter", "0"},
                    {"m_gameType", "S_"},
                    {"m_SortByTime", "0"},
                    {"m_LeagueList", ""},
                    {"SingleDouble", "double"},
                    {"clientTime", ""},
                    {"c", "A"},
                    {"fav", ""},
                    {"exlist", "0"},
                    {"keywords", ""},
                    {"m_sp", "0"}
                };

                byte[] result = await wc.UploadValuesTaskAsync("https://p12.fts368.com/Member/BetsView/BetLight/DataOdds.ashx", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserializeObject.ToString());
                JToken _running = _jo.SelectToken("$.data");

                string password = __website_name + __running_02 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";

                if (_running.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;
                    // League
                    for (int i = 0; i < _running.Count(); i++)
                    {
                        if (_running.Count() - 1 != i)
                        {
                            JToken LeagueName = _running.SelectToken("[" + i + "][57]");
                            JToken MatchID = _running.SelectToken("[" + i + "][34]").ToString().Substring(0, 8);
                            JToken HomeTeamName = _running.SelectToken("[" + i + "][38]");
                            JToken HomeScore = _running.SelectToken("[" + i + "][7]");
                            JToken AwayTeamName = _running.SelectToken("[" + i + "][39]");
                            JToken AwayScore = _running.SelectToken("[" + i + "][8]");
                            JToken MatchTimeHalf = _running.SelectToken("[" + i + "][53]");
                            String MatchTimeMinute = "";
                            String MatchStatus = "";
                            if (!MatchTimeHalf.ToString().Contains("HT"))
                            {
                                string[] MatchTimeHalf_Replace = MatchTimeHalf.ToString().Split(new string[] { "H" }, StringSplitOptions.None);
                                MatchTimeHalf = MatchTimeHalf_Replace[0].Trim() + "H";
                                MatchTimeMinute = MatchTimeHalf_Replace[1].Trim();
                                if (MatchTimeMinute.Contains("+"))
                                {
                                    string[] MatchTimeMinute_Replace = MatchTimeMinute.ToString().Split(new string[] { "+" }, StringSplitOptions.None);
                                    MatchTimeMinute = MatchTimeMinute_Replace[0].Trim();
                                }
                            }
                            else
                            {
                                MatchTimeHalf = "HT";
                                MatchTimeMinute = "0";
                            }
                            if (MatchTimeHalf.ToString() == "5H")
                            {
                                MatchTimeHalf = "HT";
                            }
                            if (MatchTimeHalf.ToString() == "0H")
                            {
                                MatchTimeHalf = "1H";
                            }
                            if (__is_numeric(MatchTimeMinute.ToString()))
                            {
                                if (MatchTimeHalf.ToString() == "2H" && Convert.ToInt32(MatchTimeMinute.ToString()) > 30)
                                {
                                    MatchTimeHalf = "FT";
                                    MatchStatus = "C";
                                }
                                else
                                {
                                    MatchStatus = "R";
                                }
                            }
                            else
                            {
                                MatchStatus = "R";
                            }
                            JToken KickOffDateTime = _running.SelectToken("[" + i + "][56]");
                            DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "MM/dd", CultureInfo.InvariantCulture);
                            KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                            String StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");
                            JToken FTHDP = _running.SelectToken("[" + i + "][10]");
                            JToken FTH = _running.SelectToken("[" + i + "][40]");
                            JToken FTA = _running.SelectToken("[" + i + "][41]");
                            JToken FTOU = _running.SelectToken("[" + i + "][12]");
                            JToken FTO = _running.SelectToken("[" + i + "][42]");
                            JToken FTU = _running.SelectToken("[" + i + "][43]");
                            JToken FT1 = _running.SelectToken("[" + i + "][17]");
                            JToken FT2 = _running.SelectToken("[" + i + "][18]");
                            JToken FTX = _running.SelectToken("[" + i + "][19]");
                            JToken FHHDP = _running.SelectToken("[" + i + "][14]");
                            JToken FHH = _running.SelectToken("[" + i + "][44]");
                            JToken FHA = _running.SelectToken("[" + i + "][45]");
                            JToken FHOU = _running.SelectToken("[" + i + "][16]");
                            JToken FHO = _running.SelectToken("[" + i + "][46]");
                            JToken FHU = _running.SelectToken("[" + i + "][47]");
                            JToken FH1 = _running.SelectToken("[" + i + "][20]");
                            JToken FH2 = _running.SelectToken("[" + i + "][21]");
                            JToken FHX = _running.SelectToken("[" + i + "][22]");
                            JToken FTOdd = _running.SelectToken("[" + i + "][48]");
                            JToken FTEven = _running.SelectToken("[" + i + "][49]");

                            ref_match_id = MatchID.ToString();
                            if (ref_match_id == _last_ref_id)
                            {
                                _row_no++;
                            }
                            else
                            {
                                _row_no = 1;
                            }

                            _last_ref_id = ref_match_id;

                            //MessageBox.Show("LeagueName: " + LeagueName + "\n" +
                            //                "MatchID: " + MatchID + "\n" +
                            //                "_row_no: " + _row_no + "\n" +
                            //                "HomeTeamName: " + HomeTeamName + "\n" +
                            //                "AwayTeamName: " + AwayTeamName + "\n" +
                            //                "HomeScore: " + HomeScore + "\n" +
                            //                "AwayScore: " + AwayScore + "\n" +
                            //                "MatchTimeHalf: " + MatchTimeHalf + "\n" +
                            //                "MatchTimeMinute: " + MatchTimeMinute + "\n" +
                            //                "MatchStatus: " + MatchStatus + "\n" +
                            //                "KickOffDateTime: " + KickOffDateTime + "\n" +
                            //                "StatementDate: " + StatementDate + "\n" +
                            //                "\n-FTHDP-\n" +
                            //                "FTHDP: " + FTHDP + "\n" +
                            //                "FTH: " + FTH + "\n" +
                            //                "FTA: " + FTA + "\n" +
                            //                "\nFTOU\n" +
                            //                "FTOU: " + FTOU + "\n" +
                            //                "FTO: " + FTO + "\n" +
                            //                "FTU: " + FTU + "\n" +
                            //                "\n1x2\n" +
                            //                "FT1: " + FT1 + "\n" +
                            //                "FT2: " + FT2 + "\n" +
                            //                "FTX: " + FTX + "\n" +
                            //                "\nOdd\n" +
                            //                "FTOdd: " + FTOdd + "\n" +
                            //                "FTEven: " + FTEven + "\n" +
                            //                "\n-FHHDP-\n" +
                            //                "FHHDP: " + FHHDP + "\n" +
                            //                "FHH: " + FHH + "\n" +
                            //                "FHA: " + FHA + "\n" +
                            //                "\nFHOU\n" +
                            //                "FHOU: " + FHOU + "\n" +
                            //                "FHO: " + FHO + "\n" +
                            //                "FHU: " + FHU + "\n" +
                            //                "\n1x2\n" +
                            //                "FH1: " + FH1 + "\n" +
                            //                "FH2: " + FH2 + "\n" +
                            //                "FHX: " + FHX + "\n"
                            //                );

                            var reqparm_ = new NameValueCollection
                            {
                                {"source_id", "5"},
                                {"sport_name", ""},
                                {"league_name", LeagueName.ToString().Trim()},
                                {"home_team", HomeTeamName.ToString().Trim()},
                                {"away_team", AwayTeamName.ToString().Trim()},
                                {"home_team_score", HomeScore.ToString()},
                                {"away_team_score", AwayScore.ToString()},
                                {"ref_match_id", ref_match_id},
                                {"odds_row_no", _row_no.ToString()},
                                {"fthdp", (FTHDP.ToString() != "-999") ? FTHDP.ToString() : "0"},
                                {"fth", (FTH.ToString() != "-999") ? FTH.ToString() : "0"},
                                {"fta", (FTA.ToString() != "-999") ? FTA.ToString() : "0"},
                                {"betidftou", "0"},
                                {"ftou", (FTOU.ToString() != "-999") ? FTOU.ToString() : "0"},
                                {"fto", (FTO.ToString() != "-999") ? FTO.ToString() : "0"},
                                {"ftu", (FTU.ToString() != "-999") ? FTU.ToString() : "0"},
                                {"betidftoe", "0"},
                                {"ftodd", (FTOdd.ToString() != "-999") ? FTOdd.ToString() : "0"},
                                {"fteven", (FTEven.ToString() != "-999") ? FTEven.ToString() : "0"},
                                {"betidft1x2", "0"},
                                {"ft1", (FT1.ToString() != "-999") ? FT1.ToString() : "0"},
                                {"ftx", (FTX.ToString() != "-999") ? FTX.ToString() : "0"},
                                {"ft2", (FT2.ToString() != "-999") ? FT2.ToString() : "0"},
                                {"specialgame", "0"},
                                {"fhhdp", (FHHDP.ToString() != "-999") ? FHHDP.ToString() : "0"},
                                {"fhh", (FHH.ToString() != "-999") ? FHH.ToString() : "0"},
                                {"fha", (FHA.ToString() != "-999") ? FHA.ToString() : "0"},
                                {"fhou", (FHOU.ToString() != "-999") ? FHOU.ToString() : "0"},
                                {"fho", (FHO.ToString() != "-999") ? FHO.ToString() : "0"},
                                {"fhu", (FHU.ToString() != "-999") ? FHU.ToString() : "0"},
                                {"fhodd", "0"},
                                {"fheven", "0"},
                                {"fh1", (FH1.ToString() != "-999") ? FH1.ToString() : "0"},
                                {"fhx", (FHX.ToString() != "-999") ? FHX.ToString() : "0"},
                                {"fh2", (FH2.ToString() != "-999") ? FH2.ToString() : "0"},
                                {"statement_date", StatementDate.ToString()},
                                {"kickoff_date", KickOffDateTime.ToString()},
                                {"match_time", MatchTimeHalf.ToString()},
                                {"match_status", MatchStatus.ToString()},
                                {"match_minute", MatchTimeMinute.ToString()},
                                {"api_status", "R"},
                                {"token_api", token},
                            };

                            try
                            {
                                WebClient wc_ = new WebClient();
                                byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                string responsebody_ = Encoding.UTF8.GetString(result_);
                                __send = 0;
                            }
                            catch (Exception err)
                            {
                                __send++;

                                if (___CheckForInternetConnection())
                                {
                                    if (__send == 5)
                                    {
                                        SendMyBot(err.ToString());
                                        __is_close = false;
                                        Environment.Exit(0);
                                    }
                                    else
                                    {
                                        await ___TaskWait_Handler(10);
                                        WebClient wc_ = new WebClient();
                                        byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                        string responsebody_ = Encoding.UTF8.GetString(result_);
                                    }
                                }
                                else
                                {
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                            }
                        }
                    }
                }

                __send = 0;
                ___SECOND_NOTRUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_02 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_02)
                        {
                            Properties.Settings.Default.______odds_issend_02 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_22 + " Under Maintenance.");
                        }

                        ___SECOND_NOTRUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___SECOND_RUNNINGAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___SECOND_NOTRUNNINGAsync()
        {
            try
            {
                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies(__url, true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                var reqparm = new NameValueCollection
                {
                    {"fc", "1"},
                    {"m_accType", "MY"},
                    {"SystemLanguage", "en-US"},
                    {"TimeFilter", "0"},
                    {"m_gameType", "S_"},
                    {"m_SortByTime", "0"},
                    {"m_LeagueList", ""},
                    {"SingleDouble", "double"},
                    {"clientTime", ""},
                    {"c", "A"},
                    {"fav", ""},
                    {"exlist", "0"},
                    {"keywords", ""},
                    {"m_sp", "0"}
                };

                byte[] result = await wc.UploadValuesTaskAsync("https://p12.fts368.com/Member/BetsView/BetLight/DataOdds.ashx", "POST", reqparm);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserializeObject.ToString());
                JToken _today = _jo.SelectToken("$.today");

                string password = __website_name + __running_02 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";

                if (_today.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;
                    // League
                    for (int i = 0; i < _today.Count(); i++)
                    {
                        JToken LeagueName = _today.SelectToken("[" + i + "][57]");
                        JToken MatchID = _today.SelectToken("[" + i + "][34]").ToString().Substring(0, 8);
                        JToken HomeTeamName = _today.SelectToken("[" + i + "][38]");
                        JToken HomeScore = _today.SelectToken("[" + i + "][7]");
                        JToken AwayTeamName = _today.SelectToken("[" + i + "][39]");
                        JToken AwayScore = _today.SelectToken("[" + i + "][8]");
                        JToken MatchTimeHalf = "";
                        String MatchTimeMinute = "";
                        String MatchStatus = "";
                        JToken KickOffDateTime = _today.SelectToken("[" + i + "][53]");
                        DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "HH:mm", CultureInfo.InvariantCulture);
                        KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                        String StatementDate = KickOffDateTime_Replace.ToString("yyyy-MM-dd 00:00:00");
                        JToken FTHDP = _today.SelectToken("[" + i + "][10]");
                        JToken FTH = _today.SelectToken("[" + i + "][40]");
                        JToken FTA = _today.SelectToken("[" + i + "][41]");
                        JToken FTOU = _today.SelectToken("[" + i + "][12]");
                        JToken FTO = _today.SelectToken("[" + i + "][42]");
                        JToken FTU = _today.SelectToken("[" + i + "][43]");
                        JToken FT1 = _today.SelectToken("[" + i + "][17]");
                        JToken FT2 = _today.SelectToken("[" + i + "][18]");
                        JToken FTX = _today.SelectToken("[" + i + "][19]");
                        JToken FHHDP = _today.SelectToken("[" + i + "][14]");
                        JToken FHH = _today.SelectToken("[" + i + "][44]");
                        JToken FHA = _today.SelectToken("[" + i + "][45]");
                        JToken FHOU = _today.SelectToken("[" + i + "][16]");
                        JToken FHO = _today.SelectToken("[" + i + "][46]");
                        JToken FHU = _today.SelectToken("[" + i + "][47]");
                        JToken FH1 = _today.SelectToken("[" + i + "][20]");
                        JToken FH2 = _today.SelectToken("[" + i + "][21]");
                        JToken FHX = _today.SelectToken("[" + i + "][22]");
                        JToken FTOdd = _today.SelectToken("[" + i + "][48]");
                        JToken FTEven = _today.SelectToken("[" + i + "][49]");

                        ref_match_id = MatchID.ToString();
                        if (ref_match_id == _last_ref_id)
                        {
                            _row_no++;
                        }
                        else
                        {
                            _row_no = 1;
                        }

                        _last_ref_id = ref_match_id;

                        //MessageBox.Show("LeagueName: " + LeagueName + "\n" +
                        //                "MatchID: " + MatchID + "\n" +
                        //                "_row_no: " + _row_no + "\n" +
                        //                "HomeTeamName: " + HomeTeamName + "\n" +
                        //                "AwayTeamName: " + AwayTeamName + "\n" +
                        //                "HomeScore: " + HomeScore + "\n" +
                        //                "AwayScore: " + AwayScore + "\n" +
                        //                "MatchTimeHalf: " + MatchTimeHalf + "\n" +
                        //                "MatchTimeMinute: " + MatchTimeMinute + "\n" +
                        //                "MatchStatus: " + MatchStatus + "\n" +
                        //                "KickOffDateTime: " + KickOffDateTime + "\n" +
                        //                "StatementDate: " + StatementDate + "\n" +
                        //                "\n-FTHDP-\n" +
                        //                "FTHDP: " + FTHDP + "\n" +
                        //                "FTH: " + FTH + "\n" +
                        //                "FTA: " + FTA + "\n" +
                        //                "\nFTOU\n" +
                        //                "FTOU: " + FTOU + "\n" +
                        //                "FTO: " + FTO + "\n" +
                        //                "FTU: " + FTU + "\n" +
                        //                "\n1x2\n" +
                        //                "FT1: " + FT1 + "\n" +
                        //                "FT2: " + FT2 + "\n" +
                        //                "FTX: " + FTX + "\n" +
                        //                "\nOdd\n" +
                        //                "FTOdd: " + FTOdd + "\n" +
                        //                "FTEven: " + FTEven + "\n" +
                        //                "\n-FHHDP-\n" +
                        //                "FHHDP: " + FHHDP + "\n" +
                        //                "FHH: " + FHH + "\n" +
                        //                "FHA: " + FHA + "\n" +
                        //                "\nFHOU\n" +
                        //                "FHOU: " + FHOU + "\n" +
                        //                "FHO: " + FHO + "\n" +
                        //                "FHU: " + FHU + "\n" +
                        //                "\n1x2\n" +
                        //                "FH1: " + FH1 + "\n" +
                        //                "FH2: " + FH2 + "\n" +
                        //                "FHX: " + FHX + "\n"
                        //                );

                        var reqparm_ = new NameValueCollection
                        {
                            {"source_id", "5"},
                            {"sport_name", ""},
                            {"league_name", LeagueName.ToString().Trim()},
                            {"home_team", HomeTeamName.ToString().Trim()},
                            {"away_team", AwayTeamName.ToString().Trim()},
                            {"home_team_score", "0"},
                            {"away_team_score", "0"},
                            {"ref_match_id", MatchID.ToString()},
                            {"odds_row_no", _row_no.ToString()},
                            {"fthdp", (FTHDP.ToString() != "-999") ? FTHDP.ToString() : "0"},
                            {"fth", (FTH.ToString() != "-999") ? FTH.ToString() : "0"},
                            {"fta", (FTA.ToString() != "-999") ? FTA.ToString() : "0"},
                            {"betidftou", "0"},
                            {"ftou", (FTOU.ToString() != "-999") ? FTOU.ToString() : "0"},
                            {"fto", (FTO.ToString() != "-999") ? FTO.ToString() : "0"},
                            {"ftu", (FTU.ToString() != "-999") ? FTU.ToString() : "0"},
                            {"betidftoe", "0"},
                            {"ftodd", "0"},
                            {"fteven", "0"},
                            {"betidft1x2", "0"},
                            {"ft1", (FT1.ToString() != "-999") ? FT1.ToString() : "0"},
                            {"ftx", (FTX.ToString() != "-999") ? FTX.ToString() : "0"},
                            {"ft2", (FT2.ToString() != "-999") ? FT2.ToString() : "0"},
                            {"specialgame", "0"},
                            {"fhhdp", (FHHDP.ToString() != "-999") ? FHHDP.ToString() : "0"},
                            {"fhh", (FHH.ToString() != "-999") ? FHH.ToString() : "0"},
                            {"fha", (FHA.ToString() != "-999") ? FHA.ToString() : "0"},
                            {"fhou", (FHOU.ToString() != "-999") ? FHOU.ToString() : "0"},
                            {"fho", (FHO.ToString() != "-999") ? FHO.ToString() : "0"},
                            {"fhu", (FHU.ToString() != "-999") ? FHU.ToString() : "0"},
                            {"fhodd", "0"},
                            {"fheven", "0"},
                            {"fh1", (FH1.ToString() != "-999") ? FH1.ToString() : "0"},
                            {"fhx", (FHX.ToString() != "-999") ? FHX.ToString() : "0"},
                            {"fh2", (FH2.ToString() != "-999") ? FH2.ToString() : "0"},
                            {"statement_date", StatementDate.ToString()},
                            {"kickoff_date", KickOffDateTime.ToString()},
                            {"match_time", "Upcoming"},
                            {"match_status", "N"},
                            {"match_minute", "0"},
                            {"api_status", "R"},
                            {"token_api", token},
                        };

                        try
                        {
                            WebClient wc_ = new WebClient();
                            byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                            string responsebody_ = Encoding.UTF8.GetString(result_);
                            __send = 0;
                        }
                        catch (Exception err)
                        {
                            __send++;

                            if (___CheckForInternetConnection())
                            {
                                if (__send == 5)
                                {
                                    SendMyBot(err.ToString());
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    WebClient wc_ = new WebClient();
                                    byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                    string responsebody_ = Encoding.UTF8.GetString(result_);
                                }
                            }
                            else
                            {
                                __is_close = false;
                                Environment.Exit(0);
                            }
                        }
                    }
                }

                // send cmd 
                if (!Properties.Settings.Default.______odds_iswaiting_02 && Properties.Settings.Default.______odds_issend_02)
                {
                    Properties.Settings.Default.______odds_issend_02 = false;
                    Properties.Settings.Default.Save();

                    SendABCTeam(__running_22 + " Back to Normal.");
                }

                Properties.Settings.Default.______odds_iswaiting_02 = false;
                Properties.Settings.Default.Save();

                Invoke(new Action(() =>
                {
                    panel4.BackColor = Color.FromArgb(16, 90, 101);
                }));

                __send = 0;
                await ___TaskWait();
                ___SECOND_RUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_02 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_02)
                        {
                            Properties.Settings.Default.______odds_issend_02 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_22 + " Under Maintenance.");
                        }

                        ___SECOND_RUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___SECOND_NOTRUNNINGAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        public static bool ___CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task ___TaskWait()
        {
            Random _random = new Random();
            int _random_number = _random.Next(10, 16);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        private async Task ___TaskWait_Handler(int sec)
        {
            sec++;
            Random _random = new Random();
            int _random_number = _random.Next(sec, sec);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        public bool __is_numeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private void __Flag()
        {
            string _flag = Path.Combine(Path.GetTempPath(), __app + " - " + __website_name + ".txt");
            using (StreamWriter sw = new StreamWriter(_flag, true))
            {
                sw.WriteLine("<<>>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<<>>");
            }
        }
    }
}