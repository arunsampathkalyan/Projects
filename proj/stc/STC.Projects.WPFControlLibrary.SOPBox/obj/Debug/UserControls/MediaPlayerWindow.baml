   M S B A M L   `   `   `  ����)  %STC.Projects.WPFControlLibrary.SOPBoxJ    DSTC.Projects.WPFControlLibrary.SOPBox.UserControls.MediaPlayerWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � 
ResizeMode$  CanMinimize=�5   	    � ShowInTaskbar .   � SizeToContent$ WidthAndHeight=�5   	   .��5      �  � Cursor$
 Hand��6   .+�5   
   �  K� ZIndex$ 6��6    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
      -   5           Command$ ApplicationCommands.Open��5
   !   6a     5      -   5        $ MediaCommands.Play��5   !   6[     5      -   5        $ MediaCommands.Pause��5   !   6]     5      -   5        $ MediaCommands.Stop��5   !   6[   .+�5      � -   5         � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $��* �6$   �� 5      $
��Auto �6$   .+�5      c� $y�	Collapsed=�6   .@�5      �� �� 5      $��MediaCommands.Play��6   $
��Play��6:   5      �� 5      $��MediaCommands.Pause��6   $��Pause��6;   5      �� 5       $��MediaCommands.Stop��6   $
��Stop��6:   5"      5#      r� 5%      -   5        $��mePlayer��5%      $��1��6/    r� LoadedBehavior$ Manual=�6<   	 r� Stretch$
	 None=�6T   �� 5'      $��2��6   ��5(      �� 5)      .R�5*      � 
 � ColumnDefinitions
 5+   "   �� 5,   &   $
��Auto �67   �� 5-   &   $
��Auto �67   �� 5.   &   $��* �67   �� 5/   &   $
��Auto �67   51      52      .@�54      �� .��55      � 
 56      �� 57   "   �� 58   "   .+�5;      �� -   5         ��Name$ btnPlay��5;   %   $��0��66   $��MediaCommands.Play��6F   $��20��6c   $��20��6o   $	��2,2q�6z   .��5<   "   �� $��../Images/play-button.png��6(   5=      �� 5>      -   5        $ btnPause��5>   %   $��0��67   $��MediaCommands.Pause��6G   $��20��6e   $��20��6q   $	��2,2q�6|   .��5?   "   �� $ ��../Images/pause-button.png��6(   5@      �� 5A      $��1��6%   $��MediaCommands.Stop��65   $��20��6R   $��20��6^   $	��2,2q�6j   .��5B   "   �� $��../Images/stop-button.png��6(   5C      5D      5E      �� 5F      $��1��6$   .��5G      �� -	   5        $��lblProgressStatus��5G   $   
00:00:006G   5H      �� 5I      $��2��6$   $��Stretch=�64   .��5J      �� -
   5        $��sliProgress��5J   !    �� IsMoveToPointEnabled . 5K   =   5L      �� 5M      $��3��6$   .��5N      � -   5        $��pbVolume��5N   &   $��50��66   $��12��6A    � Maximum$ 1Q�6M    � Value 6Y   ��  �� ElementName$ mePlayer�� �� Path$ Volume�5O      5P      5Q      5R      5S      5T      