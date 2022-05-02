using Android.App;
using Android.Content;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Travelity.Droid;
using Travelity.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(GoogleManager))]
namespace Travelity.Droid
{
    [Obsolete]
    // This Class will use the multiple interface from our local interface and From Package from NuGet.

    public class GoogleManager : Java.Lang.Object, IGooglemanager, GoogleApiClient.IConnectionCallbacks, GoogleApiClient.IOnConnectionFailedListener
    {
        public Action<GoogleUser, string> _onLoginComplete;
        public static GoogleApiClient _googleApiClient {get;set;}
        public static GoogleManager _googleManager { get;set;}
        Context _context;

        // Constructor for Google manager.
        public GoogleManager()
        {
            _context = global::Android.App.Application.Context;
            // Google manager = Instance;
            _googleManager = this;
        }

        public void Login(Action<GoogleUser,string> onLoginComplete)
        {
            GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn)
                                                                    .RequestEmail()
                                                                    .Build();
            _googleApiClient = new GoogleApiClient.Builder((_context).ApplicationContext)
                                .AddConnectionCallbacks(this)
                                .AddOnConnectionFailedListener(this)
                                .AddApi(Auth.GOOGLE_SIGN_IN_API, gso)
                                // Add Scopes to get the required API.
                                .AddScope(new Scope(Scopes.Profile))
                                .Build();
            _onLoginComplete = onLoginComplete;
            Intent signInIntent = Auth.GoogleSignInApi.GetSignInIntent(_googleApiClient);
            ((MainActivity)Forms.Context).StartActivityForResult(signInIntent, 1);
            _googleApiClient.Connect();
        }

        public void Logout()
        {
            // GsoBuilder and context is used to sign out of Google.
            var gsoBuilder = new GoogleSignInOptions.Builder(GoogleSignInOptions.DefaultSignIn).RequestEmail();
            GoogleSignIn.GetClient(_context, gsoBuilder.Build())?.SignOut();
            _googleApiClient.Disconnect();
        }


        public void OnAuthCompleted(GoogleSignInResult result)
        {
            if (result.IsSuccess)
            {
                GoogleSignInAccount account = result.SignInAccount;
                _onLoginComplete?.Invoke(new GoogleUser()
                {
                    Name = account.DisplayName,
                    Email = account.Email,
                    Picture = new string((account.PhotoUrl != null ? $"{account.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"))
                   // Picture = new Uri((account.PhotoUrl != null ? $"{account.PhotoUrl}" : $"https://autisticdating.net/imgs/profile-placeholder.jpg"))

                }, string.Empty);
            }
            else
            {
                _onLoginComplete?.Invoke(null, "An error Occurred");
            }
        }

        public void OnConnected(Bundle connectionHint)
        {

        }

        public void OnConnectionSuspended(int Cause)
        {
            _onLoginComplete?.Invoke(null, "Canceled!");
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            _onLoginComplete?.Invoke(null, result.ErrorMessage);
        }

    }
}