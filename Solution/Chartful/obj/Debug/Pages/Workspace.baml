   M S B A M L   `   `   `  ����C  ?Chartful, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Chartful.ControlsChartful.Controls  5          Chartful.Pages.Workspace   5      (Ctrlclr-namespace:Chartful.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Z VFirstFloor.ModernUI, Version=1.0.4.0, Culture=neutral, PublicKeyToken=2d21ec3cd074c59a0mui&http://firstfloorsoftware.com/ModernUI  5      -   5        $	��900��5	        � 	Resources  5         ListText �� 
TargetType$   Chartful.Controls.TextItemList �� Triggers P� Property P� Value �� Property �� Value��%�  &	        �� 5   
   "  5       5      P� 5       �@J�IsMouseOver . . �5      ��  � �� �#66666663   5      P� 5       �@ 
IsSelected . . �5      ��  � �� �#33333363   5      .�5      ��  � ��$
 Bold"�6+   �� 5       � �� �#9999996+   �� 5       � ��$ 20(�6)   �� 5       � ��$ right=�64   5      5      .��5       �   ContentRoot#��[ 6   .+�5!   
   �� $
��Auto=�6   $
��Auto=�6:   .��5"      ��  �� Orientation$ Vertical=�6   .+�5#      �� -   5        $��ItemListControl��5$      ��5#      �� *	Documents+ �� Mode$ TwoWay=���5%      �� 5&      .R�5'      � 	 � Orientation$	 
Horizontal=�6(   $��Right=�5(       5)      ��5,      �� 5-      .R�5.        $   Chartful.Controls.TextItemList
   MouseLeftButtonDown
 TextItemList_MouseClick60   #��[  51   (      TextItem 5/   (   �� *Name+$ TwoWay=�   
IsSelected 50   (   �� *
IsSelected+$ TwoWay=�51   (   52      54      � 56      $��0, 25, 0, 0q�6    � ColumnDefinitions 57      �� 58      $	��125 �6+   �� 59      $��25 �6+   �� 5:      $	��596 �6+   �� 5;      $��25 �6+   �� 5<      $��* �6+   .+�5?      �� $��0��6!   .+�5@      �� -   5        $
��Save��5@   !   $��
0, 0, 0, 0q�5A      $��25��5B      $	��Top=�5C      $
��SAVE��5D      5E      �� 5G      $��0, 10, 0, 0q�6    .��65   Document Name6D   �� 5I      -   5        $��DocumentName��5I   "   $��
0, 0, 0, 0q�5J   "   �� 5L      -   5        $��Share��5M   "   $��25��5L   "   $��0, 10, 0, 0q�5N      $	��Top=�5O      $��SHARE��5P      5Q      5R      �� 5T      $��2��6   $��1q�6-   $	��596��6A   $	��842��6N   ���#333333
�������._�5U      "   Chartful.Controls.DragCanvas  -   5          Name$ 
dragCanvas��5U   *   "   Chartful.Controls.DragCanvas   Drop$ receiver_Drop��5X   %   
������� J� 	AllowDrop . 5Y      5Z      �� 5\      $��5��6!   .+�5]      �� -   5        $��controls��5]   %   .+�5^      ��   Heading2#��[ 6(   $��Tools��6J   �� 5_      -   5        $��data1��5_   $   ���#666666$��16(�5a       .��5c   "   � .+�5d   &   A� $��5��6.   $��5��69   
�������6C   �� 5e   &   $��Title��60   5f   #   5g      5h      �� 5j      $��0, 25, 0, 0q�6%   .+�5k      �� #��[ 6(   $��
Properties��6J   $��
0, 0, 0, 5q�6\   �� 5m      -	   5        $��UIObjectList��5m   P   ��6'   �� *Selected.Objects+6d   �� 5o      $��Content��6(   ���#666666$��15(�5q   (   �� 5r      -
   5        $��TextContent��5r   &    �� TextWrapping$
 Wrap=�5s   &    �� AcceptsReturn .  �� 
AcceptsTab . $	��250��5w   &   5x      5y      5z      5{      5|      5}      5~      