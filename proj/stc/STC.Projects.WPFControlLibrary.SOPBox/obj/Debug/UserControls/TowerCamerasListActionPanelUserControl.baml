   M S B A M L   `   `   `  ����o  kSTC.Projects.WPFControlLibrary.VideoStreamingControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null��clr-namespace:STC.Projects.WPFControlLibrary.VideoStreamingControl;assembly=STC.Projects.WPFControlLibrary.VideoStreamingControl4STC.Projects.WPFControlLibrary.VideoStreamingControl  5      ` \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties 5      _   YSTC.Projects.WPFControlLibrary.SOPBox.UserControls.TowerCamerasListActionPanelUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      �VideoStreamingControl�clr-namespace:STC.Projects.WPFControlLibrary.VideoStreamingControl;assembly=STC.Projects.WPFControlLibrary.VideoStreamingControl     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      -   5          >�Name$%  CamerasListActionPanelUIcontrol��5       J� RenderTransformOrigin$ 0.5,.5&�6H   z�5
      T� 5   
    T� X$	 470Q�6    � Triggers 5      :� 5   
    :� RoutedEvent$ Page.Loaded��6   .[�5      �� .��5      �� .��5      W�  �� TargetProperty$8 2(UIElement.RenderTransform).(TranslateTransform.X)�6&    W� From$	 500X�6u   	 W� To$ 0X�6�    g� Duration$ 0:0:1D�6�   	 g� 	BeginTime$	 	0:0:0.001X�6�   
 g� 
SpeedRatio$
 3Q�6�   5      5      5      5       � 	Resources 5         MyStoryboard �� 
TargetName  IsMoseOvernone �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property P� Value �� 
TargetName  IsMoseOverGrid��%�  &	        &	 �     &	 �    �� 5   
   .��5      W� $% CamerasListActionPanelUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6V   $ 0X�6�   $	 500X�6�   $ 0:0:1D�6�   $
 4Q�6�   5      �� 5   
   " ��6'   .�5       ��  � ��
 ���� 6+   �� 5!       � ��
 ���� 6,   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�62   �� 5$       � �� �#ffffff6+   �� 5%       �@�Cursor$
 Hand��6'   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5*      �� #��z��6/   #��z��6k   5+       5,      P� 5-       �@J�IsMouseOver . . �5.   "   �� $ bg��6S    � ��
 ���� �� 5/   "    � ��
 ���� 6?   �� 50   "    � ��
 ���� 6@   �� 51   "    �@J�Opacity$	 0.5Q�6<   6K   52      54      56      57      �� 59   
   " ��6'   .�5:      ��  � ��$ 	Helvetica)�6+   �� 5;       � ��
 ���� 6+   �� 5<       � �� �#008c996,   �� 5=       � ��$ Center=�64   �� 5>       � ��$ Center=�62   �� 5?       � �� �#00ffcc6+   �� 5@       �@�Cursor$
 Hand��6'   �� 5A       � �� 5B      �� 5C      " ��6&   .R�5D      �� $ bg��6!   #��z��6-   $��1q�6W   ���#008c99._�5E      �� #��z��6/   #��z��6k   5F       5G      P� 5H       �@J�IsMouseOver . . �5I   "   �� $ bg��6O    � �� �#00ffcc�� 5J   "    � ��
 �   �6?   �� 5K   "    � �� �#008c996@   5L      5N      5P      5Q      5R      .��5V      � $	��470��6   $
��Auto��6    � ColumnDefinitions 5X   
   �� 5Y      $	��260 �6   �� 5Z      .+�5]   
   �  � RowDefinitions 5^      �� 5_      $
��Auto �6    �� 5a      $
��Auto �6    ��5d      �� 5e       �� ImageSource$K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6   .+�5h      �� @  :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   strCCTVs#��Z 6   $��0��6?   $��	Helvetica)�6L   ���#00ffcc$��0��6x   $��20(�6�   $
��Left=�6�   $	��Top=�6�   $��25��6�   $	��185��6�   �� 5j      -   5         ��Name$ Btnback��5j      $��0��6&   $��0q�6N   $��20��6Y   $��20��6d   $��Right=�6p   $��Disabled=�6�   #���  6�   ��6�   Z� ��6�   Z� .��5k      �� $��18��6   $��18��6#   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6/   $��LeftToRight=�6   5l      �� 5n      $��1��6   $��1q�6"   ���#10302d �� CornerRadius$ 2��6L   ._�5o      � $��5q�6    5p      �� 5q      �� 5r      .+�5u      �� -   5         ��Name$ CamerasList��5u   #   $��13,10,10,10q�68   $��0��6M   $
��Auto��6�   $
��Auto=�5v      $
��Auto=�6I   $��0��6y    � Cursor$
 Hand��6�   ��5u   ]   �� *CamerasList+��5w      �� 5x      .R�5y   "   � $	��230��6-   $
��Auto��69    K� IsItemsHost . $��0,0,-1,0q�6Z   5z   #   5{      ��5}      �� 5~      .R�5   "   � 
������ $	��170��6@   $��5q�6L    5�   &   �� 5�   *   $
��Auto �68   �� 5�   *   $
��Auto �68    5�   &   �� 5�   *   $
��Auto �6;   �� 5�   *   .+�5�   &   ��  �� Padding$ 5q�60   $��0��6<   $��14(�6I   ���#00ffcc$��	Helvetica)�6l   $��0��6�   $
��Left=�6�   ��6�   �� *
ItemName+�� 5�   &   $��0��60   $��14(�6=   ���#00ffcc$��	Helvetica)�6`   $��1��6w   ��6�   �� *
SerialNo+G   ASTC.Projects.WPFControlLibrary.VideoStreamingControl.UserControl1  5�   &   -   5        $��1��5�   I   $��0��6�   $��2��6�   $��10q�6�   $	��170��6�   $	��150��6�       TextExposedInXaml  6�   �� *	CameraURL+�� 5�   &   $��btn_fullscreen��6-   -   5        $��Right=�5�   �   $��25��6�   $��25��6�   #���  6�   $��1��6�   ��6`   �� *	CameraURL+��6�   Z� .��5�   *   �� $��../images/maximize.png��60   5�   '   5�   #   5�      5�      �� 5�      -   5        !   
strConfirm#��Z! 5�      $��15(�6I   $��1��6X   $��0��6e   $��0,11,120,10q�6�   $	��100��6�   $��30��6�   #���  6�   �� 5�      -   5        "   strFalseDetection#��Z" 5�      $��15(�6P   $��1��6_   $��110,11,0,10q�6�   $	��110��6�   $��30��6�   #���  6�   5�      5�      5�      � 5�   
   -   5        # �Name$# GridMediaPlayer��5�      $ K� ZIndex$$ 5��6(   $y�	Collapsed=�6:   $	��420��6Q   $	��455��6]   $��	8,10,8,10q�6j   $��2��6}   ��5�      �� 5�      $K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    5�      �� 5�      $
��Auto �6    �� 5�      .+�5�      �� -	   5        $
 Hand��5�      $��15(�6#   $��25��6N   $��25��6Y   #���  6e   $��Right=�6�   .��5�      �� $J��D/STC.Projects.WPFControlLibrary.SOPBox;component/images/minimize.png��6   5�        5�      -
   5        %  Name$% light��5�   1   $��1��6@   $	��410��6�   $	��420��6�   $��5q�6�   5�      5�      5�      