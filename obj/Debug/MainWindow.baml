   M S B A M L   `   `   `  ����B  >TestApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:TestAppTestApp  5   	       TestApp.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:TestApp     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��0��6    5      �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5        $��0��5      $#��Загрузить файлы��6%   $��25��6X   $	��110��6d   $��10q�6p   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ dateFrom��5   5   $��1��6   $��10q�6)   65     5      -   5        $ dateTo��5   5   $��2��6   $��10q�6)   65   �� 5      -   5        $��3��5      $#��Выполнить поиск��6%   $��25��6[   $	��150��6g   $��10q�6s   5      �� 5   
   $��1��6   $��0��6$   .��5      � -   5         �Name$ Table��5       5      �� 5       $
��Auto �6'   �� 5!      $
��Auto �6'   �� 5"      $
��Auto �6'   �� 5#      $
��Auto �6'   �� 5$      $
��Auto �6'   �� 5%      $
��Auto �6'   �� 5&      $
��Auto �6'    5(      �� 5)      $��* �6$   .+�5+      �� $��0��6   $��0��6)   $��ID��69   $��10q�6C   �� 5,      $��0��6   $��1��6)   $��Наименование��69   $��10q�6M   �� 5-      $��0��6   $��2��6)   $��Шифр��69   $��10q�6E   �� 5.      $��0��6   $��3��6)   $"��Дата действия с��69   $��10q�6P   �� 5/      $��0��6   $��4��6)   $$��Дата действия по��69   $��10q�6Q   �� 50      $��0��6   $��5��6)   $��IsExt��69   $��10q�6F   �� 51      $��0��6   $��6��6)   $��ExtID��69   $��10q�6F   52      53      54      55      