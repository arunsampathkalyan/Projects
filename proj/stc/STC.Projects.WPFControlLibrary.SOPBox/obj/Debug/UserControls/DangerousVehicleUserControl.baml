   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      [ WSTC.Projects.ClassLibrary.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�cclr-namespace:STC.Projects.ClassLibrary.Common.Converters;assembly=STC.Projects.ClassLibrary.Common+STC.Projects.ClassLibrary.Common.Converters 5	      �gclr-namespace:STC.Projects.ClassLibrary.Common.ChartAnimation;assembly=STC.Projects.ClassLibrary.Common/STC.Projects.ClassLibrary.Common.ChartAnimation 5
      w@clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.UserControls2STC.Projects.WPFControlLibrary.SOPBox.UserControls  5      T    NSTC.Projects.WPFControlLibrary.SOPBox.UserControls.DangerousVehicleUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      Z VEsri.ArcGISRuntime, Version=10.2.7.0, Culture=neutral, PublicKeyToken=8fc3cc631e44ad866esri+http://schemas.esri.com/arcgis/runtime/2013  5      e aTelerik.Windows.Controls, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q	 mTelerik.Windows.Controls.ImageEditor, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a
 ]Telerik.Windows.Data, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Chart, Version=2015.1.401.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Itelerik1http://schemas.telerik.com/2008/xaml/presentation   	 
   5      }	Convertercclr-namespace:STC.Projects.ClassLibrary.Common.Converters;assembly=STC.Projects.ClassLibrary.Common   	 
   5	      �ChartAnimationgclr-namespace:STC.Projects.ClassLibrary.Common.ChartAnimation;assembly=STC.Projects.ClassLibrary.Common   	 
   5
      VSOPUC@clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.UserControls   	 
   5      -   5          >�Name$  PatrolDetailsUIcontrol��5       J� Opacity$
 0.95Q�6?   z�5      T� 5   
    T� X$	 300Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 300X�6u   	 W� To$ 0X�6�    g� Duration$ 0:0:1D�6�   	 g� 	BeginTime$	 	0:0:0.001X�6�   
 g� 
SpeedRatio$
 3Q�6�   5      5      5      5       � 	Resources 5       K  ESTC.Projects.ClassLibrary.Common.Converters.DateTimeToStringConverter   DateTimeToStringConverter  StandardBorderBrush �� Color  StandardBackgroundBrush  HoverBorderBrush  SelectedBackgroundBrush  SelectedForegroundBrush  DisabledForegroundBrush  NormalBrush  NormalBorderBrush 	 HorizontalNormalBrush 
 HorizontalNormalBorderBrush  ListBoxBackgroundBrush �� 
StartPoint �� EndPoint � Color � Offset  StandardBrush  
GlyphBrush  PressedBrush  ScrollBarLineButton �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Path �� RelativeSource �� Triggers P� Property P� Value �� 
TargetName  ScrollBarPageButton  ScrollBarThumb  VerticalScrollBar � RowDefinitions ]� DecreaseRepeatButton ]� Thumb ]� IncreaseRepeatButton  HorizontalScrollBar  � ColumnDefinitions! �� Triggers" �� VerticalOffset# � Value$ �� ScrollableHeight% � Maximum& �� ViewportHeight' �� ViewportSize*( �� #ComputedVerticalScrollBarVisibility) �� Orientation* �� HorizontalOffset+ �� ScrollableWidth, �� ViewportWidth,- �� %ComputedHorizontalScrollBarVisibility  LeftScrollViewer  MyStoryboard. �� 
TargetName  IsMoseOvernone/ ��Name  IsMoseOverGrid 	 labol5  /Telerik.Windows.Controls.ChartView.ChartPalette  customPalette0   SeriesEntries?  9Telerik.Windows.Controls.ChartView.PaletteEntryCollection1   SeriesFamily5  /Telerik.Windows.Controls.ChartView.PaletteEntry2   Fill3   Stroke4   GlobalEntries��%3&  &	        &	      &	 1     &	 O     &	 m     &	 �     &	 �     &	 �     &	 �     &		     &	
 "    &	 @    &	 $    &	 �    &	 �    &	 �    7  7  7  7  &	 �    &	 �    &	 d	    7  7  7  7 	 7 
 7  7  &	 v    7  7  7  7  7  7  7  '�� �    7  7  '�� �    &	     &	 |    &	 >    &	     &	 �    &	 .      5"   
   6.   �� 5$   
   $
 #888��66   �� 5%   
   $
 #FFF��6:   �� 5&   
   $
 #DDD��63   �� 5'   
   $
 Gray��6:   �� 5(   
   $ White��6:   �� 5)   
   $
 #888��6:   �� 5*   
   $
 #888��6.   �� 5+   
   $
 #888��64   �� 5,   
   $
 #888��68   �� 5-   
   $
 #888��6>   �� 5.   
   $	 0,0&�5/      $ 1,0.001&�6   ��51      � � 52      $ White��6#   $	 0.0Q�61   � 53      $ White��6#   $	 0.6Q�61   � 54      $ #DDDDDD��6#   $	 1.2Q�63   55      57      �� 58   
   $	 0,0&�59      $	 0,1&�6   ��5;      � � 5<      $
 #FFF��6#   $	 0.0Q�60   � 5=      $
 #CCC��6#   $	 1.0Q�60   5>      5@      �� 5A   
   $ #eeac23��6-   �� 5B   
   $	 0,0&�5C      $	 0,1&�6   ��5E      � � 5F      $
 #BBB��6#   $	 0.0Q�60   � 5G      $
 #EEE��6#   $	 0.1Q�60   � 5H      $
 #EEE��6#   $	 0.9Q�60   � 5I      $
 #FFF��6#   $	 1.0Q�60   5J      5L      �� 5O   
   " ��6,   .�5P      ��  �@J�SnapsToDevicePixels . 64   �� 5Q       �@�OverridesDefaultStyle . 66   �� 5R       � |� .  6*   �� 5S       � �� 5T      �� 5U      " ��6&   .R�5V      �� $��Border��5W      $��1q�5X      $ 2��5Y      ���#004d468��  $��1q�5\      ._�5]      F� $��Center=�5^      $��Center=�5_      8�� ��5a      �� $ Content� �� *TemplatedParent+5c       5d      P� 5e       � �� . . �5f   "   �� $ Border��6)   8   � ��5g   	   5h      P� 5i       � {� .  . �5j   "   �� 8   � ��5k   	   5l      5n      5p      5q      �� 5t   
   " ��6,   .�5u      ��  �@J�SnapsToDevicePixels . 64   �� 5v       �@�OverridesDefaultStyle . 66   �� 5w       � �� .  6*   �� 5x       � |� .  6*   �� 5y       � �� 5z      �� 5{      " ��6&   .R�5|      �� 
������ 6!   5}      5      5�      �� 5�   
   " n�6'   .�5�      ��  �@J�SnapsToDevicePixels . 64   �� 5�       �@�OverridesDefaultStyle . 66   �� 5�       � �� .  6*   �� 5�       � |� .  6*   �� 5�       � �� 5�      �� 5�      " n�6&   .R�5�      �� $ 2��5�      ���#00ffcc#��z��5�      $��1q�5�      5�      5�      5�      �� 5�   
   " ��5�      .R�5�      �  5�      �� 5�      $��12��6$   �� 5�      $��0.00001* �6$   �� 5�      $��12��6$   .+�5�      �� $��3��5�      $ 2��5�      
������ 5�      �� 5�      $��0��5�      8��  $��12��5�      $��ScrollBar.LineUpCommand��5�      $��M 0 4 L 8 4 L 4 0 Z��5�      ]� 5�      $��
PART_Track��5�      $��1��5�      ��.  5�      �� 5�      8�� $��ScrollBar.PageUpCommand��5�       5�      n� 5�      8�� $��1,0,1,0q�5�      8�� 8�� 5�       5�      �� 5�      8�� $��ScrollBar.PageDownCommand��5�      5�      �� 5�      $��3��5�      8�� $��12��5�      $��ScrollBar.LineDownCommand��5�      $��M 0 0 L 4 4 L 8 0 Z��5�      5�      5�      �� 5�   
   " ��5�      .R�5�      �   5�      �� 5�      $��12��6'   �� 5�      $��0.00001* �6'   �� 5�      $��12��6'   .+�5�      �� $��3��5�      $ 2��5�      
������ 5�      �� 5�      $��0��5�      8��  $��12��5�      $��ScrollBar.LineLeftCommand��5�      $��M 4 0 L 4 8 L 0 4 Z��5�      ]� 5�      $��
PART_Track��5�      $��1��5�      ��.   5�      �� 5�      8�� $��ScrollBar.PageLeftCommand��5�       5�      n� 5�      8�� $��0,1,0,1q�5�      8�� 8�� 5�       5�      �� 5�      8�� $ ��ScrollBar.PageRightCommand��5�      5�      �� 5�      $��3��5�      8�� $��12��5�      $ ��ScrollBar.LineRightCommand��5�      $��M 0 0 L 4 4 L 0 8 Z��5�      5�      5�      �� 5�   
   " ��6+   .�5�      ��  �@J�SnapsToDevicePixels . 64   �� 5�       �@�OverridesDefaultStyle . 66   ! 5�      P� 5�       �@��Orientation$ 
Horizontal=�61   . �5�      ��  � ��$
 Auto��6.   �� 5�       � ��$ 12��6/   �� 5       � ��8   5     5     P� 5      �@��Orientation$ Vertical=�61   . �5     ��  � ��$ 12��6.   �� 5      � ��$
 Auto��6/   �� 5      � ��8  5     5     5
     �� 5  
   " ��6   .�5     ��  �@�OverridesDefaultStyle . 66   �� 5      � �� 5     �� 5     " ��6&   .R�5     �   5     �� 5  "   $
��Auto �63   �� 5  "    5     �� 5  "   �� 5  "   $
��Auto �60   .+�5     �� $��1��65   �� 5     $��PART_VerticalScrollBar��6(   
������ ## z" 5     #% z$ 5      #' z& 5!     #y�z( 5"     �� 5#     $��PART_HorizontalScrollBar��6(   $) 
Horizontal=�5$     $��1��5%     $��1��5&     ## z* 5'     #% z+ 5(     #' z, 5)     #y�z- 5*     5,     5-     5/     50     �� 52  
   " ��6)   .�53     ��  �@�OverridesDefaultStyle . 66   �� 54      � �� 55     �� 56     " ��6&   .R�57     �   58     �� 59  "   $
��Auto �63   �� 5:  "    5<     �� 5=  "   �� 5>  "   $
��Auto �60   .+�5A     �� $��1��65   �� 5C     $��PART_VerticalScrollBar��6(   ## z" 5D     #% z$ 5E     #' z& 5F     #y�z( 5G     �� 5H     $��PART_HorizontalScrollBar��6(   $) 
Horizontal=�5I     $��1��5J     $��1��5K     ## z* 5L     #% z+ 5M     #' z, 5N     #y�z- 5O     5Q     5R     5T     5U     �� 5W  
   .��5X     W� $. PatrolDetailsUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6M   $ 0X�6�   $	 300X�6�   $ 0:0:1D�6�   $
 4Q�6�   5Y     �� 5[  
   " ��6'   .�5\     ��  � ��
 ���� 6+   �� 5]      � ��
 ���� 6,   �� 5^      � ��$ Center=�6;   �� 5_      � ��$ Center=�62   �� 5`      � �� �#ffffff6+   �� 5a      �@�Cursor$
 Hand��6'   �� 5b      � �� 5c     �� 5d     " ��6&   .R�5e     �� $/ bg��6!   #��z��6-   $��1q�6W   
������ ._�5f     �� #��z��6/   #��z��6k   5g      5h     P� 5i      �@J�IsMouseOver . . �5j  "   �� $ bg��6S    � ��
 ���� �� 5k  "    � ��
 ���� 6?   �� 5l  "    � ��
 ���� 6@   �� 5m  "    �@J�Opacity$	 0.5Q�6<   6J   5n     5p     5r     5s     �� 5u  
   " ��6'   .�5v     ��  � ��$ 	Helvetica)�6+   �� 5w      � ��
 ���� 6+   �� 5x      � �� �#008c996,   �� 5y      � ��$ Center=�64   �� 5z      � ��$ Center=�62   �� 5{      � �� �#00ffcc6+   �� 5|      �@�Cursor$
 Hand��6'   �� 5}      � �� 5~     �� 5     " ��6&   .R�5�     �� $/ bg��6!   #��z��6-   $��1q�6W   ���#008c99._�5�     �� #��z��6/   #��z��6k   5�      5�     P� 5�      �@J�IsMouseOver . . �5�  "   �� $ bg��6O    � �� �#00ffcc�� 5�  "    � ��
 �   �6?   �� 5�  "    � �� �#008c996@   5�     5�     5�     5�     �� 5�  
   .R�5�     �� ���#00ffcc$��4q�6>   ��6-   �� 6>   5�       5�  
   0 5�       5�     $	1 Bar��61     5�     2 �#1E48E23 �#00616A6:     5�     2 �#008ED03 �#00616A6:     5�     2 �#00AFBA3 �#00616A6:     5�     2 �#0EB04F3 �#00616A6:     5�     2 �#87AF263 �#00616A6:     5�     2 �#F3DD4B3 �#00616A6:     5�     2 �#FF9C313 �#00616A6:     5�     2 �#FF4A4A3 �#00616A6:     5�     2 �#FF5C923 �#00616A6:     5�     2 �#A53FD03 �#00616A6:     5�     2 �#584BD23 �#00616A6:     5�     2 �#2E698E3 �#00616A6:     5�     2 �#6C7C863 �#00616A6:     5�     2 �#A9B6B73 �#00616A6:     5�     2 �#A3EAFF3 �#00616A6:     5�     2 �#14F7FF3 �#00616A6:     5�     2 �#00BFC63 �#00616A6:     5�     2 �#38B2FF3 �#00616A6:     5�     2 �#3B6FC73 �#00616A6:   5�     4 5�       5�     2 �#1E48E23 �#00616A66     5�     2 �#008ED03 �#00616A66     5�     2 �#00AFBA3 �#00616A66     5�     2 �#0EB04F3 �#00616A66     5�     2 �#87AF263 �#00616A66     5�     2 �#F3DD4B3 �#00616A66     5�     2 �#FF9C313 �#00616A66     5�     2 �#FF4A4A3 �#00616A66     5�     2 �#FF5C923 �#00616A66     5�     2 �#A53FD03 �#00616A66     5�     2 �#584BD23 �#00616A66     5�     2 �#2E698E3 �#00616A66     5�     2 �#6C7C863 �#00616A66     5�     2 �#A9B6B73 �#00616A66     5�     2 �#A3EAFF3 �#00616A66     5�     2 �#14F7FF3 �#00616A66     5�     2 �#00BFC63 �#00616A66     5�     2 �#38B2FF3 �#00616A66     5�     2 �#3B6FC73 �#00616A66   5�     5�     .��5�     � $	��260��6   $	��600��6   ��5�  
   �� 5�     5 �� ImageSource$K5 E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    5�  
   �� 5�     $
��Auto �6   �� 5�       5�  
   �� 5�     �� 5�     .+�5�  
   �� @   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources!6   strDangerousVehicleDetails#��Z6 6   $��0��6M   $��0��6Z   $��2��6j   ���#00ffcc$��20(�6�   $	��Top=�6�   $��	Helvetica)�6�   �� 5�  
   -   5        7 ��Name$7 Btnback��5�     $��0��6"   $��1��6/   $��Right=�6?   $��20��6v   $��20��6�   $��Disabled=�6�   $	��Top=�6�   #���  6  ��6�   Z� ��6�   Z� .��5�     �� $��18��6   $��18��6   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6+   $��LeftToRight=�6{   5�     �� 5�  
   $��2��6   $��0��6   $��2��6.   ���#10302d$��1q�6X   $ 2��6l   ._�5�     �  5�     �� 5�     �� 5�     $
��Auto �6$     5�     �� 5�     �� 5�     .+�5�     � $��0��6   $��2��6$    5�     �� 5�     $	��50* �6(   �� 5�     $
��160* �6(   .+�5     � $��0��6   $��0,10,0,0q�6(    5      �� 5!     $
��Auto �6,   �� 5"     $
��Auto �6,   .+�5'     �� $��1��6#   $��30��60   $	��110��6?   $��5q�6O   ���#00ffcc���#0A1114���#002b22$��	Helvetica)�6�   $��12(�6�   $
��Auto=�5(  #   $
��Auto=�6U   ��5'  �   �� *+)DangerousVehicleDetails.VehicleViolations+ 5)     ��%�  '��       �� 5-  "   " ��6(   .�5.  &   ��  � �� �#0e171b6C   �� 5/  &    � �� 50  *   �� 51  .   " ��6>   .R�52  2   �� ���#0e171b$	��0.1q�6N   $	��5,2q�6d   $	 0.7Q�6q   ._�53  6   �� 54  3   56  /   58  '   ! 59  &   P� 5:  *    �@J�IsMouseOver . . �5;  .   ��  �@�Cursor$ Arrow��6G   �� 5<  .    �@J�Opacity$ .9Q�6H   5=  +   5@  #   5D     8 �� View8 5F     �� 5G  "   !9 �� ColumnHeaderContainerStyle9 5H  &   �� 5I  *   " ��60   .�5J  .   ��  � �� 5K  2   �� 5L  6   " ��6F   .R�5M  :   �� #��z��6D   : �� Padding$: 5q�6e   #��z��6q   ; �� TextAlignment$
; Left=�6�   $��WrapWithOverflow=�6�   < � Cursor$< Arrow��6�   5N  7   5P  /   �� 5R  .    � �� �#00ffcc6K   �� 5S  .    � ��$ 13(�6I   �� 5T  .    � ��$ 2q�6H   5U  +   .P�5Y  &   �� =   strViolationID#��Z= 65   > �� Width$> 25��6�   ? �� DisplayMemberBinding? 6d   �� *	MessageId+6�   �� 5Z  &   @   strViolationType#��Z@ 65   $> 45��6�   ? 6f   �� *ViolationTypeName+6�   �� 5\  &   A   strViolationDateTime#��ZA 65   $> 80��6j   B �� CellTemplateB 5]  *   �� 5^  .   .R�5_  2   �� $/ brderTxt��69   $��0q�6K   ._�5`  6   �� $
��Wrap=�6�   $< Arrow��6�   ��6@   �� *	DateTaken+C �� 	Converter#C [  D �� ConverterParameter$D DateTime��6�   5a  3   5b  /   5d  '   �� 5i  &   E   strImage#��ZE 65   $
> Auto��6^   B 5j  *   �� 5k  .   .R�5l  2   �� $/ 
brderImage��69   $��0q�6M   $��20��6a   
������ ._�5m  6   �� $7 btnImage��6N   
������ ��6=   Z� ��6a   ��  �� F �� AncestorType"F >�G �� Mode$G FindAncestor=�$" DataContext.ShowImageCommand���5n  5   �� ��5o  :   �� 5p  >   .R�5q  B   �� H �� Orientation$H Vertical=�6M   $
��Left=�6d   .+�5r  F   �� $��20��6L   $U��O/STC.Projects.WPFControlLibrary.SOPBox;component/images/icons/Image_iconNew.jpg��6W   5s  C   5t  ?   5v  7   5w  3   I �� TriggersI 5x  2   P� 5y  6    �@J�IsMouseOver . . �5z  :   ��  �@�Cursor$
 Hand��6S   �� 5{  :   $ 
brderImage��6A    � ��
 ���� 6o   5|  7   5~  /   5�  '   �� 5�  &   J   strVideo#��ZJ 65   $
> Auto��6^   B 5�  *   �� 5�  .   .R�5�  2   �� $/ 
brderImage��69   $��0q�6M   $��20��6a   
������ ._�5�  6   �� $7 btnVideo��6N   
������ ��6=   Z� ��6`   ��  �� "F >�$G FindAncestor=�$" DataContext.ShowVideoCommand���5�  5   �� ��5�  :   �� 5�  >   .R�5�  B   �� $H Vertical=�6M   $
��Left=�6d   .+�5�  F   �� $��20��6L   $V��P/STC.Projects.WPFControlLibrary.SOPBox;component/images/icons/VideoI_iconNew.jpg��6W   5�  C   5�  ?   5�  7   5�  3   I 5�  2   P� 5�  6    �@J�IsMouseOver . . �5�  :   ��  �@�Cursor$
 Hand��6S   �� 5�  :   $ 
brderImage��6A    � ��
 ���� 6o   5�  7   5�  /   5�  '   5�  #   5�     5�     C   =STC.Projects.WPFControlLibrary.SOPBox.UserControls.ImagePopup  5�     -   5        K  Name$K imgGalleryUc��5�  '   $
��Auto=�6=   $��0��6m   $��2��6z   $	��Top=�6�   $��-250,50,-100,0q�6�   5�     � 5�     $��2��6    5�     �� 5�     $
��Auto �6,   �� 5�     �� 5�     .+�5�     �� $��0��6$   $��0��61   $��16(�6A   ���#ffd452$
; Left=�6d   L   strKPI#��ZL 6y   $��	Helvetica)�6�   � 5�     $��1��6   .+�5�     0  *Telerik.Windows.Controls.RadCartesianChart  -   5        M  Name$M 
LineChart1��5�  8   5  /Telerik.Windows.Controls.ChartView.RadChartBaseN   Palette#N [ 6L   $
��Auto=�6u   ���#00ffccO   VerticalAxisO 5�  "   3	  -Telerik.Windows.Controls.ChartView.LinearAxis	  5�  &   -
  'Telerik.Windows.Controls.ChartView.AxisP 
  Title$P عدد المخالفات��69   Q 
  LabelTemplate#Q [ 6O   R 	  	MajorStepA  ;Telerik.Windows.Controls.Converters.StringToDoubleConverter$R 1 6v   S   HorizontalAxisS 5�  "   8  2Telerik.Windows.Controls.ChartView.CategoricalAxis  5�  &   #Q [ 6>   T 
  LabelFitMode$T Rotate=�6e   U   SeriesProviderU 5�  "   <  6Telerik.Windows.Controls.ChartView.ChartSeriesProvider  5�  &   V   SourceV 6B   �� *	LineChart+W   SeriesDescriptorsW 5�  *   D  >Telerik.Windows.Controls.ChartView.CategoricalSeriesDescriptor  5�  .   >  8Telerik.Windows.Controls.ChartView.ChartSeriesDescriptorX   ItemsSourcePath$X Details��6R   Y   	ValuePath$Y Value��6l   H  BTelerik.Windows.Controls.ChartView.CategoricalSeriesDescriptorBaseZ   CategoryPath$	Z Key��6~   [   Style[ 5�  2   �� 5�  6   2  ,Telerik.Windows.Controls.ChartView.BarSeries"  6<   .�5�  :   ��  �@ LegendSettings 5�  >   =  7Telerik.Windows.Controls.ChartView.SeriesLegendSettings  5�  B   \   Title\ 6_   �� *
LegendName+5�  ;   5�  7   5�  /   5�  '   5�     5�     � 5�     $��2��6    5�     �� 5�  "   �� 5�  "   $
��Auto �60   .+�5�     *  $Telerik.Windows.Controls.RadPieChart  -   5        ]  Name$] 	PieChart1��5�  2   #N [ 6E   }�.  ^   Series.^ 5�  "   2  ,Telerik.Windows.Controls.ChartView.PieSeries  M  GSTC.Projects.ClassLibrary.Common.ChartAnimation.ChartAnimationUtilities_   PieAnimation$_ 
SweepAngle=�6|   4  .Telerik.Windows.Controls.ChartView.ChartSeries`   
ShowLabels` . a   ItemsSourcea 64   �� *0.DangerousVehicleDetails.VehiclePieKPIs.Details+b   SliceStylesb 5�  &   �� 5�  *   " F�60   .�5�  .   ��  � �� �#1e48e26E   5�  +   �� 5�  *   " F�60   .�5�  .   ��  � �� �#008ed06E   5�  +   �� 5�  *   " F�60   .�5�  .   ��  � �� �#00afba6E   5�  +   �� 5�  *   " F�60   .�5�  .   ��  � �� �#0eb04f6E   5�  +   �� 5�  *   " F�60   .�5�  .   ��  � �� �#87af266E   5�  +   �� 5�  *   " F�60   .�5�  .   ��  � �� �#f3dd4b6E   5�  +   �� 5   *   " F�60   .�5  .   ��  � �� �#ff9c316E   5  +   c   ValueBindingc 5  &   E  ?Telerik.Windows.Controls.ChartView.PropertyNameDataPointBinding  5  *   d   PropertyName$d Value��6O   e   LegendSettingse 5  &   @  :Telerik.Windows.Controls.ChartView.DataPointLegendSettings  5	  *   f   TitleBindingJ  DTelerik.Windows.Controls.ChartView.StringToDataPointBindingConverter$	f Key 6J   5  #   5     (  "Telerik.Windows.Controls.RadLegend  5     $��1��60   $��RightToLeft=�6=   ���#00ffcc$
��Auto=�6n   $��Center=�6�   $	��Top=�6�   g   Itemsg 6�   �� h �� ElementName$h 	PieChart1��$ LegendItems�i   
ItemsPaneli 5  "   �� 5  &   .R�5  *   � j � Orientation$j 
Horizontal=�64   5  '   5     5     5     5     �� 5     -   5        $7 ConfirmButton��5     $��5q�60   $��0��6;   k   
strConfirm#��Zk 6g   $��1��6�   $��90��6�   $��30��6�   $��16(�6�   #��[ 6�   �� 5     -   5        $7 CancelButton��5     $��5q�6/   $��1��6:   l   	strCancel#��Zl 6J   $��1��6u   $��70��6�   $��30��6�   $��16(�6�   #��[ 6�   5      5!     5"     5$     