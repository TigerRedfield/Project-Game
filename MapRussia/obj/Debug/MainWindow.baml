   M S B A M L   `   `   `  ����D  @MapRussia, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:MapRussia	MapRussia  5   	       MapRussia.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:MapRussia     5   	   -   5          � WindowStyle$
  None=�5       � Title$O IИнтерактивная игра "Что я знаю о России?"��5   	   $	��400��6E   $	��400��6R   $	��400��6^   $	��400��6n    � 
ResizeMode$ NoResize=�6}    � WindowStartupLocation$ CenterScreen=�6�   .��5	      � 
���P�� � RowDefinitions 5
   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��0 �6   .+�5   
   � $��0��6   
����d� � ColumnDefinitions 5      �� 5      $��0.15* �6#   �� 5      $��* �6#   �� 5      $��0.10* �6#   �� 5      $��0.10* �6#   �� 5      $��0.10* �6#   .+�5      �� -   5         ��Name$ ButtonQ��5      $��0��6&   
�������$��	5 10 5 10q�6I   .��5      �� .+�5      �� $��?��6    $��20(�6)   5       � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%_   '��       �� 5      " ��6   .�5      �� 	 �@��CornerRadius$
 20��69   5      5       5!      �� 5"      -   5        $ ButtonExitMenu��5"      $��4��6-   $	��✖��6=   
�������$��8(�6\   $��	0 10 5 10q�6i   6|   �� 5#      -   5        $ ButtonHidden��5#      $��2��6+   
�������$��	0 10 5 10q�6O   .��5$      �� -   5         ��Name$ ImageHidden��5$      $3��-Resources/free-icon-minimize-sign-6057365.png��6-   $��5q�6d   5%      �� 5&      -   5        $ ButtonUnwrap��5&      $��3��6+   
�������$��	0 10 5 10q�6O   .��5'      �� $��Resources/move.png��6   $��5q�64   5(      �� 5*      $��Главное меню��6   $��Center=�6,   $��Center=�6I   $��30 0 0 0q�6d   $��1��6v      TitleFormat#��[  6�   5+      � 5-   
   $��1��6    5.      �� 5/      $��0 �6    �� 50      $��* �6    �� 51      $��* �6    �� 52      $��* �6    .+�54      �� -   5        $ ButtonStart��54      ���#308
��� ���$��1��6N   $��1��6^   $��100, 20q�6k   .��55      ��  �� Orientation$ 
Horizontal=�6   .+�56      �� $��Начать игру��6      
TextFormat#��[ 63   $�� 20, 0, 20, 0q�6W   57      58      �� 5:      -   5        $ ButtonRules��5:      ���#308
��� ���$��1��6N   $��2��6^   $��100, 20q�6l   .��5;      �� $ 
Horizontal=�6   .+�5<      �� $��Правила игры��6    #��[ 64   $�� 20, 0, 20, 0q�6X   5=      5>      �� 5@      -	   5        $ 
ButtonExit��5@      ���#308
��� ���$��1��6N   $��3��6^   $��100, 20q�6l   .��5A      �� $ 
Horizontal=�6   .+�5B      �� $��Выйти из игры��6    #��[ 65   $�� 20, 0, 20, 0q�6Y   5C      5D      5E      5F      5G      