   M S B A M L   `   `   `  ����$   STC.Projects.ClassLibrary.Common;    5STC.Projects.ClassLibrary.Common.PaggingControl.Pager   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5        .��5      �   � ColumnDefinitions  5   
   �� 5	      $��25 �6   �� 5
      $��* �6   �� 5      $��25 �6   .+�5   
   �� -   5         ��Name$ btnPreviousPages��5      $��30��6+   $��25��66   
������ $��<��6[   6�   �� 5   
   -   5         ��Name$ 	pagerList��5      $��1��6*   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   6<   5      ��5      �� 5      .R�5      ��  ��Name$	 brd��6   -   5        $��25��5   *   $��30��66   
������ ���#00ffcc$��1q�6�   $��2q�6�   ._�5      ��  ��Name$ txtBlkPageNum��6$   $��Center=�6<   $��Center=�5   %   ��5   %   �� *Page+6=   5       �� Triggers 5      �� 5       �� Value$
 true��6J    �� Binding 6&   �� 	 �� Path$	 
IsSelected�.a�5      �� 
 �� 
TargetName$	
 brd��6%    �� Property � �� �� Value �#00ffcc6L   �� 5      $	
 brd��6%    � �� �#00ffcc6M   �� 5      $	
 brd��6%    �@J�Opacity$ .7Q�6I   �� 5       $
 txtBlkPageNum��6%    � ��
 �   �6V   �� 5!      $
 txtBlkPageNum��6%    � ��$ 14(�6T   5"      5$      5&      �� 5'   
   -   5        $ btnNextPages��5'      $��30��6'   $��25��62   $��2��6>   
������ $��>��5(   *   6V   5)      5*      