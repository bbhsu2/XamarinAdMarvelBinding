#Opera Mediaworks AdMarvel SDK Xamarin Binding
Xamarin Binding for AdMarvel iOS SDK

[SDK Details](https://wiki.operamediaworks.com/display/AMS/AdMarvel+SDK+Key+Integration+Steps)

##Example
```C#

public class CategoryViewController : UITableViewController, IAdMarvelDelegate
{
    protected AdMarvelView adMarvelView;
    public CategoryViewController()
    {
        SetAdMarvelView();
    }

    void SetAdmarvelView()
    {
        adMarvelView = AdMarvelView.CreateView(this);
        adMarvelView.GetAdWithNotification();
    }

    public string PartnerId()
    {
        return "[your ID]";
    }

    public string SiteId()
    {
        return "[your site ID]";
    }

    public UIViewController ApplicationUIViewController()
    {
        return this;
    }

    [Export("getAdSucceeded")]
    public void GetAdSucceeded()
    {
        //handle Ad Succeeded
    }

    [Export("getAdFailed")]
    public void GetAdFailed()
    {
        //handle Show Another Ad
    }
}```
