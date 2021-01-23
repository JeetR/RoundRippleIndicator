## Documentation Ripple ProgressBar (Loading Indicator)

### latest version 1.3.1

#### How to Install from Nuget?

[RippleProgressar on Nuget](https://www.nuget.org/packages/com.jeet.RippleProgressBar)

###### Steps to Install

1. Open Visual Studio
2. Right Click on your project in Solution explorer
3. Click on > Manage Nuget packages
4. Search for > com.jeet.RippleProgressBar
5. Install the latest and greatest version.
6. Sample Usage is shown below.


Here is a simple code sample to help you get started.
 ```
 <customRoundRippleProgressBar:RippleProgressBar
            x:Name="CustomRoundRippleBar"
            MinHeight="200"
            MinWidth="200"
            Visibility="Visible"
            CircleColor="Blue"
            AnimationTimeInMilliSeconds="2000"
            />
you need to add Namespace in order to use this.
eg: xmlns:customRoundRippleProgressBar="clr-namespace:RippleProgressBar;assembly=RippleProgressBar"
you could jsut enter the name of control 'RippleProgressBar' in XAML and visual studio will suggest to import it. 
```

#### The duration of ripples and color of circles can be controlled from XAML or code behind.

### In case of any issues shoot an email at 
#### ranjan.satyajeet2012@gmail.com