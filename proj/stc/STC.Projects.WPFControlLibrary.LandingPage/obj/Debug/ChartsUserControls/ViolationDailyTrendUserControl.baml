   M S B A M L   `   `   `  ����[  WSTC.Projects.ClassLibrary.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�gclr-namespace:STC.Projects.ClassLibrary.Common.ChartAnimation;assembly=STC.Projects.ClassLibrary.Common/STC.Projects.ClassLibrary.Common.ChartAnimation  5      e aSTC.Projects.WPFControlLibrary.LandingPage, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null{Bclr-namespace:STC.Projects.WPFControlLibrary.LandingPage.ViewModel4STC.Projects.WPFControlLibrary.LandingPage.ViewModel 5      }Cclr-namespace:STC.Projects.WPFControlLibrary.LandingPage.Properties5STC.Projects.WPFControlLibrary.LandingPage.Properties 5	      b   \STC.Projects.WPFControlLibrary.LandingPage.ChartsUserControls.ViolationDailyTrendUserControl   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      k gTelerik.Windows.Controls.Chart, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e	 aTelerik.Windows.Controls, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p
 lTelerik.Windows.Controls.Navigation, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7GTelerik1http://schemas.telerik.com/2008/xaml/presentation    	 
 5      �ChartAnimationgclr-namespace:STC.Projects.ClassLibrary.Common.ChartAnimation;assembly=STC.Projects.ClassLibrary.Common    	 
 5      Z	ViewModelBclr-namespace:STC.Projects.WPFControlLibrary.LandingPage.ViewModel    	 
 5      SpCclr-namespace:STC.Projects.WPFControlLibrary.LandingPage.Properties    	 
 5	        � 	Resources  5       �� 
TargetType   StripLinesStyle �� Property �� Value 	 labol J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   Color   
BlurRadius   Opacity   ShadowDepth5  /Telerik.Windows.Controls.ChartView.ChartPalette  customPalette	   SeriesEntries?  9Telerik.Windows.Controls.ChartView.PaletteEntryCollection
   SeriesFamily5  /Telerik.Windows.Controls.ChartView.PaletteEntry   Fill   Stroke   GlobalEntries  telrikCombBoxStyle*  $Telerik.Windows.Controls.RadComboBox �� 
TargetType �� ImageSource.  (Telerik.Windows.Controls.RadComboBoxItem ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 q     &	 6    &	 l
    '�� Z    '�� �    '�� �    '�� �    '�� �    �� 5   
   " ��6   .�5      ��  � �� �#1313136'   �� 5       � ��$ 2��60   5      �� 5   
   .R�5      �� ���#00ffcc$��Hevetica)�6-   $��4q�6T   ��6C   ��  5        5      $ #00ffcc��6'   $ 5Q�67   $ .9Q�6F   $ 0Q�6S   5      5        5   
   	 5        5      $

 Line��61     5       �#fffc21 �#fffc216:     5       �#ff0012 �#ff00126:     5       �#00d8ff �#00d8ff6:     5        �#18ff00 �#18ff006:     5!       �#73a337 �#73a3376:     5$       �#1e48e2
 �����6:     5%       �#00afba
 �����6:     5&       �#0eb04f
 �����6:     5'       �#87af26
 �����6:     5(       �#f3dd4b
 �����6:     5)       �#ff9c31
 �����6:     5*       �#ff4a4a
 �����6:     5+       �#ff5c92
 �����6:     5,       �#a53fd0
 �����6:     5-       �#584bd2
 �����6:     5.       �#2e698e
 �����6:     5/       �#6c7c86
 �����6:     50       �#a9b6b7
 �����6:     51       �#a3eaff
 �����6:     52       �#14f7ff
 �����6:     53       �#00bfc6
 �����6:     54       �#38b2ff
 �����6:     55       �#3b6fc7
 �����6:     56       �#607D8B
 �����6:     57       �#FFCB00
 �����6:   58       5;        5=       �#fff799
 �����66     5>       �#f26522
 �����66     5?       �#0051a0
 �����66     5@       �#f06eaa
 �����66     5A       �#73a337
 �����66     5C       �#1e48e2
 �����66     5D       �#00afba
 �����66     5E       �#0eb04f
 �����66     5F       �#87af26
 �����66     5G       �#f3dd4b
 �����66     5H       �#ff9c31
 �����66     5I       �#ff4a4a
 �����66     5J       �#ff5c92
 �����66     5K       �#a53fd0
 �����66     5L       �#584bd2
 �����66     5M       �#2e698e
 �����66     5N       �#6c7c86
 �����66     5O       �#a9b6b7
 �����66     5P       �#a3eaff
 �����66     5Q       �#14f7ff
 �����66     5R       �#00bfc6
 �����66     5S       �#38b2ff
 �����66     5T       �#3b6fc7
 �����66     5U       �#607D8B
 �����66     5V       �#FFCB00
 �����66   5Y      �� 5[   
   "  6+   .�5\      ��  � �� �#00ffcc6+   �� 5]       � �� 5^      �� 5_      "  6&   .R�5`      �� #��z��6!   ._�5a      �� #��z��5b   /   #��z��5c   $   #��z��5d   $   5e      5f      5h      �� 5j       � �� 5k      �� 5l      $ ..\Images\dotted_bg.png��6!   5n      �� 5p       � �� 5q      �� 5r      comboBoxItemStyle��"  67   .�5s      ��  � �� �#00ffcc67   �� 5t       � ��
 �   �68   �� 5u       � �� 5v      �� 5w   "   $ ..\Images\dotted_bg.png��6-   5y      5      5�      �� 5�       �@J�Effect 5�        5�      $ #00ffcc��6'   $ 3Q�67   $ .9Q�6F   $ 0Q�6S   5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��$ Center=�62   �� 5�       � ��$ Center=�64   �� 5�       � ��
 ���� 6+   �� 5�       � �� �#00ffcc6+   �� 5�       �@��Padding$ 0q�6(   �� 5�       � ��$ 12(�6)   66   �� 5�       �@J�Effect 5�        5�      $ #00ffcc��6'   $ 3Q�67   $ .9Q�6F   $ 0Q�6S   5�      5�      �� 5�   
   " ��6   .�5�      ��  � �� �#00ffcc6+   �� 5�       � ��
 ���� 6+   �� 5�       � ��$ Center=�64   �� 5�       � ��$ Center=�62   �� 5�       �@J�Effect 5�        5�      $ #00ffcc��6'   $ 15Q�67   $ 1Q�6G   $ 0Q�6S   5�      �� 5�       � �� 5�      �� 5�      " ��6&   .R�5�      �� $ bg��6!   #��z��6-   $��1q�6W   ���#00ffcc._�5�      �� #��z��6/   #��z��6k   5�       5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� $ bg��6O    � �� �#49b13c�� 5�   "    � �� �#ffffff6?   5�      5�      5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��
 ���� 6+   �� 5�       � �� �#00ffcc6+   �� 5�       � ��$ 0q�6(   �� 5�       � ��$ Center=�69   �� 5�       � �� �#00ffcc6,   �� 5�       �@J�Effect 5�        5�      $ #00ffcc��6'   $ 4Q�67   $ .6Q�6F   $ 0Q�6S   5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��
 ���� 6+   �� 5�       � �� �#00ffcc6+   �� 5�       � ��$ 0q�6(   �� 5�       � ��$ Center=�62   �� 5�       � ��
 ���� 6+   �� 5�       � �� �#00ffcc6+   �� 5�       � ��$ 0q�6(   �� 5�       � ��$ Center=�69   �� 5�       �@J�Effect 5�        5�      $ #00ffcc��6'   $ 4Q�67   $ .6Q�6F   $ 0Q�6S   5�      5�      �� 5�   
   " ��6   .�5�      ��  � ��
 ���� 6+   �� 5�       � �� �#00ffcc6+   �� 5�       � ��$ 0q�6(   �� 5�       � ��$ Center=�69   �� 5�       �@��
IsEditable . 6+   �� 5�       � �� �#00ffcc6,   �� 5�       � ��$ 1q�60   �� 5�       �@J�Effect 5�        5�      $ #00ffcc��6'   $ 4Q�67   $ .6Q�6F   $ 0Q�6S   5�      5�      5�       � DataContext 5�      W QSTC.Projects.WPFControlLibrary.LandingPage.ViewModel.ViolationDailyTrendViewModel  5�   
   .��5�      � $	��600��6   $	��290��6   ��5�   
   �� 5�       �� Triggers 5�      :� 5�       :� RoutedEvent$ Page.Loaded��6#   [�5�      �� 5�      .��5�   "   �� .��5�   &   W�  �� TargetProperty$U O(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)�66    g� 	BeginTime$ 0:0:0.1X�5�   5    g� Duration$ 0:0:1D�6I    W� From$	 700X�6[   	 W� To$ 0X�6f   5�   #   5�      5     5     z�5  
   V� 5     .~�5     T� �� 5     5      � RowDefinitions 5  
   �� 5     $��30 �6   �� 5     .+�5"  
   � 
������ .+�5#     �� $��15(�6   $��	10,0,10,0q�6&   E  ?STC.Projects.WPFControlLibrary.LandingPage.Properties.Resources   strViolationsDailyTrend#��Z 69   ���#00ffcc$��Center=�6�   $
��Left=�6�    5$       5%     $ #00ffcc��6'   $ 3Q�67   $ .5Q�6F   $ 0Q�6S   5'     5(     � 5*  
   $��1��6    5+     �� 5,     $��50 �6    �� 5-     �� 5.     $��50 �6    .+�51     � $��	10,0,10,0q�6    � ColumnDefinitions 52     �� 53     �� 54     �� 55     �� 56     �� 57     �� 58     .+�5:       -   5           Name$  cmbBoxfromYear��5:  &   $��0��5;  *   $��25��6:   #��[ 5<  *   ��5=  &   �� *FromYearValueColl+! �� SelectedItem! 5?  *   �� *FromYearValue+6S   �� 5A     $��0��6   $��1��6)   "   strTo#��Z" 69   6_     5C     -   5        $  cmbBoxToYear��5C  &   $��2��5D  *   $��25��6:   #��[ 5E  *   ��5F  &   �� *ToYearValueColl+! 5G  *   �� *ToYearValue+6Q   �� 5I     $��0��6   $��4��6)   #   strHighestDay#��Z# 69   6g   �� 5J     -   5        $ ��Name$$ txtBlkHighQuarterValue��5J     ���#ffd452$��0��6Q   $��5��6^   $
��Left=�6�   % �� TextAlignment$% Justify=�6�   $��15,0,0,0q�6�   ��6n   �� *HighestValue+6�   5K     0	  *Telerik.Windows.Controls.RadCartesianChart	  5N     -   5        & 	 Name$& 	lineChart��5N  (   ' 	  	HoverMode$' FadeOtherSeries=�6<   5
  /Telerik.Windows.Controls.ChartView.RadChartBase( 
  Palette#( [ 6X   $
��Auto=�5O  (   $��1��6Z   ���#090909���#00ffcc) 	  Grid) 5Q     ;  5Telerik.Windows.Controls.ChartView.CartesianChartGrid  5R     *   MajorLinesVisibility$* XY=�61   +   MajorYLineStyle#+ [  6L   ,   MajorXLineStyle#, [  6   - 	  VerticalAxis- 5U     3  -Telerik.Windows.Controls.ChartView.LinearAxis  5V     -  'Telerik.Windows.Controls.ChartView.Axis.   Title$*. $متوسط عدد المخالفات��6)   /   LabelTemplate#/ [ 6E   0 	  HorizontalAxis0 5X     8  2Telerik.Windows.Controls.ChartView.CategoricalAxis  5Y     #/ [ 6.   1 
  	Behaviors1 5\     =  7Telerik.Windows.Controls.ChartView.ChartTooltipBehavior  5]     2   VerticalOffset$2 7Q�63   3 
  TooltipTemplate3 5`     �� 5a     .R�5b     � 4 J� IsHitTestVisible4 .  .+�5c     �� 
�������$��1q�6<   5 �� CornerRadius$5 1��6P   ._�5d  "   �� 
�������.+�5e  &   �� $��11(�60   6 �� Padding$6 2,2,2,2q�6U   ��6>   �� *Value+6U   5f  #   5g     5h     5i     7 
  SeriesProvider7 5l     <  6Telerik.Windows.Controls.ChartView.ChartSeriesProvider  5m     8   Source8 62   �� *ViolationsCollection+9   SeriesDescriptors9 5n     D  >Telerik.Windows.Controls.ChartView.CategoricalSeriesDescriptor  5o     >  8Telerik.Windows.Controls.ChartView.ChartSeriesDescriptor:   ItemsSourcePath$: Details��6B   ;   	ValuePath$; Value��6\   H  BTelerik.Windows.Controls.ChartView.CategoricalSeriesDescriptorBase<   CategoryPath$	< Key��6n   =   Style= 5p  "   �� 5�  &   3  -Telerik.Windows.Controls.ChartView.LineSeries"  6,   .�5�  *   �� A  ;Telerik.Windows.Controls.ChartView.CategoricalStrokedSeries �@ StrokeThickness$ 2Q�6L   �� 5�  *   :  4Telerik.Windows.Controls.ChartView.CategoricalSeries �@ CombineMode$ Cluster=�6H   �� 5�  *   4  .Telerik.Windows.Controls.ChartView.ChartSeries �@ 
ShowLabels .  6G   �� 5�  *    �@ LegendSettings 5�  .   =  7Telerik.Windows.Controls.ChartView.SeriesLegendSettings  5�  2   >   Title> 6O   �� *
LegendName+5�  +   �� 5�  *   <  6Telerik.Windows.Controls.ChartView.PointTemplateSeries �@ PointTemplate 5�  .   �� 5�  2   .R�5�  6   A� $��5��6>   $��5��6H   #��� ��6S   #��� ��6�   5�  3   5�  +   �� 5�  *    �@ StrokeShapeStyle 5�  .   �� 5�  2   " F�68   .�5�  6   ��  � �� 6O   �� *	LineColor+5�  3   5�  +   5�  '   5�     5�     5�     ( 	 "Telerik.Windows.Controls.RadLegend  5�     -   5        ?  Name$? chartLegend��5�      ���#00ffcc$
��Auto=�6J   $��2��5�  Z   $��Center=�5�      $��Center=�6=   @   Items@ 5�      �� A �� ElementName$A 	lineChart��B �� Path$B LegendItems�C   
ItemsPanelC 5�     �� 5�     .R�5�     �� D �� Orientation$D 
Horizontal=�6%   5�     E   ItemTemplateE 5�     �� 5�     .R�5�     �� $
��brdr��6!   -   5        $��0q�5�  m   $��5,5,0,0q�6�   ��6J   �� *
MarkerFill+._�5�     � .+�5�  "   �� $��txtBlk��6,   $
��Wrap=�6:   $	��100��6N   $��35 0 0 0q�6�   ��6^   �� *Title+��6u   �� *
MarkerFill+6�   F� 5�  "   $��60��6'   +����    ��� р��     � -1��� р��    	$��12��5�     $��3��5�  9   $
��Left=�5�     ��5�     �� *
MarkerFill+5�     5�     5�     5�     5�     5�     5�     5�     