   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      Q    KSTC.Projects.WPFControlLibrary.SOPBox.UserControls.CameraDetailsUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      -   5          >�Name$  camUIcontrol��5       J� Opacity$
 0.95Q�5      z�5
      T� 5   
    T� X$	 385Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 300X�6u   	 W� To$ 0X�6�    g� Duration$ 0:0:1D�6�   	 g� 	BeginTime$	 	0:0:0.001X�6�   
 g� 
SpeedRatio$
 3Q�6�   5      5      5      5       � 	Resources 5         MyStoryboard �� 
TargetName  IsMoseOvernone �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	 �     �� 5   
   .��5      W� $ camUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6C   $ 0X�6�   $	 300X�6�   $ 0:0:1D�6�   $
 4Q�6�   5      �� 5   
   " ��6'   .�5       ��  � ��
 ���� 6+   �� 5!       � ��
 ���� 6,   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�62   �� 5$       � �� �#ffffff6+   �� 5%       �@�Cursor$
 Hand��6'   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5*      �� #��z��6/   #��z��6k   5+       5,      P� 5-       �@J�IsMouseOver . . �5.   "   �� $ bg��6S    � ��
 ���� �� 5/   "    � ��
 ���� 6?   �� 50   "    � ��
 ���� 6@   �� 51   "    �@J�Opacity$	 0.5Q�6<   6J   52      54      56      57      58      .��5:      �� $	��300��6   $	��500��6   ��5;   
   �� 5<       �� ImageSource$N H/STC.Projects.WPFControlLibrary.SOPBox;component/images/big_box_blue.png��6    j� Stretch$
 Fill=�6p   ._�5?   
   � $��
15,20,15,0q�6   ��5A      �� 5B      $K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    � RowDefinitions 5E      �� 5F      $
��Auto �6    60   �� 5G      $��15 �6    6.   �� 5H      $
��Auto �6    60   �� 5I      $
��Auto �6    60   �� 5J      6"    � ColumnDefinitions 5M      �� 5N      $
��Auto �6#   62   �� 5O      6%   .+�5R      �� @   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   strCameraDetails#��Z 6   $��0��6G   ���#00ffcc$��0��6i   $��20(�6y   $��	Helvetica)�6�   $��Center=�6�   $	��Top=�6�   $��	18,19,7,0q�6�   $��2��6�   �� 5T      -   5         ��Name$ Btnback��5T      $��0��6&   $��1��63   $��5q�6^   $��20��6i   $��20��6t   $��Right=�6�   $��Disabled=�6�   $	��Top=�6�   #���  6  ��6�   Z� ��6�   Z� .��5U      �� $��18��6   $��18��6#   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6/   $��LeftToRight=�6   5V      �� 5X      $��2��6   $��Normal"�6%   $��18(�69   $��	Helvetica)�6G   ���#00ffcc$��	15,5,15,5q�6s   $��0��6�      strCCTVName#��Z 6�   6�   �� 5Y      $��2��6   $��18(�6%   ���#00ffcc$��	Helvetica)�6H   $��	15,5,15,5q�6_   $��1��6r   ��6�   ��  �� Path$ Camera.ItemName�6�   �� 5[      $��3��6   $��Normal"�6%   $��18(�69   $��	Helvetica)�6G   ���#00ffcc$��	15,5,15,5q�6s   $��0��6�      strCCTVSerialNo#��Z 6�   6�   �� 5\      $��3��6   $��18(�6%   ���#00ffcc$��	Helvetica)�6H   $��	15,5,15,5q�6_   $��1��6r   ��6�   �� $ Camera.SerialNo�6�   r� 5^      -   5        $��VideoControl��5^   L   $��4��6   $��0��6(   $��2��68    r� LoadedBehavior$
 Play=�6`   $��30,50q�6v     r� Source$9  3http://stc-connect.cloudapp.net/MediaFiles/demo.mp4?�6�   5`      5a      5b      