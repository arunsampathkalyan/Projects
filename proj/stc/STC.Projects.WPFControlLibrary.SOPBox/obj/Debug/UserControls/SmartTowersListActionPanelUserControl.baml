   M S B A M L   `   `   `  ����`  \STC.Projects.WPFControlLibrary.SOPBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulls>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties0STC.Projects.WPFControlLibrary.SOPBox.Properties  5      ^    XSTC.Projects.WPFControlLibrary.SOPBox.UserControls.SmartTowersListActionPanelUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Lp>clr-namespace:STC.Projects.WPFControlLibrary.SOPBox.Properties     5      -   5          >�Name$)  #SmartTowersListActionPanelUIcontrol��5       J� Opacity$
 0.95Q�6L    J� RenderTransformOrigin$ 0.5,.5&�5      z�5
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
   .��5      W� $) #SmartTowersListActionPanelUIcontrol��6   $8 2(UIElement.RenderTransform).(TranslateTransform.X)�6Z   $ 0X�6�   $	 300X�6�   $	 0:0:1D�6�   $ 4Q�6�   5      �� 5   
   " ��6'   .�5       ��  � ��
 ���� 6+   �� 5!       � ��
 ���� 6,   �� 5"       � ��$ Center=�6;   �� 5#       � ��$ Center=�62   �� 5$       � �� �#ffffff6+   �� 5%       �@�Cursor$
 Hand��6'   �� 5&       � �� 5'      �� 5(      " ��6&   .R�5)      �� $ bg��6!   #��z��6-   $��1q�6W   
������ ._�5*      �� #��z��6/   #��z��6k   5+       5,      P� 5-       �@J�IsMouseOver . . �5.   "   �� $ bg��6S    � ��
 ���� �� 5/   "    � ��
 ���� 6?   �� 50   "    � ��
 ���� 6@   �� 51   "    �@J�Opacity$	 0.5Q�6<   6J   52      54      56      57      �� 59   
   " ��6'   .�5:      ��  � ��$ 	Helvetica)�6+   �� 5;       � ��
 ���� 6+   �� 5<       � �� �#008c996,   �� 5=       � ��$ Center=�64   �� 5>       � ��$ Center=�62   �� 5?       � �� �#00ffcc6+   �� 5@       �@�Cursor$
 Hand��6'   �� 5A       � �� 5B      �� 5C      " ��6&   .R�5D      �� $ bg��6!   #��z��6-   $��1q�6W   ���#008c99._�5E      �� #��z��6/   #��z��6k   5F       5G      P� 5H       �@J�IsMouseOver . . �5I   "   �� $ bg��6O    � �� �#00ffcc�� 5J   "    � ��
 �   �6?   �� 5K   "    � �� �#008c996@   5L      5N      5P      5Q      5R      .��5T      � $	��260��6   $	��280��6   ��5V   
   �� 5W       �� ImageSource$K E/STC.Projects.WPFControlLibrary.SOPBox;component/images/dotted_bg.png��6    � RowDefinitions 5Z   
   �� 5[      $
��Auto �6   �� 5\       � ColumnDefinitions 5_   
   �� 5`      $
��Auto �6   6.   �� 5a      6!   .+�5d   
   �� @   :STC.Projects.WPFControlLibrary.SOPBox.Properties.Resources   	strAction#��Z 6   $��0��6<   ���#00ffcc$��0��6^   $��20(�6n   $��	Helvetica)�6|   $
��Left=�6�   �� 5f   
   -   5         ��Name$ Btnback��5f      $��0��6"   $��1��6/   $��20��6Z   $��20��6e   $��Right=�6q   $��Disabled=�6�   #���  6�   ��6�   Z� ��6�   Z� .��5g      �� $��18��6   $��18��6   $L��F/STC.Projects.WPFControlLibrary.SOPBox;component/images/close_blue.png��6+   $��LeftToRight=�6{   5h      �� 5j   
   $��2��6   $��0��6   $��2��6.   ���#10302d$��1q�6X    �� CornerRadius$ 2��6l   ._�5k      � $��10,10,10,10q�6    5l      �� 5n      $
��Auto �6$   64   �� 5o      $
��Auto �6$   64   �� 5p      $
��Auto �6$   65   �� 5q      $
��Auto �6$   �� 5r      $
��Auto �6$    5t      �� 5u      6)   �� 5v      6)   .+�5y      ��    strNewMessage#��Z 6   $��16(�6H   $��0��6V   $��0��6c   $��2��6s   ���#00ffcc$��	Helvetica)�6�   $
��20,7q�6�   $��Center=�6�   $��22��6�   �� 5z      -   5        $��cmb_Actions��5z      $��16(�6.   $��1��6<   $��0��6I   $��2��6Y    �� DisplayMemberPath$ MessageDescription��6�   $��	20,7,20,7q�6�   ���#00ffcc��6m   �� *ActionsList+ �� SelectedItem 6�   �� *SelectedAction+6�   �� 5|      -   5            	strChange#��Z  5|      $��16(�6D   $��2��6R   $��0��6_   $��2��6o   $��7q�6�   $	��120��6�   $��30��6�   #���  6�   �� 5~      -   5        $��lblCurrentMsg��5~      $��3��61   $��0��6>   $��2��6N   $��16(�6b   ���#00ffcc$��15,10,15,10q�6�   $��Center=�6�   ! �� TextAlignment$! Center=�6�   $��33��6�   $��	Helvetica)�6�   ��6�   �� *OldVMS+�� 5�      -   5        "   
strConfirm#��Z" 5�      $��16(�6E   $��4��6S   $��0��6`   $��2��6p   $��0,10,120,10q�6�   $	��100��6�   $��30��6�   #���  6�   �� 5�      -   5        #   strSkip#��Z# 5�      $��16(�6B   $��4��6P   $��1��6]   $��20,10,15,10q�6�   $��75��6�   $��30��6�   #���  6�   5�      5�      5�      5�      