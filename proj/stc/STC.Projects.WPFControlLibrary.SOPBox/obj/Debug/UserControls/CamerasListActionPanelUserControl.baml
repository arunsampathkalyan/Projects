   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      Z    TSTC.Projects.WPFControlLibrary.SOPBox.UserControls.CamerasListActionPanelUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      -   5          >�Name$%  CamerasListActionPanelUIcontrol��5       J� Opacity$
 0.95Q�5       J� RenderTransformOrigin$ 0.5,.5&�5   7   z�5
      T� 5   
    T� X$	 385Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 300X�6u   	 W� To$ 0X�6�   	 g� Duration$	 0:0:1D�6�   
 g� 	BeginTime$
 	0:0:0.001X�6�    g� 
SpeedRatio$ 3Q�6�   5      5      5      5       � 	Resources 5         MyStoryboard �� 
TargetName  IsMoseOvernone �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName  IsMoseOverGrid��%�  &	        &	 �     &	 �    �� 5   
   .��5      W� $% CamerasListActionPanelUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6V   $ 0X�6�   $	 300X�6�   $	 0:0:1D�6�   $ 4Q�6�   5      �� 5   
   " ��6'   .�5       ��  � ��
 ���� 6+   �� 5!       � ��
 ���� 6,   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�62   �� 5$       � �� �#ffffff6+   �� 5%       �@�Cursor$
 Hand��6'   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5*      �� #��z��6/   #��z��6k   5+       5,      P� 5-       �@J�IsMouseOver . . �5.   "   �� $ bg��6S    � ��
 ���� �� 5/   "    � ��
 ���� 6?   �� 50   "    � ��
 ���� 6@   �� 51   "    �@J�Opacity$	 0.5Q�6<   6J   52      54      56      57      �� 59   
   " ��6'   .�5:      ��  � ��$ 	Helvetica)�6+   �� 5;       � ��
 ���� 6+   �� 5<       � �� �#008C996,   �� 5=       � ��$ Center=�64   �� 5>       � ��$ Center=�62   �� 5?       � �� �#00ffcc6+   �� 5@       �@�Cursor$
 Hand��6'   �� 5A       � �� 5B      �� 5C      " ��6&   .R�5D      �� $ bg��6!   #��z��6-   $��1q�6W   ���#008C99._�5E      �� #��z��6/   #��z��6k   5F       5G      P� 5H       �@J�IsMouseOver . . �5I   "   �� $ bg��6O    � �� �#49b13c�� 5J   "    � �� �#00ffcc6?   �� 5K   "    � �� �#008C996@   5L      5N      5P      5Q      5R      .��5T      �� $	��300��6   ��5U   
   �� 5V       �� ImageSource$N H/STC.Projects.WPFControlLibrary.SOPBox;component/images/big_box_blue.png��6    j� Stretch$
 Fill=�6p   ._�5Y   
   � $��15,25,15,40q�6   ��5[      �� 5\      $K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    � RowDefinitions 5_      �� 5`      $
��Auto �6    60   �� 5a      $
��Auto �6    60   �� 5b      $	��410 �6    6/   �� 5c      $��38 �6     � ColumnDefinitions 5f      �� 5g      $
��Auto �6#   62   �� 5h      6%   .+�5k      �� @   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   strNearbyCamera#��Z 6   $��0��6F   ���#00ffcc$��0��6h   $��20(�6x   $��	Helvetica)�6�   $
��Left=�6�   $	��Top=�6�   $��	16,15,0,0q�6�   $��24��6�   $	��222��6�   $��3��6�   �� 5m      -   5         ��Name$ Btnback��5m      $��0��6&   $��0��63   $��0,5,10,0q�6^   $��20��6p   $��20��6{   $��Right=�6�   $��Disabled=�6�   $	��Top=�6�   #���  6  ��6�   Z� ��6  Z� .��5n      �� $��18��6   $��18��6#   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6/   $��LeftToRight=�6   5o      �� 5q      -   5         ��Name$ CamerasList��5q      $��
10,15,0,15q�60   $��2��6D   $��0��6Q   $	��250��6a   $	��380��6m   $
��Auto=�5r      $
��Auto=�6I   $
��Left=�6y    � Cursor$
 Hand��6�   ��5q   z   �� *CamerasList+��5s      �� 5t      .R�5u      � $	��250��6%   $	��400��61    K� IsItemsHost . $
��Auto=�6Q   $
��Auto=�6�   5v      5w      ��5y      �� 5z      .R�5{      � 
������ $	��170��68   $��5q�6D    5|      �� 5}   "   $��27 �60   6>   �� 5~   "   $
��Auto �60   6@   �� 5   "   62    5�      �� 5�   "   $
��Auto �63   6B   �� 5�   "   $
��Auto �63   6B   .+�5�      �� $��0��6(   $��16(�65   ���#00ffcc$��	Helvetica)�6X   $��	10,5,10,5q�6o   $��0��6�   $
��Left=�6�   ��6�   �� *
ItemName+�� 5�      $��0��6(   $��16(�65   ���#00ffcc$��	Helvetica)�6X   $��	10,5,10,5q�6o   $��1��6�   $��Right=�6�   ��6�   �� *
SerialNo+r� 5�      $��VideoControl��6\   -   5        $��1��5�   +   $��0��68   $��2��6H     r� LoadedBehavior$
  Play=�6�   $��10,10,10,10q�6�   $	��170��6�   $	��150��6�   ! r� Source$9! 3http://stc-connect.cloudapp.net/MediaFiles/demo.mp4?�6�   5�      5�      5�      �� 5�      -   5        $��flt_canvas_MediaPlayer��5�   f   " K� ZIndex$	" 500��6   $��2��6(   $y�Hidden=�65   $��Center=�6I   $��10q�6�   $��0��6�   $	��300��6�   $	��450��6�   .+�5�      r� -   5        $��FullScreenVideo��5�   C   $
  Play=�6Z   $	��290��6p   $	��450��6|   5�      �� 5�      -   5        #   strNext#��Z# 5�      $��16(�6>   $��2��6L   $��0��6Y   $	��124��6�   $��30��6�   #��[ 6�   $��71,405,125,13q�6�   $��2��6�   5�      5�      5�      