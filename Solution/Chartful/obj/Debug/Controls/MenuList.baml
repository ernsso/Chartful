   M S B A M L   `   `   `  ����C  ?Chartful, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Chartful.ControlsChartful.Controls  5           Chartful.Controls.MenuList   5      (Ctrlclr-namespace:Chartful.Controls  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5        � 	Resources  5
         ListText �� 
TargetType$   Chartful.Controls.TextItemList �� Triggers P� Property P� Value �� Property �� Value��%�  &	        �� 5   
   "  5       5      P� 5       �@J�IsMouseOver . . �5      ��  � �� �#66666667   5      P� 5       �@ 
IsSelected . . �5      ��  � �� �#33333363   5      .�5      ��  � ��$
 Bold"�6+   �� 5       � �� �#9999996+   �� 5       � ��$ 20(�6)   �� 5       � ��$ right=�64   5      5      .��5      � .+�5   
   �� -   5        $��ItemListControl��5       ��5      �� *	Documents+ �� Mode$ TwoWay=���5!      �� 5"      .R�5#      �  � Orientation$ 
Horizontal=�6    $��Right=�5$       5%      ��5(      �� 5)      .R�5*        $   Chartful.Controls.TextItemList	   MouseLeftButtonDown	 TextItemList_MouseClick6(   #��[  5-   (   
   TextItem
 5+   (   �� *Name+$ TwoWay=�   
IsSelected 5,   (   �� *
IsSelected+$ TwoWay=�5-   (   5.      50      51      52      