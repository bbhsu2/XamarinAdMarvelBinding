using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;

namespace AdMarvelBinding
{
    [BaseType (typeof(NSObject))]
    [Model, Protocol]
    public interface AdMarvelDelegate 
    {
        [Abstract]
        [Export ("partnerId")]
        string PartnerId();

        [Abstract]
        [Export ("siteId")]
        string SiteId();

        [Abstract]
        [Export ("applicationUIViewController")]
        UIViewController ApplicationUIViewController();

        [Export("targetingParameters")]
        NSDictionary TargetingParameters();

        [Export("adMarvelViewFrame")]
        RectangleF AdMarvelViewFrame();

        // Return YES if you want to enable testing.  Defaults to testing not enabled (NO).
        [Export("testingEnabled")]
        bool TestingEnabled();

        // Return NO if you want to disable animations when a new ad displayed.  Defaults to animations enabled (YES).
        [Export("animationsEnabled")]
        bool AnimationsEnabled();
       
        // Note that the UI color should be specified using [UIColor colorWithRed:green:blue:alpha:] 
        //since grayscale values will cause issues in some cases.
        [Export("backgroundColor")]
        UIColor BackgroundColor();

        [Export("textAdFontColor")]
        UIColor TextAdFontColor();

        [Export("textAdBackgroundColor")]
        UIColor TextAdBackgroundColor();

        [Export("textAdBorderColor")]
        UIColor TextAdBorderColor();

        [Export("fullScreenToolBarColor")]
        UIColor FullScreenToolBarColor();

        [Export("fullScreenToolBarStyle")]
        UIBarStyle FullScreenToolBarStyle();

        [Export("locationServicesEnabled")]
        bool LocationServicesEnabled();

        [Export("locationObject")]
        CLLocation LocationObject();

        [Export("fullScreenWebViewActivated")]
        void FullScreenWebViewActivated();

        [Export("fullScreenWebViewClosed")]
        void FullScreenWebViewClosed();

        [Export("getAdSucceeded")]
        void GetAdSucceeded();

        [Export("getAdFailed")]
        void GetAdFailed();

        //- (void) handleAdMarvelSDKClick:(NSString*)urlString;
        [Export("handleAdMarvelSDKClick:urlString")]
        void HandleAdMarvelSDKClick(NSString UrlString);

        [Export("adDidExpand")]
        void AdDidExpand();

        [Export("adDidCollapse")]
        void AdDidCollapse();

        [Export("getInterstitialAdSucceeded")]
        void GetInterstitialAdFailed();

        [Export("interstitialActivated")]
        void InterstitialActivated();


        [Export("interstitialClosed")]
        void InterstitialClosed();

        [Export("disableRotations")]
        void DisableRotations();


        [Export("enableRotations")]
        void EnableRotations();

        [Export("adMarvelViewWasClicked")]
        void AdMarvelViewWasClicked();
    }

    public interface IAdMarvelDelegate{}

    [BaseType(typeof(UIView))]
    [Protocol]
    interface AdMarvelView
    {
        // Create an AdMarvel view with an AdMarvelDelegate containing all the desired configuration values.
        // This method only returns a reference to the AdMarvelView so make sure you retain it if needed.
        // We do recommend retaining the AdMarvelView so that you can set the delegate to nil before releasing it.
        [Static, Export("createAdMarvelViewWithDelegate:")]
        AdMarvelView CreateView(IAdMarvelDelegate del);
//        +(AdMarvelView*) createAdMarvelViewWithDelegate:(NSObject<AdMarvelDelegate>*) delegate;
//

        // Optional method for creating an AdMarvel view and prefetching the initial ad.
        // Setting prefetch to YES should speed up the first getAd call if this call can be done a lot earlier in the initialization before getAd is needed.  
        // If getAd is going to be called right after this method then there is no reason to enable prefetching.
        // This should only be used if all the necessary targeting information is available in the delegate and getAd it gauranteed to be called later. 
//        +(AdMarvelView*) createAdMarvelViewWithDelegate:(NSObject<AdMarvelDelegate>*) delegate prefetchAd:(BOOL)prefetchAd;
        [Static, Export("createAdMarvelViewWithDelegate:delegate:")]
        AdMarvelView CreateView(IAdMarvelDelegate del, bool PrefetchAd);
        // Asynchrounsly get an ad from the AdMarvel SDK.  
        // If one isn't available then the view will do nothing, leaving any current ad displayed.  If one is available it will replace the existing ad.  
        // In most cases you will want to use getAdWithNotification unless you are just using a timer to refresh an existing ad and don't care the result of the call.
        
        [Export("getAd")]
        void GetAd();

        // Same behavior as getAd except the delegate will receive a call to either getAdSucceeded or getAdFailed.
        // It is recommended that you use this method always so you can perform conditional logic based on whether an ad was successfully retrieved or not.
        [Export("getAdWithNotification")]
        void GetAdWithNotification();

        // Method for checking if the view is currently taking up the full screen (such as when a full screen web view is launched when an ad is clicked).
        [Export("isInFullScreenMode")]
        bool IsInFullScreenMode();

        // Check what version of the SDK you are using.
        [Export("getSDKVersion")]
        string GetSDKVersion();

        // These functions let the app directly interact with an expandable ad.  In general the app will only want to do this to collapse an ad after the adDidExpand delegate method has been called 
        // and the app needs to update its display (ex: has a popup of it's own to display or is switching tabs).  An app should really only expand an ad if it has previously collapsed it.
        // These functions have no effect if they are called and either there is no ad or the current ad is not an AdMarvel expandable ad.
        [Export("collapseAd")]
        void CollapseAd();

        [Export("expandAd")]
        void ExpandAd();

        // Method for checking if the view is currently displaying an expanded ad
        [Export("isAdExpanded")]
        bool IsAdExpanded();

        // Asynchronously load an interstital.
        // The callbacks getInterstitialAdSucceeded and getInterstitialAdFailed let you know whether this call succeded or not.  Once an interstitial is loaded you will need to call displayInterstitial.
        // It is suggested you set the TARGETING_PARAM_INT_TYPE targeting type correctly so interstitials can be targeted at different events.
        // NOTE: If you are calling this from within applicationDidFinishLaunching the network calls for the ad request will not start executing until the applicationDidFinishLaunching call returns.
        [Export("getInterstitialAd")]
        void GetInterstitialAd();

        // Displays an interstitial if loaded.
        // To load an interstitial you need to have previously called getInterstitialAd.  If an interstitial is availble this will return YES and display it.  If no interstital is loaded it will return NO.
        // NOTE: Since an interstitial could be a video or could click to video it is important that your app always creates a new MPMoviePlayerController each time it plays its own videos.  
        [Export("displayInterstitial")]
        bool DisplayInterstitial();

        // Check if an interstitial is loaded.
        // You should call getInterstitialAd before this check.  This is best used as a fast check before displaying in content (ScreenChange) interstitials that have been pre loaded. 
        // If you are waiting for an interstitial to load (such as for a launch (AppOpen) interstitials) you should listen for the delegate callbacks that let you know if the getInterstitialAd request succeeded.
        [Export("isInterstitialReady")]
        bool IsInterstitialReady();

        // Sets the AdMarvelDelegate for this AdMarvelView.
        // This needs to be used to set the delegate to nil if it is ever going to be dealloced.  This prevents a released delegate from being referenced by the AdMarvelView.
        // The delegate should also be set to nil right before you release the AdMarvelView itself.
//        -(void) setDelegate:(NSObject<AdMarvelDelegate>*) delegate;
        [Export("setDelegate:")]
        void SetDelegate(IAdMarvelDelegate del);

        // These methods let the app indicate whether it considers the AdMarvelView to be visible or not.  This is useful when AdMarvelViews are embedded in a scrolling view and need to be loaded in advance.
        // Calling these methods will let the AdMarvelView notify the creative of the status change. For most ads this is a no op but if an ad supports this feature it can take the appropriate action.  
        // These methods are required when you are using custom offline campaigns for recording the appropriate impression data.  These methods are not needed for interstitial ads however. 
        [Export("adMarvelViewDisplayed")]
        void AdMarvelViewDisplayed();

        [Export("adMarvelViewHidden")]
        void AdMarvelViewHidden();
    }
}