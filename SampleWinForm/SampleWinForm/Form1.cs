using StatePrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ZaifApiWrapper;

namespace SampleWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CancellationTokenSource cts;
        static Stateprinter printer = new Stateprinter();

        private async void buttonExecute_Click(object sender, EventArgs e)
        {
            buttonExecute.Enabled = false;
            try
            {
                if (!ValidateChildren(ValidationConstraints.Enabled))
                    return;

                textBoxOutput.Clear();

                var type = (Type)bindingSourceClass.Current;
                var option = new ApiClientOption(textBoxKey.Text, textBoxSecret.Text);
                var api = Activator.CreateInstance(type, new[] { option });

                var method = (MethodInfo)bindingSourceMethod.Current;

                var dt = (DataTable)bindingSourceParameter.DataSource;
                var row =((DataRowView)bindingSourceParameter.Current).Row;

                var args = row.ItemArray.Select(x => x == DBNull.Value ? null : x).ToList();
                cts = new CancellationTokenSource();
                args.Add(cts.Token);

                try
                {
                    var res = await (dynamic)method.Invoke(api, args.ToArray());
                    textBoxOutput.Text += printer.PrintObject(res);
                }
                catch (Exception ex)
                {
                    textBoxOutput.Text += ex + Environment.NewLine;
                }
            }
            finally
            {
                buttonExecute.Enabled = true;
                cts = null;
            }
        }

        static object ChangeType(string value, Type type)
        {
            var t = Nullable.GetUnderlyingType(type) ?? type;
            return string.IsNullOrEmpty(value) ? null : Convert.ChangeType(value, t);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        Type[] _apiTypes = new[] { typeof(PublicApi), typeof(TradeApi), typeof(FutureApi), typeof(LeverageApi) };

        private void Form1_Shown(object sender, EventArgs e)
        {
            bindingSourceClass.DataSource = _apiTypes;

            comboBoxClass.DataSource = bindingSourceClass;
            comboBoxClass.DisplayMember = "Name";
        }

        Type[] _keyRequireTypes = { typeof(TradeApi), typeof(LeverageApi) };

        private void bindingSourceClass_CurrentChanged(object sender, EventArgs e)
        {
            var source = from m in ((Type)bindingSourceClass.Current).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                         where m.GetParameters().All(x => x.ParameterType != typeof(IDictionary<string, string>))
                         select m;

            textBoxKey.Enabled = textBoxSecret.Enabled = _keyRequireTypes.Contains(bindingSourceClass.Current);

            bindingSourceMethod.DataSource = source.ToList();

            comboBoxMethod.DataSource = bindingSourceMethod;
            comboBoxMethod.DisplayMember = "Name";

            errorProvider1.SetError(textBoxKey, null);
            errorProvider1.SetError(textBoxSecret, null);
        }

        DataSet _ds = new DataSet();

        private void bindingSourceMethod_CurrentChanged(object sender, EventArgs e)
        {
            var method = (MethodInfo)bindingSourceMethod.Current;

            DataTable dt;
        
            //メソッド毎に一意なら良い
            var tableName = method.ToString();
            if (_ds.Tables.Contains(tableName))
            {
                dt = _ds.Tables[tableName];
            }
            else
            {
                dt = new DataTable(tableName);

                var parameters = from m in method.GetParameters()
                                 where m.ParameterType != typeof(CancellationToken)
                                 select m;

                dt.Columns.AddRange(
                    parameters.Select(x => new DataColumn(x.Name, Nullable.GetUnderlyingType(x.ParameterType) ?? x.ParameterType)).ToArray());
                
                dt.Rows.Add(parameters.Select(x => x.HasDefaultValue ? x.DefaultValue : GetDefault(x.ParameterType)).ToArray());

                _ds.Tables.Add(dt);
            }

            bindingSourceParameter.DataSource = dt;
            dataGridViewParameter.DataSource = bindingSourceParameter;

            foreach (var col in dataGridViewParameter.Columns.Cast<DataGridViewColumn>())
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        const string PATTERN = "[0-9a-z]{8}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{4}-[0-9a-z]{12}";

        private void textBoxKey_Validating(object sender, CancelEventArgs e)
        {
            if (!textBoxKey.Enabled)
            {
                errorProvider1.SetError(textBoxKey, null);
                return;
            }
            e.Cancel = !Regex.IsMatch(textBoxKey.Text, PATTERN);
            if (e.Cancel)
                errorProvider1.SetError(textBoxKey, "APIキーを入力してください。");
        }

        private void textBoxKey_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxKey, null);
        }

        private void textBoxSecret_Validating(object sender, CancelEventArgs e)
        {
            if (!textBoxSecret.Enabled)
            {
                errorProvider1.SetError(textBoxSecret, null);
                return;
            }
            e.Cancel = !Regex.IsMatch(textBoxSecret.Text, PATTERN);
            if (e.Cancel)
                errorProvider1.SetError(textBoxSecret, "APIシークレットを入力してください。");
        }

        private void textBoxSecret_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(textBoxSecret, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            errorProvider1.SetIconAlignment(textBoxKey, ErrorIconAlignment.MiddleLeft);
            errorProvider1.SetIconAlignment(textBoxSecret, ErrorIconAlignment.MiddleLeft);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (cts != null)
                cts.Cancel();
        }
    }
}
