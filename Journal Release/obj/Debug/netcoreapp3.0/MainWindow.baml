   M S B A M L   `   `   `  ����J  FJournal Release, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Journal_ReleaseJournal_Release  5   	        Journal_Release.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Journal_Release     5   	   -   5          � Title$  
MainWindow��5   	   $	��950��6   $
��1000��6*    � Icon$" Pictures\Background_Icon.jpg��67   .��5	      � .+�5
   
   c� 
�������$��30��6%   $	��Top=�64   $	��3,0q�6L   .@�5      �� -   5         ��Name$ ImportDB��5       � ToolTip 5      _� 5      .��6   Импорт из БД5      ��5      �� 5      $��Pictures\ImportDB.jpg��6   5      �� 5      -   5        $ 
ImportWord��5       5      _� 5      .��6   Импорт из Word5      .��5      �� $��Pictures\ImportWord.jpg��6   5      �� 5      -   5        $ Export��5       5      _� 5      .��6   Экспорт в PDF5!      .��5#      �� $��Pictures\ExportPDF.jpg��6   5$      �� 5%      -   5        $
 Path��5%       5&      _� 5'      .��6   .,Текущее расположение БД67   .��5)      �� $��Pictures\Path.jpg��6   5*      �� 5+      -   5        $ Settings��5+       5,      _� 5-      .��6   Настройки6)   .��5/      �� $��Pictures\Settings.jpg��6   50      �� 51      -   5        $ Refresh��51       52      _� 53      .��6   Обновить55      .��57      �� $��Pictures\Refresh.jpg��6   58      �� 59      -   5        $	 SQL��59       5:      _� 5;      .��6   SQL запрос6*   .��5=      �� $��Pictures\SQL.jpg��6   5>      �� 5?      -	   5        $
 Info��5?       5@      _� 5A      .��6   Информация6*   .��5C      �� $��Pictures\Info.jpg��6   5D      5E      �� 5G   
   -
   5         ��Name$
 Main��5G   $    J� Opacity$
 0.85Q�6    �� TabStripPlacement$	 Top=�62   $��0,25,0,0q�6J   $
��Auto=�6\   .@�5I      �� -   5        $
��list��5I      ��5J      �� 5K       �� Orientation$ 
Horizontal=�6!   .+�5L      �� Список6,   5M      ��5O      �� 5P      $ Vertical=�6!   $
��Auto=�68   $
��True��6h   $ 3Q�6�   .+�5Q      �� .+�5R      �� $
��Auto=�6+   .��5S      &   System.Windows.Controls.DataGrid  -   5        $��listGrid��5S   (   $
��Left=�68   $	��800��6S   $
��True��6`   $��1,1,1,10q�6�      RowBackground
 �����	   AlternatingRowBackground
	 �����$��Perpetua)�6�   $��20(�6�   $��Italic$�6�   
   AutoGenerateColumns$
 False��6	  $
��Bold"�6%  $ 3Q�67   �� IsTextSearchEnabled$
 True��6�     
IsReadOnly$
 True��6�     Columns 5T   "   0  *System.Windows.Controls.DataGridTextColumn  5U   &   ,  &System.Windows.Controls.DataGridColumn   Header$ Id��69      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6\   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *Id+6\     5V   &   $ Имя компа��69   $ * 6g    6L   �� *PcName+6g     5W   &   $ Дата��69   $ * 6`    6G   �� *Date+6`     5X   &   $  Ответственный��69   $ * 6s    6P   �� *Responsibility+6s     5Y   &   $% Пароль локадмина��69   $ * 6o      
FontFamily$ Consolas)�6y    6S   �� *	AdmPass+6y     5Z   &   $ Пароль биоса��69   $ * 6l   $ Consolas)�6v    6O   �� *
BiosPass+6v     5[   &   $ Модель��69   $ * 6c    6I   �� *Model+6c   5]      5^      5_      �� 5`      $ 
Horizontal=�6%   $��0q�6>   $��Right=�6I   .+�5a      �� $��Имя компа��6$   $��30��68   $	��120��6G   $��10, 0, 5, 0q�6V   $
��Left=�6k   $��Perpetua)�6�   $
��Bold"�6�   $��Italic$�6�   $	 0.7Q�6�   $��20(�6�   �� 5b      -   5         ��Name$ 
SearchName��5b   &   $��25��6:   $	��130��6F   $��25��6R   $	��130��6a   
�������$��10, 0, 30, 0q�6�   $
��Left=�6�   $��Perpetua)�6�   $
��Bold"�6�   $��Italic$�6�    �� TextWrapping$ NoWrap=�6�   $	 0.7Q�6	  $��20(�6  66  �� 5c      -   5        $ SearchButton��5c   %   $��
Поиск��6;   $��30��6K   $��60��6Z   $��Right=�6h   $��10, 2, 10, 2q�6�   $
��Bold"�6�   
�������
���   �6�   5d      5e      5g      �� 5i      -   5        $��check��5i      ��5j      �� 5k      $ 
Horizontal=�6!   .+�5l      �� Чеклист6-   5m      ��5o      �� 5p      $
��Auto=�6#   $
��Auto=�6H   .��5q      �� -   5         ��Name$ 	testCheck��5q   !   .+�5r      �  � ColumnDefinitions 5s      �� 5t   "   �� 5u   "   �� 5v   "   �� 5w   "    � RowDefinitions 5z      �� 5{   "   �� 5|   "   .+�5      �� $��0��6)   $
��Left=�69   .+�5�   &   �� -   5        $��pcType01��5�   /   $��90��6?    �� SelectedIndex$ 1��6p   $ Type installing��6�   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5          �Name$ Page01��5�   ,     � NavigationUIVisibility$ Hidden=�6<     � Source$ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�      $��1��6)   $
��Left=�69   .+�5�   &   �� -   5        $��pcType02��5�   /   $��90��6?   $ 1��6J   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5        $ Page02��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�   "   $��2��6-   $
��Left=�6=   .+�5�   &   �� -   5        $��pcType03��5�   /   $��90��6?   $ 1��6p   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5        $ Page03��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�   "   $��3��6-   $
��Left=�6=   .+�5�   &   �� -   5        $��pcType04��5�   /   $��90��6?   $ 1��6p   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5        $ Page04��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�   "   $��0��6-   $��1��6=   $
��Left=�6J   .+�5�   &   �� -   5        $��pcType05��5�   /   $��90��6?   $ 1��6p   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5        $ Page05��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�   "   $��1��6-   $��1��6=   $
��Left=�6J   .+�5�   &   �� -   5        $��pcType06��5�   /   $��90��6?   $ 1��6p   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5        $ Page06��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�   "   $��2��6-   $��1��6=   $
��Left=�6J   .+�5�   &   �� -   5        $��pcType07��5�   /   $��90��6?   $ 1��6p   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -   5        $ Page07��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   �� 5�   "   $��3��6-   $��1��6=   $
��Left=�6J   .+�5�   &   �� -   5        $��pcType08��5�   /   $��90��6?   $ 1��6p   .@�5�   *   �� Auto6:   �� 5�   *   Manual6<   5�   '    � 5�   &   -    5        $ Page08��5�   ,   $ Hidden=�6<   $ Manual.xaml?�6\   $
��Left=�6q   5�   #   5�      5�      5�      5�      5�      5�      5�      