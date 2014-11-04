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

    public RectangleF AdMarvelViewFrame()
    {
        return new RectangleF(0.0f, 0.0f, UIScreen.MainScreen.Bounds.Width, 50.0f);
    }


    public UIViewController ApplicationUIViewController()
    {
        return this;
    }

    public void GetAdSucceeded()
    {
        //handle Ad Succeeded
    }

    public void GetAdFailed()
    {
        //handle Show Another Ad
    }
}```
