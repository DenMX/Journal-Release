   M S B A M L   `   `   `  ���� J  FJournal Release, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Journal_ReleaseJournal_Release       Journal_Release.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:Journal_Release     -     � Title$  
MainWindow��$	��950��$
��1000�� � Icon$" Pictures\Background_Icon.jpg��.��� .+�c� 
�������$��30��$	��Top=�$	��3,0q�.@��� -    ��Name$ ImportDB�� � ToolTip _� .��Импорт из БД���� $��Pictures\ImportDB.jpg���� -   $ 
ImportWord�� _� .��Импорт из Word.���� $��Pictures\ImportWord.jpg���� -   $ Export�� _� .��Экспорт в PDF.���� $��Pictures\ExportPDF.jpg���� -   $
 Path�� _� .��.,Текущее расположение БД.���� $��Pictures\Path.jpg���� -   $ Settings�� _� .��Настройки.���� $��Pictures\Settings.jpg���� -   $ Refresh�� _� .��Обновить.���� $��Pictures\Refresh.jpg���� -   $	 SQL�� _� .��SQL запрос.���� $��Pictures\SQL.jpg���� -	   $
 Info�� _� .��Информация.���� $��Pictures\Info.jpg���� -
    ��Name$
 Main�� J� Opacity$
 0.85Q� �� TabStripPlacement$	 Top=�$��0,25,0,0q�$
��Auto=�.@��� -   $
��list������  �� Orientation$ 
Horizontal=�.+��� Список���� $ Vertical=�$
��Auto=�$
��True��$ 3Q�.+��� .+��� $
��Auto=�.��&   System.Windows.Controls.DataGrid  -   $��listGrid��$
��Left=�$	��800��$
��True��$��1,1,1,10q�   RowBackground
 �����	   AlternatingRowBackground
	 �����$��Perpetua)�$��20(�$��Italic$�
   AutoGenerateColumns$
 False��$
��Bold"�$ 3Q� �� IsTextSearchEnabled$
 True��   
IsReadOnly$
 True��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ Имя компа��$ *  �� *PcName+  $ Дата��$ *  �� *Date+  $  Ответственный��$ *  �� *Responsibility+  $% Пароль локадмина��$ *    
FontFamily$ Consolas)� �� *	AdmPass+  $ Пароль биоса��$ * $ Consolas)� �� *
BiosPass+  $ Модель��$ *  �� *Model+�� $ 
Horizontal=�$��0q�$��Right=�.+��� $��Имя компа��$��30��$	��120��$��10, 0, 5, 0q�$
��Left=�$��Perpetua)�$
��Bold"�$��Italic$�$	 0.7Q�$��20(��� -    ��Name$ 
SearchName��$��25��$	��130��$��25��$	��130��
�������$��10, 0, 30, 0q�$
��Left=�$��Perpetua)�$
��Bold"�$��Italic$� �� TextWrapping$ NoWrap=�$	 0.7Q�$��20(��� -   $ SearchButton��$��
Поиск��$��30��$��60��$��Right=�$��10, 2, 10, 2q�$
��Bold"�
�������
���   ��� -   $��check������ $ 
Horizontal=�.+��� Чеклист���� $
��Auto=�$
��Auto=�.���� -    ��Name$ 	testCheck��.+��  � ColumnDefinitions �� �� �� ��  � RowDefinitions �� �� .+��� $��0��$
��Left=�.+��� -   $��pcType01��$��90�� �� SelectedIndex$ 1��$ Type installing��.@��� Auto�� Manual � -     �Name$ Page01��  � NavigationUIVisibility$ Hidden=�  � Source$ Manual.xaml?�$
��Left=��� $��1��$
��Left=�.+��� -   $��pcType02��$��90��$ 1��.@��� Auto�� Manual � -   $ Page02��$ Hidden=�$ Manual.xaml?�$
��Left=��� $��2��$
��Left=�.+��� -   $��pcType03��$��90��$ 1��.@��� Auto�� Manual � -   $ Page03��$ Hidden=�$ Manual.xaml?�$
��Left=��� $��3��$
��Left=�.+��� -   $��pcType04��$��90��$ 1��.@��� Auto�� Manual � -   $ Page04��$ Hidden=�$ Manual.xaml?�$
��Left=��� $��0��$��1��$
��Left=�.+��� -   $��pcType05��$��90��$ 1��.@��� Auto�� Manual � -   $ Page05��$ Hidden=�$ Manual.xaml?�$
��Left=��� $��1��$��1��$
��Left=�.+��� -   $��pcType06��$��90��$ 1��.@��� Auto�� Manual � -   $ Page06��$ Hidden=�$ Manual.xaml?�$
��Left=��� $��2��$��1��$
��Left=�.+��� -   $��pcType07��$��90��$ 1��.@��� Auto�� Manual � -   $ Page07��$ Hidden=�$ Manual.xaml?�$
��Left=��� $��3��$��1��$
��Left=�.+��� -   $��pcType08��$��90��$ 1��.@��� Auto�� Manual � -    $ Page08��$ Hidden=�$ Manual.xaml?�$
��Left=�