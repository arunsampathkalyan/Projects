   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters0STC.Projects.WPFControlLibrary.SOPBox.Converters  5   	   C    =STC.Projects.WPFControlLibrary.SOPBox.UserControls.ImagePopup   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   U
Converters>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters     5   	   -   5          >�Name$  ImagePopupUC��5   4   $	��200��6	   $	��200��6   $	��900��6%    � 	Resources 5      P   JSTC.Projects.WPFControlLibrary.SOPBox.Converters.BoolToVisibilityConverter    BoolToVisibilityConverterKeyW   QSTC.Projects.WPFControlLibrary.SOPBox.Converters.InverseBoolToVisibilityConverter ' #InverseBoolToVisibilityConverterKey  MyStoryboardOpen �� 
TargetName �� TargetProperty W� From	 W� To g� Duration g� 
SpeedRatio  MyStoryboard  IsMoseOvernone �� 
TargetType	 �� Property
 �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	      &	 &     &	 �     &	 �      5   
   6V     5   
   6d   �� 5
   
   .��5      W� $ ImagePopupUC��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6C   $	 700X�6�   $ 0X�6�   $ 0:0:1D�6�   $ 2Q�6�   5      �� 5   
   .��5      W� $ ImagePopupUC��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6C   $ 0X�6�   $	 750X�6�   $ 0:0:1D�6�   $ 4Q�6�   5      �� 5   
   " ��6'   .�5      �� 	 � ��

 ���� 6+   �� 5      	 � ��

 ���� 6,   �� 5      	 � ��$
 Center=�6;   �� 5      	 � ��$
 Center=�62   �� 5      	 � ��
 �#ffffff6+   �� 5      	 �@�Cursor$

 Hand��6'   �� 5      	 � ��
 5      �� 5      " ��6&   .R�5      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5      �� #��z��6/   #��z��6k   5       5      P� 5        �@J�IsMouseOver . . �5!   "   �� $ bg��6S   	 � ��

 ���� �� 5"   "   	 � ��

 ���� 6?   �� 5#   "   	 � ��

 ���� 6@   �� 5$   "   	 �@J�Opacity$	
 0.5Q�6<   6J   5%      5'      5)      5*      5+      z�5,      T� 5-   
    T� X$	 750Q�6   .��5/      � ��50   
   �� 51       �� ImageSource$K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6   .+�53   
   �� -   5         ��Name$ btnClose��53      $��0��6#   $��1��60   $��Right=�6@   $��20��6s   $��20��6~   $��Disabled=�6�   $	��Top=�6�   #���  6  ��6�   Z� ��6�   Z� .��54      �� $��18��6   $��18��6   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6+   $��LeftToRight=�6{   55      r� 56   
   $��5,25,5,5q�63    r� LoadedBehavior$
 Play=�6E    r� UnloadedBehavior$
 Stop=�6[    r� Source 6   �� *
VideoURL+y�57      �� *IsVideoStreamingVisible+ �� 	Converter# [  �� 58   
   
������ |�.  
�������$	��0.5q�5:   *   $��0q�5;      $��5,25,5,5q�5<      $��Stretch=�5=      $
��Auto=�5?      ��58      �� *ImageURLList+y�5>      �� *IsVideoStreamingVisible+# [ ��5@      �� 5A      .R�5B      �� $ 	imgBorder��6   $��1q�60   
������ $��1q�6^    �� Padding$ 3q�6i   ._�5C      � .+�5D      �� -   5         � Cursor$
 Hand��5D   8   $	��100��5E       $	��150��5F       $
��Left=�5G       ��5D   $   �� 5G   m   5l      5n       �� Triggers 5o      P� 5p       �@J�IsMouseOver . . �5q      �� $ 	imgBorder��6%   	 � ��
 �#00ffcc6S   �� 5r      $ 	imgBorder��6%   	 � ��$	
 1.5q�6W   5s      5u      ��5w      �� 5x      .R�5y      � $	��600��6     � Orientation$ 
Horizontal=�6/   5z      5|      5}      5~      