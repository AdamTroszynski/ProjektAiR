   M S B A M L   `   `   `  ????H  DMultiView App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MultiViewApp.ViewMultiViewApp.View  5      ?$clr-namespace:MultiViewApp.ViewModelMultiViewApp.ViewModel  5          MultiViewApp.View.View4   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      R NOxyPlot.Wpf, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.1.2.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5      -localclr-namespace:MultiViewApp.View   5      /vm$clr-namespace:MultiViewApp.ViewModel   5        ? DataContext  5      ,   &MultiViewApp.ViewModel.View4_ViewModel  5   
   .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??* ?6   ?? 5      $
??Auto ?6   ?? 5      $
??Auto ?6   .+?5   
   ?? $??0??6   $??Wykres??5      $??Center=?5      $
??Left=?5      $
??15,5q?5      $??40(?5      $??DemiBold"?6"   ???#63255c5        OxyPlot.Wpf.PlotView  5   
   -   5          Name$ DataPlotView??5      $??1??5       $??1??6$   $??0,0,10,0q?5!        OxyPlot.Wpf.PlotViewBase   Model 5"      ?? *DataPlotModel+? 5%   
   $??2??6   $??1??6    ? ColumnDefinitions 5&      ?? 5'      $
??Auto ?6#   ?? 5(      $??* ?6#   ?? 5)      $
??Auto ?6#   ?? 5*      $
??Auto ?6#   ?? 5+      $
??Auto ?6#   .+?5-      ?? $??0??5.      $??
20,10,0,10q?5/      $??IP address: ??50      $??Center=?6%   ?? 51      $??1??52      $??	0,10,0,10q?53      $	??100??54      $
??Left=?55      ??56      ?? *	IpAddress+?? 57      $??2??58      $??	0,10,0,10q?59      $??Sample time: ??5:      $??Right=?5;      $??Center=?6-   ?? 5<      $??3??5=      $??	0,10,0,10q?5>      $??50??5?      $
??Left=?5@      ??5A      ?? *
SampleTime+?? 5B      $??4??5C      $??
0,10,20,10q?5D      $	?? ms??5E      $??Right=?5F      $??Center=?6-   5G      ? 5I   
   $??3??6   $??1??6    5J      ?? 5K      $??* ?6#   ?? 5L      $
??Auto ?6#   ?? 5M      $
??Auto ?6#   ?? 5N      $??* ?6#   .+?5P      ?? $??0??5Q      $??30??5R      $	??150??5S      $??
0,10,30,10q?5T      $??START??5V      $??Right=?5W      ??5U      ?? *StartButton+.??5W   -   START5Y      ?? 5Z      $??1??5[      $??30??5\      $	??150??5]      $??
0,10,15,10q?5^      ??5_      ?? *
StopButton+.??60   STOP5a      ?? 5b      $??2??5c      $??30??5d      $	??150??5e      $??
15,10,0,10q?5f      $
??Left=?5g   8   ??6   ?? *UpdateConfigButton+.??6S   UPDATE CONFIG5i      ?? 5j      $??3??5k      $??30??5l      $	??150??5m      $??
30,10,0,10q?5n      $
??Left=?5o   9   ??6   ?? *DefaultConfigButton+.??6T   DEFAULT CONFIG5q      5r      5t      5u      