using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText incomePerHourEditText;
        EditText workHourPerDayEditText;
        EditText taxRateEditText;
        EditText savingsRateEditText;

        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView annualSavingsTextView;
        TextView spendableIncomeTextView;

        Button calculateButton;
        RelativeLayout resultLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            ConnectViews();
        }

        void ConnectViews()
        {
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHourPerDayEditText = FindViewById<EditText>(Resource.Id.workHoursEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            savingsRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);

            workSummaryTextView = FindViewById<TextView>(Resource.Id.workSummaryTextView);
            grossIncomeTextView = FindViewById<TextView>(Resource.Id.grossIncomeTextView);
            taxPayableTextView = FindViewById<TextView>(Resource.Id.taxPayableTextView);
            annualSavingsTextView = FindViewById<TextView>(Resource.Id.annualSavingsTextView);
            spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spendableIncomeTextView);

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;
        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            //Take user inputs
            double incomePerHour = double.Parse(incomePerHourEditText.Text);
            double workHourPerDay = double.Parse(workHourPerDayEditText.Text);
            double taxRate = double.Parse(taxRateEditText.Text);
            double savingsRate = double.Parse(savingsRateEditText.Text);

            //Calculations
            double annualWorkHourSummary = workHourPerDay * 5 * 50; // 52 weeks/year minus 2 vacation weeks
            double annualIncome = incomePerHour * workHourPerDay * 5 * 50;
            double taxPayable = (taxRate / 100) * annualIncome;
            double annualSavings = (savingsRate / 100) * annualIncome;
            double spendableIncome = annualIncome - annualSavings - taxPayable;

            //Display results
            grossIncomeTextView.Text = annualIncome.ToString("#,##") + " USD";
            workSummaryTextView.Text = annualWorkHourSummary.ToString("#,##") + " HRS";
            taxPayableTextView.Text = taxPayable.ToString("#,##") + " USD";
            annualSavingsTextView.Text = annualSavings.ToString("#,##") + " USD";
            spendableIncomeTextView.Text = spendableIncome.ToString("##,#") + "  USD";

            resultLayout.Visibility = Android.Views.ViewStates.Visible;
        }
    }
}