   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters0STC.Projects.WPFControlLibrary.SOPBox.Converters  5   	   I    CSTC.Projects.WPFControlLibrary.SOPBox.UserControls.ImagePopupWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   U
Converters>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Converters     5   	   -   5          �Name$  ImagePopupWindowUC��5	   4    � 
ResizeMode$ NoResize=�5   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5   	   $	��200��5	   	   $	��200��6   $	��600��6%   
������ 
������ $��	0,100,0,0q�6�    � WindowStyle$
 None=�6�    � 	Resources 5
      P   JSTC.Projects.WPFControlLibrary.SOPBox.Converters.BoolToVisibilityConverter    BoolToVisibilityConverterKeyW   QSTC.Projects.WPFControlLibrary.SOPBox.Converters.InverseBoolToVisibilityConverter ' #InverseBoolToVisibilityConverterKey  MyStoryboardOpen �� 
TargetName �� TargetProperty g� Duration	 g� 
SpeedRatio  MyStoryboard  IsMoseOvernone
 �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	      &	 &     &	 �     &	        5   
   6V     5   
   6d   �� 5   
   .��5      W� $ ImagePopupWindowUC��6   $ (Window.Left)�6I   $ 0:0:1D�6t   $	 2Q�6�   5      �� 5   
   .��5      W� $ ImagePopupWindowUC��6   $ (Window.Left)�6I   $ 0:0:1D�6t   $	 4Q�6�   5      �� 5   
   "
 ��6'   .�5      ��  � ��
 ���� 6+   �� 5       � ��
 ���� 6,   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�62   �� 5       � �� �#ffffff6+   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5       " ��6&   .R�5!      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5"      �� #��z��6/   #��z��6k   5#       5$      P� 5%       �@J�IsMouseOver . . �5&   "   �� $ bg��6S    � ��
 ���� �� 5'   "    � ��
 ���� 6?   �� 5(   "    � ��
 ���� 6@   �� 5)   "    �@J�Opacity$	 0.5Q�6<   6J   5*      5,      5.      5/      50      .��5A      �� ���#00ffcc$��.4q�6#   ._�5B   
   � ��5C      �� 5D       �� ImageSource$K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6   .+�5F      �� -   5         ��Name$ btnClose��5F      $��0��6'   $��1��64   $��Right=�6D   $��20��6w   $��20��6�   $��Disabled=�6�   $	��Top=�6�   #���  6  ��6�   Z� ��6�   Z� .��5G      �� $��18��6   $��18��6#   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6/   $��LeftToRight=�6   5H      r� 5I      $��5,25,5,5q�67    r� LoadedBehavior$
 Play=�6I    r� UnloadedBehavior$
 Stop=�6_    r� Source 6   �� *
VideoURL+y�5J      �� *IsVideoStreamingVisible+ �� 	Converter# [  �� 5K      
������ |�.  
�������$	��0.5q�5M   *   $��0q�5N      $��5,25,5,5q�5O      $��Stretch=�5P      $
��Auto=�5R      ��5K      �� *ImageURLBitmap+y�5Q      �� *IsVideoStreamingVisible+# [ ��5S      �� 5T      .R�5U      �� $ 	imgBorder��6!   $��1q�64   
������ $��1q�6b    �� Padding$ 3q�6m   ._�5V      � .+�5W   "   �� -   5         � Cursor$
 Hand��5W   <   $	��100��5X       $	��150��5Y       $
��Left=�5Z       ��5W   (   �� 5Z   m   5      5�       �� Triggers 5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� $ 	imgBorder��6)    � �� �#00ffcc6W   �� 5�   "   $ 	imgBorder��6)    � ��$	 1.5q�6[   5�      5�      ��5�      �� 5�      .R�5�      � $	��600��6$    � Orientation$ 
Horizontal=�63   5�      5�      5�      5�      5�      