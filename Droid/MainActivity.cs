using Android.App;
using Android.OS;
using Android.Widget;
using System.IO;
using System.Threading.Tasks;
using CopyAssetsProject.Utils;

namespace CopyAssetsProject.Droid
{
    [Activity(Label = "CopyAssetsProject", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Start the copy assets button.
        /// </summary>
        private Button _copyAssetsButton;

        /// <summary>
        /// Used to display the database path.
        /// </summary>
        private TextView _pathTextView;

        /// <summary>
        /// The name of the database.
        /// </summary>
        private const string databaseName = "database.db3";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _copyAssetsButton = FindViewById<Button>(Resource.Id.CopyButton);

            _pathTextView = FindViewById<TextView>(Resource.Id.PathTextView);
        }

		protected override void OnResume()
		{
            base.OnResume();
            _copyAssetsButton.Click += CopyAssetsButton_Click;
		}

		/// <summary>
		/// Copy assets button action.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void CopyAssetsButton_Click(object sender, System.EventArgs e)
        {
            DeployDatabaseFromAssetsAsync().FireAndForgetSafeAsync();
            _pathTextView.Text = "The database path : \n" + GetDefaultFolderPath();
        }

        /// <summary>
        /// Deploy the database from the assets folder to the application app.
        /// </summary>
        public async Task DeployDatabaseFromAssetsAsync()
        {
            // Android application default folder.
            var dbFile = GetDefaultFolderPath();

            // Check if the file already exists.
            if (!File.Exists(dbFile))
            {
                using (FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    // Assets is comming from the current context.
                    await Assets.Open(databaseName).CopyToAsync(writeStream);
                }
            }
        }

        /// <summary>
        /// Gets the default folder path.
        /// </summary>
        /// <returns>The default folder path.</returns>
        private string GetDefaultFolderPath()
        {
            var appFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var dbFile = Path.Combine(appFolder, databaseName);

            return dbFile;
        }

		protected override void OnPause()
		{
            base.OnPause();
            _copyAssetsButton.Click -= CopyAssetsButton_Click;
		}
	}
}