   M S B A M L   `   `   `  ���� J  FJournal Release, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Journal_ReleaseJournal_Release  $    Journal_Release.SettingsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     /localclr-namespace:Journal_Release       � Title$  SettingsWindow��$	��500��$	��800�� � 
ResizeMode$ NoResize=� � Icon$ PIctures\Settings.jpg��.��� .+���  �� TabStripPlacement$
 Left=�$��1q�.@��� $��Программы��.���� .+���  �� Orientation$ 
Horizontal=�$��Center=�.+��� -   $��SoftType��$	��200���� -   $��+��   CancelButton#��[  �� $
��Auto=�$
��Auto=�.��&   System.Windows.Controls.DataGrid  -     Name$ SoftGrid��$	��280��$��20(�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Название��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
SoftName+  $ Путь�� �� *
SoftPath+  $ Параметры�� �� *SoftParametr+4  .System.Windows.Controls.DataGridTemplateColumn  $ Удалить��	   CellTemplate	 �� .R��� -   #��[  .��x� 
 � ColumnDefinitions
 �� $
��1.5* ��� $
��1.5* ��� ��  � RowDefinitions �� $��35 ��� $��35 ��� $��35 �.+��� $"��Название софта:��$��0��$��0���� -    ��Name$ SoftName��$��0��$��1���� $��	Путь:��$��1��$��0���� -   $ SoftPath��$��1��$��1���� -    ��Name$ SoftPathButton��$	��...��#��[  $��1��$��1��$��Center=��� $��Параметры:��$��1��$��2���� -   $ SoftParametrs��$��1��$��3���� -	   $ AddSoft��$��+��$��4��$��2���� $ 
Horizontal=�$��Right=��� $��Драйверы��.���� .+��� $ 
Horizontal=�$��Center=�.+��� -
   $��DriverModel��$	��200���� -   $��+��#��[  �� $
��Auto=�$
��Auto=�.��  -   $ DriversGrid��$	��280��$	��300��$��20(�   $ Название��   FontSize$ 20Q� �� *
DriverName+  $ Путь��$ 20Q� �� *
DriverPath+  $ Параметры�� �� *DriverParametr+  $ Удалить��	 �� .R��� -   #��[  .��x� 
 �� �� �� ��  �� $��35 ��� $��35 �.+��� $!��Название софта��$��0��$��0���� -   $ 
DriverName��$��0��$��1���� $��Путь��$��1��$��0���� -   $ 
DriverPath��$��1��$��1���� -   $	��...��#��[  $��1��$��1��$��Center=��� $��Параметры��$��1��$��2���� -   $ DriverParametrs��$��1��$��2���� -   $ 	AddDriver��$��+��$��Right=��� $��Настройки��.���� $
��Auto=�$
��Auto=�.���� $ Vertical=� �� CanVerticallyScroll$
 True�� �� CanHorizontallyScroll$
 True��$
��Auto��$
��Auto��.+��� $��PowerShell:���� -   $ PsText�� �� TextWrapping$
 Wrap=� �� AcceptsReturn$
 True�� �� 
AcceptsTab$
 True��$�� ��$ �� HorizontalScrollBarVisibility$
 Auto=�" �� VerticalScrollBarVisibility$
 Auto=�$	��655��$	��325���� $ 
Horizontal=�.+��� $��PsExec��$��Bottom=��� -   $ Path_PsExec���� -   $��+��#��[  $��Bottom=��� $��Путь скриптов��$��Bottom=��� -   $��ScriptFolder���� -   $
 Save��$��Сохранить��$	��120���� $
��BIOS��$
��Auto=�$
��Auto=�.��]� .+�� 
 �� $��* ��� $��* � �� $��30 ��� $��* ��� $��* ��� $��40 �.+��� -   $��	biosModel��$��2���� -   $ biosSettingsBox��$��1��$��2��$
 Wrap=�$
 True��$
 True��$
��Left=� �� MinLines$ 2��$��0q�$	��330��$	��325��$
 Auto=�$
 Auto=� �� AutoWordSelection$
 True���� -   $ setBiosPassword��$��1��$��1��$
 Wrap=�$
 True��$
 True��$ 2��$	��160��$	��335��$��0q�$
��Auto=�$
��Auto=�.�� � ToolTip �� $(��"PS script installing bios password��.���� -   $ resetBiosPassword��$��2��$��1��$
 Wrap=�$
 True��$
 True��$	��160��$	��335��$��0q�$
��Auto=�$
��Auto=�.�� �� $'��!PS script for reset BIOS password��.���� -   $��Сохранить��$��1��$��3��$	��100��$��30��