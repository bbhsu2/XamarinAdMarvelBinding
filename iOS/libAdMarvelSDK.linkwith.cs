using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libAdMarvelSDK.a", LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, ForceLoad = true, Frameworks="Accounts AdSupport AssetsLibrary AudioToolBox AVFoundation CFNetwork CoreGraphics CoreLocation CoreMedia CoreTelephony EventKit EventKitUI Foundation iAd MediaPlayer MessageUI MobileCoreServices QuartzCore Security Social StoreKit SystemConfiguration UIKit")]
